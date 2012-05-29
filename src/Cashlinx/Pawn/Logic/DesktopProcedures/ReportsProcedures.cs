/**************************************************************************
* Namespace:       CashlinxDesktop.DesktopProcedures
* Class:           ReportsProcedures
* 
* Description      The class handles data retrieval for reporting
* 
* History
*  3/4/2010 S.Murphy, Initial Development
*  no ticket 3/8/2010 SMurphy handle no data in WriteData()
*  no ticket 3/12/2010 SMurphy in Gun Disposition report - created a more unique filename 
*  no ticket 3/16/2010 SMurphy in Gun Disposition report - fix problem from more unique filename 
*  PWNU00000582 4/1/2010 SMurphy added start date info
*  PWNU00000618 4/7/2010 SMurphy Full Locations Report dropping data and sort issues
*  no ticket SMurphy 4/14/2010 restructured to allow Gun Disposition to match other reports
*  PWNU00000618 4/17/2010 replaced SMurphy 4/16/2010 Changed "RLO" to "RLN"
*  no ticket SMurphy changed reference for ReportsObject
*  Madhu 11/10/10 Madhu fix for defect PWNU00001411
*  Tracy 12/2/10   
*  EDW 3/8/12 - OH Partial Payments
* ******************************************************************************/
using System;
using System.Collections.Generic;
using System.Data;
using Common.Controllers.Application;
using Common.Controllers.Database.Oracle;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Objects.Mapper;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using Common.Libraries.Utility.Type;

namespace Pawn.Logic.DesktopProcedures
{
    public static class ReportsProcedures
    {
        public static ReportObject.DetailInventoryLinesAndSummary GetDetailInventoryData(DataSet outputData, ReportObject report)
        {
            var data = new ReportObject.DetailInventoryLinesAndSummary();
            var listInventorylines = new List<ReportObject.DetailInventoryLines>();
            var dtInventoryLines = outputData.Tables["detailed_inventory_rc"];
            if (dtInventoryLines != null && dtInventoryLines.IsInitialized && dtInventoryLines.Rows != null)
            {
                DataRow[] rows = dtInventoryLines.Select(report.ReportFilter, report.ReportSortSQL);
                for (int i = 0; i < rows.Length; i++)
                {
                    try
                    {
                        ReportObject.DetailInventoryLines inventoryLine = new ReportObject.DetailInventoryLines();
                        inventoryLine.status_cd = rows[i]["status_cd"].ToString();
                        inventoryLine.hold_desc = rows[i]["hold_desc"].ToString();
                        inventoryLine.ICN = rows[i]["ICN"].ToString();
                        inventoryLine.md_desc = rows[i]["md_desc"].ToString();
                        inventoryLine.quantity = Convert.ToInt32(rows[i]["quantity"]);
                        inventoryLine.item_amt = Convert.ToDecimal(rows[i]["item_amt"]);

                        //Madhu fix for defect PWNU00001411 
                        inventoryLine.item_amt = decimal.Parse(inventoryLine.item_amt.ToString("#0.00"));

                        inventoryLine.loc_aisle = rows[i]["loc_aisle"].ToString();
                        inventoryLine.loc_shelf = rows[i]["loc_shelf"].ToString();
                        inventoryLine.location = rows[i]["location"].ToString();
                        inventoryLine.gun_number = rows[i]["gun_number"].ToString();
                        inventoryLine.xicn = rows[i]["xicn"].ToString();
                        listInventorylines.Add(inventoryLine);
                    }
                    catch (Exception e)
                    {
                        report.ReportError = e.Message;
                        report.ReportErrorLevel = (int)LogLevel.ERROR;
                    }
                }

                data.InventoryData = listInventorylines;
            }

            List<ReportObject.DetailInventorySummary> listSummary = new List<ReportObject.DetailInventorySummary>();
            DataTable dtInventorySummary = outputData.Tables["detailed_inventory_totals_rc"];
            if (dtInventorySummary != null && dtInventorySummary.IsInitialized && dtInventorySummary.Rows != null)
            {
                DataRow[] rows = dtInventorySummary.Select(report.ReportFilter, report.ReportSortSQL);
                for (int i = 0; i < rows.Length; i++)
                {
                    try
                    {
                        ReportObject.DetailInventorySummary inventorySummary = new ReportObject.DetailInventorySummary();
                        inventorySummary.status_cd = rows[i]["status_cd"].ToString();
                        inventorySummary.on_Hold = rows[i]["on_Hold"].ToString();
                        inventorySummary.quantity = Convert.ToInt32(rows[i]["quantity"]);
                        inventorySummary.item_amt = Convert.ToDecimal(rows[i]["item_amt"]);
                        listSummary.Add(inventorySummary);
                    }
                    catch (Exception e)
                    {
                        report.ReportError = e.Message;
                        report.ReportErrorLevel = (int)LogLevel.ERROR;
                    }
                }
            }
            //here get summary info
            data.SummaryData = listSummary;
            //report.DetailInventoryData = data;
            return data;
        }

