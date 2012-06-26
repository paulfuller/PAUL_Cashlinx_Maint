using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Forms.Components;
using Common.Libraries.Objects.Business;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using Pawn.Forms.Report;

namespace Pawn.Forms.Pawn.Services.ChargeOff
{
    public partial class ChargeOffDetails : CustomBaseForm
    {
        public Item ChargeOffItem;
        private string chargeoffReason;
        private string atfNumber;
        private string caseNumber;
        private string charityOrg;
        private string charityAddr;
        private string charityCity;
        private string charityState;
        private string charityZip;
        private string customerNumber;
        private string replacedICN;

        public ChargeOffDetails()
        {
            InitializeComponent();
        }

        private void ChargeOffDetails_Load(object sender, EventArgs e)
        {
            labelICN.Text = ChargeOffItem.Icn;
            labelChargeOffAmt.Text = ChargeOffItem.ItemAmount.ToString("c");
            richTextBoxMdDesc.Text = ChargeOffItem.TicketDescription;

            comboBoxReason.Items.Clear();
            chargeoffReason = "COFFBRKN";
            if (ChargeOffItem.IsGun)
            {
                comboBoxReason.Items.Add("Destroyed");
                comboBoxReason.Items.Add("Donation");
                //comboBoxReason.Items.Add("Replacement of Property");
                customTextBoxAuthBy.Required = true;
                customLabelAuthBy.Required = true;
            }
            else
            {
                comboBoxReason.Items.Add("Broken");
                comboBoxReason.Items.Add("Destroyed");
                comboBoxReason.Items.Add("Donation");
                comboBoxReason.Items.Add("NXT");
                //comboBoxReason.Items.Add("Replacement of Property");
                comboBoxReason.Items.Add("Store Use");
                customTextBoxAuthBy.Required = false;
                customLabelAuthBy.Required = false;
            }

            comboBoxReason.SelectedIndex = 0;
        }

        private void customButtonBack_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.None;
            this.Close();
        }

