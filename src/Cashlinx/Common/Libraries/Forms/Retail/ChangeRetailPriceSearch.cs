using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database;
using Common.Libraries.Forms.Components;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Retail;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Shared;

namespace Common.Libraries.Forms.Retail
{
    public partial class ChangeRetailPriceSearch : CustomBaseForm
    {
        public NavBox NavControlBox;

        private ChangeRetailPriceSearch()
        {
            InitializeComponent();
        }

        public ChangeRetailPriceSearch(DesktopSession desktopSession)
        {
            DesktopSession = desktopSession;
            InitializeComponent();
            continueButton.Enabled = false;
            NavControlBox = new NavBox
            {
                Owner = this
            };
        }

        public DesktopSession DesktopSession { get; private set; }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            foreach (RetailItem item in DesktopSession.ActiveRetail.RetailItems)
            {
                LockUnlockRetailItem(item, false);
            }
            var res = MessageBox.Show("Are you sure you want to exit the Change Retail Price form?", "Pawn System Question",
                                      MessageBoxButtons.YesNo);
            if (res == DialogResult.Yes)
            {
                NavControlBox.Action = NavBox.NavAction.CANCEL;
            }
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            string icn = txtICN.Text.Trim();

            if (!Utilities.IsIcnValid(icn))
            {
                return;
            }

            var searchFor = new List<string> { string.Empty };
            var searchValues = new List<string> { icn };

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
                    var searchFor = new List<string> { string.Empty };
                    var searchValues = new List<string> { icn };
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

            RetailProcedures.SearchForItem(searchFor, searchValues, DesktopSession, searchFlag, false, out searchItems, out errorCode, out errorText);

            RetailItem item = null;
            ItemSearchResults searchResults = new ItemSearchResults(DesktopSession, ItemSearchResultsMode.CHANGE_RETAIL_PRICE);
            if (searchItems.Count == 0)
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
                if (Commons.CheckICNSubItem(item.Icn))
                {
                    MessageBox.Show("Invalid ICN");
                    return;
                }
            }

            if (searchItems.Count == 1 && searchItems[0].ItemStatus != ProductStatus.PFI)
            {
                searchResults.ShowDialog();
                return;
            }

            if (searchItems.Count == 1)
            {
                item = searchItems[0];
            }
            else if (searchItems.Count > 1)
            {
                var distinctItems = (from sItem in searchItems
                                    where ((sItem.IsJewelry && sItem.Jewelry.Any(j => j.SubItemNumber == 0)) ||
                                    !sItem.IsJewelry)
                                    select sItem).ToList();

                if (distinctItems.Count == 0)
                {
                    //item = selectItems.SelectedItem;
                    item = searchItems.First();
                    if (Item.ItemLocked(item))
                    {
                        MessageBox.Show(Item.ItemLockedMessage);
                        return;
                    }
                    //item = selectItems.SelectedItem;
                    if (Commons.CheckICNSubItem(item.Icn))
                    {
                        MessageBox.Show("Invalid ICN");
                        return;
                    }
                }
                else
                {
                   
                    SelectItems selectItems = new SelectItems(searchItems, ItemSearchResultsMode.CHANGE_RETAIL_PRICE);
                    selectItems.ErrorMessage = "The Short code entered is a duplicate. Please make your selection from the list";
                    if (selectItems.ShowDialog() == DialogResult.OK)
                    {
                        item = selectItems.SelectedItem;
                        if (Item.ItemLocked(item))
                        {
                            MessageBox.Show(Item.ItemLockedMessage);
                            return;
                        }
                        if (Commons.CheckICNSubItem(item.Icn))
                        {
                            MessageBox.Show("Invalid ICN");
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
            }

            LockUnlockRetailItem(item, true);

            DesktopSession.ActiveRetail = new SaleVO();
            DesktopSession.ActiveRetail.RetailItems.Add(item);

            //BZ: 899 - Allow the submit, form changes are handled in the flow executor
            NavControlBox.Action = NavBox.NavAction.SUBMIT;
        }

        private void LockUnlockRetailItem(RetailItem item, bool lockItem)
        {
            string errorText = null;
            string errorCode = null;
            if (item.mDocType != "7")
                RetailProcedures.LockItem(DesktopSession, item.Icn, out errorCode, out errorText, lockItem ? "Y" : "N");
        }

        private void ChangeRetailPriceSearch_Shown(object sender, EventArgs e)
        {
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
    }
}
