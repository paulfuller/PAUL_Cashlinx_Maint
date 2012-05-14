using System;
using System.Collections.Generic;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Objects.Pawn;
using Common.Libraries.Utility.RawTextPrinting;

namespace Reports
{
    public class IndianaPoliceCardReport
    {
        private const int LeftColumnWidth = 45;
        private const string HorizontalSeparatorValue = "| ";
        private const string VerticalSeparatorValue = "-";
        private const string IntersectingSeparatorValue = "+";
        private const int RightColumnWidth = 32;

        public RawTextDocument Document
        {
            get;
            private set;
        }

        public IndianaPoliceCardReport()
        {
            Document = new RawTextDocument();
        }

// ReSharper disable UnusedMethodReturnValue.Global
        public bool Print(string ipAddress, uint port)
// ReSharper restore UnusedMethodReturnValue.Global
        {
            return Document.Print(ipAddress, port);
        }

// ReSharper disable UnusedMember.Global
        public bool Save(string fileName)
// ReSharper restore UnusedMember.Global
        {
            return Document.Save(fileName);
        }

        public void BuildDocument(string ticketNumber/*PawnLoan pawnLoan*/, List<Common.Libraries.Objects.Business.Item> listItems, CustomerVO customerInfo, string storeName, AddressVO selectedCustomerAddress, KeyValuePair<string, string> lastIdUsed, DateTime statusDateTime, Boolean isLoan)
        {
            var pageNumber = 1;

            // Emulation specific control codes to force the police card to fit on 3x5.
            Document.AddPrinterCode(PrinterCode.LineDensity8Dpi);
            Document.AddPrinterCode(PrinterCode.HorizontalSpacing171Cpi);

            foreach (var item in listItems)
            {
                var mdls = new MerchandiseDescriptionLineSeparator(8, RightColumnWidth);
                var descRows = mdls.SplitIntoRows(item.TicketDescription);

                // -------------------------------------------------------------
                var row = Document.CreateNewRow();
                row.WriteText("NAME OF CUSTOMER", LeftColumnWidth);
                row.WriteText(HorizontalSeparatorValue);
                row.WriteText("SERIAL NUMBER", RightColumnWidth);

                // -------------------------------------------------------------
                row = Document.CreateNewRow();

                row.WriteText(customerInfo.LastName + ", " + customerInfo.FirstName + " " + customerInfo.MiddleInitial, 
                    LeftColumnWidth, RawTextFlags.ForceUpper);

                row.WriteText(HorizontalSeparatorValue);
                var serialNumber = string.Empty;
                if (item.QuickInformation.SerialNumber != null)
                {
                    serialNumber = item.QuickInformation.SerialNumber;
                }
                row.WriteText(serialNumber, RightColumnWidth);

                // -------------------------------------------------------------
                row = Document.CreateNewRow();
                row.WriteRepeatingText(VerticalSeparatorValue, 45);
                row.WriteText(IntersectingSeparatorValue);
                row.WriteRepeatingText(VerticalSeparatorValue, 28);

                // -------------------------------------------------------------
                row = Document.CreateNewRow();
                row.WriteText("CUSTOMER SIGNATURE", LeftColumnWidth);
                row.WriteText(HorizontalSeparatorValue);
                row.WriteText("ITEM", RightColumnWidth);

                // -------------------------------------------------------------
                row = Document.CreateNewRow();
                row.WriteText(string.Empty, LeftColumnWidth);
                row.WriteText(HorizontalSeparatorValue);
                row.WriteText(item.CategoryDescription, RightColumnWidth);

                // -------------------------------------------------------------
                row = Document.CreateNewRow();
                row.WriteRepeatingText(VerticalSeparatorValue, 45);
                row.WriteText(IntersectingSeparatorValue);
                row.WriteRepeatingText(VerticalSeparatorValue, 28);

                // -------------------------------------------------------------
                row = Document.CreateNewRow();
                row.WriteText("ADDRESS", LeftColumnWidth);
                row.WriteText(HorizontalSeparatorValue);
                row.WriteText("DESIGN / MODEL / MAKE / CAL /", RightColumnWidth);

                // -------------------------------------------------------------
                row = Document.CreateNewRow();
                row.WriteText(selectedCustomerAddress.Address1 + " " + selectedCustomerAddress.Address2 + " " + selectedCustomerAddress.UnitNum, LeftColumnWidth);
                row.WriteText(HorizontalSeparatorValue);
                row.WriteText("         SIZE / DESC.", RightColumnWidth);

                // -------------------------------------------------------------
                row = Document.CreateNewRow();
                row.WriteRepeatingText(VerticalSeparatorValue, 27);
                row.WriteText(IntersectingSeparatorValue);
                row.WriteRepeatingText(VerticalSeparatorValue, 10);
                row.WriteText(IntersectingSeparatorValue);
                row.WriteRepeatingText(VerticalSeparatorValue, 6);
                row.WriteText(IntersectingSeparatorValue);

                row = Document.CreateNewRow();
                row.WriteText("CITY - STATE - ZIP", 27);
                row.WriteText(HorizontalSeparatorValue);
                row.WriteText("DOB", 9);
                row.WriteText(HorizontalSeparatorValue);
                row.WriteText("EYES", 5);
                row.WriteText(HorizontalSeparatorValue);
                row.WriteText(descRows.Length >= 1 ? descRows[0].Trim() : string.Empty, RightColumnWidth);

                // -------------------------------------------------------------
                row = Document.CreateNewRow();
                var city = selectedCustomerAddress.City;
                if (city.Length > 17)
                {
                    city = city.Substring(0, 17);
                }
                row.WriteText(city + ", " + selectedCustomerAddress.State_Code + selectedCustomerAddress.ZipCode, 27);
                
                row.WriteText(HorizontalSeparatorValue);
                row.WriteText(customerInfo.DateOfBirth.ToString("MM/dd/yy"), 9);
                row.WriteText(HorizontalSeparatorValue);
                row.WriteText(customerInfo.EyeColor, 5);
                row.WriteText(HorizontalSeparatorValue);
                row.WriteText(descRows.Length >= 2 ? descRows[1].Trim() : string.Empty, RightColumnWidth);

                // -------------------------------------------------------------
                row = Document.CreateNewRow();
                row.WriteRepeatingText(VerticalSeparatorValue, 6);
                row.WriteText(IntersectingSeparatorValue);
                row.WriteRepeatingText(VerticalSeparatorValue, 7);
                row.WriteText(IntersectingSeparatorValue);
                row.WriteRepeatingText(VerticalSeparatorValue, 12);
                row.WriteText(IntersectingSeparatorValue);
                row.WriteRepeatingText(VerticalSeparatorValue, 7);
                row.WriteText(IntersectingSeparatorValue);
                row.WriteRepeatingText(VerticalSeparatorValue, 2);
                row.WriteText(IntersectingSeparatorValue);
                row.WriteRepeatingText(VerticalSeparatorValue, 6);
                row.WriteText(IntersectingSeparatorValue);
                row.WriteText(" ");
                //MDSE Desc Line 3
                row.WriteText(descRows.Length >= 3 ? descRows[2].Trim() : string.Empty, RightColumnWidth);

                // -------------------------------------------------------------
                row = Document.CreateNewRow();
                row.WriteText("SEX", 6);
                row.WriteText(HorizontalSeparatorValue);
                row.WriteText("HEIGHT", 6);
                row.WriteText(HorizontalSeparatorValue);
                row.WriteText("WEIGHT", 11);
                row.WriteText(HorizontalSeparatorValue);
                row.WriteText("HAIR", 6);
                row.WriteText(HorizontalSeparatorValue);
                row.WriteText("ORIGIN", 8);
                row.WriteText(HorizontalSeparatorValue);
                //MDSE Desc Line 4
                row.WriteText(descRows.Length >= 4 ? descRows[3].Trim() : string.Empty, RightColumnWidth);

                // -------------------------------------------------------------
                row = Document.CreateNewRow();
                row.WriteText(customerInfo.Gender, 6);
                row.WriteText(HorizontalSeparatorValue);
                row.WriteText(customerInfo.Height, 6);
                row.WriteText(HorizontalSeparatorValue);
                row.WriteText(customerInfo.Weight.ToString(), 11, RawTextFlags.Right);
                row.WriteText(HorizontalSeparatorValue);
                row.WriteText(customerInfo.HairColor, 6);
                row.WriteText(HorizontalSeparatorValue);
                row.WriteText(customerInfo.Race, 8);
                row.WriteText(HorizontalSeparatorValue);
                //MDSE Desc Line 5
                row.WriteText(descRows.Length >= 5 ? descRows[4].Trim() : string.Empty, RightColumnWidth);

                // -------------------------------------------------------------
                row = Document.CreateNewRow();
                row.WriteText("------");
                row.WriteText(IntersectingSeparatorValue);
                row.WriteText("-------");
                row.WriteText(IntersectingSeparatorValue);
                row.WriteText("------------");
                row.WriteText(IntersectingSeparatorValue);
                row.WriteText("-------");
                row.WriteText(IntersectingSeparatorValue);
                row.WriteText("---------");
                row.WriteText(IntersectingSeparatorValue);
                row.WriteText(" ");
                //MDSE Desc Line 6
                row.WriteText(descRows.Length >= 6 ? descRows[5].Trim() : string.Empty, RightColumnWidth);

                // -------------------------------------------------------------
                row = Document.CreateNewRow();
                row.WriteText("IDENTIFICATION TYPE DR LIC OR VALID PHOTO ID", LeftColumnWidth);
                row.WriteText(HorizontalSeparatorValue);
                //MDSE Desc Line 7
                row.WriteText(descRows.Length >= 7 ? descRows[6].Trim() : string.Empty, RightColumnWidth);

                // -------------------------------------------------------------
                row = Document.CreateNewRow();
                //row.WriteText("DL-NV-2000407563", LeftColumnWidth);

                var id = customerInfo.getIdByTypeandIssuer(lastIdUsed.Key, lastIdUsed.Value);
                var idLine = string.Empty;
                if (id != null)
                {
                    idLine = id.IdType + "-" + id.IdIssuerCode + "-" + id.IdValue;
                    if (idLine.Length > LeftColumnWidth)
                    {
                        idLine.Substring(0, LeftColumnWidth - 1);
                    }
                }
                row.WriteText(idLine, LeftColumnWidth);

                row.WriteText(HorizontalSeparatorValue);
                //MDSE Desc Line 8
                row.WriteText(descRows.Length >= 8 ? descRows[7].Trim() : string.Empty, RightColumnWidth);

                // -------------------------------------------------------------
                row = Document.CreateNewRow();
                row.WriteText("-------");
                row.WriteText(IntersectingSeparatorValue);
                row.WriteText("-------");
                row.WriteText(IntersectingSeparatorValue);
                row.WriteText("------");
                row.WriteText(IntersectingSeparatorValue);
                row.WriteText("------");
                row.WriteText(IntersectingSeparatorValue);
                row.WriteText("------------");
                row.WriteText(IntersectingSeparatorValue);
                row.WriteText("--");
                row.WriteText(IntersectingSeparatorValue);
                row.WriteText("--------------");
                row.WriteText(IntersectingSeparatorValue);
                row.WriteText("--------------");

                // -------------------------------------------------------------
                row = Document.CreateNewRow();
                row.WriteText("CONS", 7);
                row.WriteText(HorizontalSeparatorValue);
                row.WriteText("TRADE", 6);
                row.WriteText(HorizontalSeparatorValue);
                row.WriteText("PURC", 5);
                row.WriteText(HorizontalSeparatorValue);
                row.WriteText("PAWN", 5);
                row.WriteText(HorizontalSeparatorValue);
                row.WriteText("TICKET NO.", 11);
                row.WriteText(HorizontalSeparatorValue);
                row.WriteText("DATE & TIME", 16);
                row.WriteText(HorizontalSeparatorValue);
                row.WriteText("LOAN AMOUNT", 11);

                // -------------------------------------------------------------
                row = Document.CreateNewRow();
                row.WriteText(string.Empty, 7);
                row.WriteText(HorizontalSeparatorValue);
                row.WriteText(string.Empty, 6);
                row.WriteText(HorizontalSeparatorValue);
                if (!isLoan)
                {
                    row.WriteText("X", 5);
                }
                else
                {
                    row.WriteText(string.Empty, 5);
                }
                row.WriteText(HorizontalSeparatorValue);
                if (isLoan)
                {
                    row.WriteText("X", 5);
                }
                else
                {
                    row.WriteText(string.Empty, 5);
                }

                row.WriteText(HorizontalSeparatorValue);
                row.WriteText(ticketNumber, 11);
                row.WriteText(HorizontalSeparatorValue);
                row.WriteText(statusDateTime.ToString("MM/dd/yy HH:mm"), 16);
                row.WriteText(HorizontalSeparatorValue);
                row.WriteText(String.Format("{0:0.00}", item.ItemAmount), 11);

                // -------------------------------------------------------------
                row = Document.CreateNewRow();
                row.WriteText("-------");
                row.WriteText(IntersectingSeparatorValue);
                row.WriteText("-------");
                row.WriteText(IntersectingSeparatorValue);
                row.WriteText("------");
                row.WriteText(IntersectingSeparatorValue);
                row.WriteText("------");
                row.WriteText(IntersectingSeparatorValue);
                row.WriteText("------------");
                row.WriteText(IntersectingSeparatorValue);
                row.WriteText("-----------------");
                row.WriteText(IntersectingSeparatorValue);
                row.WriteText("--------------");

                // -------------------------------------------------------------
                row = Document.CreateNewRow();
                row.WriteText(storeName, 45);
                row.WriteText("PAGE " + pageNumber + " OF " + listItems.Count, 30, RawTextFlags.Right);

                // -------------------------------------------------------------
                for (var blankRowCount = 0; blankRowCount < 2; blankRowCount++) //25
                {
                    row = Document.CreateNewRow();
                    row.WriteText(string.Empty);
                }

                /*
                for (var blankRowCount = 0; blankRowCount < 5; blankRowCount++)
                {
                    row = Document.CreateNewRow();
                    row.WriteText(PrinterCode.FormFeed.ToString() + " ");
                }
                row = Document.CreateNewRow();
                row.WriteText("ED1");
                row = Document.CreateNewRow();
                row.WriteText("123456789012345678901234567890123456789012345678901234567890123456789012345678\f");
                row = Document.CreateNewRow();
                row.WriteText("ED2");
                row = Document.CreateNewRow();
                row.WriteText("                                                                              \f");
                row = Document.CreateNewRow();
                row.WriteText("ED3");
                row = Document.CreateNewRow();
                row.WriteText("/f");
                row = Document.CreateNewRow();
                row.WriteText("ED4");
                row = Document.CreateNewRow();
                row.WriteText("<FF>");
                row = Document.CreateNewRow();
                row.WriteText("ED5");
                row = Document.CreateNewRow();
                row.WriteText('\xC'.ToString());
                row = Document.CreateNewRow();
                row.WriteText("ED6");
                row = Document.CreateNewRow();
                row.WriteText('\f'.ToString());
                row = Document.CreateNewRow();
                row.WriteText("ED7");
                row = Document.CreateNewRow();
                var pc = new PrinterCode("<CSI>");
                row.WriteText(pc.NonMtplSequence+"\f");
                row = Document.CreateNewRow();
                row.WriteText("ED8");
                row = Document.CreateNewRow();
                row.WriteText(pc.MtplSequence + "\f");
                row = Document.CreateNewRow();
                row.WriteText("ED9");

                row.WriteText(pc.NonMtplSequence + '\xC'.ToString());
                row = Document.CreateNewRow();
                row.WriteText("ED10");
                row = Document.CreateNewRow();
                row.WriteText(pc.MtplSequence + '\xC'.ToString());
                row = Document.CreateNewRow();
                row.WriteText("ED11");
                */
                // -------------------------------------------------------------
                pageNumber++;
            }
        }
    }
}
