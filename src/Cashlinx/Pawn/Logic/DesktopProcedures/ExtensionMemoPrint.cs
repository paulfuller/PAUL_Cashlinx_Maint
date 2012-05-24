using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Objects.Pawn;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Shared;

namespace Pawn.Logic.DesktopProcedures
{
    public class ExtensionMemoPrint
    {

        string transactionDate = "";
        string transactionTime = "";
        string customerName = "";

        string empNo = "";
        string tktNo = "";

        string prevMaturityDate;
        string newMaturityDate;

        string lastRedeemDate;
        decimal pawnChargePaidToday = 0.0M;

        decimal amtFinanced = 0.0M;
        decimal pawnChargeMaturity = 0.0M;
        decimal pawnChargeRedeem = 0.0M;
        decimal totalAtMaturity = 0.0M;
        decimal totalAtRedeem = 0.0M;
        decimal dailyAmount = 0.0M;
        int numofExtendLoans = 0;
        int lines = 0;
        string current_item_line;
        string principalAmount = "";

        string dailyAmt = "";
        string totalAmtAtMaturity = "";
        string totalAmtAtRedeem = "";
        decimal sumOfAllmaturityAmounts = 0.0M;
        decimal sumOfAllDailyAmounts = 0.0M;
        decimal sumOfAllAmountsPaidToday = 0.0M;
        

        decimal refundAmt = 0.0M;

        //To DO:Get values from db
        private static readonly string STORE_ADDRESS = "2900 N MAIN STREET";
        private static readonly string STORE_CITY = "FORT WORTH";
        private static readonly string STORE_STATE = "TX";
        private static readonly string STORE_ZIP = "76106";
        private static readonly string STORE_NAME = "CASH AMERICA PAWN OF DFW #56";

        private static readonly string UNREDEEMEDDISCLOSURE1 = "YOU ARE NOT OBLIGATED TO REPAY THIS LOAN. HOWEVER, TO PROTECT YOUR PROPERTY FROM LOSS DUE TO " +
System.Environment.NewLine +
"NON-PAYMENT, YOU MUST EXTEND OR RENEW YOUR LOAN OR REDEEM YOUR PROPERTY ON OR BEFORE THE LAST " +
System.Environment.NewLine +
"DAY OF GRACE.";
        private static readonly string FINALSTMT = "KEEP THIS MEMORANDUM WITH YOUR PAWN TICKET(S). BRING YOUR PAWN TICKET(S) TO REDEEM YOUR PROPERTY.";
        Queue<string> itemLines = new Queue<string>();
        PrintDocument printDocument1 = new PrintDocument();
        PrintPreviewDialog printPreviewDialog1 = new PrintPreviewDialog();

        public List<PawnLoan> ExtensionLoans
        {
            get;
            set;
        }

