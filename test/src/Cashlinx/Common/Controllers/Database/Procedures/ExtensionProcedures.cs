using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Common.Controllers.Application;
using Common.Controllers.Database.Oracle;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Shared;
using Oracle.DataAccess.Client;

namespace Common.Controllers.Database.Procedures
{
    public static class ExtensionProcedures
    {


        public static bool ExtensionEligibility(
            DateTime loanDateMade,
            DateTime extensionDate,
            DateTime dueDate,
            DateTime pfiNote,
            DateTime partialPmtDate,
            DateTime pfiDate,
            out ExtensionTerms extensionType)
        {
            extensionType = ExtensionTerms.MONTHLY;
            DateTime dailyModePeriod = pfiDate.AddDays(-30);
            //If extension date is the same as partial payment date then
            //extension is not allowed
            if (extensionDate == partialPmtDate)
            {
                return false;
            }

            LoanMonthDates LM1 = new LoanMonthDates();
            LM1.startDate = loanDateMade;
            LM1.endDate = LM1.startDate.AddMonths(1);
            LoanMonthDates LM2 = new LoanMonthDates();
            LM2.startDate = LM1.endDate;
            LM2.endDate = LM2.startDate.AddMonths(1);
            LoanMonthDates LM3 = new LoanMonthDates();
            LM3.startDate = LM2.endDate;
            LM3.endDate = LM3.startDate.AddMonths(1);
            //if there is no partial payment and if extension date
            //has passed the due date extension can only be monthly
            if (partialPmtDate == DateTime.MaxValue)
            {
                if (extensionDate >= LM1.endDate)
                {
                    extensionType = ExtensionTerms.MONTHLY;
                    return true;
                }
            }
            else
            {
                //partial payment is made and it is not in the daily mode period
                //or it is in the daily mode period but the extension date is greater than the pfi date
                if (extensionDate >= pfiDate.AddMonths(1) || !(partialPmtDate <= pfiDate && partialPmtDate >= dailyModePeriod))
                {
                    DateTime first_cycle_end = DateTime.MaxValue;
                    DateTime next_cycle_end = DateTime.MaxValue;
                    if (partialPmtDate == LM1.endDate || partialPmtDate == LM2.endDate || partialPmtDate == LM3.endDate)
                        first_cycle_end = partialPmtDate;
                    else
                    {
                        if (partialPmtDate < LM1.endDate)
                            first_cycle_end = LM1.endDate;
                        else if (partialPmtDate < LM2.endDate)
                            first_cycle_end = LM2.endDate;
                        else if (partialPmtDate < LM3.endDate)
                            first_cycle_end = LM3.endDate;
                    }
                    if (first_cycle_end != DateTime.MaxValue)
                        next_cycle_end = first_cycle_end.AddMonths(1);

                    if (extensionDate >= next_cycle_end)
                    {
                        extensionType = ExtensionTerms.MONTHLY;
                        return true;
                    }

                }
                if (extensionDate < pfiDate.AddMonths(1) && (partialPmtDate <= pfiDate && partialPmtDate >= dailyModePeriod))
                {
                    //partial payment date is in daily mode period and extension date is less than 1 month from pfi date
                    extensionType = ExtensionTerms.DAILY;
                    return true;
                }
            }
            return false;
        }


        public static bool GetExtensionPeriod(DateTime partialPaymentDate,
            DateTime loanDateMade,
            DateTime currentDate,
            DateTime dueDate,
            DateTime pfiNote,
            DateTime pfiDate,
            ExtensionTerms extensionType,
            out int daysToPay,
            out int monthsToPay,
            out DateTime lastCycleEnd)
        {
            //Set output vars
            string errorCode = string.Empty;
            string errorText = string.Empty;
            daysToPay = 0;
            monthsToPay = 0;

            lastCycleEnd = DateTime.MaxValue;



            //Ensure the data accessor is valid
            if (GlobalDataAccessor.Instance.OracleDA == null ||
                GlobalDataAccessor.Instance.OracleDA.Initialized == false)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteGetNextNumber Failed",
                                                            new ApplicationException("ExecuteGetNextNumber Failed: Data accessor instance is invalid"));
                return (false);
            }
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;
            List<string> miscFlags = new List<string>();
            if (extensionType == ExtensionTerms.MONTHLY)
                miscFlags.Add("M");
            else
                miscFlags.Add("D");
            //Create parameter list

            List<OracleProcParam> oParams = new List<OracleProcParam>();
            oParams.Add(new OracleProcParam("date_made", loanDateMade.ToShortDateString()));
            oParams.Add(new OracleProcParam("due_date", dueDate.ToShortDateString()));
            oParams.Add(new OracleProcParam("pu_date", currentDate.ToShortDateString()));
            oParams.Add(new OracleProcParam("pp_date_made", partialPaymentDate.ToShortDateString()));
            oParams.Add(new OracleProcParam("misc_flags", true, miscFlags));
            oParams.Add(new OracleProcParam("o_cycles_late", OracleDbType.Decimal, DBNull.Value, ParameterDirection.Output, 1));
            oParams.Add(new OracleProcParam("o_days_into_cycle", OracleDbType.Decimal, DBNull.Value, ParameterDirection.Output, 1));
            oParams.Add(new OracleProcParam("o_last_cyc_end", OracleDbType.Date, DBNull.Value, ParameterDirection.Output, 1));
            oParams.Add(new OracleProcParam("o_next_cyc_end", OracleDbType.Date, DBNull.Value, ParameterDirection.Output, 1));
            oParams.Add(new OracleProcParam("o_pp_days_aref", OracleDbType.Decimal, DBNull.Value, ParameterDirection.Output, 1));
            oParams.Add(new OracleProcParam("o_pp_days_cred", OracleDbType.Decimal, DBNull.Value, ParameterDirection.Output, 1));