        private void customButtonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Abort;
            this.Close();
        }

        private void comboBoxReason_SelectedIndexChanged(object sender, EventArgs e)
        {
            charityOrg = string.Empty;
            charityAddr = string.Empty;
            charityCity = string.Empty;
            charityState = string.Empty;
            charityZip = string.Empty;
            atfNumber = string.Empty;
            caseNumber = string.Empty;

            switch (comboBoxReason.Items[comboBoxReason.SelectedIndex].ToString())
            {
                case "Broken":
                    chargeoffReason = "COFFBRKN";
                    break;
                case "Store Use":
                    chargeoffReason = "COFFSTRU";
                    break;
                case "Donation":
                    chargeoffReason = "COFFDONATION";
                    break;
                case "NXT":
                    chargeoffReason = "COFFNXT";
                    break;
                case "Destroyed":
                    chargeoffReason = "COFFDESTROY";
                    break;
                case "Replacement of Property":
                    chargeoffReason = "COFFREPLPROP";
                    break;
                default:
                    chargeoffReason = "COFFBRKN";
                    break;
            }

            //if (chargeoffReason.Equals("COFFDESTROY"))
            //{
            //    ChargeOffPoliceInfo policeinfoFrm = new ChargeOffPoliceInfo();
            //    policeinfoFrm.ShowDialog();
            //    atfNumber = policeinfoFrm.ATFNumber;
            //    caseNumber = policeinfoFrm.CaseNumber;
            //}
            if (chargeoffReason.Equals("COFFDONATION"))
            {
                var charityFrm = new ChargeOffCharityData();
                charityFrm.ShowDialog();
                if (charityFrm.DialogResult == DialogResult.OK)
                {
                    charityOrg = charityFrm.CharityOrg;
                    charityAddr = charityFrm.CharityAddr;
                    charityCity = charityFrm.CharityCity;
                    charityState = charityFrm.CharityState;
                    charityZip = charityFrm.CharityZip;
                }

            }
            else if (chargeoffReason.Equals("COFFREPLPROP"))
            {
                var replacementFrm = new ChargeOffReplacementData();
                replacementFrm.ShowDialog();
                customerNumber = replacementFrm.CustNumber;
                replacedICN = replacementFrm.ReplacedICN;
            }
        }

        private void customButtonSubmit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.None;

            if (richTextBoxComment.Text.Trim().Count() == 0)
            {
                MessageBox.Show("Comment should be entered");
                return;
            }

            if (comboBoxReason.SelectedItem.ToString() == "Donation" && charityOrg == null)
            {
                MessageBox.Show("Charge-off reason requires additional information that was not provided, please reselect reason and enter required information.");
                return;
            }

            if (customTextBoxAuthBy.isValid || !customTextBoxAuthBy.Required)
            {
                List<string> icn = new List<string>();
                icn.Add(ChargeOffItem.Icn);
                List<string> retailPrice = new List<string>();
                retailPrice.Add("0");
                List<string> jCase = new List<string>();
                jCase.Add("");
                List<string> statusReason = new List<string>();
                statusReason.Add(chargeoffReason);
                List<int> qty = new List<int>();
                qty.Add(ChargeOffItem.Quantity);
                string errorCode;
                string errorText;
                int saleTicketNumber;

                bool retValue = MerchandiseProcedures.InsertInventoryChargeOff(GlobalDataAccessor.Instance.OracleDA,
                    GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                    ShopDateTime.Instance.ShopDate.ToShortDateString(),
                    ShopDateTime.Instance.ShopDate.ToShortDateString() + " " + ShopDateTime.Instance.ShopTime.ToString(),
                    "",
                    GlobalDataAccessor.Instance.DesktopSession.FullUserName,
                    icn,
                    qty,
                    statusReason,
                    retailPrice,
                    "",
                    "",
                    "",
                    "0",
                    GlobalDataAccessor.Instance.DesktopSession.CashDrawerName,
                    "SALE",
                    "0",
                    "0",
                    jCase,
                    customTextBoxAuthBy.Text,
                    atfNumber,
                    caseNumber,
                    charityOrg,
                    charityAddr,
                    charityCity,
                    charityState,
                    charityZip,
                    replacedICN,
                    richTextBoxComment.Text.ToString(),
                    out saleTicketNumber,
                    out errorCode,
                    out errorText);
                if (!retValue)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Error trying to complete chargeoff " + errorText);
                    MessageBox.Show("Error completing charge off ");
                    return;
                }
                MessageBox.Show("Charge off completed successfully");

                this.DialogResult = DialogResult.OK;

                //set fields
                InventoryChargeOffFields invFields = new InventoryChargeOffFields();
                invFields.ATFIncidentNumber = atfNumber;
                invFields.AuthorizedBy = customTextBoxAuthBy.Text;
                invFields.ChargeOffAmount = ChargeOffItem.ItemAmount.ToString("C");
                invFields.ChargeOffNumber = saleTicketNumber.ToString();
                invFields.CharitableOrganization = charityOrg;
                invFields.Comment = richTextBoxComment.Text.ToString();

                if (!string.IsNullOrEmpty(GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.LastName) && !string.IsNullOrEmpty(GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.FirstName))
                {
                    invFields.CustomerName = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.LastName + ", " +
                                             GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.FirstName;
                }
                else
                {
                    invFields.CustomerName = string.Empty;
                }

                invFields.EmployeeNumber = GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile.EmployeeNumber;
                invFields.ICN = ChargeOffItem.Icn;
                invFields.MerchandiseDescription = ChargeOffItem.TicketDescription;
                invFields.PoliceCaseNumber = caseNumber;
                invFields.ReasonForChargeOff = chargeoffReason;
                invFields.ReplacementLoanNumber = string.Empty;
                invFields.GunNumber = ChargeOffItem.GunNumber;
                invFields.IsGun = ChargeOffItem.IsGun;
                CreateReportObject cro = new CreateReportObject();
                cro.GetInventoryChargeOffReport(invFields);
                //document generation here
                //chargeoffReason, charityOrg, caseNumber, atfNumber, customTextBoxAuthBy.Text,saleTicketNumber,  ChargeOffItem.TicketDescription
                // richTextBoxComment.Text.ToString()
                this.Close();
            }
            else
            {
                MessageBox.Show("Authorized by should be entered");
                return;
            }
        }
    }
}
