using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database.Couch;
using Common.Controllers.Database.Procedures;
using Common.Controllers.Security;
using Common.Libraries.Forms.Components;
using Common.Libraries.Forms.Pawn.Products.DescribeMerchandise;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Objects.Purchase;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using Common.Libraries.Objects.Pawn;
using Reports;

namespace Pawn.Forms.ReleaseFingerprints
{
    public partial class ReleaseFingerprintsAuthorization : Form
    {
        private bool isFormValid = false;

        public NavBox NavControlBox;// { get; set; }

        public ReleaseFingerprintsAuthorization()
        {
            InitializeComponent();
            this.NavControlBox = new NavBox();
        }

        private void ReleaseFingerprintsAuthorization_Load(object sender, EventArgs e)
        {
            this.NavControlBox.Owner = this;

            loadFormData();
        }

        private void loadFormData()
        {
            LoanBuyNumberLabelText.Text = GlobalDataAccessor.Instance.DesktopSession.TicketLookedUp.ToString();

            switch (GlobalDataAccessor.Instance.DesktopSession.TicketTypeLookedUp)
            {
                case ProductType.BUY:
                    LoanBuyNumberLabel.Text = "Buy #:";
                    var purchases = GlobalDataAccessor.Instance.DesktopSession.Purchases;
                    MerchandiseDataGridView.AutoGenerateColumns = false;
                    MerchandiseDataGridView.DataSource = purchases.SelectMany(purchaseVo => purchaseVo.Items).ToList();
                    break;

                case ProductType.PAWN:
                    LoanBuyNumberLabel.Text = "Loan #:";
                    var pawnLoan = GlobalDataAccessor.Instance.DesktopSession.CurrentPawnLoan;
                    MerchandiseDataGridView.AutoGenerateColumns = false;
                    MerchandiseDataGridView.DataSource = pawnLoan.Items;
                    break;
            }


        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            GlobalDataAccessor.Instance.DesktopSession.ClearPawnLoan();
            this.NavControlBox.Action = NavBox.NavAction.CANCEL;
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            GlobalDataAccessor.Instance.DesktopSession.ClearPawnLoan();
            this.NavControlBox.Action = NavBox.NavAction.BACK;
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

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            if (isFormValid)
            {
                submitForm();
            }
        }

        private void submitForm()
        {
            validateForm();

            if (isFormValid)
            {

                bool returnValue = false;
                DialogResult dgr = DialogResult.Retry;

                CustomerVO currentCustomer = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer;

                string currDate = ShopDateTime.Instance.ShopDate.FormatDate();
                string currDateTime = currDate + " " + ShopDateTime.Instance.ShopTime.ToString();

                ReleaseFingerprintsInfo releaseFingerprintsInfo = new ReleaseFingerprintsInfo()
                                                                      {
                                                                          RefType =
                                                                              GlobalDataAccessor.Instance.DesktopSession
                                                                                  .TicketTypeLookedUp == ProductType.BUY
                                                                                  ? "2"
                                                                                  : "1",
                                                                          RefNumber =
                                                                              GlobalDataAccessor.Instance.DesktopSession
                                                                              .TicketLookedUp,
                                                                          Comment = CommentText.Text,
                                                                          StoreNumber =
                                                                              GlobalDataAccessor.Instance.CurrentSiteId.
                                                                              StoreNumber,
                                                                          SubpoenaNumber = SubpoenaNumberText.Text,
                                                                          OfficerFirstName = OfficerFirstNameText.Text,
                                                                          OfficerLastName = OfficerLastNameText.Text,
                                                                          BadgeNumber = BadgeNumberText.Text,
                                                                          CaseNumber = CaseNumberText.Text,
                                                                          Agency = AgencyText.Text,
                                                                          TransactionDate = currDateTime,
                                                                          SeizeStatus = "FPRNT"
                                                                      };

                do
                {
                    int seizeNumber = 0;

                    returnValue = HoldsProcedures.AddReleaseFingerprints(releaseFingerprintsInfo, 
                                                                         out seizeNumber);

                    if (returnValue && seizeNumber > 0)
                    {
                        MessageBox.Show("Transaction completed successfully");
                        //Print release fingerprints document

                        //Print authorization to release fingerprints form
                        printAuthorizationToReleaseFingerprints(releaseFingerprintsInfo, currentCustomer, seizeNumber);

                        break;
                    }
                    dgr = MessageBox.Show(Commons.GetMessageString("ProcessingError"), "Error",
                                          MessageBoxButtons.RetryCancel);
                } while (dgr == DialogResult.Retry);

                this.NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
            }
            else
            {
                //Set background color to yellow for any required controls that are empty.
                this.Controls.OfType<CustomTextBox>()
                    .Where(r => r.Required)
                    .Where(t => !t.isValid).ToList()
                    .ForEach(control => control.BackColor = Color.Yellow);
            }
        }

