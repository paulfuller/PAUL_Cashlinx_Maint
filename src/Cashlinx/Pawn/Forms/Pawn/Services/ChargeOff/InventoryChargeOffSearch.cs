using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database;
using Common.Libraries.Forms.Components;
using Common.Libraries.Forms.Retail;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Retail;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Shared;
using Pawn.Logic;

namespace Pawn.Forms.Pawn.Services.ChargeOff
{
    public partial class InventoryChargeOffSearch : CustomBaseForm
    {
        public NavBox NavControlBox;

        public InventoryChargeOffSearch()
        {
            InitializeComponent();
            continueButton.Enabled = false;

        }

        public DesktopSession CDS
        {
            get { return GlobalDataAccessor.Instance.DesktopSession; }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void continueButton_Click(object sender, EventArgs e)
        {

            if (!continueButton.Enabled)
            {
                return;
            }
            string icn = txtICN.Text.Trim();

            if (!Utilities.IsIcnValid(icn))
            {
                return;
            }

            List<string> searchFor = new List<string>() { "" };
            List<string> searchValues = new List<string>() { icn };

            

            if (FindItem(searchFor, searchValues))
            {
                txtICN.Text = ""; // reset txt to simplify for entering a new charge off item
            }

        }

        private void txtICN_TextChanged(object sender, EventArgs e)
        {
            if (!isCACC(this.txtICN.Text))
            {
                txtQty.Text = "0";
                lblQty.Visible = false;
                txtQty.Visible = false;
            }
            this.customLabelError.Visible = false;
            EnableDisableContinue();
        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {
            int caccQty = 0;

            if (txtQty.Text.Trim().Length > 0 && int.TryParse(txtQty.Text, out caccQty) && caccQty > 0)
            {
                this.customLabelError.Visible = false;
                EnableDisableContinue();
            }
            else if (txtICN.Text.Length > 0)
            {
                this.customLabelError.Text = "CACC charge off must be a positive number, greater than 0.";
                this.customLabelError.Visible = true;

                this.continueButton.Enabled = false;
            }
        }

        private void EnableDisableContinue()
        {
            string icn = this.txtICN.Text.Trim();
            int qty = 0;

            if (Utilities.IsIcnValid(icn) )
            {
                if (isCACC (icn) && !txtQty.Visible)
                {
                    lblQty.Visible = true;
                    txtQty.Visible = true;

                    return;
                }

                if (!txtQty.Visible || (txtQty.Visible && txtQty.Text.Trim().Length > 0 && int.TryParse(txtQty.Text, out qty) && qty > 0))
                {
                    
                   continueButton.Enabled = true;

                    if (icn.Length == Icn.ICN_LENGTH)
                    {
                        List<string> searchFor = new List<string>() { "" };
                        List<string> searchValues = new List<string>() { icn };
                        FindItem(searchFor, searchValues);
                    }
                }
                else
                    continueButton.Enabled = false;
            }
            else
            {
                lblQty.Visible = false;
                txtQty.Visible = false;
                txtQty.Text = "0";

                continueButton.Enabled = false;
            }
        }

        private bool FindItem(List<string> searchFor, List<string> searchValues)
        {
            bool retval = false;
            string errorText = null;
            string errorCode = null;
            List<RetailItem> searchItems;
            string searchFlag = "NORMAL";
            this.customLabelError.Visible = false;
            this.customLabelError.Text = string.Empty;


            bool loadQty = this.txtQty.Visible;

            RetailProcedures.SearchForItem(searchFor, searchValues, CDS, searchFlag, loadQty, out searchItems, out errorCode, out errorText);

            RetailItem item = null;
            ItemSearchResults searchResults = new ItemSearchResults(GlobalDataAccessor.Instance.DesktopSession, ItemSearchResultsMode.CHARGEOFF);
            if (searchItems.Count == 0)
            {
                this.customLabelError.Visible = true;
                this.customLabelError.Text = " This ICN number was not found in the current shop. Please check the number and try again.";
                return retval;
 
            }

            if (searchItems.Count == 1 && searchItems[0].ItemStatus != ProductStatus.PFI)
            {

                searchResults.ShowDialog();
                return retval;
            }

            if (searchItems.Count == 1)
            {
                item = searchItems[0];
                if (Item.ItemLocked(item))
                {
                    MessageBox.Show(Item.ItemLockedMessage);
                    return retval;
                }
            }
            else if (searchItems.Count > 1)
            {
                var distinctItems = from sItem in searchItems
                                    where ((sItem.IsJewelry && sItem.Jewelry.Any(j => j.SubItemNumber == 0)) ||
                                    !sItem.IsJewelry)
                                    select sItem;
                if (distinctItems.Any())
                {
                    SelectItems selectItems = new SelectItems(searchItems, ItemSearchResultsMode.CHARGEOFF);
                    selectItems.ErrorMessage = "The Short code entered is a duplicate. Please make your selection from the list";
                    if (selectItems.ShowDialog() == DialogResult.OK)
                    {
                        item = selectItems.SelectedItem;
                        if (Item.ItemLocked(item))
                        {
                            this.customLabelError.Text = Item.ItemLockedMessage;
                            this.customLabelError.Visible = true;
                            return retval;
                        }
                        if (item.mDocType == "9")
                        {
                            this.customLabelError.Text = "Item is not eligible for Charge off.";
                            this.customLabelError.Visible = true;
                            return retval;
                        }
                        else if (item.Icn.Substring(Icn.ICN_LENGTH - 1) != "0")
                        {
                            this.customLabelError.Text = "Item is not eligible for Charge off.";
                            this.customLabelError.Visible = true;
                            return retval;
                        }

                        if (item.HoldType == HoldTypes.POLICEHOLD.ToString())
                        {
                            this.customLabelError.Visible = true;
                            this.customLabelError.Text = "This merchandise is on Police Hold. The Police Hold must be released before the item can be charged off.";
                            return retval;
                        }

                    }
                    else
                    {
                        return retval;
                    }
                }
                else
                {
                    item = searchItems[0];
                    if (Item.ItemLocked(item))
                    {
                        MessageBox.Show(Item.ItemLockedMessage);
                        return retval;
                    }
                }
            }

            if (item != null)
            {
                if (item.HoldType == HoldTypes.POLICEHOLD.ToString())
                {
                    this.customLabelError.Text = "This merchandise is on Police Hold. The Police Hold must be released before the item can be charged off.";
                    this.customLabelError.Visible = true;
                    return retval;
                }
                else if (item.mDocType == "9")
                {
                    this.customLabelError.Text = "Cannot charge off NXT items.";
                    this.customLabelError.Visible = true;
                    return retval;
                }
                else if (item.Icn.Substring(Icn.ICN_LENGTH - 1) != "0")
                {
                    this.customLabelError.Text = "Cannot charge off sub items.";
                    this.customLabelError.Visible = true;
                    return retval;
                }
                else if (int.Parse(this.txtQty.Text) > item.Quantity) 
                {
                    this.customLabelError.Text =  item.Quantity + " items were found for this CACC category.  Please revise the charge off quantity.";
                    this.customLabelError.Visible = true;

                    this.continueButton.Enabled = false;

                    return retval;
                }
                else
                {
                    //if (txtQty.Visible)
                    if (isCACC(this.txtICN.Text))
                    {

                        int qty= 0;

                        if (int.TryParse(txtQty.Text, out qty) && (item.Quantity > 0))
                        {
                            //do we need to check to make sure DB didn't give us 0 or null 
                            decimal qtyRatio = (qty / Convert.ToDecimal(item.Quantity));
                            item.ItemAmount = item.PfiAmount * qtyRatio; // item.ItemAmount * qty; // 
                            item.Quantity = qty; // int.Parse(this.txtQty.Text);
                        }

                    }

                    ChargeOffDetails detailsForm = new ChargeOffDetails
                                                   {
                                                       ChargeOffItem = item
                                                   };
                    detailsForm.ShowDialog();

                    retval = (detailsForm.DialogResult == DialogResult.OK);
                    if (retval) 
                        txtICN.Text = string.Empty;
                }
            }

            return retval;
        }



        private void ChangeRetailPriceSearch_Shown(object sender, EventArgs e)
        {
            txtICN.Text = string.Empty;
            txtICN.Focus();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.cancelButton_Click(null, new EventArgs());
                return true;
            }

            if (keyData == Keys.Enter)
            {
                if (this.ActiveControl == this.txtICN || (isCACC(txtICN.Text) && this.ActiveControl == this.txtQty))
                {
                    this.continueButton_Click(null, new EventArgs());
                }
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void InventoryChargeOffSearch_Load(object sender, EventArgs e)
        {
            txtICN.Text = string.Empty;
        }

        private void txtICN_Leave(object sender, EventArgs e)
        {
            string icn = this.txtICN.Text.Trim();

            if (Utilities.IsIcnValid(icn) && isCACC (icn))
            {
                lblQty.Visible = true;
                txtQty.Visible = true;
            }
            else
            {
                lblQty.Visible = false;
                txtQty.Visible = false;
                txtQty.Text = "0";
            }

        }

        private bool isCACC (string icn)
        {
            bool retval = false;
            
            Icn thisIcn = new Icn();

            thisIcn.ParseIcn(icn);


            if (thisIcn.DocumentNumber == 3362 || thisIcn.DocumentNumber == 3363 ||
                thisIcn.DocumentNumber == 3350 || thisIcn.DocumentNumber == 3262 ||
                thisIcn.DocumentNumber == 3380)

                retval = true;

            return retval;
        }

        private void txtQty_Leave(object sender, EventArgs e)
        {
            //int caccQty = 0;

            //if (txtQty.Text.Trim().Length > 0 && int.TryParse(txtQty.Text, out caccQty) && caccQty > 0)
            //{
            //    this.customLabelError.Visible = false;
            //    EnableDisableContinue();                   
            //}
            //else
            //{
            //    this.customLabelError.Text = "CACC charge off must be a positive integer, greater than 0.";
            //    this.customLabelError.Visible = true;
            //}
        }
    }
}
