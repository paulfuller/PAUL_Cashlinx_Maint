using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Forms.Pawn.Products.DescribeMerchandise;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Objects.Pawn;
using Common.Libraries.Objects.Purchase;
using Common.Controllers.Application;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Shared;
using Pawn.Logic.DesktopProcedures;

namespace Pawn.Forms.ReleaseFingerprints
{
    public partial class VoidReleaseFingerprintsAuthorization : Form
    {
        public NavBox NavControlBox { get; set; }
        public VoidReleaseFingerprintsDataContext DataContext;

        private VoidReleaseFingerprintsAuthorization()
        {
            InitializeComponent();
        }

        public VoidReleaseFingerprintsAuthorization(VoidReleaseFingerprintsDataContext dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            LoadFormData();
        }

        public void LoadFormData()
        {
            VoidDateText.Text = DataContext.DateOfAuthorization;
            LoanBuyNumberText.Text = DataContext.LoanNumber.ToString();
            OfficerNameText.Text = DataContext.OfficerName;
            SubpoenaNumberText.Text = DataContext.SubpoenaNumber;
            CommentText.Text = DataContext.OriginalComments;
            EmployeeNumberText.Text = DataContext.EmployeeNumber;
            AuthorizationNumberText.Text = DataContext.AuthorizationNumber;
            AgencyText.Text = DataContext.Agency;
            BadgeNumberText.Text = DataContext.OfficerBadgeNumber;
            CaseNumberText.Text = DataContext.CaseNumber;
            LoanBuyNumberLabel.Text = "Loan #: ";

            VoidReasonComboBox.Items.Add("--Please Select--");
            VoidReasonComboBox.Items.Add("Added Authorization in Error");
            VoidReasonComboBox.Items.Add("Agency Request");
            VoidReasonComboBox.SelectedIndex = 0;

            MerchandiseDataGridView.AutoGenerateColumns = false;
            VoidReasonComboBox.Focus();

            if (DataContext.ReferenceCode == 2)
                getBuyData(GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber, DataContext.ReferenceNumber.ToString());
            else
                getLoanData(GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber, DataContext.ReferenceNumber.ToString());

            switch (GlobalDataAccessor.Instance.DesktopSession.TicketTypeLookedUp)
            {
                case ProductType.BUY:
                    LoanBuyNumberLabel.Text = "Buy #:";
                    var purchases = GlobalDataAccessor.Instance.DesktopSession.Purchases;
                    MerchandiseDataGridView.DataSource = purchases.SelectMany(p => p.Items).ToList();
                    break;
                case ProductType.PAWN:
                    LoanBuyNumberLabel.Text = "Loan #:";
                    var pawnLoan = GlobalDataAccessor.Instance.DesktopSession.CurrentPawnLoan;
                    MerchandiseDataGridView.DataSource = pawnLoan.Items;
                    break;
            }


        }

        public bool isFormDataValid()
        {
            return (VoidReasonComboBox.Text != "--Please Select--" && VoidCommentCustomTextBox.Text.Trim() != string.Empty);
        }


        private void buttonCancel_Click(object sender, EventArgs e)
        {
            GlobalDataAccessor.Instance.DesktopSession.ClearPawnLoan();
        }

        private void VoidSubmitButton_Click(object sender, EventArgs e)
        {
            if (isFormDataValid())
            {
                string errorCode;
                string errorText;
                if (VoidProcedures.VoidReleaseFingerprints(
                     VoidProcedures.VoidCode.VOID_RELEASEFINGERPRINTS,
                     DataContext.AuthorizationNumber,
                     GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                     VoidReasonComboBox.Text,
                     VoidCommentCustomTextBox.Text,
                     out errorCode,
                     out errorText))
                {
                    // Success
                    MessageBox.Show("Void Successful");
                }
                
                //this.NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
            }
        }

        private void VoidCommentCustomTextBox_Leave(object sender, EventArgs e)
        {
           VoidSubmitButton.Enabled = isFormDataValid();
        }

        private void VoidReasonComboBox_Leave(object sender, EventArgs e)
        {
            VoidSubmitButton.Enabled = isFormDataValid();
        }

        private bool getLoanData(string StoreNumber, string LoanNumber)
        {
            bool retValue = false;
            string errorCode;
            string errorText;
            PawnLoan pawnLoan;
            PawnAppVO pawnApplication;
            CustomerVO customerObj;

            retValue = CustomerLoans.GetPawnLoan(GlobalDataAccessor.Instance.DesktopSession,
                                                Utilities.GetIntegerValue(
                                                        StoreNumber, 0),
                                                        Utilities.GetIntegerValue(LoanNumber, 0),
                                                "0", StateStatus.BLNK, true, out pawnLoan,
                                                  out pawnApplication, out customerObj,
                                                  out errorCode, out errorText);

            if (retValue && pawnLoan != null)
            {
                GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer = customerObj;

                GlobalDataAccessor.Instance.DesktopSession.CurrentPawnLoan = pawnLoan;

                GlobalDataAccessor.Instance.DesktopSession.TicketTypeLookedUp = ProductType.PAWN;
                GlobalDataAccessor.Instance.DesktopSession.TicketLookedUp = Utilities.GetIntegerValue(LoanNumber, 0);
            }

            return retValue;
        }

