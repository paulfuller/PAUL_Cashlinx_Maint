using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Database;
using Common.Libraries.Forms.Components;
using Common.Libraries.Forms.Retail;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Retail;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Shared;
using Pawn.Forms.Pawn.Services.PFI;
using Pawn.Logic;

namespace Pawn.Forms.Retail
{
    public partial class ChangeItemAssignmentType : CustomBaseForm
    {
        #region Private Fields
        RetailItem _item = null;

        #endregion

        # region Private Properties
        private DesktopSession CDS
        {
            get
            {
                return GlobalDataAccessor.Instance.DesktopSession;
            }
        }

        #endregion

        #region Constructor
        public ChangeItemAssignmentType()
        {
            InitializeComponent();
        }

        #endregion

        #region Event Handler Methods

        private void findButton_Click(object sender, EventArgs e)
        {
            string icn = txtICN.Text.Trim();

            List<string> searchFor = new List<string>() { "" };
            List<string> searchValues = new List<string>() { icn };
            if (!Utilities.IsIcnValid(icn))
                return;
            FindItem(searchFor, searchValues);
        }

        private void txtICN_TextChanged(object sender, EventArgs e)
        {
            EnableDisableFind();
        }

        private void customButtonSubmit_Click(object sender, EventArgs e)
        {
            //
            //pawn retail //update_mdse_trantype
            string errorCode;
            string errorText;
            //here convert the value from the drop down
            bool retVal = RetailProcedures.UpdateItem(CDS, _item, out errorCode, out errorText);

            if (!retVal)
            {
                MessageBox.Show(errorText);
                return;
            }
            if (!_item.PfiAssignmentType.Equals(PfiAssignment.Refurb))
            {
                if (_item.PfiTags == 0)
                    _item.PfiTags = 1;
                GlobalDataAccessor.Instance.DesktopSession.PrintTags(_item, CurrentContext.READ_ONLY);
            }
            assignmentTypeCombo.SelectedIndexChanged -= new EventHandler(this.assignmentTypeCombo_SelectedIndexChanged);
            _item = null;
            DisplayItem();
            txtICN.Text = string.Empty;
            txtICN.Focus();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (this.ActiveControl.Equals(txtICN))
                {
                    findButton.PerformClick();
                    return true;
                }
                else if (this.ActiveControl.Equals(customButtonCancel2))
                {
                    customButtonCancel2.PerformClick();
                    return true;
                }
                else
                {
                    customButtonSubmit.PerformClick();
                    return true;
                }
            }
            else if (keyData == Keys.Escape)
            {
                customButtonCancel2.PerformClick();
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        #endregion

        #region Private Helper Methods
        private void ReloadItem()
        {
        }

        private void EnableDisableFind()
        {
            string icn = this.txtICN.Text.Trim();
            if (Utilities.IsIcnValid(icn))
            {
                customButtonSubmit.Enabled = true;

                if (icn.Length == Icn.ICN_LENGTH)
                {
                    List<string> searchFor = new List<string>() { "" };
                    List<string> searchValues = new List<string>() { icn };
                    FindItem(searchFor, searchValues);
                }
            }
            else
            {
                customButtonSubmit.Enabled = false;
            }
        }

        private void FindItem(List<string> searchFor, List<string> searchValues)
        {
            string errorText = null;
            string errorCode = null;
            List<RetailItem> searchItems;
            string searchFlag = "NORMAL";

            //BZ # 441
            if (txtICN.TextLength == 18)
            {
                string subItemNum = txtICN.Text.Substring(txtICN.Text.Length - 2);
                if (Convert.ToInt32(subItemNum) > 0)
                {
                    MessageBox.Show("You have entered an ICN of SubItem. The assignment type for this item cannot be updated.");
                    return;
                }
            }
            //BZ # 441 - end

            RetailProcedures.SearchForItem(searchFor, searchValues, CDS, searchFlag, false, out searchItems, out errorCode, out errorText);

            if (searchItems.Count == 0)
            {
                ShowIcnDoesNotExistMessage();
                return;
            }
         
            if (searchItems.Count == 1 && !IsAssignmentTypeChangeAllowed(searchItems[0]))
            {
                MessageBox.Show("You have entered an ICN for CACC or NXT. The assignment type for this item cannot be changed.");
                return;
            }

            List<RetailItem> allowItems = searchItems.FindAll(i => IsAssignmentTypeChangeAllowed(i));

            if (allowItems.Count == 1)
            {
                _item = allowItems[0];
                if (Item.ItemLocked(_item))
                {
                    MessageBox.Show(Item.ItemLockedMessage);
                    return;
                }
            }
            else if (allowItems.Count > 1)
            {
                var distinctItems = (from sItem in allowItems
                                     where ((sItem.IsJewelry && sItem.Jewelry.Any(j => j.SubItemNumber == 0)) ||
                                     !sItem.IsJewelry)
                                     select sItem).ToList();

                if (distinctItems.Count == 0)
                {
                    _item = searchItems.First();
                    if (Item.ItemLocked(_item))
                    {
                        MessageBox.Show(Item.ItemLockedMessage);
                        return;
                    }
                }
                else
                {
                    SelectItems selectItems = new SelectItems(allowItems, ItemSearchResultsMode.CHANGE_ITEM_ASSIGNMENT_TYPE);
                    if (selectItems.ShowDialog() == DialogResult.OK)
                    {
                        _item = selectItems.SelectedItem;
                        if (Item.ItemLocked(_item))
                        {
                            MessageBox.Show(Item.ItemLockedMessage);
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
            }

            //here write code to display item in another mode
            DisplayItem();
        }

        private void ShowIcnDoesNotExistMessage()
        {
            MessageBox.Show("The ICN# does not exist.  Please try another number.");
        }

        private bool IsAssignmentTypeChangeAllowed(Item item)
        {
            Icn icn = new Icn(item.Icn);
            return icn.DocumentType == DocumentType.Acquisition || icn.DocumentType == DocumentType.Loan || icn.DocumentType == DocumentType.MerchandisePurchaseReceipt;
        }

        private void LoadAssignmentDropdown()
        {
            if (_item == null)
            {
                return;
            }

            PfiAssignment pf = Utilities.AssignmentTypeFromString(_item.TranType);
            labelCurrentAssignmentType.Text = Utilities.AssignmentTypeFullName(pf);
            assignmentTypeCombo.Items.Clear();
            assignmentTypeCombo.Items.Add(new AssignmentTypePair(PfiAssignment.Normal, "Normal"));
            assignmentTypeCombo.Items.Add(new AssignmentTypePair(PfiAssignment.Sell_Back, "Sell_Back"));
            if (_item.IsJewelry)
            {
                assignmentTypeCombo.Items.Add(new AssignmentTypePair(PfiAssignment.Scrap, "Scrap"));
                assignmentTypeCombo.Items.Add(new AssignmentTypePair(PfiAssignment.Refurb, "Refurb"));
                //BZ # 441
                //assignmentTypeCombo.Items.Add(new AssignmentTypePair(PfiAssignment.Wholesale, "WholeSale"));
                assignmentTypeCombo.Items.Add(new AssignmentTypePair(PfiAssignment.Excess, "Excess"));
                //BZ # 441 - end
            }
            else if (_item.IsGun)
            {
                assignmentTypeCombo.Items.Add(new AssignmentTypePair(PfiAssignment.CAF, "CAF"));
            }
            //BZ # 441
            /*else if (_item.ItemReason != ItemReason.CACC)
            {
            assignmentTypeCombo.Items.Add(new AssignmentTypePair(PfiAssignment.Wholesale, "WholeSale"));
            }*/
            //BZ # 441 - end
            try
            {
                int selectedIndex = 0;
                AssignmentTypePair selectedPair = null;
                foreach (AssignmentTypePair pair in assignmentTypeCombo.Items)
                {
                    if (pair.AssignmentTypeProperty == pf)
                    {
                        selectedPair = pair;
                        break;
                    }
                }
                if (selectedPair != null)
                    selectedIndex = assignmentTypeCombo.Items.IndexOf(selectedPair);
                assignmentTypeCombo.SelectedIndex = selectedIndex;
            }
            catch (Exception)
            {
                return;
            }
            //if(item.IsJewelry)
            assignmentTypeCombo.SelectedIndexChanged += new EventHandler(this.assignmentTypeCombo_SelectedIndexChanged);
        }

        private void DisplayItem()
        {
            panelItem.Visible = _item != null;
            customButtonSubmit.Visible = _item != null;
            LoadAssignmentDropdown();
            labelItemDescription.Text = _item == null ? string.Empty : _item.TicketDescription;
            //labelCurrentAssignmentType.Text = _item.MerchandiseType;
        }

        #endregion

        #region Helper Classes
        public class AssignmentTypePair
        {
            public AssignmentTypePair(PfiAssignment assignmentType, string description)
            {
                AssignmentTypeProperty = assignmentType;
                Description = description;
            }

            public PfiAssignment AssignmentTypeProperty { get; set; }

            public string Description { get; set; }

            public override string ToString()
            {
                return Description;
            }
        }

        #endregion

        private void assignmentTypeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox assignmentTypeCombo = sender as ComboBox;
            AssignmentTypePair assignmentTypePair = assignmentTypeCombo.SelectedItem as AssignmentTypePair;
            PfiAssignment assignmentType = assignmentTypePair.AssignmentTypeProperty;

            if (!_item.PfiAssignmentType.Equals(assignmentType) && assignmentType.Equals(PfiAssignment.Refurb))
            {
                PFI_AssignmentTypeRefurb refurbForm = new PFI_AssignmentTypeRefurb();
                refurbForm.Item = _item;
                refurbForm.ShowDialog();
            }
            _item.PfiAssignmentType = assignmentType;
            if (!_item.PfiAssignmentType.Equals(PfiAssignment.Refurb))
            {
                _item.RefurbNumber = 0;
            }
        }
    }
}
