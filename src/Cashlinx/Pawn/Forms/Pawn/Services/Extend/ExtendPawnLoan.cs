/**************************************************************************************************************
* CashlinxDesktop
* ExtendPawnLoan
* This form is used to enter details for extending a loan
* Sreelatha Rengarajan 10/01/2009 Initial version
 * SR 6/24/2010 Changed the calculation of last day to pickup on form load to 
 * be the same as the leave event of number of days to extend field
**************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Pawn;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Shared;

namespace Pawn.Forms.Pawn.Services.Extend
{
    public partial class ExtendPawnLoan : Form
    {
        private const int DAYS_IN_MONTH_FOR_INTEREST_CALCULATIONS = 30;

        private List<PawnLoan> selectedLoans;
        private int numberOfLoansToExtend;
        private int currIndex = 0;
        private bool CloseForm = false;
        private bool CanSubmitForm = false;
        private SiteId siteID;
        private int daysToExtend = 0;
        private decimal AmtToExtend = 0.0M;
        private string newPickupDate = string.Empty;
        private bool _loansExtended;
        private DateTime ShopCurrentDate;
        private decimal dailyAmount;
        private ExtensionTerms _extensionType = ExtensionTerms.DAILY;
        private List<int> skippedTicketNumbers = new List<int>();
        const int MAXLENGTHFORAMTTOEXTEND = 7;
        const int MAXLENGTHFORDAYSTOEXTEND = 4;
        private DateTime currentPFIDate;
        private bool suppressDaysChangedEvent = false;
        DateTime LastCycleEnd = DateTime.MaxValue;
        int daysToPay = 0;
        int monthsToPay = 0;
        decimal interestAmount = 0;
        decimal serviceAmount = 0;
        bool partialPaymentAllowed;

        private DesktopSession DS
        {
            get { return GlobalDataAccessor.Instance.DesktopSession; }
        }

        public bool LoansExtended
        {
            get { return _loansExtended; }

        }

        public ExtensionTerms ExtensionType
        {
            set
            {
                _extensionType = value;
            }

        }

        public ExtendPawnLoan()
        {
            InitializeComponent();
        }

        private void ExtendPawnLoan_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                dateCalendarLastPickupDate.SelectDate += checkLastPickupDate;
                ShopCurrentDate = ShopDateTime.Instance.ShopDate;
                selectedLoans = GlobalDataAccessor.Instance.DesktopSession.ExtensionLoans;
                //CashlinxDesktopSession.Instance.PrintSingleMemoOfExtension = false;
                numberOfLoansToExtend = selectedLoans.Count;
                this.customTextBoxAmtToExtend.ErrorMessage = Commons.GetMessageString("InvalidAmountToExtend");

                if (_extensionType == ExtensionTerms.MONTHLY)
                {
                    this.customTextBoxNumDaystoExtend.ErrorMessage = Commons.GetMessageString("InvalidMonthsToExtend");
                    this.labelNumDaysToExtendHeading.Text = "Number of Months to Extend";
                    this.labelDailyAmtHeading.Text = "Monthly Amount";
                    this.customTextBoxAmtToExtend.MaxLength = MAXLENGTHFORAMTTOEXTEND;
                    this.customTextBoxNumDaystoExtend.MaxLength = MAXLENGTHFORDAYSTOEXTEND;
                    dateCalendarLastPickupDate.AllowMonthlySelection = true;
                }
                else
                    this.customTextBoxNumDaystoExtend.ErrorMessage = Commons.GetMessageString("InvalidDaysToExtend");

                showPawnLoanData();
            }

            customButtonCalculate.Location = customButtonContinue.Location;
            CanSubmitForm = true;
            UpdateSubmitState();
        }

        private void showPawnLoanData()
        {
            if (currIndex >= 0)
            {
                var pl = selectedLoans[currIndex];

                siteID = new SiteId
                         {
                             Alias = GlobalDataAccessor.Instance.CurrentSiteId.Alias,
                             Company = GlobalDataAccessor.Instance.CurrentSiteId.Company,
                             Date = ShopDateTime.Instance.ShopDate,
                             LoanAmount = pl.Amount,
                             State = pl.OrgShopState,
                             StoreNumber = pl.OrgShopNumber,
                             TerminalId = GlobalDataAccessor.Instance.CurrentSiteId.TerminalId
                         };

                var businessRulesProcedures = new BusinessRulesProcedures(DS);
                partialPaymentAllowed = businessRulesProcedures.IsPartialPaymentAllowed(siteID);
                var partialPaymentInLastMonth = pl.PartialPayments.Any(pp => pp.Date_Made > pl.PfiNote && pp.Date_Made < pl.PfiEligible);
                var allowFutureInterestPayments = businessRulesProcedures.AllowFutureInterestPayments(siteID);
                var storageFee = businessRulesProcedures.GetStorageFee(siteID);

                labelLoanNumber.Text = pl.TicketNumber.ToString();
                labelCurrDueDate.Text = pl.DueDate.FormatDate();

                DateTime PartialPmtDate = DateTime.MaxValue;


                if (pl.PartialPaymentPaid)
                {
                    Common.Libraries.Objects.Pawn.PartialPayment pmt = pl.PartialPayments.OrderByDescending(pp => pp.Time_Made).FirstOrDefault();
                    PartialPmtDate = pmt.Date_Made;
                    interestAmount = pmt.CUR_FIN_CHG;
                    serviceAmount = pmt.Cur_Srv_Chg;
                }
                else
                {
                    interestAmount = pl.InterestAmount;
                    serviceAmount = pl.ServiceCharge;
                }
                if (partialPaymentAllowed && _extensionType != ExtensionTerms.DAILY)
                {
                    ExtensionProcedures.GetExtensionPeriod(PartialPmtDate,
                        pl.DateMade,
                        ShopDateTime.Instance.ShopDate,
                        pl.DueDate,
                        pl.PfiNote,
                        pl.PfiEligible,
                        pl.ExtensionType,
                        out daysToPay,
                        out monthsToPay,
                        out LastCycleEnd);
                }
                else
                {
                    pl.ExtensionType = _extensionType;
                    //daysToPay = 30;
                }

                int daysToAdd;
                decimal amountToExtend;

                dailyAmount = ServiceLoanProcedures.GetDailyAmount(pl.ExtensionType, interestAmount, serviceAmount);
                if (pl.ExtensionType == ExtensionTerms.MONTHLY)
                {
                    customTextBoxNumDaystoExtend.Visible = false;
                    ddlNumDaystoExtend.Visible = true;
                    suppressDaysChangedEvent = true;
                    for (int i = 1; i <= monthsToPay; i++)
                        ddlNumDaystoExtend.Items.Add(i.ToString());
                    if (ddlNumDaystoExtend.Items.Count > 0)
                        ddlNumDaystoExtend.SelectedIndex = 0;
                    suppressDaysChangedEvent = false;

                    customTextBoxAmtToExtend.Visible = false;
                    lblAmtToExtend.Visible = true;
                    int monthsToExtendBy = 1;
                    if (partialPaymentAllowed && pl.PartialPaymentPaid)
                    {
                        monthsToExtendBy = GetDaysToExtendFromUI() - 1;

                        amountToExtend = (monthsToExtendBy * dailyAmount) + (daysToPay * interestAmount / 30) + (daysToPay * serviceAmount / 30);
                    }
                    else
                    {
                        monthsToExtendBy = GetDaysToExtendFromUI();
                        amountToExtend = monthsToExtendBy * dailyAmount;
                    }
                    lblAmtToExtend.Text = amountToExtend.ToString("f2");
                    this.ActiveControl = this.ddlNumDaystoExtend;

                    var pfiDateAdjusted = false;
                    labelAdjustedDueDate.Text =
                        new BusinessRulesProcedures(DS).GetValidDueDate(pl.DueDate.AddMonths(Utilities.GetIntegerValue(ddlNumDaystoExtend.Text, 1)), siteID);
                    dateCalendarLastPickupDate.SelectedDate =
                                 new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).GetValidPFIDate(
                                     pl.PfiEligible.AddMonths(Utilities.GetIntegerValue(ddlNumDaystoExtend.Text, 1)),
                                     siteID, ref pfiDateAdjusted);
                    dateCalendarLastPickupDate.Enabled = false;
                    
                }
                else
                {

                    customTextBoxNumDaystoExtend.Visible = true;
                    ddlNumDaystoExtend.Visible = false;
                    customTextBoxNumDaystoExtend.Text = "30";
                    labelNumDaysToExtendHeading.Visible = true;
                    customTextBoxAmtToExtend.Visible = true;
                    lblAmtToExtend.Visible = false;
                    SetAmountToExtendToUI(ServiceLoanProcedures.GetAmountToExtend(GetDaysToExtendFromUI(), dailyAmount));
                    daysToAdd = GetDaysToExtendFromUI();
                    this.ActiveControl = this.customTextBoxNumDaystoExtend;
                    var pfiDateAdjusted = false;
                    labelDailyAmtHeading.Text = "Daily Amount:";
                    if (partialPaymentAllowed && pl.PartialPaymentPaid && !allowFutureInterestPayments)
                    {
                        labelNumDaysToExtendHeading.Text = "One Month's Full Charge";
                        //customTextBoxNumDaystoExtend.Visible = false;
                        ddlNumDaystoExtend.Visible = false;
                        lblNumDaysToExtend.Visible = true;
                        //customTextBoxAmtToExtend.Visible = false;
                        lblAmtToExtend.Visible = false;
                        //lblNumDaysToExtend.Text = fullMonth;
                        customTextBoxAmtToExtend.Visible = true;
                        customTextBoxAmtToExtend.Text = (daysToPay * dailyAmount).ToString("f2");
                        customTextBoxNumDaystoExtend.Text = (interestAmount + serviceAmount).ToString("f2");
                        labelAdjustedDueDate.Text =
                            new BusinessRulesProcedures(DS).GetValidDueDate(LastCycleEnd.AddMonths(1), siteID);
                        dateCalendarLastPickupDate.SelectedDate =
                                     new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).GetValidPFIDate(
                                         pl.PfiEligible.AddMonths(1),
                                         siteID, ref pfiDateAdjusted);
                        dateCalendarLastPickupDate.Enabled = false;
                        customTextBoxAmtToExtend.Enabled = false;
                        customTextBoxNumDaystoExtend.Enabled = false;

                    }
                    else
                    {

                        labelAdjustedDueDate.Text =
                            new BusinessRulesProcedures(DS).GetValidDueDate(pl.DueDate.AddDays(daysToAdd), siteID);
                        dateCalendarLastPickupDate.SelectedDate =
                                     new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).GetValidPFIDate(
                                         pl.PfiEligible.AddDays(daysToAdd),
                                         siteID, ref pfiDateAdjusted);
                    }
                }

                lblNumDaysToExtend.Visible = false;

                labelDailyAmount.Text = String.Format("{0:0.0000}", dailyAmount);

                daysToExtend = GetDaysToExtendFromUI();
                AmtToExtend = GetAmountToExtendFromUI();
                newPickupDate = dateCalendarLastPickupDate.SelectedDate;
                labelLoanSelection.Text = (this.currIndex + 1) + " of " + this.numberOfLoansToExtend;
                //}

                currentPFIDate = pl.PfiEligible;

                //Show the selection for printing memo of extension in single page 
                //if this is the last loan to be processed
                if (currIndex + 1 == numberOfLoansToExtend)
                {
                    //Check if there are any service loans set for extension already
                    var idx = GlobalDataAccessor.Instance.DesktopSession.ServiceLoans.FindIndex(ploan => ploan.TempStatus == StateStatus.E);

                    if (new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).PrintMultipleMemoOfExtension(siteID) && (numberOfLoansToExtend > 1 || idx >= 0) &&
                        !GlobalDataAccessor.Instance.DesktopSession.PrintSingleMemoOfExtension)
                        this.checkBoxPrintSingleMemoForExtn.Visible = true;
                    this.customButtonContinue.Text = "Submit";
                }
                if (numberOfLoansToExtend == 1)
                    this.customButtonSkip.Visible = false;

            }

        }



        private void customTextBoxNumDaystoExtend_Leave(object sender, EventArgs e)
        {
            //Do the calculation for other fields only if the data changed in this field
            if ((daysToExtend != GetDaysToExtendFromUI() &&
                customTextBoxNumDaystoExtend.isValid) || customTextBoxAmtToExtend.Text.Length == 0)
            {
                if (GetDaysToExtendFromUI() <= 0)
                {
                    if (_extensionType == ExtensionTerms.DAILY)
                        MessageBox.Show(Commons.GetMessageString("ZeroDaysToExtendError"));
                    else
                        MessageBox.Show(Commons.GetMessageString("ZeroMonthsToExtendError"));
                    customTextBoxNumDaystoExtend.Focus();
                    CloseForm = false;
                }
                else
                {
                    CalculateDataFromNumberOfDaysToExtend();
                    if (CloseForm)
                    {
                        CanSubmitForm = true;
                        UpdateSubmitState();
                    }
                }

            }

        }

        private void ddlNumDaystoExtend_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!suppressDaysChangedEvent)
            {
                CalculateDataFromNumberOfDaysToExtend();
                CanSubmitForm = true;
                UpdateSubmitState();
            }
        }

        //Calculate the Amount to extend, new due date and last day to pickup
        //using the number of days to extend
        private void CalculateDataFromNumberOfDaysToExtend()
        {
            var numDaysToExtend = GetDaysToExtendFromUI();
            var pfiDateAdjusted = false;
            decimal amountToExtend;
            int monthsToExtendBy;
            if (partialPaymentAllowed)
            {
                if (_extensionType == ExtensionTerms.MONTHLY)
                {
                    if (selectedLoans[currIndex].PartialPaymentPaid)
                    {
                        monthsToExtendBy = GetDaysToExtendFromUI() - 1;

                        amountToExtend = (monthsToExtendBy * dailyAmount) + (daysToPay * interestAmount / 30) + (daysToPay * serviceAmount / 30);
                    }
                    else
                    {
                        monthsToExtendBy = GetDaysToExtendFromUI();
                        amountToExtend = monthsToExtendBy * dailyAmount;
                    }


                    
                }
                else
                {

                    //amountToExtend = (numDaysToExtend * dailyAmount) + (daysToPay * interestAmount / 30) + (daysToPay * serviceAmount / 30);
                    amountToExtend = numDaysToExtend * dailyAmount;
                }
            }
            else
                amountToExtend = numDaysToExtend * dailyAmount;
            SetAmountToExtendToUI(amountToExtend.ToString("f2"));
            var daysToAdd = numDaysToExtend * ServiceLoanProcedures.GetNumberOfDaysToExtendBy(_extensionType);
            if (_extensionType == ExtensionTerms.DAILY)
            {
                labelAdjustedDueDate.Text =
                    new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).GetValidDueDate(
                        Utilities.GetDateTimeValue(labelCurrDueDate.Text).AddDays(daysToAdd), siteID);
                dateCalendarLastPickupDate.SelectedDate =
                                new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).GetValidPFIDate(
                                    currentPFIDate.AddDays(daysToAdd),
                                    siteID, ref pfiDateAdjusted);

            }
            else
            {
                labelAdjustedDueDate.Text =
                    new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).GetValidDueDate(
                        Utilities.GetDateTimeValue(labelCurrDueDate.Text).AddMonths(numDaysToExtend), siteID);
                dateCalendarLastPickupDate.SelectedDate =
                                new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).GetValidPFIDate(
                                    currentPFIDate.AddMonths(numDaysToExtend),
                                    siteID, ref pfiDateAdjusted);

            }

            /* dateCalendarLastPickupDate.SelectedDate =
                 BusinessRulesProcedures.GetValidPFIDateWithWaitdays(
                     Utilities.GetDateTimeValue(labelAdjustedDueDate.Text),
                     siteID, ref pfiDateAdjusted);*/
            if (pfiDateAdjusted)
                MessageBox.Show(Commons.GetMessageString("ExtendDateAdjustedMessage"));
            checkAmtToExtend();
            daysToExtend = GetDaysToExtendFromUI();
            AmtToExtend = GetAmountToExtendFromUI();
            newPickupDate = dateCalendarLastPickupDate.SelectedDate;


        }

        private void SetAmountToExtendToUI(string value)
        {
            customTextBoxAmtToExtend.Text = value;
            lblAmtToExtend.Text = value;
        }

        private void customTextBoxAmtToExtend_Leave(object sender, EventArgs e)
        {
            //If amount to extend value changed do the calculation
            if ((AmtToExtend != GetAmountToExtendFromUI() &&
                customTextBoxAmtToExtend.isValid) || (customTextBoxNumDaystoExtend.Text.Length == 0))
            {
                if (GetAmountToExtendFromUI() < Utilities.GetDecimalValue(labelDailyAmount.Text))
                {
                    if (_extensionType == ExtensionTerms.DAILY)
                        MessageBox.Show(Commons.GetMessageString("AmountToExtendLessThanDailyAmt"));
                    else
                        MessageBox.Show(Commons.GetMessageString("AmountToExtendLessThanMonthlyAmount"));
                    customTextBoxAmtToExtend.Focus();
                    CloseForm = false;
                }
                else
                {
                    CalculateDataFromAmountToExtend();
                    if ((GetAmountToExtendFromUI() > (Utilities.GetDecimalValue(labelDailyAmount.Text) * GetDaysToExtendFromUI())) ||
                        (GetAmountToExtendFromUI() < (Utilities.GetDecimalValue(labelDailyAmount.Text) * GetDaysToExtendFromUI())))
                    {
                        MessageBox.Show(this._extensionType == ExtensionTerms.DAILY
                                                ? Commons.GetMessageString("AmountToExtendMoreThanDailyAmt")
                                                : Commons.GetMessageString("AmountToExtendMoreThanMonthlyAmount"));
                        customTextBoxAmtToExtend.Text = Utilities.GetStringValue(Math.Round(Utilities.GetDecimalValue(labelDailyAmount.Text) * GetDaysToExtendFromUI(), 2));
                    }
                    if (CloseForm)
                    {
                        CanSubmitForm = true;
                        UpdateSubmitState();
                    }

                }

            }

        }

        private void UpdateSubmitState()
        {
            if (CanSubmitForm)
            {
                customButtonContinue.Visible = true;
                customButtonCalculate.Visible = false;
            }
            else
            {
                customButtonContinue.Visible = false;
                customButtonCalculate.Visible = true;
            }
        }

        //Calculate the days to extend, new due date and the last day to pickup
        //using the amount to extend
        private void CalculateDataFromAmountToExtend()
        {
            var pfiDateAdjusted = false;
            customTextBoxNumDaystoExtend.Text = ServiceLoanProcedures.GetNumberOfExtendDaysFromExtensionAmount(GetAmountToExtendFromUI(), dailyAmount);
            var daysToAdd = GetDaysToExtendFromUI() * ServiceLoanProcedures.GetNumberOfDaysToExtendBy(_extensionType);

            labelAdjustedDueDate.Text =
                new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).GetValidDueDate(
                    Utilities.GetDateTimeValue(labelCurrDueDate.Text).AddDays(
                        daysToAdd), siteID);
            /* dateCalendarLastPickupDate.SelectedDate =
                 BusinessRulesProcedures.GetValidPFIDateWithWaitdays(
                     Utilities.GetDateTimeValue(labelAdjustedDueDate.Text),
                     siteID, ref pfiDateAdjusted);*/
            dateCalendarLastPickupDate.SelectedDate =
                            new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).GetValidPFIDate(
                                currentPFIDate.AddDays(daysToAdd),
                                siteID, ref pfiDateAdjusted);

            if (pfiDateAdjusted)
                MessageBox.Show(Commons.GetMessageString("ExtendDateAdjustedMessage"));

            checkAmtToExtend();
            daysToExtend = GetDaysToExtendFromUI();
            AmtToExtend = GetAmountToExtendFromUI();
            newPickupDate = dateCalendarLastPickupDate.SelectedDate;
 

        }

        //Check if the amount to extend exceeds the pickup or renewal amount
        //and if it does if the business rule allows it
        private void checkAmtToExtend()
        {
            CloseForm = true;
            if (GetAmountToExtendFromUI() > selectedLoans[currIndex].PickupAmount)
            {
                if (!(new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).IsExtensionAllowedPastPickupAmount(siteID)))
                {
                    labelExtendPastPickupAmount.Text += selectedLoans[currIndex].PickupAmount.ToString();
                    labelExtendPastPickupAmount.Visible = true;
                    CloseForm = false;
                }
 
            }
            else
                labelExtendPastPickupAmount.Visible = false;
            if (GetAmountToExtendFromUI() > selectedLoans[currIndex].RenewalAmount)
            {
                if (!(new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).IsExtensionAllowedPastRenewalAmount(siteID)))
                {
                    labelExtendPastRenewAmt.Text += selectedLoans[currIndex].RenewalAmount.ToString();
                    labelExtendPastRenewAmt.Visible = true;
                    CloseForm = false;
                }
 
            }
            else
                labelExtendPastRenewAmt.Visible = false;

        }

        private void dateCalendarLastPickupDate_Leave(object sender, EventArgs e)
        {
            checkLastPickupDate();
            if (CloseForm)
            {
                CanSubmitForm = true;
                UpdateSubmitState();
            }
       
        }

        private void checkLastPickupDate()
        {
            if (newPickupDate != dateCalendarLastPickupDate.SelectedDate)
            {
                if (Utilities.GetDateTimeValue(dateCalendarLastPickupDate.SelectedDate) < selectedLoans[currIndex].PfiEligible)
                {
                    MessageBox.Show(Commons.GetMessageString("ExtensionPickupDateError"));
                    dateCalendarLastPickupDate.Focus();
                    CloseForm = false;
                }
                else
                {
 
                    CalculateDataFromLastDayToPickup();
                }

            }

        }


        //Calculate the number of days to extend, new due date
        //and the amount to extend based on the last day to pickup
        private void CalculateDataFromLastDayToPickup()
        {
            var pfiDateAdjusted = false;
            //DateTime originalPfiDate = Utilities.GetDateTimeValue(dateCalendarLastPickupDate.SelectedDate);
            var originalPfiDate = currentPFIDate;
            dateCalendarLastPickupDate.SelectedDate =
                new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).GetValidPFIDate(
                    Utilities.GetDateTimeValue(dateCalendarLastPickupDate.SelectedDate), siteID,
                    ref pfiDateAdjusted);
            var newPFiDate = Utilities.GetDateTimeValue(dateCalendarLastPickupDate.SelectedDate);
            var extensionDays = Utilities.GetIntegerValue((newPFiDate - originalPfiDate).TotalDays);
            labelAdjustedDueDate.Text =
                new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).GetValidDueDate(
                    Utilities.GetDateTimeValue(labelCurrDueDate.Text).AddDays(
                        extensionDays), siteID);
            if (_extensionType == ExtensionTerms.DAILY)
                customTextBoxNumDaystoExtend.Text = (Utilities.GetDateTimeValue(labelAdjustedDueDate.Text) -
                     Utilities.GetDateTimeValue(labelCurrDueDate.Text)).TotalDays.ToString();
            else
                customTextBoxNumDaystoExtend.Text =
                    Math.Round((Utilities.GetDateTimeValue(labelAdjustedDueDate.Text) -
                                Utilities.GetDateTimeValue(labelCurrDueDate.Text)).TotalDays / 30, 0).ToString();

            SetAmountToExtendToUI(ServiceLoanProcedures.GetAmountToExtend(GetDaysToExtendFromUI(), dailyAmount));
            if (pfiDateAdjusted)
                MessageBox.Show(Commons.GetMessageString("ExtendDateAdjustedMessage"));
            daysToExtend = GetDaysToExtendFromUI();
            AmtToExtend = GetAmountToExtendFromUI();
            newPickupDate = dateCalendarLastPickupDate.SelectedDate;
            checkAmtToExtend();

        }

        private void customTextBoxNumDaystoExtend_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                customTextBoxNumDaystoExtend.Text = (Utilities.GetIntegerValue(customTextBoxNumDaystoExtend.Text) + 1).ToString();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Down)
            {
                customTextBoxNumDaystoExtend.Text = (Utilities.GetIntegerValue(customTextBoxNumDaystoExtend.Text) - 1).ToString();
                e.Handled = true;
            }
            ChangeCanSubmitValue(e.KeyCode);
        }

        private void ChangeCanSubmitValue(Keys key)
        {
            var isVisibleChange = false;
            if (key >= Keys.D0 && key <= Keys.D9)
            {
                isVisibleChange = true;
            }
            else if (key >= Keys.NumPad0 && key <= Keys.NumPad9)
            {
                isVisibleChange = true;
            }
            else if (key == Keys.Decimal)
            {
                isVisibleChange = true;
            }
            else if (key == Keys.Back || key == Keys.Delete)
            {
                isVisibleChange = true;
            }

            if (isVisibleChange)
            {
                CanSubmitForm = false;
                UpdateSubmitState();
            }
        }

        private void customTextBoxAmtToExtend_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                customTextBoxAmtToExtend.Text = (GetAmountToExtendFromUI() + Utilities.GetDecimalValue(labelDailyAmount.Text)).ToString();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Down)
            {
                customTextBoxAmtToExtend.Text = (GetAmountToExtendFromUI() - Utilities.GetDecimalValue(labelDailyAmount.Text)).ToString();
                e.Handled = true;
            }
            ChangeCanSubmitValue(e.KeyCode);
        }

        private void buttonContinue_Click(object sender, EventArgs e)
        {

            var error = false;
            var message = string.Empty;
            if (customTextBoxAmtToExtend.Visible && customTextBoxAmtToExtend.Text.Length == 0)
            {
                CloseForm = false;
                error = true;
                message = "Amount should not be blank";
            }

            if (customTextBoxNumDaystoExtend.Visible && customTextBoxNumDaystoExtend.Text.Length == 0)
            {
                error = true;
                message = "\nNo of days to Extend should not be blank";
                CloseForm = false;
            }

            if (error)
            {
                MessageBox.Show(message);
                return;
            }


            //Check the extend amount to see if it exceeds renewal amount or pickup amount
            checkAmtToExtend();
            if (CloseForm)
            {
                decimal amountToextend = GetAmountToExtendFromUI();
                int numberOfDaysToExtend = GetDaysToExtendFromUI();
                if (amountToextend > 0)
                {

                    //Set the selected values for extension into the pawn loan
                    //selectedLoans[currIndex].NewMadeDate = Utilities.GetDateTimeValue(selectedLoans[currIndex].DateMade);
                    int daysToAdjust = (Utilities.GetDateTimeValue(labelAdjustedDueDate.Text) - selectedLoans[currIndex].DueDate).Days;
                    selectedLoans[currIndex].NewMadeDate = Utilities.GetDateTimeValue(selectedLoans[currIndex].DateMade).AddDays(daysToAdjust);
                    selectedLoans[currIndex].NewDueDate = Utilities.GetDateTimeValue(labelAdjustedDueDate.Text);
                    selectedLoans[currIndex].NewPfiEligible = Utilities.GetDateTimeValue(dateCalendarLastPickupDate.SelectedDate);
                    selectedLoans[currIndex].IsExtended = true;
                    selectedLoans[currIndex].ExtensionAmount = GetAmountToExtendFromUI();
                    selectedLoans[currIndex].DailyAmount = Utilities.GetDecimalValue(labelDailyAmount.Text);
                    //Add the new PFi mailer date
                    selectedLoans[currIndex].NewPfiNote = Utilities.GetDateTimeValue(selectedLoans[currIndex].NewPfiEligible).AddDays(-30);

                    var fee = new Fee
                              {
                                  FeeType = FeeTypes.SERVICE,
                                  Value = GetAmountToExtendFromUI(),
                                  OriginalAmount = GetAmountToExtendFromUI(),
                                  FeeState = FeeStates.ASSESSED,
                                  FeeDate = Utilities.GetDateTimeValue(ShopDateTime.Instance.ShopTransactionTime, DateTime.Now)


                              };
                    //To DO: When we know more details about the extension fees
                    //we need to call getloanfees passing the service type of extension
                    //and add those fees to the list of fees as well
                    selectedLoans[currIndex].OriginalFees.Clear();
                    selectedLoans[currIndex].OriginalFees.Add(fee);
                }
                else
                    return;
                currIndex++;
                if (currIndex < numberOfLoansToExtend)
                    showPawnLoanData();
                else
                {

                    _loansExtended = true;
                    CloseForm = true;
                    SetExtendedLoans();
                    if (this.checkBoxPrintSingleMemoForExtn.Visible)
                        GlobalDataAccessor.Instance.DesktopSession.PrintSingleMemoOfExtension = checkBoxPrintSingleMemoForExtn.Checked;
                    else
                        GlobalDataAccessor.Instance.DesktopSession.PrintSingleMemoOfExtension = false;
                    this.Close();
                }
            }


        }

        private void SetExtendedLoans()
        {
            if (skippedTicketNumbers.Count > 0)
            {
                for (var i = 0; i < skippedTicketNumbers.Count; i++)
                {
                    if (selectedLoans != null)
                    {
                        var index = i;
                        var iDx = selectedLoans.FindIndex(pl => pl.TicketNumber == skippedTicketNumbers[index]);
                        if (iDx >= 0)
                            selectedLoans.RemoveAt(iDx);
                    }
                }
            }
            GlobalDataAccessor.Instance.DesktopSession.SkippedTicketNumbers = skippedTicketNumbers;
            GlobalDataAccessor.Instance.DesktopSession.ExtensionLoans = selectedLoans;

        }

        private void ExtendPawnLoan_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !CloseForm;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            _loansExtended = false;
            CloseForm = true;
            this.Close();
        }

        private void customButtonSkip_Click(object sender, EventArgs e)
        {

            var pawnloanToSkip = selectedLoans[currIndex];
            skippedTicketNumbers.Add(pawnloanToSkip.TicketNumber);
            currIndex++;
            if (currIndex < numberOfLoansToExtend)
                showPawnLoanData();
            else
            {
                //if the last loan is skipped close form
                //and if all loans were skipped set the appropriate variables to reflect that
                CloseForm = true;
                if (skippedTicketNumbers.Count == numberOfLoansToExtend)
                {
                    GlobalDataAccessor.Instance.DesktopSession.ExtensionLoans = new List<PawnLoan>();
                    _loansExtended = false;
                }
                else
                {
                    SetExtendedLoans();
                    _loansExtended = true;
                }
                this.Close();
            }

        }

        private void dateCalendarLastPickupDate_SelectedDateChanged(object sender, EventArgs e)
        {
            CanSubmitForm = false;
            UpdateSubmitState();
        }

        private decimal GetAmountToExtendFromUI()
        {
            return lblAmtToExtend.Visible ? Utilities.GetDecimalValue(lblAmtToExtend.Text, 0) : Utilities.GetDecimalValue(customTextBoxAmtToExtend.Text, 0);
        }

        private int GetDaysToExtendFromUI()
        {
            if (ddlNumDaystoExtend.Visible)
            {
                return Utilities.GetIntegerValue(ddlNumDaystoExtend.Text, 0);
            }
            else if (customTextBoxNumDaystoExtend.Visible)
            {
                return Utilities.GetIntegerValue(customTextBoxNumDaystoExtend.Text, 0);
            }

            //MessageBox.Show("Unsupported case: Can't get days to extend from UI in daily mode");
            return 0;
        }
    }
}
