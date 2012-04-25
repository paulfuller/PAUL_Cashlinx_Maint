/************************************************************************
 * Namespace:       CashlinxDesktop.DesktopForms.Pawn.Customer.Stats
 * Class:           Controller_Stats
 * 
 * Description      A Controller form for Customer Pawn Loan Stats
 * 
 * History
 * David D Wise, Initial Development
 * Sreelatha Rengarajan Added Renewal, Paydown, Extend, Police Seize amounts
 * SR 4/16/2010 Fixed the amount shown for pickup and the service charges calculation
 * SR 5/19/2010 Fixed the service charges paid calculation since it was omitting fees if different fee
 * types were the same amounts
 * SR 11/30/2010 Changed the layout and the way in which stats data is pulled
 * ***************************************************************************************/

using System;
using System.Data;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Forms;
using Common.Libraries.Utility;
using Pawn.Logic;

namespace Pawn.Forms.Pawn.Customer.Stats
{
    public partial class Controller_Stats : Form
    {
        private int Number_LoansInPawn;
        private decimal Amount_LoansInPawn;
        private int Number_Loans = 0;
        private decimal Amount_Loans = 0;
        private int Number_LoansPoliceSeized;
        private decimal Amount_LoansPoliceSeized;

        private decimal Amount_PawnServiceCharges = 0;
        private decimal PCT_LoanPick = 0;
        private decimal PCT_LoanPickJewelry = 0;
        private decimal PCT_LoanPickUpGenMerchandise = 0;
        private int Number_Buys = 0;
        private decimal Amount_Buys = 0;
        private int Number_Sales = 0;
        private decimal Amount_Sales = 0;
        private int Number_Refunds = 0;
        private decimal Amount_Refunds = 0;
        private decimal PCT_AverageDiscount = 0;
        private decimal Cust_Overall_PCT = 0;
        private int Number_ActiveLayAways = 0;
        private decimal Amount_ActiveLayAways = 0;
        private int Number_PaidLayAways = 0;
        private decimal Amount_PaidLayAways = 0;
        private decimal Amount_StoreCreditAvailable = 0;
        private DateTime Customer_Since = DateTime.MinValue;
        private string custNumber;


        public NavBox NavControlBox;


 

        public Controller_Stats()
        {
            InitializeComponent();
            this.NavControlBox = new NavBox();
            Setup();
            this.NavControlBox.Owner = this;

                
        }

