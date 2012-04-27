using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using Common.Controllers.Database;
using Common.Libraries.Forms.Components;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Application;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Shared;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Objects;
using Common.Libraries.Utility.ISharp;
using Common.Libraries.Utility.Logger;
using Common.Controllers.Security;
using Reports;

namespace Pawn.Forms.Pawn.Services.PFI
{
    public partial class PFI_Mailers : Form
    {
        private static string cashlinxDirectory = new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;
        private string pfiMailerTemplateOneTwo = SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseTemplatePath + @"\\ps3877_1_2.pdf";
        private string pfiMailerTemplateThree = SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseTemplatePath + @"\\ps3877_3.pdf";
        private string pfiMailerTemplateFour = SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseTemplatePath + @"\\ps3877_4.pdf";

        public PFI_Mailers()
        {
            InitializeComponent();

            this.dateReprintMailers.SelectedDate = ShopDateTime.Instance.ShopDate.ToShortDateString();
            this.dateReprintMailbook.SelectedDate = ShopDateTime.Instance.ShopDate.ToShortDateString();
            this.dateTicketRange.SelectedDate = ShopDateTime.Instance.ShopDate.ToShortDateString();
            this.lblPFIMailerDate.Text = ShopDateTime.Instance.ShopDate.ToShortDateString();

            this.rdoReprintMailers.Checked = false;
            this.rdoReprintMailersForTickets.Checked = false;
            this.rdoReprintMailbook.Checked = false;
            this.rdoPrintPFIMailers.Checked = true;

            this.rdoPrintPFIMailers.Focus();
        }

        #region PRIVATE METHODS

        private Boolean DisplayPrintPFIMailersConfirmationBox(Int32 pfiMailerCount, Boolean usingTicketRange)
        {
            var isContinue = false;

            if (pfiMailerCount > 0)
            {
                String displayMessage;
                if (!usingTicketRange)
                {
                    displayMessage = "for this date. ";
                }
                else
                {
                    displayMessage = "for this ticket range. ";
                }

                var pwdChangeConfirm = MessageBox.Show(
                                "There are " + pfiMailerCount + " PFI Mailers to print " + displayMessage + "Do you want to continue?",
                                "PFI Mailer Confirmation Message",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question);

                if (pwdChangeConfirm == DialogResult.Yes)
                {
                    isContinue = true;
                }
            }
            else
            {
                MessageBox.Show("There are no PFI Mailers to be printed for this date.");
            }

            return isContinue;
        }

        private bool IsTicketRangeValid()
        {
            var isTicketRangeValid = false;
            var beginTicket = 0;
            var endTicket = 0;

            if (txtBeginTicket.Text.Trim().Length == 0 && txtEndTicket.Text.Trim().Length != 0)
            {
                txtBeginTicket.Text = txtEndTicket.Text.Trim();
            }
            else if (txtEndTicket.Text.Trim().Length == 0 && txtBeginTicket.Text.Trim().Length != 0)
            {
                txtEndTicket.Text = txtBeginTicket.Text.Trim();
            }

            var validBeginTicket = Int32.TryParse(txtBeginTicket.Text, out beginTicket);
            var validEndTicket = Int32.TryParse(txtEndTicket.Text, out endTicket);

            if (validBeginTicket && validEndTicket)
            {
                if (endTicket < beginTicket)
                {
                    MessageBox.Show("Begin Ticket is greater than the End Ticket. Please adjust the ticket range.");
                }
                else
                {
                    isTicketRangeValid = true;
                }
            }
            else
            {
                MessageBox.Show("Invalid ticket range.");
            }

            return isTicketRangeValid;
        }

        private void PrintPFIMailers(PFIMailerOption pfiMailerOption)
        {
            //Create output variables
            string errorCode;
            string errorText;

            var businessRulesProcedures = new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession);
            var pfiMailerAdjustmentDays = businessRulesProcedures.GetNumberOfDaysToAddForPFIMailer(GlobalDataAccessor.Instance.CurrentSiteId);

            var pfiMailerList = new List<ReportObject.PFIMailer>();
            var pfiMailerFilesToDeleteList = new List<String>();