        public static List<ReportObject.LoanAudit> GetLoanAuditData(DataTable outputData, ReportObject report)
        {
            List<ReportObject.LoanAudit> data = new List<ReportObject.LoanAudit>();

            if (outputData != null && outputData.IsInitialized && outputData.Rows != null && outputData.Rows.Count > 0)
            {
                string sort;
                if (report.ReportSortSQL.Equals("date_made"))
                    sort = report.ReportSortSQL + ", ticket_number";
                else
                    sort = "ticket_number, " + report.ReportSortSQL;

                DataRow[] rows = outputData.Select(report.ReportFilter, sort);

                var lastLoan = string.Empty;

                for (int i = 0; i < rows.Length; i++)
                {
                    try
                    {
                        ReportObject.LoanAudit loanAuditData = new ReportObject.LoanAudit();

                        loanAuditData.CategoryName = rows[i]["cat_desc"].ToString();
                        loanAuditData.CurrTktNumber = rows[i]["ticket_number"].ToString();
                        loanAuditData.FullMdseDescr = rows[i]["md_desc"].ToString();
                        loanAuditData.GunNumber = rows[i]["gun_number"].ToString();

                        if (loanAuditData.GunNumber == "0")
                        {
                            loanAuditData.GunNumber = string.Empty;
                        }

                        loanAuditData.Location = rows[i]["location"].ToString();
                        loanAuditData.LocationAisle = rows[i]["loc_aisle"].ToString().ToUpper();

                        if (rows[i]["amount"].ToString().Length == 0)
                        {
                            loanAuditData.MdseLoanAmount = 0;
                        }
                        else
                        {
                            loanAuditData.MdseLoanAmount = Convert.ToDecimal(rows[i]["amount"]);
                        }

                        if (lastLoan != loanAuditData.CurrTktNumber)
                        {
                            lastLoan = loanAuditData.CurrTktNumber;
                            loanAuditData.TotalLoanAmt += Convert.ToDecimal(rows[i]["PRIN_AMOUNT"]);
                        }

                        loanAuditData.CurrentLoanAmount = Convert.ToDecimal(rows[i]["CURRENT_AMOUNT"]);

                        if (string.IsNullOrEmpty(rows[i]["PRIN_INC_PARPAY"].ToString()))
                        {
                            loanAuditData.PrincipalAmount = 0;
                        }
                        else
                        {
                            loanAuditData.PrincipalAmount = Convert.ToDecimal(rows[i]["PRIN_INC_PARPAY"]);
                        }
                        
                        loanAuditData.OrigTktNumber = rows[i]["org_ticket"].ToString();
                        loanAuditData.PawnLoanOrigDate = Convert.ToDateTime(rows[i]["date_made"]).ToString("MM/dd/yyyy");
                        loanAuditData.Shelf = rows[i]["loc_shelf"].ToString().ToUpper();
                        loanAuditData.ShopLoanStatus = rows[i]["status"].ToString();

                        data.Add(loanAuditData);
                    }
                    catch (Exception e)
                    {
                        report.ReportError = e.Message;
                        report.ReportErrorLevel = (int)LogLevel.ERROR;
                    }
                }
                report.LoanAuditData = data;
            }

            return data;
        }

        public static List<ReportObject.InPawnJewelry> GetInPawnJewelryData(DataTable outputData, ReportObject report)
        {
            List<ReportObject.InPawnJewelry> data = new List<ReportObject.InPawnJewelry>();

            if (outputData != null && outputData.IsInitialized && outputData.Rows != null && outputData.Rows.Count > 0)
            {
                //sort by location
                DataRow[] rows = outputData.Select(report.ReportFilter, report.ReportSortSQL);
                string holdLocation = string.Empty;
                Int32 intCount = 0;

                ReportObject.InPawnJewelry inPawnJewelryData = new ReportObject.InPawnJewelry();
                try
                {
                    holdLocation = rows[0]["location"].ToString();

                    for (Int32 i = 0; i < rows.Length; i++)
                    {
                        bool boolNextLocation = true;
                        for (; boolNextLocation && i < rows.Length; i++)
                        {
                            inPawnJewelryData = new ReportObject.InPawnJewelry();
                            if (holdLocation == rows[i]["location"].ToString())
                            {
                                intCount++;
                            }
                            else
                            {
                                inPawnJewelryData.Location = holdLocation.ToString();
                                inPawnJewelryData.ItemCount = intCount;
                                data.Add(inPawnJewelryData);
                                intCount = 1;
                                boolNextLocation = false;
                            }
                        }
                        if (i < rows.Length)
                        {
                            holdLocation = rows[i]["location"].ToString();
                        }
                    }

                    //get the last one
                    inPawnJewelryData.Location = holdLocation.ToString();
                    inPawnJewelryData.ItemCount = ++intCount;
                    data.Add(inPawnJewelryData);

                    report.InPawnJewelryData = data;
                }
                catch (Exception e)
                {
                    report.ReportError = e.Message;
                    report.ReportErrorLevel = (int)LogLevel.ERROR;
                }
            }

            return data;
        }

        public static List<ReportObject.GunAudit> GetGunAuditData(DataTable outputData, ReportObject report)
        {
            List<ReportObject.GunAudit> data = new List<ReportObject.GunAudit>();

            if (outputData != null && outputData.IsInitialized && outputData.Rows != null && outputData.Rows.Count > 0)
            {
                DataRow[] rows = outputData.Select(report.ReportFilter, report.ReportSortSQL);

                for (int i = 0; i < rows.Length; i++)
                {
                    try
                    {
                        ReportObject.GunAudit gunAuditData = new ReportObject.GunAudit();
                        gunAuditData.GunNumber = Convert.ToInt32(rows[i]["gun_number"]);
                        gunAuditData.CurrTktNumber = rows[i]["ticket_number"].ToString();
                        gunAuditData.OrigTktNumber = rows[i]["org_ticket"].ToString();
                        gunAuditData.ICN = rows[i]["icn"].ToString();
                        gunAuditData.GunType = rows[i]["gun_type"].ToString();
                        gunAuditData.MdseLoanAmount = rows[i]["amount"].ToString();
                        if (gunAuditData.MdseLoanAmount != "")
                        {
                            gunAuditData.MdseLoanAmount = Convert.ToDouble(rows[i]["amount"]).ToString("N");
                        }
                        else
                        {
                            gunAuditData.MdseLoanAmount = ".00";
                        }
                        gunAuditData.Cost = rows[i]["pfi_amount"].ToString();
                        if (gunAuditData.Cost != "")
                        {
                            gunAuditData.Cost = Convert.ToDouble(rows[i]["pfi_amount"]).ToString("N");
                        }
                        else
                        {
                            gunAuditData.Cost = ".00";
                        }
                        gunAuditData.ShopLoanStatus = rows[i]["status_cd"].ToString();
                        gunAuditData.GunImporter = rows[i]["importer"].ToString();
                        gunAuditData.LocationAisle = rows[i]["loc_aisle"].ToString();
                        gunAuditData.Shelf = rows[i]["loc_shelf"].ToString();
                        gunAuditData.Location = rows[i]["location"].ToString();
                        gunAuditData.FullMdseDescr = rows[i]["md_desc"].ToString();
                        gunAuditData.Category = Convert.ToInt16(rows[i]["cat_code"]);

                        data.Add(gunAuditData);
                    }
                    catch (Exception e)
                    {
                        report.ReportError = e.Message;
                        report.ReportErrorLevel = (int)LogLevel.ERROR;
                    }
                }
                report.GunAuditData = data;
            }

            return data;
        }

