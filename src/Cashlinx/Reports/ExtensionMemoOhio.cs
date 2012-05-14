using System;
using System.IO;
using System.Linq;
using Common.Controllers.Application;
using Common.Controllers.Security;
using Common.Libraries.Objects;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Document = iTextSharp.text.Document;

namespace Reports
{
    public class ExtensionMemoOhio : AbstractExtensionMemo
    {
        //Private variables
        private Font rptFont;
        private Font rptFontBold;
        private decimal totDailyAmt = 0.0M;
        private decimal totAmtPaidToday = 0.0M;
        private Document document;
        private int numOfLoans = 0;
        private decimal totalAmtDueAtMaturity = 0;

        public ExtensionMemoOhio(IPdfLauncher pdfLauncher)
            : base(pdfLauncher)
        {

        }

        public override bool Print()
        {
            return ExtensionMemoData.All(PrintMemo);
        }

        private bool PrintMemo(ExtensionMemoInfo extnInfo)

        {
            try
            {
                //Set Font for the report
                rptFont = FontFactory.GetFont("Arial", 8, Font.NORMAL);
                rptFontBold = FontFactory.GetFont("Arial", 8, Font.BOLD);

               var rptFileName = RptObject.ReportTempFileFullName.Replace(".pdf", "_" + extnInfo.TicketNumber + ".pdf");
               if (!string.IsNullOrEmpty(rptFileName))
                {
                    document = new Document(PageSize.HALFLETTER.Rotate());
                    document.AddTitle(RptObject.ReportTitle);
                    const int mainTableColumns = 6;
                    var writer = PdfWriter.GetInstance(document, new FileStream(rptFileName, FileMode.Create));
                    var mainTable = new PdfPTable(mainTableColumns)
                                    {
                                        WidthPercentage = 100
                                    };

                    //Insert the report header
                    var headerTable = new PdfPTable(5);
                    BuildReportHeaderTable(headerTable, extnInfo);
                    mainTable.AddCell(new PdfPCell(headerTable) { Colspan = mainTableColumns, Border = Rectangle.NO_BORDER });
                    mainTable.AddCell(new PdfPCell(new Phrase()) { Border = Rectangle.NO_BORDER, Colspan = mainTableColumns }); // empty row

                    //Set number of header rows
                    mainTable.HeaderRows = 0;

                    document.Open();

                    var topDataTable = new PdfPTable(5)
                                       {
                                           WidthPercentage = 100
                                       };
                    topDataTable.AddCell(new PdfPCell(new Paragraph("Previous Maturity Date :", rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT });
                    topDataTable.AddCell(new PdfPCell(new Paragraph(extnInfo.DueDate.FormatDate(), rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT });
                    topDataTable.AddCell(new PdfPCell(new Phrase()) { Border = Rectangle.NO_BORDER });
                    topDataTable.AddCell(new PdfPCell(new Paragraph("New Maturity Date :", rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });
                    topDataTable.AddCell(new PdfPCell(new Paragraph(extnInfo.NewDueDate.FormatDate(), rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });

                    topDataTable.AddCell(new PdfPCell(new Paragraph("Previous Default Date :", rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT });
                    topDataTable.AddCell(new PdfPCell(new Paragraph(extnInfo.OldPfiEligible.FormatDate(), rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT });
                    topDataTable.AddCell(new PdfPCell(new Phrase()) { Border = Rectangle.NO_BORDER });
                    topDataTable.AddCell(new PdfPCell(new Paragraph("Last Day of Grace :", rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });
                    topDataTable.AddCell(new PdfPCell(new Paragraph(extnInfo.NewPfiEligible.FormatDate(), rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });

                    topDataTable.AddCell(new PdfPCell(new Paragraph("Pawn Charge Paid Today :", rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT });
                    topDataTable.AddCell(new PdfPCell(new Paragraph(extnInfo.ExtensionAmount.ToString("c"), rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT });
                    topDataTable.AddCell(new PdfPCell(new Phrase()) { Border = Rectangle.NO_BORDER });
                    topDataTable.AddCell(new PdfPCell(new Paragraph("Pawn Charge Paid to :", rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });
                    topDataTable.AddCell(new PdfPCell(new Paragraph(extnInfo.DueDate.FormatDate(), rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });

                    mainTable.AddCell(new PdfPCell(topDataTable) { Colspan = mainTableColumns, Border = Rectangle.NO_BORDER });
                    mainTable.AddCell(new PdfPCell(new Phrase()) { Border = Rectangle.NO_BORDER, Colspan = mainTableColumns, FixedHeight = 10 }); // empty row

                    var bottomDataTable = new PdfPTable(5)
                                          {
                                              WidthPercentage = 100
                                          };
                    bottomDataTable.AddCell(new PdfPCell(new Paragraph("AMOUNTS DUE AT MATURITY DATE", rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 2 });
                    bottomDataTable.AddCell(new PdfPCell(new Phrase()) { Border = Rectangle.NO_BORDER });
                    bottomDataTable.AddCell(new PdfPCell(new Paragraph("AMOUNTS DUE ON LAST DAY TO REDEEM", rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT, Colspan = 2 });

                    bottomDataTable.AddCell(new PdfPCell(new Paragraph("Amount Financed :", rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT });
                    bottomDataTable.AddCell(new PdfPCell(new Paragraph(extnInfo.Amount.ToString("c"), rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT });
                    bottomDataTable.AddCell(new PdfPCell(new Phrase()) { Border = Rectangle.NO_BORDER });
                    bottomDataTable.AddCell(new PdfPCell(new Paragraph("Amount Financed :", rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });
                    bottomDataTable.AddCell(new PdfPCell(new Paragraph(extnInfo.Amount.ToString("c"), rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });

                    if (GlobalDataAccessor.Instance.CurrentSiteId.State.Equals(States.Ohio))
                    {

                        bottomDataTable.AddCell(new PdfPCell(new Paragraph("Pawn Charge :", rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT });
                        bottomDataTable.AddCell(new PdfPCell(new Paragraph((extnInfo.InterestAmount + extnInfo.Fees).ToString("c"), rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT });
                        bottomDataTable.AddCell(new PdfPCell(new Phrase()) { Border = Rectangle.NO_BORDER });
                        bottomDataTable.AddCell(new PdfPCell(new Paragraph("Pawn Charge :", rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });
                        bottomDataTable.AddCell(new PdfPCell(new Paragraph(((extnInfo.InterestAmount + extnInfo.Fees) * extnInfo.ExtendedBy).ToString("c"), rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });

                        bottomDataTable.AddCell(new PdfPCell(new Paragraph("**TOTAL** :", rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT });
                        bottomDataTable.AddCell(new PdfPCell(new Paragraph((extnInfo.Amount + extnInfo.InterestAmount + extnInfo.Fees).ToString("c"), rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT });
                        bottomDataTable.AddCell(new PdfPCell(new Phrase()) { Border = Rectangle.NO_BORDER });
                        bottomDataTable.AddCell(new PdfPCell(new Paragraph("**TOTAL** :", rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });
                        bottomDataTable.AddCell(new PdfPCell(new Paragraph((extnInfo.Amount + ((extnInfo.InterestAmount + extnInfo.Fees) * extnInfo.ExtendedBy)).ToString("c"), rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });

                        mainTable.AddCell(new PdfPCell(bottomDataTable) { Colspan = mainTableColumns, Border = Rectangle.NO_BORDER });
                        mainTable.AddCell(new PdfPCell(new Phrase()) { Border = Rectangle.NO_BORDER, Colspan = mainTableColumns }); // empty row
                    }
                    else
                    {
                        //Indiana

                        bottomDataTable.AddCell(new PdfPCell(new Paragraph("Pawn Charge :", rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT });
                        bottomDataTable.AddCell(new PdfPCell(new Paragraph((extnInfo.InterestAmount + extnInfo.Fees).ToString("c"), rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT });
                        bottomDataTable.AddCell(new PdfPCell(new Phrase()) { Border = Rectangle.NO_BORDER });
                        bottomDataTable.AddCell(new PdfPCell(new Paragraph("Pawn Charge :", rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });
                        bottomDataTable.AddCell(new PdfPCell(new Paragraph(((extnInfo.InterestAmount + extnInfo.Fees) * extnInfo.ExtendedBy).ToString("c"), rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });

                        bottomDataTable.AddCell(new PdfPCell(new Paragraph("**TOTAL** :", rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT });
                        bottomDataTable.AddCell(new PdfPCell(new Paragraph((extnInfo.Amount + extnInfo.InterestAmount + extnInfo.Fees).ToString("c"), rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT });
                        bottomDataTable.AddCell(new PdfPCell(new Phrase()) { Border = Rectangle.NO_BORDER });
                        bottomDataTable.AddCell(new PdfPCell(new Paragraph("**TOTAL** :", rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });
                        bottomDataTable.AddCell(new PdfPCell(new Paragraph((extnInfo.Amount + ((extnInfo.InterestAmount + extnInfo.Fees) * extnInfo.ExtendedBy)).ToString("c"), rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });

                        mainTable.AddCell(new PdfPCell(bottomDataTable) { Colspan = mainTableColumns, Border = Rectangle.NO_BORDER });
                        mainTable.AddCell(new PdfPCell(new Phrase()) { Border = Rectangle.NO_BORDER, Colspan = mainTableColumns }); // empty row
                    }

                    if(GlobalDataAccessor.Instance.CurrentSiteId.State.Equals(States.Ohio))
                    {
                    mainTable.AddCell(new PdfPCell(new Phrase()) { Border = Rectangle.NO_BORDER, Colspan = mainTableColumns });
                    mainTable.AddCell(new PdfPCell(new Paragraph("IF THE PLEDGOR FAILS TO REDEEM OR EXTEND THE LOAN BEFORE THE", rptFontBold)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = mainTableColumns });
                    mainTable.AddCell(new PdfPCell(new Paragraph("CLOSE OF BUSINESS ON THE LAST DAY OF GRACE, THE LICENSEE BECOMES THE OWNER", rptFontBold)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = mainTableColumns });
                    mainTable.AddCell(new PdfPCell(new Paragraph("OF THE PLEDGED PROPERTY.", rptFontBold)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = mainTableColumns });
                    mainTable.AddCell(new PdfPCell(new Phrase()) { Border = Rectangle.NO_BORDER, Colspan = mainTableColumns }); // empty row
                    }
                    else
                    {
                    //Indiana  

                    mainTable.AddCell(new PdfPCell(new Paragraph("Daily Rate of Pawn Charge Accrual If Redeemed After Maturity:", rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT,Colspan = 3});
                    mainTable.AddCell(new PdfPCell(new Paragraph((extnInfo.DailyAmount).ToString("c"),rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT });
                        
                    mainTable.AddCell(new PdfPCell(new Phrase()) { Border = Rectangle.NO_BORDER, Colspan = mainTableColumns }); // empty row
                    
                        
                    mainTable.AddCell(new PdfPCell(new Phrase()) { Border = Rectangle.NO_BORDER, Colspan = mainTableColumns });
                    mainTable.AddCell(new PdfPCell(new Paragraph("UNREDEEMED PLEDGED GOODS MUST BE HELD BY PAWNBROKER FOR SIXTY (60 DAYS", rptFontBold)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = mainTableColumns });
                    mainTable.AddCell(new PdfPCell(new Paragraph("(NOT NECESSARILY TWO (2) MONTHS FROM NEW MATURITY DATE.", rptFontBold)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = mainTableColumns });
                    mainTable.AddCell(new PdfPCell(new Phrase()) { Border = Rectangle.NO_BORDER, Colspan = mainTableColumns }); // empty row
                    }

                    if (GlobalDataAccessor.Instance.CurrentSiteId.State.Equals(States.Ohio))
                    {
                     mainTable.AddCell(new PdfPCell(new Paragraph("Last Day Of Grace :", rptFontBold)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT, Colspan = mainTableColumns / 2 });
                     mainTable.AddCell(new PdfPCell(new Paragraph(extnInfo.NewPfiEligible.FormatDate(), rptFontBold)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = mainTableColumns / 2 });
                     mainTable.AddCell(new PdfPCell(new Phrase()) { Border = Rectangle.NO_BORDER, Colspan = mainTableColumns, FixedHeight = 20 }); // empty row
                    }
                    mainTable.AddCell(new PdfPCell(new Phrase()) { Border = Rectangle.NO_BORDER, Colspan = mainTableColumns });
                    mainTable.AddCell(new PdfPCell(new Paragraph("Pledger :", rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT });
                    mainTable.AddCell(new PdfPCell(new Paragraph(extnInfo.CustomerName, rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT });
                    mainTable.AddCell(new PdfPCell(new Paragraph(new Phrase())) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT });
                    mainTable.AddCell(new PdfPCell(new Paragraph("_________________________________________", rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 3 });

                    mainTable.AddCell(new PdfPCell(new Paragraph(new Phrase())) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 3 });
                    mainTable.AddCell(new PdfPCell(new Paragraph("Pledger", rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 3 });

                    mainTable.AddCell(new PdfPCell(new Phrase()) { Border = Rectangle.NO_BORDER, Colspan = mainTableColumns, FixedHeight = 20 }); // empty row
                    mainTable.AddCell(new PdfPCell(new Paragraph("PAWNBROKER:", rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = mainTableColumns });
                    mainTable.AddCell(new PdfPCell(new Paragraph("Prepare in duplicate; original to Pledgor; second copy for store file.", rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = mainTableColumns });

                    document.Add(mainTable);
                    document.Close();

                    AddDocument(extnInfo, rptFileName);
                    return true;
                }

                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Extension memo printing failed since file name is not set");
                return false;
            }
            catch (Exception de)
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Extension memo printing" + de.Message);
                return false;
            }
        }

        private void BuildReportHeaderTable(PdfPTable headingtable, ExtensionMemoInfo extnInfo)
        {
            if (GlobalDataAccessor.Instance.CurrentSiteId.State.Equals(States.Ohio))
            {
                var dt = ShopDateTime.Instance.FullShopDateTime;

                // first row of header
                var cell = new PdfPCell(new Paragraph("Date:", rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT, NoWrap = true };
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Paragraph(ShopDateTime.Instance.ShopDate.ToString("MM/dd/yyyy"), rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT, NoWrap = true };
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Paragraph("MEMORANDUM OF EXTENSION", rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_CENTER, NoWrap = true };
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Paragraph("Ticket No:", rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT, NoWrap = true };
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Paragraph(extnInfo.TicketNumber.ToString(), rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT, NoWrap = true };
                headingtable.AddCell(cell);

                //second row of header
                cell = new PdfPCell(new Paragraph("Time:", rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT, NoWrap = true };
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Paragraph(dt.ToString("t"), rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT, NoWrap = true };
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Paragraph(RptObject.ReportStore, rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_CENTER, NoWrap = true };
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Paragraph("Emp No:", rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT, NoWrap = true };
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Paragraph(Employee, rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT, NoWrap = true };
                headingtable.AddCell(cell);

                //third row of header
                cell = new PdfPCell(new Paragraph(string.Empty)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 2 };
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Paragraph(RptObject.ReportStoreDesc, rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_CENTER, NoWrap = true };
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Paragraph(string.Empty)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 2 };
                headingtable.AddCell(cell);

                //empty line
                cell = new PdfPCell(new Phrase(string.Empty)) { Colspan = 5, Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };
                headingtable.AddCell(cell);
            }

            else
            {
                var hgt = 1.0f;

               var dt = ShopDateTime.Instance.FullShopDateTime;

                // first row of header

                var cell = new PdfPCell(new Phrase("MEMORANDUM OF EXTENSION", rptFontBold)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_CENTER, NoWrap = true, Colspan = 5 };
                headingtable.AddCell(cell);


                cell = new PdfPCell(new Phrase(string.Empty)) { Colspan = 5, Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_CENTER };
                headingtable.AddCell(cell);

                headingtable.AddCell(new PdfPCell(new Paragraph(" ", rptFont)) { Border = Rectangle.TOP_BORDER,BorderColorTop = BaseColor.GRAY,BorderWidthTop = hgt , HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 5 });
               

                //second
                 cell = new PdfPCell(new Paragraph("Date:", rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT, NoWrap = true};
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Paragraph(ShopDateTime.Instance.ShopDate.ToString("MM/dd/yyyy"), rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT};
                headingtable.AddCell(cell);

                
                cell = new PdfPCell(new Paragraph(RptObject.ReportStore, rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_CENTER, NoWrap = true };
                headingtable.AddCell(cell);


                cell = new PdfPCell(new Paragraph("Ticket No:", rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT, NoWrap = true };
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Paragraph(extnInfo.TicketNumber.ToString(), rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT, NoWrap = true };
                headingtable.AddCell(cell);

                //third row of header
                cell = new PdfPCell(new Paragraph("Time:", rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT, NoWrap = true};
                headingtable.AddCell(cell);

                
                cell = new PdfPCell(new Paragraph(dt.ToString("t"), rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Paragraph(RptObject.ReportStoreDesc, rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_CENTER, NoWrap = true };
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Paragraph("Emp No:", rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT, NoWrap = true };
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Paragraph(Employee, rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT, NoWrap = true };
                headingtable.AddCell(cell);

                
                cell = new PdfPCell(new Paragraph(string.Empty)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 2 };
                headingtable.AddCell(cell);

               
                //empty line
                cell = new PdfPCell(new Phrase(string.Empty)) { Colspan = 5, Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };
                headingtable.AddCell(cell);
                
            }
            
        }
    }
}