            //Execute stored proc
            DataSet outputSet;
            bool retVal;
            try
            {
                retVal = GlobalDataAccessor.Instance.OracleDA.issueSqlStoredProcCommand("ccsowner",
                                                                                            "service_pawn_loans", "get_cycles_late_cnt", oParams, null, "o_return_code",
                                                                                            "o_return_text", out outputSet);
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("get_cycles_late_cnt Failed", oEx);
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("get_cycles_late_cnt Failed: return value is false", new ApplicationException());
                return (false);
            }

            //Get output number
            int cyclesLate = 0;
            int daysIntoCycle = 0;
            DataTable outputDt = outputSet.Tables["OUTPUT"];
            if (outputDt != null && outputDt.IsInitialized && outputDt.Rows != null && outputDt.Rows.Count > 0)
            {
                DataRow dr = outputDt.Rows[0];
                if (dr != null && dr.ItemArray.Length > 0)
                {
                    object nextNumObj = dr.ItemArray.GetValue(1);
                    if (nextNumObj != null)
                    {
                        var nextNumStr = (string)nextNumObj;
                        cyclesLate = Utilities.GetIntegerValue(nextNumStr);
                    }
                }
                DataRow dr1 = outputDt.Rows[1];
                if (dr1 != null && dr1.ItemArray.Length > 0)
                {
                    object nextNumObj = dr1.ItemArray.GetValue(1);
                    if (nextNumObj != null)
                    {
                        var nextNumStr = (string)nextNumObj;
                        daysIntoCycle = Utilities.GetIntegerValue(nextNumStr);
                    }
                }
                DataRow dr2 = outputDt.Rows[2];
                if (dr2 != null && dr2.ItemArray.Length > 0)
                {
                    object nextNumObj = dr2.ItemArray.GetValue(1);
                    if (nextNumObj != null)
                    {
                        var nextNumStr = (string)nextNumObj;
                        lastCycleEnd = Utilities.GetDateTimeValue(nextNumStr);
                    }
                }

            }
            DateTime currentDateLoanStartDate = DateTime.MaxValue;
            DateTime ppmtLoanStartDate = DateTime.MaxValue;
            if (extensionType == ExtensionTerms.MONTHLY)
            {
                int cycles_late=0;
                
                
                
                int numberOfMonths;
                DateTime next_cycle_end = loanDateMade;
                if (partialPaymentDate == DateTime.MaxValue)
                {
                    monthsToPay = 1;
                    if (cyclesLate == 0)
                    {
                        daysToPay = 0;
                    }
                    else
                    {

                        monthsToPay += cyclesLate;
                        if (currentDate > lastCycleEnd)
                            daysToPay = (currentDate - lastCycleEnd).Days;
                        else
                            daysToPay = (lastCycleEnd - currentDate).Days;
                    }
                    
 

                }



                else
                {
                    int ppmtLoanMonth = PartialPaymentProcedures.GetLoanMonth(loanDateMade, dueDate, pfiNote, pfiDate, partialPaymentDate, ref ppmtLoanStartDate);
                    int paidDays = (partialPaymentDate - ppmtLoanStartDate).Days;

                    
                    int currentDateLoanMonth = PartialPaymentProcedures.GetLoanMonth(loanDateMade, dueDate, pfiNote, pfiDate, currentDate, ref currentDateLoanStartDate);
                    // 1 is added below so that when the loan month transition date is same as L_PartPmt_Date, it is not considered
                    int daysLeft =30-paidDays;
                    next_cycle_end = ppmtLoanStartDate.AddMonths(1);

                    while (currentDate >= next_cycle_end)
                    {
                        cycles_late++;
                        next_cycle_end = next_cycle_end.AddMonths(1);
                    }
                    daysToPay = daysLeft;

                    monthsToPay = cycles_late;
     


                }

                
            }
            else
            {
                if (lastCycleEnd != DateTime.MaxValue && partialPaymentDate != DateTime.MaxValue)
                {
                    int ppmtLoanMonth = PartialPaymentProcedures.GetLoanMonth(loanDateMade, dueDate, pfiNote, pfiDate, partialPaymentDate, ref ppmtLoanStartDate);
                    int paidDays = (partialPaymentDate - ppmtLoanStartDate).Days;

                    //int currentDateLoanMonth = PartialPaymentProcedures.GetLoanMonth(loanDateMade, dueDate, pfiNote, pfiDate, currentDate, ref currentDateLoanStartDate);
                    //int daysinPpmTdatecycle = (partialPaymentDate - currentDateLoanStartDate).Days;
                    int daysfromcurrenttoppmt = (currentDate - partialPaymentDate).Days;
                    if (paidDays + daysfromcurrenttoppmt > 30)
                        daysToPay = 30 - paidDays;
                    else
                        daysToPay = daysfromcurrenttoppmt;
                }
            }

            return (true);

        }

   

        private struct LoanMonthDates
        {
            public DateTime startDate;
            public DateTime endDate;
        }

    }
}
