using System.Linq;
using Common.Controllers.Application;
using Common.Controllers.Database.Procedures;
using Common.Controllers.Security;
using Common.Libraries.Forms.Components;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using System;
using System.Collections.Generic;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Objects.Pawn;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.ISharp;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using Common.Libraries.Utility;

namespace Pawn.Logic.PrintQueue
{
    public class LostTicketStatementPrint
    {
        string transactionDate = "";
        string customerName = "";
        decimal loanAmount = 0.0M;
        int tktNo = 0;
        string custAddr;
        string custAddrLine2;
        decimal lateCharges = 0.0M;
        string idData;
        decimal refundAmt = 0.0M;
        string loanDate;
        string loanDueDate;
        decimal totalPayments = 0.0M;
        decimal lostTktFee = 0.0M;
        string apr;
        string custMiddleInitial = "";
 
        public void Print(PawnLoan pawnLoan)
        {
            if (pawnLoan != null)
            {
                //Get all the data to print from the desktop session
                CustomerVO currentCust = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer;
                if (pawnLoan.CustomerNumber != currentCust.CustomerNumber)
                    currentCust = CustomerProcedures.getCustomerDataByCustomerNumber(GlobalDataAccessor.Instance.DesktopSession, pawnLoan.CustomerNumber);
                if (currentCust != null)
                {
                    //Set all the date for printing
                    transactionDate = ShopDateTime.Instance.ShopDate.FormatDate();
                    tktNo = pawnLoan.TicketNumber;
                    customerName = currentCust.LastName + "," + currentCust.FirstName;
                    custMiddleInitial = currentCust.MiddleInitial.ToUpper();
                                         
                    loanAmount = pawnLoan.Amount;
                    AddressVO custAddress = currentCust.getHomeAddress();
                    if (custAddress != null)
                    {
                        custAddr = custAddress.Address1 + " " + custAddress.Address2;
                        custAddrLine2 = custAddress.City + "," + custAddress.State_Code + " " + custAddress.ZipCode;
                    }
                    IdentificationVO custId = currentCust.getFirstIdentity();
                    if (custId != null)
                    {
                        idData = custId.IdType + "-" + custId.IdIssuerCode + "-" + custId.IdValue;
                    }
                    loanDate = pawnLoan.OriginationDate.FormatDate();
                    loanDueDate = pawnLoan.DueDate.FormatDate();
                    lostTktFee = pawnLoan.LostTicketInfo.LostTicketFee;
                    //Get the interest amount
                    decimal interestCharges = 0.0M;
                    var finCharges = (from feeData in pawnLoan.Fees
                                      where feeData.FeeType == FeeTypes.INTEREST
                                      select feeData).FirstOrDefault();

                    if (finCharges.Value != 0)
                        interestCharges = finCharges.Value;
                    
                    //Get the late fees
                    //If it is negative then it is a refund else it is a late fee
                    var lateFee = (from feeData in pawnLoan.Fees
                                   where feeData.FeeType == FeeTypes.LATE
                                   select feeData).FirstOrDefault();

                    if (lateFee.Value < 0)
                        refundAmt = lateFee.Value;
                    else
                        lateCharges = lateFee.Value;
                    totalPayments = loanAmount + interestCharges + refundAmt + lateCharges + lostTktFee;
                    apr = pawnLoan.InterestRate.ToString();

                    //Open the pdf file
                    try
                    {
                        var fileName = SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseTemplatePath + "\\" + "lostticket.pdf";

                        if (GlobalDataAccessor.Instance.CurrentSiteId.State.Equals(States.Ohio))
                        {
                            fileName = SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseTemplatePath + "\\" + "lostticketOH.pdf";
                        }

                        PDFITextSharpUtilities.PdfSharpTools pdfTools;//=new PawnUtilities.ISharp.PDFITextSharpUtilities.PdfSharpTools(fileName);
                        PDFITextSharpUtilities.OpenPDFFile(fileName, out pdfTools);

                        //Generate output file name
                        string timeStamp = DateTime.Now.ToString("yyMMddHHmmssf");
                        string genDocsDir = SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseLogPath + "\\";
                        string outputFileName = genDocsDir + "LostTicketStatement" + "-" + timeStamp + ".pdf";
                        if (pdfTools == null)
                        {
                            if (FileLogger.Instance.IsLogError)
                            {
                                FileLogger.Instance.logMessage(LogLevel.ERROR, "LostTicketStatementPrint", "Could not get PDF tools instance for lost ticket statement");
                            }
                            return;
                        }
                        pdfTools.PrepForStamping(outputFileName);
                        Dictionary<string, string> lostTicketData = new Dictionary<string, string>();
                        lostTicketData.Add("TransactionDate", transactionDate);
                        lostTicketData.Add("CustomerName", customerName);
                        lostTicketData.Add("CustomerMiddleInitial", custMiddleInitial);
                        lostTicketData.Add("CustomerAddrLine1", custAddr);
                        lostTicketData.Add("CustomerAddrLine2", custAddrLine2);
                        lostTicketData.Add("CustomerIdData", idData);
                        lostTicketData.Add("DateMade", loanDate);
                        lostTicketData.Add("DateDue", loanDueDate);
                        lostTicketData.Add("TicketNumber", tktNo.ToString());
                        lostTicketData.Add("VerifyTicketNumber", tktNo.ToString());
                        lostTicketData.Add("CustomerGender", currentCust.Gender);
                        lostTicketData.Add("CustomerRace", currentCust.Race);
                        lostTicketData.Add("CustomerDOB", currentCust.DateOfBirth.FormatDate());
                        lostTicketData.Add("CustomerHeight", currentCust.Height);
                        if (currentCust.Weight > 0)
                        {
                            lostTicketData.Add("CustomerWeight", currentCust.Weight.ToString() + " lbs");
                        }
                        lostTicketData.Add("CustomerHairColor", currentCust.HairColor);
                        lostTicketData.Add("CustomerEyeColor", currentCust.EyeColor);
                        lostTicketData.Add("LostType", Commons.GetLostTicketType(pawnLoan.LostTicketInfo.LSDTicket));
                        lostTicketData.Add("AmtFinanced", String.Format("{0:C}", pawnLoan.Amount));
                        lostTicketData.Add("AmtFinanceCharges", String.Format("{0:C}", pawnLoan.InterestAmount));
                        lostTicketData.Add("AmtLateCharges", String.Format("{0:C}", lateCharges));
                        lostTicketData.Add("AmtRefunds", String.Format("{0:C}", refundAmt));
                        lostTicketData.Add("AmtTotalPayments", String.Format("{0:C}", totalPayments));
                        lostTicketData.Add("APR", apr);
                        lostTicketData.Add("LostTicketFee", String.Format("{0:0.00}", lostTktFee));

                        if (GlobalDataAccessor.Instance.CurrentSiteId.State.Equals(States.Ohio))
                        {
                            lostTicketData.Add("CurPrinAmount", pawnLoan.CurrentPrincipalAmount.ToString("c"));
                        }

                        //Add the data to the pdf file
                        if (!(PDFITextSharpUtilities.StampSimplePDFWithFormFields(pdfTools, outputFileName, false, lostTicketData)))
                        {
                            if (FileLogger.Instance.IsLogError)
                            {
                                FileLogger.Instance.logMessage(LogLevel.ERROR, "ProcessTenderController", "Could not stamp PDF document for wipe drive");
                            }
                        }
                        //Get the printer details for the form
                        string storeNumber = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;

                        //03/18/2010 GJL - Change for pawn sec to allow development to continue and to facilitate 
                        //real machine names in the pawn sec database instead of 47-byte GUID values
                        //Printing lostticket.pdf
                        const string formName = "lostticket.pdf";
                        if (SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled &&
                            GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.IsValid)
                        {
                            if (FileLogger.Instance.IsLogInfo)
                            {
                                FileLogger.Instance.logMessage(LogLevel.INFO, this, "Printing lost ticket statement on: {0}",
                                                               GlobalDataAccessor.Instance.DesktopSession.LaserPrinter);
                            }
                            if (!(PDFITextSharpUtilities.PrintOutputPDFFile(
                                GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.IPAddress,
                                GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.Port.ToString(), 1, pdfTools)))
                            {
                                if (FileLogger.Instance.IsLogError)
                                {
                                    FileLogger.Instance.logMessage(LogLevel.ERROR, "LostTicketStatement",
                                                                   "Could not print lost ticket statement PDF file");
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        BasicExceptionHandler.Instance.AddException("Error in printing lost ticket statement", new ApplicationException(ex.Message));
                        return;
                    }
                }
            }
        }
    }
}
