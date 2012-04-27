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
            string icn = txtICN.Text.Trim();

            if (!Utilities.IsIcnValid(icn))
            {
                return;
            }

            List<string> searchFor = new List<string>() { "" };
            List<string> searchValues = new List<string>() { icn };

            FindItem(searchFor, searchValues);
        }

        private void txtICN_TextChanged(object sender, EventArgs e)
        {
            EnableDisableContinue();
        }

        private void EnableDisableContinue()
        {
            string icn = this.txtICN.Text.Trim();

            if (Utilities.IsIcnValid(icn))
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
            {
                continueButton.Enabled = false;
            }
        }

        private void FindItem(List<string> searchFor, List<string> searchValues)
        {
            string errorText = null;
            string errorCode = null;
            List<RetailItem> searchItems;
            string searchFlag = "NORMAL";
            this.customLabelError.Visible = false;
            this.customLabelError.Text = string.Empty;
            RetailProcedures.SearchForItem(searchFor, searchValues, CDS, searchFlag, false, out searchItems, out errorCode, out errorText);

            RetailItem item = null;
            ItemSearchResults searchResults = new ItemSearchResults(GlobalDataAccessor.Instance.DesktopSession, ItemSearchResultsMode.CHARGEOFF);
            if (searchItems.Count == 0)
            {
                this.customLabelError.Visible = true;
                this.customLabelError.Text = " This ICN number was not found in the current shop. Please check the number and try again.";
                return;
 
            }

            if (searchItems.Count == 1 && searchItems[0].ItemStatus != ProductStatus.PFI)
            {

                searchResults.ShowDialog();
                return;
            }

            if (searchItems.Count == 1)
            {
                item = searchItems[0];
                if (Item.ItemLocked(item))
                {
                    MessageBox.Show(Item.ItemLockedMessage);
                    return;
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
                            return;
                        }
                        if (item.mDocType == "9")
                        {
                            this.customLabelError.Text = "Item is not eligible for Charge off.";
                            this.customLabelError.Visible = true;
                            return;
                        }
                        else if (item.Icn.Substring(Icn.ICN_LENGTH - 1) != "0")
                        {
                            this.customLabelError.Text = "Item is not eligible for Charge off.";
                            this.customLabelError.Visible = true;
                            return;
                        }

                        if (item.HoldType == HoldTypes.POLICEHOLD.ToString())
                        {
                            this.customLabelError.Visible = true;
                            this.customLabelError.Text = "This merchandise is on Police Hold. The Police Hold must be released before the item can be charged off.";
                            return;
                        }

                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    item = searchItems[0];
                    if (Item.ItemLocked(item))
                    {
                        MessageBox.Show(Item.ItemLockedMessage);
                        return;
                    }
                }
            }

            if (item != null)
            {
                if (item.HoldType == HoldTypes.POLICEHOLD.ToString())
                {
                    this.customLabelError.Text = "This merchandise is on Police Hold. The Police Hold must be released before the item can be charged off.";
                    this.customLabelError.Visible = true;
                    return;
                }
                else if (item.mDocType == "9")
                {
                    this.customLabelError.Text = "Cannot charge off NXT items.";
                    this.customLabelError.Visible = true;
                    return;
                }
                else if (item.Icn.Substring(Icn.ICN_LENGTH - 1) != "0")
                {
                    this.customLabelError.Text = "Cannot charge off sub items.";
                    this.customLabelError.Visible = true;
                    return;
                }
                else
                {
                    ChargeOffDetails detailsForm = new ChargeOffDetails
                                                   {
                                                       ChargeOffItem = item
                                                   };
                    detailsForm.ShowDialog();
                }
            }
 
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
                if (this.ActiveControl == this.txtICN)
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
    }
}
