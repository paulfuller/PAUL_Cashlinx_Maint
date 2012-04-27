using System;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Reports.DSTR
{
    public class Group40_Retail : AbstractDSTRGroup
    {
        private int count;
        private decimal total;
        private string _title;
        private decimal total_cost;
        private decimal total_tax;
        private decimal total_fees;
        private decimal grandTotal;
        private bool isLayaway = false;

        public Group40_Retail(DSTRReportContext dstrContext, string dataTableName, string title)
            : base(dstrContext, dataTableName)
        {
            _title = title;
            if (_title.Equals("Layaway Payments"))
                isLayaway = true;
        }

        protected override void OnBuildSection()
        {
            try
            {
                
                if (GroupData.Rows.Count < 1)
                    return;

                count = 0;
                total = 0;
                total_tax = 0;
                total_fees = 0;
                grandTotal = 0;

                
                // Header row
                HeaderCells.Clear();
                HeaderCells.Add(GetCell("CSR #", DataFontBold, Element.ALIGN_LEFT));
                if (!isLayaway)
                {
                    HeaderCells.Add(GetCell("MSR #", DataFontBold, Element.ALIGN_LEFT));
                }
                if (!isLayaway && _title.Equals ("Retail Sale Refunds"))
                    HeaderCells.Add(GetCell("Refund #", DataFontBold, Element.ALIGN_LEFT));
                else
                    HeaderCells.Add(GetCell("Layaway #", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("\nItem Description", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Tender Type", DataFontBold, Element.ALIGN_LEFT)); 
                HeaderCells.Add(GetCell("Customer \nor Vendor Name", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Time", DataFontBold, Element.ALIGN_LEFT));
                if (!isLayaway)
                {
                    HeaderCells.Add(GetCell("\nItem\nCost", DataFontBold, Element.ALIGN_RIGHT));
                    HeaderCells.Add(GetCell("Sale Amount", DataFontBold, Element.ALIGN_RIGHT));
                    HeaderCells.Add(GetCell("Sales Tax", DataFontBold, Element.ALIGN_RIGHT));
                    HeaderCells.Add(GetCell("Other Fees", DataFontBold, Element.ALIGN_RIGHT));
                    if (_title.Equals("Retail Sale Refunds"))
                        HeaderCells.Add(GetCell("Refund\nAmount", DataFontBold, Element.ALIGN_RIGHT));  
                    else
                        HeaderCells.Add(GetCell("Retail\nAmount", DataFontBold, Element.ALIGN_RIGHT));  
                }
                else
                {
                    HeaderCells.Add(GetCell("", DataFontBold, Element.ALIGN_RIGHT));
                    HeaderCells.Add(GetCell("Service Fee", DataFontBold, Element.ALIGN_RIGHT));
                    HeaderCells.Add(GetCell("Layaway Amount", DataFontBold, Element.ALIGN_RIGHT));
                }

                PdfPTable dataTable = new PdfPTable(HeaderCells.Count);

                if (dataTable.NumberOfColumns == 12)
                    dataTable.SetWidths(new[] { 6f, 7f, 9f, 27f, 20f, 23f, 7f, 7f, 7f, 7f, 7f, 7f });
                else
                    dataTable.SetWidths(new[] { 6f, 9f, 27f, 20f, 23f, 7f, 7f, 7f, 7f });

                AddGroupTitle(dataTable, _title);
                AddHeaderCells(dataTable);    
              
                dataTable.HeaderRows = 2;

                PrintGroupDetail(dataTable);

                
                dataTable.AddCell(GetCell("Total", DataFont, Element.ALIGN_JUSTIFIED, 1, Rectangle.TOP_BORDER));
                dataTable.AddCell(GetCell(count.ToString(), DataFont, Element.ALIGN_LEFT, 1, Rectangle.TOP_BORDER));

                if (dataTable.NumberOfColumns == 12)
                {

                    dataTable.AddCell(GetCell("", DataFont, Element.ALIGN_RIGHT, 5, Rectangle.TOP_BORDER));
                    dataTable.AddCell(GetCell(GetCurrencyValue(total_cost), DataFont, Element.ALIGN_RIGHT, 1, Rectangle.TOP_BORDER));
                    dataTable.AddCell(GetCell(GetCurrencyValue(total), DataFont, Element.ALIGN_RIGHT, 1, Rectangle.TOP_BORDER));
                    dataTable.AddCell(GetCell(GetCurrencyValue(total_tax), DataFont, Element.ALIGN_RIGHT, 1, Rectangle.TOP_BORDER));
                    dataTable.AddCell(GetCell(GetCurrencyValue(total_fees), DataFont, Element.ALIGN_RIGHT, 1, Rectangle.TOP_BORDER));
                    dataTable.AddCell(GetCell(GetCurrencyValue(grandTotal), DataFont, Element.ALIGN_RIGHT, 1, Rectangle.TOP_BORDER));
                }
                else
                {
                    dataTable.AddCell(GetCell("", DataFont, Element.ALIGN_RIGHT, 5, Rectangle.TOP_BORDER));
                    dataTable.AddCell(GetCell(GetCurrencyValue(total_fees), DataFont, Element.ALIGN_RIGHT, 1, Rectangle.TOP_BORDER));
                    dataTable.AddCell(GetCell(GetCurrencyValue(total), DataFont, Element.ALIGN_RIGHT, 1, Rectangle.TOP_BORDER));
                }

                AddGroupContentsToPdfTable(dataTable);

            }
            catch (Exception ex)
            {
                ErrorText = ex.Message;
                ErrorCode = "1";
                throw;
            }
        }



        protected override void PrintGroupDetailRecord(PdfPTable pdfTable, int index, DataRow dataRow, bool shadeRow)
        {
            decimal amount = GetDecimalValue(dataRow["AMOUNT"]); // this now contains the coupon total for a sale
            decimal tax = GetDecimalValue(dataRow["SALES_TAX"]);
            decimal fee = GetDecimalValue(dataRow["FEE_AMOUNT"]);
            decimal retail_amount = GetDecimalValue(dataRow["TOTAL_SALE"]);

            if (!isLayaway && amount != retail_amount + tax + fee)
                amount = retail_amount + tax + fee - amount;

            Font font = DataFont;

            if (GetStringValue(dataRow["STATUS_CD"]).Equals("VO"))
            {
                font = DataFontBold;
                amount *= -1;
                tax *= -1;
                fee *= -1;
                retail_amount *= -1;
                
            }

            else
            {
                count++;
                total += retail_amount;
                total_tax += tax;
                total_fees += fee;
                grandTotal += amount;
            }

          // ROW 1
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["CSR_NR"]), font, Element.ALIGN_JUSTIFIED, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["RECEIPT_NR"]), font, Element.ALIGN_LEFT, shadeRow));
            if (!isLayaway)
                pdfTable.AddCell(GetCell(GetStringValue(dataRow["TICKET_NR"]), font, Element.ALIGN_LEFT, shadeRow));

            pdfTable.AddCell(GetCell("", font, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["TENDER_TYPE"]), font, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["NAME"]), font, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["TIME"]), font, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell("", font, Element.ALIGN_RIGHT, shadeRow));

            if (!isLayaway)
            {
                pdfTable.AddCell(GetCell(GetCurrencyValue(retail_amount), font, Element.ALIGN_RIGHT, shadeRow));
                pdfTable.AddCell(GetCell(GetCurrencyValue(tax), font, Element.ALIGN_RIGHT, shadeRow));
                pdfTable.AddCell(GetCell(GetCurrencyValue(fee), font, Element.ALIGN_RIGHT, shadeRow));
                pdfTable.AddCell(GetCell(GetCurrencyValue(amount), font, Element.ALIGN_RIGHT, shadeRow));
            }
            else
            {
                pdfTable.AddCell(GetCell(GetCurrencyValue(fee), font, Element.ALIGN_RIGHT, shadeRow));
                pdfTable.AddCell(GetCell(GetCurrencyValue(retail_amount), font, Element.ALIGN_RIGHT, shadeRow));
            }

          // ROW 2
            pdfTable.AddCell(GetCell("", font, Element.ALIGN_JUSTIFIED, shadeRow, (isLayaway? 2:3)));
            PdfPCell descCell = GetCell(GetStringValue(dataRow["MD_DESC"]), font, Element.ALIGN_LEFT, shadeRow, 3, false);
            //descCell.NoWrap = false;
            pdfTable.AddCell(descCell);
            
            pdfTable.AddCell(GetCell("", font, Element.ALIGN_RIGHT, shadeRow));
            if (!isLayaway)
            {
                pdfTable.AddCell(GetCell(GetStringValue(dataRow["ITEM_AMOUNT"]), font, Element.ALIGN_RIGHT, shadeRow));
                pdfTable.AddCell(GetCell("", font, Element.ALIGN_RIGHT, shadeRow, 4));
            }
            else
            {
                pdfTable.AddCell(GetCell("", font, Element.ALIGN_RIGHT, shadeRow));
                pdfTable.AddCell(GetCell("", font, Element.ALIGN_RIGHT, shadeRow, 3));
            }
            
        }

        protected override void LoadDataTable(string dataTableName)
        {
            DataTable dtRetail;
            DataTable dtRetailDetl;

            DSTRContext.ReportData.GetTable(dataTableName, out dtRetail);
            DSTRContext.ReportData.GetTable(dataTableName + "_DETL", out dtRetailDetl);
            if (dtRetail != null && dtRetailDetl != null)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("STATUS_CD", typeof(System.String));
                dt.Columns.Add("CSR_NR", typeof(System.String));
                dt.Columns.Add("MD_DESC", typeof (System.String));
                dt.Columns.Add("TICKET_NR", typeof (System.String));
                dt.Columns.Add("RECEIPT_NR", typeof(System.String));
                dt.Columns.Add("TENDER_TYPE", typeof(System.String));
                dt.Columns.Add("NAME", typeof(System.String));
                dt.Columns.Add("TIME", typeof(System.String));
                dt.Columns.Add("ITEM_AMOUNT", typeof(System.String));
                dt.Columns.Add("AMOUNT", typeof(System.String)); //typeof(System.Decimal)
                dt.Columns.Add("SALES_TAX", typeof(System.String));
                dt.Columns.Add("FEE_AMOUNT", typeof(System.String));
                dt.Columns.Add("TOTAL_SALE", typeof(System.String));

                for (int i = 0; i < dtRetail.Rows.Count; i++ )
                {

                    DataRow drData = dtRetail.Rows[i];
                    bool isVoided = drData["STATUS_CD"].ToString().Equals("VO");
                    DataRow dr = dt.NewRow();

                    dr["STATUS_CD"] = drData["STATUS_CD"];
                    dr["CSR_NR"] = drData["CSR_NR"];
                    dr["MD_DESC"] = "";
                    dr["TICKET_NR"] = drData["TICKET_NUMBER"];
                    dr["RECEIPT_NR"] = drData["RECEIPT_NUMBER"];
                    decimal totalAmt = 0;
                    decimal couponAmt = 0;

                    if (dr["RECEIPT_NR"].ToString().Length > 0)
                    {
                        int lastReceiptNr = int.Parse(dr["RECEIPT_NR"].ToString());
                        decimal receiptAmt = 0;

                        for (int j = i; j < dtRetail.Rows.Count ; j++, i++)
                        {
                            if (dtRetail.Rows[j]["RECEIPT_NUMBER"] == null || dtRetail.Rows[j]["RECEIPT_NUMBER"].ToString().Length == 0)
                                continue;

                            if (lastReceiptNr != int.Parse(dtRetail.Rows[j]["RECEIPT_NUMBER"].ToString()))
                            {
                                i--;
                                break;
                            }

                            if (decimal.TryParse(dtRetail.Rows[j]["REF_AMT"].ToString(), out receiptAmt))
                            {
                                if (dtRetail.Rows[j]["TENDER_TYPE"].Equals("COUPON"))
                                    couponAmt += receiptAmt;

                                totalAmt += receiptAmt;
                            }

                            if (isVoided)
                            {
                                dr["TENDER_TYPE"] = "VOID  ";
                                continue;
                            }
                            dr["TENDER_TYPE"] +=
                                string.Format("{0,10:C} {1,-15}\n",
                                    dtRetail.Rows[j]["REF_AMT"],
                                    dtRetail.Rows[j]["TENDER_TYPE"]);




                        }
                    }

                    else
                    {
                        dr["TENDER_TYPE"] +=
                            ((isVoided) ? "VOID  " : "" +
                                string.Format("{0,10:C} {1,-15}\n",
                                    dtRetail.Rows[0]["REF_AMT"],
                                    dtRetail.Rows[0]["TENDER_TYPE"]));
                    }

                    dr["NAME"] = drData["NAME"];
                    dr["TIME"] = drData["TIME"];
                    dr["ITEM_AMOUNT"] = "";

                    dr["AMOUNT"] = couponAmt; // GetDecimalValue(drData["AMOUNT"]);//totalAmt;
                    dr["SALES_TAX"] = drData["SALES_TAX"];
                    dr["FEE_AMOUNT"] = drData["FEE_AMOUNT"];
                    dr["TOTAL_SALE"] = GetDecimalValue(drData["TOTAL_SALE"]);

                    // use linq
                    for (int detl_iter = 0; detl_iter < dtRetailDetl.Rows.Count; detl_iter++)
                    {
                        bool isRelated = false;

                        if (!dataTableName.Equals("LAYAWAY_41"))
                            isRelated = drData["RECEIPT_NUMBER"].Equals(dtRetailDetl.Rows[detl_iter]["TICKET_NUMBER"]);

                        else
                            isRelated = drData["TICKET_NUMBER"].Equals(dtRetailDetl.Rows[detl_iter]["TICKET_NUMBER"]); 
                        

                        if (isRelated)
                        {
                            string tmpDesc = dtRetailDetl.Rows[detl_iter]["MD_DESC"].ToString();

                            dr["MD_DESC"] += tmpDesc.Substring(0, (tmpDesc.Length > 125) ? 125 : tmpDesc.Length) + "\n";

                            if (!isVoided) 
                                total_cost += GetDecimalValue(dtRetailDetl.Rows[detl_iter]["ITEM_COST"]);
                            dr["ITEM_AMOUNT"] += GetCurrencyValue(dtRetailDetl.Rows[detl_iter]["ITEM_COST"]) + "\n";
                        }


                    }

                    dt.Rows.Add(dr);
                    

                }

                if (dt.Rows.Count > 0)
                {
                    GroupData = dt;
                    DataLoaded = true;
                }
            }
        }

    }
}
