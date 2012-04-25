using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Controllers.Database.Procedures
{
    public static class PartialPaymentProcedures
    {
        public static void GetPartialPaymentDaysToPay(
            DateTime partialPaymentDate,
            DateTime currentDate,
            DateTime loanDate,
            DateTime dueDate,
            DateTime pfiDate,
            DateTime pfiNote,out int numberOfDaysToPay)
        {
            DateTime first_cycle_end=dueDate;
            DateTime next_cycle_end = dueDate;
            DateTime ppmtLoanStartDate=DateTime.MaxValue;
            DateTime currentDateLoanStartDate=DateTime.MaxValue;
            int ppmtLoanMonth=GetLoanMonth(loanDate,dueDate,pfiNote,pfiDate,partialPaymentDate,ref ppmtLoanStartDate);
            int currentDateLoanMonth = GetLoanMonth(loanDate, dueDate, pfiNote, pfiDate, currentDate,ref currentDateLoanStartDate);
            int daysAlreadyPaid=0;
            int daysToPay=0;



                if (ppmtLoanMonth == currentDateLoanMonth)
                {

                    //This will result in zero if Date1 and due date are the same
                    daysAlreadyPaid = (partialPaymentDate - currentDateLoanStartDate).Days;
                    daysToPay = (currentDate - partialPaymentDate).Days;

                    // Need to apply ceiling for 30 days
                    if (daysToPay + daysAlreadyPaid > 30)
                    {
                        numberOfDaysToPay = 30 - daysAlreadyPaid;

                        //The below scenario will occur when loan date is 1/1/2012 and a partial payment is made on 2/1/2012 and the user is trying to make another partial payment on the same day as PAIDDAYS will result in 31

                        if (numberOfDaysToPay < 0) numberOfDaysToPay = 0;
                    }
                    else
                        numberOfDaysToPay = daysToPay;

                }
                else
                {

                    int daysToPayremainingMonths = 0;
                    daysAlreadyPaid = (partialPaymentDate - ppmtLoanStartDate).Days;
                    int daysToPayFirstLoanMonth = (30 - daysAlreadyPaid);

                    //The below scenario will occur when loan date is 1/1/2012 and a partial payment is made on 2/1/2012 and the user is trying to make another partial payment on say for ex: 2/15/2012 as PAIDDAYS1 will be 31
                    if (daysToPayFirstLoanMonth < 0) daysToPayFirstLoanMonth = 0; // Might result in 1/1/2012 loan


                    int daysToPaySecondMonth = (currentDate - currentDateLoanStartDate).Days;

                    //Apply 30 day ceiling
                    if (daysToPaySecondMonth > 30) // Need to apply ceiling for 30 days
                    {
                        daysToPaySecondMonth = 30;
                    }
                    if (currentDate > partialPaymentDate && currentDateLoanMonth - ppmtLoanMonth > 1)
                    {
                        int numOfMonths = ((currentDateLoanStartDate - ppmtLoanStartDate).Days / 30 - 1);

                        daysToPayremainingMonths = 30 * numOfMonths;



                    }
                    numberOfDaysToPay = daysToPayFirstLoanMonth + daysToPaySecondMonth + daysToPayremainingMonths;
                }


            


       }


        public static int GetLoanMonth(DateTime loanDate, DateTime dueDate, DateTime pfiNote, DateTime pfiDate, DateTime currentDate,ref DateTime loanStartDateMonth)
        {
            int loanMonth = 1;
            DateTime next_cycle_end = loanDate.AddMonths(1);
            DateTime first_cycle_end = loanDate;
            
            while (currentDate >= next_cycle_end)
            {
                loanMonth++;
                first_cycle_end = first_cycle_end.AddMonths(1);
                next_cycle_end = next_cycle_end.AddMonths(1);

            }
            loanStartDateMonth = first_cycle_end;
            return loanMonth;

        }





    }
}