        private bool getBuyData(string StoreNumber, string PurchaseNumber)
        {
            bool retValue = false;

            PurchaseVO purchaseObj = null;
            CustomerVO customerObj = null;
            string errorCode;
            string errorText;
            string tenderType;

            retValue = PurchaseProcedures.GetPurchaseData(Utilities.GetIntegerValue(StoreNumber, 0),
                                                          Utilities.GetIntegerValue(PurchaseNumber, 0),
                                                          "2", StateStatus.BLNK, "", true, out purchaseObj,
                                                          out customerObj, out tenderType, out errorCode,
                                                          out errorText);

            //Put the return data in the desktop session if found.
            if (retValue && purchaseObj != null)
            {
                GlobalDataAccessor.Instance.DesktopSession.Purchases.Clear();
                GlobalDataAccessor.Instance.DesktopSession.Purchases.Add(purchaseObj);
                if (purchaseObj.EntityType != "V" && customerObj != null)
                {
                    GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer = customerObj;
                }
                GlobalDataAccessor.Instance.DesktopSession.TicketLookedUp = Utilities.GetIntegerValue(PurchaseNumber, 0);
                GlobalDataAccessor.Instance.DesktopSession.TicketTypeLookedUp = ProductType.BUY;
            }

            return retValue;
        }

        private void MerchandiseDataGridView_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            switch (GlobalDataAccessor.Instance.DesktopSession.TicketTypeLookedUp)
            {
                case ProductType.BUY:
                    ShowBuyDetail(e);
                    break;

                case ProductType.PAWN:
                    ShowPawnItemDetail(e);
                    break;
            }
        }

        private void ShowBuyDetail(DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow myRow = MerchandiseDataGridView.Rows[e.RowIndex];
            int itemTicketNumber = GlobalDataAccessor.Instance.DesktopSession.TicketLookedUp;

            if (e.ColumnIndex == 1 && e.RowIndex >= 0)
            {
                var purchases = GlobalDataAccessor.Instance.DesktopSession.Purchases;

                string icn = Utilities.GetStringValue(myRow.Cells[ICN.Index].Value, "");

                var purchaseItems = purchases.SelectMany(purchaseVo => purchaseVo.Items).ToList();

                int iDx = purchaseItems.FindIndex(item => item.Icn == icn);

                if (iDx >= 0)
                {
                    // Need to populate pawnLoan from GetCat5
                    int iCategoryMask = GlobalDataAccessor.Instance.DesktopSession.CategoryXML.GetCategoryMask(purchaseItems[iDx].CategoryCode);
                    DescribedMerchandise dmPurchaseItem = new DescribedMerchandise(iCategoryMask);
                    Item purchaseItem = purchaseItems[iDx];
                    Item.PawnItemMerge(ref purchaseItem, dmPurchaseItem.SelectedPawnItem, true);

                    purchaseItems.RemoveAt(iDx);
                    purchaseItems.Insert(iDx, purchaseItem);

                    PurchaseVO activePurchase = purchases.Find(p => p.TicketNumber == itemTicketNumber);
                    activePurchase.Items = purchaseItems;

                    GlobalDataAccessor.Instance.DesktopSession.ActivePurchase = activePurchase;
                    DescribeItem myForm = new DescribeItem(GlobalDataAccessor.Instance.DesktopSession, CurrentContext.READ_ONLY, iDx, true)
                    {
                        SelectedProKnowMatch = purchaseItems[iDx].SelectedProKnowMatch
                    };
                    myForm.ShowDialog(this);
                }
            }
        }

        private void ShowPawnItemDetail(DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow myRow = MerchandiseDataGridView.Rows[e.RowIndex];

            if (e.ColumnIndex == 1 && e.RowIndex >= 0)
            {
                PawnLoan pawnLoan = GlobalDataAccessor.Instance.DesktopSession.CurrentPawnLoan;

                string icn = Utilities.GetStringValue(myRow.Cells[ICN.Index].Value, "");

                int iDx = pawnLoan.Items.FindIndex(p => p.Icn == icn);

                if (iDx >= 0)
                {
                    // Need to populate pawnLoan from GetCat5
                    int iCategoryMask = GlobalDataAccessor.Instance.DesktopSession.CategoryXML.GetCategoryMask(pawnLoan.Items[iDx].CategoryCode);
                    DescribedMerchandise dmPawnItem = new DescribedMerchandise(iCategoryMask);
                    Item pawnItem = pawnLoan.Items[iDx];
                    Item.PawnItemMerge(ref pawnItem, dmPawnItem.SelectedPawnItem, true);
                    pawnLoan.Items.RemoveAt(iDx);
                    pawnLoan.Items.Insert(iDx, pawnItem);
                    // End GetCat5 populate
                    //Add the current loan as the active pawn loan
                    GlobalDataAccessor.Instance.DesktopSession.PawnLoans.Insert(0, pawnLoan);
                    // Placeholder for ReadOnly DescribedItem.cs
                    DescribeItem myForm = new DescribeItem(GlobalDataAccessor.Instance.DesktopSession, CurrentContext.READ_ONLY, iDx)
                    {
                        SelectedProKnowMatch = pawnLoan.Items[iDx].SelectedProKnowMatch
                    };
                    myForm.ShowDialog(this);
                }
            }
        }

    }

    public class VoidReleaseFingerprintsDataContext
    {
        public string DateOfAuthorization { get; set; }
        public int LoanNumber { get; set; }
        public string OfficerName { get; set; }
        public string SubpoenaNumber { get; set; }
        public string OriginalComments { get; set; }
        public string EmployeeNumber { get; set; }
        public string AuthorizationNumber { get; set; }
        public string Agency { get; set; }
        public string OfficerBadgeNumber { get; set; }
        public string CaseNumber { get; set; }
        
        public int ReferenceNumber { get; set; }
        public int ReferenceCode { get; set; }
    }

}
