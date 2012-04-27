using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database;
using Common.Libraries.Forms;
using Common.Libraries.Forms.Components;
using Common.Libraries.Forms.Retail;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Retail;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Shared;
using Pawn.Logic;

namespace Pawn.Forms
{
    public partial class ReprintTag : CustomBaseForm
    {
        public ReprintTag()
        {
            InitializeComponent();
            continueButton.Enabled = false;
            NavControlBox = new NavBox
            {
                Owner = this
            };
        }

        public NavBox NavControlBox;

        private void cancelButton_Click(object sender, EventArgs e)
        {
            //NavControlBox.Action = NavBox.NavAction.CANCEL;
            var res = MessageBox.Show("Are you sure you want to leave the Reprint Tag form?", "Pawn System Question", MessageBoxButtons.YesNo);
            if (res == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            string icn = txtICN.Text.Trim();

            if (!Utilities.IsIcnValid(icn) && !ValidateTicketNumber(icn))
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
                    List<string> searchFor = new List<string> { "" };
                    List<string> searchValues = new List<string> { icn };
                    FindItem(searchFor, searchValues);
                }
            }
            else
            {
                continueButton.Enabled = false;
            }
            if(ValidateTicketNumber(icn))
                continueButton.Enabled = true;
        }

        private bool ValidateTicketNumber(string ticketNumber)
        {
            bool validated = true;
            if (ticketNumber.Length <= 6 && !ticketNumber.Contains("."))
            {
                //check to make sure it's all numbers
                char[] ticketNumberChar = ticketNumber.ToCharArray();
                for (int i = 0; i <= ticketNumberChar.Length - 1; i++)
                {
                    if (!Char.IsNumber(ticketNumberChar[i]))
                        validated = false;
                }
            }
            return validated;
        }

       

        private void FindItem(List<string> searchFor, List<string> searchValues)
        {
            string errorText = null;
            string errorCode = null;
            List<RetailItem> searchItems;
            string searchFlag = "NORMAL";
            var dSession = GlobalDataAccessor.Instance.DesktopSession;
            RetailProcedures.SearchForItemPUR(searchFor, searchValues, dSession, searchFlag, out searchItems, out errorCode, out errorText);

            RetailItem item = null;
            ItemSearchResults searchResults = new ItemSearchResults(dSession, ItemSearchResultsMode.REPRINT_TAG);
            if (searchItems.Count == 0)
            {
                searchResults.Message = "No valid records found.  Please enter another number.";
                searchResults.ShowDialog();
                return;
            }

            if (searchItems.Count == 1)
            {
                item = searchItems[0];
                if (Item.ItemLocked(item))
                {
                    searchResults.Message = Item.ItemLockedMessage;
                    searchResults.ShowDialog();
                    return;
                }
                if (Commons.CheckICNSubItem(item.Icn))
                {
                    MessageBox.Show("Invalid ICN");
                    return;
                }
            }
            else if (searchItems.Count > 1)
            {
               //here remove items with ICNSubItem true
                SelectItems selectItems = new SelectItems(searchItems, ItemSearchResultsMode.REPRINT_TAG);
                selectItems.ErrorMessage = "The Short code entered is a duplicate. Please make your selection from the list";
                if (selectItems.ShowDialog() == DialogResult.OK)
                {
                    item = selectItems.SelectedItem;
                    if (Item.ItemLocked(item))
                    {
                        searchResults.Message = Item.ItemLockedMessage;
                        searchResults.ShowDialog();
                        return;
                    }
                    if (Commons.CheckICNSubItem(item.Icn))
                    {
                        MessageBox.Show("Invalid ICN");
                        DialogResult = DialogResult.None;
                        return;
                    }
                }
                else
                {
                    txtICN.Text = string.Empty;
                    txtICN.Focus();
                    return;
                }
            }

            if (item != null)
            {
                if (item.ItemStatus == ProductStatus.IP || item.ItemStatus == ProductStatus.PS || item.ItemStatus == ProductStatus.SOLD)
                {
                    MessageBox.Show("The ICN# entered is not a valid number.  Please try again.");
                    return;
                }
                
                ReprintTagVerify tagVerify = new ReprintTagVerify(dSession, item, ReprintVerifySender.ReprintTag);
                tagVerify.ShowDialog(this);
            }

            //BZ: 899 - After the reprint tag verify has finished, do not allow this form to close
            //- this form should only close when the user hits cancel
            //this.Close();
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
                if (this.ActiveControl == this.txtICN && this.txtICN.Enabled)
                {
                    this.continueButton_Click(null, new EventArgs());
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
