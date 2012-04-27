using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Business;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using Pawn.Forms.UserControls;
using Pawn.Logic.PrintQueue;

namespace Pawn.Forms.Pawn.ShopAdministration.ShopCash
{
    public partial class BankTransferFrom : Form
    {
        private string errorCode;
        private string errorText;
        private const string DENOMINATIONCURRENCY = "USD";
        private const string BNL = "BNL";
        private int transferNumber;
        public BankTransferFrom()
        {
            InitializeComponent();
        }

        private void BankTransferFrom_Load(object sender, EventArgs e)
        {
            labelTransferDate.Text = ShopDateTime.Instance.ShopDate.ToShortDateString();
            labelCashDrawerName.Text = GlobalDataAccessor.Instance.DesktopSession.StoreSafeName;

            DataTable bankInfo;
            bool retVal = ShopCashProcedures.GetStoreBankInfo(GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,GlobalDataAccessor.Instance.DesktopSession,
                out bankInfo, out errorCode, out errorText);
            ArrayList bankData = new ArrayList();
            if (retVal && bankInfo != null && bankInfo.Rows.Count > 0)
            {
                foreach (DataRow dr in bankInfo.Rows)
                    {
                        bankData.Add(new ComboBoxData(dr["storebankid"].ToString(), dr["bankname"] + "-" + dr["accountnumber"]));
                    }
            }
            bankData.Add(new ComboBoxData(BNL, "Bank Not Listed"));
            comboBoxBankData.DataSource = bankData;
            comboBoxBankData.DisplayMember = "Description";
            comboBoxBankData.ValueMember = "Code";
            currencyEntry2.Calculate += new CurrencyEntry.AddHandler(currencyEntry2_Calculate);
            currencyEntry2.OtherTenderClick += new CurrencyEntry.AddOTClick(currencyEntry2_OtherTenderClick);
            //Do not show the currency panel when it first loads
            panelCurrency.Visible = false;
            pictureBox1.Image = global::Common.Properties.Resources.plus_icon_small;
            panel1.Location = new Point(10, 100);
            panelChkdata.Location = new Point(76, 42);
            this.Size = new Size(784, 400);
            
        }

        void currencyEntry2_OtherTenderClick()
        {
            panel1.Visible = !panel1.Visible;
        }

        void currencyEntry2_Calculate(decimal currencyTotal)
        {
            customTextBoxTrAmount.Text = string.Format("{0:C}", currencyTotal);
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (panelCurrency.Visible)
            {
                panelCurrency.Visible = false;
                pictureBox1.Image = global::Common.Properties.Resources.plus_icon_small;
                panel1.Location = new Point(10, 100);
                this.Size = new Size(784, 400);

            }
            else
            {
                panelCurrency.Visible = true;
                pictureBox1.Image = global::Common.Properties.Resources.minus_icon_small;
                panel1.Location = new Point(10, 436);
                this.Size = new Size(784, 697);
            }
        }

        private void customButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void customButtonSubmit_Click(object sender, EventArgs e)
        {

            string selectedBankId=comboBoxBankData.SelectedValue.ToString();

            if (!string.IsNullOrEmpty(selectedBankId) && !string.IsNullOrEmpty(customTextBoxTrAmount.Text) &&
                customTextBoxCheckNo.isValid)
            {
                string bankName = string.Empty;
                string bankRoutingNumber = string.Empty;
                string bankAccountNumber = string.Empty;
                if (selectedBankId.Equals(BNL))
                {
                    selectedBankId = "";
                    bankName = customTextBoxBankName.Text;
                    bankRoutingNumber = customTextBoxRoutingNumber.Text;
                    bankAccountNumber = customTextBoxAcctNumber.Text;
                    if (string.IsNullOrEmpty(bankName) || string.IsNullOrEmpty(bankRoutingNumber) ||
                        string.IsNullOrEmpty(bankAccountNumber))
                    {
                        MessageBox.Show("Enter Bank details and submit");
                        return;
                    }
                }                
                string transferAmount = customTextBoxTrAmount.Text;
                if (transferAmount.StartsWith("$"))
                    transferAmount = transferAmount.Substring(1);
                if (Utilities.GetDecimalValue(transferAmount,0) > 0)
                {
                    GlobalDataAccessor.Instance.beginTransactionBlock();
                bool retVal = ShopCashProcedures.InsertBankTransfer(selectedBankId, DENOMINATIONCURRENCY,
                    GlobalDataAccessor.Instance.DesktopSession.StoreSafeID, 
                    ShopDateTime.Instance.ShopDate.FormatDate().ToString() + " " + ShopDateTime.Instance.ShopTime.ToString(),
                    "", GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile.UserID,
                    Utilities.GetDecimalValue(transferAmount, 0).ToString(), CashTransferTypes.BANKTOSHOP.ToString(),
                    GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                    customTextBoxCheckNo.Text,"",bankName,bankAccountNumber,bankRoutingNumber,GlobalDataAccessor.Instance.DesktopSession,
                    out transferNumber, out errorCode, out errorText);
                if (retVal)
                {
                    GlobalDataAccessor.Instance.endTransactionBlock(EndTransactionType.COMMIT);
                    MessageBox.Show(@"Bank Transfer data successfully inserted");
                    var bankTransferdata = new CashTransferVO();
                    bankTransferdata.TransferNumber = transferNumber;
                    bankTransferdata.TransferStatus = CashTransferStatusCodes.ACCEPTED.ToString();
                    bankTransferdata.TransferAmount = Utilities.GetDecimalValue(transferAmount,0);
                    bankTransferdata.BankName = comboBoxBankData.Text.ToString();
                    bankTransferdata.CheckNumber = customTextBoxBankName.Text;
                    bankTransferdata.DestinationEmployeeName = GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile.UserName;
                    bankTransferdata.DestinationEmployeeNumber = GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile.EmployeeNumber;
                    var sourceSite = new SiteId();
                    sourceSite = GlobalDataAccessor.Instance.CurrentSiteId;
                    bankTransferdata.SourceShopInfo = sourceSite;
                    var bankTransferFrm = new BankAndInternalCashTransfer();
                    bankTransferFrm.CashTransferdata = bankTransferdata;
                    bankTransferFrm.ShowDialog();

                }
                else
                {
                    GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Bank Transfer into store failed " + errorText);
                }
                }
                else
                {
                    MessageBox.Show("Transfer amount cannot be 0");
                    return;
                }
                this.Close();
            }
            else
            {
                MessageBox.Show(@"Fill in all the required information and submit");
                return;
            }
 
        }

        private void customTextBoxTrAmount_Leave(object sender, EventArgs e)
        {
            if (customTextBoxTrAmount.isValid)
            {
                if (customTextBoxTrAmount.Text.StartsWith("$"))
                    customTextBoxTrAmount.Text = customTextBoxTrAmount.Text.Substring(1);

                customTextBoxTrAmount.Text = string.Format("{0:C}", Utilities.GetDecimalValue(customTextBoxTrAmount.Text, 0));
            }
        }

        private void comboBoxBankData_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelBankData.Visible = comboBoxBankData.SelectedValue.ToString() == BNL;
            panelChkdata.Location = comboBoxBankData.SelectedValue.ToString() == BNL? new Point(94, 98):panelChkdata.Location = new Point(76, 42);;
        }

  
    }
}