            var retVal = false;

            switch (pfiMailerOption)
            {
                case PFIMailerOption.PrintPFIMailers:
                    retVal = PawnProcedures.ExecuteGetPFIMailerData(GlobalDataAccessor.Instance.OracleDA,
                        pfiMailerOption,
                        out pfiMailerList,
                        out errorCode,
                        out errorText,
                        pfiMailerAdjustmentDays,
                        pfiDate: ShopDateTime.Instance.ShopDate);
                    break;
                case PFIMailerOption.ReprintMailbook:
                    retVal = PawnProcedures.ExecuteGetPFIMailerData(GlobalDataAccessor.Instance.OracleDA,
                        pfiMailerOption,
                        out pfiMailerList,
                        out errorCode,
                        out errorText,
                        pfiDate: Utilities.GetDateTimeValue(this.dateReprintMailbook.SelectedDate));
                    break;
                case PFIMailerOption.ReprintPFIMailersByDate:
                    retVal = PawnProcedures.ExecuteGetPFIMailerData(GlobalDataAccessor.Instance.OracleDA,
                        pfiMailerOption,
                        out pfiMailerList,
                        out errorCode,
                        out errorText,
                        pfiDate: Utilities.GetDateTimeValue(this.dateReprintMailers.SelectedDate));
                    break;
                case PFIMailerOption.ReprintPFIMailersByTicket:
                    retVal = PawnProcedures.ExecuteGetPFIMailerData(GlobalDataAccessor.Instance.OracleDA,
                        pfiMailerOption,
                        out pfiMailerList,
                        out errorCode,
                        out errorText,
                        startTicketNumber: Utilities.GetIntegerValue(this.txtBeginTicket.Text),
                        endTicketNumber: Utilities.GetIntegerValue(this.txtEndTicket.Text),
                        pfiDate: Utilities.GetDateTimeValue(this.dateTicketRange.SelectedDate));
                    break;
                case PFIMailerOption.GetTodaysPrintedPFIMailerTickets:
                    retVal = PawnProcedures.ExecuteGetPFIMailerData(
                        GlobalDataAccessor.Instance.OracleDA,
                        pfiMailerOption,
                        out pfiMailerList,
                        out errorCode,
                        out errorText,
                        pfiDate: Utilities.GetDateTimeValue(this.dateTicketRange.SelectedDate));
                    break;

                default:
                    System.Diagnostics.Debug.Assert(false, "Unexpected Default case. Forget to add handler?");
                    break;
            }