        public static List<ReportObject.RefurbItem> GetRefurbItemsList(DataTable outputData, ReportObject report)
        {
            List<ReportObject.RefurbItem> data = new List<ReportObject.RefurbItem>();

            if (outputData != null && outputData.IsInitialized && outputData.Rows != null && outputData.Rows.Count > 0)
            {
                DataRow[] rows = outputData.Select(report.ReportFilter, report.ReportSortSQL);

                for (int i = 0; i < rows.Length; i++)
                {
                    try
                    {
                        ReportObject.RefurbItem refurbItem = new ReportObject.RefurbItem();
                        refurbItem.Cost = Convert.ToDecimal(rows[i]["pfi_amount"]);
                        refurbItem.Description = rows[i]["md_desc"].ToString();
                        //refurbItem.ExpectedOrNotExpected = rows[i]["loc_aisle"].ToString();
                        refurbItem.ICN = rows[i]["icn"].ToString();
                        refurbItem.RefurbNumber = rows[i]["rfb_no"].ToString();
                        refurbItem.TransferDate = Convert.ToDateTime(rows[i]["transdate"].ToString());
                        refurbItem.TransferNumber = rows[i]["transticketnum"].ToString();
                        data.Add(refurbItem);
                    }
                    catch (Exception e)
                    {
                        report.ReportError = e.Message;
                        report.ReportErrorLevel = (int)LogLevel.ERROR;
                    }
                }
            }
            return data;
        }


        public static List<ReportObject.GunAuditATFOpenRecords> GetGunAuditATFOpenRecordsData(DataTable outputData, ReportObject report)
        {
            List<ReportObject.GunAuditATFOpenRecords> data = new List<ReportObject.GunAuditATFOpenRecords>();

            if (outputData != null && outputData.IsInitialized && outputData.Rows != null && outputData.Rows.Count > 0)
            {
                DataRow[] rows = outputData.Select(report.ReportFilter, report.ReportSortSQL);

                for (int i = 0; i < rows.Length; i++)
                {
                    try
                    {
                        ReportObject.GunAuditATFOpenRecords gunAuditData = new ReportObject.GunAuditATFOpenRecords();
                        gunAuditData.GunNumber = Convert.ToInt32(rows[i]["gun_number"]);
                        gunAuditData.ICN = rows[i]["icn"].ToString();
                        gunAuditData.LocationAisle = rows[i]["loc_aisle"].ToString();
                        gunAuditData.Shelf = rows[i]["loc_shelf"].ToString();
                        gunAuditData.Location = rows[i]["location"].ToString();
                        gunAuditData.Importer = rows[i]["importer"].ToString();
                        gunAuditData.GunType = rows[i]["gun_type"].ToString();

                        gunAuditData.Manufacturer = rows[i]["Manufacturer"].ToString();
                        gunAuditData.Model = rows[i]["Model"].ToString();
                        gunAuditData.SerialNumber = rows[i]["Serial_Number"].ToString();
                        gunAuditData.GaugeOrCaliber = rows[i]["Caliber"].ToString();
                        gunAuditData.TransactionNumber = rows[i]["ticket_number"].ToString();
                        gunAuditData.Status = rows[i]["Status_CD"].ToString();
                        //gunAuditData.StatusDescription = rows[i]["Status_CD"].ToString();
                        gunAuditData.GunDate = Convert.ToDateTime(rows[i]["Md_Date"]);
                        if(!string.IsNullOrEmpty(rows[i]["ACQUIRE_MIDDLE_INITIAL"].ToString()))
                            gunAuditData.CustomerName = rows[i]["acquire_last_name"].ToString() + " " +   rows[i]["ACQUIRE_MIDDLE_INITIAL"].ToString() + " " + rows[i]["acquire_first_name"].ToString();
                        else
                            gunAuditData.CustomerName = rows[i]["acquire_last_name"].ToString() + " "  + rows[i]["acquire_first_name"].ToString();
                        gunAuditData.CustomerAddress = rows[i]["Acquire_Address"].ToString();
                        gunAuditData.CustomerAddress1 = rows[i]["Acquire_city"].ToString() + ", " + rows[i]["Acquire_state"].ToString() + " " + rows[i]["Acquire_postal_code"].ToString();
                        if(rows[i]["acquire_id_type"].ToString().Equals("DRIVERLIC"))
                            gunAuditData.CustomerID = "DL: " + rows[i]["acquire_id_number"].ToString();
                        else
                            gunAuditData.CustomerID = "ID: " + rows[i]["acquire_id_number"].ToString();
                        gunAuditData.HoldDesc = rows[i]["Hold_Desc"].ToString();
                        gunAuditData.HoldType = rows[i]["Hold_Type"].ToString();
                        /*gunAuditData.CurrTktNumber = rows[i]["ticket_number"].ToString();
                        gunAuditData.OrigTktNumber = rows[i]["org_ticket"].ToString();
                        
                        gunAuditData.GunType = rows[i]["gun_type"].ToString();
                        gunAuditData.MdseLoanAmount = rows[i]["amount"].ToString();
                        if (gunAuditData.MdseLoanAmount != "")
                        {
                            gunAuditData.MdseLoanAmount = Convert.ToDouble(rows[i]["amount"]).ToString("N");
                        }
                        else
                        {
                            gunAuditData.MdseLoanAmount = ".00";
                        }
                        gunAuditData.Cost = rows[i]["pfi_amount"].ToString();
                        if (gunAuditData.Cost != "")
                        {
                            gunAuditData.Cost = Convert.ToDouble(rows[i]["pfi_amount"]).ToString("N");
                        }
                        else
                        {
                            gunAuditData.Cost = ".00";
                        }
                        gunAuditData.ShopLoanStatus = rows[i]["status_cd"].ToString();
                        gunAuditData.GunImporter = rows[i]["importer"].ToString();
                        gunAuditData.FullMdseDescr = rows[i]["md_desc"].ToString();
                        gunAuditData.Category = Convert.ToInt16(rows[i]["cat_code"]);*/

                        data.Add(gunAuditData);
                    }
                    catch (Exception e)
                    {
                        report.ReportError = e.Message;
                        report.ReportErrorLevel = (int)LogLevel.ERROR;
                    }
                }
                report.GunAuditDataATFOpenRecordsData = data;
            }

            return data;
        }

