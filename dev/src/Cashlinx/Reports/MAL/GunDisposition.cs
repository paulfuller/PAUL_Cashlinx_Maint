using System;
using System.Data;
using Common.Libraries.Utility.String;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Reports.MAL
{
    class GunDispositionRpt : AbstractGroup
    { 
        //private decimal _TotalAmount;
        //private int _CountNonVoided;

        public GunDispositionRpt(ReportContext context, string dataTableName)
            : base(context, dataTableName)
        {
        }

        protected override void OnBuildSection ()
        {
            try
            {
                if (GroupData.Rows.Count < 1)
                    return;
            
                // Header row
                HeaderCells.Add(GetCell("#", DataFontBold, Element.ALIGN_JUSTIFIED));
                HeaderCells.Add(GetCell("Transaction No.", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Transaction Type", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Gun No.", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Manufacturer", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Importer", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Model", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Serial No.", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Gun Type", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Caliber", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Seizure No.", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("# of 4473 Forms", DataFontBold, Element.ALIGN_CENTER));

                PdfPTable dataTable = new PdfPTable(HeaderCells.Count);

                dataTable.SetWidths(new[] { 6f, 20f, 20f, 10f, 20f, 20f, 20f, 20f, 20f, 20f, 20f, 20f });
                AddGroupTitle(dataTable, "Gun Disposition Report");
                AddHeaderCells(dataTable);
                dataTable.HeaderRows = 2;


                PrintGroupDetail(dataTable);
                /*
                //dataTable.AddCell(GetCell("", DataFontBold, Element.ALIGN_JUSTIFIED, HeaderCells.Count));
                dataTable.AddCell(GetCell("Customer Name:", DataFontBold, Element.ALIGN_RIGHT, 2));
                dataTable.AddCell(GetCell(lastCust.name, DataFontBold, Element.ALIGN_LEFT, HeaderCells.Count - 2));

                //dataTable.AddCell(GetCell("", DataFontBold, Element.ALIGN_JUSTIFIED));
                dataTable.AddCell(GetCell("Address:", DataFontBold, Element.ALIGN_RIGHT, 2));
                dataTable.AddCell(GetCell(lastCust.addr, DataFontBold, Element.ALIGN_LEFT, HeaderCells.Count - 2));

                //dataTable.AddCell(GetCell("", DataFontBold, Element.ALIGN_JUSTIFIED));
                dataTable.AddCell(GetCell("ID:", DataFontBold, Element.ALIGN_RIGHT, 2));
                dataTable.AddCell(GetCell(lastCust.id, DataFontBold, Element.ALIGN_LEFT, HeaderCells.Count - 2));

                dataTable.AddCell(GetCell(PawnUtilities.String.StringUtilities.fillString("=", 200), DataFont, Element.ALIGN_CENTER, HeaderCells.Count));
                dataTable.AddCell(GetCell("", DataFontBold, Element.ALIGN_JUSTIFIED, HeaderCells.Count));
                */

                dataTable.AddCell(GetCell(string.Format("{0} ATF 4473 Form(s)", nr_forms), DataFont, Element.ALIGN_LEFT, HeaderCells.Count));
                dataTable.AddCell(GetCell("", DataFontBold, Element.ALIGN_JUSTIFIED, HeaderCells.Count));
                dataTable.AddCell(GetCell("", DataFontBold, Element.ALIGN_JUSTIFIED, HeaderCells.Count));

                dataTable.AddCell(GetCell(@"This is a list of all outgoing gun transactions for one day.  Compare this list to your 4473 forms. 
Check for accuracy. Verify name, address and dates.  You should have one 4473 form for each transaction.", DataFont, Element.ALIGN_CENTER, HeaderCells.Count));
                AddGroupContentsToPdfTable(dataTable);


            }
            catch (Exception ex)
            {
                ErrorText = ex.Message;
                ErrorCode = "1";
                throw;
            }            
        }

        private int group_nr = 1;
        private int nr_forms = 0;
        private string lastCustNr;

        private struct cust_info
        {
            public string name;
            public string addr;
            public string id;
        }

        private cust_info lastCust;

        //protected override void PrintGroupDetailRecord(PdfPTable pdfTable, int index, DataView data, bool shadeRow)
        protected override void PrintGroupDetailRecord(PdfPTable pdfTable, DataView data)
        {
            Font font = DataFont;
            string currentTransactionDate = string.Empty;
            string lastTransactionDate = string.Empty;
            string lastCustomerNumber = string.Empty;
            string currentCustomerNumber = string.Empty;
            int rowcount = 0;
            for (int i = 0; i < data.Table.Rows.Count; i++)
            {
                rowcount++;
                DataRow dataRow = data.Table.Rows[i];
                //currentTransactionDate = GetStringValue(dataRow["TRANS_DT"]);
                //currentCustomerNumber = GetStringValue(dataRow["CUST_NR"]);
                currentTransactionDate = data[i]["TRANS_DT"].ToString();
                currentCustomerNumber = data[i]["CUST_NR"].ToString();
                if (currentTransactionDate != lastTransactionDate)
                {
                    lastCustomerNumber = string.Empty;
                }

                if (lastCustomerNumber == currentCustomerNumber)
                {
                    pdfTable.AddCell(GetCell("", font, Element.ALIGN_JUSTIFIED));
                    pdfTable.AddCell(GetCell(data[i]["TRANS_NR"].ToString(), font, Element.ALIGN_JUSTIFIED));
                    pdfTable.AddCell(GetCell(data[i]["TRANS_TYPE"].ToString(), font, Element.ALIGN_LEFT));
                    pdfTable.AddCell(GetCell(data[i]["GUN_NR"].ToString(), font, Element.ALIGN_LEFT));

                    pdfTable.AddCell(GetCell(data[i]["Manufacturer"].ToString(), font, Element.ALIGN_LEFT));
                    pdfTable.AddCell(GetCell(data[i]["Importer"].ToString(), font, Element.ALIGN_LEFT));
                    pdfTable.AddCell(GetCell(data[i]["Model"].ToString(), font, Element.ALIGN_LEFT));
                    pdfTable.AddCell(GetCell(data[i]["Serial_number"].ToString(), font, Element.ALIGN_LEFT));
                    pdfTable.AddCell(GetCell(data[i]["GUN_TYPE"].ToString(), font, Element.ALIGN_LEFT));
                    pdfTable.AddCell(GetCell(data[i]["Caliber"].ToString(), font, Element.ALIGN_LEFT));

                    pdfTable.AddCell(GetCell(data[i]["SEIZURE_TICKET"].ToString(), font, Element.ALIGN_LEFT));
                    pdfTable.AddCell(GetCell("", font, Element.ALIGN_LEFT));

                    lastCust.name = data[i]["CUST_NAME"].ToString();
                    lastCust.addr = data[i]["CUST_ADDR"].ToString();
                    lastCust.id = data[i]["CUST_ID"].ToString();

                }
                else
                {
                    ///need to terminate last cust here
                    if (rowcount != 1)
                    {
                        font = DataFontBold;
                        pdfTable.AddCell(GetCell("Customer Name:", font, Element.ALIGN_RIGHT, 2));
                        pdfTable.AddCell(GetCell(lastCust.name, font, Element.ALIGN_LEFT, HeaderCells.Count - 2));

                        pdfTable.AddCell(GetCell("Address:", font, Element.ALIGN_RIGHT, 2));
                        pdfTable.AddCell(GetCell(lastCust.addr, font, Element.ALIGN_LEFT, HeaderCells.Count - 2));

                        pdfTable.AddCell(GetCell("ID:", font, Element.ALIGN_RIGHT, 2));
                        pdfTable.AddCell(GetCell(lastCust.id, font, Element.ALIGN_LEFT, HeaderCells.Count - 2));

                        pdfTable.AddCell(GetCell(StringUtilities.fillString("=", 200), DataFont, Element.ALIGN_CENTER, HeaderCells.Count));
                        pdfTable.AddCell(GetCell("", font, Element.ALIGN_JUSTIFIED, HeaderCells.Count));
                        font = DataFont;
                    }
                    lastCustNr = data[i]["CUST_NR"].ToString();
                    lastCust.name = data[i]["CUST_NAME"].ToString();
                    lastCust.addr = data[i]["CUST_ADDR"].ToString();
                    lastCust.id = data[i]["CUST_ID"].ToString();

                    if (currentTransactionDate != lastTransactionDate)
                    {
                        pdfTable.AddCell(GetCell(currentTransactionDate, DataFontBold, Element.ALIGN_LEFT, false, 12, 0));
                    }
                    pdfTable.AddCell(GetCell(GetStringValue(group_nr++), font, Element.ALIGN_JUSTIFIED));
                    pdfTable.AddCell(GetCell(data[i]["TRANS_NR"].ToString(), font, Element.ALIGN_JUSTIFIED));
                    pdfTable.AddCell(GetCell(data[i]["TRANS_TYPE"].ToString(), font, Element.ALIGN_LEFT));
                    pdfTable.AddCell(GetCell(data[i]["GUN_NR"].ToString(), font, Element.ALIGN_LEFT));
                    pdfTable.AddCell(GetCell(data[i]["Manufacturer"].ToString(), font, Element.ALIGN_LEFT));
                    pdfTable.AddCell(GetCell(data[i]["Importer"].ToString(), font, Element.ALIGN_LEFT));
                    pdfTable.AddCell(GetCell(data[i]["Model"].ToString(), font, Element.ALIGN_LEFT));
                    pdfTable.AddCell(GetCell(data[i]["Serial_number"].ToString(), font, Element.ALIGN_LEFT));
                    pdfTable.AddCell(GetCell(data[i]["GUN_TYPE"].ToString(), font, Element.ALIGN_LEFT));
                    pdfTable.AddCell(GetCell(data[i]["Caliber"].ToString(), font, Element.ALIGN_LEFT));


                    pdfTable.AddCell(GetCell(data[i]["SEIZURE_TICKET"].ToString(), font, Element.ALIGN_LEFT));
                    pdfTable.AddCell(GetCell(data[i]["NR_FORMS"].ToString(), font, Element.ALIGN_CENTER));
                    this.nr_forms += GetIntValue(data[i]["NR_FORMS"].ToString());
                }

                if (rowcount == data.Table.Rows.Count)
                {
                    font = DataFontBold;
                    pdfTable.AddCell(GetCell("Customer Name:", font, Element.ALIGN_RIGHT, 2));
                    pdfTable.AddCell(GetCell(lastCust.name, font, Element.ALIGN_LEFT, HeaderCells.Count - 2));

                    pdfTable.AddCell(GetCell("Address:", font, Element.ALIGN_RIGHT, 2));
                    pdfTable.AddCell(GetCell(lastCust.addr, font, Element.ALIGN_LEFT, HeaderCells.Count - 2));

                    pdfTable.AddCell(GetCell("ID:", font, Element.ALIGN_RIGHT, 2));
                    pdfTable.AddCell(GetCell(lastCust.id, font, Element.ALIGN_LEFT, HeaderCells.Count - 2));

                    pdfTable.AddCell(GetCell(StringUtilities.fillString("=", 200), DataFont, Element.ALIGN_CENTER, HeaderCells.Count));
                    pdfTable.AddCell(GetCell("", font, Element.ALIGN_JUSTIFIED, HeaderCells.Count));
                    font = DataFont;

                    lastCustNr = GetStringValue(dataRow["CUST_NR"]);
                    lastCust.name = GetStringValue(dataRow["CUST_NAME"]);
                    lastCust.addr = GetStringValue(dataRow["CUST_ADDR"]);
                    lastCust.id = GetStringValue(dataRow["CUST_ID"]);
                }
                /*
                if (string.IsNullOrEmpty(lastCustNr))
                {
                    lastCustNr = GetStringValue(dataRow["CUST_NR"]);
                    lastCust = new cust_info();

                    lastCust.name = GetStringValue(dataRow["CUST_NAME"]);
                    lastCust.addr = GetStringValue(dataRow["CUST_ADDR"]);
                    lastCust.id = GetStringValue(dataRow["CUST_ID"]);

                    pdfTable.AddCell(GetCell(GetStringValue(group_nr++), font, Element.ALIGN_JUSTIFIED));
                    pdfTable.AddCell(GetCell(GetStringValue(dataRow["TRANS_NR"]), font, Element.ALIGN_JUSTIFIED));
                    pdfTable.AddCell(GetCell(GetStringValue(dataRow["TRANS_TYPE"]), font, Element.ALIGN_LEFT));
                    pdfTable.AddCell(GetCell(GetStringValue(dataRow["GUN_NR"]), font, Element.ALIGN_LEFT));
                    pdfTable.AddCell(GetCell(GetStringValue(dataRow["Manufacturer"]), font, Element.ALIGN_LEFT));
                    pdfTable.AddCell(GetCell(GetStringValue(dataRow["Importer"]), font, Element.ALIGN_LEFT));
                    pdfTable.AddCell(GetCell(GetStringValue(dataRow["Model"]), font, Element.ALIGN_LEFT));
                    pdfTable.AddCell(GetCell(GetStringValue(dataRow["Serial_number"]), font, Element.ALIGN_LEFT));
                    pdfTable.AddCell(GetCell(GetStringValue(dataRow["GUN_TYPE"]), font, Element.ALIGN_LEFT));
                    pdfTable.AddCell(GetCell(GetStringValue(dataRow["Caliber"]), font, Element.ALIGN_LEFT));


                    pdfTable.AddCell(GetCell(GetStringValue(dataRow["SEIZURE_TICKET"]), font, Element.ALIGN_LEFT));
                    pdfTable.AddCell(GetCell(GetStringValue(dataRow["NR_FORMS"]), font, Element.ALIGN_CENTER));
                    this.nr_forms += GetIntValue(dataRow["NR_FORMS"]);
                }
                else if (!GetStringValue(dataRow["CUST_NR"]).Equals(lastCustNr))
                {
                    font = DataFontBold;
                    pdfTable.AddCell(GetCell("Customer Name:", font, Element.ALIGN_RIGHT, 2));
                    pdfTable.AddCell(GetCell(lastCust.name, font, Element.ALIGN_LEFT, HeaderCells.Count - 2));

                    pdfTable.AddCell(GetCell("Address:", font, Element.ALIGN_RIGHT, 2));
                    pdfTable.AddCell(GetCell(lastCust.addr, font, Element.ALIGN_LEFT, HeaderCells.Count - 2));

                    pdfTable.AddCell(GetCell("ID:", font, Element.ALIGN_RIGHT, 2));
                    pdfTable.AddCell(GetCell(lastCust.id, font, Element.ALIGN_LEFT, HeaderCells.Count - 2));

                    pdfTable.AddCell(GetCell(PawnUtilities.String.StringUtilities.fillString("=", 200), DataFont, Element.ALIGN_CENTER, HeaderCells.Count));
                    pdfTable.AddCell(GetCell("", font, Element.ALIGN_JUSTIFIED, HeaderCells.Count));
                    font = DataFont;

                    lastCustNr = GetStringValue(dataRow["CUST_NR"]);
                    lastCust.name = GetStringValue(dataRow["CUST_NAME"]);
                    lastCust.addr = GetStringValue(dataRow["CUST_ADDR"]);
                    lastCust.id = GetStringValue(dataRow["CUST_ID"]);

                    pdfTable.AddCell(GetCell(GetStringValue(group_nr++), font, Element.ALIGN_JUSTIFIED));
                    pdfTable.AddCell(GetCell(GetStringValue(dataRow["TRANS_NR"]), font, Element.ALIGN_JUSTIFIED));
                    pdfTable.AddCell(GetCell(GetStringValue(dataRow["TRANS_TYPE"]), font, Element.ALIGN_LEFT));
                    pdfTable.AddCell(GetCell(GetStringValue(dataRow["GUN_NR"]), font, Element.ALIGN_LEFT));


                    pdfTable.AddCell(GetCell(GetStringValue(dataRow["Manufacturer"]), font, Element.ALIGN_LEFT));
                    pdfTable.AddCell(GetCell(GetStringValue(dataRow["Importer"]), font, Element.ALIGN_LEFT));
                    pdfTable.AddCell(GetCell(GetStringValue(dataRow["Model"]), font, Element.ALIGN_LEFT));
                    pdfTable.AddCell(GetCell(GetStringValue(dataRow["Serial_number"]), font, Element.ALIGN_LEFT));
                    pdfTable.AddCell(GetCell(GetStringValue(dataRow["GUN_TYPE"]), font, Element.ALIGN_LEFT));
                    pdfTable.AddCell(GetCell(GetStringValue(dataRow["Caliber"]), font, Element.ALIGN_LEFT));


                    pdfTable.AddCell(GetCell(GetStringValue(dataRow["SEIZURE_TICKET"]), font, Element.ALIGN_LEFT));
                    pdfTable.AddCell(GetCell(GetStringValue(dataRow["NR_FORMS"]), font, Element.ALIGN_CENTER));
                    this.nr_forms += GetIntValue(dataRow["NR_FORMS"]);
                }

                else
                {
                    pdfTable.AddCell(GetCell("", font, Element.ALIGN_JUSTIFIED));
                    pdfTable.AddCell(GetCell(GetStringValue(dataRow["TRANS_NR"]), font, Element.ALIGN_JUSTIFIED));
                    pdfTable.AddCell(GetCell(GetStringValue(dataRow["TRANS_TYPE"]), font, Element.ALIGN_LEFT));
                    pdfTable.AddCell(GetCell(GetStringValue(dataRow["GUN_NR"]), font, Element.ALIGN_LEFT));

                    pdfTable.AddCell(GetCell(GetStringValue(dataRow["Manufacturer"]), font, Element.ALIGN_LEFT));
                    pdfTable.AddCell(GetCell(GetStringValue(dataRow["Importer"]), font, Element.ALIGN_LEFT));
                    pdfTable.AddCell(GetCell(GetStringValue(dataRow["Model"]), font, Element.ALIGN_LEFT));
                    pdfTable.AddCell(GetCell(GetStringValue(dataRow["Serial_number"]), font, Element.ALIGN_LEFT));
                    pdfTable.AddCell(GetCell(GetStringValue(dataRow["GUN_TYPE"]), font, Element.ALIGN_LEFT));
                    pdfTable.AddCell(GetCell(GetStringValue(dataRow["Caliber"]), font, Element.ALIGN_LEFT));

                    pdfTable.AddCell(GetCell(GetStringValue(dataRow["SEIZURE_TICKET"]), font, Element.ALIGN_LEFT));
                    pdfTable.AddCell(GetCell("", font, Element.ALIGN_LEFT));
                }
                */
                lastTransactionDate = currentTransactionDate;
                lastCustomerNumber = currentCustomerNumber;

                /*
                pdfTable.AddCell(GetCell("Customer Name:", DataFontBold, Element.ALIGN_RIGHT, 2));
                pdfTable.AddCell(GetCell(lastCust.name, DataFontBold, Element.ALIGN_LEFT, HeaderCells.Count - 2));

                //dataTable.AddCell(GetCell("", DataFontBold, Element.ALIGN_JUSTIFIED));
                pdfTable.AddCell(GetCell("Address:", DataFontBold, Element.ALIGN_RIGHT, 2));
                pdfTable.AddCell(GetCell(lastCust.addr, DataFontBold, Element.ALIGN_LEFT, HeaderCells.Count - 2));

                //dataTable.AddCell(GetCell("", DataFontBold, Element.ALIGN_JUSTIFIED));
                pdfTable.AddCell(GetCell("ID:", DataFontBold, Element.ALIGN_RIGHT, 2));
                pdfTable.AddCell(GetCell(lastCust.id, DataFontBold, Element.ALIGN_LEFT, HeaderCells.Count - 2));

                pdfTable.AddCell(GetCell(PawnUtilities.String.StringUtilities.fillString("=", 200), DataFont, Element.ALIGN_CENTER, HeaderCells.Count));
                pdfTable.AddCell(GetCell("", DataFontBold, Element.ALIGN_JUSTIFIED, HeaderCells.Count));
                 */
            }
        }

    }
}
