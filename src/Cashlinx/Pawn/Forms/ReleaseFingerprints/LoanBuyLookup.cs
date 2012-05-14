using System;
using System.Linq;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Objects.Pawn;
using Common.Libraries.Objects.Purchase;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Shared;
using Pawn.Logic.DesktopProcedures;

namespace Pawn.Forms.ReleaseFingerprints
{
    public partial class LoanBuyLookup : Form
    {
        public NavBox NavControlBox;// { get; set; }

        public LoanBuyLookup()
        {
            InitializeComponent();
            this.NavControlBox = new NavBox();
        }

        private void LoanBuyLookup_Load(object sender, EventArgs e)
        {
            this.NavControlBox.Owner = this;
            this.LoanStoreText.Text = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
            this.BuyStoreText.Text = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;

            ActiveControl = LoanNumberText;
            LoanNumberText.Enabled = true;
            BuyNumberText.Enabled = false;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            GlobalDataAccessor.Instance.DesktopSession.ClearPawnLoan();
            this.NavControlBox.Action = NavBox.NavAction.CANCEL;
        }

        private void buttonContinue_Click(object sender, EventArgs e)
        {
            labelErrorMessage.Visible = false;

            //Get the radio button that is selected
            var checkedButton = this.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);

            if (checkedButton == this.BuyNumberRadioButton)
            {
                if (!getBuyData())
                {
                    showErrorMessage("The buy number was not found.");
                }
            }
            else
            {
                if (!getLoanData())
                {
                    showErrorMessage("The loan number was not found.");
                }
            }

        }

        private void showErrorMessage(string message)
        {
            labelErrorMessage.Text = message;
            labelErrorMessage.Visible = true;
        }

        private bool getLoanData()
        {
            bool retValue = false;
            string errorCode;
            string errorText;
            PawnLoan pawnLoan;
            PawnAppVO pawnApplication;
            CustomerVO customerObj;

            retValue = CustomerLoans.GetPawnLoan(GlobalDataAccessor.Instance.DesktopSession,
                                                Utilities.GetIntegerValue(BuyStoreText.Text, 0), Utilities.GetIntegerValue(LoanNumberText.Text, 0), 
                                                "0", StateStatus.BLNK, true, out pawnLoan,
                                                  out pawnApplication, out customerObj, 
                                                  out errorCode, out errorText);

            if(retValue && pawnLoan != null)
            {
                GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer = customerObj;

                GlobalDataAccessor.Instance.DesktopSession.CurrentPawnLoan = pawnLoan;

                GlobalDataAccessor.Instance.DesktopSession.TicketTypeLookedUp = ProductType.PAWN;
                GlobalDataAccessor.Instance.DesktopSession.TicketLookedUp = Utilities.GetIntegerValue(LoanNumberText.Text, 0);
                this.NavControlBox.Action = NavBox.NavAction.SUBMIT;
            }

            return retValue;
        }

        private bool getBuyData()
        {
            bool retValue = false;

            PurchaseVO purchaseObj = null;
            CustomerVO customerObj = null;
            string errorCode;
            string errorText;
            string tenderType;

            retValue = PurchaseProcedures.GetPurchaseData(Utilities.GetIntegerValue(BuyStoreText.Text, 0),
                                                          Utilities.GetIntegerValue(BuyNumberText.Text, 0),
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
                GlobalDataAccessor.Instance.DesktopSession.TicketLookedUp = Utilities.GetIntegerValue(BuyNumberText.Text, 0);
                GlobalDataAccessor.Instance.DesktopSession.TicketTypeLookedUp = ProductType.BUY;
                this.NavControlBox.Action = NavBox.NavAction.SUBMIT;
            }

            return retValue;
        }

        private void LoanNumberRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            //Get the radio button that is selected
            var checkedButton = this.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);

            if (checkedButton == this.BuyNumberRadioButton)
            {
                BuyNumberText.Enabled = true;
                LoanNumberText.Enabled = false;
                ActiveControl = BuyNumberText;
            }
            else
            {
                LoanNumberText.Enabled = true;
                BuyNumberText.Enabled = false;
                ActiveControl = LoanNumberText;
            }
        }
    }
}