            if (retVal)
            {
                if (pfiMailerOption == PFIMailerOption.GetTodaysPrintedPFIMailerTickets)
                {
                    if (pfiMailerList.Count > 0)
                    {
                        txtBeginTicket.Text = pfiMailerList[0].ticketNumber.ToString();
                        txtEndTicket.Text = pfiMailerList[pfiMailerList.Count-1].ticketNumber.ToString();
                    }
                }
                else if (pfiMailerOption == PFIMailerOption.ReprintMailbook)
                {
                    PrintMailingBook(pfiMailerFilesToDeleteList, pfiMailerList);
                    MessageBox.Show(@"PFI Mailbook Printing Complete");
                }
                else
                {
                    if (DisplayPrintPFIMailersConfirmationBox(pfiMailerList.Count, PFIMailerOption.ReprintPFIMailersByTicket == pfiMailerOption))
                    {
                        if (pfiMailerList.Count > 0)
                        {
                            var mailFee = businessRulesProcedures.GetPFIMailerFee(GlobalDataAccessor.Instance.CurrentSiteId);

                            var ticketNumberList = new List<int>();
                            var originalPFINoteList = new List<DateTime>();
                            var pfiEligibleDateList = new List<DateTime>();

                            var pfiMailerFileNamesToMerge = new List<String>();
                            var recordCount = 1;
                            foreach (var pfiMailer in pfiMailerList)
                            {
                                var pfiMailerLocal = pfiMailer;

                                if (pfiMailerOption == PFIMailerOption.PrintPFIMailers)
                                    // Spec Change
                                    //pfiMailerOption == PFIMailerOption.ReprintPFIMailersByTicket ||
                                    //pfiMailerOption == PFIMailerOption.ReprintPFIMailersByDate)
                                {
                                    //
                                    pfiMailerLocal.pfiEligibleDate = GetPFIEligibleDate();

                                    //process pfi eligible date adjustment
                                    if (ShopDateTime.Instance.ShopDate > pfiMailerLocal.originalPFINote)
                                    {
                                        // Special Rule PWN_BR-33 Adjustment
                                        pfiMailerLocal.pfiEligibleDate = GetPFIDateAdjustmentRule33(pfiMailerLocal.pfiEligibleDate);
                                    }

                                    //PawnProcedures.ExecuteUpdatePFIMailerData(
                                    //    GlobalDataAccessor.Instance.OracleDA,
                                    //    out errorCode,
                                    //    out errorText,
                                    //    pfiMailerLocal.storeNumber,
                                    //    pfiMailerLocal.ticketNumber,
                                    //    mailFee,
                                    //    pfiMailerLocal.originalPFINote,
                                    //    "Y",
                                    //    pfiEligibleDate);

                                    ticketNumberList.Add(pfiMailerLocal.ticketNumber);
                                    originalPFINoteList.Add(pfiMailerLocal.originalPFINote);
                                    pfiEligibleDateList.Add(pfiMailerLocal.pfiEligibleDate);
                                }

                                var pfiMailerDocument = new PFIMailerDocument(PdfLauncher.Instance);
                                var pfiMailerFileName = "PFIMailer" + recordCount + ".pdf";

                                var reportObject = new ReportObject
                                {
                                    ReportTempFileFullName =
                                        Path.Combine(
                                            SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseLogPath,
                                            pfiMailerFileName)
                                };

                                pfiMailerDocument.ReportObject = reportObject;

                                pfiMailerDocument.CreateReport(pfiMailerLocal);

                                if (FileLogger.Instance.IsLogInfo)
                                {
                                    FileLogger.Instance.logMessage(
                                        LogLevel.INFO, "PFI_Mailers", "Creating PFI Mailer {0}, Ticket Number {1}",
                                        pfiMailerFileName, pfiMailer.ticketNumber);
                                }

                                //var strReturnMessage =
                                //    PrintingUtilities.printDocument(
                                //        reportObject.ReportTempFileFullName,
                                //        GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.IPAddress,
                                //        GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.Port, 1);
                                //
                                //if (!strReturnMessage.Contains("SUCCESS"))
                                //{
                                //    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "PFI Mailer : " + strReturnMessage);
                                //}

                                pfiMailerFileNamesToMerge.Add(Path.Combine(SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseLogPath, pfiMailerFileName));
                                recordCount++;

                            } // foreach (var pfiMailer in pfiMailerList)

                            // Merge PFIMailerX.pdf doc's
                            var mergedOutputFile = Path.Combine(SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseLogPath, "PFIMailer.pdf");
                            var wasPDFMergeSuccessful = PDFITextSharpUtilities.MergePDFFiles(mergedOutputFile, pfiMailerFileNamesToMerge.ToArray());

                            if (wasPDFMergeSuccessful)
                            {
                                var strReturnMessage =
                                    PrintingUtilities.printDocument(
                                        mergedOutputFile,
                                        GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.IPAddress,
                                        GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.Port, 1);

                                if (FileLogger.Instance.IsLogInfo)
                                {
                                    FileLogger.Instance.logMessage(
                                        LogLevel.INFO, "PFI_Mailers", "Printing PFI Mailer {0}",
                                        mergedOutputFile);
                                }

                                if (!strReturnMessage.Contains("SUCCESS"))
                                {
                                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "PFI Mailer : " + strReturnMessage);
                                }
                            }

                            if (pfiMailerOption == PFIMailerOption.PrintPFIMailers)
                            {
                                PawnProcedures.ExecuteAddPFIMailerDataArray(
                                    GlobalDataAccessor.Instance.OracleDA,
                                    out errorCode,
                                    out errorText,
                                    int.Parse(GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreNumber),
                                    ticketNumberList,
                                    originalPFINoteList,
                                    pfiEligibleDateList,
                                    mailFee);
                            }

                            // Spec Change
                            //else if (pfiMailerOption == PFIMailerOption.ReprintPFIMailersByDate ||
                            //         pfiMailerOption == PFIMailerOption.ReprintPFIMailersByTicket)
                            //{
                            //    PawnProcedures.ExecuteAddPFIMailerDataArray(
                            //        GlobalDataAccessor.Instance.OracleDA,
                            //        out errorCode,
                            //        out errorText,
                            //        int.Parse(GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreNumber),
                            //        ticketNumberList,
                            //        originalPFINoteList,
                            //        pfiEligibleDateList,
                            //        mailFee);
                            //}

                            // Spec Change
                            //if (pfiMailerOption != PFIMailerOption.ReprintPFIMailersByTicket)
                            {
                                PrintMailingBook(pfiMailerFilesToDeleteList, pfiMailerList);
                            }
                        }
                    }
                }
            }
            else
            {
                if (pfiMailerOption == PFIMailerOption.ReprintMailbook)
                {
                    MessageBox.Show(@"There is no Mail Book for this date.");
                }
                //else if (pfiMailerOption == PFIMailerOption.GetTodaysPrintedPFIMailerTickets)
                //{
                //    MessageBox.Show(@"There are no PFI Mailers to be printed for this ticket range.");
                //}
                else
                {
                    MessageBox.Show(@"There are no PFI Mailers to be printed for this date.");
                }
            }
        }

        private void PrintMailingBook(List<string> pfiMailerFilesToDeleteList, List<ReportObject.PFIMailer> pfiMailerList)
        {
            if (pfiMailerList.Count > 0)
            {
                PDFITextSharpUtilities.PdfSharpTools tools;

                var pageCount = 1;

                if (pfiMailerList.Count > 8)
                {
                    pageCount++;

                    var remainder = 0;

                    pageCount = Math.DivRem(pfiMailerList.Count, 8, out remainder);

                    if (remainder > 0)
                    {
                        pageCount++;
                    }
                }

                pageCount++;

                var rowCount = 1;

                var outputFilePageThree = SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseLogPath + "\\ps3877_output_main_page1_" +
                                         rowCount + ".pdf";
                var outputFilePageFour = SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseLogPath + "\\ps3877_output_last_page_" +
                                         rowCount + ".pdf";
                var mergedOutputFile = SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseLogPath + "\\PFIMailerMailbook_ps3877_output_" +
                                       rowCount + ".pdf";

                var pfiData = new Dictionary<string, string>
                              {
                                  {
                                      "page_x_of_x_1", "(page 1 of " + pageCount + ")"
                                  },
                                  {
                                      "#_mailers_printed", pfiMailerList.Count.ToString()
                                  },
                                  {
                                      "shop_name_shop_no", pfiMailerList[0].storeName
                                  },
                                  {
                                      "shop_address", pfiMailerList[0].storeAddress
                                  },
                                  {
                                      "shop_city_state_zip",
                                      pfiMailerList[0].storeCity + ", " + pfiMailerList[0].storeState + " " + pfiMailerList[0].storeZipCode
                                  }
                              };

                var pfiOutputFileList = new List<string>
                                        {
                                            this.pfiMailerTemplateOneTwo
                                        };

                for (var index = 0; index < pfiMailerList.Count; index++)
                {
                    pfiData.Add("row" + rowCount + "_num", (index + 1).ToString());
                    pfiData.Add("row" + rowCount + "_address1", pfiMailerList[index].customerName);
                    pfiData.Add("row" + rowCount + "_address2", pfiMailerList[index].customerAddress);
                    pfiData.Add(
                        "row" + rowCount + "_address3",
                        pfiMailerList[index].customerCity + ", " + pfiMailerList[index].customerState + " " + pfiMailerList[index].customerZipCode);

                    if (index == (pfiMailerList.Count - 1))
                    {
                        PDFITextSharpUtilities.OpenPDFFile(this.pfiMailerTemplateThree, out tools);
                        PDFITextSharpUtilities.StampSimplePDFWithFormFields(tools, outputFilePageThree, true, pfiData);

                        pfiOutputFileList.Add(outputFilePageThree);
                        pfiMailerFilesToDeleteList.Add(outputFilePageThree);

                        break;
                    }
                    else if (rowCount % 8 == 0)
                    {
                        var pageNumber = GetPageNumberByRowNumber(index + 1).ToString();

                        PDFITextSharpUtilities.OpenPDFFile(this.pfiMailerTemplateThree, out tools);
                        PDFITextSharpUtilities.StampSimplePDFWithFormFields(tools, outputFilePageThree, true, pfiData);

                        pfiOutputFileList.Add(outputFilePageThree);
                        pfiMailerFilesToDeleteList.Add(outputFilePageThree);

                        outputFilePageThree = SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseLogPath + "\\ps3877_output_main_page" +
                                              pageNumber + ".pdf";

                        pfiData = new Dictionary<string, string>
                                  {
                                      {
                                          "page_x_of_x_1", "(page " + pageNumber + " of " + pageCount + ")"
                                      },
                                      {
                                          "#_mailers_printed", pfiMailerList.Count.ToString()
                                      },
                                      {
                                          "shop_name_shop_no", pfiMailerList[index].storeNumber.ToString()
                                      },
                                      {
                                          "shop_address", pfiMailerList[index].storeAddress
                                      },
                                      {
                                          "shop_city_state_zip",
                                          pfiMailerList[index].storeCity + ", " + pfiMailerList[index].storeState + " " + pfiMailerList[index].storeZipCode
                                      }
                                  };

                        rowCount = 1;
                    }
                    else
                    {
                        rowCount++;
                    }
                }

                pfiData.Add("page_x_of_x_2", "(page " + pageCount + " of " + pageCount + ")");

                PDFITextSharpUtilities.OpenPDFFile(this.pfiMailerTemplateFour, out tools);
                PDFITextSharpUtilities.StampSimplePDFWithFormFields(tools, outputFilePageFour, true, pfiData);

                pfiOutputFileList.Add(outputFilePageFour);
                pfiMailerFilesToDeleteList.Add(outputFilePageFour);

                var wasPDFMergeSuccessful = PDFITextSharpUtilities.MergePDFFiles(mergedOutputFile, pfiOutputFileList.ToArray());

                if (wasPDFMergeSuccessful)
                {
                    var strReturnMessage =
                        PrintingUtilities.printDocument(
                            mergedOutputFile,
                            GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.IPAddress,
                            GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.Port, 1);

                    if (!strReturnMessage.Contains("SUCCESS"))
                    {
                        FileLogger.Instance.logMessage(LogLevel.ERROR, this, "PFI Mailer : " + strReturnMessage);
                    }
                }
                else
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "PFI Mailer Merge Failed: " + mergedOutputFile);
                }
            }
        }
        #endregion

        private DateTime GetPFIEligibleDate()
        {
            var businessRulesProcedures = new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession);

            var pfiEligibleDate = ShopDateTime.Instance.ShopDate.AddDays(businessRulesProcedures.GetPFIEligibleAdjustmentTriggerDays(GlobalDataAccessor.Instance.CurrentSiteId));

            return pfiEligibleDate;
        }

        private DateTime GetPFIDateAdjustmentRule33(DateTime pfiEligibleDate)
        {
            var businessRulesProcedures = new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession);

            var underwritePawnLoanUtility = new UnderwritePawnLoanUtility(GlobalDataAccessor.Instance.DesktopSession);

            if (underwritePawnLoanUtility.IsShopClosed(pfiEligibleDate))
            {
                var adjustmentDirection = businessRulesProcedures.GetPFIEligibleDateAdjustmentDirection(GlobalDataAccessor.Instance.CurrentSiteId);

                for (var i = 1; i < 15; i++)
                {
                    switch (adjustmentDirection)
                    {
                        case "F":
                            pfiEligibleDate = pfiEligibleDate.AddDays(1);
                            break;
                        case "B":
                            pfiEligibleDate = pfiEligibleDate.AddDays(-1);
                            break;
                    }

                    if (!underwritePawnLoanUtility.IsShopClosed(pfiEligibleDate))
                    {
                        return pfiEligibleDate;
                    }
                }
            }
            return pfiEligibleDate;
        }

        private int GetPageNumberByRowNumber(int rowNumber)
        {
            rowNumber++;

            var remainder = 0;
            var pageNumber = Math.DivRem(rowNumber, 8, out remainder);

            if (pageNumber == 0)
                pageNumber++;

            if (remainder > 0)
                pageNumber++;

            return pageNumber;
        }

        private void rdoPrintPFIMailers_CheckedChanged(object sender, EventArgs e)
        {
            toggleRadioButtons();
        }

        private void rdoReprintMailers_CheckedChanged(object sender, EventArgs e)
        {
            toggleRadioButtons();
        }

        private void rdoReprintMailersForTickets_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdoReprintMailersForTickets.Checked)
            {
                PrintPFIMailers(PFIMailerOption.GetTodaysPrintedPFIMailerTickets);
            }

            toggleRadioButtons();
        }

        private void rdoReprintMailbook_CheckedChanged(object sender, EventArgs e)
        {
            toggleRadioButtons();
        }

        private void toggleRadioButtons()
        {
            if (this.rdoPrintPFIMailers.Checked)
            {
                this.rdoPrintPFIMailers.Checked = true;
                this.rdoReprintMailers.Checked = false;
                this.rdoReprintMailersForTickets.Checked = false;
                this.rdoReprintMailbook.Checked = false;
            }

            if (this.rdoReprintMailers.Checked)
            {
                this.rdoPrintPFIMailers.Checked = false;
                this.rdoReprintMailers.Checked = true;
                this.rdoReprintMailersForTickets.Checked = false;
                this.rdoReprintMailbook.Checked = false;
            }

            if (this.rdoReprintMailersForTickets.Checked)
            {
                this.rdoPrintPFIMailers.Checked = false;
                this.rdoReprintMailers.Checked = false;
                this.rdoReprintMailersForTickets.Checked = true;
                this.rdoReprintMailbook.Checked = false;
            }

            if (this.rdoReprintMailbook.Checked)
            {
                this.rdoPrintPFIMailers.Checked = false;
                this.rdoReprintMailers.Checked = false;
                this.rdoReprintMailersForTickets.Checked = false;
                this.rdoReprintMailbook.Checked = true;
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            if (this.rdoPrintPFIMailers.Checked)
            {
                PrintPFIMailers(PFIMailerOption.PrintPFIMailers);
            }

            if (this.rdoReprintMailers.Checked)
            {
                PrintPFIMailers(PFIMailerOption.ReprintPFIMailersByDate);
            }

            if (this.rdoReprintMailersForTickets.Checked)
            {
                if (IsTicketRangeValid())
                    PrintPFIMailers(PFIMailerOption.ReprintPFIMailersByTicket);
            }

            if (this.rdoReprintMailbook.Checked)
            {
                PrintPFIMailers(PFIMailerOption.ReprintMailbook);
            }
        }

        private void dateReprintMailers_Enter(object sender, EventArgs e)
        {
            rdoReprintMailers.Checked = true;
            toggleRadioButtons();
        }

        private void txtBeginTicket_Enter(object sender, EventArgs e)
        {
            rdoReprintMailersForTickets.Checked = true;
            toggleRadioButtons();
        }

        private void dateReprintMailbook_Enter(object sender, EventArgs e)
        {
            rdoReprintMailbook.Checked = true;
            toggleRadioButtons();
        }

        private void dateTicketRange_Enter(object sender, EventArgs e)
        {
            txtBeginTicket_Enter(sender, e);
        }

        private void dateTicketRange_SelectedDateChanged(object sender, EventArgs e)
        {
            PrintPFIMailers(PFIMailerOption.GetTodaysPrintedPFIMailerTickets);
            txtBeginTicket_Enter(sender, e);
        }
    }
}