        public void Print()
        {
            //Check if multiple loans should be printed in one memo or not
            bool printMultipleInOneMemo = GlobalDataAccessor.Instance.DesktopSession.PrintSingleMemoOfExtension;
            //Get all the data to print from the desktop session
            CustomerVO currentCust = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer;
            printDocument1.PrintPage +=
                printDocument1_PrintPage;
            printDocument1.QueryPageSettings +=
                printDocument1_QueryPageSettings;
            printPreviewDialog1.Document = printDocument1;
            numofExtendLoans = ExtensionLoans.Count;
            empNo = GlobalDataAccessor.Instance.DesktopSession.UserName.ToLowerInvariant();
            //Set all the data for printing
            transactionDate = ShopDateTime.Instance.ShopDate.FormatDate();
            transactionTime = ShopDateTime.Instance.ShopTime.Hours + ":" +
                              ShopDateTime.Instance.ShopTime.Minutes;

            foreach (PawnLoan pawnLoan in ExtensionLoans)
            {
                StringBuilder loanData = new StringBuilder();
                if (currentCust != null)
                {
                    if (pawnLoan.CustomerNumber != currentCust.CustomerNumber)
                        currentCust = CustomerProcedures.getCustomerDataByCustomerNumber(GlobalDataAccessor.Instance.DesktopSession, pawnLoan.CustomerNumber);

                        
                        customerName = !string.IsNullOrEmpty(currentCust.MiddleInitial)
                                           ? currentCust.LastName + "," + currentCust.FirstName + "," +
                                             currentCust.MiddleInitial
                                           : currentCust.LastName + "," + currentCust.FirstName;
                        tktNo = pawnLoan.TicketNumber.ToString().PadRight(10);
                        principalAmount = pawnLoan.Amount.ToString().PadRight(10);
                        dailyAmount = pawnLoan.DailyAmount;
                        dailyAmt = string.Format("{0:C}",dailyAmount).PadRight(10);
                        prevMaturityDate = pawnLoan.DueDate.FormatDate().PadRight(20);
                        newMaturityDate = pawnLoan.NewDueDate.FormatDate().PadRight(15);

                        lastRedeemDate = pawnLoan.NewPfiEligible.FormatDate().PadRight(15); ;
                        pawnChargePaidToday = pawnLoan.ExtensionAmount;

                        pawnChargeMaturity = pawnLoan.InterestAmount;
                        totalAtMaturity = (amtFinanced + pawnChargeMaturity);
                        totalAmtAtMaturity = string.Format("{0:C}", totalAtMaturity).PadRight(8);
                        pawnChargeRedeem = (Utilities.GetDecimalValue((pawnLoan.NewPfiEligible - pawnLoan.DueDate).TotalDays / 30)) * pawnLoan.InterestAmount;
                        totalAtRedeem = amtFinanced + pawnChargeRedeem;
                        totalAmtAtRedeem = string.Format("{0:C}",totalAtRedeem).PadRight(10);
                        loanData.Append(tktNo);
                        loanData.Append(principalAmount);
                        loanData.Append(prevMaturityDate);
                        loanData.Append(dailyAmt);
                        loanData.Append(newMaturityDate);
                        loanData.Append(totalAmtAtMaturity);
                        loanData.Append(lastRedeemDate);
                        loanData.Append(totalAmtAtRedeem);
                        loanData.Append(string.Format("{0:C}",pawnChargePaidToday));
                        if (printMultipleInOneMemo)
                        {
                            sumOfAllAmountsPaidToday += pawnChargePaidToday;
                            sumOfAllDailyAmounts += dailyAmount;
                            sumOfAllmaturityAmounts += totalAtMaturity;
                        }
                        else
                        {
                            sumOfAllAmountsPaidToday = pawnChargePaidToday;
                            sumOfAllDailyAmounts = dailyAmount;
                            sumOfAllmaturityAmounts = totalAtMaturity;
                            numofExtendLoans = 1;

                        }
                    

                        itemLines.Enqueue(loanData.ToString());
                        if (printMultipleInOneMemo)
                            continue;
                   

                     printDocument1.Print();
                        
                    
                }
            }
            if (printMultipleInOneMemo)
                printDocument1.Print();
               

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            bool nextPage = false;
            lines = 0;
            //Header in each page

            e.Graphics.DrawString("Date:" + transactionDate, new Font("Courier New", 9, FontStyle.Regular), Brushes.Black, 632, 13);
            e.Graphics.DrawString("MEMORANDUM OF EXTENSION", new Font("Courier New", 9, FontStyle.Regular), Brushes.Black, 310, 13);
            //e.Graphics.DrawString("Ticket No:  " + tktNo.ToString(), new Font("Courier New", 9, FontStyle.Regular), Brushes.Black, 632, 13);
            e.Graphics.DrawString("Time:" + transactionTime, new Font("Courier New", 9, FontStyle.Regular), Brushes.Black, 635, 35);
            e.Graphics.DrawString("Emp No:" + empNo, new Font("Courier New", 9, FontStyle.Regular), Brushes.Black, 13, 35);
            e.Graphics.DrawString(STORE_NAME, new Font("Courier New", 9, FontStyle.Regular), Brushes.Black, 310, 45);
            e.Graphics.DrawString(STORE_ADDRESS + " " + STORE_CITY + " " + STORE_STATE + " " + STORE_ZIP, new Font("Courier New", 9, FontStyle.Regular), Brushes.Black, 241, 58);


            e.Graphics.DrawString("Loan" + System.Environment.NewLine + "Number " , new Font("Courier New", 8, FontStyle.Regular), Brushes.Black, 13, 78);
            e.Graphics.DrawString("Loan" + System.Environment.NewLine +"Amount" + System.Environment.NewLine +"(Principal)", new Font("Courier New", 8, FontStyle.Regular), Brushes.Black, 80, 78);
            e.Graphics.DrawString("Finance" + System.Environment.NewLine + "Charges(PSC) " + System.Environment.NewLine + "Paid thru", new Font("Courier New", 8, FontStyle.Regular), Brushes.Black, 181, 78);
            e.Graphics.DrawString("Daily Amt" + System.Environment.NewLine + "of Finance " + System.Environment.NewLine + "Charges(FSC)", new Font("Courier New", 8, FontStyle.Regular), Brushes.Black, 299, 78);
            e.Graphics.DrawString("New" + System.Environment.NewLine + "Maturity " + System.Environment.NewLine + "Date", new Font("Courier New", 8, FontStyle.Regular), Brushes.Black, 399, 78);
            e.Graphics.DrawString("Amt Due " + System.Environment.NewLine + "At maturity" + System.Environment.NewLine + "Date", new Font("Courier New", 8, FontStyle.Regular), Brushes.Black, 489, 78);
            e.Graphics.DrawString("New" + System.Environment.NewLine + "Last Day " +System.Environment.NewLine + "of Grace", new Font("Courier New", 8, FontStyle.Regular), Brushes.Black, 589, 78);
            e.Graphics.DrawString("Amt Due " + System.Environment.NewLine + "to redeem " + System.Environment.NewLine + "on Last Day" + System.Environment.NewLine + "of Grace", new Font("Courier New", 8, FontStyle.Regular), Brushes.Black, 669, 78);
            e.Graphics.DrawString("Amt Paid " + System.Environment.NewLine  + "Today", new Font("Courier New", 8, FontStyle.Regular), Brushes.Black, 749, 78);
            
            
            
            

            //Loan Details
            int y = 123;

            while (itemLines.Count > 0 && !nextPage)
            {


                lines++;
                if (lines > 15)
                {
                    nextPage = true;
                    e.HasMorePages = true;

                }
                else
                {
                    current_item_line = itemLines.Dequeue();
                    y = y + 20;
                    e.Graphics.DrawString(current_item_line, new Font("Courier New", 9, FontStyle.Regular), Brushes.Black, 13, y);
                }
            }

            
            //Footer

            e.Graphics.DrawString("TOTAL AMOUNT DUE AT MATURITY  :" + string.Format("{0:C}", sumOfAllmaturityAmounts), new Font("Courier New", 9, FontStyle.Regular), Brushes.Black, 13, 756);

            e.Graphics.DrawString("Total Number of Loans Extended  :" + numofExtendLoans.ToString(), new Font("Courier New", 9, FontStyle.Regular), Brushes.Black, 13, 800);

            e.Graphics.DrawString("Total Finance Charge (Pawn Service Charge ('PSC')) Paid Today: " + string.Format("{0:C}", sumOfAllAmountsPaidToday), new Font("Courier New", 9, FontStyle.Regular), Brushes.Black, 13, 820);

            e.Graphics.DrawString("Total Daily Pawn Service Charges: " + string.Format("{0:C}", sumOfAllDailyAmounts), new Font("Courier New", 9, FontStyle.Regular), Brushes.Black, 13, 840);

            e.Graphics.DrawString("Pledgor : " + customerName, new Font("Courier New", 9, FontStyle.Regular), Brushes.Black, 12, 889);
            e.Graphics.DrawLine(new Pen(Brushes.Black), new Point(542, 895), new Point(678, 895));
            e.Graphics.DrawString("pledgor signature", new Font("Courier New", 9, FontStyle.Regular), Brushes.Black, 558, 920);

            e.Graphics.DrawString(UNREDEEMEDDISCLOSURE1, new Font("Courier New", 9, FontStyle.Regular), Brushes.Black, 13, 972);
            

            
            e.Graphics.DrawString(FINALSTMT, new Font("Courier New", 9, FontStyle.Regular), Brushes.Black, 15, 1020);


        }

        private void printDocument1_QueryPageSettings(object sender, System.Drawing.Printing.QueryPageSettingsEventArgs e)
        {

            //e.PageSettings.Landscape = true;


        }

    }
}