        public static int fullLocationsCompare(ReportObject.FullLocations x, ReportObject.FullLocations y)
        {
            int retval = 0;

            if ((retval = x.TransactionType.CompareTo(y.TransactionType)) != 0) return retval;
            else if ((retval = x.TransactionType.CompareTo(y.TransactionType)) != 0) return retval;
            else if ((retval = x.LocationAisle.CompareTo(y.LocationAisle)) != 0) return retval;
            else if ((retval = x.LocationAisle.CompareTo(y.LocationAisle)) != 0) return retval;
            else if ((retval = x.LocationShelf.CompareTo(y.LocationShelf)) != 0) return retval;
            else if ((retval = x.LocationShelf.CompareTo(y.LocationShelf)) != 0) return retval;
            else if ((retval = x.Location.CompareTo(y.Location)) != 0) return retval;
            else
                return x.CurrentTicketNumber.CompareTo(y.CurrentTicketNumber);
        }

        public static List<ReportObject.FullLocations> GetFullLocationData(DataSet outputData, ReportObject report)
        {
            List<ReportObject.FullLocations> dataPeopleTicket = new List<ReportObject.FullLocations>();
            List<ReportObject.FullLocations> dataReturn = new List<ReportObject.FullLocations>();
            DataTable mdseTable = new DataTable();
            DataTable cloneTable = new DataTable();

            if (outputData != null)
            {
                mdseTable = outputData.Tables["mdse"];

                if (mdseTable == null && cloneTable != null)
                {
                    report.ReportErrorLevel = (int)LogLevel.ERROR;
                    report.ReportError = "Report data for Full Locations Report contained pawn information but no matching merchandise!";
                }
                else
                {
                    //load up 3 people cursors 
                    cloneTable = MergeDatasets(outputData, new string[] { "pawn",  "purchase" }, report);
                }
            }

            //check
            if (report.ReportErrorLevel == 0)
            {
                //load up the FullLocationsData object with people/ticket info
                dataPeopleTicket = PrepFullLocationsData(cloneTable, "", "status_cd Asc, familyname Asc, firstname Asc, org_ticket Asc, prev_ticket Asc, ticket_number Asc", report);

                //next get all item data from mdse cursor match on orig ticket = ICN_doc
                if (mdseTable != null)
                {
                    foreach (ReportObject.FullLocations reportData in dataPeopleTicket)
                    {
                        try
                        {
                            //copy from people/ticket info
                            ReportObject.FullLocations fullLocationData = new ReportObject.FullLocations();

                            fullLocationData.CurrentTicketNumber = reportData.CurrentTicketNumber;
                            fullLocationData.OriginalTicketNumber = reportData.OriginalTicketNumber;
                            fullLocationData.PreviousTicketNumber = reportData.PreviousTicketNumber;
                            fullLocationData.EmployeeID = reportData.EmployeeID;
                            fullLocationData.FirstName = reportData.FirstName;
                            fullLocationData.LastName = reportData.LastName;
                            fullLocationData.TransactionType = reportData.TransactionType;
                            fullLocationData.SortOrder = reportData.SortOrder;

                            //find mdse records
                            DataRow[] searchRows;
                            if (reportData.TransactionType == "PUR")
                            {
                                searchRows = mdseTable.Select("icn_doc = " + reportData.CurrentTicketNumber, "icn_item Asc");
                            }
                            else
                            {
                                searchRows = mdseTable.Select("icn_doc = " + reportData.OriginalTicketNumber, "icn_item Asc");
                            }

                            //PWNU00000618 4/7/2010 SMurphy Full Locations Report dropping data
                            if (searchRows.Length > 0)//found a match
                            {
                                //check for dup record - if in > 1 cursor (pawn, ext, rollover) will cause invalid dupes
                                string holdItemNumber = string.Empty;

                                //load up the mdse values
                                for (int i = 0; i < searchRows.Length; i++)
                                {
                                    if (holdItemNumber == searchRows[i]["icn_item"].ToString())
                                    {
                                        holdItemNumber = string.Empty;
                                        break;
                                    }
                                    else
                                    {
                                        holdItemNumber = searchRows[i]["icn_item"].ToString();
                                    }

                                    if (i > 0)
                                    {
                                        fullLocationData = new ReportObject.FullLocations();
                                        fullLocationData.CurrentTicketNumber = reportData.CurrentTicketNumber;
                                        fullLocationData.OriginalTicketNumber = reportData.OriginalTicketNumber;
                                        fullLocationData.PreviousTicketNumber = reportData.PreviousTicketNumber;
                                        fullLocationData.EmployeeID = reportData.EmployeeID;
                                        fullLocationData.FirstName = reportData.FirstName;
                                        fullLocationData.LastName = reportData.LastName;
                                        fullLocationData.TransactionType = reportData.TransactionType;
                                        fullLocationData.SortOrder = reportData.SortOrder;
                                    }
                                    //new stuff
                                    fullLocationData.LocationAisle = searchRows[i]["loc_aisle"].ToString();
                                    fullLocationData.LocationShelf = searchRows[i]["loc_shelf"].ToString();
                                    fullLocationData.Location = searchRows[i]["location"].ToString();
                                    fullLocationData.CategoryCode = searchRows[i]["cat_code"].ToString();
                                    fullLocationData.FullMerchandiseDescription = searchRows[i]["md_desc"].ToString();
                                    fullLocationData.Importer = searchRows[i]["importer"].ToString();

                                    fullLocationData.TransactionAmount = searchRows[i]["amount"].ToString();
                                    if (!string.IsNullOrEmpty(fullLocationData.TransactionAmount))
                                    {
                                        fullLocationData.TransactionAmount = Convert.ToDouble(searchRows[i]["amount"]).ToString("N");
                                    }

                                    fullLocationData.PKValue = searchRows[i]["pk_amount"].ToString();
                                    if (!string.IsNullOrEmpty(fullLocationData.PKValue))
                                    {
                                        fullLocationData.PKValue = Convert.ToDouble(searchRows[i]["pk_amount"]).ToString("N");
                                    }

                                    fullLocationData.ProKnowRetailValue = searchRows[i]["pkr_amount"].ToString();
                                    if (!string.IsNullOrEmpty(fullLocationData.ProKnowRetailValue))
                                    {
                                        fullLocationData.ProKnowRetailValue = Convert.ToDouble(searchRows[i]["pkr_amount"]).ToString("N");
                                    }

                                    for (int k = 12; k < 26; k++)
                                    {
                                        if (!String.IsNullOrEmpty(searchRows[i][k].ToString()))
                                        {
                                            if (searchRows[i][k].ToString().ToUpper().Contains("CLASS"))
                                            {
                                                string[] itemClass = searchRows[i][k].ToString().Split('-');

                                                if (itemClass.GetLength(0) == 2)
                                                    fullLocationData.Class = itemClass[1].Trim();

                                                break;
                                            }
                                        }
                                    }
                                    if (Convert.ToInt32(fullLocationData.CategoryCode) >= 1000 &&
                                        Convert.ToInt32(fullLocationData.CategoryCode) <= 1999)
                                    {
                                        fullLocationData.RecordType = "Jewelry";
                                    }
                                    else if (Convert.ToInt32(fullLocationData.CategoryCode) >= 4110 &&
                                             Convert.ToInt32(fullLocationData.CategoryCode) <= 4340)
                                    {
                                        fullLocationData.RecordType = "Gun";
                                    }
                                    else
                                    {
                                        fullLocationData.RecordType = "Other";
                                    }

                                    dataReturn.Add(fullLocationData);
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            report.ReportError = e.Message;
                            report.ReportErrorLevel = (int)LogLevel.ERROR;
                        }
                    }
                }//no mdseTable data
            }

            dataReturn.Sort(fullLocationsCompare);

            return dataReturn;
        }

        public static DataTable GetStoresInMarketForUserId(string userId)
        {
            string package = "PAWN_STORE_PROCS";
            string procedure = "getStoresInMarketForUserID";

            List<PairType<string, string>> refCursors = new List<PairType<string, string>>();
            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_user_id", userId));

            refCursors = new List<PairType<string, string>>
            {
                new PairType<string, string>("o_stores_in_a_market", "MARKET_STORES"),
            };

            //data retrieval
            DataSet outputDataSet = null;
            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            try
            {
                dA.issueSqlStoredProcCommand(
                    "ccsowner", package, procedure,
                    inParams, refCursors, "o_error_code", "o_error_desc",
                    out outputDataSet);
            }
            catch (System.Exception)
            {
                outputDataSet = null;
            }

            if (outputDataSet == null || !outputDataSet.IsInitialized || outputDataSet.Tables.Count == 0)
            {
                outputDataSet = null;
            }

            if (outputDataSet != null)
            {
                return outputDataSet.Tables["MARKET_STORES"];
            }

            return null;
        }

        public static DataSet GetCursors(ReportObject report)
        {
            string package = string.Empty;
            string procedure = string.Empty;

            List<PairType<string, string>> refCursors = new List<PairType<string, string>>();
            List<OracleProcParam> inParams = new List<OracleProcParam>();

            switch (report.ReportNumber)
            {
                case (int)ReportIDs.RefurbList:
                    package = "PAWN_REPORTS";
                    procedure = "GetRefurbList";
                    inParams.Add(new OracleProcParam("p_store_number", report.ReportStore));

                    refCursors = new List<PairType<string, string>>
                    {
                        new PairType<string, string>("o_expected", "refurb_items_Expected"),
                        new PairType<string, string>("o_not_expected", "refurb_items_Not_Expected")
                    };
                    break;
                case (int)ReportIDs.DailySales:
                    package = "PAWN_REPORTS";
                    procedure = "get_discounted_sales";

                    inParams.Add(new OracleProcParam("p_store_number", report.ReportStore));
                    inParams.Add(new OracleProcParam("p_start_date", report.ReportParms[0].ToString()));
                    inParams.Add(new OracleProcParam("p_end_date", report.ReportParms[1].ToString()));
                    inParams.Add(new OracleProcParam("p_low_retail", report.ReportParms[2].ToString()));
                    inParams.Add(new OracleProcParam("p_high_retail", report.ReportParms[3].ToString()));
                    inParams.Add(new OracleProcParam("p_low_disc_pct", report.ReportParms[4].ToString()));
                    inParams.Add(new OracleProcParam("p_high_disc_pct", report.ReportParms[5].ToString()));
                    inParams.Add(new OracleProcParam("p_sort_by", report.ReportSort));
                    inParams.Add(new OracleProcParam("p_incld", report.ReportParms[6].ToString()));
                    refCursors = new List<PairType<string, string>>
                    {
                        new PairType<string, string>("o_sales_data", "daily_sales"),
                        new PairType<string, string>("o_sales_sumary", "daily_sales_summary")
                    };

                    break;

                case (int)ReportIDs.DetailInventory:
                    package = "PAWN_REPORTS";
                    procedure = "detailed_inventory_data";
                    string aisleValue = Convert.ToString(report.ReportParms[(int)EnumDetailInventory.Aisle]);
                    string shelfValue = Convert.ToString(report.ReportParms[(int)EnumDetailInventory.Shelf]);
                    string otherValue = Convert.ToString(report.ReportParms[(int)EnumDetailInventory.Other]);
                    inParams.Add(new OracleProcParam("p_store_number", report.ReportStore));
                    inParams.Add(new OracleProcParam("p_aisle", aisleValue));
                    inParams.Add(new OracleProcParam("p_shelf", shelfValue));
                    if (otherValue == "Select")
                        inParams.Add(new OracleProcParam("p_other", string.Empty));
                    else
                        inParams.Add(new OracleProcParam("p_other", otherValue));
                    //inParams.Add(new OracleProcParam("o_total", DataTypeConstants.PawnDataType.STRING, finaldate, ParameterDirection.Output, 12));
                    //inParams.Add(new OracleProcParam("o_store_name", DataTypeConstants.PawnDataType.STRING, store, ParameterDirection.Output, 256));

                    refCursors = new List<PairType<string, string>>
                    {
                        new PairType<string, string>("o_data", "detailed_inventory_rc"),
                        new PairType<string, string>("o_total", "detailed_inventory_totals_rc")
                    };
                    break;

                case (int)ReportIDs.RifleDispositionReport:
                case (int)ReportIDs.GunDispositionReport:

                    package = "gun_disp_rpt_201";
                    procedure = "get_rpt_data";
                    string store = string.Empty;
                    string finaldate = string.Empty;
                    inParams.Add(new OracleProcParam("day_cnt", (int)report.ReportParms[(int)GunDispEnums.DAYCOUNT]));
                    inParams.Add(new OracleProcParam("begin_date", Convert.ToDateTime(report.ReportParms[(int)ReportEnums.STARTDATE])));
                    inParams.Add(new OracleProcParam("init_day", Convert.ToDateTime(report.ReportParms[(int)ReportEnums.ENDDATE])));
                    inParams.Add(new OracleProcParam("forwards", (int)report.ReportParms[(int)GunDispEnums.DIRECTION]));
                    inParams.Add(new OracleProcParam("store_num", report.ReportStore));
                    inParams.Add(new OracleProcParam("report_type", report.ReportNumber));
                    inParams.Add(new OracleProcParam("o_final_day", DataTypeConstants.PawnDataType.STRING, finaldate, ParameterDirection.Output, 12));
                    inParams.Add(new OracleProcParam("o_store_name", DataTypeConstants.PawnDataType.STRING, store, ParameterDirection.Output, 256));

                    refCursors = new List<PairType<string, string>>
                    {
                        new PairType<string, string>("o_gun_detail", "GUN_DETAIL"),
                        new PairType<string, string>("o_header", "GUN_HEADER")
                    };
                    break;

                case (int)ReportIDs.LoanAuditReport://Loan Audit Report
                case (int)ReportIDs.FullLocationsReport://Full Locations Report
                case (int)ReportIDs.InPawnJewelryLocationReport://In Pawn Jewelry
                case (int)ReportIDs.GunAuditReportATFOpenRecords://Monthly Gun Audit Report
                    package = "PAWN_GENERATE_DOCUMENTS";
                    procedure = "reports";

                    List<string> inString1 = new List<string>();
                    inString1.Add(" ");
                    inParams.Add(new OracleProcParam("p_names", true, inString1));

                    inString1 = new List<string>();
                    inString1.Add(report.ReportStore);
                    inString1.Add(report.ReportParms[(int)ReportEnums.STARTDATE].ToString());
                    inString1.Add(report.ReportParms[(int)ReportEnums.ENDDATE].ToString());
                    inString1.Add(report.ReportNumber.ToString());
                    inParams.Add(new OracleProcParam("p_values", true, inString1));

                    //output cursors
                    //Setup ref cursor array
                    refCursors.Add(new PairType<string, string>("r_gun_audit", "gun_audit"));
                    refCursors.Add(new PairType<string, string>("r_loan_audit", "loan_audit"));
                    refCursors.Add(new PairType<string, string>("r_inpawn_jewelry", "inpawn_jewelry"));
                    refCursors.Add(new PairType<string, string>("r_extension", "extension"));
                    refCursors.Add(new PairType<string, string>("r_rollover", "rollover"));
                    refCursors.Add(new PairType<string, string>("r_pawn", "pawn"));
                    refCursors.Add(new PairType<string, string>("r_purchase", "purchase"));
                    refCursors.Add(new PairType<string, string>("r_mdse", "mdse"));
                    break;
                case (int)ReportIDs.GunAuditReport://Monthly Gun Audit Report
                    package = "PAWN_GENERATE_DOCUMENTS";
                    procedure = "reports";

                    List<string> inString = new List<string>();
                    inString.Add(" ");
                    inParams.Add(new OracleProcParam("p_names", true, inString));

                    inString = new List<string>();
                    inString.Add(report.ReportStore);
                    inString.Add(report.ReportParms[(int)ReportEnums.STARTDATE].ToString());
                    inString.Add(report.ReportParms[(int)ReportEnums.ENDDATE].ToString());
                    inString.Add(report.ReportNumber.ToString());
                    inParams.Add(new OracleProcParam("p_values", true, inString));

                    //output cursors
                    //Setup ref cursor array
                    refCursors.Add(new PairType<string, string>("r_gun_audit", "gun_audit"));
                    refCursors.Add(new PairType<string, string>("r_loan_audit", "loan_audit"));
                    refCursors.Add(new PairType<string, string>("r_inpawn_jewelry", "inpawn_jewelry"));
                    refCursors.Add(new PairType<string, string>("r_extension", "extension"));
                    refCursors.Add(new PairType<string, string>("r_rollover", "rollover"));
                    refCursors.Add(new PairType<string, string>("r_pawn", "pawn"));
                    refCursors.Add(new PairType<string, string>("r_purchase", "purchase"));
                    refCursors.Add(new PairType<string, string>("r_mdse", "mdse"));
                    break;
                case (int)ReportIDs.Snapshot://Snapshot Report
                    package = "PAWN_DSTR";
                    procedure = "get_snapShotSummary";
                    inParams.Add(new OracleProcParam("p_store_number", report.ReportStore));
                    string report_date = report.ReportParms[0].ToString();
                    inParams.Add(new OracleProcParam("p_report_date", report_date));

                    string supportsPartialPayments = "N";
                    if (new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).IsPartialPaymentAllowed(CashlinxDesktopSession.Instance.CurrentSiteId))
                    {
                        supportsPartialPayments = "Y";
                    }
                    inParams.Add(new OracleProcParam("p_support_partial_payments", supportsPartialPayments));
                    
                    refCursors.Add(new PairType<string, string>("o_category", "categories"));
                    refCursors.Add(new PairType<string, string>("o_snapshot", "snapshot"));
                    break;

                case (int)ReportIDs.CACCSales:
                    package = "PAWN_RETAIL";
                    procedure = "get_cacc_sales";
                    inParams.Add(new OracleProcParam("p_store_number", report.ReportStore));
                    inParams.Add(new OracleProcParam("p_report_date", report.ReportParms[0].ToString()));

                    refCursors.Add(new PairType<string, string>("o_cacc_sales", "cacc_sales"));
                    break;

                case (int)ReportIDs.JewelryCount:
                    package = "PAWN_RETAIL";  
                    procedure = "get_jewelry_sales";
                    inParams.Add(new OracleProcParam("p_store_number", report.ReportStore));
                    inParams.Add(new OracleProcParam("p_report_date", report.ReportParms[0].ToString()));

                    refCursors.Add(new PairType<string, string>("o_jewelry_sales", "jewelry_sales"));
                    refCursors.Add(new PairType<string, string>("o_jewelry_smry", "jewelry_smry"));
                    break;
            }

            //data retrieval
            DataSet outputDataSet = null;
            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;
            bool retval = false;

            try
            {

                retval = dA.issueSqlStoredProcCommand(
                    "ccsowner", package, procedure,
                    inParams, refCursors, "o_error_code", "o_error_desc",
                    out outputDataSet);

                if (!retval) // try again, just in case it was caused by the database package being out of synch
                {
                    retval = dA.issueSqlStoredProcCommand(
                        "ccsowner", package, procedure,
                        inParams, refCursors, "o_error_code", "o_error_desc",
                        out outputDataSet);
                }


            }
            catch (System.Exception)
            {
                report.ReportError = "There was an error processing your request. Please contact your administrator.";
                report.ReportErrorLevel = (int)LogLevel.ERROR;
            }

            if (!retval)
            {
                report.ReportErrorLevel = (int)LogLevel.INFO;
                report.ReportError = "There was an error retreiving data from the database, please try again.  If this error persists, please contact customer support.";
            }
 
            else if (outputDataSet == null || !outputDataSet.IsInitialized || outputDataSet.Tables == null || outputDataSet.Tables.Count == 0)
            {
                report.ReportErrorLevel = (int)LogLevel.INFO;
                report.ReportError = ReportConstants.NODATA;
            }

            return outputDataSet;
        }

        private static DataTable MergeDatasets(DataSet outputData, string[] cursors, ReportObject report)
        {
            DataTable cloneTable = new DataTable();
            report.ReportError = string.Empty; 
            report.ReportErrorLevel = 0;

            try
            {
                //find a table that isn't null so it can be cloned for the concatenated datatable
                for (int i = 0; i < cursors.Length; i++)
                {
                    if (outputData.Tables[cursors[i]] != null)
                    {
                        cloneTable = outputData.Tables[cursors[i]].Clone();
                        break;
                    }
                }

                //merge all non-null tables into a single table
                if (cloneTable != null)
                {
                    for (int i = 0; i < cursors.Length; i++)
                    {
                        if (outputData.Tables[cursors[i]] != null)
                        {
                            cloneTable.Merge(outputData.Tables[cursors[i]], true, MissingSchemaAction.Ignore);
                            //PWNU00000618 SMurphy 4/7/2010 - Full Locations Report dropping data
                            //break;
                        }
                    }
                }

                if (cloneTable == null || !cloneTable.IsInitialized || cloneTable.Rows == null || cloneTable.Rows.Count == 0)
                {
                    report.ReportError = ReportConstants.NODATA;
                    report.ReportErrorLevel = (int)LogLevel.INFO;
                }
            }
            catch (Exception e)
            {
                report.ReportError = e.Message;
                report.ReportErrorLevel = (int)LogLevel.ERROR;
            }

            return (cloneTable);
        }

        private static List<ReportObject.FullLocations> PrepFullLocationsData(DataTable dataTable, string filter, string sort, ReportObject report)
        {
            List<ReportObject.FullLocations> tmpData = new List<ReportObject.FullLocations>();

            DataRow[] rows = dataTable.Select();
            try
            {
                //sort = "org_ticket Asc, familyname Asc, firstname Asc"
                for (int i = 0; i < rows.Length; i++)
                {
                    //set the sort based on status 
                    switch (rows[i]["status_cd"].ToString())
                    {
                        case ("A"):
                            rows[i]["status_cd"] = "3";
                            break;

                        case ("IP"):
                            rows[i]["status_cd"] = "1";
                            break;

                        case ("PD"):
                        case ("RN"):
                        case ("RLN"):
                            rows[i]["status_cd"] = "2";
                            break;
                        case ("PUR"):
                            rows[i]["status_cd"] = "4";
                            break;
                        default:
                            rows[i]["status_cd"] = "5";
                            break;
                    }
                }

                //sort: status code - after it has been modified, org_ticket 

                rows = dataTable.Select(filter, sort);


                for (int i = 0; i < rows.Length; i++)
                {
                    ReportObject.FullLocations tmpLocationData = new ReportObject.FullLocations();

                    tmpLocationData.CurrentTicketNumber = rows[i]["ticket_number"].ToString();
                    tmpLocationData.OriginalTicketNumber = rows[i]["org_ticket"].ToString();
                    tmpLocationData.PreviousTicketNumber = rows[i]["prev_ticket"].ToString();
                    tmpLocationData.EmployeeID = rows[i]["ent_id"].ToString();
                    tmpLocationData.LastName = rows[i]["familyname"].ToString();
                    tmpLocationData.FirstName = rows[i]["firstname"].ToString();

                    //set the trans type to display based on the incoming status code
                    switch (rows[i]["status_cd"].ToString())
                    {
                        case "3":
                            tmpLocationData.TransactionType = "ELN";
                            tmpLocationData.SortOrder = 3;
                            break;

                        case "1":
                            tmpLocationData.TransactionType = "NLN";
                            tmpLocationData.SortOrder = 1;
                            break;

                        case "2":
                            tmpLocationData.TransactionType = "RLN";
                            tmpLocationData.SortOrder = 2;
                            break;
                        case "4":
                            tmpLocationData.TransactionType = "PUR";
                            tmpLocationData.SortOrder = 4;
                            break;
                        case "5":
                            tmpLocationData.TransactionType = "LAY";
                            tmpLocationData.SortOrder = 5;
                            break;
                    }

                    tmpData.Add(tmpLocationData);
                }
            }
            catch (Exception e)
            {
                report.ReportError = "Error in Prepping Full Locations Data!  " + e.Message;
                report.ReportErrorLevel = (int)LogLevel.ERROR;
            }

            return tmpData;
        }

        public static ReportObject.SnapshotContext GetSnapshotData(DataSet outputDataSet, ReportObject report)
        {
            ReportObject.SnapshotContext context = new ReportObject.SnapshotContext();

            Dictionary<double, ReportObject.Snapshot> snapshots = new Dictionary<double, ReportObject.Snapshot>();
            DataTable dtSnapshot = outputDataSet.Tables["snapshot"];
            if (dtSnapshot != null && dtSnapshot.IsInitialized && dtSnapshot.Rows != null && dtSnapshot.Rows.Count > 0)
            {
                DataRow[] rows = dtSnapshot.Select(report.ReportFilter, report.ReportSortSQL);

                for (int i = 0; i < rows.Length; i++)
                {
                    try
                    {
                        ReportObject.Snapshot snapshotData = new ReportObject.Snapshot();
                        if (rows[i]["SEQ"] != DBNull.Value)
                        {
                            snapshotData.SequenceNumber = Convert.ToDouble(rows[i]["SEQ"]);
                            if (rows[i]["TOTAL_COUNT"] != DBNull.Value)
                            {
                                snapshotData.Count = Convert.ToInt32(rows[i]["TOTAL_COUNT"]);
                            }
                            else
                                snapshotData.Count = 0;

                            if (rows[i]["TOTAL_AMOUNT"] != DBNull.Value)
                            {
                                snapshotData.Amount = Convert.ToDecimal(rows[i]["TOTAL_AMOUNT"]);
                            }
                            else
                                snapshotData.Amount = 0;

                            snapshots.Add(snapshotData.SequenceNumber, snapshotData);
                        }
                    }
                    catch (Exception e)
                    {
                        report.ReportError = e.Message;
                        report.ReportErrorLevel = (int)LogLevel.ERROR;
                    }
                }

                context.Data = snapshots;
            }

            List<ReportObject.SnapshotCategory> availableCategories = new List<ReportObject.SnapshotCategory>();
            DataTable dtCategories = outputDataSet.Tables["categories"];
            if (dtCategories != null && dtCategories.IsInitialized && dtCategories.Rows != null)
            {
                DataRow[] rows = dtCategories.Select(report.ReportFilter, report.ReportSortSQL);

                for (int i = 0; i < rows.Length; i++)
                {
                    try
                    {
                        ReportObject.SnapshotCategory availableCategory = new ReportObject.SnapshotCategory();
                        if (rows[i]["MINSEQ"].ToString().Length != 0)
                        {
                            availableCategory.MinSequenceNumber = Convert.ToDouble(rows[i]["MINSEQ"]);
                            availableCategory.MaxSequenceNumber = Convert.ToDouble(rows[i]["MAXSEQ"]);
                            availableCategories.Add(availableCategory);
                        }
                    }
                    catch (Exception e)
                    {
                        report.ReportError = e.Message;
                        report.ReportErrorLevel = (int)LogLevel.ERROR;
                    }
                }

                context.AvailableCategories = availableCategories;
            }

            return context;
        }
    }
}