        private void printAuthorizationToReleaseFingerprints(ReleaseFingerprintsInfo releaseFingerprintsInfo, CustomerVO currentCustomer, int seizeNumber)
        {
            //Call print Police seize form if print is enabled
            if (SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled)
            {
                var address = currentCustomer.getHomeAddress();

                var releaseFingerprintsContext = new Reports.AuthorizationToReleaseFingerprints.
                    ReleaseFingerprintsContext()
                {
                    EmployeeNumber =
                        GlobalDataAccessor.Instance.DesktopSession.
                        UserName,
                    ShopName =
                        GlobalDataAccessor.Instance.CurrentSiteId.
                        StoreName,
                    ShopAddress =
                        GlobalDataAccessor.Instance.CurrentSiteId.
                            StoreAddress1 + " " +
                        GlobalDataAccessor.Instance.CurrentSiteId.
                            StoreAddress2,
                    ShopCity =
                        GlobalDataAccessor.Instance.CurrentSiteId.
                        StoreCityName,
                    ShopState =
                        GlobalDataAccessor.Instance.CurrentSiteId.State,
                    ShopZipCode =
                        GlobalDataAccessor.Instance.CurrentSiteId.
                        StoreZipCode,
                    CustomerName = currentCustomer.CustomerName,
                    CustomerAddress1 = address.Address1,
                    CustomerAddress2 = address.Address2,
                    CustomerCity = address.City,
                    CustomerState = address.State_Code,
                    CustomerAddressUnit = address.UnitNum,
                    CustomerZipCode = address.ZipCode,
                    TicketNumber = releaseFingerprintsInfo.RefNumber,
                    Agency = releaseFingerprintsInfo.Agency,
                    CaseNumber = releaseFingerprintsInfo.CaseNumber,
                    SubpoenaNumber =
                        releaseFingerprintsInfo.SubpoenaNumber,
                    OfficerName =
                        releaseFingerprintsInfo.OfficerFirstName + " " +
                        releaseFingerprintsInfo.OfficerLastName,
                    BadgeNumber = releaseFingerprintsInfo.BadgeNumber,
                    TransactionDate = releaseFingerprintsInfo.TransactionDate,
                    OutputPath =
                        SecurityAccessor.Instance.EncryptConfig.
                            ClientConfig.GlobalConfiguration.
                            BaseLogPath +
                        "\\AuthorizationToReleaseFingerprints_" +
                        DateTime.Now.ToString("MMddyyyyhhmmssFFFFFFF") +
                        ".pdf"
                };

                var authorizationToReleaseFingerprints = new AuthorizationToReleaseFingerprints(
                    seizeNumber, GlobalDataAccessor.Instance.DesktopSession.TicketTypeLookedUp,
                    releaseFingerprintsContext, null);

                authorizationToReleaseFingerprints.Print();

                string strReturnMessage =
                    PrintingUtilities.printDocument(
                        releaseFingerprintsContext.OutputPath,
                        GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.IPAddress,
                        GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.Port, 2);
                if (!strReturnMessage.Contains("SUCCESS"))
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this,
                                                   "Authorization to release fingerprints : " +
                                                   strReturnMessage);

                //---------------------------------------------------
                // Place document into couch
                var filePath = releaseFingerprintsContext.OutputPath;
                //authorizationToReleaseFingerprints.Save(policeCardFilePath);

                var pDoc = new CouchDbUtils.PawnDocInfo();
                var dA = GlobalDataAccessor.Instance.OracleDA;
                var cC = GlobalDataAccessor.Instance.CouchDBConnector;
                //Set document add calls
                pDoc.UseCurrentShopDateTime = true;
                pDoc.SetDocumentSearchType(CouchDbUtils.DocSearchType.POLICE_CARD);
                pDoc.StoreNumber = GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreNumber;
                pDoc.CustomerNumber = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.CustomerNumber;
                pDoc.TicketNumber = seizeNumber;
                pDoc.DocumentType = Common.Libraries.Objects.Doc.Document.DocTypeNames.PDF;
                pDoc.DocFileName = filePath;
                //Add this document to the pawn document registry and document storage
                string errText;
                if (!CouchDbUtils.AddPawnDocument(dA, cC, GlobalDataAccessor.Instance.DesktopSession.UserName, ref pDoc, out errText))
                {
                    if (FileLogger.Instance.IsLogError)
                    {
                        FileLogger.Instance.logMessage(LogLevel.ERROR, this,
                            "Could not store release authorization to release fingerprints document in document storage: {0} - FileName: {1}", errText, filePath);
                    }

                    BasicExceptionHandler.Instance.AddException(
                        "Could not store police card document in document storage",
                        new ApplicationException("Could not store authorization to release fingerprints document in document storage: " + errText));
                }

            }
        }

        private void validateForm()
        {
            //Get all invalid required fields
            var invalidTextBoxControlsCount = this.Controls.OfType<CustomTextBox>().Where(r => r.Required).Where(t => !t.isValid).Count();

            //If any required controls are not set to valid, then the form is not valid
            isFormValid = invalidTextBoxControlsCount <= 0;

            //If the form is valid, then enable the submit button.
            SubmitButton.Enabled = isFormValid;
        }

        private void OnFormPreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            validateForm();
        }
    }
}
