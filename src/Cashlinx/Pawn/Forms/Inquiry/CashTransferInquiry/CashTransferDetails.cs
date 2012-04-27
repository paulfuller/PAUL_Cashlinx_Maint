using Common.Controllers.Security;
using Common.Libraries.Forms.Components;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Application;
using System;
using System.Data;
using System.Windows.Forms;
using Pawn.Logic;
using Reports.Inquiry;

namespace Pawn.Forms.Inquiry.CashTransferInquiry
{
    public partial class CashTransferDetails : Form
    {
        public enum TransferTypeEnum
        {
            BANK,
            INTERNAL, // SAFE is a special case of a internal
            SHOP
            //SAFE
        }

        private DataSet _theData;
        private DataView _selectedData;
        private TransferTypeEnum _transferTypeEnum;

        public CashTransferDetails()
        {
            InitializeComponent();
        }

        public CashTransferDetails(String key, DataSet ds, int rowIdx, TransferTypeEnum transferTypeEnum)
        {
            InitializeComponent();
            _transferTypeEnum = transferTypeEnum;

            #region DATABINDING
            _theData = ds;

            try
            {
                String tableName = "";
                switch (_transferTypeEnum)
                {
                    case TransferTypeEnum.SHOP:
                        tableName = "CASH_XFER_DETAILS";
                        lblTransferType.Text = "Shop to Shop";
                        panelShopToShop.Visible = true;
                        panelInternal.Visible = false;
                        panelTransferToBank.Visible = false;
                        panelTransferFromBank.Visible = false;
                        panelShopToShop.BringToFront();
                        break;
                    case TransferTypeEnum.INTERNAL:
                        // SAFE is a special case of INTERNAL
                        tableName = "CASH_XFER_DETAILS";
                        lblTransferType.Text = "Internal";
                        panelShopToShop.Visible = false;
                        panelInternal.Visible = true;
                        panelTransferToBank.Visible = false;
                        panelTransferFromBank.Visible = false;
                        this.panelInternal.BringToFront();
                        break;
                    case TransferTypeEnum.BANK:
                        tableName = "CASH_XFER_DETAILS";
                        panelShopToShop.Visible = false;
                        panelInternal.Visible = false;
                        panelTransferToBank.Visible = false;
                        panelTransferFromBank.Visible = false;
                        this.panelTransferToBank.BringToFront();
                        break;
                    default:
                        System.Diagnostics.Debug.Assert(false, "Unhandled case. Forget to add handler?");
                        break;
                }

                if (_theData.DefaultViewManager.DataViewSettings[tableName] != null)
                {
                    //_theData.DefaultViewManager.DataViewSettings[tableName].RowFilter = "TRANSFERNUMBER='" + key + "'";
                    _selectedData = _theData.DefaultViewManager.CreateDataView(_theData.Tables[tableName]);

                    lblTransferStatus.Text = _selectedData[0]["TRANSFERSTATUS"].ToString();
                    lblTransferNumber.Text = _selectedData[0]["TRANSFERNUMBER"].ToString();

                    switch (transferTypeEnum)
                    {
                        case TransferTypeEnum.SHOP:
                            {
                                lblSourceShopName.Text = _selectedData[0]["SOURCE_NAME"].ToString();
                                lblDestShopName.Text = _selectedData[0]["DEST_NAME"].ToString();

                                lblTransferDate.Text = string.Format("{0:d}", _selectedData[0]["TRANSERDATE"].ToString());
                                lblStatusDate.Text = string.Format("{0:d}", _selectedData[0]["STATUSDATE"].ToString());

                                if (_selectedData[0]["SOURCE_ADDR1"].ToString().Length != 0 &&
                                    _selectedData[0]["SOURCE_ADDR2"].ToString().Length != 0)
                                {
                                    lblSourceAddr1.Text = _selectedData[0]["SOURCE_ADDR1"].ToString();
                                    lblSourceAddr2.Text = _selectedData[0]["SOURCE_ADDR2"].ToString();
                                }
                                else
                                {
                                    lblSourceAddr1.Text = "";
                                    lblSourceAddr2.Text = _selectedData[0]["SOURCE_ADDR1"].ToString();
                                }

                                lblSourceCSZ.Text = _selectedData[0]["SOURCE_CSZ"].ToString();
                                lblSourcePhone.Text = _selectedData[0]["SOURCE_PHONE"].ToString();

                                // in certain scenarios, we can have multiple records, use the first non-blank value
                                String sourceMgr = _selectedData[0]["SOURCE_MGR"].ToString();
                                for (int rowIndex = 0; rowIndex < _selectedData.Table.Rows.Count && sourceMgr.Length == 0; rowIndex++)
                                {
                                    sourceMgr = _selectedData[rowIndex]["SOURCE_MGR"].ToString();
                                }
                                lblSourceMgr.Text = sourceMgr;

                                if (_selectedData[0]["DEST_ADDR1"].ToString().Length != 0 &&
                                    _selectedData[0]["DEST_ADDR2"].ToString().Length != 0)
                                {
                                    lblDestAddr1.Text = _selectedData[0]["DEST_ADDR1"].ToString();
                                    lblDestAddr2.Text = _selectedData[0]["DEST_ADDR2"].ToString();
                                }
                                else
                                {
                                    lblDestAddr1.Text = "";
                                    lblDestAddr2.Text = _selectedData[0]["DEST_ADDR1"].ToString();
                                }

                                lblDestCSZ.Text = _selectedData[0]["DEST_CSZ"].ToString();
                                lblDestPhone.Text = _selectedData[0]["DEST_PHONE"].ToString();

                                // in certain scenarios, we can have multiple records, use the first non-blank value
                                String destMgr = _selectedData[0]["DEST_MGR"].ToString();
                                for (int rowIndex = 0; rowIndex < _selectedData.Table.Rows.Count && destMgr.Length == 0; rowIndex++)
                                {
                                    destMgr = _selectedData[rowIndex]["DEST_MGR"].ToString();
                                }
                                lblDestMgr.Text = destMgr;

                                double amount = 0.0;
                                double.TryParse(_selectedData[0]["transferamount"].ToString(), out amount);

                                lblSourceTransferAmount.Text = string.Format("{0:C}", amount);

                                lblTransportedBy.Text = _selectedData[0]["TRANSPORTEDBY"].ToString();
                                lblBagNumber.Text = _selectedData[0]["DEPOSITBAGNUMBER"].ToString();

                                lblSourceComments.Text = _selectedData[0]["SOURCECOMMENT"].ToString();
                                lblDestComments.Text = _selectedData[0]["DESTINATIONCOMMENT"].ToString();
                            }
                            break;
                        case TransferTypeEnum.INTERNAL:
                            {
                                // SAFE is a special case of INTERNAL
                                lblSourceTransferFrom.Text = _selectedData[0]["source"].ToString();

                                double amount = 0.0;
                                double.TryParse(_selectedData[0]["transferamount"].ToString(), out amount);
                                lbInternallSourceTransferAmount.Text = string.Format("{0:C}", amount);

                                lblSourceUserId.Text = _selectedData[0]["userid"].ToString();
                                lblDestinationTransferTo.Text = _selectedData[0]["destination"].ToString();
                                lblDestinationAcceptedBy.Text = _selectedData[0]["acceptedby"].ToString();

                                DateTime dt = DateTime.MinValue;
                                DateTime.TryParse(_selectedData[0]["transferdate"].ToString(), out dt);
                                lblInternalTransferDateTime.Text = dt.ToShortDateString() + " " + dt.ToShortTimeString();
                            }
                            break;
                        case TransferTypeEnum.BANK:
                            {
                                if ("SHOPTOBANK" == _selectedData[0]["transfertype"].ToString())
                                {
                                    lblTransferType.Text = "Transfer To Bank";
                                    panelTransferToBank.Visible = true;
                                    panelTransferToBank.BringToFront();

                                    lblToBankSourceTransferFrom.Text = _selectedData[0]["source"].ToString();

                                    double amount = 0.0;
                                    double.TryParse(_selectedData[0]["transferamount"].ToString(), out amount);
                                    lblToBankSourceTransferAmount.Text = string.Format("{0:C}", amount);

                                    lblToBankSourceUserId.Text = _selectedData[0]["userid"].ToString();
                                    lblToBankDestBankName.Text = _selectedData[0]["destination"].ToString();
                                    lblToBankDestAccountNumber.Text = _selectedData[0]["routingnumber"].ToString();
                                    lblToBankDestBagNumber.Text = _selectedData[0]["depositbagnumber"].ToString();

                                    DateTime dt = DateTime.MinValue;
                                    DateTime.TryParse(_selectedData[0]["transferdate"].ToString(), out dt);
                                    lblToBankTransferDateTime.Text = dt.ToShortDateString() + " " + dt.ToShortTimeString();
                                }
                                else
                                {
                                    lblTransferType.Text = "Transfer From Bank";
                                    panelTransferFromBank.Visible = true;
                                    panelTransferFromBank.BringToFront();

                                    lblFromBankTransferFrom.Text = _selectedData[0]["source"].ToString();

                                    double amount = 0.0;
                                    double.TryParse(_selectedData[0]["transferamount"].ToString(), out amount);
                                    lblFromBankTransferAmount.Text = string.Format("{0:C}", amount);

                                    lblFromBankUserId.Text = _selectedData[0]["userid"].ToString();
                                    lblFromBankTransferTo.Text = _selectedData[0]["destination"].ToString();
                                    lblFromBankAccountNumber.Text = _selectedData[0]["routingnumber"].ToString();
                                    lblFromBankCheckNumber.Text = _selectedData[0]["checknumber"].ToString();

                                    DateTime dt = DateTime.MinValue;
                                    DateTime.TryParse(_selectedData[0]["transferdate"].ToString(), out dt);
                                    lblFromBankTransferDateTime.Text = dt.ToShortDateString() + " " + dt.ToShortTimeString();
                                }
                            }
                            break;
                        default:
                            System.Diagnostics.Debug.Assert(false, "Unhandled case. Forget to add handler?");
                            break;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("An Error Occurred on the Pawn Loan Extensions Detail Screen");
            }


            #endregion
        }

        private void Back_btn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Refine_btn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Retry;
            this.Close();
        }

        private void Print_btn_Click(object sender, EventArgs e)
        {
            string rptDir =
                SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseLogPath;

            var ro = new ReportObject();
            ro.CreateTemporaryFullName();
            ro.ReportTempFileFullName = rptDir + ro.ReportTempFileFullName;
            ro.ReportStore = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
            ro.ReportStoreDesc = GlobalDataAccessor.Instance.CurrentSiteId.StoreName;
            ro.ReportSort = "USERID";
            ro.RunDate = DateTime.Now.ToShortDateString();

            switch (_transferTypeEnum)
            {
                case (TransferTypeEnum.BANK):
                    {
                        var report = new CashTransferBankTransferReport();
                        report.reportObject = ro;

                        var isSuccessful = report.CreateReport(_theData.Tables[0]);

                        if (isSuccessful)
                        {
                            DesktopSession.ShowPDFFile(report.reportObject.ReportTempFileFullName, false);
                            this.TopMost = false;
                        }
                    }
                    break;
                case (TransferTypeEnum.INTERNAL):
                    {
                        var report = new CashTransferInternalTransferReport();
                        report.reportObject = ro;

                        var isSuccessful = report.CreateReport(_theData.Tables[0]);

                        if (isSuccessful)
                        {
                            DesktopSession.ShowPDFFile(report.reportObject.ReportTempFileFullName, false);
                            this.TopMost = false;
                        }
                    }
                    break;
                case (TransferTypeEnum.SHOP):
                    {
                        var report = new CashTransferShopToShopTransferReport();
                        report.reportObject = ro;

                        var isSuccessful = report.CreateReport(_theData.Tables[0]);

                        if (isSuccessful)
                        {
                            DesktopSession.ShowPDFFile(report.reportObject.ReportTempFileFullName, false);
                            this.TopMost = false;
                        }
                    }
                    break;
                default:
                    System.Diagnostics.Debug.Assert(false, "Unhandled Condition. Forget to add handler?");
                    break;
            }
        }

        private void Cancel_btn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