        private void Setup()
        {

            DataSet dsCustStats = new DataSet();
            string errorCode;
            string errorText;
            bool bRetValue;
            //Save the stats data in session until the user wishes to cancel out of the screen
            //including any of the tabs or when the user does not wish to continue with the same
            //customer so that we do not call the SP multiple times in the same session
            //if not needed.
            //string fromdate = ShopDateTime.Instance.ShopDate.AddMonths(-6).ToShortDateString();
            string todate = ShopDateTime.Instance.ShopDate.ToShortDateString(); 
            dsCustStats = GlobalDataAccessor.Instance.DesktopSession.CustStatsDataSet;
            Sts_StatisticsLabel.Text = "Customer Statistics as of " + todate;
            custNumber = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.CustomerNumber;
            if (dsCustStats == null)
            {

                bRetValue = new CustomerDBProcedures(GlobalDataAccessor.Instance.DesktopSession).GetCustomerStatsData(
                    GlobalDataAccessor.Instance.OracleDA,
                    custNumber,
                    out dsCustStats,
                    out errorCode,
                    out errorText);

                if (bRetValue && dsCustStats != null)
                    GlobalDataAccessor.Instance.DesktopSession.CustStatsDataSet = dsCustStats;
            }
            else
            {
                bRetValue = true;

            }

            if (bRetValue)
            {
                //Sts_StoreCreditAvailablePanel.Visible = false;

                Customer_Since = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.CustomerSince;
                Sts_General_CustomerSinceLabel.Text = "Customer Since:  "+  Customer_Since.Year;
                Sts_creditAsOfLabel.Text = string.Format("Store Credit as of: {0:g}", DateTime.Now);
                if (dsCustStats != null)
                {
                    // CheckBox for Store credit
                    DataTable outputDt = dsCustStats.Tables["OUTPUT"];
                    if (outputDt != null && outputDt.IsInitialized &&
                        outputDt.Rows != null && outputDt.Rows.Count > 0)
                    {
                        DataRow dr = outputDt.Rows[0];
                        if (dr != null && dr.ItemArray.Length > 0)
                        {
                            string storeCredit_obj = dr.Field<string> (1);

                            if (storeCredit_obj != "null")
                            {
                                Sts_StoreCreditAvailablePanel.Visible = true;
                                Amount_StoreCreditAvailable = decimal.Parse(storeCredit_obj);
                            }
                        }
                    }


                    if (dsCustStats.Tables["customer_stats"] == null)
                    {
                        MessageBox.Show("No stats returned for Current Customer.", "Customer Loan Lookup");
                        //Sts_PawnLoanPanel.Visible = false;
                        //Sts_BuysPanel.Visible = false;
                        //Sts_LayAwaysPanel.Visible = false;
                        //Sts_PawnLoanPanel.Visible = false;
                        //Sts_SalesPanel.Visible = false;
                        //panel3.Visible = false;
                    }
                    else
                    {

                        DataTable custData = dsCustStats.Tables[0];

                        Sts_StatisticsLabel.Text = "Customer Statistics as of " + custData.Rows[0].Field<DateTime>("LASTUPDATEDATE").ToShortDateString();

                        Number_Loans = Utilities.GetIntegerValue(custData.Rows[0]["loan_count"], 0);

                        Amount_Loans = Utilities.GetDecimalValue(custData.Rows[0]["loan_amount"], 0);

                        Number_LoansInPawn = Utilities.GetIntegerValue(custData.Rows[0]["loans_ip_count"], 0);

                        Amount_LoansInPawn = Utilities.GetDecimalValue(custData.Rows[0]["loans_ip_amount"], 0);

                        //Buy data

                        Number_Buys = Utilities.GetIntegerValue(custData.Rows[0]["purchase_count"], 0);
                        Amount_Buys = Utilities.GetDecimalValue(custData.Rows[0]["purchase_amount"], 0);
                                              

                        //Police Seized Loans Data
                        Number_LoansPoliceSeized = Utilities.GetIntegerValue(custData.Rows[0]["seize_count"], 0);
                        Amount_LoansPoliceSeized = Utilities.GetDecimalValue(custData.Rows[0]["seize_amount"], 0);

                        //Total fees paid by the customer when they did extension,pickup,renewal and paydown plus
                        //the amount of the loans picked up

                        Amount_PawnServiceCharges = Utilities.GetDecimalValue(custData.Rows[0]["pawn_svc_charges"], 0);

                        PCT_LoanPick = Utilities.GetDecimalValue(custData.Rows[0]["loan_pu_pct"]);
                        PCT_LoanPickJewelry = Utilities.GetDecimalValue(custData.Rows[0]["loan_pu_pct_jew"]);
                        PCT_LoanPickUpGenMerchandise = Utilities.GetDecimalValue(custData.Rows[0]["loan_pu_pct_gen"]);

                        //Sales Data
                        Number_Sales = Utilities.GetIntegerValue(custData.Rows[0]["sales_count"], 0);
                        Amount_Sales = Utilities.GetDecimalValue(custData.Rows[0]["sales_amount"], 0);

                        //Refund Data
                        Number_Refunds = Utilities.GetIntegerValue(custData.Rows[0]["refund_count"], 0);
                        Amount_Refunds = Utilities.GetDecimalValue(custData.Rows[0]["refund_amount"], 0);
                        //Discount percentages
                        PCT_AverageDiscount = Utilities.GetDecimalValue(custData.Rows[0]["cust_avg_disc_pct"], 0);
                        Cust_Overall_PCT = Utilities.GetDecimalValue(custData.Rows[0]["cust_ovl_gp_pct"], 0);

                        //Layaway data
                        Number_ActiveLayAways = Utilities.GetIntegerValue(custData.Rows[0]["layaway_count"], 0);
                        Amount_ActiveLayAways = Utilities.GetDecimalValue(custData.Rows[0]["layaway_amount"], 0);
                        Number_PaidLayAways = Utilities.GetIntegerValue(custData.Rows[0]["layaway_paid_count"], 0);
                        Amount_PaidLayAways = Utilities.GetDecimalValue(custData.Rows[0]["layaway_paid_amount"], 0);

                        //store credit
                        Amount_StoreCreditAvailable = Utilities.GetDecimalValue(custData.Rows[0]["store_credit"], 0);
                        
                    }
                    labelLoansNumber.Text = String.Format("{0}", Number_Loans);
                    labelLoansAmount.Text = String.Format("{0:0.00}", Amount_Loans);
                    labelLoansInPawnNumber.Text = String.Format("{0}", Number_LoansInPawn);
                    labelLoansInPawnAmount.Text = String.Format("{0:0.00}", Amount_LoansInPawn);
                    labelSeizeNumber.Text = String.Format("{0}", Number_LoansPoliceSeized);
                    labelSeizeAmount.Text = String.Format("{0:0.00}", Amount_LoansPoliceSeized);

                    labelPawnSVCChargesPaid.Text = String.Format("{0:0.00}", Amount_PawnServiceCharges);

                    labelLoansPUPct.Text = String.Format("{0:0.00%}", PCT_LoanPick);
                    labelLoansPUPctJewel.Text = String.Format("{0:0.00%}", PCT_LoanPickJewelry);
                    labelLoansPUPctGenMdse.Text = String.Format("{0:0.00%}", PCT_LoanPickUpGenMerchandise);

                    labelBuyNumber.Text = String.Format("{0}", Number_Buys);
                    labelBuyAmount.Text = String.Format("{0:0.00}", Amount_Buys);
                    labelSalesNumber.Text = String.Format("{0}", Number_Sales);
                    labelSalesAmount.Text = String.Format("{0:0.00}", Amount_Sales);
                    labelRefundNumber.Text = String.Format("{0}", Number_Refunds);
                    labelRefundAmount.Text = String.Format("{0:0.00}", Amount_Refunds);
                    labelAvgDiscPct.Text = String.Format("{0:0.00%}", PCT_AverageDiscount);
                    labelCustOverallGPPCT.Text = String.Format("{0:0.00%}", Cust_Overall_PCT);
                    labelActiveLayawayNumber.Text = String.Format("{0}", Number_ActiveLayAways);
                    labelActiveLayawayAmount.Text = String.Format("{0:0.00}", Amount_ActiveLayAways);
                    labelPaidLayawayNumber.Text = String.Format("{0}", Number_PaidLayAways);
                    labelPaidLayawayAmount.Text = String.Format("{0:0.00}", Amount_PaidLayAways);
                    labelStoreCreditAmountAvail.Text = String.Format("{0:0.00}", Amount_StoreCreditAvailable);
                }
            }

            
            
            

        }




        private void customButtonCancel_Click(object sender, EventArgs e)
        {
            if (GlobalDataAccessor.Instance.DesktopSession.StartNewPawnLoan || GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger.Equals("newpawnloan", StringComparison.OrdinalIgnoreCase) ||
                GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger.Equals("describemerchandise",StringComparison.OrdinalIgnoreCase))
            {
                GlobalDataAccessor.Instance.DesktopSession.TabStateClicked = FlowTabController.State.None;
                this.NavControlBox.IsCustom = true;
                this.NavControlBox.CustomDetail = "Close";
                this.NavControlBox.Action = NavBox.NavAction.BACK;

            }
            else
            {
                var dR = MessageBox.Show("Do you want to continue processing this customer?", "Stats", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dR == DialogResult.No)
                {
                    GlobalDataAccessor.Instance.DesktopSession.ClearCustomerList();

                }
                //1/29/2010 According to QA requirement Cancel should take you to ring menu!
                this.NavControlBox.IsCustom = true;
                this.NavControlBox.CustomDetail = "Menu";
                this.NavControlBox.Action = NavBox.NavAction.BACK;
            }

        }
    }
}
