//*****************************************************************************
// 3-mar-2010  rjm  aAdded code to get our dollar values right justified on 
//                  line.  (PWNU00000238)
//
// 5-mar-2010  rjm   Changed madeTime from Timespan type to string type.  
//                   Changed assignment of value from madeDate.timeofDay to  
//                   ShopDateTime.Instance.ShopShortTime. This fixed the 
//                   issue of 12:00 AM showing up on the reciept printout.
//                    
//
// 9-mar-2010  rjm  Added amount tendered and change due on receipts 
//                  for extensions (PWNU00000456)
//
// 9-mar-2010  rjm  Added dollar signs ($) where missing. Added code to 
//                  get our dollar values right justified for extesion printouts
//
// 16-mar-2010 rjm  Changed generateLoanReceipt() to use a different template. 
//                  The new template is called srvcReceiptTemplate.tpl and
//                  has no disclaimer.  
//                   
// 26-mar-2010 rjm  Several changes to  generateLoanReceipt().
//                  1. removed Astericks (*) from display of ticket number
//                     for service receipt printouts
//                  2. made all dollar amounts right justified  
//                  3. changed text on receipts from 'Change Due' to 'Change'
//                  4. Added disclaimer to void of new loan
//                  5. Added two new methods  BuildSubTotalLine() and
//                     BuildDetailLine(). This removed a bunch of copy and 
//                     pasted code.
//
// 29-mar-2010 rjm  reformatted phone number to look like '(214) 555-9867'
//
// 05/21/2010 SR Added lost ticket printing for renewals and paydown
// 
// 06/14/2010 GJL Adding document storage integration        
//
// 01/04/2012 EDW CR#15166 - Put ID used to pickup firearm, in gun table
//
// 04/16/2012 EDW Build 13 - 1. Only populate Pawn.PfiNote for new loans in certain states.
//                           2. Indiana Police Cards for Pawn, and Customer Buys.
//*****************************************************************************
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Database;
using Common.Controllers.Database.Couch;
using Common.Controllers.Database.Couch.Impl;
using Common.Controllers.Database.Oracle;
using Common.Controllers.Database.Procedures;
using Common.Controllers.Security;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Objects.Layaway;
using Common.Libraries.Objects.Pawn;
using Common.Libraries.Objects.Purchase;
using Common.Libraries.Objects.Retail;
using Common.Libraries.Objects.Rules.Structure;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Collection;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.ISharp;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using Common.Libraries.Utility.String;
using Common.Libraries.Utility.Type;
//using LostTicketStatementPrint = Pawn.Logic.DesktopProcedures.LostTicketStatementPrint;
//using PickSlipPrint = Common.Controllers.Database.PickSlipPrint;
using Pawn.Logic.PrintQueue;
using Reports;
using Reports.Layaway;
using ReportObject = Common.Controllers.Application.ReportObject;
using ProcessTenderMode = Common.Controllers.Database.Procedures.ProcessTenderProcedures.ProcessTenderMode;
using Document = Common.Libraries.Objects.Doc.Document;

namespace Pawn.Logic.DesktopProcedures
{
    public sealed class ProcessTenderController : MarshalByRefObject
    {
        public static readonly string GUN_MERCH_TYPE_H = "H";
        public static readonly string GUN_MERCH_TYPE_L = "L";
        public static readonly int GUN_CATEG = 4390;
        public static readonly double NUM_DAYS_LEAP_YEAR = 366.0000d;
        public static readonly double NUM_DAYS_NONLEAP_YEAR = 365.0000d;

        public static readonly string NEXTNUM_GUN = "GUN";
        public static readonly string NEXTNUM_TKT = "TICKET";
        public static readonly int MAX_MASKS = 15;

        public static readonly string PROKNOWCODE = "PK";
        public static readonly string PROKNOWRETAILCODE = "PKR";
        public static readonly string PROCALLCODE = "PC";

        public static readonly string PMETALCODE = "PMETL";
        public static readonly string STONECODE = "STONE";
        public static readonly string INPAWN = "IP";

        public static readonly string CUSTPURCHASEMDSEREVMRTYPE = "PUR";
        public static readonly string VENDPURCHASEMDSEREVMRTYPE = "PURCH";
        public static readonly string RETURNMDSEREVMRTYPE = "RET";
        public static readonly string PURCHASEMDSEREVMRDESC = "PURCHASE PRICE";

        public static readonly PairType<int, int> WIPEDRV_CAT1 =
        new PairType<int, int>(3820, 3839);
        public static readonly PairType<int, int> WIPEDRV_CAT2 =
        new PairType<int, int>(3850, 3899);

        public static readonly int OTHERDSC_CODE = 999;

        private const string SERIALNUMBERATTRIBUTE = "Serial Number";
        private const decimal COSTPERCENTAGEFROMRETAIL = .65M;
        private bool IsRenewed = false;
        private bool printAddendum = false;
        private ReportObject.PawnTicketAddendum pTicketAddendum = new ReportObject.PawnTicketAddendum();


        public static string BUSINESS_UNIT;
        public static string STORE_NUMBER;
        public static string STORE_ADDRESS;
        public static string STORE_CITY;
        public static string STORE_STATE;
        public static string STORE_ZIP;
        public static string STORE_NAME;
        public static string STORE_PHONE;

        public static readonly string GUN_NUM_SHORT = "G#";
        public static readonly string GUN_NUM_LONG = "Gun #";

        private const string COMMA_SPACE = ", ";
        private const string SPACE = " ";

        #region Singleton variables
        /// <summary>
        /// Internal class lock object
        /// </summary>
        static readonly object mutexIntObj = new object();
        /// <summary>
        /// Singleton instance variable
        /// </summary>
#if __MULTI__
        // ReSharper disable InconsistentNaming
        static readonly object mutexObj = new object();
        static readonly Dictionary<int, ProcessTenderController> multiInstance =
        new Dictionary<int, ProcessTenderController>();
        // ReSharper restore InconsistentNaming
#else
        static readonly ProcessTenderController instance = new ProcessTenderController();

#endif
        /// <summary>
        /// Static constructor - forces compiler to initialize the object prior to any code access
        /// </summary>
        static ProcessTenderController()
        {
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }

        /// <summary>
        /// Static instance property accessor
        /// </summary>
        public static ProcessTenderController Instance
        {
            get
            {
#if (!__MULTI__)
                return (instance);
#else
                lock (mutexObj)
                {
                int tId = Thread.CurrentThread.ManagedThreadId;
                if (multiInstance.ContainsKey(tId))
                {
                return (multiInstance[tId]);
                }
                var sdT = new ProcessTenderController();
                multiInstance.Add(tId, sdT);
                return (sdT);
                }
#endif
            }
        }

        #endregion

        #region Private Fields
        private string formattedPhone; // = (817) 625-1195
        private const int MAX_LEN = 41;
        //private const int MAX_BOLD_LEN = 36;
        private List<PairType<int, long>> gunItemNumberIndices;
        private List<PairType<int, long>> jewelryItemIndices;
        private List<int> actualGunLineNumbers;
        //private List<Int64> gunNumbers;
        private Int64 ticketNumber;
        private Int64 purchaseNumber;
        private Int64 fullTicketNumber;
        private List<Hashtable> pawnTickets;
        private List<string> pawnTicketFiles;
        private DataTable docinfo;
        private DataTable printerinfo;
        private DataTable ipportinfo;
        private DateTime madeDate;
        //private TimeSpan madeTime;
        private string madeTime;
        private List<bool> retValues;
        private List<string> errorCodes;
        private List<string> errorTexts;
        private ProcessTenderMode operationalMode;
        private ArrayList _pawnTicketAddedToCouchDB = null;
        private bool isAddndmAllowed;

        #endregion

        #region Accessors
        public ProcessTenderMode OperationalMode
        {
            get
            {
                return (this.operationalMode);
            }
        }

        public Int64 TicketNumber
        {
            get
            {
                return (this.ticketNumber);
            }
        }

        public List<string> TicketFiles
        {
            get
            {
                return (this.pawnTicketFiles);
            }
        }

        public void ResetLoanData()
        {
            this.resetData();
        }

        public Int64 ReceiptNumber { get; private set; }

        public List<PairType<Int64, string>> ProcessedTickets { get; set; }

        #endregion

        #region Private Methods
        private void resetData()
        {
            lock (mutexIntObj)
            {
                this.gunItemNumberIndices.Clear();
                this.jewelryItemIndices.Clear();
                this.actualGunLineNumbers.Clear();
                this.ticketNumber = 0L;
                this.fullTicketNumber = 0L;
                this.ReceiptNumber = 0L;
                if (CollectionUtilities.isNotEmpty(this.pawnTickets))
                {
                    foreach (Hashtable h in this.pawnTickets)
                    {
                        if (h == null)
                            continue;
                        h.Clear();
                    }
                    this.pawnTickets.Clear();
                }
                if (this.docinfo != null)
                    this.docinfo.Clear();
                if (this.printerinfo != null)
                    this.printerinfo.Clear();
                if (this.ipportinfo != null)
                    this.ipportinfo.Clear();
                this.madeDate = ShopDateTime.Instance.ShopDate;
                this.madeTime = ShopDateTime.Instance.ShopShortTime;
                this.retValues.Clear();
                this.errorCodes.Clear();
                this.errorTexts.Clear();
                this.pawnTicketFiles.Clear();
                this.errorCodes.Clear();
                this.errorTexts.Clear();
                this.operationalMode = ProcessTenderMode.INITIALIZED;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void analyzeActiveLoan()
        {
            lock (mutexIntObj)
            {
                PawnLoan pLoan;
                if (GlobalDataAccessor.Instance.DesktopSession.CurrentPawnLoan != null)
                    pLoan = GlobalDataAccessor.Instance.DesktopSession.CurrentPawnLoan;
                else
                    pLoan = GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan;
                if (pLoan == null || CollectionUtilities.isEmpty(pLoan.Items))
                {
                    return;
                }
                this.gunItemNumberIndices.Clear();
                for (int i = 0; i < pLoan.Items.Count; ++i)
                {
                    Item pItem = pLoan.Items[i];
                    if (pItem == null)
                    {
                        continue;
                    }
                    this.gunItemNumberIndices.Add(new PairType<int, long>(-1, -1L));

                    if (pItem.IsJewelry)
                    {
                        this.jewelryItemIndices.Add(new PairType<int, long>(i, -1L));
                        continue;
                    }
                    if (!pItem.IsGun)
                        continue;
                    this.gunItemNumberIndices.Last().Left = i;
                    this.gunItemNumberIndices.Last().Right = pItem.GunNumber;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool retrieveTicketNumber()
        {
            bool rt = false;
            lock (mutexIntObj)
            {
                string errorCode, errorDesc;
                rt = ProcessTenderProcedures.ExecuteGetNextNumber(
                    GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreNumber,
                    NEXTNUM_TKT, ShopDateTime.Instance.ShopDate,
                    out this.ticketNumber,
                    out errorCode,
                    out errorDesc);
                this.fullTicketNumber = this.ticketNumber;
            }
            return (rt);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool retrieveGunNumbers(CustomerProductDataVO customerProdData)
        {
            if (customerProdData == null || CollectionUtilities.isEmpty(customerProdData.Items))
            {
                return (false);
            }

            if (!IsGunInvolved(customerProdData))
            {
                return (true);
            }

            lock (mutexIntObj)
            {
                foreach (PairType<int, long> gunNum in this.gunItemNumberIndices)
                {
                    //If item is not a gun, continue
                    if (gunNum.Left == -1)
                        continue;
                    long nextGunNumber;
                    string errorCode = string.Empty;
                    string errorDesc = string.Empty;
                    try
                    {
                        ProcessTenderProcedures.ExecuteGetNextNumber(
                            GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreNumber,
                            NEXTNUM_GUN, ShopDateTime.Instance.ShopDate,
                            out nextGunNumber,
                            out errorCode,
                            out errorDesc);
                    }
                    catch (Exception eX)
                    {
                        this.errorCodes.Add(errorCode);
                        this.errorTexts.Add(errorDesc);
                        BasicExceptionHandler.Instance.AddException("Next num gun value failed: error code = " + errorCode +
                                                                    ", errorDesc = " + errorDesc, new ApplicationException(eX.Message));
                        return (false);
                    }
                    gunNum.Right = nextGunNumber;
                }
            }
            return (true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool generatePawnTicketData()
        {
            Hashtable pawnTicketData;

            lock (mutexIntObj)
            {
                //Get ticket format 
                //BusinessRuleVO tktFmtVo = GlobalDataAccessor.Instance.DesktopSession.PawnBusinessRuleVO["PWN_BR-000"];
                string formName = string.Empty;
                bool retVal = new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).GetTicketName(GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId, out formName);
                if (!retVal)
                {
                    const string errMsg = "Form name to print ticket could not be found in rules file";
                    if (FileLogger.Instance.IsLogFatal)
                    {
                        FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                    }

                    BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                    return (false);

                }

                string storeNumber = GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreNumber;
                //string state = GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.State;
                //03/18/2010 GJL - Change for pawn sec to allow development to continue and to facilitate 
                //real machine names in the pawn sec database instead of 47-byte GUID values
                string terminalId;// = GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.TerminalId;
                string state = GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.State;
                if (state.Equals(States.Texas))
                {
                    formName = "ticketfmtL.TX";
                }
                else if (state.Equals(States.Oklahoma))
                {
                    formName = "ticketfmtL.OK";
                }

                if (!string.IsNullOrEmpty(global::Pawn.Properties.Resources.OverrideMachineName))
                {
                    terminalId = global::Pawn.Properties.Resources.OverrideMachineName;
                }
                else
                {
                    terminalId = GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.TerminalId;
                }
                /*if (tktFmtVo != null)
                {
                    if (!tktFmtVo.getComponentValue("PAWNTICKETFORMAT", ref formName))
                    {
                        BasicExceptionHandler.Instance.AddException("Could not find PAWNTICKETFORMAT for the current site", new ApplicationException());
                    }
                }*/

                //Get cashlinx desktop session
                DesktopSession cds = GlobalDataAccessor.Instance.DesktopSession;

                //Populate printer data into hash table
                pawnTicketData = new Hashtable();
                cds.LaserPrinter.AddDefaultFieldsToPrinterData(formName, 1, ref pawnTicketData);
                //Get the current site id
                SiteId siteId = cds.CurrentSiteId;

                //Get the customer
                CustomerVO currentCust = cds.ActiveCustomer;

                //GJL 3/30/2010
                // Must check to see if the customer is null
                if (currentCust == null)
                {
                    //Fatal log entry
                    //Return out or throw ApplicationException
                    string errMsg = string.Format("Customer is null.  Cannot produce loan ticket data for store {0}", siteId.StoreNumber);
                    if (FileLogger.Instance.IsLogFatal)
                    {
                        FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                    }

                    BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                    return (false);
                }

                //Get the loan vo
                PawnLoan currentLoan;
                if (cds.CurrentPawnLoan != null)
                {
                    currentLoan = cds.CurrentPawnLoan;
                }
                else
                {
                    currentLoan = cds.ActivePawnLoan;
                }
                var currentLoanUwVO = (UnderwritePawnLoanVO)currentLoan.ObjectUnderwritePawnLoanVO;
                //this.madeDate = currentLoanUwVO.MadeDate;

                this.madeDate = ShopDateTime.Instance.ShopDate;
                this.madeTime = ShopDateTime.Instance.ShopShortTime;
                DateTime shopDate = this.madeDate;
                String shopTime = ShopDateTime.Instance.ShopShortTime;   //ShopDateTime.Instance.ShopTime;

                //Compute top value
                decimal topValue = currentLoan.Amount +
                                    currentLoan.InterestAmount +
                                    currentLoan.ServiceCharge;

                //********* CUSTOMER SECTION **************//
                string lastFirstName = currentCust.LastName + ", " + currentCust.FirstName;
                //GJL 3/30/2010
                // - NOTE: Do not perform the Trim operation on a potentially null or empty
                //         string. The Trim operation has been moved inside the if statement
                //         The offending code has been commented out.
                //
                //if (!string.IsNullOrEmpty(currentCust.MiddleInitial.Trim()))
                bool setMiddleName = false;
                if (!string.IsNullOrEmpty(currentCust.MiddleInitial))
                {
                    string trimmedMiddle = currentCust.MiddleInitial.Trim();
                    if (!string.IsNullOrEmpty(trimmedMiddle))
                    {
                        trimmedMiddle = trimmedMiddle.Substring(0, 1).ToUpper();
                        lastFirstName += " " + trimmedMiddle;
                        pawnTicketData.Add("cust_middle_in", trimmedMiddle);
                        setMiddleName = true;
                    }
                }

                if (!setMiddleName)
                {
                    pawnTicketData.Add("cust_middle_in", " ");
                }

                DateTime curDOB = currentCust.DateOfBirth;
                pawnTicketData.Add("cust_last_first_mi", lastFirstName);
                AddressVO custAddr = currentCust.getHomeAddress();
                if (custAddr != null)
                {
                    if (!string.IsNullOrEmpty(custAddr.Address2))
                    {
                        if (!string.IsNullOrEmpty(custAddr.UnitNum))
                        {
                            pawnTicketData.Add("cust_address", custAddr.Address1 + ", " + custAddr.Address2 + " " + custAddr.UnitNum);
                        }
                        else
                        {
                            pawnTicketData.Add("cust_address", custAddr.Address1 + ", " + custAddr.Address2);
                        }
                    }
                    else if (!string.IsNullOrEmpty(custAddr.Address1))
                    {
                        if (!string.IsNullOrEmpty(custAddr.UnitNum))
                        {
                            pawnTicketData.Add("cust_address", custAddr.Address1);
                        }
                        else
                        {
                            pawnTicketData.Add("cust_address", custAddr.Address1 + " " + custAddr.UnitNum);
                        }
                    }

                    pawnTicketData.Add("cust_city_state_zip", custAddr.City + ", " + custAddr.State_Code + " " + custAddr.ZipCode);
                }
                else
                {
                    //Customer address is null
                    string errMsg = string.Format("Customer ({0},{1}) has null address.  Cannot create ticket data",
                                                  currentCust.CustomerNumber, currentCust.CustomerName);
                    if (FileLogger.Instance.IsLogFatal)
                    {
                        FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                    }

                    BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                }

                // Added for Indiana
                ContactVO custContactInfo = currentCust.getPrimaryContact();
                if (custContactInfo != null)
                {
                    pawnTicketData.Add("cust_phone", custContactInfo.CountryDialNumCode + custContactInfo.ContactAreaCode + custContactInfo.ContactPhoneNumber + custContactInfo.ContactExtension);
                }
                else
                {
                    pawnTicketData.Add("cust_phone", " ");
                }

                //Fix date format issue - GJL 06/15/09
                pawnTicketData.Add("cust_dob", curDOB.ToString("d", DateTimeFormatInfo.InvariantInfo));
                IdentificationVO idVo = currentCust.getFirstIdentity();
                if (idVo == null ||
                    string.IsNullOrEmpty(idVo.IdType) ||
                    string.IsNullOrEmpty(idVo.IdIssuer) ||
                    string.IsNullOrEmpty(idVo.IdValue))
                {
                    pawnTicketData.Add("cust_ID", "ID Not Found");
                }
                else
                {
                    var idType = idVo.IdType;
                    //For Indiana, display the abbreviated versions
                    if (state.Equals(States.Indiana))
                    {
                        idType = idType.Replace("DRIVERLIC", "DL");
                        idType = idType.Replace("PASSPORT", "PP");
                    }

                    pawnTicketData.Add("cust_ID", idType + "-" + idVo.IdIssuerCode + "-" + idVo.IdValue);
                }

                pawnTicketData.Add("_A_", currentCust.Age);
                //Customer Eye color
                pawnTicketData.Add("_E_", currentCust.EyeColor);
                //Customer Height
                pawnTicketData.Add("_P_", currentCust.Height);
                //Customer's gender
                pawnTicketData.Add("_S_", currentCust.Gender);

                pawnTicketData.Add("_HC_", currentCust.HairColor);//Oklahoma

                pawnTicketData.Add("cust_weight", currentCust.Weight);//Oklahoma

                pawnTicketData.Add("_R_", currentCust.Race); //Oklahoma

                string tempTicket = siteId.StoreNumber.PadLeft(5, '0') + this.fullTicketNumber;
                //**************** LOAN DETAILS *************************//
                pawnTicketData.Add("lnh_ticket", this.fullTicketNumber);
                //Do not print previous ticket number for new pawn loan - GJL 06/15/09
                if (this.OperationalMode == ProcessTenderMode.SERVICELOAN)
                {
                    pawnTicketData.Add("lnh_prev_ticket", currentLoan.PrevTicketNumber);
                    pawnTicketData.Add("lnh_org_ticket", currentLoan.OrigTicketNumber);
                }
                else
                {
                    pawnTicketData.Add("lnh_prev_ticket", " ");
                    pawnTicketData.Add("lnh_org_ticket", this.fullTicketNumber);
                }

                pawnTicketData.Add("lnh_time", shopTime);
                pawnTicketData.Add("lnh_date", shopDate.ToString("d", DateTimeFormatInfo.InvariantInfo));
                pawnTicketData.Add("lnh_due", currentLoanUwVO.DueDate.ToString("d", DateTimeFormatInfo.InvariantInfo));
                pawnTicketData.Add("lnh_grace", currentLoanUwVO.GraceDate.ToString("d", DateTimeFormatInfo.InvariantInfo));
                pawnTicketData.Add("lnh_ent", (string.IsNullOrEmpty(cds.UserName) ? "USER" : cds.UserName));
                pawnTicketData.Add("lnh_last_line", currentLoan.Items.Count);
                pawnTicketData.Add("lnh_pfi_elig", currentLoanUwVO.PFIDate.ToString("d", DateTimeFormatInfo.InvariantInfo));

                if (this.OperationalMode == ProcessTenderMode.SERVICELOAN)
                {
                    pawnTicketData.Add("lnh_refin", currentLoan.Amount.ToString("N"));
                    pawnTicketData.Add("dir_cash", "0.00");
                }
                else
                {
                    pawnTicketData.Add("lnh_refin", "0.00");
                    pawnTicketData.Add("dir_cash", currentLoan.Amount.ToString("N"));
                }

                pawnTicketData.Add("104lnh_ticket", "104" + tempTicket);
                pawnTicketData.Add("fin_chg", currentLoan.InterestAmount);
                decimal totalFinanceCharge = currentLoan.InterestAmount;
                if (currentLoan.StorageFeeAllowed)
                {
                    decimal strgFee = currentLoan.Fees.Find(p => p.FeeType == FeeTypes.STORAGE).Value;
                    pawnTicketData.Add("serv_chg", strgFee);
                    totalFinanceCharge = strgFee + currentLoan.InterestAmount;
                    topValue += strgFee;
                }
                else
                {
                    // Indiana, does not have Storage fees, but the pawn ticket form needs the serv_chg field populated. This is really the Service fee, in this case
                    decimal servFee = currentLoan.Fees.Find(p => p.FeeType == FeeTypes.SERVICE).Value;
                    pawnTicketData.Add("serv_chg", servFee);
                    totalFinanceCharge = servFee + currentLoan.InterestAmount;

                    // Service Fee's are allready included in the TopValue, far above.
                    //topValue += servFee;
                }

                int minimumHoursInPawn = new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).GetMinimumHoursInPawn(GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId);
                pawnTicketData.Add("store_min_inpawn", "MINIMUM TIME IN PAWN IS " + (minimumHoursInPawn / 24).ToString() + " DAYS"); //Indiana

                pawnTicketData.Add("_FIN_CHG", totalFinanceCharge); //Oklahoma and OHIO

                pawnTicketData.Add("_TOP_", topValue);
                if (state.ToUpper().Equals(States.Oklahoma) || state.ToUpper().Equals(States.Ohio))
                {
                    pawnTicketData.Add("_FIN_APR", currentLoan.InterestRate.ToString("f2"));
                }
                else
                {
                    pawnTicketData.Add("_FIN_APR", currentLoan.InterestRate);
                }

                pawnTicketData.Add("lnh_amount", currentLoan.Amount.ToString("N"));
                if (pawnTicketData.ContainsKey("form_location"))
                {
                    pawnTicketData["form_location"] = SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseTemplatePath;
                }
                else
                {
                    pawnTicketData.Add("form_location", SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseTemplatePath);
                }

                //***************** STORE DETAILS ***********************//
                //Get store details
                pawnTicketData.Add("store_name", STORE_NAME);
                pawnTicketData.Add("store_address_city_state_zip", STORE_ADDRESS + ", " + STORE_CITY + ", " + STORE_STATE + " " + STORE_ZIP);
                pawnTicketData.Add("store_phone", formattedPhone);

                //***************** DESC MERCHANDISE ********************//
                //Build entire loan description string
                //Get max length of desc line for store
                BusinessRuleVO ticketDims = cds.PawnBusinessRuleVO["PWN_BR-022"];
                string maxLineLength = "66";
                bool res = ticketDims.getComponentValue("CL_PWN_0005_MAXLINELNGTH", ref maxLineLength);
                if (!res)
                {
                    string errMsg = "Could not retrieve max line length from rules";
                    if (FileLogger.Instance.IsLogWarn)
                    {
                        FileLogger.Instance.logMessage(LogLevel.WARN, this, errMsg);
                    }
                    BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                }
                string maxLinesPerTkt = "10";
                res = ticketDims.getComponentValue("CL_PWN_0006_MAXLINESPTKT", ref maxLinesPerTkt);
                if (!res)
                {
                    string errMsg = "Could not retrieve max lines per ticket from rules";
                    if (FileLogger.Instance.IsLogWarn)
                    {
                        FileLogger.Instance.logMessage(LogLevel.WARN, this, errMsg);
                    }
                    BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                }

                string contAllowed = "N";
                res = ticketDims.getComponentValue("CL_PWN_0069_CONTDDESCALLWD", ref contAllowed);
                if (!res)
                {
                    string errMsg = "Could not retrieve continuation allowed value from rules";
                    if (FileLogger.Instance.IsLogWarn)
                    {
                        FileLogger.Instance.logMessage(LogLevel.WARN, this, errMsg);
                    }
                    BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                }

                string addndmAllowed = "N";
                res = ticketDims.getComponentValue("CL_PWN_0068_ADDNDMALLWD", ref addndmAllowed);
                if (!res)
                {
                    string errMsg = "Could not retrieve addendum allowed value from rules";
                    if (FileLogger.Instance.IsLogWarn)
                    {
                        FileLogger.Instance.logMessage(LogLevel.WARN, this, errMsg);
                    }

                    BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                }

                //Compute total number of chars allowed on one ticket
                int maxLineLengthVal = Utilities.GetIntegerValue(maxLineLength, 66);
                int maxLinesPerTktVal = Utilities.GetIntegerValue(maxLinesPerTkt, 10);
                bool isContAllowed = contAllowed == "Y" ? true : false;
                bool isAddndmAllowed = addndmAllowed == "Y" ? true : false;
                string pawnDesc;
                if (!LoanTicketLengthCalculator.ComputeDescription(currentLoan, this.gunItemNumberIndices, out pawnDesc))
                {
                    //If we cannot compute description, there is a problem
                    string errMsg = string.Format("Cannot compute description for loan {0}", currentLoan.TicketNumber);
                    if (FileLogger.Instance.IsLogFatal)
                    {
                        FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                    }

                    BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                    return (false);
                }

                List<string> splitStrs;
                List<PairType<int, long>> validGunNumberLines;
                if (!LoanTicketLengthCalculator.SplitDescription(pawnDesc, maxLineLengthVal, out splitStrs, out validGunNumberLines))
                {
                    string errMsg = string.Format("Cannot split string for pawn ticket in loan {0}", currentLoan.TicketNumber);
                    if (FileLogger.Instance.IsLogFatal)
                    {
                        FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                    }

                    BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                    return (false);
                }

                //If the split comes back with too many lines and neither continuation nor addendum not allowed, must exit the process
                if (splitStrs != null && splitStrs.Count > maxLinesPerTktVal && !isContAllowed && !isAddndmAllowed)
                {
                    //Invalid number of lines for the ticket because not allowing continuation
                    string errMsg =
                            string.Format(
                                          "Cannot fit description on one ticket (lines allowed = {0}, lines computed = {1}).  Too many lines and continuation not allowed.",
                                          maxLinesPerTkt, splitStrs.Count);
                    if (FileLogger.Instance.IsLogFatal)
                    {
                        FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                    }

                    BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                    return (false);
                }

                #region Compute number of tickets to print
                var numberTicketsToPrint = 1;
                List<string> addendumDescData = new List<string>();
                if (splitStrs.Count <= maxLinesPerTktVal)
                {
                    numberTicketsToPrint = 1;
                }
                else
                {
                    if (isContAllowed)
                    {
                        numberTicketsToPrint = (splitStrs.Count / maxLinesPerTktVal);
                        if (splitStrs.Count % maxLinesPerTktVal > 0)
                        {
                            numberTicketsToPrint++;
                        }
                    }
                    else if (isAddndmAllowed)
                    {
                        try
                        {

                            //If the first character of the string which should go into the
                            //addendum is not the start of the item description as denoted by [x]
                            //then we have to split the description at the previous item
                            if (!splitStrs[maxLinesPerTktVal].StartsWith("["))
                            {
                                int j;
                                for (j = maxLinesPerTktVal - 1; j >= 0; j--)
                                {
                                    int strRemoveIndx = splitStrs[j].LastIndexOf("[", System.StringComparison.Ordinal);
                                    if (strRemoveIndx > -1)
                                    {
                                        var stringToRemove = splitStrs[j].Substring(strRemoveIndx);
                                        splitStrs[j] = splitStrs[j].Substring(0, strRemoveIndx - 1);
                                        addendumDescData.Add(stringToRemove);
                                        break;
                                    }
                                }

                                for (int i = j + 1; i < splitStrs.Count; i++)
                                {
                                    addendumDescData.Add(splitStrs[i]);
                                }
                            }
                            else
                            {
                                for (int i = maxLinesPerTktVal; i < splitStrs.Count; i++)
                                {
                                    addendumDescData.Add(splitStrs[i]);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            if (FileLogger.Instance.IsLogFatal)
                            {
                                FileLogger.Instance.logMessage(LogLevel.FATAL, this, "Error forming addendum data" + ex.Message);
                            }

                        }
                    }
                }
                #endregion

                //Set print variables in hash table
                pawnTicketData["##TEMPLATEFILENAME##"] = formName;
                //Set number of copies
                if (!(pawnTicketData.ContainsKey("##HOWMANYCOPIES##")))
                {
                    pawnTicketData.Add("##HOWMANYCOPIES##", "1");
                }

                //Set md desc lines and print
                string fillStringUnderscore = StringUtilities.fillString("_", maxLineLengthVal);
                int gunNumIdx = 0;
                Regex re = new Regex(@"\[\d*\]");
                for (int i = 0; i < numberTicketsToPrint; i++)
                {
                    int offset = i * maxLinesPerTktVal;
                    int desc_idx = 1;
                    int gunloc_idx = 1;
                    gunNumIdx = i;
                    for (int j = offset; j < maxLinesPerTktVal + offset; ++j)
                    {
                        int keyIdx = desc_idx;
                        int mdLocKeyIdx = gunNumIdx + 1;
                        string keyNum = ("" + keyIdx).PadLeft(2, '0');
                        string mdKeyNum = ("" + mdLocKeyIdx).PadLeft(2, '0');
                        string mdDescKey = "md_desc_" + keyNum;
                        string mdLocKey = "md_location_" + mdKeyNum;

                        string descVal = string.Empty;
                        if (j < splitStrs.Count)
                        {
                            descVal = splitStrs[j];
                        }
                        //Set default values
                        pawnTicketData[mdDescKey] = " ";
                        pawnTicketData[mdLocKey] = " ";

                        //Update description index
                        ++desc_idx;
                        //Update gun location index
                        ++gunloc_idx;

                        if (!string.IsNullOrEmpty(descVal))
                        {
                            if (CollectionUtilities.isNotEmpty(validGunNumberLines))
                            {
                                var gunNumList = validGunNumberLines.FindAll(x => x.Left == mdLocKeyIdx);
                                if (CollectionUtilities.isNotEmpty(gunNumList))
                                {
                                    string gStr = string.Empty;
                                    foreach (var gEntry in gunNumList)
                                    {
                                        gStr = gStr + GUN_NUM_LONG + gEntry.Right + " ";
                                    }

                                    if (!string.IsNullOrEmpty(gStr))
                                    {
                                        pawnTicketData[mdLocKey] = gStr;
                                        gunNumIdx++;
                                    }
                                    else
                                    {
                                        pawnTicketData[mdLocKey] = fillStringUnderscore;
                                        gunNumIdx++;
                                    }
                                }
                                else
                                {
                                    if (state.Equals("OK"))
                                    {
                                        pawnTicketData[mdLocKey] = " ";
                                    }
                                    else
                                    {
                                        pawnTicketData[mdLocKey] = fillStringUnderscore;
                                    }

                                    gunNumIdx++;
                                }
                            }
                            else
                            {
                                if (state.Equals("OK"))
                                {
                                    pawnTicketData[mdLocKey] = " ";
                                }
                                else
                                {
                                    pawnTicketData[mdLocKey] = fillStringUnderscore;
                                }

                                gunNumIdx++;
                            }

                            /*                            
                             * if (descVal.Contains(GUN_NUM_SHORT))
                            {
                                //If we have a situation where a regular item and a gun number item sit on the same line,
                                //we need to determine if the gun number is after the regular item
                                //If it is after the regular item, we must increment the gun number index
                                bool incrementAfter = false;
                                if (re.IsMatch(descVal))
                                {
                                    int gunIdx = descVal.IndexOf(GUN_NUM_SHORT);
                                    //Check if the non-gun item comes before the gun item in the string
                                    if (!re.IsMatch(descVal, gunIdx))
                                    {
                                        //If we are here, non gun item precedes gun item in the string, increment gun number index
                                        gunNumIdx++;
                                    }
                                    else
                                    {
                                        incrementAfter = true;
                                    }

                                }

                                if (!incrementAfter && gunNumIdx < this.gunItemNumberIndices.Count)
                                {
                                    if (pawnTicketData.ContainsKey(mdLocKey))
                                    {
                                        pawnTicketData[mdLocKey] = GUN_NUM_LONG + this.gunItemNumberIndices[gunNumIdx].Right;
                                    }
                                    else
                                    {
                                        pawnTicketData.Add(mdLocKey, GUN_NUM_LONG + this.gunItemNumberIndices[gunNumIdx].Right);
                                    }
                                    gunNumIdx++;
                                }
                                else if (incrementAfter)
                                {
                                    //Increment the gun number index as the non-gun item on this list came after the gun number index
                                    gunNumIdx++;
                                    pawnTicketData[mdLocKey] = fillStringUnderscore;
                                }
                            }*/

                        }
                        else
                        {
                            pawnTicketData[mdLocKey] = fillStringUnderscore;
                            gunNumIdx++;
                        }

                        if (descVal.Length > maxLineLengthVal)
                        {
                            descVal = descVal.Substring(0, maxLineLengthVal);
                        }

                        if (string.IsNullOrEmpty(descVal))
                        {
                            descVal = "  ";
                        }

                        if (pawnTicketData.ContainsKey(mdDescKey))
                        {
                            pawnTicketData[mdDescKey] = descVal;
                        }
                        else
                        {
                            pawnTicketData.Add(mdDescKey, descVal);
                        }
                    }

                    this.pawnTickets.Add(pawnTicketData);
                    Hashtable pTickDat = new Hashtable(pawnTicketData);
                    pawnTicketData = pTickDat;
                    printAddendum = false;
                    if (isAddndmAllowed && addendumDescData.Count > 0)
                    {
                        printAddendum = true;
                        pTicketAddendum.customerFirstName = currentCust.FirstName;
                        pTicketAddendum.customerLastName = currentCust.LastName;
                        pTicketAddendum.customerInitials = currentCust.MiddleInitial;
                        pTicketAddendum.dateMade = this.madeDate;
                        pTicketAddendum.merchandiseDescription = addendumDescData;
                        pTicketAddendum.customerMiddleInitial = currentCust.MiddleInitial;
                        pTicketAddendum.customerSuffix = currentCust.CustTitleSuffix;
                        pTicketAddendum.dateMade = shopDate;
                        pTicketAddendum.dueDate = currentLoanUwVO.DueDate;
                        pTicketAddendum.itemNumber = 1;
                        pTicketAddendum.loanAmount = currentLoan.Amount;
                        pTicketAddendum.numberOfItems = currentLoan.Items.Count;
                        pTicketAddendum.pawnFinanceCharge = currentLoan.InterestAmount;
                        pTicketAddendum.pfiEligibleDate = currentLoanUwVO.PFIDate;
                    }
                }
            }

            return (true);
        }

        /// <summary>
        /// Determines if a pawn loan has a gun or not
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static bool IsGunInvolved(CustomerProductDataVO p)
        {
            if (p == null || CollectionUtilities.isEmpty(p.Items))
                return (false);
            foreach (var pI in p.Items)
            {
                if (pI.IsGun)
                    return (true);
            }
            return (false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fullPawnTicketNumber"></param>
        /// <param name="mercDescription"></param>
        /// <param name="customerNumber"></param>
        /// <param name="receiptNumber"></param>
        /// <returns></returns>
        public static bool PrintWipeDriveDocument(
            string fullPawnTicketNumber,
            string mercDescription,
            string customerNumber,
            Int64 receiptNumber)
        {
            Dictionary<string, string> eDeviceData;
            lock (mutexIntObj)
            {
                //Get ticket format 
                BusinessRuleVO tktFmtVo = GlobalDataAccessor.Instance.DesktopSession.PawnBusinessRuleVO["PWN_BR-000"];
                string storeNumber = GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreNumber;
                //03/18/2010 GJL - Change for pawn sec to allow development to continue and to facilitate 
                //real machine names in the pawn sec database instead of 47-byte GUID values
                /*string terminalId;// = GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.TerminalId;
                if (!string.IsNullOrEmpty(global::Pawn.Properties.Resources.OverrideMachineName))
                {
                    terminalId = global::Pawn.Properties.Resources.OverrideMachineName;
                }
                else
                {
                    terminalId = GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.TerminalId;
                }*/
                string formName = "eDeviceTemplate.pdf";

                if (tktFmtVo != null)
                {
                    if (!tktFmtVo.getComponentValue("EDEVICEFORM", ref formName))
                    {
                        if (FileLogger.Instance.IsLogWarn)
                        {
                            FileLogger.Instance.logMessage(LogLevel.WARN, "ProcessTenderController", "Could not find edevice form template name for the current site");
                        }
                    }
                }

                try
                {

                    //Get cashlinx desktop session
                    DesktopSession cds = GlobalDataAccessor.Instance.DesktopSession;
                    eDeviceData = new Dictionary<string, string>();
                    cds.LaserPrinter.AddDefaultFieldsToPrinterData(formName, 1, ref eDeviceData);
                    //Set form specific data
                    eDeviceData.Add("eda_pwn_ticket_num", storeNumber + fullPawnTicketNumber);
                    eDeviceData.Add("eda_short_merchandise_desc", mercDescription);
                    DateTime shopDate = ShopDateTime.Instance.ShopDate.Date;
                    string monthDate = shopDate.ToString("m");
                    int monthDateIdx = monthDate.IndexOf(' ');
                    string monthName = "" + shopDate.Month;
                    if (monthDateIdx != -1)
                    {
                        monthName = monthDate.Substring(0, monthDateIdx + 1);
                    }
                    string shopDateStr = shopDate.ToShortDateString();
                    eDeviceData.Add("eda_month_day_year", shopDateStr);

                    //Prepare and print form
                    PDFITextSharpUtilities.PdfSharpTools tools;
                    PDFITextSharpUtilities.OpenPDFFile(
                        SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseTemplatePath + "\\" + formName,
                        out tools);
                    if (tools == null)
                    {
                        if (FileLogger.Instance.IsLogError)
                        {
                            FileLogger.Instance.logMessage(LogLevel.ERROR, "ProcessTenderController", "Could not get PDF tools instance for wipe drive");
                            return (false);
                        }
                    }
                    //Generate output file name
                    string timeStamp = DateTime.Now.ToString("yyMMddHHmmssf");
                    string genDocsDir = SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseLogPath + "\\";
                    string outputFileName = genDocsDir + formName + "-" + timeStamp + ".pdf";

                    //Stamp PDF
                    if (!PDFITextSharpUtilities.StampSimplePDFWithFormFields(tools,
                                                                             outputFileName, false, eDeviceData))
                    {
                        if (FileLogger.Instance.IsLogError)
                        {
                            FileLogger.Instance.logMessage(LogLevel.ERROR, "ProcessTenderController", "Could not stamp PDF document for wipe drive");
                        }
                        return (false);
                    }

                    if (SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled &&
                        cds.LaserPrinter.IsValid)
                    {
                        if (FileLogger.Instance.IsLogInfo)
                        {
                            FileLogger.Instance.logMessage(LogLevel.INFO, "ProcessTenderController",
                                                           "Printing wipe drive document on printer {0}", cds.LaserPrinter);
                        }

                        if (!PDFITextSharpUtilities.PrintOutputPDFFile(cds.LaserPrinter.IPAddress, cds.LaserPrinter.Port.ToString(), 1,
                                                                           tools))
                        {
                            if (FileLogger.Instance.IsLogError)
                            {
                                FileLogger.Instance.logMessage(LogLevel.ERROR, "ProcessTenderController",
                                                               "Could not print wipe drive PDF file");
                            }
                        }
                    }

                    if (tools != null)
                    {
                        if (FileLogger.Instance.IsLogInfo)
                        {
                            FileLogger.Instance.logMessage(LogLevel.INFO, "ProcessTenderController", "Printed wipe drive document to {0}",
                                                           cds.LaserPrinter);
                        }
                        OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;
                        SecuredCouchConnector cC = GlobalDataAccessor.Instance.CouchDBConnector;
                        int ticketNumber;
                        if (!Int32.TryParse(fullPawnTicketNumber, out ticketNumber))
                        {
                            //Could not parse ticket number, not a failure condition
                            //but should be noted
                        }
                        //Ensure full file name is valid
                        //Get accessors
                        var pDoc = new CouchDbUtils.PawnDocInfo();

                        //Set document add calls
                        pDoc.UseCurrentShopDateTime = true;
                        pDoc.StoreNumber = cds.CurrentSiteId.StoreNumber;
                        pDoc.CustomerNumber = customerNumber;
                        pDoc.TicketNumber = ticketNumber;
                        pDoc.DocumentType = Common.Libraries.Objects.Doc.Document.DocTypeNames.PDF;
                        pDoc.DocFileName = tools.OutputFileName;
                        pDoc.ReceiptNumber = receiptNumber;

                        //Add this document to the pawn document registry and document storage
                        string errText;
                        if (!CouchDbUtils.AddPawnDocument(dA, cC, cds.UserName, ref pDoc, out errText))
                        {
                            if (FileLogger.Instance.IsLogError)
                                FileLogger.Instance.logMessage(LogLevel.ERROR,
                                                               "ProcessTenderController",
                                                               "Could not store wipe drive form in document storage: {0} - FileName: {1}",
                                                               errText,
                                                               tools.OutputFileName);
                            BasicExceptionHandler.Instance.AddException(
                                "Could not store wipe drive form in document storage",
                                new ApplicationException(
                                    "Could not store wipe drive form in document storage: " +
                                    errText));
                        }
                    }

                    if (FileLogger.Instance.IsLogDebug)
                    {
                        FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "Printed wipe drive document {0} to file output named {1} with a status of {2}", formName, outputFileName, true);
                    }
                }
                catch (Exception eX)
                {
                    if (FileLogger.Instance.IsLogError)
                    {
                        FileLogger.Instance.logMessage(LogLevel.ERROR, null, "Could not print wipe drive document: " + eX.ToString());
                    }
                    BasicExceptionHandler.Instance.AddException("Could not print wipe drive document", new ApplicationException("Wipe drive document printer error", eX));
                    return (false);
                }
            }
            return (true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pawnDateMade"></param>
        /// <param name="pawnTicketNumber"></param>
        /// <param name="custNumber"></param>
        /// <returns></returns>
        public static bool PrintBackgroundFireCheckDocument(
            DateTime pawnDateMade,
            string pawnTicketNumber,
            string custNumber)
        {
            Dictionary<string, string> fbcfData;
            lock (mutexIntObj)
            {
                //Get ticket format 
                BusinessRuleVO tktFmtVo = GlobalDataAccessor.Instance.DesktopSession.PawnBusinessRuleVO["PWN_BR-000"];
                string storeNumber = GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreNumber;
                //03/18/2010 GJL - Change for pawn sec to allow development to continue and to facilitate 
                //real machine names in the pawn sec database instead of 47-byte GUID values
                string formName = "FBCFtemplate.TX";
                if (tktFmtVo != null)
                {
                    if (!tktFmtVo.getComponentValue("FBCFFORM", ref formName))
                    {
                        //BasicExceptionHandler.Instance.AddException("Could not find FBCFFORM for the current site", new ApplicationException());
                    }
                }

                try
                {
                    fbcfData = new Dictionary<string, string>();
                    //Get cashlinx desktop session
                    DesktopSession cds = GlobalDataAccessor.Instance.DesktopSession;

                    //Set print variables in hash table
                    //SR 3/31/2010 Removed the following 3 lines since they are already added in the dictionary
                    //fbcfData.Add("##TEMPLATEFILENAME##", formName);
                    //fbcfData.Add("##HOWMANYCOPIES##", "1");
                    //fbcfData.Add("form_location", SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseTemplatePath);
                    //Set printer data
                    cds.LaserPrinter.AddDefaultFieldsToPrinterData(formName, 1, ref fbcfData);

                    //Set form specific data
                    fbcfData.Add("fbf_shop_name_and_number", STORE_NAME + ", " + cds.CurrentSiteId.StoreNumber);
                    fbcfData.Add("fbf_shop_address", STORE_ADDRESS);
                    fbcfData.Add("fbf_shop_city_state_zip", STORE_CITY + ", " + STORE_STATE + " " + STORE_ZIP);
                    fbcfData.Add("fbf_pwn_ticket_num", storeNumber + pawnTicketNumber);
                    fbcfData.Add("fbf_empl_id", cds.UserName.ToLowerInvariant());
                    fbcfData.Add("fbf_pwn_date_made", pawnDateMade.ToString("d", DateTimeFormatInfo.InvariantInfo));
                    fbcfData.Add("fbf_business_unit", GetLongBusinessUnit() + " from all liability in the event you are ineligible to pick up your firearm(s).");
                    string midInitial = cds.ActiveCustomer.MiddleInitial;
                    fbcfData.Add("fbf_cust_last_first_middle_initial",
                                 (cds.ActiveCustomer.LastName + ", " +
                                  cds.ActiveCustomer.FirstName + " " +
                                  (string.IsNullOrEmpty(midInitial) ? " " : midInitial)));

                    //Prepare and print form
                    PDFITextSharpUtilities.PdfSharpTools tools;
                    PDFITextSharpUtilities.OpenPDFFile(
                        SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseTemplatePath + "\\" + formName,
                        out tools);
                    if (tools == null)
                    {
                        if (FileLogger.Instance.IsLogError)
                        {
                            FileLogger.Instance.logMessage(LogLevel.ERROR, "ProcessTenderController", "Could not get PDF tools instance for FBCF print");
                            return (false);
                        }
                    }
                    //Generate output file name
                    string timeStamp = DateTime.Now.ToString("yyMMddHHmmssf");
                    string genDocsDir = SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseLogPath + "\\";
                    string outputFileName = genDocsDir + formName + "-" + timeStamp + ".pdf";

                    //Stamp PDF
                    if (!PDFITextSharpUtilities.StampSimplePDFWithFormFields(tools,
                                                                             outputFileName, false, fbcfData))
                    {
                        if (FileLogger.Instance.IsLogError)
                        {
                            FileLogger.Instance.logMessage(LogLevel.ERROR, "ProcessTenderController", "Could not stamp PDF document for FBCF print");
                        }
                        return (false);
                    }

                    if (SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled &&
                        cds.LaserPrinter.IsValid)
                    {
                        if (FileLogger.Instance.IsLogInfo)
                        {
                            FileLogger.Instance.logMessage(LogLevel.INFO, "ProcessTenderController", "Printing FBCF document on printer {0}",
                                                           cds.LaserPrinter);
                        }
                        if (!PDFITextSharpUtilities.PrintOutputPDFFile(
                            cds.LaserPrinter.IPAddress,
                            cds.LaserPrinter.Port.ToString(), 1,
                            tools))
                        {
                            if (FileLogger.Instance.IsLogError)
                            {
                                FileLogger.Instance.logMessage(LogLevel.ERROR, "ProcessTenderController", "Could not print FBCF PDF file");
                            }
                        }
                    }

                    if (tools != null)
                    {
                        if (FileLogger.Instance.IsLogInfo)
                        {
                            FileLogger.Instance.logMessage(LogLevel.INFO, "ProcessTenderController", "Printed FBCF on printer {0}",
                                                           cds.LaserPrinter);
                        }
                        int ticketNumber = 0;
                        OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;
                        SecuredCouchConnector cC = GlobalDataAccessor.Instance.CouchDBConnector;
                        if (!Int32.TryParse(pawnTicketNumber, out ticketNumber))
                        {
                            //Could not parse ticket number, not a failure condition
                            //but should be noted
                        }
                        //Ensure full file name is valid
                        //Get accessors
                        var pDoc = new CouchDbUtils.PawnDocInfo();

                        //Set document add calls
                        pDoc.UseCurrentShopDateTime = true;
                        pDoc.StoreNumber = storeNumber;
                        pDoc.CustomerNumber = custNumber;
                        pDoc.TicketNumber = ticketNumber;
                        pDoc.DocumentType = Document.DocTypeNames.PDF;
                        pDoc.DocFileName = tools.OutputFileName;

                        //Add this document to the pawn document registry and document storage
                        string errText;
                        if (!CouchDbUtils.AddPawnDocument(dA, cC, cds.UserName, ref pDoc, out errText))
                        {
                            if (FileLogger.Instance.IsLogError)
                                FileLogger.Instance.logMessage(LogLevel.ERROR, "ProcessTenderController",
                                                               "Could not store firearm background check form in document storage: {0} - FileName: {1}", errText, tools.OutputFileName);
                            BasicExceptionHandler.Instance.AddException(
                                "Could not store firearm background check form in document storage",
                                new ApplicationException("Could not store firearm background check form in document storage: " + errText));
                        }
                    }

                    if (FileLogger.Instance.IsLogDebug)
                    {
                        FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "Printed FBCF document {0} to file output named {1} with a status of {2}", formName, outputFileName, true);
                    }
                }
                catch (Exception eX)
                {
                    if (FileLogger.Instance.IsLogError)
                    {
                        FileLogger.Instance.logMessage(LogLevel.ERROR, null, "Could not print FBCF document: " + eX.ToString());
                    }
                    BasicExceptionHandler.Instance.AddException("Could not print FBCF document", new ApplicationException("FBCF document printer error", eX));
                    return (false);
                }
            }
            return (true);
        }

        private static string GetLongBusinessUnit()
        {
            switch (BUSINESS_UNIT)
            {
                case "CA":
                    return "Cash America";
                case "CL":
                    return "CashLand";
                default:
                    return BUSINESS_UNIT;
            }
        }

        private bool pawnTicketPrint()
        {
            if (CollectionUtilities.isEmpty(this.pawnTickets))
            {
                this.pawnTickets = new List<Hashtable>();
            }
            if (CollectionUtilities.isEmpty(this.ProcessedTickets))
            {
                this.ProcessedTickets = new List<PairType<long, string>>();
            }
            if (_pawnTicketAddedToCouchDB == null)
            {
                _pawnTicketAddedToCouchDB = new ArrayList();
            }
            foreach (Hashtable h in this.pawnTickets)
            {
                string fileName = "";
#if (!__MULTI__)
                GenerateDocumentsPrinter.PrintAndDisplayDocument(h, !SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled, out fileName);
#endif
                this.pawnTicketFiles.Add(fileName);

                this.ProcessedTickets.Add(new PairType<long, string>(Utilities.GetLongValue(h["lnh_ticket"], 0), fileName));
            }

            //Print firearm document if there is a firearm on the new loan
            DesktopSession cds = GlobalDataAccessor.Instance.DesktopSession;
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;
            SecuredCouchConnector cC = GlobalDataAccessor.Instance.CouchDBConnector;
            PawnLoan currPawnLoan = null;
            if (cds.CurrentPawnLoan != null && cds.ServiceLoans.Count > 0)
                currPawnLoan = cds.CurrentPawnLoan;
            else if (cds.ActivePawnLoan != null)
                currPawnLoan = cds.ActivePawnLoan;

            //Store all pawn tickets
            foreach (PairType<long, string> pairType in this.ProcessedTickets)
            {
                bool alreadyStored = false;
                if (string.IsNullOrEmpty(pairType.Right))
                    continue;

                //Ensure full file name is valid
                //Get accessors
                var pDoc = new CouchDbUtils.PawnDocInfo();
                //Set document add calls
                pDoc.UseCurrentShopDateTime = true;
                pDoc.StoreNumber = cds.CurrentSiteId.StoreNumber;
                pDoc.CustomerNumber = cds.ActiveCustomer.CustomerNumber;
                pDoc.TicketNumber = (int)pairType.Left;
                pDoc.DocumentType = Document.DocTypeNames.PDF;
                pDoc.DocFileName = pairType.Right;
                pDoc.ReceiptNumber = this.ReceiptNumber;

                //Add this document to the pawn document registry and document storage
                string errText;
                //search arraylist first
                if (_pawnTicketAddedToCouchDB.Count > 0)
                {
                    foreach (int ticketNumber in _pawnTicketAddedToCouchDB)
                    {
                        if (ticketNumber == (int)pairType.Left)
                        {
                            alreadyStored = true;
                            break;
                        }
                    }
                }
                if (!alreadyStored)
                {
                    if (!CouchDbUtils.AddPawnDocument(dA, cC, cds.UserName, ref pDoc, out errText))
                    {
                        if (FileLogger.Instance.IsLogError)
                            FileLogger.Instance.logMessage(LogLevel.ERROR, this,
                                                           "Could not store pawn ticket in document storage: {0} - FileName: {1}", errText, pairType.Right);
                        BasicExceptionHandler.Instance.AddException(
                            "Could not store pawn ticket in document storage",
                            new ApplicationException("Could not store pawn ticket in document storage: " + errText));
                    }
                    else
                    {
                        _pawnTicketAddedToCouchDB.Add((int)pairType.Left);
                    }
                }
            }

            if (currPawnLoan != null)
            {
                //Fix for BZ # 115
                if (!IsRenewed || GlobalDataAccessor.Instance.DesktopSession.CustomerNotPledgor)
                {
                    if (IsGunInvolved(currPawnLoan))
                    {
                        if (FileLogger.Instance.IsLogDebug)
                        {
                            FileLogger.Instance.logMessage(LogLevel.DEBUG, this, "Printing FBCF document");
                        }
                        PrintBackgroundFireCheckDocument(
                            this.madeDate,
                            "" + this.fullTicketNumber,
                            GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.CustomerNumber);
                    }
                    for (int i = 0; i < 2; i++) //BZ # 505
                    {
                        HandleWipeDriveCategory(currPawnLoan, ReceiptNumber);
                    }
                }
                IsRenewed = false;
                //End BZ # 115
            }
            return true;
        }

        private void HandleWipeDriveCategory(CustomerProductDataVO product, long rcptNumber)
        {
            foreach (Item pItem in product.Items)
            {
                if (pItem == null)
                    continue;

                if (FileLogger.Instance.IsLogDebug)
                {
                    FileLogger.Instance.logMessage(LogLevel.DEBUG, this, "Printing wipe drive document");
                }
                int pItemCatCode = pItem.CategoryCode;
                if (GlobalDataAccessor.Instance.DesktopSession.WipeDriveCategories != null && GlobalDataAccessor.Instance.DesktopSession.WipeDriveCategories.Count > 0)
                {
                    // if item category exists in the Wipe Drive category list
                    if (GlobalDataAccessor.Instance.DesktopSession.WipeDriveCategories.IndexOf(pItemCatCode) > -1)
                    {
                        PrintWipeDriveDocument(
                            "" + this.fullTicketNumber,
                            pItem.CategoryDescription,
                            GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.CustomerNumber,
                            rcptNumber);
                    }
                }
                else
                {
                    BasicExceptionHandler.Instance.AddException("Wipe Drive Categories are empty or invalid - Can't print document",
                                                                new ApplicationException());
                    FileLogger.Instance.logMessage(LogLevel.WARN, this, "Wipe Drive Categories are invalid - Can't print document");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool submitPawnTicketPrintJobsAndDisplayNewLoan()
        {
            bool couchIssue = false;
            lock (mutexIntObj)
            {
                string detailLine;

                int spaceLen;
                string spacer;
                DesktopSession cds = GlobalDataAccessor.Instance.DesktopSession;
                pawnTicketPrint();

                //Print receipts
                var data = new Dictionary<string, string>();

                PawnLoan currentLoan;
                if (cds.ActivePawnLoan != null)
                {
                    currentLoan = cds.ActivePawnLoan;
                }
                else
                {
                    currentLoan = cds.CurrentPawnLoan;
                }

                data.Add("store_short_name", STORE_NAME);
                data.Add("store_street_address", STORE_ADDRESS);
                data.Add("store_city_state_zip", STORE_CITY + ", " + STORE_STATE + " " + STORE_ZIP);
                data.Add("store_phone", formattedPhone);
                string curTime = this.madeTime.ToString();
                string curDate = this.madeDate.ToString("d", DateTimeFormatInfo.InvariantInfo);
                data.Add("f_date_and_time", curDate + " " + curTime);
                data.Add("f_cust_name", cds.ActiveCustomer.LastName + ", " + cds.ActiveCustomer.FirstName.Substring(0, 1));
                string receiptNumStr = "" + this.ReceiptNumber;
                if (!string.IsNullOrEmpty(receiptNumStr))
                {
                    data.Add("receipt_number", receiptNumStr);
                    data.Add("_BARDATA_H_02", receiptNumStr);
                }
                else
                {
                    data.Add("receipt_number", "******");
                    data.Add("_BARDATA_H_02", "000");
                }

                data.Add("emp_number", cds.UserName);
                string ticketAmount = currentLoan.Amount.ToString("C");
                string ticketNumb = "*****" + this.fullTicketNumber;
                //GJL 3/30/2010 - Pulling the length from a constant string is not
                //good coding practice - offending code has been commented out
                //spaceLen = MAX_LEN -("1 Pawn Loan: ".Length + ticketNumb.Length + ticketAmount.Length);
                const string receiptSpaceEntryStr = "1 Pawn Loan: ";
                spaceLen = MAX_LEN - (receiptSpaceEntryStr.Length + ticketNumb.Length + ticketAmount.Length);

                //GJL 3/30/2010 - Creating an empty string to pad is not
                //good coding practice (plus it is allocated every time the
                //app executes this code)..just use string.Empty
                //Offending code has been commented out
                //spacer = "".PadLeft(spaceLen, ' ');
                spacer = string.Empty.PadLeft(spaceLen, ' ');
                detailLine = "<B>" + receiptSpaceEntryStr + ticketNumb + spacer + ticketAmount;

                data.Add("DETAIL001", detailLine);
                data.Add("DETAIL002", "<L>");
                data.Add("DETAIL003", "<S>");

                //**************************************************************
                // this bit of manipulation is to get our dollar values right 
                // justified on line.  rjm 3-mar-2010
                //**************************************************************
                //GJL - 03/30/2010
                //Correcting offending code per comments above regarding
                //constant strings and empty strings - offending code has been
                //commented out
                const string grandTotalStr = "Grand Total";
                const string zeroDollarStr = "$0.00";
                const string amountRecStr = "Amount Received From Customer";
                const string amountPydStr = "Cash Paid To Customer";

                //spaceLen = MAX_LEN - ("Grand Total".Length + ticketAmount.Length);
                spaceLen = MAX_LEN - (grandTotalStr.Length + ticketAmount.Length);
                //spacer = "".PadLeft(spaceLen, ' ');
                spacer = string.Empty.PadLeft(spaceLen, ' ');
                //detailLine = "Grand Total" + spacer + ticketAmount;
                detailLine = grandTotalStr + spacer + ticketAmount;
                data.Add("DETAIL004", "<R>" + detailLine);

                //spaceLen = MAX_LEN - "Amount Received From Customer$0.00".Length;
                spaceLen = MAX_LEN - string.Concat(amountRecStr, zeroDollarStr).Length;
                //spacer = "".PadLeft(spaceLen, ' ');
                spacer = string.Empty.PadLeft(spaceLen, ' ');
                //detailLine = "Amount Received From Customer" + spacer + "$0.00";
                detailLine = amountRecStr + spacer + zeroDollarStr;
                data.Add("DETAIL005", "<R>" + detailLine);

                //spaceLen = MAX_LEN - ("Cash Paid To Customer".Length + ticketAmount.Length);
                spaceLen = MAX_LEN - (amountPydStr.Length + ticketAmount.Length);
                //spacer = "".PadLeft(spaceLen, ' ');
                spacer = string.Empty.PadLeft(spaceLen, ' ');
                //detailLine = "Cash Paid To Customer" + spacer + ticketAmount;
                detailLine = amountPydStr + spacer + ticketAmount;
                data.Add("DETAIL006", "<R>" + detailLine);

                //string s9 = "123456789A123456789B123456789C123456789D";
                data.Add("##TEMPLATEFILENAME##", "receiptTemplate.tpl");
                data.Add("##IPADDRESS01##", GlobalDataAccessor.Instance.DesktopSession.ReceiptPrinter.IPAddress);
                data.Add("##PORTNUMBER01##", GlobalDataAccessor.Instance.DesktopSession.ReceiptPrinter.Port.ToString());

                //Set number of copies
                //-- TODO: Set number of copies to 2 for pilot
                data.Add("##HOWMANYCOPIES##", "2");
                string fullFileName;
#if (!__MULTI__)
                if (SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled &&
                    GlobalDataAccessor.Instance.DesktopSession.ReceiptPrinter.IsValid)
                {
                    PrintingUtilities.PrintReceipt(data, false, out fullFileName);
                }
                else
                {
                    //Only for storage purposes since printing is disabled
                    PrintingUtilities.PrintReceipt(data, true, out fullFileName);
                }

                //Ensure full file name is valid
                if (!string.IsNullOrEmpty(fullFileName))
                {
                    //Get accessors
                    OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;
                    SecuredCouchConnector cC = GlobalDataAccessor.Instance.CouchDBConnector;
                    var pDoc = new CouchDbUtils.PawnDocInfo();

                    //Set document add calls
                    pDoc.UseCurrentShopDateTime = true;
                    pDoc.StoreNumber = cds.CurrentSiteId.StoreNumber;
                    pDoc.CustomerNumber = cds.ActiveCustomer.CustomerNumber;
                    pDoc.TicketNumber = (int)ticketNumber;
                    pDoc.DocumentType = Document.DocTypeNames.RECEIPT;
                    pDoc.DocFileName = fullFileName;
                    pDoc.ReceiptNumber = this.ReceiptNumber;

                    //Add this document to the pawn document registry and document storage
                    string errText;
                    if (!CouchDbUtils.AddPawnDocument(dA, cC, cds.UserName, ref pDoc, out errText))
                    {
                        couchIssue = true;

                        if (FileLogger.Instance.IsLogError)
                        {
                            FileLogger.Instance.logMessage(
                                LogLevel.ERROR, this,
                                "Could not store receipt in document storage: {0} - FileName: {1}", errText, fullFileName);
                        }

                        BasicExceptionHandler.Instance.AddException(
                            "Could not store receipt in document storage",
                            new ApplicationException("Could not store receipt in document storage: " + errText));
                    }
                }

                #region Print addendum if exists
                if (printAddendum)
                {
                    try
                    {
                        ReportObject rptObj = new ReportObject();
                        rptObj.ReportStore = GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreName;
                        rptObj.ReportStoreDesc = GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreAddress1 + "," +
                                                 GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreCityName + "," +
                                                 GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.State + "," +
                                                 GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreZipCode;
                        rptObj.ReportTitle = "Addendum";
                        rptObj.ReportTempFileFullName = SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseLogPath + "\\Addendum" + DateTime.Now.ToString("MMddyyyyhhmmssFFFFFFF") + ".pdf";

                        var pDocument = new Reports.PawnTicketAddendumDocument(PdfLauncher.Instance);
                        pDocument.ReportObject = rptObj;
                        pTicketAddendum.ticketNumber = (int)ticketNumber;
                        pDocument.CreateReport(pTicketAddendum);
                        var pDoc = new CouchDbUtils.PawnDocInfo();
                        var dA = GlobalDataAccessor.Instance.OracleDA;
                        var cC = GlobalDataAccessor.Instance.CouchDBConnector;

                        //Set document add calls
                        pDoc.UseCurrentShopDateTime = true;
                        pDoc.SetDocumentSearchType(CouchDbUtils.DocSearchType.TICKET_ADDENDUM);
                        pDoc.StoreNumber = cds.CurrentSiteId.StoreNumber;
                        pDoc.CustomerNumber = cds.ActiveCustomer.CustomerNumber;
                        pDoc.TicketNumber = (int)ticketNumber;
                        pDoc.DocumentType = Document.DocTypeNames.PDF;
                        pDoc.DocFileName = pDocument.ReportObject.ReportTempFileFullName;
                        pDoc.ReceiptNumber = this.ReceiptNumber;

                        //Add this document to the pawn document registry and document storage
                        string errText;
                        if (!CouchDbUtils.AddPawnDocument(dA, cC, cds.UserName, ref pDoc, out errText))
                        {
                            couchIssue = true;

                            if (FileLogger.Instance.IsLogError)
                                FileLogger.Instance.logMessage(LogLevel.ERROR, this,
                                                               "Could not store addendum document in document storage: {0} - FileName: {1}", errText, pDocument.ReportObject.ReportTempFileFullName);
                            BasicExceptionHandler.Instance.AddException(
                                "Could not store addendum document in document storage",
                                new ApplicationException("Could not store addendum document in document storage: " + errText));
                        }

                        if (SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled &&
                            cds.LaserPrinter.IsValid)
                        {

                            if (FileLogger.Instance.IsLogInfo)
                            {
                                FileLogger.Instance.logMessage(LogLevel.INFO, "ProcessTenderController", "Printing extension memo on {0}",
                                                               cds.LaserPrinter);
                            }
                            string errMsg = PrintingUtilities.printDocument(rptObj.ReportTempFileFullName, cds.LaserPrinter.IPAddress,
                                                                            cds.LaserPrinter.Port, 2);
                            if (errMsg.IndexOf("SUCCESS", StringComparison.OrdinalIgnoreCase) == -1)
                            {
                                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Cannot print extension memo");
                            }
                        }


                    }
                    catch (Exception ex)
                    {
                        if (FileLogger.Instance.IsLogError)
                            FileLogger.Instance.logMessage(LogLevel.ERROR, this,
                                                           "Could not print addendum document" + ex.Message);

                    }
                }
                #endregion

                #region Indiana-Police Card
                // New Pawn Loan + Customer Buys, Customers only
                bool policeCardNeeded = new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).IsPoliceCardNeededForStore(GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId);
                if (policeCardNeeded &&
                    GlobalDataAccessor.Instance.CurrentSiteId.State.Equals(States.Indiana) &&
                    cds.ActiveVendor == null)
                {
                    try
                    {
                        // show popup dialog
                        var pm = new Common.Libraries.Forms.ProcessingMessage("Police Card is Printing for Loan # " + ticketNumber.ToString(), 3000);
                        pm.ShowDialog();

                        var report = new IndianaPoliceCardReport();

                        //currentLoan.TicketNumber = (int)ticketNumber;
                        var custAddress = cds.ActiveCustomer.getHomeAddress();
                        if (custAddress == null && cds.ActiveCustomer.CustomerAddress.Count != 0)
                        {
                            custAddress = cds.ActiveCustomer.CustomerAddress[0];
                        }

                        DateTime dtStatus = new DateTime(((Common.Libraries.Objects.Pawn.UnderwritePawnLoanVO)currentLoan.ObjectUnderwritePawnLoanVO).MadeDate.Year,
                            ((Common.Libraries.Objects.Pawn.UnderwritePawnLoanVO)currentLoan.ObjectUnderwritePawnLoanVO).MadeDate.Month,
                            ((Common.Libraries.Objects.Pawn.UnderwritePawnLoanVO)currentLoan.ObjectUnderwritePawnLoanVO).MadeDate.Day);
                        dtStatus = dtStatus + ((Common.Libraries.Objects.Pawn.UnderwritePawnLoanVO)currentLoan.ObjectUnderwritePawnLoanVO).MadeTime;

                        report.BuildDocument(ticketNumber.ToString(), currentLoan.Items, cds.ActiveCustomer, cds.CurrentSiteId.StoreName, custAddress, cds.LastIdUsed, dtStatus, true);

                        if (SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled &&
                            cds.IndianaPoliceCardPrinter.IsValid)
                        {
                            if (!report.Print(cds.IndianaPoliceCardPrinter.IPAddress, (uint)cds.IndianaPoliceCardPrinter.Port))
                            {
                                if (FileLogger.Instance.IsLogError)
                                {
                                    String errorMessage = "Cannot print Indiana Police Card on " + cds.IndianaPoliceCardPrinter;
                                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, errorMessage);
                                    MessageBox.Show(errorMessage);
                                }
                            }
                        }

                        //---------------------------------------------------
                        // Place document into couch
                        const string policeCardFileName = "PoliceCard.txt";
                        var policeCardFilePath = SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseLogPath + "\\" + policeCardFileName;
                        report.Save(policeCardFilePath);

                        var pDoc = new CouchDbUtils.PawnDocInfo();
                        var dA = GlobalDataAccessor.Instance.OracleDA;
                        var cC = GlobalDataAccessor.Instance.CouchDBConnector;
                        //Set document add calls
                        pDoc.UseCurrentShopDateTime = true;
                        pDoc.SetDocumentSearchType(CouchDbUtils.DocSearchType.POLICE_CARD);
                        pDoc.StoreNumber = cds.CurrentSiteId.StoreNumber;
                        pDoc.CustomerNumber = cds.ActiveCustomer.CustomerNumber;
                        pDoc.TicketNumber = (int)ticketNumber;
                        pDoc.DocumentType = Document.DocTypeNames.TEXT;
                        pDoc.DocFileName = policeCardFilePath;
                        //Add this document to the pawn document registry and document storage
                        string errText;
                        if (!CouchDbUtils.AddPawnDocument(dA, cC, cds.UserName, ref pDoc, out errText))
                        {
                            couchIssue = true;

                            if (FileLogger.Instance.IsLogError)
                            {
                                FileLogger.Instance.logMessage(LogLevel.ERROR, this,
                                    "Could not store police card document in document storage: {0} - FileName: {1}", errText, policeCardFilePath);
                            }

                            BasicExceptionHandler.Instance.AddException(
                                "Could not store police card document in document storage",
                                new ApplicationException("Could not store police card document in document storage: " + errText));
                        }
                    }
                    catch (Exception ex)
                    {
                        if (FileLogger.Instance.IsLogError)
                        {
                            FileLogger.Instance.logMessage(
                                LogLevel.ERROR,
                                this,
                                "Could not print Police Card" + ex.Message);
                        }
                    }
                }
                #endregion
#endif

                if (couchIssue)
                {
                    MessageBox.Show(
                        "Transaction completed but the document could not be saved.  \n" +
                        "Please call Shop System Support.  \n",
                        "Application Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Stop);
                }
            }


            return (true);
        }

        //****************************************************************
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //****************************************************************
        private bool generateLoanReceipt(ProcessTenderMode mode, string userName,
                                         CustomerVO curCustomer, List<PawnLoan> pawnLoans,
                                         ReceiptDetailsVO rVO)
        {
            string detailLine;
            int spaceLen;
            string spacer;

            if (mode == ProcessTenderMode.NEWLOAN)
            {
                //Handled with another method (FOR NOW)
                // submitPawnTicketPrintJobsAndDisplayNewLoan()
                return (false);
            }

            //Handle other situations here (void / service / etc)
            lock (mutexIntObj)
            {
                //Print receipts
                var data = new Dictionary<string, string>();
                data.Add("store_short_name", STORE_NAME);
                data.Add("store_street_address", STORE_ADDRESS);
                data.Add("store_city_state_zip", STORE_CITY + ", " + STORE_STATE + " " + STORE_ZIP);
                data.Add("store_phone", formattedPhone);
                string curTime = ShopDateTime.Instance.ShopShortTime;
                string curDate = ShopDateTime.Instance.ShopDate.ToString("d", DateTimeFormatInfo.InvariantInfo);
                data.Add("f_date_and_time", curDate + " " + curTime);
                data.Add("f_cust_name", curCustomer.LastName + ", " + curCustomer.FirstName.Substring(0, 1));
                if (!string.IsNullOrEmpty(rVO.ReceiptNumber))
                {
                    data.Add("receipt_number", rVO.ReceiptNumber);
                    data.Add("_BARDATA_H_02", rVO.ReceiptNumber);
                }
                else
                {
                    data.Add("receipt_number", "******");
                    data.Add("_BARDATA_H_02", "000");
                }
                data.Add("emp_number", userName);
                if (mode == ProcessTenderMode.VOIDLOAN)
                {
                    data.Add("##TEMPLATEFILENAME##", "ReceiptTemplate.tpl");
                    PawnLoan pLoan = pawnLoans.First();
                    decimal voidAmount = 0.0M;
                    voidAmount = Utilities.GetDecimalValue(rVO.RefAmounts.First(), 0);
                    string ticketAmount = voidAmount.ToString("C");
                    //string ticketNumb = "*****" + pLoan.TicketNumber;

                    //GJL 3/30/2010 Code on line below computing spaceLen must be changed!!
                    spaceLen = MAX_LEN - ("1 Pawn Loan ".Length +
                                          rVO.RefEvents.First().Length +
                                          " VOID: ".Length +
                                          pLoan.TicketNumber.ToString().Length +
                                          ticketAmount.Length);
                    //GJL 3/30/2010 Code on line below computing spacer must be changed!!
                    spacer = "".PadLeft(spaceLen, ' ');

                    //GJL 3/30/2010 Code on line below computing detailLine must be changed!!
                    detailLine = "1 Pawn Loan " + rVO.RefEvents.First() + " VOID: " + pLoan.TicketNumber + spacer + ticketAmount;
                    data.Add("DETAIL001", "<B>" + detailLine);
                    data.Add("DETAIL002", "<L>");
                    data.Add("DETAIL003", "<S>");

                    //GJL 3/30/2010 Code on line below computing spaceLen must be changed!!
                    spaceLen = MAX_LEN - ("VOID Amount".Length + ticketAmount.Length);
                    //GJL 3/30/2010 Code on line below computing spacer must be changed!!
                    spacer = "".PadLeft(spaceLen, ' ');
                    //GJL 3/30/2010 Code on line below computing detailLine must be changed!!
                    detailLine = "VOID Amount" + spacer + ticketAmount;
                    data.Add("DETAIL004", "<R>" + detailLine);
                }
                else if (mode == ProcessTenderMode.SERVICELOAN)
                {
                    int detailCount = 1;
                    int loanCount = 1;
                    decimal totalAmount = 0.00M;
                    decimal changeDueAmount = 0.00M;
                    decimal amountRecvd = 0.00M;

                    string detailKey = string.Empty;
                    decimal totalPickupAmount = 0.00M;
                    decimal totalRenewalAmount = 0.00M;
                    decimal totalExtendAmount = 0.00M;
                    decimal totalPaydownAmount = 0.00M;
                    decimal totalPartialPaymentAmount = 0.00M;

                    bool pickup = false;
                    bool renew = false;
                    bool extend = false;
                    bool paydown = false;
                    bool ppmnt = false;

                    data.Add("##TEMPLATEFILENAME##", "srvcReceiptTemplate.tpl");
                    List<ReceiptItems> pickupStatusArraylist = new List<ReceiptItems>();
                    List<ReceiptItems> renewalStatusArraylist = new List<ReceiptItems>();
                    List<ReceiptItems> extensionStatusArraylist = new List<ReceiptItems>();
                    List<ReceiptItems> paydownStatusArraylist = new List<ReceiptItems>();
                    List<ReceiptItems> partPmntStatusArraylist = new List<ReceiptItems>();

                    foreach (PawnLoan pLoan in pawnLoans)
                    {
                        if (pLoan == null)
                            continue;
                        switch (pLoan.TempStatus)
                        {
                            case StateStatus.P:
                                {
                                    totalPickupAmount += pLoan.PickupAmount;
                                    pickup = true;
                                    BuildDetailLine(pickupStatusArraylist, pLoan.PickupAmount,
                                                    "Pawn Loan: Pickup",
                                                    pLoan.TicketNumber.ToString());
                                    break;
                                }
                            case StateStatus.RN:
                                {
                                    totalRenewalAmount += pLoan.RenewalAmount;
                                    renew = true;
                                    BuildDetailLine(renewalStatusArraylist, pLoan.RenewalAmount,
                                                    "Pawn Loan: Renewal",
                                                    pLoan.TicketNumber.ToString());
                                    break;
                                }

                            case StateStatus.E:
                                {
                                    totalExtendAmount += pLoan.ExtensionAmount;
                                    extend = true;
                                    BuildDetailLine(extensionStatusArraylist, pLoan.ExtensionAmount,
                                                    "Pawn Loan: Extension",
                                                    pLoan.TicketNumber.ToString());
                                    break;
                                }
                            case StateStatus.PD:
                                {
                                    totalPaydownAmount += pLoan.PaydownAmount;
                                    paydown = true;
                                    BuildDetailLine(paydownStatusArraylist, pLoan.PaydownAmount,
                                                    "Pawn Loan: Paydown",
                                                    pLoan.TicketNumber.ToString());
                                    break;
                                }
                            case StateStatus.PPMNT:
                                {
                                    decimal partialPaymentAmount = pLoan.PartialPayments.Find(p => p.Status_cde == "New").PMT_AMOUNT;
                                    totalPartialPaymentAmount += partialPaymentAmount;
                                    ppmnt = true;
                                    BuildDetailLine(paydownStatusArraylist, partialPaymentAmount,
                                                    "Pawn Loan: Partial Payment",
                                                    pLoan.TicketNumber.ToString());
                                    break;

                                }
                            default:
                                {
                                    throw new ArgumentOutOfRangeException(
                                        string.Format(
                                            "{0} is out of range for print receipt in process tender - not handled yet!",
                                            pLoan.TempStatus.ToString()));
                                }
                        }
                        ++loanCount;
                    }
                    int loanDataCount = 1;
                    if (pickup)
                    {
                        BuildSubTotalLine(data, totalPickupAmount,
                                          "Total Pickup Amount", ref detailCount, pickupStatusArraylist, ref loanDataCount);
                    }

                    if (extend)
                    {
                        BuildSubTotalLine(data, totalExtendAmount,
                                          "Total Extension Amount", ref detailCount, extensionStatusArraylist, ref loanDataCount);
                    }

                    if (renew)
                    {
                        BuildSubTotalLine(data, totalRenewalAmount,
                                          "Total Renewal Amount", ref detailCount, renewalStatusArraylist, ref loanDataCount);
                    }

                    if (paydown)
                    {
                        BuildSubTotalLine(data, totalPaydownAmount,
                                          "Total Paydown Amount", ref detailCount, paydownStatusArraylist, ref loanDataCount);
                    }
                    if (ppmnt)
                    {
                        BuildSubTotalLine(data, totalPartialPaymentAmount,
                                          "Total Partial Payment Amount", ref detailCount, partPmntStatusArraylist, ref loanDataCount);

                    }

                    //Update detail key
                    detailKey = string.Format(
                        "DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                    ++detailCount;
                    data.Add(detailKey, "<L>");

                    //Update detail key
                    detailKey = string.Format(
                        "DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                    ++detailCount;
                    data.Add(detailKey, "<S>");

                    //Compute total
                    totalAmount = totalPickupAmount + totalRenewalAmount + totalExtendAmount + totalPaydownAmount + totalPartialPaymentAmount;

                    detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                    spaceLen = MAX_LEN - ("Grand Total".Length + totalAmount.ToString().Length) - 1;
                    spacer = "".PadLeft(spaceLen, ' ');
                    detailLine = "Grand Total" + spacer + totalAmount.ToString("C");
                    data.Add(detailKey, "<R>" + detailLine);
                    ++detailCount;

                    //Update detail key
                    amountRecvd = GlobalDataAccessor.Instance.DesktopSession.CashTenderFromCustomer;
                    detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                    //GJL 3/30/2010 Code on line below computing spaceLen must be changed!!
                    spaceLen = MAX_LEN - ("Amount Received From Customer".Length + amountRecvd.ToString().Length) - 1;
                    //GJL 3/30/2010 Code on line below computing spacer must be changed!!
                    spacer = "".PadLeft(spaceLen, ' ');
                    //GJL 3/30/2010 Code on line below computing detailLine must be changed!!
                    detailLine = "Amount Received From Customer" + spacer + "$" + amountRecvd;
                    data.Add(detailKey, "<R>" + detailLine);
                    ++detailCount;

                    detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                    data.Add(detailKey, "<R>                                 ________");
                    ++detailCount;

                    changeDueAmount = amountRecvd == 0 ? 0 : amountRecvd - totalAmount;
                    detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                    //GJL 3/30/2010 Code on line below computing spaceLen must be changed!!
                    spaceLen = MAX_LEN - ("Change".Length + changeDueAmount.ToString().Length) - 1;
                    //GJL 3/30/2010 Code on line below computing spacer must be changed!!
                    spacer = "".PadLeft(spaceLen, ' ');
                    detailLine = "Change" + spacer + "$" + changeDueAmount;
                    data.Add(detailKey, "<R>" + detailLine);
                    ++detailCount;
                }

                //Set template name
                data.Add("##IPADDRESS01##", GlobalDataAccessor.Instance.DesktopSession.ReceiptPrinter.IPAddress);
                data.Add("##PORTNUMBER01##", GlobalDataAccessor.Instance.DesktopSession.ReceiptPrinter.Port.ToString());

                //Set number of receipt copies
                //-- TODO: Changed to two receipts for pilot
                //
                data.Add("##HOWMANYCOPIES##", "2");
#if (!__MULTI__)
                string fullFileName = string.Empty;
                if (SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled &&
                    GlobalDataAccessor.Instance.DesktopSession.ReceiptPrinter.IsValid)
                {
                    PrintingUtilities.PrintReceipt(
                        data,
                        false,
                        out fullFileName);
                }
                else
                {
                    //Only for storage purposes since printing is disabled
                    PrintingUtilities.PrintReceipt(data, true, out fullFileName);
                }

                //generateLoanReceipt(mode, userName,curCustomer, pawnLoans, rVO);
                //Ensure full file name is valid
                if (!string.IsNullOrEmpty(fullFileName))
                {
                    //Get accessors
                    var cds = GlobalDataAccessor.Instance.DesktopSession;
                    var dA = GlobalDataAccessor.Instance.OracleDA;
                    var cC = GlobalDataAccessor.Instance.CouchDBConnector;
                    foreach (var pLoan in pawnLoans)
                    {
                        var pDoc = new CouchDbUtils.PawnDocInfo();

                        //Set document add calls
                        pDoc.UseCurrentShopDateTime = true;
                        pDoc.StoreNumber = cds.CurrentSiteId.StoreNumber;
                        pDoc.CustomerNumber = cds.ActiveCustomer.CustomerNumber;
                        pDoc.DocumentType = Document.DocTypeNames.RECEIPT;
                        pDoc.DocFileName = fullFileName;
                        pDoc.TicketNumber = pLoan.TicketNumber;
                        long recNumL = 0L;
                        if (long.TryParse(rVO.ReceiptNumber, out recNumL))
                        {
                            pDoc.ReceiptNumber = recNumL;
                        }

                        //Add this document to the pawn document registry and document storage
                        string errText;
                        if (!CouchDbUtils.AddPawnDocument(dA, cC, userName, ref pDoc, out errText))
                        {
                            if (FileLogger.Instance.IsLogError)
                                FileLogger.Instance.logMessage(LogLevel.ERROR, this,
                                                               "Could not store receipt in document storage: {0} - FileName: {1}", errText, fullFileName);
                            BasicExceptionHandler.Instance.AddException(
                                "Could not store receipt in document storage",
                                new ApplicationException("Could not store receipt in document storage: " + errText));
                        }
                    }
                }
#endif
            }
            return (true);
        }

        public bool GenerateSaleReceipt(ProcessTenderMode mode, string userName,
                                        CustomerVO curCustomer, SaleVO currentSale,
                                        string vendorName,
                                        ReceiptDetailsVO rVO)
        {
            CustomerVO activeCustomer;
            string detailLine;
            int spaceLen;
            string spacer;
            bool containsFirearm = false;
            bool customerDataNotNull = false;
            lock (mutexIntObj)
            {
                //Print receipts
                try
                {
                    if (string.IsNullOrEmpty(GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.LastName) && GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.CustomerNumber != null)
                    {
                        activeCustomer =
                                CustomerProcedures.getCustomerDataByCustomerNumber(GlobalDataAccessor.Instance.DesktopSession,
                                                                                   GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.
                                                                                           CustomerNumber);
                        GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer = activeCustomer;
                    }
                    string custFirstName = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer != null && GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.CustomerNumber != string.Empty ? GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.FirstName : "";//.Substring(0, 1);
                    string custLastName = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer != null && GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.CustomerNumber != string.Empty ? GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.LastName : GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.EntityName;
                    string retailSaleHeading = "1 Retail Sale - MSR # " + GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.TicketNumber;
                    string retailRefundHeading1 = "1 Retail Sale - Refund # " + GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.TicketNumber;
                    string retailRefundVoidHeading = "Orignial MSR # - " + currentSale.RefNumber;
                    string retailVoidHeading1 = "1 Retail Sale - VOID MSR # " + currentSale.TicketNumber;
                    var data = new Dictionary<string, string>();
                    data.Add("store_short_name", STORE_NAME);
                    data.Add("store_street_address", STORE_ADDRESS);
                    data.Add("store_city_state_zip", STORE_CITY + ", " + STORE_STATE + " " + STORE_ZIP);
                    data.Add("store_phone", formattedPhone);
                    string curTime = ShopDateTime.Instance.ShopShortTime;
                    string curDate = ShopDateTime.Instance.ShopDate.ToString("d", DateTimeFormatInfo.InvariantInfo);
                    data.Add("f_date_and_time", curDate + " " + curTime);

                    //here check to see if total is less than 10 bucks, skip this section if it is
                    if (!string.IsNullOrEmpty(custLastName) && !string.IsNullOrEmpty(custFirstName))
                    {
                        data.Add("f_cust_name", "Customer: " + custLastName + ", " + custFirstName.Substring(0, 1));
                        customerDataNotNull = true;
                    }
                    else if (!string.IsNullOrEmpty(custLastName))
                    {
                        data.Add("f_cust_name", "Customer: " + custLastName);
                        customerDataNotNull = true;
                    }
                    else if (GlobalDataAccessor.Instance.DesktopSession.ActiveVendor != null && !string.IsNullOrEmpty(GlobalDataAccessor.Instance.DesktopSession.ActiveVendor.Name))
                    {
                        data.Add("f_cust_name", "Vendor: " + GlobalDataAccessor.Instance.DesktopSession.ActiveVendor.Name);
                        customerDataNotNull = true;

                    }
                    else
                    {
                        data.Add("f_cust_name", string.Empty);
                    }

                    //need to get Receipt number
                    if (!string.IsNullOrEmpty(rVO.ReceiptNumber))
                    {
                        data.Add("receipt_number", rVO.ReceiptNumber);
                        data.Add("_BARDATA_H_02", rVO.ReceiptNumber);
                    }
                    else
                    {
                        data.Add("receipt_number", "******");
                        data.Add("_BARDATA_H_02", "000");
                    }
                    data.Add("emp_number", userName);
                    int detailCount = 1;
                    decimal amountRecvd = 0.00M;
                    string detailKey = string.Empty;
                    decimal totalSaleAmount = 0.00M;
                    if (mode == ProcessTenderMode.RETAILSALE)
                        data.Add("##TEMPLATEFILENAME##", "retailReceiptTemplate.tpl");
                    else
                        data.Add("##TEMPLATEFILENAME##", "retailRefundTemplate.tpl");
                    List<ReceiptItems> saleItemsArraylist = new List<ReceiptItems>();
                    foreach (RetailItem saleItem in currentSale.RetailItems)
                    {
                        int qty = 0;
                        if (saleItem == null)
                            continue;
                        if (saleItem.Quantity > 0)
                            qty = saleItem.Quantity;
                        else
                            qty = 1;
                        if (mode == ProcessTenderMode.RETAILSALE)
                        {
                            totalSaleAmount += (saleItem.NegotiatedPrice * qty) - (saleItem.CouponAmount + saleItem.ProratedCouponAmount);
                        }
                        else
                        {
                            totalSaleAmount += (saleItem.RetailPrice * qty) - (saleItem.CouponAmount + saleItem.ProratedCouponAmount);
                        }
                        AddSaleDetailLineToList(saleItemsArraylist, saleItem, mode);
                        if (saleItem.IsGun)
                            containsFirearm = true;
                    }
                    if (mode == ProcessTenderMode.RETAILREFUND)
                        totalSaleAmount = GlobalDataAccessor.Instance.DesktopSession.TenderTransactionAmount.TotalAmount;
                    if (mode == ProcessTenderMode.RETAILSALE)
                    {
                        detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                        spaceLen = MAX_LEN - (retailSaleHeading.Length) - 1;
                        spacer = "".PadLeft(spaceLen, ' ');
                        detailLine = retailSaleHeading + spacer;
                        data.Add(detailKey, "<R>" + detailLine);
                        ++detailCount;
                    }
                    else if (mode == ProcessTenderMode.RETAILREFUND || mode == ProcessTenderMode.RETAILVOID)
                    {
                        if (mode == ProcessTenderMode.RETAILREFUND)
                        {
                            detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                            spaceLen = MAX_LEN - (retailRefundHeading1.Length) - 1;
                            spacer = "".PadLeft(spaceLen, ' ');
                            detailLine = retailRefundHeading1 + spacer;
                            data.Add(detailKey, "<R>" + detailLine);
                            ++detailCount;

                            detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                            spaceLen = MAX_LEN - (retailRefundVoidHeading.Length) - 1;
                            spacer = "".PadLeft(spaceLen, ' ');
                            detailLine = retailRefundVoidHeading + spacer;
                            data.Add(detailKey, "<R>" + detailLine);
                            ++detailCount;
                        }
                        else
                        {
                            detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                            spaceLen = MAX_LEN - (retailVoidHeading1.Length) - 1;
                            spacer = "".PadLeft(spaceLen, ' ');
                            detailLine = retailVoidHeading1 + spacer;
                            data.Add(detailKey, "<R>" + detailLine);
                            ++detailCount;
                        }
                    }

                    WriteEmptyLineToReceipt(ref data, ref detailCount);

                    int salesItemsCounter = 1;
                    if (totalSaleAmount > 0.00M)
                    {
                        WriteSaleDetailLines(data, ref detailCount, saleItemsArraylist, ref salesItemsCounter, currentSale, mode);
                    }
                    string retailPriceText = "Retail Price:";
                    string salesTaxText = "Sales Tax:";
                    string backGroundCheckText = "Background Check Fee:";
                    string shippingAndHandlingText = "Shipping And Handling:";
                    string totalRetailText = "Total Retail:";
                    string receivedFromText = "Received from Customer:";
                    decimal salesTaxAmt = 0.0m;
                    decimal backgroundCheckFeeAmt = 0.0m;
                    decimal totalRetailAmt = 0.0m;
                    decimal shippingAndHandlingAmt = 0.0m;
                    string retailAmtToPrint = string.Empty;
                    string retailCouponAmountString = string.Empty;
                    string taxAmtToPrint = string.Empty;
                    string shippingAndHandlingAmtToPrint = string.Empty;
                    string backGroundAmtToPrint = string.Empty;
                    string totalRetailAmtToPrint = string.Empty;
                    string receivedFromAmtToPrint = string.Empty;
                    string spacer1 = string.Empty;
                    string spacer2 = string.Empty;
                    string refNumberCredit = string.Empty;
                    string refNumberStoreCredit = string.Empty;
                    string refNumberDebit = string.Empty;
                    string refNumberCheck = string.Empty;
                    string refundSubAmountDescription = "Refund Amount:";
                    string refundTotalDescription = "Total Refund:";
                    string voidSubAmountDescription = "Void Amount:";
                    string voidTotalDescription = "Total Void:";
                    string couponDescription = "Coupon:";
                    decimal tenderByCouponAmount = 0.0m;
                    decimal refundSubTotalAmount = 0.0m;
                    decimal refundTaxAmount = 0.0m;
                    string refundCouponAmountString = string.Empty;
                    string refundTotalAmountString = string.Empty;
                    string refundSubTotalAmountString = string.Empty;
                    string refundTaxAmountString = string.Empty;

                    string refundedByCashAmountString = string.Empty;
                    string refundedByCreditCardAmountString = string.Empty;
                    string refundedByDebitCardAmountString = string.Empty;
                    string refundedByCheckAmountString = string.Empty;
                    string changeDueToCustomerString = string.Empty;
                    string changeDueToCustomerDescription = "Change Due:";
                    //string saleCreditCardDescription = "Credit Card";
                    //string saleDebitCardDescription = "Debit Card";
                    decimal tenderByCashInAmount = 0.0m;
                    decimal tenderByCashOutAmount = 0.0m;
                    decimal tenderByCreditCardAmount = 0.0m;
                    decimal tenderByDebitCardAmount = 0.0m;
                    decimal tenderByCheckAmount = 0.0m;
                    decimal tenderByStoreCreditAmount = 0.0m;
                    decimal totalTenderAmount = 0.0m;
                    foreach (TenderEntryVO tEntry in GlobalDataAccessor.Instance.DesktopSession.TenderData)
                    {
                        string tenderType = tEntry.TenderType.ToString();
                        decimal amount = tEntry.Amount;
                        if (tenderType == TenderTypes.CASHOUT.ToString())
                        {
                            tenderByCashOutAmount += Utilities.GetDecimalValue(amount, 0);
                        }
                        if (tenderType == TenderTypes.CASHIN.ToString())
                        {
                            tenderByCashInAmount += Utilities.GetDecimalValue(amount, 0);
                        }
                        if (tenderType == TenderTypes.CREDITCARD.ToString())
                        {
                            refNumberCredit = tEntry.ReferenceNumber;
                            tenderByCreditCardAmount += Utilities.GetDecimalValue(amount, 0);
                        }
                        if (tenderType == TenderTypes.DEBITCARD.ToString())
                        {
                            refNumberDebit = tEntry.ReferenceNumber;
                            tenderByDebitCardAmount += Utilities.GetDecimalValue(amount, 0);
                        }
                        if (tenderType == TenderTypes.CHECK.ToString())
                        {
                            refNumberCheck = tEntry.ReferenceNumber;
                            tenderByCheckAmount += Utilities.GetDecimalValue(amount, 0);
                        }
                        if (tenderType == TenderTypes.COUPON.ToString())
                        {
                            tenderByCouponAmount += Utilities.GetDecimalValue(amount, 0);
                        }
                        if (tenderType == TenderTypes.STORECREDIT.ToString())
                        {
                            tenderByStoreCreditAmount += Utilities.GetDecimalValue(amount, 0);
                            refNumberStoreCredit = tEntry.ReferenceNumber;
                        }
                    }
                    totalTenderAmount = tenderByCashInAmount + tenderByCreditCardAmount + tenderByDebitCardAmount + tenderByCheckAmount + tenderByStoreCreditAmount;
                    if (currentSale.Fees.Count > 0)
                    {
                        foreach (Fee fee in currentSale.Fees)
                        {
                            if (fee.FeeType == FeeTypes.BACKGROUND_CHECK_FEE)
                            {
                                backgroundCheckFeeAmt = fee.Value; //fee.value or fee.OriginalAmount ?
                                break;
                            }
                        }
                    }
                    //totalSaleAmount = totalSaleAmount - tenderByCouponAmount;
                    if (mode == ProcessTenderMode.RETAILSALE)
                    {
                        WriteEmptyLineToReceipt(ref data, ref detailCount);

                        if (GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.SalesTaxPercentage > 0.0m)
                            salesTaxAmt = Math.Round((GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.SalesTaxPercentage / 100) * totalSaleAmount, 2);
                        shippingAndHandlingAmt = currentSale.ShippingHandlingCharges;
                        totalRetailAmt = totalSaleAmount + salesTaxAmt + shippingAndHandlingAmt + backgroundCheckFeeAmt;
                        detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                        retailAmtToPrint = FormatAmountToPrint(totalSaleAmount);

                        spaceLen = MAX_LEN - (retailPriceText.Length + retailAmtToPrint.Length);
                        spacer1 = "".PadLeft(spaceLen - 5, ' ');
                        spacer2 = "".PadLeft(5, ' ');
                        detailLine = spacer1 + retailPriceText + spacer2 + retailAmtToPrint;
                        data.Add(detailKey, "<R>" + detailLine);
                        ++detailCount;

                        //Coupon here
                        /*
                        if (tenderByCouponAmount > 0.0m)
                        {
                            detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                            retailCouponAmountString = FormatAmountToPrint(tenderByCouponAmount);
                            spaceLen = MAX_LEN - (couponDescription.Length + retailCouponAmountString.Length + 2);
                            spacer1 = "".PadLeft(spaceLen - 5, ' ');
                            spacer2 = "".PadLeft(5, ' ');
                            detailLine = spacer1 + couponDescription + spacer2 + "(" + retailCouponAmountString + ")";
                            data.Add(detailKey, "<R>" + detailLine);
                            ++detailCount;
                        }*/

                        if (shippingAndHandlingAmt > 0.0m)
                        {
                            detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                            shippingAndHandlingAmtToPrint = FormatAmountToPrint(shippingAndHandlingAmt);
                            spaceLen = MAX_LEN - (shippingAndHandlingText.Length + shippingAndHandlingAmtToPrint.Length);
                            spacer1 = "".PadLeft(spaceLen - 5, ' ');
                            spacer2 = "".PadLeft(5, ' ');
                            detailLine = spacer1 + shippingAndHandlingText + spacer2 + shippingAndHandlingAmtToPrint;
                            data.Add(detailKey, "<R>" + detailLine);
                            ++detailCount;
                        }

                        if (salesTaxAmt >= 0.0m)
                        {
                            detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                            taxAmtToPrint = FormatAmountToPrint(salesTaxAmt);
                            spaceLen = MAX_LEN - (salesTaxText.Length + taxAmtToPrint.Length);
                            spacer1 = "".PadLeft(spaceLen - 5, ' ');
                            spacer2 = "".PadLeft(5, ' ');
                            detailLine = spacer1 + salesTaxText + spacer2 + taxAmtToPrint;
                            data.Add(detailKey, "<R>" + detailLine);
                            ++detailCount;
                        }
                        if (backgroundCheckFeeAmt > 0.0m)
                        {
                            detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                            backGroundAmtToPrint = FormatAmountToPrint(backgroundCheckFeeAmt);
                            spaceLen = MAX_LEN - (backGroundCheckText.Length + backGroundAmtToPrint.Length);
                            spacer1 = "".PadLeft(spaceLen - 5, ' ');
                            spacer2 = "".PadLeft(5, ' ');
                            detailLine = spacer1 + backGroundCheckText + spacer2 + backGroundAmtToPrint;
                            data.Add(detailKey, "<R>" + detailLine);
                            ++detailCount;
                        }
                        detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                        totalRetailAmtToPrint = FormatAmountToPrint(totalRetailAmt);
                        spaceLen = MAX_LEN - (totalRetailText.Length + totalRetailAmtToPrint.Length);
                        spacer1 = "".PadLeft(spaceLen - 5, ' ');
                        spacer2 = "".PadLeft(5, ' ');
                        detailLine = spacer1 + totalRetailText + spacer2 + totalRetailAmtToPrint;
                        data.Add(detailKey, "<R>" + detailLine);
                        ++detailCount;

                        WriteEmptyLineToReceipt(ref data, ref detailCount);

                        amountRecvd = (tenderByCashInAmount + Math.Abs(GlobalDataAccessor.Instance.DesktopSession.CashTenderToCustomer)) + tenderByCheckAmount + tenderByCreditCardAmount + tenderByDebitCardAmount + tenderByStoreCreditAmount;
                        amountRecvd = Utilities.GetDecimalValue(amountRecvd, 0);
                        detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                        receivedFromAmtToPrint = FormatAmountToPrint(amountRecvd);//need to change amt here to actual amt received once that is determined thru cash tender
                        spaceLen = MAX_LEN - (receivedFromText.Length + receivedFromAmtToPrint.Length);
                        spacer1 = "".PadLeft(spaceLen - 5, ' ');
                        spacer2 = "".PadLeft(5, ' ');
                        detailLine = spacer1 + receivedFromText + spacer2 + receivedFromAmtToPrint;
                        data.Add(detailKey, "<R>" + detailLine);
                        ++detailCount;

                        //Change due to customer
                        if (Math.Abs(GlobalDataAccessor.Instance.DesktopSession.CashTenderToCustomer) > 0.0m)
                        {
                            detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                            changeDueToCustomerString = FormatAmountToPrint(Math.Abs(GlobalDataAccessor.Instance.DesktopSession.CashTenderToCustomer));
                            spaceLen = MAX_LEN - (changeDueToCustomerDescription.Length + changeDueToCustomerString.Length);
                            spacer1 = "".PadLeft(spaceLen - 5, ' ');
                            spacer2 = "".PadLeft(5, ' ');
                            detailLine = spacer1 + changeDueToCustomerDescription + spacer2 + changeDueToCustomerString;
                            data.Add(detailKey, "<R>" + detailLine);
                            ++detailCount;
                        }
                        //start to show tenderTypes here
                        /*
                        if (tenderByCheckAmount > 0.0m)
                        {
                        if (!string.IsNullOrEmpty(refNumberCheck))
                        refNumberCheck = "(" + refNumberCheck + ")";
                        WriteTenderType(ref data, tenderByCheckAmount, refNumberCheck, ref detailCount, checkText);
                        }
                        if (tenderByCashInAmount > 0.0m)
                        {
                        detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                        refundedByCashAmountString = FormatAmountToPrint(tenderByCashInAmount);
                        spaceLen = MAX_LEN - (paidByText.Length + cashText.Length + refundedByCashAmountString.Length);
                        spacer1 = "".PadLeft(spaceLen - 5, ' ');
                        spacer2 = "".PadLeft(5, ' ');
                        detailLine = spacer2 + paidByText + cashText + spacer1 + refundedByCashAmountString;
                        data.Add(detailKey, "<R>" + detailLine);
                        ++detailCount;
                        }
                        if (tenderByCreditCardAmount > 0.0m)
                        {
                        if (!string.IsNullOrEmpty(refNumberCredit))
                        refNumberCredit = "(" + refNumberCredit + ")";
                        WriteTenderType(ref data, tenderByCreditCardAmount, refNumberCredit, ref detailCount, saleCreditCardDescription);
                        }
                        //Debit card
                        //var byDC = currentSale.TenderDataDetails.Where(tenderType => tenderType.TenderType == TenderTypes.DEBITCARD.ToString());
                        if (tenderByDebitCardAmount > 0.0m)
                        {
                        if (!string.IsNullOrEmpty(refNumberDebit))
                        refNumberDebit = "(" + refNumberDebit + ")";
                        WriteTenderType(ref data, tenderByDebitCardAmount, refNumberDebit, ref detailCount, saleDebitCardDescription);
                        }
                        if (tenderByStoreCreditAmount > 0.0m)
                        {
                        if (!string.IsNullOrEmpty(refNumberStoreCredit))
                        refNumberStoreCredit = "(" + refNumberStoreCredit + ")";
                        WriteTenderType(ref data, tenderByStoreCreditAmount, refNumberStoreCredit, ref detailCount, labelStoreCredit);
                        }*/
                    }
                    else if (mode == ProcessTenderMode.RETAILREFUND || mode == ProcessTenderMode.RETAILVOID)
                    {
                        //here add Cash Tender stuff
                        //var byCash = currentSale.TenderDataDetails.Where(tenderType => tenderType.TenderType == TenderTypes.CASHOUT.ToString());
                        if (mode == ProcessTenderMode.RETAILREFUND)
                        {
                            refundSubTotalAmount = GlobalDataAccessor.Instance.DesktopSession.TenderTransactionAmount.SubTotalAmount;
                            if (GlobalDataAccessor.Instance.DesktopSession.TenderTransactionAmount.SalesTaxPercentage > 0.0m)
                            {
                                decimal taxPercent = GlobalDataAccessor.Instance.DesktopSession.TenderTransactionAmount.SalesTaxPercentage;
                                refundTaxAmount = Math.Round((taxPercent / 100) * refundSubTotalAmount, 2);
                            }
                            totalRetailAmt = refundSubTotalAmount + refundTaxAmount;
                            detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                            refundSubTotalAmountString = FormatAmountToPrint(refundSubTotalAmount);

                            //Refund Amount
                            spaceLen = MAX_LEN - (refundSubAmountDescription.Length + refundSubTotalAmountString.Length);
                            spacer1 = "".PadLeft(spaceLen - 5, ' ');
                            spacer2 = "".PadLeft(5, ' ');
                            detailLine = spacer1 + refundSubAmountDescription + spacer2 + refundSubTotalAmountString;
                            data.Add(detailKey, "<R>" + detailLine);
                            ++detailCount;
                        }
                        else
                        {
                            refundSubTotalAmount = totalSaleAmount;
                            decimal salesTaxp = GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.SalesTaxPercentage;
                            if (GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.SalesTaxPercentage > 0.0m)
                                refundTaxAmount = Math.Round(refundSubTotalAmount * salesTaxp / 100, 2);
                            totalRetailAmt = refundSubTotalAmount + refundTaxAmount;
                            detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                            refundSubTotalAmountString = FormatAmountToPrint(refundSubTotalAmount);

                            //Refund Amount
                            spaceLen = MAX_LEN - (voidSubAmountDescription.Length + refundSubTotalAmountString.Length);
                            spacer1 = "".PadLeft(spaceLen - 5, ' ');
                            spacer2 = "".PadLeft(5, ' ');
                            detailLine = spacer1 + voidSubAmountDescription + spacer2 + refundSubTotalAmountString;
                            data.Add(detailKey, "<R>" + detailLine);
                            ++detailCount;
                        }

                        //Sales Tax
                        if (refundTaxAmount > 0.0m)
                        {
                            detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                            refundTaxAmountString = FormatAmountToPrint(refundTaxAmount);
                            spaceLen = MAX_LEN - (salesTaxText.Length + refundTaxAmountString.Length);
                            spacer1 = "".PadLeft(spaceLen - 5, ' ');
                            spacer2 = "".PadLeft(5, ' ');
                            detailLine = spacer1 + salesTaxText + spacer2 + refundTaxAmountString;
                            data.Add(detailKey, "<R>" + detailLine);
                            ++detailCount;
                        }

                        //Coupon here
                        /*if (tenderByCouponAmount > 0.0m)
                        {
                            detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                            refundCouponAmountString = FormatAmountToPrint(tenderByCouponAmount);
                            spaceLen = MAX_LEN - (couponDescription.Length + refundCouponAmountString.Length + 2);
                            spacer1 = "".PadLeft(spaceLen - 5, ' ');
                            spacer2 = "".PadLeft(5, ' ');
                            detailLine = spacer1 + couponDescription + spacer2 + "(" + refundCouponAmountString + ")";
                            data.Add(detailKey, "<R>" + detailLine);
                            ++detailCount;
                        }*/

                        if (mode == ProcessTenderMode.RETAILREFUND)
                        {
                            //total Refund Here
                            detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                            refundTotalAmountString = FormatAmountToPrint(totalRetailAmt);
                            spaceLen = MAX_LEN - (refundTotalDescription.Length + refundTotalAmountString.Length);
                            spacer1 = "".PadLeft(spaceLen - 5, ' ');
                            spacer2 = "".PadLeft(5, ' ');
                            detailLine = spacer1 + refundTotalDescription + spacer2 + refundTotalAmountString;
                            data.Add(detailKey, "<R>" + detailLine);
                            ++detailCount;
                        }
                        else
                        {
                            detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                            refundTotalAmountString = FormatAmountToPrint(totalRetailAmt);
                            spaceLen = MAX_LEN - (voidTotalDescription.Length + refundTotalAmountString.Length);
                            spacer1 = "".PadLeft(spaceLen - 5, ' ');
                            spacer2 = "".PadLeft(5, ' ');
                            detailLine = spacer1 + voidTotalDescription + spacer2 + refundTotalAmountString;
                            data.Add(detailKey, "<R>" + detailLine);
                            ++detailCount;
                        }
                        //start here to show tenderTypes
                        /*
                        if (tenderByCashOutAmount > 0.0m)
                        {
                        detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                        refundedByCashAmountString = FormatAmountToPrint(tenderByCashOutAmount);
                        spaceLen = MAX_LEN - (refundedByCashDescription.Length + refundedByCashAmountString.Length);
                        spacer1 = "".PadLeft(spaceLen - 5, ' ');
                        spacer2 = "".PadLeft(5, ' ');
                        detailLine = spacer1 + refundedByCashDescription + spacer2 + refundedByCashAmountString;
                        data.Add(detailKey, "<R>" + detailLine);
                        ++detailCount;
                        }
                        //credit card
                        //var byCC = currentSale.TenderDataDetails.Where(tenderType => tenderType.TenderType == TenderTypes.CREDITCARD.ToString());
                        if (tenderByCreditCardAmount > 0.0m)
                        {
                        if (!string.IsNullOrEmpty(refNumberCredit))
                        refNumberCredit = "(" + refNumberCredit + ")";
                        WriteTenderType(ref data, tenderByCreditCardAmount, refNumberCredit, ref detailCount, refundedByCreditCardDescription);
                        }
                        //Debit card
                        //var byDC = currentSale.TenderDataDetails.Where(tenderType => tenderType.TenderType == TenderTypes.DEBITCARD.ToString());
                        if (tenderByDebitCardAmount > 0.0m)
                        {
                        if (!string.IsNullOrEmpty(refNumberDebit))
                        refNumberDebit = "(" + refNumberDebit + ")";
                        WriteTenderType(ref data, tenderByDebitCardAmount, refNumberDebit, ref detailCount, refundedByDebitCardDescription);
                        }
                        //Check card
                        //var byCheck = currentSale.TenderDataDetails.Where(tenderType => tenderType.TenderType == TenderTypes.CHECK.ToString());
                        if (tenderByCheckAmount > 0.0m)
                        {
                        if (!string.IsNullOrEmpty(refNumberCheck))
                        refNumberCheck = "(" + refNumberCheck + ")";
                        WriteTenderType(ref data, tenderByCheckAmount, refNumberCheck, ref detailCount, refundedByCheckDescription);
                        }
                        if (tenderByStoreCreditAmount > 0.0m)
                        {
                        if (!string.IsNullOrEmpty(refNumberStoreCredit))
                        refNumberCredit = "(" + refNumberStoreCredit + ")";
                        WriteTenderType(ref data, tenderByStoreCreditAmount, refNumberStoreCredit, ref detailCount, labelStoreCredit);
                        }*/
                        //end tendertypes
                    }
                    WriteLineInReceipt(ref data, ref detailCount);
                    WriteEmptyLineToReceipt(ref data, ref detailCount);
                    ParseTenderTypes(ref data, ref detailCount, mode);
                    WriteLineInReceipt(ref data, ref detailCount);

                    //Set template name
                    //data.Add("##IPADDRESS01##", "192.168.106.33");
                    data.Add("##IPADDRESS01##", GlobalDataAccessor.Instance.DesktopSession.ReceiptPrinter.IPAddress);
                    data.Add("##PORTNUMBER01##", GlobalDataAccessor.Instance.DesktopSession.ReceiptPrinter.Port.ToString());

                    //Set number of receipt copies
                    //-- TODO: Changed to two receipts for pilot
                    //
                    data.Add("##HOWMANYCOPIES##", "2");

                    //here add signature line if totalAmount is Greater than 10
                    if (totalRetailAmt > 10m)
                    {
                        ++detailCount;
                        detailKey = string.Format(
                            "DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                        ++detailCount;
                        data.Add(detailKey, "<S>");

                        detailKey = string.Format("Customer_Employee_Signatures{0}", detailCount.ToString().PadLeft(3, '0'));
                        ++detailCount;
                        data.Add(detailKey, "<R>X _____________________________________");

                        detailKey = string.Format("Customer_Employee_Signatures{0}", detailCount.ToString().PadLeft(3, '0'));
                        ++detailCount;
                        data.Add(detailKey, "<R>Customer Signature");

                        if (mode == ProcessTenderMode.RETAILREFUND || mode == ProcessTenderMode.RETAILVOID)
                        {
                            detailKey = string.Format(
                                "Customer_Employee_Signatures{0}", detailCount.ToString().PadLeft(3, '0'));
                            ++detailCount;
                            data.Add(detailKey, "<S>");

                            detailKey = string.Format("Customer_Employee_Signatures{0}", detailCount.ToString().PadLeft(3, '0'));
                            ++detailCount;
                            data.Add(detailKey, "<R>X ____________________________________");

                            detailKey = string.Format("Customer_Employee_Signatures{0}", detailCount.ToString().PadLeft(3, '0'));
                            ++detailCount;
                            data.Add(detailKey, "<R>Employee Signature");
                        }
                    }
                    else if (totalRetailAmt <= 9.99m && !containsFirearm && !customerDataNotNull && (tenderByCashInAmount > 0.0m
                    && tenderByCheckAmount <= 0.0m
                    && tenderByCreditCardAmount <= 0.0m
                    && tenderByDebitCardAmount <= 0.0m
                    && tenderByStoreCreditAmount <= 0.0m)
                    )
                    {
                        if (data.ContainsKey("f_cust_name"))
                            data["f_cust_name"] = string.Empty;
                    }

#if (!__MULTI__)
                    string fullFileName = string.Empty;
                    if (mode == ProcessTenderMode.RETAILSALE || mode == ProcessTenderMode.RETAILREFUND)
                    {
                        CallPrintReceipt(data, out fullFileName);
                        CallPrintReceipt(data, out fullFileName);
                    }
                    else
                    {
                        CallPrintReceipt(data, out fullFileName);
                    }
                    /*GenerateSaleReceipt( mode,  userName,
                    curCustomer,  currentSale,
                    vendorName,
                    rVO);*/
                    //Ensure full file name is valid
                    if (!string.IsNullOrEmpty(fullFileName))
                    {
                        //Get accessors
                        DesktopSession cds = GlobalDataAccessor.Instance.DesktopSession;
                        OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;
                        SecuredCouchConnector cC = GlobalDataAccessor.Instance.CouchDBConnector;
                        //foreach (PurchaseVO purchase in purchases)
                        //{
                        var pDoc = new CouchDbUtils.PawnDocInfo();

                        //Set document add calls
                        pDoc.UseCurrentShopDateTime = true;
                        pDoc.StoreNumber = cds.CurrentSiteId.StoreNumber;
                        pDoc.CustomerNumber = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.CustomerNumber;
                        pDoc.DocumentType = Document.DocTypeNames.RECEIPT;
                        pDoc.DocFileName = fullFileName;
                        pDoc.TicketNumber = currentSale.TicketNumber;
                        long recNumL = 0L;
                        if (long.TryParse(rVO.ReceiptNumber, out recNumL))
                        {
                            pDoc.ReceiptNumber = recNumL;
                        }

                        //Add this document to the pawn document registry and document storage
                        string errText;
                        if (!CouchDbUtils.AddPawnDocument(dA, cC, userName, ref pDoc, out errText))
                        {
                            if (FileLogger.Instance.IsLogError)
                                FileLogger.Instance.logMessage(LogLevel.ERROR, this,
                                                               "Could not store receipt in document storage: {0} - FileName: {1}", errText, fullFileName);
                            BasicExceptionHandler.Instance.AddException(
                                "Could not store receipt in document storage",
                                new ApplicationException("Could not store receipt in document storage: " + errText));
                        }
                    }
#endif
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Problem Creating Receipt");
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this,
                                                   "Sale Receipt ", ex.Message);

                    return (false);
                }
            }
            return (true);
        }

        private void WriteEmptyLineToReceipt(ref Dictionary<string, string> data, ref int detailCount)
        {
            string detailKey = string.Format(
                "DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
            ++detailCount;
            data.Add(detailKey, "<S>");
        }

        private void WriteLineInReceipt(ref Dictionary<string, string> data, ref int detailCount)
        {
            string detailKey = string.Format(
                "DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
            ++detailCount;
            data.Add(detailKey, "<R>_______________________________________");
        }

        private bool ParseTenderTypes(ref Dictionary<string, string> data, ref int detailCount, ProcessTenderMode mode)
        {
            bool tenderTypeFound = false;
            string labelStoreCredit = "STORE CREDIT";
            string creditCard = "CREDIT CARD";
            string debitCard = "DEBIT CARD";
            string labelCheck = "CHECK";
            string labelCash = "CASH";
            string labelCoupon = "COUPON:";
            string paymentOrRefund = "Paid by: ";
            if (mode == ProcessTenderMode.LAYPAYMENTREF || mode == ProcessTenderMode.RETAILREFUND || mode == ProcessTenderMode.RETAILVOIDREFUND || mode == ProcessTenderMode.LAYPAYMENTREFVOID)
            {
                paymentOrRefund = "Refunded by: ";
            }

            foreach (TenderEntryVO tEntry in GlobalDataAccessor.Instance.DesktopSession.TenderData)
            {
                string tenderType = tEntry.TenderType.ToString();
                decimal amount = tEntry.Amount;
                string referenceNumber = tEntry.ReferenceNumber;
                if (!string.IsNullOrEmpty(referenceNumber))
                    referenceNumber = "(" + referenceNumber + ")";

                if (tenderType == TenderTypes.CASHOUT.ToString())
                {
                    /*detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                    tenderByCashOutAmountToString = FormatAmountToPrint(amount);
                    spaceLen = MAX_LEN - (labelPaidBy.Length + labelCash.Length + tenderByCashOutAmountToString.Length);
                    spacer1 = "".PadLeft(spaceLen - 5, ' ');
                    spacer2 = "".PadLeft(5, ' ');
                    detailLine = spacer2 + labelPaidBy + labelCash + spacer1 + tenderByCashOutAmountToString;
                    data.Add(detailKey, "<R>" + detailLine);
                    ++detailCount;*/
                    WriteTenderType(ref data, amount, referenceNumber, ref detailCount, paymentOrRefund + labelCash);
                    tenderTypeFound = true;
                }
                if (tenderType == TenderTypes.CASHIN.ToString())
                {
                    /*detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                    tenderByCashInAmountToString = FormatAmountToPrint(amount);
                    spaceLen = MAX_LEN - (labelPaidBy.Length + labelCash.Length + tenderByCashInAmountToString.Length);
                    spacer1 = "".PadLeft(spaceLen - 5, ' ');
                    spacer2 = "".PadLeft(5, ' ');
                    detailLine = spacer2 + labelPaidBy + labelCash + spacer1 + tenderByCashInAmountToString;
                    data.Add(detailKey, "<R>" + detailLine);
                    ++detailCount;*/
                    WriteTenderType(ref data, amount, referenceNumber, ref detailCount, paymentOrRefund + labelCash);
                    tenderTypeFound = true;
                }
                if (tenderType == TenderTypes.CREDITCARD.ToString())
                {
                    WriteTenderType(ref data, amount, referenceNumber, ref detailCount, paymentOrRefund + creditCard);
                }
                if (tenderType == TenderTypes.DEBITCARD.ToString())
                {
                    WriteTenderType(ref data, amount, referenceNumber, ref detailCount, paymentOrRefund + debitCard);
                    tenderTypeFound = true;
                }
                if (tenderType == TenderTypes.CHECK.ToString())
                {
                    WriteTenderType(ref data, amount, referenceNumber, ref detailCount, paymentOrRefund + labelCheck);
                    tenderTypeFound = true;
                }
                /*if (tenderType == TenderTypes.COUPON.ToString())
                {
                    detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                    tenderByCouponAmountToString = FormatAmountToPrint(amount);
                    spaceLen = MAX_LEN - (labelCoupon.Length + tenderByCouponAmountToString.Length);
                    spacer1 = "".PadLeft(spaceLen - 5, ' ');
                    spacer2 = "".PadLeft(3, ' ');
                    detailLine = spacer1 + labelCoupon + spacer2 + "(" + tenderByCouponAmountToString + ")";
                    data.Add(detailKey, "<R>" + detailLine);
                    ++detailCount;
                    WriteTenderType(ref data, amount, referenceNumber, ref detailCount, paymentOrRefund + labelCoupon);
                    tenderTypeFound = true;
                }*/
                if (tenderType == TenderTypes.STORECREDIT.ToString())
                {
                    WriteTenderType(ref data, amount, referenceNumber, ref detailCount, paymentOrRefund + labelStoreCredit);
                }
            }
            return tenderTypeFound;
        }

        private void WriteTenderType(ref Dictionary<string, string> data,
                                     decimal tenderAmount,
                                     string refNumber,
                                     ref int detailCount,
                                     string tenderDescription)
        {
            string amountToWriteToString = string.Empty;
            int lenText;
            string detailKey;
            int spaceLen;
            string detailLine = string.Empty;
            string spacer1 = string.Empty;
            string spacer2 = string.Empty;
            //if (!string.IsNullOrEmpty(refNumber))
            //refNumber = "(" + refNumber + ")";
            detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
            amountToWriteToString = FormatAmountToPrint(tenderAmount);
            //spaceLen = MAX_LEN - (paidByText.Length + saleDebitCardDescription.Length + refNumberDebit.Length + refundedByDebitCardAmountString.Length);
            lenText = tenderDescription.Length + refNumber.Length;
            if (lenText > 23)
            {
                spaceLen = MAX_LEN - (tenderDescription.Length);
                spacer1 = "".PadLeft(spaceLen - 5, ' ');
                spacer2 = "".PadLeft(5, ' ');
                detailLine = spacer2 + tenderDescription;
                data.Add(detailKey, "<R>" + detailLine);
                ++detailCount;

                spaceLen = MAX_LEN - (refNumber.Length + amountToWriteToString.Length);
                spacer1 = "".PadLeft(spaceLen - 7, ' ');
                spacer2 = "".PadLeft(7, ' ');
                detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                detailLine = spacer2 + refNumber + spacer1 + amountToWriteToString;
                data.Add(detailKey, "<R>" + detailLine);
                ++detailCount;
            }
            else
            {
                spaceLen = MAX_LEN - (tenderDescription.Length + refNumber.Length + amountToWriteToString.Length);
                if (spaceLen >= 5)
                    spacer1 = "".PadLeft(spaceLen - 5, ' ');
                spacer2 = "".PadLeft(5, ' ');
                detailLine = spacer2 + tenderDescription + refNumber + spacer1 + amountToWriteToString;
                data.Add(detailKey, "<R>" + detailLine);
                ++detailCount;
            }
        }

        public bool GenerateLayawayReceipt(ProcessTenderMode mode, string userName,
                                           CustomerVO curCustomer, LayawayVO currentLayaway,
                                           string vendorName,
                                           ReceiptDetailsVO rVO)
        {
            string detailLine;
            int spaceLen;
            string spacer;
            lock (mutexIntObj)
            {
                //Print receipts
                try
                {
                    string custFirstName = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.FirstName;//.Substring(0, 1);
                    string custLastName = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.LastName;

                    //layaway variables
                    string layawaySaleHeading = "1 Layaway Payment";
                    string layawayNumberHeading = "Lay #: ";

                    //layaway void variables
                    string layawayVoidHeading = "1 Layaway-VOID";
                    string spacer1 = string.Empty;
                    string spacer2 = string.Empty;
                    var data = new Dictionary<string, string>();
                    data.Add("store_short_name", STORE_NAME);
                    data.Add("store_street_address", STORE_ADDRESS);
                    data.Add("store_city_state_zip", STORE_CITY + ", " + STORE_STATE + " " + STORE_ZIP);
                    data.Add("store_phone", formattedPhone);
                    string curTime = ShopDateTime.Instance.ShopShortTime;
                    string curDate = ShopDateTime.Instance.ShopDate.ToString("d", DateTimeFormatInfo.InvariantInfo);
                    data.Add("f_date_and_time", curDate + " " + curTime);
                    if (!string.IsNullOrEmpty(custLastName) && !string.IsNullOrEmpty(custFirstName))
                        data.Add("f_cust_name", "Customer: " + custLastName + ", " + custFirstName.Substring(0, 1));
                    else
                        data.Add("f_cust_name", string.Empty);
                    //need to get Receipt number
                    if (!string.IsNullOrEmpty(rVO.ReceiptNumber) && Convert.ToInt32(rVO.ReceiptNumber) > 0)
                    {
                        data.Add("receipt_number", rVO.ReceiptNumber);
                        data.Add("_BARDATA_H_02", rVO.ReceiptNumber);
                    }
                    else
                    {
                        data.Add("receipt_number", "******");
                        data.Add("_BARDATA_H_02", "000");
                    }
                    data.Add("emp_number", userName);
                    int detailCount = 1;
                    decimal amountRecvd = 0.00M;
                    string detailKey = string.Empty;
                    decimal totalSaleAmount = 0.00M;
                    data.Add("##TEMPLATEFILENAME##", "LayawayTemplate.tpl");
                    List<ReceiptItems> layawayItemsArraylist = new List<ReceiptItems>();
                    foreach (RetailItem layawayItem in currentLayaway.RetailItems)
                    {
                        int qty = 0;
                        if (layawayItem == null)
                            continue;
                        if (layawayItem.Quantity > 0)
                            qty = layawayItem.Quantity;
                        else
                            qty = 1;
                        if (mode == ProcessTenderMode.LAYAWAY)
                            totalSaleAmount += (layawayItem.NegotiatedPrice * qty) - (layawayItem.ProratedCouponAmount + layawayItem.CouponAmount);
                        else
                            totalSaleAmount += (layawayItem.RetailPrice * qty) - (layawayItem.ProratedCouponAmount + layawayItem.CouponAmount);
                        AddSaleDetailLineToList(layawayItemsArraylist, layawayItem, mode);
                    }
                    if (mode != ProcessTenderMode.LAYAWAYVOID)
                    {
                        detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                        spaceLen = MAX_LEN - (layawaySaleHeading.Length) - 1;
                        spacer = "".PadLeft(spaceLen, ' ');
                        detailLine = layawaySaleHeading + spacer;
                        data.Add(detailKey, "<R>" + detailLine);
                        ++detailCount;

                        detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                        spaceLen = MAX_LEN - (layawayNumberHeading.Length + currentLayaway.TicketNumber.ToString().Length + 2);
                        spacer1 = "".PadLeft(spaceLen - 5, ' ');
                        detailLine = layawayNumberHeading + currentLayaway.TicketNumber.ToString() + spacer1;
                        data.Add(detailKey, "<R>" + detailLine);
                        ++detailCount;
                    }
                    else
                    {
                        detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                        spaceLen = MAX_LEN - (layawayVoidHeading.Length) - 1;
                        spacer = "".PadLeft(spaceLen, ' ');
                        detailLine = layawayVoidHeading + spacer;
                        data.Add(detailKey, "<R>" + detailLine);
                        ++detailCount;
                    }

                    detailKey = string.Format(
                        "DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                    ++detailCount;
                    data.Add(detailKey, "<S>");

                    int salesItemsCounter = 1;
                    if (totalSaleAmount > 0.00M)
                    {
                        WriteSaleDetailLines(data, ref detailCount, layawayItemsArraylist, ref salesItemsCounter, currentLayaway, mode);
                    }
                    string layawaySubtotalString = "Layaway Sub-Total:";
                    string layawayTotalString = "Layaway Total:";
                    string salesTaxText = "Sales Tax:";
                    string layawayFeeDescription = "Layaway Fee:";
                    string backGroundCheckText = "Background Check Fee:";
                    string shippingAndHandlingText = "Shipping And Handling:";
                    string receivedFromText = "Received from Customer:";
                    decimal salesTaxAmt = 0.0m;
                    decimal backgroundCheckFeeAmt = 0.0m;
                    decimal totalRetailAmt = 0.0m;
                    decimal shippingAndHandlingAmt = 0.0m;
                    string retailAmtToPrint = string.Empty;
                    string retailCouponAmountString = string.Empty;
                    string taxAmtToPrint = string.Empty;
                    string shippingAndHandlingAmtToPrint = string.Empty;
                    string backGroundAmtToPrint = string.Empty;
                    string refNumberCredit = string.Empty;
                    string refNumberDebit = string.Empty;
                    string refNumberCheck = string.Empty;
                    decimal layawayFeeAmt = 0.0m;
                    string layawayFeeAmtToString = string.Empty;
                    string totalRetailAmtToPrint = string.Empty;
                    string receivedFromAmtToPrint = string.Empty;
                    string couponDescription = "Coupon:";
                    decimal tenderByCouponAmount = 0.0m;
                    string changeDueToCustomerString = string.Empty;
                    string changeDueToCustomerDescription = "Change Due:";
                    //string saleCreditCardDescription = "Credit Card";
                    //string saleDebitCardDescription = "Debit Card";
                    decimal tenderByCashInAmount = 0.0m;
                    decimal tenderByCashOutAmount = 0.0m;
                    decimal tenderByCreditCardAmount = 0.0m;
                    decimal tenderByDebitCardAmount = 0.0m;
                    decimal tenderByCheckAmount = 0.0m;
                    decimal tenderByStoreCreditAmount = 0.0m;
                    string refNumberStoreCredit = string.Empty;

                    foreach (TenderEntryVO tEntry in GlobalDataAccessor.Instance.DesktopSession.TenderData)
                    {
                        string tenderType = tEntry.TenderType.ToString();
                        decimal amount = tEntry.Amount;
                        if (tenderType == TenderTypes.CASHOUT.ToString())
                        {
                            tenderByCashOutAmount += Utilities.GetDecimalValue(amount, 0);
                        }
                        if (tenderType == TenderTypes.CASHIN.ToString())
                        {
                            tenderByCashInAmount += Utilities.GetDecimalValue(amount, 0);
                        }
                        if (tenderType == TenderTypes.CREDITCARD.ToString())
                        {
                            refNumberCredit = tEntry.ReferenceNumber;
                            tenderByCreditCardAmount += Utilities.GetDecimalValue(amount, 0);
                        }
                        if (tenderType == TenderTypes.DEBITCARD.ToString())
                        {
                            refNumberDebit = tEntry.ReferenceNumber;
                            tenderByDebitCardAmount += Utilities.GetDecimalValue(amount, 0);
                        }
                        if (tenderType == TenderTypes.CHECK.ToString())
                        {
                            refNumberCheck = tEntry.ReferenceNumber;
                            tenderByCheckAmount += Utilities.GetDecimalValue(amount, 0);
                        }
                        if (tenderType == TenderTypes.COUPON.ToString())
                        {
                            tenderByCouponAmount += Utilities.GetDecimalValue(amount, 0);
                        }
                        if (tenderType == TenderTypes.STORECREDIT.ToString())
                        {
                            tenderByStoreCreditAmount += Utilities.GetDecimalValue(amount, 0);
                            refNumberStoreCredit = tEntry.ReferenceNumber;
                        }
                    }

                    WriteEmptyLineToReceipt(ref data, ref detailCount);
                    if (GlobalDataAccessor.Instance.DesktopSession.ActiveLayaway.SalesTaxPercentage > 0.0m)
                        salesTaxAmt = Math.Round((GlobalDataAccessor.Instance.DesktopSession.ActiveLayaway.SalesTaxPercentage / 100) * totalSaleAmount, 2);
                    //var backGroundfee = currentSale.Fees.Where(fee => fee.FeeType == FeeTypes.BACKGROUND_CHECK_FEE);
                    //backgroundCheckFeeAmt = backGroundfee.First().OriginalAmount;
                    foreach (Fee fee in currentLayaway.Fees)
                    {
                        if (fee.FeeType == FeeTypes.BACKGROUND_CHECK_FEE)
                        {
                            backgroundCheckFeeAmt = fee.Value; //fee.value or fee.OriginalAmount ?
                            break;
                        }
                    }

                    foreach (Fee fee in currentLayaway.Fees)
                    {
                        if (fee.FeeType == FeeTypes.SERVICE || fee.FeeType == FeeTypes.INTEREST)
                        {
                            layawayFeeAmt += fee.Value; //fee.value or fee.OriginalAmount ?
                            // break;
                        }
                    }
                    shippingAndHandlingAmt = currentLayaway.ShippingHandlingCharges;
                    totalRetailAmt = totalSaleAmount + salesTaxAmt + shippingAndHandlingAmt + backgroundCheckFeeAmt + layawayFeeAmt - tenderByCouponAmount;
                    detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));

                    retailAmtToPrint = FormatAmountToPrint(totalSaleAmount);
                    spaceLen = MAX_LEN - (layawaySubtotalString.Length + retailAmtToPrint.Length);
                    spacer1 = "".PadLeft(spaceLen - 5, ' ');
                    spacer2 = "".PadLeft(5, ' ');
                    //spacer = "".PadLeft(spaceLen/2, ' ');
                    detailLine = spacer1 + layawaySubtotalString + spacer2 + retailAmtToPrint;
                    data.Add(detailKey, "<R>" + detailLine);
                    ++detailCount;

                    //Coupon here
                    /*if (tenderByCouponAmount > 0.0m)
                    {
                        detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                        retailCouponAmountString = FormatAmountToPrint(tenderByCouponAmount);
                        spaceLen = MAX_LEN - (couponDescription.Length + retailCouponAmountString.Length);
                        spacer1 = "".PadLeft(spaceLen - 5, ' ');
                        spacer2 = "".PadLeft(3, ' ');
                        detailLine = spacer1 + couponDescription + spacer2 + "(" + retailCouponAmountString + ")";
                        data.Add(detailKey, "<R>" + detailLine);
                        ++detailCount;
                    }*/

                    if (shippingAndHandlingAmt > 0.0m)
                    {
                        detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                        shippingAndHandlingAmtToPrint = FormatAmountToPrint(shippingAndHandlingAmt);
                        spaceLen = MAX_LEN - (shippingAndHandlingText.Length + shippingAndHandlingAmtToPrint.Length);
                        spacer1 = "".PadLeft(spaceLen - 5, ' ');
                        spacer2 = "".PadLeft(5, ' ');
                        //spacer = "".PadLeft(spaceLen/2, ' ');
                        detailLine = spacer1 + shippingAndHandlingText + spacer2 + shippingAndHandlingAmtToPrint;
                        data.Add(detailKey, "<R>" + detailLine);
                        ++detailCount;
                    }

                    if (salesTaxAmt > 0.0m)
                    {
                        detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                        taxAmtToPrint = FormatAmountToPrint(salesTaxAmt);
                        spaceLen = MAX_LEN - (salesTaxText.Length + taxAmtToPrint.Length);
                        spacer1 = "".PadLeft(spaceLen - 5, ' ');
                        spacer2 = "".PadLeft(5, ' ');
                        //spacer = "".PadLeft(spaceLen/2, ' ');
                        detailLine = spacer1 + salesTaxText + spacer2 + taxAmtToPrint;
                        data.Add(detailKey, "<R>" + detailLine);
                        ++detailCount;
                    }

                    if (backgroundCheckFeeAmt > 0.0m)
                    {
                        detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                        backGroundAmtToPrint = FormatAmountToPrint(backgroundCheckFeeAmt);
                        spaceLen = MAX_LEN - (backGroundCheckText.Length + backGroundAmtToPrint.Length);
                        spacer1 = "".PadLeft(spaceLen - 5, ' ');
                        spacer2 = "".PadLeft(5, ' ');
                        //spacer = "".PadLeft(spaceLen/2, ' ');
                        detailLine = spacer1 + backGroundCheckText + spacer2 + backGroundAmtToPrint;
                        data.Add(detailKey, "<R>" + detailLine);
                        ++detailCount;
                    }
                    if (layawayFeeAmt > 0.0m)
                    {
                        detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                        layawayFeeAmtToString = FormatAmountToPrint(layawayFeeAmt);
                        spaceLen = MAX_LEN - (layawayFeeDescription.Length + layawayFeeAmtToString.Length);
                        spacer1 = "".PadLeft(spaceLen - 5, ' ');
                        spacer2 = "".PadLeft(5, ' ');
                        //spacer = "".PadLeft(spaceLen/2, ' ');
                        detailLine = spacer1 + layawayFeeDescription + spacer2 + layawayFeeAmtToString;
                        data.Add(detailKey, "<R>" + detailLine);
                        ++detailCount;
                    }
                    detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                    totalRetailAmtToPrint = FormatAmountToPrint(totalRetailAmt);
                    spaceLen = MAX_LEN - (layawayTotalString.Length + totalRetailAmtToPrint.Length);
                    spacer1 = "".PadLeft(spaceLen - 5, ' ');
                    spacer2 = "".PadLeft(5, ' ');
                    //spacer = "".PadLeft(spaceLen/2, ' ');
                    detailLine = spacer1 + layawayTotalString + spacer2 + totalRetailAmtToPrint;
                    data.Add(detailKey, "<R>" + detailLine);
                    ++detailCount;

                    detailKey = string.Format(
                        "DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                    ++detailCount;
                    data.Add(detailKey, "<S>");
                    //foreach (TenderEntryVO tEntry in GlobalDataAccessor.Instance.DesktopSession.TenderData)
                    //{
                    //    amountRecvd += tEntry.Amount;
                    //}
                    //Update detail key
                    //amountRecvd = currentSale.TenderDataDetails.Sum(amtRvd => Utilities.GetDecimalValue(amtRvd, 0));
                    //foreach (TenderData td in currentSale.TenderDataDetails)
                    //{
                    //    amountRecvd += td.TenderAmount;
                    //}
                    amountRecvd = (tenderByCashInAmount + Math.Abs(GlobalDataAccessor.Instance.DesktopSession.CashTenderToCustomer)) + tenderByCheckAmount + tenderByCreditCardAmount + tenderByDebitCardAmount;
                    amountRecvd = Utilities.GetDecimalValue(amountRecvd, 0);
                    detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                    receivedFromAmtToPrint = FormatAmountToPrint(amountRecvd);//need to change amt here to actual amt received once that is determined thru cash tender
                    spaceLen = MAX_LEN - (receivedFromText.Length + receivedFromAmtToPrint.Length);
                    spacer1 = "".PadLeft(spaceLen - 5, ' ');
                    spacer2 = "".PadLeft(5, ' ');
                    //spacer = "".PadLeft(spaceLen/2, ' ');
                    detailLine = spacer1 + receivedFromText + spacer2 + receivedFromAmtToPrint;
                    data.Add(detailKey, "<R>" + detailLine);
                    ++detailCount;

                    //Change due to customer
                    if (Math.Abs(GlobalDataAccessor.Instance.DesktopSession.CashTenderToCustomer) > 0.0m)
                    {
                        //refundedByCheckAmount = Convert.ToDecimal(byCheck.First().TenderAmount);
                        detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                        changeDueToCustomerString = FormatAmountToPrint(Math.Abs(GlobalDataAccessor.Instance.DesktopSession.CashTenderToCustomer));
                        spaceLen = MAX_LEN - (changeDueToCustomerDescription.Length + changeDueToCustomerString.Length);
                        spacer1 = "".PadLeft(spaceLen - 5, ' ');
                        spacer2 = "".PadLeft(5, ' ');
                        detailLine = spacer1 + changeDueToCustomerDescription + spacer2 + changeDueToCustomerString;
                        data.Add(detailKey, "<R>" + detailLine);
                        ++detailCount;
                    }

                    WriteLineInReceipt(ref data, ref detailCount);
                    //Change due to customer
                    /*if (GlobalDataAccessor.Instance.DesktopSession.CashTenderToCustomer > 0.0m)
                    {
                    //refundedByCheckAmount = Convert.ToDecimal(byCheck.First().TenderAmount);
                    detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                    changeDueToCustomerString = FormatAmountToPrint(GlobalDataAccessor.Instance.DesktopSession.CashTenderToCustomer);
                    spaceLen = MAX_LEN - (changeDueToCustomerDescription.Length + changeDueToCustomerString.Length);
                    spacer1 = "".PadLeft(spaceLen - 5, ' ');
                    spacer2 = "".PadLeft(5, ' ');
                    detailLine = spacer2 + changeDueToCustomerDescription + spacer1 + "(" + changeDueToCustomerString + ")";
                    data.Add(detailKey, "<R>" + detailLine);
                    ++detailCount;
                    }*/

                    //now add lines for tender types
                    /*if (tenderByCheckAmount > 0.0m)
                    {
                    //refundedByCheckAmount = Convert.ToDecimal(byCheck.First().TenderAmount);
                    if (!string.IsNullOrEmpty(refNumberCheck))
                    refNumberCheck = "(" + refNumberCheck + ")";
                    detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                    refundedByCheckAmountString = FormatAmountToPrint(tenderByCheckAmount);
                    spaceLen = MAX_LEN - (paidByText.Length + checkText.Length + refNumberCheck.Length + refundedByCheckAmountString.Length);
                    spacer1 = "".PadLeft(spaceLen - 5, ' ');
                    spacer2 = "".PadLeft(5, ' ');
                    detailLine = spacer2 + paidByText + checkText + refNumberCheck + spacer1 + refundedByCheckAmountString;
                    data.Add(detailKey, "<R>" + detailLine);
                    ++detailCount;
                    }

                    if (tenderByCashInAmount > 0.0m)
                    {
                    detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                    refundedByCashAmountString = FormatAmountToPrint(tenderByCashInAmount);
                    spaceLen = MAX_LEN - (paidByText.Length + cashText.Length + refundedByCashAmountString.Length);
                    spacer1 = "".PadLeft(spaceLen - 5, ' ');
                    spacer2 = "".PadLeft(5, ' ');
                    detailLine = spacer2 + paidByText + cashText + spacer1 + refundedByCashAmountString;
                    data.Add(detailKey, "<R>" + detailLine);
                    ++detailCount;
                    }
                    if (tenderByCreditCardAmount > 0.0m)
                    {
                    if (!string.IsNullOrEmpty(refNumberCredit))
                    refNumberCredit = "(" + refNumberCredit + ")";
                    detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                    refundedByCreditCardAmountString = FormatAmountToPrint(tenderByCreditCardAmount);
                    spaceLen = MAX_LEN - (paidByText.Length + saleCreditCardDescription.Length + refNumberCredit.Length + refundedByCreditCardAmountString.Length);
                    spacer1 = "".PadLeft(spaceLen - 5, ' ');
                    spacer2 = "".PadLeft(5, ' ');
                    detailLine = spacer2 + paidByText + saleCreditCardDescription + refNumberCredit + spacer1 + refundedByCreditCardAmountString;
                    data.Add(detailKey, "<R>" + detailLine);
                    ++detailCount;
                    }

                    //Debit card
                    //var byDC = currentSale.TenderDataDetails.Where(tenderType => tenderType.TenderType == TenderTypes.DEBITCARD.ToString());
                    if (tenderByDebitCardAmount > 0.0m)
                    {
                    //refundedByDebitCardAmount = Convert.ToDecimal(byDC.First().TenderAmount);
                    if (!string.IsNullOrEmpty(refNumberDebit))
                    refNumberDebit = "(" + refNumberDebit + ")";
                    detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                    refundedByDebitCardAmountString = FormatAmountToPrint(tenderByDebitCardAmount);
                    spaceLen = MAX_LEN - (paidByText.Length + saleDebitCardDescription.Length + refNumberDebit.Length + refundedByDebitCardAmountString.Length);
                    spacer1 = "".PadLeft(spaceLen - 5, ' ');
                    spacer2 = "".PadLeft(5, ' ');
                    detailLine = spacer2 + paidByText + saleDebitCardDescription + refNumberDebit + spacer1 + refundedByDebitCardAmountString;
                    data.Add(detailKey, "<R>" + detailLine);
                    ++detailCount;
                    }

                    if (tenderByStoreCreditAmount > 0.0m)
                    {
                    if (!string.IsNullOrEmpty(refNumberStoreCredit))
                    refNumberStoreCredit = "(" + refNumberStoreCredit + ")";
                    WriteTenderType(ref data, tenderByStoreCreditAmount, refNumberStoreCredit, ref detailCount, labelStoreCredit);
                    }*/
                    //here insert code for tendertype after that is verified
                    ParseTenderTypes(ref data, ref detailCount, mode);
                    WriteLineInReceipt(ref data, ref detailCount);

                    //Set template name
                    data.Add("##IPADDRESS01##", GlobalDataAccessor.Instance.DesktopSession.ReceiptPrinter.IPAddress);
                    data.Add("##PORTNUMBER01##", GlobalDataAccessor.Instance.DesktopSession.ReceiptPrinter.Port.ToString());

                    //Set number of receipt copies
                    //-- TODO: Changed to two receipts for pilot
                    //
                    data.Add("##HOWMANYCOPIES##", "2");

                    //here add signature line if totalAmount is Greater than 10
                    if (totalRetailAmt > 10m)
                    {
                        //here add signature line if totalAmount is Greater than 10
                        ++detailCount;
                        detailKey = string.Format(
                            "DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                        ++detailCount;
                        data.Add(detailKey, "<S>");

                        detailKey = string.Format("Customer_Employee_Signatures{0}", detailCount.ToString().PadLeft(3, '0'));
                        ++detailCount;
                        data.Add(detailKey, "<R>X_______________________________________");

                        detailKey = string.Format("Customer_Employee_Signatures{0}", detailCount.ToString().PadLeft(3, '0'));
                        ++detailCount;
                        data.Add(detailKey, "<S>");

                        detailKey = string.Format("Customer_Employee_Signatures{0}", detailCount.ToString().PadLeft(3, '0'));
                        ++detailCount;
                        data.Add(detailKey, "<R>EMP______________________________________");
                    }
#if (!__MULTI__)
                    string fullFileName = string.Empty;
                    if (SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled &&
                        GlobalDataAccessor.Instance.DesktopSession.ReceiptPrinter.IsValid)
                    {
                        PrintingUtilities.PrintReceipt(
                            data,
                            false,
                            out fullFileName);
                    }
                    else
                    {
                        //Only for storage purposes since printing is disabled
                        PrintingUtilities.PrintReceipt(data, true, out fullFileName);
                    }

                    //Ensure full file name is valid
                    if (!string.IsNullOrEmpty(fullFileName))
                    {
                        //Get accessors
                        DesktopSession cds = GlobalDataAccessor.Instance.DesktopSession;
                        OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;
                        SecuredCouchConnector cC = GlobalDataAccessor.Instance.CouchDBConnector;
                        //foreach (PurchaseVO purchase in purchases)
                        //{
                        var pDoc = new CouchDbUtils.PawnDocInfo();

                        //Set document add calls
                        pDoc.UseCurrentShopDateTime = true;
                        pDoc.StoreNumber = cds.CurrentSiteId.StoreNumber;
                        pDoc.CustomerNumber = ((mode == ProcessTenderMode.VENDORPURCHASE) ? GlobalDataAccessor.Instance.DesktopSession.ActiveVendor.ID : GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.CustomerNumber);
                        pDoc.DocumentType = Document.DocTypeNames.RECEIPT;
                        pDoc.DocFileName = fullFileName;
                        pDoc.TicketNumber = currentLayaway.TicketNumber;
                        long recNumL = 0L;
                        if (long.TryParse(rVO.ReceiptNumber, out recNumL))
                        {
                            pDoc.ReceiptNumber = recNumL;
                        }

                        //Add this document to the pawn document registry and document storage
                        string errText;
                        if (!CouchDbUtils.AddPawnDocument(dA, cC, userName, ref pDoc, out errText))
                        {
                            if (FileLogger.Instance.IsLogError)
                                FileLogger.Instance.logMessage(LogLevel.ERROR, this,
                                                               "Could not store receipt in document storage: {0} - FileName: {1}", errText, fullFileName);
                            BasicExceptionHandler.Instance.AddException(
                                "Could not store receipt in document storage",
                                new ApplicationException("Could not store receipt in document storage: " + errText));
                        }
                        // }
                    }
#endif
                }
                catch (Exception)
                {
                    return (false);
                }
            }
            return (true);
        }

        private void ResetDeskTopSessionVariables()
        {
            GlobalDataAccessor.Instance.DesktopSession.CashTenderToCustomer = 0.0m;
            GlobalDataAccessor.Instance.DesktopSession.CashTenderFromCustomer = 0.0m;
        }

        public bool GenerateLayawayPaymentReceipt(ProcessTenderMode mode, string userName,
                                                  CustomerVO curCustomer, LayawayVO currentLayaway,
                                                  string vendorName,
                                                  ReceiptDetailsVO rVO)
        {
            string detailLine;
            int spaceLen;
            string spacer;
            lock (mutexIntObj)
            {
                try
                {
                    //Print receipts
                    string custFirstName = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.FirstName;//.Substring(0, 1);
                    string custLastName = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.LastName;
                    //layaway variables
                    string labelLayawayServiceFee = "Layaway Service Fee:";
                    string layawaySaleHeading = "Layaway Payment";
                    string layawayPickupLabel = "Layaway - Paid Out";
                    string layawayPaymentRefundLabel = "Layaway Payment Refund";
                    string layawayNumberHeading = "Lay #: ";
                    string labelBalanceDue = "Balance Due:";
                    string labelNextPaymentDate = "Next Payment Due:";
                    string labelNextPaymentAmount = "Next Payment Amount:";
                    string labelDelinquentAmount = "Delinquent Amount:";
                    string labelChangeDueToCustomer = "Change Due:";
                    string layawayPaymentAmountToString = string.Empty;
                    string balanceDueAmountToString = string.Empty;
                    string nextPaymentAmountToString = string.Empty;
                    string delinquentAmountToString = string.Empty;
                    string changeDueToCustomerString = string.Empty;
                    string refNumberCredit = string.Empty;
                    string refNumberDebit = string.Empty;
                    string refNumberCheck = string.Empty;
                    string refNumberStoreCredit = string.Empty;
                    DateTime nextPaymentDate = DateTime.MinValue;
                    decimal layawayPaymentAmount = 0.0m;
                    decimal balanceDueAmount = 0.0m;
                    decimal nextPaymentAmount = 0.0m;
                    //decimal changeDueAmount = 0.0m;
                    decimal delinquentAmount = 0.0m;
                    decimal tenderByCashInAmount = 0.0m;
                    decimal tenderByCashOutAmount = 0.0m;
                    decimal tenderByCreditCardAmount = 0.0m;
                    decimal tenderByDebitCardAmount = 0.0m;
                    decimal tenderByCheckAmount = 0.0m;
                    decimal tenderByCouponAmount = 0.0m;
                    decimal tenderByStoreCreditAmount = 0.0m;
                    bool customerDataNotNull = false;
                    //layaway void variables
                    string spacer1 = string.Empty;
                    string spacer2 = string.Empty;
                    var data = new Dictionary<string, string>();
                    data.Add("store_short_name", STORE_NAME);
                    data.Add("store_street_address", STORE_ADDRESS);
                    data.Add("store_city_state_zip", STORE_CITY + ", " + STORE_STATE + " " + STORE_ZIP);
                    data.Add("store_phone", formattedPhone);
                    string curTime = ShopDateTime.Instance.ShopShortTime;
                    string curDate = ShopDateTime.Instance.ShopDate.ToString("d", DateTimeFormatInfo.InvariantInfo);
                    data.Add("f_date_and_time", curDate + " " + curTime);
                    if (!string.IsNullOrEmpty(custLastName) && !string.IsNullOrEmpty(custFirstName))
                    {
                        data.Add("f_cust_name", "Customer: " + custLastName + ", " + custFirstName.Substring(0, 1));
                        customerDataNotNull = true;
                    }
                    else
                    {
                        data.Add("f_cust_name", string.Empty);
                    }
                    //need to get Receipt number
                    if (!string.IsNullOrEmpty(rVO.ReceiptNumber) && Convert.ToInt32(rVO.ReceiptNumber) > 0)
                    {
                        data.Add("receipt_number", rVO.ReceiptNumber);
                        data.Add("_BARDATA_H_02", rVO.ReceiptNumber);
                    }
                    else
                    {
                        data.Add("receipt_number", "******");
                        data.Add("_BARDATA_H_02", "000");
                    }
                    data.Add("emp_number", userName);
                    int detailCount = 1;
                    string detailKey = string.Empty;
                    data.Add("##TEMPLATEFILENAME##", "LayawayTemplate.tpl");
                    if (GlobalDataAccessor.Instance.DesktopSession.TenderData == null || GlobalDataAccessor.Instance.DesktopSession.TenderData.Count <= 0)
                        return (false);
                    foreach (TenderEntryVO tEntry in GlobalDataAccessor.Instance.DesktopSession.TenderData)
                    {
                        string tenderType = tEntry.TenderType.ToString();
                        decimal amount = tEntry.Amount;
                        if (tenderType == TenderTypes.CASHOUT.ToString())
                        {
                            tenderByCashOutAmount += Utilities.GetDecimalValue(amount, 0);
                        }
                        if (tenderType == TenderTypes.CASHIN.ToString())
                        {
                            tenderByCashInAmount += Utilities.GetDecimalValue(amount, 0);
                        }
                        if (tenderType == TenderTypes.CREDITCARD.ToString())
                        {
                            refNumberCredit = tEntry.ReferenceNumber;
                            tenderByCreditCardAmount += Utilities.GetDecimalValue(amount, 0);
                        }
                        if (tenderType == TenderTypes.DEBITCARD.ToString())
                        {
                            refNumberDebit = tEntry.ReferenceNumber;
                            tenderByDebitCardAmount += Utilities.GetDecimalValue(amount, 0);
                        }
                        if (tenderType == TenderTypes.CHECK.ToString())
                        {
                            refNumberCheck = tEntry.ReferenceNumber;
                            tenderByCheckAmount += Utilities.GetDecimalValue(amount, 0);
                        }
                        if (tenderType == TenderTypes.COUPON.ToString())
                        {
                            tenderByCouponAmount += Utilities.GetDecimalValue(amount, 0);
                        }
                        if (tenderType == TenderTypes.STORECREDIT.ToString())
                        {
                            tenderByStoreCreditAmount += Utilities.GetDecimalValue(amount, 0);
                            refNumberStoreCredit = tEntry.ReferenceNumber;
                        }
                    }
                    layawayPaymentAmount = tenderByCashInAmount + tenderByCreditCardAmount + tenderByDebitCardAmount + tenderByCheckAmount + tenderByStoreCreditAmount;// +tenderByCouponAmount;

                    //Start loop here to show all payment info, need to get some more info on this
                    //set variables, need to verify actual value for this
                    if (mode == ProcessTenderMode.LAYPAYMENTVOID || mode == ProcessTenderMode.LAYAWAYVOID)
                    {
                        layawaySaleHeading = layawaySaleHeading + " - VOID";
                    }
                    if (mode == ProcessTenderMode.LAYPAYMENTREFVOID)
                        layawayPaymentRefundLabel = layawayPaymentRefundLabel + " - VOID";

                    LayawayPaymentHistoryBuilder historyBuilder = new LayawayPaymentHistoryBuilder();
                    if ((currentLayaway != null) && (mode == ProcessTenderMode.LAYPAYMENT || mode == ProcessTenderMode.LAYPICKUP || mode == ProcessTenderMode.LAYAWAY))
                    {
                        LayawayVO serviceLayaway = currentLayaway;
                        string msr = string.Empty;
                        if (serviceLayaway.LoanStatus != ProductStatus.PU)
                        {
                            ticketNumber = serviceLayaway.TicketNumber;
                            historyBuilder.Layaway = serviceLayaway;
                            if (mode != ProcessTenderMode.LAYAWAY)
                                historyBuilder.AddTemporaryReceipt(serviceLayaway.Payments.Last().Amount, ReceiptEventTypes.LAYPMT, ShopDateTime.Instance.FullShopDateTime);
                            historyBuilder.Calculate();
                            //layawayPaymentAmount = serviceLayaway.Payments.Last().Amount; //need to get correct amount here//nnae
                            detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                            spaceLen = MAX_LEN - (layawaySaleHeading.Length) - 1;
                            spacer = "".PadLeft(spaceLen, ' ');
                            detailLine = layawaySaleHeading + spacer;
                            data.Add(detailKey, "<R>" + detailLine);
                            ++detailCount;

                            //write code here to add layawayPaymentAmount
                            detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                            layawayPaymentAmountToString = FormatAmountToPrint(layawayPaymentAmount);
                            spaceLen = MAX_LEN - (layawayNumberHeading.Length + serviceLayaway.TicketNumber.ToString().Length + layawayPaymentAmountToString.Length);
                            spacer1 = "".PadLeft(spaceLen - 5, ' ');
                            spacer2 = "".PadLeft(5, ' ');
                            detailLine = layawayNumberHeading + serviceLayaway.TicketNumber.ToString() + spacer1 + spacer2 + layawayPaymentAmountToString;
                            data.Add(detailKey, "<R>" + detailLine);
                            ++detailCount;

                            //add line
                            detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                            data.Add(detailKey, "<R>_________________________________________");
                            ++detailCount;

                            detailKey = string.Format(
                                "DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                            data.Add(detailKey, "<S>");
                            ++detailCount;

                            nextPaymentDate = serviceLayaway.NextPayment;
                            nextPaymentAmount = serviceLayaway.NextDueAmount;
                            balanceDueAmount = historyBuilder.GetBalanceOwed();
                            delinquentAmount = historyBuilder.GetDelinquentAmount(ShopDateTime.Instance.FullShopDateTime);

                            //Balance Due
                            detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                            balanceDueAmountToString = FormatAmountToPrint(balanceDueAmount);
                            spaceLen = MAX_LEN - (labelBalanceDue.Length + balanceDueAmountToString.Length);
                            spacer1 = "".PadLeft(spaceLen - 5, ' ');
                            spacer2 = "".PadLeft(5, ' ');
                            detailLine = spacer1 + labelBalanceDue + spacer2 + balanceDueAmountToString;
                            data.Add(detailKey, "<R>" + detailLine);
                            ++detailCount;

                            //Next Payment Due Date
                            detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                            spaceLen = MAX_LEN - (labelNextPaymentDate.Length + nextPaymentDate.ToShortDateString().Length);
                            spacer1 = "".PadLeft(spaceLen - 7, ' ');
                            if (nextPaymentDate.ToShortDateString().Length == 8)
                                spacer2 = "".PadLeft(7, ' ');
                            else if (nextPaymentDate.ToShortDateString().Length == 9)
                                spacer2 = "".PadLeft(6, ' ');
                            else if (nextPaymentDate.ToShortDateString().Length == 10)
                                spacer2 = "".PadLeft(5, ' ');
                            detailLine = spacer1 + labelNextPaymentDate + spacer2 + nextPaymentDate.ToShortDateString();
                            data.Add(detailKey, "<R>" + detailLine);
                            ++detailCount;

                            //Next Payment Due Amount
                            detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                            nextPaymentAmountToString = FormatAmountToPrint(nextPaymentAmount);
                            spaceLen = MAX_LEN - (labelNextPaymentAmount.Length + nextPaymentAmountToString.Length);
                            spacer1 = "".PadLeft(spaceLen - 5, ' ');
                            spacer2 = "".PadLeft(5, ' ');
                            detailLine = spacer1 + labelNextPaymentAmount + spacer2 + nextPaymentAmountToString;
                            data.Add(detailKey, "<R>" + detailLine);
                            ++detailCount;

                            //Delinquent Amount
                            detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                            delinquentAmountToString = FormatAmountToPrint(delinquentAmount);
                            spaceLen = MAX_LEN - (labelDelinquentAmount.Length + delinquentAmountToString.Length);
                            spacer1 = "".PadLeft(spaceLen - 5, ' ');
                            spacer2 = "".PadLeft(5, ' ');
                            detailLine = spacer1 + labelDelinquentAmount + spacer2 + delinquentAmountToString;
                            data.Add(detailKey, "<R>" + detailLine);
                            ++detailCount;
                            /*detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                            data.Add(detailKey, "<R>_________________________________________");
                            ++detailCount;*/
                        }
                        else
                        {
                            //serviceLayawayCount = 1;
                            //add space here 
                            detailKey = string.Format(
                                "DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                            data.Add(detailKey, "<S>");
                            ++detailCount;

                            //pickup stuff here
                            List<ReceiptItems> saleItemsArraylist = new List<ReceiptItems>();
                            int salesItemsCounter = 1;
                            int pickupLayawayCounter = 1;
                            decimal retailAmount;
                            decimal retailTaxAmount;
                            decimal retailTotalAmount;
                            decimal priorPaymentsAmount;
                            decimal amountPaidToday;
                            decimal layawayServiceFee = 0.0m;
                            string retailAmountToString;
                            string retailTaxAmountToString;
                            string retailTotalAmountToString;
                            string labelRetailPrice = "Retail Price:";
                            string labelTotalRetail = "Total Retail:";
                            string labelSalesTax = "Sales Tax:";
                            string labelPriorPayments = "Prior Payments:";
                            string labelAmountPaidToday = "Amount Paid Today:";
                            string priorPaymentsAmountToString;
                            string amountPaidTodayToString;
                            string paidOffNextPaymentDate = "**/**/****";
                            string layawayServiceFeeTostring = string.Empty;

                            retailAmount = 0.0m;
                            retailTotalAmount = 0.0m;
                            retailTaxAmount = 0.0m;
                            priorPaymentsAmount = 0.0m;
                            amountPaidToday = 0.0m;
                            retailAmountToString = string.Empty;
                            retailTaxAmountToString = string.Empty;
                            retailTotalAmountToString = string.Empty;
                            priorPaymentsAmountToString = string.Empty;
                            amountPaidTodayToString = string.Empty;
                            historyBuilder.Layaway = currentLayaway;
                            historyBuilder.AddTemporaryReceipt(currentLayaway.Payments.Last().Amount, ReceiptEventTypes.LAYPMT, ShopDateTime.Instance.FullShopDateTime);
                            historyBuilder.Calculate();
                            string layawayNumberAndTicketNumber = layawayNumberHeading + currentLayaway.TicketNumber.ToString();
                            if (mode == ProcessTenderMode.LAYPICKUP)
                            {
                                for (int i = 0; i < rVO.RefEvents.Count; i++)
                                {
                                    if (rVO.RefEvents[i].ToString() == ReceiptEventTypes.SALE.ToString())
                                        msr = rVO.RefNumbers[i].ToString();
                                }
                                layawayNumberAndTicketNumber = layawayNumberAndTicketNumber + " MSR# " + msr;
                            }
                            /*for (int i = 0; i < rVO.RefEvents.Count; i++)
                            {
                            if (rVO.RefEvents[i].ToString() == ReceiptEventTypes.LAYPMT.ToString())
                            msr = rVO.RefNumbers[i].ToString();
                            else if (rVO.RefEvents[i].ToString() == ReceiptEventTypes.LAY.ToString())
                            msr = rVO.RefNumbers[i].ToString();
                            }*/
                            //write header here 
                            layawayPickupLabel = pickupLayawayCounter + " " + layawayPickupLabel;
                            detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                            spaceLen = MAX_LEN - (layawayPickupLabel.Length) - 1;
                            spacer = "".PadLeft(spaceLen, ' ');
                            detailLine = layawayPickupLabel + spacer;
                            data.Add(detailKey, "<R>" + detailLine);
                            ++detailCount;

                            //write code here to add layawayPaymentAmount
                            detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                            spaceLen = MAX_LEN - (layawayNumberAndTicketNumber.Length);
                            spacer1 = "".PadLeft(spaceLen - 5, ' ');
                            spacer2 = "".PadLeft(5, ' ');
                            detailLine = layawayNumberAndTicketNumber + spacer1 + spacer2;
                            data.Add(detailKey, "<R>" + detailLine);
                            ++detailCount;

                            detailKey = string.Format(
                                "DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                            data.Add(detailKey, "<S>");
                            ++detailCount;

                            foreach (RetailItem saleItem in currentLayaway.RetailItems)
                            {
                                int qty = 0;
                                if (saleItem == null)
                                    continue;
                                if (saleItem.Quantity > 0)
                                    qty = saleItem.Quantity;
                                else
                                    qty = 1;
                                retailAmount += saleItem.RetailPrice * qty;
                                AddSaleDetailLineToList(saleItemsArraylist, saleItem, mode);
                            }
                            if (retailAmount > 0.0M)
                            {
                                WriteSaleDetailLines(data, ref detailCount, saleItemsArraylist, ref salesItemsCounter, currentLayaway, mode);
                            }
                            foreach (Fee fee in currentLayaway.Fees)
                            {
                                if (fee.FeeType == FeeTypes.SERVICE)
                                    layawayServiceFee += fee.OriginalAmount;
                            }
                            //space
                            detailKey = string.Format(
                                "DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                            data.Add(detailKey, "<S>");
                            ++detailCount;

                            if (currentLayaway.SalesTaxPercentage > 0.0m)
                                retailTaxAmount = Math.Round((currentLayaway.SalesTaxPercentage / 100) * retailAmount, 2);
                            else
                                retailTaxAmount = Math.Round((8.25m / 100) * retailAmount, 2);
                            retailTotalAmount = retailAmount + retailTaxAmount + layawayServiceFee;

                            //retail Price
                            detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                            retailAmountToString = FormatAmountToPrint(retailAmount);
                            spaceLen = MAX_LEN - (labelRetailPrice.Length + retailAmountToString.Length);
                            spacer1 = "".PadLeft(spaceLen - 5, ' ');
                            spacer2 = "".PadLeft(5, ' ');
                            detailLine = spacer1 + labelRetailPrice + spacer2 + retailAmountToString;
                            data.Add(detailKey, "<R>" + detailLine);
                            ++detailCount;

                            //Layaway Service Fee
                            detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                            layawayServiceFeeTostring = FormatAmountToPrint(layawayServiceFee);
                            spaceLen = MAX_LEN - (labelLayawayServiceFee.Length + layawayServiceFeeTostring.Length);
                            spacer1 = "".PadLeft(spaceLen - 5, ' ');
                            spacer2 = "".PadLeft(5, ' ');
                            detailLine = spacer1 + labelLayawayServiceFee + spacer2 + layawayServiceFeeTostring;
                            data.Add(detailKey, "<R>" + detailLine);
                            ++detailCount;

                            //sales tax
                            detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                            retailTaxAmountToString = FormatAmountToPrint(retailTaxAmount);
                            spaceLen = MAX_LEN - (labelSalesTax.Length + retailTaxAmountToString.Length);
                            spacer1 = "".PadLeft(spaceLen - 5, ' ');
                            spacer2 = "".PadLeft(5, ' ');
                            detailLine = spacer1 + labelSalesTax + spacer2 + retailTaxAmountToString;
                            data.Add(detailKey, "<R>" + detailLine);
                            ++detailCount;

                            //totalRetail
                            detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                            retailTotalAmountToString = FormatAmountToPrint(retailTotalAmount);
                            spaceLen = MAX_LEN - (labelTotalRetail.Length + retailTotalAmountToString.Length);
                            spacer1 = "".PadLeft(spaceLen - 5, ' ');
                            spacer2 = "".PadLeft(5, ' ');
                            detailLine = spacer1 + labelTotalRetail + spacer2 + retailTotalAmountToString;
                            data.Add(detailKey, "<R>" + detailLine);
                            ++detailCount;

                            //line
                            detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                            data.Add(detailKey, "<R>_________________________________________");
                            ++detailCount;

                            //nextPaymentDate = layaway.NextPayment;
                            //nextPaymentAmount = layaway.NextDueAmount;
                            balanceDueAmount = historyBuilder.GetBalanceOwed();
                            delinquentAmount = historyBuilder.GetDelinquentAmount(ShopDateTime.Instance.FullShopDateTime);
                            amountPaidToday = currentLayaway.Payments.Last().Amount;
                            //priorPaymentsAmount = Math.Round(layaway.Payments.Sum(layPmt => layPmt.Amount), 2) - amountPaidToday;
                            foreach (Receipt rcpt in currentLayaway.Receipts)
                                priorPaymentsAmount += rcpt.Amount;
                            //priorPaymentsAmount = priorPaymentsAmount - amountPaidToday;

                            //prior payments, sum of all payments - the last payment
                            detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                            priorPaymentsAmountToString = FormatAmountToPrint(priorPaymentsAmount);
                            spaceLen = MAX_LEN - (labelPriorPayments.Length + priorPaymentsAmountToString.Length);
                            spacer1 = "".PadLeft(spaceLen - 5, ' ');
                            spacer2 = "".PadLeft(5, ' ');
                            detailLine = spacer1 + labelPriorPayments + spacer2 + priorPaymentsAmountToString;
                            data.Add(detailKey, "<R>" + detailLine);
                            ++detailCount;

                            //amount paid today, the last payment
                            detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                            amountPaidTodayToString = FormatAmountToPrint(amountPaidToday);
                            spaceLen = MAX_LEN - (labelAmountPaidToday.Length + amountPaidTodayToString.Length);
                            spacer1 = "".PadLeft(spaceLen - 5, ' ');
                            spacer2 = "".PadLeft(5, ' ');
                            detailLine = spacer1 + labelAmountPaidToday + spacer2 + amountPaidTodayToString;
                            data.Add(detailKey, "<R>" + detailLine);
                            ++detailCount;

                            //Balance Due
                            detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                            balanceDueAmountToString = FormatAmountToPrint(balanceDueAmount);
                            spaceLen = MAX_LEN - (labelBalanceDue.Length + balanceDueAmountToString.Length);
                            spacer1 = "".PadLeft(spaceLen - 5, ' ');
                            spacer2 = "".PadLeft(5, ' ');
                            detailLine = spacer1 + labelBalanceDue + spacer2 + balanceDueAmountToString;
                            data.Add(detailKey, "<R>" + detailLine);
                            ++detailCount;

                            //Next Payment Due Date
                            detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                            spaceLen = MAX_LEN - (labelNextPaymentDate.Length + paidOffNextPaymentDate.Length);
                            spacer1 = "".PadLeft(spaceLen - 5, ' ');
                            spacer2 = "".PadLeft(5, ' ');
                            detailLine = spacer1 + labelNextPaymentDate + spacer2 + paidOffNextPaymentDate;
                            data.Add(detailKey, "<R>" + detailLine);
                            ++detailCount;

                            //Next Payment Due Amount
                            detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                            nextPaymentAmountToString = FormatAmountToPrint(nextPaymentAmount);
                            spaceLen = MAX_LEN - (labelNextPaymentAmount.Length + nextPaymentAmountToString.Length);
                            spacer1 = "".PadLeft(spaceLen - 5, ' ');
                            spacer2 = "".PadLeft(5, ' ');
                            detailLine = spacer1 + labelNextPaymentAmount + spacer2 + nextPaymentAmountToString;
                            data.Add(detailKey, "<R>" + detailLine);
                            ++detailCount;

                            //Delinquent Amount
                            detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                            delinquentAmountToString = FormatAmountToPrint(delinquentAmount);
                            spaceLen = MAX_LEN - (labelDelinquentAmount.Length + delinquentAmountToString.Length);
                            spacer1 = "".PadLeft(spaceLen - 5, ' ');
                            spacer2 = "".PadLeft(5, ' ');
                            detailLine = spacer1 + labelDelinquentAmount + spacer2 + delinquentAmountToString;
                            data.Add(detailKey, "<R>" + detailLine);
                            ++detailCount;
                            //line
                            /* if (pickupLayawayCounter < GlobalDataAccessor.Instance.DesktopSession.ServiceLayaways.Count)
                            {
                            detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                            data.Add(detailKey, "<R>_________________________________________");
                            ++detailCount;
                            }*/
                        }
                    }
                    else
                    {
                        if (currentLayaway == null)
                            currentLayaway = GlobalDataAccessor.Instance.DesktopSession.ActiveLayaway;
                        ticketNumber = currentLayaway.TicketNumber;
                        historyBuilder.Layaway = currentLayaway;
                        historyBuilder.AddTemporaryReceipt(currentLayaway.DownPayment, ReceiptEventTypes.LAY, ShopDateTime.Instance.FullShopDateTime);
                        historyBuilder.Calculate();

                        if (mode == ProcessTenderMode.LAYPAYMENTREF || mode == ProcessTenderMode.LAYPAYMENTREFVOID)
                        {
                            if (mode == ProcessTenderMode.LAYPAYMENTREF)
                            {
                                layawayPaymentAmount += tenderByCashOutAmount;
                            }
                            else
                            {
                                layawayPaymentAmount = historyBuilder.GetTotalRefunded(); //needs to be changed to get correct amount //nnae
                            }
                            layawayPaymentRefundLabel = "1 " + layawayPaymentRefundLabel;
                            detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                            spaceLen = MAX_LEN - (layawayPaymentRefundLabel.Length) - 1;
                            spacer = "".PadLeft(spaceLen, ' ');
                            detailLine = layawayPaymentRefundLabel + spacer;
                            data.Add(detailKey, "<R>" + detailLine);
                            ++detailCount;
                        }
                        else
                        {
                            layawaySaleHeading = "1 " + layawaySaleHeading;
                            detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                            spaceLen = MAX_LEN - (layawaySaleHeading.Length) - 1;
                            spacer = "".PadLeft(spaceLen, ' ');
                            detailLine = layawaySaleHeading + spacer;
                            data.Add(detailKey, "<R>" + detailLine);
                            ++detailCount;
                        }
                        //write code here to add layawayPaymentAmount
                        detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                        layawayPaymentAmountToString = FormatAmountToPrint(layawayPaymentAmount);
                        spaceLen = MAX_LEN - (layawayNumberHeading.Length + currentLayaway.TicketNumber.ToString().Length + layawayPaymentAmountToString.Length);
                        spacer1 = "".PadLeft(spaceLen - 5, ' ');
                        spacer2 = "".PadLeft(5, ' ');
                        detailLine = layawayNumberHeading + currentLayaway.TicketNumber.ToString() + spacer1 + spacer2 + layawayPaymentAmountToString;
                        data.Add(detailKey, "<R>" + detailLine);
                        ++detailCount;

                        //add line
                        detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                        data.Add(detailKey, "<R>_________________________________________");
                        ++detailCount;

                        detailKey = string.Format(
                            "DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                        data.Add(detailKey, "<S>");
                        ++detailCount;

                        nextPaymentDate = currentLayaway.NextPayment;
                        nextPaymentAmount = currentLayaway.NextDueAmount;
                        balanceDueAmount = historyBuilder.GetBalanceOwed();
                        delinquentAmount = historyBuilder.GetDelinquentAmount(ShopDateTime.Instance.FullShopDateTime);

                        //Balance Due
                        detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                        balanceDueAmountToString = FormatAmountToPrint(balanceDueAmount);
                        spaceLen = MAX_LEN - (labelBalanceDue.Length + balanceDueAmountToString.Length);
                        spacer1 = "".PadLeft(spaceLen - 5, ' ');
                        spacer2 = "".PadLeft(5, ' ');
                        detailLine = spacer1 + labelBalanceDue + spacer2 + balanceDueAmountToString;
                        data.Add(detailKey, "<R>" + detailLine);
                        ++detailCount;

                        //Next Payment Due Date
                        detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                        spaceLen = MAX_LEN - (labelNextPaymentDate.Length + nextPaymentDate.ToShortDateString().Length);
                        spacer1 = "".PadLeft(spaceLen - 7, ' ');
                        spacer2 = FormatDateToPrint(nextPaymentDate);
                        detailLine = spacer1 + labelNextPaymentDate + spacer2 + nextPaymentDate.ToShortDateString();
                        data.Add(detailKey, "<R>" + detailLine);
                        ++detailCount;

                        //Next Payment Due Amount
                        detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                        nextPaymentAmountToString = FormatAmountToPrint(nextPaymentAmount);
                        spaceLen = MAX_LEN - (labelNextPaymentAmount.Length + nextPaymentAmountToString.Length);
                        spacer1 = "".PadLeft(spaceLen - 5, ' ');
                        spacer2 = "".PadLeft(5, ' ');
                        detailLine = spacer1 + labelNextPaymentAmount + spacer2 + nextPaymentAmountToString;
                        data.Add(detailKey, "<R>" + detailLine);
                        ++detailCount;

                        //Delinquent Amount
                        detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                        delinquentAmountToString = FormatAmountToPrint(delinquentAmount);
                        spaceLen = MAX_LEN - (labelDelinquentAmount.Length + delinquentAmountToString.Length);
                        spacer1 = "".PadLeft(spaceLen - 5, ' ');
                        spacer2 = "".PadLeft(5, ' ');
                        detailLine = spacer1 + labelDelinquentAmount + spacer2 + delinquentAmountToString;
                        data.Add(detailKey, "<R>" + detailLine);
                        ++detailCount;
                    }

                    WriteLineInReceipt(ref data, ref detailCount);
                    //loop thru tendertypes and write all of them out

                    bool tenderTypeFound = false;
                    string receivedFromAmtToPrint = string.Empty;
                    string receivedFromText = "RECEIVED FROM CUSTOMER:";
                    decimal amountRecvd = 0.0m;
                    if (mode != ProcessTenderMode.LAYPAYMENTREF)
                    {
                        amountRecvd = (tenderByCashInAmount + Math.Abs(GlobalDataAccessor.Instance.DesktopSession.CashTenderToCustomer)) + tenderByCheckAmount + tenderByCreditCardAmount + tenderByDebitCardAmount + tenderByStoreCreditAmount;
                        amountRecvd = Utilities.GetDecimalValue(amountRecvd, 0);
                        detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                        receivedFromAmtToPrint = FormatAmountToPrint(amountRecvd);//need to change amt here to actual amt received once that is determined thru cash tender
                        spaceLen = MAX_LEN - (receivedFromText.Length + receivedFromAmtToPrint.Length);
                        spacer1 = "".PadLeft(spaceLen - 5, ' ');
                        spacer2 = "".PadLeft(5, ' ');
                        detailLine = spacer1 + receivedFromText + spacer2 + receivedFromAmtToPrint;
                        data.Add(detailKey, "<R>" + detailLine);
                        ++detailCount;

                        if (Math.Abs(GlobalDataAccessor.Instance.DesktopSession.CashTenderToCustomer) > 0.0m)
                        {
                            detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                            changeDueToCustomerString = FormatAmountToPrint(Math.Abs(GlobalDataAccessor.Instance.DesktopSession.CashTenderToCustomer));
                            spaceLen = MAX_LEN - (labelChangeDueToCustomer.Length + changeDueToCustomerString.Length);
                            spacer1 = "".PadLeft(spaceLen - 5, ' ');
                            spacer2 = "".PadLeft(5, ' ');
                            detailLine = spacer1 + labelChangeDueToCustomer + spacer2 + changeDueToCustomerString;
                            data.Add(detailKey, "<R>" + detailLine);
                            ++detailCount;
                            tenderTypeFound = true;
                        }
                        WriteLineInReceipt(ref data, ref detailCount);
                    }
                    ParseTenderTypes(ref data, ref detailCount, mode);

                    if (tenderTypeFound)
                    {
                        WriteLineInReceipt(ref data, ref detailCount);
                    }

                    string forfeitWarning1 = "*** This layaway is not current and";
                    string forfeitWarning2 = "is subject to forfeiture***";
                    if (delinquentAmount > 0.0m)
                    {
                        WriteEmptyLineToReceipt(ref data, ref detailCount);
                        detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                        spaceLen = MAX_LEN - (forfeitWarning1.Length);
                        spacer1 = "".PadLeft(spaceLen / 2, ' ');
                        detailLine = spacer1 + forfeitWarning1 + spacer1;
                        data.Add(detailKey, "<R>" + detailLine);
                        ++detailCount;

                        detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                        spaceLen = MAX_LEN - (forfeitWarning2.Length);
                        spacer1 = "".PadLeft(spaceLen / 2, ' ');
                        detailLine = spacer1 + forfeitWarning2 + spacer1;
                        data.Add(detailKey, "<R>" + detailLine);
                        ++detailCount;

                    }

                    //Set template name
                    data.Add("##IPADDRESS01##", GlobalDataAccessor.Instance.DesktopSession.ReceiptPrinter.IPAddress);
                    data.Add("##PORTNUMBER01##", GlobalDataAccessor.Instance.DesktopSession.ReceiptPrinter.Port.ToString());

                    //Set number of receipt copies
                    //-- TODO: Changed to two receipts for pilot
                    //
                    data.Add("##HOWMANYCOPIES##", "2");

                    //here add signature line if totalAmount is Greater than 10
                    ++detailCount;
                    detailKey = string.Format(
                        "DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                    ++detailCount;
                    data.Add(detailKey, "<S>");

                    detailKey = string.Format("Customer_Employee_Signatures{0}", detailCount.ToString().PadLeft(3, '0'));
                    ++detailCount;
                    data.Add(detailKey, "<R>X_______________________________________");

                    detailKey = string.Format("Customer_Employee_Signatures{0}", detailCount.ToString().PadLeft(3, '0'));
                    ++detailCount;
                    data.Add(detailKey, "<S>");

                    detailKey = string.Format("Customer_Employee_Signatures{0}", detailCount.ToString().PadLeft(3, '0'));
                    ++detailCount;
                    data.Add(detailKey, "<R>EMP______________________________________");

                    //here check customer Data, remove Customer Name if amt is less than 10 and customer is not in session and tendertype == cash
                    if (layawayPaymentAmount < 10.00m && !customerDataNotNull && (tenderByCashInAmount > 0.0m
                        && tenderByCheckAmount <= 0.0m
                        && tenderByCreditCardAmount <= 0.0m
                        && tenderByDebitCardAmount <= 0.0m
                        && tenderByStoreCreditAmount <= 0.0m)
                        )
                    {
                        if (data.ContainsKey("f_cust_name"))
                            data["f_cust_name"] = string.Empty;
                    }
#if (!__MULTI__)
                    string fullFileName = string.Empty;
                    if (mode == ProcessTenderMode.LAYAWAY || mode == ProcessTenderMode.LAYPAYMENT || mode == ProcessTenderMode.LAYPICKUP)
                    {
                        CallPrintReceipt(data, out fullFileName);
                        CallPrintReceipt(data, out fullFileName);
                    }
                    else
                    {
                        CallPrintReceipt(data, out fullFileName);
                    }
                    //GenerateLayawayPickingSlip(rVO);
                    //GenerateLayawayPaymentReceipt(mode, userName,curCustomer, currentLayaway,vendorName, rVO);
                    //Ensure full file name is valid
                    if (!string.IsNullOrEmpty(fullFileName))
                    {
                        //Get accessors
                        DesktopSession cds = GlobalDataAccessor.Instance.DesktopSession;
                        OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;
                        SecuredCouchConnector cC = GlobalDataAccessor.Instance.CouchDBConnector;
                        //foreach (PurchaseVO purchase in purchases)
                        //{
                        var pDoc = new CouchDbUtils.PawnDocInfo();

                        //Set document add calls
                        pDoc.UseCurrentShopDateTime = true;
                        pDoc.StoreNumber = cds.CurrentSiteId.StoreNumber;
                        pDoc.CustomerNumber = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.CustomerNumber;
                        pDoc.DocumentType = Document.DocTypeNames.RECEIPT;
                        pDoc.DocFileName = fullFileName;
                        pDoc.TicketNumber = currentLayaway.TicketNumber;
                        long recNumL = 0L;
                        if (long.TryParse(rVO.ReceiptNumber, out recNumL))
                        {
                            pDoc.ReceiptNumber = recNumL;
                        }

                        //Add this document to the pawn document registry and document storage
                        string errText;
                        if (!CouchDbUtils.AddPawnDocument(dA, cC, userName, ref pDoc, out errText))
                        {
                            if (FileLogger.Instance.IsLogError)
                                FileLogger.Instance.logMessage(LogLevel.ERROR, this,
                                                               "Could not store receipt in document storage: {0} - FileName: {1}", errText, fullFileName);
                            BasicExceptionHandler.Instance.AddException(
                                "Could not store receipt in document storage",
                                new ApplicationException("Could not store receipt in document storage: " + errText));
                        }
                    }
#endif
                }
                catch (Exception)
                {
                    return (false);
                }
            }
            return (true);
        }

        public static void CallPrintReceipt(Dictionary<string, string> data, out string fullFileName)
        {
            if (SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled &&
                GlobalDataAccessor.Instance.DesktopSession.ReceiptPrinter.IsValid)
            {
                PrintingUtilities.PrintReceipt(
                    data,
                    false,
                    out fullFileName);
            }
            else
            {
                //Only for storage purposes since printing is disabled
                PrintingUtilities.PrintReceipt(data, true, out fullFileName);
            }
        }

        private string FormatDateToPrint(DateTime dateToFormat)
        {
            string spacer = "".PadLeft(5, ' ');
            if (dateToFormat.ToShortDateString().Length == 8)
                spacer = "".PadLeft(7, ' ');
            else if (dateToFormat.ToShortDateString().Length == 9)
                spacer = "".PadLeft(6, ' ');
            else if (dateToFormat.ToShortDateString().Length == 10)
                spacer = "".PadLeft(5, ' ');
            return spacer;
        }

        private string FormatAmountToPrint(decimal amt)
        {
            //detailLine = spacer + salesTaxText + spacer + "$" + salesTaxAmt;
            string amount = string.Empty;
            string formattedAmt = amt.ToString("C");
            formattedAmt = formattedAmt.Replace("$", string.Empty);
            if (formattedAmt.Length == 3)
                amount = "$" + "".PadLeft(4, ' ') + formattedAmt;
            else if (formattedAmt.Length == 4)
                amount = "$" + "".PadLeft(5, ' ') + formattedAmt;
            else if (formattedAmt.Length == 5)
                amount = "$" + "".PadLeft(4, ' ') + formattedAmt;
            else if (formattedAmt.Length == 6)
                amount = "$" + "".PadLeft(3, ' ') + formattedAmt;
            else if (formattedAmt.Length == 7)
                amount = "$" + "".PadLeft(2, ' ') + formattedAmt;
            else if (formattedAmt.Length == 8)
                amount = "$" + "".PadLeft(1, ' ') + formattedAmt;
            else if (formattedAmt.Length == 9)
                amount = "$" + amt;
            return amount;
        }

        public bool generatePurchaseReceipt(ProcessTenderMode mode, string userName,
                                            CustomerVO curCustomer, List<PurchaseVO> purchases,
                                            string vendorName,
                                            ReceiptDetailsVO rVO)
        {
            string detailLine;
            int spaceLen;
            string spacer;
            bool transactStarted = false;
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;
            bool couchIssue = false;

            lock (mutexIntObj)
            {
                //Print receipts
                try
                {
                    var data = new Dictionary<string, string>();
                    data.Add("store_short_name", STORE_NAME);
                    data.Add("store_street_address", STORE_ADDRESS);
                    data.Add("store_city_state_zip", STORE_CITY + ", " + STORE_STATE + " " + STORE_ZIP);
                    data.Add("store_phone", formattedPhone);
                    string curTime = ShopDateTime.Instance.ShopShortTime;
                    string curDate = ShopDateTime.Instance.ShopDate.ToString("d", DateTimeFormatInfo.InvariantInfo);
                    data.Add("f_date_and_time", curDate + " " + curTime);
                    if (string.IsNullOrEmpty(vendorName) && !string.IsNullOrEmpty(curCustomer.LastName) && !string.IsNullOrEmpty(curCustomer.FirstName))
                        data.Add("f_cust_name", curCustomer.LastName + ", " + curCustomer.FirstName.Substring(0, 1));
                    else if (!string.IsNullOrEmpty(vendorName))
                        data.Add("f_cust_name", "Vendor: " + vendorName);
                    else
                        data.Add("f_cust_name", string.Empty);

                    if (!string.IsNullOrEmpty(rVO.ReceiptNumber))
                    {
                        data.Add("receipt_number", rVO.ReceiptNumber);
                        data.Add("_BARDATA_H_02", rVO.ReceiptNumber);
                    }
                    else
                    {
                        data.Add("receipt_number", "******");
                        data.Add("_BARDATA_H_02", "000");
                    }

                    data.Add("emp_number", userName);
                    if (mode == ProcessTenderMode.VOIDLOAN || mode == ProcessTenderMode.VOIDBUY)
                    {
                        data.Add("##TEMPLATEFILENAME##", "srvcReceiptTemplate.tpl");
                        PurchaseVO purch = purchases.First();
                        decimal voidAmount = 0.0M;
                        voidAmount = Utilities.GetDecimalValue(rVO.RefAmounts.First(), 0);
                        string ticketAmount = voidAmount.ToString("C");
                        //string ticketNumb = "*****" + pLoan.TicketNumber;

                        //GJL 3/30/2010 Code on line below computing spaceLen must be changed!!
                        if (rVO.RefEvents.First().Equals("VRET"))
                            spaceLen = MAX_LEN - ("1 Void Purchase Return:".Length +
                                                  purch.TicketNumber.ToString().Length +
                                                  ticketAmount.Length);
                        else
                            spaceLen = MAX_LEN - ("1 Void Purchase :".Length +
                                                  purch.TicketNumber.ToString().Length +
                                                  ticketAmount.Length);
                        //GJL 3/30/2010 Code on line below computing spacer must be changed!!
                        spacer = "".PadLeft(spaceLen, ' ');

                        //GJL 3/30/2010 Code on line below computing detailLine must be changed!!
                        if (rVO.RefEvents.First().Equals("VRET"))
                            detailLine = "1 Void Purchase Return :" + purch.TicketNumber + spacer + ticketAmount;
                        else
                            detailLine = "1 Void Purchase :" + purch.TicketNumber + spacer + ticketAmount;
                        data.Add("DETAIL001", "<B>" + detailLine);
                        data.Add("DETAIL002", "<L>");
                        data.Add("DETAIL003", "<S>");

                        //GJL 3/30/2010 Code on line below computing spaceLen must be changed!!
                        spaceLen = MAX_LEN - ("VOID Amount".Length + ticketAmount.Length);
                        //GJL 3/30/2010 Code on line below computing spacer must be changed!!
                        spacer = "".PadLeft(spaceLen, ' ');
                        //GJL 3/30/2010 Code on line below computing detailLine must be changed!!
                        detailLine = "VOID Amount" + spacer + ticketAmount;
                        data.Add("DETAIL004", "<R>" + detailLine);
                    }
                    else if (mode == ProcessTenderMode.PURCHASE ||
                             mode == ProcessTenderMode.VENDORPURCHASE ||
                             mode == ProcessTenderMode.RETURNBUY)
                    {
                        int detailCount = 1;
                        int purchaseCount = 1;
                        decimal totalAmount = 0.00M;
                        decimal amountRecvd = 0.00M;

                        string detailKey = string.Empty;
                        decimal totalPurchaseAmount = 0.00M;
                        bool amountDisbursed = false;

                        data.Add("##TEMPLATEFILENAME##", "srvcReceiptTemplate.tpl");
                        List<ReceiptItems> purchaseStatusArraylist = new List<ReceiptItems>();

                        foreach (PurchaseVO purchase in purchases)
                        {
                            if (purchase == null)
                                continue;
                            switch (purchase.LoanStatus)
                            {
                                case ProductStatus.PUR:
                                case ProductStatus.PFI:
                                    {
                                        totalPurchaseAmount += purchase.Amount;
                                        BuildDetailLine(purchaseStatusArraylist, purchase.Amount,
                                                        "Merchandise Purchase",
                                                        purchase.TicketNumber.ToString());
                                        amountDisbursed = true;
                                        break;
                                    }
                                case ProductStatus.RET:
                                    {
                                        decimal amtReturned = 0.0m;
                                        foreach (Item item in purchase.Items)
                                            amtReturned += item.ItemAmount;
                                        //var amtReturned = (from item in purchase.Items
                                        //select item.ItemAmount).Sum();
                                        totalPurchaseAmount += amtReturned;//purchase.Items.Sum(amt => amt.ItemAmount);
                                        BuildDetailLine(purchaseStatusArraylist, amtReturned,
                                                        "Merchandise Return",
                                                        purchase.TicketNumber.ToString());
                                        break;
                                    }

                                default:
                                    {
                                        throw new ArgumentOutOfRangeException(
                                            string.Format(
                                                "{0} is out of range for print receipt in process tender - not handled yet!",
                                                purchase.TempStatus.ToString()));
                                    }
                            }
                            ++purchaseCount;
                        }
                        int loanDataCount = 1;
                        if (totalPurchaseAmount > 0.00M)
                        {
                            BuildSubTotalLine(data, totalPurchaseAmount,
                                              "Total Purchase Amount", ref detailCount, purchaseStatusArraylist, ref loanDataCount);
                        }

                        //Update detail key
                        detailKey = string.Format(
                            "DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                        ++detailCount;
                        data.Add(detailKey, "<L>");

                        //Update detail key
                        detailKey = string.Format(
                            "DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                        ++detailCount;
                        data.Add(detailKey, "<S>");

                        //Compute total
                        totalAmount = totalPurchaseAmount;
                        amountRecvd = totalPurchaseAmount;

                        detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                        spaceLen = MAX_LEN - ("Grand Total".Length + totalAmount.ToString().Length) - 1;
                        spacer = "".PadLeft(spaceLen, ' ');
                        detailLine = "Grand Total" + spacer + "$" + totalAmount;
                        data.Add(detailKey, "<R>" + detailLine);
                        ++detailCount;

                        //Update detail key
                        //amountRecvd = GlobalDataAccessor.Instance.DesktopSession.CashTenderFromCustomer;
                        detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));

                        string AMTTOCUSTOMER = "Amount To Customer";
                        string AMTTOVENDOR = "Amount To Vendor";
                        if (amountDisbursed)
                            if (string.IsNullOrEmpty(vendorName))
                                spaceLen = MAX_LEN - (AMTTOCUSTOMER.Length + totalAmount.ToString().Length) - 1;
                            else
                                spaceLen = MAX_LEN - (AMTTOVENDOR.Length + totalAmount.ToString().Length) - 1;
                        else
                            spaceLen = MAX_LEN - ("Amount Received From Customer".Length + amountRecvd.ToString().Length) - 1;

                        spacer = "".PadLeft(spaceLen, ' ');

                        if (amountDisbursed)
                            if (string.IsNullOrEmpty(vendorName))
                                detailLine = AMTTOCUSTOMER + spacer + "$" + amountRecvd;
                            else
                                detailLine = AMTTOVENDOR + spacer + "$" + amountRecvd;
                        else
                            detailLine = "Amount Received From Customer" + spacer + "$" + amountRecvd;
                        data.Add(detailKey, "<R>" + detailLine);
                        ++detailCount;

                        detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                        data.Add(detailKey, "<R>                                 ________");
                        ++detailCount;
                        //changeDueAmount = amountRecvd - totalAmount;
                        //detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                        //spaceLen = MAX_LEN - ("Change".Length + changeDueAmount.ToString().Length) - 1;
                        //spacer = "".PadLeft(spaceLen, ' ');
                        //detailLine = "Change" + spacer + "$" + changeDueAmount;
                        //data.Add(detailKey, "<R>" + detailLine);
                        //++detailCount;
                    }

                    //Set template name

                    data.Add("##IPADDRESS01##", GlobalDataAccessor.Instance.DesktopSession.ReceiptPrinter.IPAddress);
                    data.Add("##PORTNUMBER01##", GlobalDataAccessor.Instance.DesktopSession.ReceiptPrinter.Port.ToString());

                    //Set number of receipt copies
                    //-- TODO: Changed to two receipts for pilot
                    //
                    data.Add("##HOWMANYCOPIES##", "2");
                    string fullFileName = string.Empty;
                    if (SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled &&
                        GlobalDataAccessor.Instance.DesktopSession.ReceiptPrinter.IsValid)
                    {
                        PrintingUtilities.PrintReceipt(
                            data,
                            false,
                            out fullFileName);
                    }
                    else
                    {
                        //Only for storage purposes since printing is disabled
                        PrintingUtilities.PrintReceipt(data, true, out fullFileName);
                    }

                    //Ensure full file name is valid
                    if (!string.IsNullOrEmpty(fullFileName))
                    {
                        //Get accessors
                        DesktopSession cds = GlobalDataAccessor.Instance.DesktopSession;
                        SecuredCouchConnector cC = GlobalDataAccessor.Instance.CouchDBConnector;
                        foreach (PurchaseVO purchase in purchases)
                        {
                            var pDoc = new CouchDbUtils.PawnDocInfo();

                            //Set document add calls
                            pDoc.UseCurrentShopDateTime = true;
                            pDoc.StoreNumber = cds.CurrentSiteId.StoreNumber;
                            pDoc.CustomerNumber = ((mode == ProcessTenderMode.VENDORPURCHASE) ? GlobalDataAccessor.Instance.DesktopSession.ActiveVendor.ID : GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.CustomerNumber);
                            pDoc.DocumentType = Document.DocTypeNames.RECEIPT;
                            pDoc.DocFileName = fullFileName;
                            pDoc.TicketNumber = purchase.TicketNumber;
                            long recNumL = 0L;
                            if (long.TryParse(rVO.ReceiptNumber, out recNumL))
                            {
                                pDoc.ReceiptNumber = recNumL;
                            }

                            //Add this document to the pawn document registry and document storage
                            string errText;
                            if (!CouchDbUtils.AddPawnDocument(dA, cC, userName, ref pDoc, out errText))
                            {
                                couchIssue = true;

                                if (FileLogger.Instance.IsLogError)
                                {
                                    FileLogger.Instance.logMessage(LogLevel.ERROR, this,
                                                                    "Could not store receipt in document storage: {0} - FileName: {1}",
                                                                    errText, fullFileName);
                                }

                                BasicExceptionHandler.Instance.AddException("Could not store receipt in document storage",
                                                                            new ApplicationException(
                                                                                    "Could not store receipt in document storage: " +
                                                                                    errText));
                            }
                        }
                    }

                    #region Indiana-Police Card
                    // New Pawn Loan + Customer Buys, Customers only
                    bool policeCardNeeded = new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).IsPoliceCardNeededForStore(GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId);
                    if (mode == ProcessTenderMode.PURCHASE &&
                        policeCardNeeded &&
                        GlobalDataAccessor.Instance.CurrentSiteId.State.Equals(States.Indiana) &&
                        GlobalDataAccessor.Instance.DesktopSession.ActiveVendor == null)
                    {
                        try
                        {
                            var cds = GlobalDataAccessor.Instance.DesktopSession;

                            //Get the loan vo
                            var currentPurchase = cds.ActivePurchase;

                            // show popup dialog
                            var pm = new Common.Libraries.Forms.ProcessingMessage("Police Card is Printing for Customer Buy # " + currentPurchase.TicketNumber.ToString(), 3000);
                            pm.ShowDialog();

                            var report = new IndianaPoliceCardReport();

                            //currentPurchase.TicketNumber = (int)ticketNumber;
                            var custAddress = cds.ActiveCustomer.getHomeAddress();
                            if (custAddress == null && cds.ActiveCustomer.CustomerAddress.Count != 0)
                            {
                                custAddress = cds.ActiveCustomer.CustomerAddress[0];
                            }
                            report.BuildDocument(currentPurchase.TicketNumber.ToString(), currentPurchase.Items, cds.ActiveCustomer, cds.CurrentSiteId.StoreName, custAddress, cds.LastIdUsed, ShopDateTime.Instance.FullShopDateTime, false);

                            if (SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled &&
                                cds.IndianaPoliceCardPrinter.IsValid)
                            {
                                report.Print(cds.IndianaPoliceCardPrinter.IPAddress, (uint)cds.IndianaPoliceCardPrinter.Port);
                            }

                            //---------------------------------------------------
                            // Place document into couch
                            const string policeCardFileName = "PoliceCard.txt";
                            var policeCardFilePath = SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseLogPath + "\\" + policeCardFileName;
                            report.Save(policeCardFilePath);

                            var pDoc = new CouchDbUtils.PawnDocInfo();
                            var cC = GlobalDataAccessor.Instance.CouchDBConnector;
                            //Set document add calls
                            pDoc.UseCurrentShopDateTime = true;
                            pDoc.SetDocumentSearchType(CouchDbUtils.DocSearchType.POLICE_CARD);
                            pDoc.StoreNumber = cds.CurrentSiteId.StoreNumber;
                            pDoc.CustomerNumber = cds.ActiveCustomer.CustomerNumber;
                            pDoc.TicketNumber = currentPurchase.TicketNumber;
                            pDoc.DocumentType = Document.DocTypeNames.TEXT;
                            pDoc.DocFileName = policeCardFilePath;
                            //Add this document to the pawn document registry and document storage
                            string errText;
                            if (!CouchDbUtils.AddPawnDocument(dA, cC, cds.UserName, ref pDoc, out errText))
                            {
                                couchIssue = true;

                                if (FileLogger.Instance.IsLogError)
                                {
                                    FileLogger.Instance.logMessage(LogLevel.ERROR, this,
                                        "Could not store police card document in document storage: {0} - FileName: {1}", errText, policeCardFilePath);
                                }

                                BasicExceptionHandler.Instance.AddException(
                                    "Could not store police card document in document storage",
                                    new ApplicationException("Could not store police card document in document storage: " + errText));
                            }
                        }
                        catch (Exception ex)
                        {
                            if (FileLogger.Instance.IsLogError)
                            {
                                FileLogger.Instance.logMessage(
                                    LogLevel.ERROR,
                                    this,
                                    "Could not print Police Card" + ex.Message);
                            }
                        }
                    }
                    #endregion

                    if (couchIssue)
                    {
                        MessageBox.Show(
                            "Transaction completed but the document could not be saved.  \n" +
                            "Please call Shop System Support.  \n",
                            "Application Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Stop);
                    }

                }
                catch (Exception eX)
                {
                    string errMsg = string.Format("Could not generate purchase receipt.  Exception thrown: {0} {1}", eX, eX.Message);
                    if (FileLogger.Instance.IsLogError)
                    {
                        FileLogger.Instance.logMessage(LogLevel.ERROR, this, errMsg);
                    }
                    BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg, eX));
                    return (false);
                }
            }

            return (true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pObject"></param>
        /// <param name="p"></param>
        /// <param name="isGunItem"></param>
        /// <param name="gunNumbers">Pair listing matching pawn item index to gun number</param>
        /// <returns></returns>
        /// 
        private static Int64 GetGunNumberForPawnItem(
            CustomerProductDataVO pObject,
            Item p,
            int index,
            List<PairType<int, Int64>> gunNumbers,
            ref bool isGunItem)
        {
            if (pObject == null || p == null || CollectionUtilities.isEmpty(pObject.Items))
            {
                isGunItem = false;
                return (-1L);
            }
            if (!p.IsGun)
            {
                isGunItem = false;
                return (-1L);
            }

            //int pawnItemIdx = pObject.Items.IndexOf(p);

            int pawnItemIdx = index;

            PairType<int, long> gunData = gunNumbers[pawnItemIdx];

            isGunItem = true;
            return (gunData.Right);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private static Int64[] getMasks(Item p)
        {
            if (p == null)
            {
                return (null);
            }

            var rt = new Int64[MAX_MASKS];
            //Initialize array with all zeroes or 
            //the proper mask value
            for (int i = 1; i <= MAX_MASKS; ++i)
            {
                rt[i - 1] = 0L;
                int i1 = i;
                int attrIdx = p.Attributes.FindIndex(itemAttrib => itemAttrib.MaskOrder == i1);
                if (attrIdx < 0)
                {
                    continue;
                }
                ItemAttribute pAttrib = p.Attributes[attrIdx];
                Int64 answerCode = pAttrib.Answer.AnswerCode;
                rt[i - 1] = answerCode;
            }
            return (rt);
        }

        private static Int64[] getMasks(JewelrySet jSet)
        {
            if (jSet == null)
            {
                return (null);
            }

            var rt = new Int64[MAX_MASKS];
            for (int i = 1; i <= MAX_MASKS; ++i)
            {
                rt[i - 1] = 0L;
                if (i > 6)
                {
                    continue;
                }
                if (jSet.ItemAttributeList.Count > 0)
                {
                    ItemAttribute pAttrib = jSet.ItemAttributeList[i - 1];
                    rt[i - 1] = pAttrib.Answer.AnswerCode;
                }
            }
            return (rt);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool performProcessTenderNewLoanInserts()
        {
            DesktopSession cds = GlobalDataAccessor.Instance.DesktopSession;
            SiteId curSiteId = cds.CurrentSiteId;
            bool retVal = false;

            lock (mutexIntObj)
            {
                //Get Pawn loan
                PawnLoan curLoan = cds.ActivePawnLoan;
                if (curLoan == null ||
                    CollectionUtilities.isEmpty(curLoan.Items) ||
                    curLoan.ObjectUnderwritePawnLoanVO == null)
                {
                    BasicExceptionHandler.Instance.AddException("Active loan is invalid", new ApplicationException());
                    FileLogger.Instance.logMessage(LogLevel.FATAL, this, "Active loan is invalid");
                    return (false);
                }

                UnderwritePawnLoanVO curLoanU =
                    (UnderwritePawnLoanVO)curLoan.ObjectUnderwritePawnLoanVO;
                int curStoreNumberVal = curLoan.Items[0].mStore;
                CustomerVO cust = cds.ActiveCustomer;
                OracleDataAccessor ods = GlobalDataAccessor.Instance.OracleDA;

                if (ods == null || ods.Initialized == false)
                {
                    BasicExceptionHandler.Instance.AddException("Oracle data accessor is invalid", new ApplicationException());
                    FileLogger.Instance.logMessage(LogLevel.FATAL, this, "Oracle data accessor is invalid");
                    return (false);
                }

                //Execute PAWN insert
                Int64 pwnAppIdVal = Int64.Parse(cds.CurPawnAppId);
                string errorCode = string.Empty;
                string errorText = string.Empty;
                try
                {
                    decimal dPrePaidCityFee = curLoan.Fees.Where(f => f.FeeType == FeeTypes.PREPAID_CITY).Sum(ppf => ppf.Value);

                    decimal dStorageFee = curLoan.Fees.Where(f => f.FeeType == FeeTypes.STORAGE).Sum(sf => sf.Value);

                    string madetime = ShopDateTime.Instance.ShopDate.ToShortDateString() + " " +
                                      ShopDateTime.Instance.ShopTime.ToString();

                    // Build 13
                    var isPFIMailersRequiredForState = new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).IsPFIMailersRequiredForState(GlobalDataAccessor.Instance.CurrentSiteId);

                    DateTime dtPFINote = curLoanU.PFINotifyDate;
                    if (!isPFIMailersRequiredForState)
                    {
                        dtPFINote = DateTime.MinValue;
                    }

                    retVal = ProcessTenderProcedures.ExecuteInsertNewPawnLoanRecord(
                        pwnAppIdVal, this.fullTicketNumber, Utilities.GetIntegerValue(GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreNumber),
                        cust.CustomerNumber, curLoanU.MadeDate,
                        Utilities.GetDateTimeValue(madetime, DateTime.Now), curLoanU.DueDate, curLoanU.PFIDate,
                        dtPFINote, curLoan.Amount, curLoan.InterestRate,
                        curLoan.InterestAmount, curLoan.ServiceCharge, 0.0M, "IP",
                        curLoan.Items.Count, cds.LoggedInUserSecurityProfile.UserName, "" + curLoan.PrintNotice,
                        IsGunInvolved(curLoan), cds.Clothing, "" + curLoan.NegotiableFinanceCharge,
                        "" + curLoan.NegotiableServiceCharge, cds.TTyId,
                        "", cds.TTyId, dPrePaidCityFee,
                        curLoan.InterestAmount, dStorageFee, 0.0M,
                        0.0M, out errorCode, out errorText);

                    this.retValues.Add(retVal);
                    this.errorCodes.Add(errorCode);
                    this.errorTexts.Add(errorText);

                    if (!retVal)
                    {
                        BasicExceptionHandler.Instance.AddException("ProcessTender.performProcessTenderInserts.ExecuteInsertNewPawnLoanRecord",
                                                                    new ApplicationException(errorText));
                        FileLogger.Instance.logMessage(LogLevel.FATAL, this, "Process tender encountered an error when inserting the new pawn loan record:" + errorText);

                        return false;
                    }
                }
                catch (Exception eX)
                {
                    BasicExceptionHandler.Instance.AddException("ProcessTender.performProcessTenderInserts.ExecuteInsertNewPawnLoanRecord",
                                                                new ApplicationException("Exception thrown", eX));
                    FileLogger.Instance.logMessage(LogLevel.FATAL, this, "Process tender encountered an exception when inserting the new pawn loan record:" + eX.Message + ", " + eX.StackTrace);

                    return (false);
                }

                //Execute MDSE insert (also MDHIST and OTHERDSC if necessary)
                if (CollectionUtilities.isEmpty(curLoan.Items))
                {
                    FileLogger.Instance.logMessage(LogLevel.FATAL, this, "Process tender cannot print data with out any loan items");
                    return (false);
                }

                Item firstPawnItem = curLoan.Items[0];
                int pawnItemIdx = 1;
                foreach (Item pItem in curLoan.Items)
                {
                    bool isGunItem = false;
                    bool isJewelryItem = false;
                    Int64 gunNumber = 0L;
                    gunNumber = GetGunNumberForPawnItem(curLoan, pItem, pawnItemIdx - 1, this.gunItemNumberIndices, ref isGunItem);
                    if (!isGunItem)
                    {
                        if (pItem.IsJewelry)
                            isJewelryItem = true;
                    }

                    try
                    {
                        QuickCheck pItemQInfo = pItem.QuickInformation;
                        Int64[] primaryMasks = getMasks(pItem);
                        ProKnowMatch pKMatch = pItem.SelectedProKnowMatch;
                        ProKnowData pKData;
                        ProCallData pCData;
                        if (pKMatch != null)
                        {
                            pKData = pKMatch.selectedPKData;
                            pCData = pKMatch.proCallData;
                        }
                        else
                        {
                            pKData.RetailAmount = 0.0M;
                            pKData.LoanAmount = 0.0M;
                            pCData.NewRetail = 0.0M;
                        }

                        //Insert MDSE record for this pawn item
                        bool curRetValue = ProcessTenderProcedures.ExecuteInsertMDSERecord(
                            firstPawnItem.mStore, firstPawnItem.mStore, firstPawnItem.mYear, this.fullTicketNumber,
                            "" + firstPawnItem.mDocType, pawnItemIdx, 0, pItem.CategoryCode,
                            cust.CustomerNumber, pItem.ItemAmount,
                            (isGunItem ? gunNumber : 0L), pItemQInfo.Manufacturer,
                            pItemQInfo.Model, pItemQInfo.SerialNumber, pItemQInfo.Weight,
                            primaryMasks, pItem.TicketDescription, pKData.RetailAmount,
                            pItem.StorageFee, cds.UserName,
                            curLoanU.MadeDate.FormatDate(), curLoanU.MadeDate.ToShortDateString() + " " + curLoanU.MadeTime.ToString(), "", "", firstPawnItem.mDocType, "", out errorCode, out errorText);

                        this.retValues.Add(curRetValue);
                        this.errorCodes.Add(errorCode);
                        this.errorTexts.Add(errorText);
                        if (curRetValue)
                        {
                            if (isGunItem)
                            {
                                //Insert gun book record
                                AddressVO custAddr = cust.getHomeAddress();
                                string custAddress = string.Empty;
                                string custCity = string.Empty;
                                string custState = string.Empty;
                                string custPostalCode = string.Empty;

                                if (custAddr != null)
                                {
                                    custAddress = custAddr.Address1 + " " + custAddr.Address2;
                                    custCity = custAddr.City;
                                    custState = custAddr.State_Code;
                                    custPostalCode = custAddr.ZipCode;
                                }

                                IdentificationVO primaryId = cust.getFirstIdentity();
                                string truncGunType = pItemQInfo.GunType;
                                if (!string.IsNullOrEmpty(truncGunType))
                                {
                                    if (truncGunType.Length > 10)
                                    {
                                        truncGunType = truncGunType.Substring(0, 10);
                                    }
                                }
                                bool gunRetVal = ProcessTenderProcedures.ExecuteInsertGunBookRecord(
                                    curSiteId.TerminalId, ShopDateTime.Instance.ShopDate, cds.UserName,
                                    0L, curSiteId.StoreNumber, firstPawnItem.mStore, firstPawnItem.mYear,
                                    this.fullTicketNumber, "" + firstPawnItem.mDocType, pawnItemIdx, 0,
                                    pItemQInfo.Manufacturer, pItemQInfo.Importer, pItemQInfo.SerialNumber,
                                    pItemQInfo.Caliber, truncGunType, pItemQInfo.Model,
                                    cust.LastName, cust.FirstName,
                                    string.IsNullOrEmpty(cust.MiddleInitial) ? "" : cust.MiddleInitial.Substring(0, 1),
                                    cust.CustomerNumber,
                                    custAddress, custCity, custState, custPostalCode, primaryId.IdType,
                                    primaryId.IdIssuerCode, primaryId.IdValue,
                                    Utilities.GetDateTimeValue(ShopDateTime.Instance.ShopTransactionTime, DateTime.Now), cds.UserName, firstPawnItem.HasGunLock ? 1 : 0, ProductStatus.IP.ToString(), out errorCode, out errorText);

                                this.retValues.Add(gunRetVal);
                                this.errorCodes.Add(errorCode);
                                this.errorTexts.Add(errorText);
                            }

                            if (pKData.LoanAmount > 0.0M)
                            {
                                bool pkRetVal = ProcessTenderProcedures.ExecuteInsertMDHistRecord(
                                    firstPawnItem.mStore, firstPawnItem.mYear, this.fullTicketNumber,
                                    "" + firstPawnItem.mDocType, pawnItemIdx, 0, ProductStatus.IP.ToString(), PROKNOWCODE,
                                    pKData.LoanAmount, pKData.LoanAmount, 0.0M, 0.0M,
                                    cds.UserName, curLoanU.MadeDate, out errorCode, out errorText);

                                this.retValues.Add(pkRetVal);
                                this.errorCodes.Add(errorCode);
                                this.errorTexts.Add(errorText);
                            }

                            if (pKData.RetailAmount > 0.0M)
                            {
                                bool pkrRetVal = ProcessTenderProcedures.ExecuteInsertMDHistRecord(
                                    firstPawnItem.mStore, firstPawnItem.mYear, this.fullTicketNumber,
                                    "" + firstPawnItem.mDocType, pawnItemIdx, 0, ProductStatus.IP.ToString(), PROKNOWRETAILCODE,
                                    0.0M, 0.0M, pKData.RetailAmount, 0.0M,
                                    cds.UserName, curLoanU.MadeDate, out errorCode, out errorText);

                                this.retValues.Add(pkrRetVal);
                                this.errorCodes.Add(errorCode);
                                this.errorTexts.Add(errorText);
                            }

                            if (pCData.NewRetail > 0.0M)
                            {
                                bool pcRetVal = ProcessTenderProcedures.ExecuteInsertMDHistRecord(
                                    firstPawnItem.mStore, firstPawnItem.mYear, this.fullTicketNumber,
                                    "" + firstPawnItem.mDocType, pawnItemIdx, 0, ProductStatus.IP.ToString(), PROCALLCODE,
                                    0.0M, 0.0M, 0.0M, pCData.NewRetail,
                                    cds.UserName, curLoanU.MadeDate, out errorCode, out errorText);

                                this.retValues.Add(pcRetVal);
                                this.errorCodes.Add(errorCode);
                                this.errorTexts.Add(errorText);
                            }

                            //Check answers on main pawn item for otherdsc insert
                            foreach (ItemAttribute iAttr in pItem.Attributes)
                            {
                                if (iAttr.Answer.AnswerCode == OTHERDSC_CODE)
                                {
                                    bool otherDscVal = ProcessTenderProcedures.ExecuteInsertOtherDscRecord(
                                        GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                                        firstPawnItem.mStore, firstPawnItem.mYear, this.fullTicketNumber,
                                        "" + firstPawnItem.mDocType, pawnItemIdx, 0, iAttr.MaskOrder,
                                        iAttr.Answer.AnswerText, cds.UserName, out errorCode, out errorText);

                                    this.retValues.Add(otherDscVal);
                                    this.errorCodes.Add(errorCode);
                                    this.errorTexts.Add(errorText);
                                }
                            }

                            //Check comment
                            if (!string.IsNullOrEmpty(pItem.Comment))
                            {
                                bool otherDscVal = ProcessTenderProcedures.ExecuteInsertOtherDscRecord(
                                    GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                                    firstPawnItem.mStore, firstPawnItem.mYear, this.fullTicketNumber,
                                    "" + firstPawnItem.mDocType, pawnItemIdx, 0, OTHERDSC_CODE,
                                    pItem.Comment, cds.UserName, out errorCode, out errorText);

                                this.retValues.Add(otherDscVal);
                                this.errorCodes.Add(errorCode);
                                this.errorTexts.Add(errorText);
                            }

                            //If jewelry item, execute the MDSE insert for all jewelry pieces
                            if (isJewelryItem)
                            {
                                if (pItem.TotalLoanGoldValue > 0.0M)
                                {
                                    //Insert pmetal value
                                    bool pMetalVal = ProcessTenderProcedures.ExecuteInsertMDHistRecord(
                                        firstPawnItem.mStore, firstPawnItem.mYear, this.fullTicketNumber,
                                        "" + firstPawnItem.mDocType, pawnItemIdx, 0, ProductStatus.IP.ToString(), PMETALCODE,
                                        0.0M, 0.0M, 0.0M, pItem.TotalLoanGoldValue,
                                        cds.UserName, curLoanU.MadeDate,
                                        out errorCode, out errorText);

                                    this.retValues.Add(pMetalVal);
                                    this.errorCodes.Add(errorCode);
                                    this.errorTexts.Add(errorText);
                                }

                                if (pItem.TotalLoanStoneValue > 0.0M)
                                {
                                    //Insert stone value
                                    bool stoneVal = ProcessTenderProcedures.ExecuteInsertMDHistRecord(
                                        firstPawnItem.mStore, firstPawnItem.mYear, this.fullTicketNumber,
                                        "" + firstPawnItem.mDocType, pawnItemIdx, 0, ProductStatus.IP.ToString(), STONECODE,
                                        0.0M, 0.0M, 0.0M, pItem.TotalLoanStoneValue,
                                        cds.UserName, curLoanU.MadeDate,
                                        out errorCode, out errorText);

                                    this.retValues.Add(stoneVal);
                                    this.errorCodes.Add(errorCode);
                                    this.errorTexts.Add(errorText);
                                }
                                //Perform jewelry set inserts
                                List<JewelrySet> jSets = pItem.Jewelry;

                                if (jSets.Count > 0)
                                {
                                    for (int subItemNum = 1; subItemNum <= jSets.Count; ++subItemNum)
                                    {
                                        JewelrySet curJSet = jSets[subItemNum - 1];
                                        if (curJSet == null)
                                        {
                                            continue;
                                        }

                                        Int64[] curJSetMasks = getMasks(curJSet);
                                        //Fix to prevent additional mdse record for jewelry with only one sub item
                                        if (curJSetMasks.Length > 0 && curJSetMasks[0] == 0)
                                        {
                                            continue;
                                        }

                                        bool jRetVal = false;

                                        jRetVal = ProcessTenderProcedures.ExecuteInsertMDSERecord(
                                            firstPawnItem.mStore, firstPawnItem.mStore, firstPawnItem.mYear, this.fullTicketNumber,
                                            "" + firstPawnItem.mDocType, pawnItemIdx, subItemNum, pItem.CategoryCode,
                                            cust.CustomerNumber, pItem.ItemAmount,
                                            0L, pItemQInfo.Manufacturer,
                                            pItemQInfo.Model, pItemQInfo.SerialNumber, pItemQInfo.Weight,
                                            curJSetMasks, curJSet.TicketDescription,
                                            pKData.RetailAmount,
                                            pItem.StorageFee, cds.UserName,
                                            curLoanU.MadeDate.FormatDate(), curLoanU.MadeDate.ToShortDateString() + " " + curLoanU.MadeTime.ToString(), "", "", firstPawnItem.mDocType, "", out errorCode, out errorText);

                                        this.retValues.Add(jRetVal);
                                        this.errorCodes.Add(errorCode);
                                        this.errorTexts.Add(errorText);

                                        foreach (ItemAttribute iAttr in curJSet.ItemAttributeList)
                                        {
                                            if (iAttr.Answer.AnswerCode == OTHERDSC_CODE)
                                            {
                                                bool otherJDscVal = ProcessTenderProcedures.ExecuteInsertOtherDscRecord(
                                                    GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                                                    firstPawnItem.mStore, firstPawnItem.mYear, this.fullTicketNumber,
                                                    "" + firstPawnItem.mDocType, pawnItemIdx, subItemNum, iAttr.MaskOrder,
                                                    iAttr.Answer.AnswerText, cds.UserName, out errorCode, out errorText);

                                                this.retValues.Add(otherJDscVal);
                                                this.errorCodes.Add(errorCode);
                                                this.errorTexts.Add(errorText);
                                            }
                                        }
                                    }
                                }
                            }

                            pawnItemIdx++;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    catch (Exception eX)
                    {
                        var sB = new StringBuilder("Massive failure listing");
                        if (CollectionUtilities.isNotEmpty(this.errorCodes) && CollectionUtilities.isNotEmpty(this.errorTexts))
                        {
                            int i = 0;
                            foreach (string s in this.errorCodes)
                            {
                                sB.AppendFormat(
                                    "{0} - ErrorCode = {1} ErrorText = {2}",
                                    i,
                                    s,
                                    this.errorTexts[i]);
                            }

                            BasicExceptionHandler.Instance.AddException("The whole world came to an end", new ApplicationException("Massive Fatal Exception", eX));
                            FileLogger.Instance.logMessage(LogLevel.FATAL, this, "Beyond fatal insert statement failure...");
                            return (false);
                        }
                    }
                }
            }

            return (true);
        }

        private bool performProcessTenderPurchaseInserts()
        {
            DesktopSession cds = GlobalDataAccessor.Instance.DesktopSession;
            SiteId curSiteId = cds.CurrentSiteId;
            bool retVal = false;

            lock (mutexIntObj)
            {
                //Get purchase object
                PurchaseVO curPurchase = GlobalDataAccessor.Instance.DesktopSession.Purchases[0];
                if (curPurchase == null ||
                    CollectionUtilities.isEmpty(curPurchase.Items))
                {
                    BasicExceptionHandler.Instance.AddException("Active purchase object is invalid", new ApplicationException());
                    FileLogger.Instance.logMessage(LogLevel.FATAL, this, "Active purchase object is invalid");
                    return (false);
                }

                CustomerVO cust = cds.ActiveCustomer;
                OracleDataAccessor ods = GlobalDataAccessor.Instance.OracleDA;
                if (ods == null || ods.Initialized == false)
                {
                    BasicExceptionHandler.Instance.AddException("Oracle data accessor is invalid", new ApplicationException());
                    FileLogger.Instance.logMessage(LogLevel.FATAL, this, "Oracle data accessor is invalid");
                    return (false);
                }

                //Execute purchase header insert
                string errorCode;
                string errorText;
                int purchaseTktNumber;
                try
                {
                    string pfiDate = new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).GetValidPFIDateWithWaitdaysForBuy(ShopDateTime.Instance.ShopDate,
                                                                                               GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId);

                    retVal = PurchaseProcedures.InsertPurchaseRecord(PurchaseTypes.CUSTOMER.ToString(), GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreNumber,
                                                                     cust.CustomerNumber, "", "", pfiDate, curPurchase.Amount, 0, ProductStatus.PUR.ToString(),
                                                                     ShopDateTime.Instance.ShopDate.ToShortDateString(), ShopDateTime.Instance.ShopTransactionTime.ToString(),
                                                                     cds.LoggedInUserSecurityProfile.UserName, cds.CashDrawerName, ShopDateTime.Instance.ShopDate.ToShortDateString(),
                                                                     curPurchase.Amount, curPurchase.Items.Count, curPurchase.PurchaseOrderNumber, "",
                                                                     curPurchase.Freight, curPurchase.SalesTax, curPurchase.ManualTicketNumber, curPurchase.MiscFlags,
                                                                     cds.TTyId, "", "", "", "", out purchaseTktNumber,
                                                                     out errorCode, out errorText);
                    this.retValues.Add(retVal);

                    if (!retVal)
                    {
                        this.errorCodes.Add(errorCode);
                        this.errorTexts.Add(errorText);
                        BasicExceptionHandler.Instance.AddException("ProcessTender.performProcessTenderInserts.InsertPurchaseRecord",
                                                                    new ApplicationException(errorText));
                        FileLogger.Instance.logMessage(LogLevel.FATAL, this, "Process tender encountered an error when inserting the purchase header record:" + errorText);

                        return false;
                    }
                    purchaseNumber = purchaseTktNumber;
                }
                catch (Exception eX)
                {
                    BasicExceptionHandler.Instance.AddException("ProcessTender.performProcessTenderInserts.InsertPurchaseRecord",
                                                                new ApplicationException("Exception thrown", eX));
                    FileLogger.Instance.logMessage(LogLevel.FATAL, this, "Process tender encountered an exception when inserting the purchase header record:" + eX.Message + ", " + eX.StackTrace);

                    return (false);
                }

                //Execute MDSE insert (also MDSEREV and OTHERDSC if necessary)
                if (CollectionUtilities.isEmpty(curPurchase.Items))
                {
                    FileLogger.Instance.logMessage(LogLevel.FATAL, this, "Process tender cannot print data with out any purchase items");
                    return (false);
                }

                int pawnItemIdx = 1;
                foreach (Item pItem in curPurchase.Items)
                {
                    bool isGunItem = false;
                    bool isJewelryItem = false;
                    for (int i = 0; i < pItem.Quantity; i++)
                    {
                        Int64 gunNumber = 0L;
                        gunNumber = GetGunNumberForPawnItem(curPurchase, pItem, pawnItemIdx - 1, this.gunItemNumberIndices, ref isGunItem);
                        if (!isGunItem)
                        {
                            if (pItem.IsJewelry)
                                isJewelryItem = true;
                        }

                        try
                        {
                            QuickCheck pItemQInfo = pItem.QuickInformation;
                            Int64[] primaryMasks = getMasks(pItem);
                            ProKnowMatch pKMatch = pItem.SelectedProKnowMatch;
                            ProKnowData pKData;
                            ProCallData pCData;
                            if (pKMatch != null)
                            {
                                pKData = pKMatch.selectedPKData;
                                pCData = pKMatch.proCallData;
                            }
                            else
                            {
                                pKData.RetailAmount = 0.0M;
                                pKData.LoanAmount = 0.0M;
                                pCData.NewRetail = 0.0M;
                                pKData.PurchaseAmount = 0.0M;
                            }

                            //Insert MDSE record for this pawn item
                            //populate the item object with the data that is going into the database
                            string icn = Utilities.IcnGenerator(pItem.mStore, pItem.mYear, purchaseTktNumber,
                                                                pItem.mDocType, pawnItemIdx, 0);
                            pItem.Icn = icn;
                            pItem.GunNumber = isGunItem ? gunNumber : 0L;
                            //pItem.RetailPrice = pKData.RetailAmount;
                            bool curRetValue = ProcessTenderProcedures.ExecuteInsertMDSERecord(
                                pItem.mStore, pItem.mStore, pItem.mYear, purchaseTktNumber,
                                pItem.mDocType, pawnItemIdx, 0, pItem.CategoryCode,
                                cust.CustomerNumber, pItem.ItemAmount,
                                (isGunItem ? gunNumber : 0L), pItemQInfo.Manufacturer,
                                pItemQInfo.Model, pItemQInfo.SerialNumber, pItemQInfo.Weight,
                                primaryMasks, pItem.TicketDescription, pKData.RetailAmount,
                                pItem.StorageFee, cds.UserName,
                                ShopDateTime.Instance.ShopDate.ToShortDateString(), ShopDateTime.Instance.ShopTransactionTime.ToString(), curPurchase.EntityType, curPurchase.EntityNumber, pItem.mDocType, "", out errorCode, out errorText);
                            this.retValues.Add(curRetValue);
                            if (!curRetValue)
                            {
                                this.errorCodes.Add(errorCode);
                                this.errorTexts.Add(errorText);
                            }

                            if (curRetValue)
                            {
                                pItem.mDocNumber = purchaseTktNumber;
                                pItem.mItemOrder = pawnItemIdx;

                                if (isGunItem)
                                {
                                    //Insert gun book record
                                    AddressVO custAddr = cust.getHomeAddress();
                                    string custAddress = string.Empty;
                                    string custCity = string.Empty;
                                    string custState = string.Empty;
                                    string custPostalCode = string.Empty;
                                    if (custAddr != null)
                                    {
                                        custAddress = custAddr.Address1 + " " + custAddr.Address2;
                                        custCity = custAddr.City;
                                        custState = custAddr.State_Code;
                                        custPostalCode = custAddr.ZipCode;
                                    }
                                    IdentificationVO primaryId = cust.getFirstIdentity();
                                    string truncGunType = pItemQInfo.GunType;
                                    if (!string.IsNullOrEmpty(truncGunType))
                                    {
                                        if (truncGunType.Length > 10)
                                        {
                                            truncGunType = truncGunType.Substring(0, 10);
                                        }
                                    }
                                    bool gunRetVal = ProcessTenderProcedures.ExecuteInsertGunBookRecord(
                                        curSiteId.TerminalId, ShopDateTime.Instance.ShopDate, cds.UserName,
                                        0L, curSiteId.StoreNumber, pItem.mStore, pItem.mYear,
                                        purchaseTktNumber, pItem.mDocType.ToString(), pawnItemIdx, 0,
                                        pItemQInfo.Manufacturer, pItemQInfo.Importer, pItemQInfo.SerialNumber,
                                        pItemQInfo.Caliber, truncGunType, pItemQInfo.Model,
                                        cust.LastName, cust.FirstName,
                                        string.IsNullOrEmpty(cust.MiddleInitial) ? "" : cust.MiddleInitial.Substring(0, 1),
                                        cust.CustomerNumber,
                                        custAddress, custCity, custState, custPostalCode, primaryId.IdType,
                                        primaryId.IdIssuerCode, primaryId.IdValue,
                                        Utilities.GetDateTimeValue(ShopDateTime.Instance.ShopTransactionTime, DateTime.Now),
                                        cds.UserName, pItem.HasGunLock ? 1 : 0,
                                        ProductStatus.PUR.ToString(), out errorCode, out errorText);
                                    this.retValues.Add(gunRetVal);
                                    if (!gunRetVal)
                                    {
                                        this.errorCodes.Add(errorCode);
                                        this.errorTexts.Add(errorText);
                                    }
                                }

                                if (pKData.RetailAmount > 0.0M)
                                {
                                    bool pkrRetVal = ProcessTenderProcedures.ExecuteInsertMDHistRecord(
                                        pItem.mStore, pItem.mYear, purchaseTktNumber,
                                        "" + pItem.mDocType, pawnItemIdx, 0, ProductStatus.PUR.ToString(), PROKNOWRETAILCODE,
                                        0.0M, 0.0M, pKData.RetailAmount, 0.0M,
                                        cds.UserName, ShopDateTime.Instance.ShopDate, out errorCode, out errorText);
                                    this.retValues.Add(pkrRetVal);

                                    if (!pkrRetVal)
                                    {
                                        this.errorCodes.Add(errorCode);
                                        this.errorTexts.Add(errorText);
                                    }
                                }
                                if (pKData.PurchaseAmount > 0.0M)
                                {
                                    bool pkrRetVal = ProcessTenderProcedures.ExecuteInsertMDHistRecord(
                                        pItem.mStore, pItem.mYear, purchaseTktNumber,
                                        pItem.mDocType, pawnItemIdx, 0, ProductStatus.PUR.ToString(), PROKNOWCODE,
                                        0.0M, 0.0M, pKData.RetailAmount, 0.0M,
                                        cds.UserName, ShopDateTime.Instance.ShopDate, out errorCode, out errorText);
                                    this.retValues.Add(pkrRetVal);
                                    if (!pkrRetVal)
                                    {
                                        this.errorCodes.Add(errorCode);
                                        this.errorTexts.Add(errorText);
                                    }
                                }

                                //Check answers on main pawn item for otherdsc insert
                                foreach (ItemAttribute iAttr in pItem.Attributes)
                                {
                                    if (iAttr.Answer.AnswerCode == OTHERDSC_CODE)
                                    {
                                        bool otherDscVal = ProcessTenderProcedures.ExecuteInsertOtherDscRecord(
                                            GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                                            pItem.mStore, pItem.mYear, purchaseTktNumber,
                                            pItem.mDocType, pawnItemIdx, 0, iAttr.MaskOrder,
                                            iAttr.Answer.AnswerText, cds.UserName, out errorCode, out errorText);
                                        this.retValues.Add(otherDscVal);
                                        if (!otherDscVal)
                                        {
                                            this.errorCodes.Add(errorCode);
                                            this.errorTexts.Add(errorText);
                                        }
                                    }
                                }

                                //Check comment
                                if (!string.IsNullOrEmpty(pItem.Comment))
                                {
                                    bool otherDscVal = ProcessTenderProcedures.ExecuteInsertOtherDscRecord(
                                        GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                                        pItem.mStore, pItem.mYear, purchaseTktNumber,
                                        pItem.mDocType, pawnItemIdx, 0, OTHERDSC_CODE,
                                        pItem.Comment, cds.UserName, out errorCode, out errorText);
                                    this.retValues.Add(otherDscVal);
                                    if (!otherDscVal)
                                    {
                                        this.errorCodes.Add(errorCode);
                                        this.errorTexts.Add(errorText);
                                    }
                                }

                                //If jewelry item, execute the MDSE insert for all jewelry pieces
                                if (isJewelryItem)
                                {
                                    //Perform jewelry set inserts
                                    List<JewelrySet> jSets = pItem.Jewelry;

                                    if (jSets.Count > 0)
                                    {
                                        for (int subItemNum = 1; subItemNum <= jSets.Count; ++subItemNum)
                                        {
                                            JewelrySet curJSet = jSets[subItemNum - 1];
                                            if (curJSet == null)
                                            {
                                                continue;
                                            }

                                            Int64[] curJSetMasks = getMasks(curJSet);
                                            //Fix to prevent additional mdse record for jewelry with only one sub item
                                            if (curJSetMasks.Length > 0 && curJSetMasks[0] == 0)
                                                continue;
                                            bool jRetVal = false;
                                            jRetVal = ProcessTenderProcedures.ExecuteInsertMDSERecord(
                                                pItem.mStore, pItem.mStore, pItem.mYear, purchaseTktNumber,
                                                pItem.mDocType, pawnItemIdx, subItemNum, pItem.CategoryCode,
                                                cust.CustomerNumber, pItem.ItemAmount,
                                                0L, pItemQInfo.Manufacturer,
                                                pItemQInfo.Model, pItemQInfo.SerialNumber, pItemQInfo.Weight,
                                                curJSetMasks, curJSet.TicketDescription,
                                                pKData.RetailAmount,
                                                pItem.StorageFee, cds.UserName,
                                                ShopDateTime.Instance.ShopDate.ToShortDateString(), ShopDateTime.Instance.ShopTransactionTime.ToString(), "", "", pItem.mDocType, "", out errorCode, out errorText);
                                            this.retValues.Add(jRetVal);
                                            if (!jRetVal)
                                            {
                                                this.errorCodes.Add(errorCode);
                                                this.errorTexts.Add(errorText);
                                            }

                                            foreach (ItemAttribute iAttr in curJSet.ItemAttributeList)
                                            {
                                                if (iAttr.Answer.AnswerCode == OTHERDSC_CODE)
                                                {
                                                    bool otherJDscVal = ProcessTenderProcedures.ExecuteInsertOtherDscRecord(
                                                        GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                                                        pItem.mStore, pItem.mYear, purchaseTktNumber,
                                                        pItem.mDocType, pawnItemIdx, subItemNum, iAttr.MaskOrder,
                                                        iAttr.Answer.AnswerText, cds.UserName, out errorCode, out errorText);
                                                    this.retValues.Add(otherJDscVal);
                                                    if (!otherJDscVal)
                                                    {
                                                        this.errorCodes.Add(errorCode);
                                                        this.errorTexts.Add(errorText);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                //Insert merchandise revision record
                                bool mdseRevVal = MerchandiseProcedures.InsertMerchandiseRevision(GlobalDataAccessor.Instance.DesktopSession,
                                    GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreNumber,
                                    pItem.mYear,
                                    purchaseTktNumber,
                                    pItem.mDocType,
                                    pawnItemIdx,
                                    0,
                                    pItem.mStore,
                                    purchaseTktNumber.ToString(),
                                    pItem.mDocType,
                                    PURCHASEMDSEREVMRDESC,
                                    //pKData.PurchaseAmount,
                                    pItem.ItemAmount * pItem.QuickInformation.Quantity,
                                    CUSTPURCHASEMDSEREVMRTYPE,
                                    "",
                                    "",
                                    cds.UserName,
                                    out errorCode,
                                    out errorText);
                                if (!mdseRevVal)
                                {
                                    this.errorCodes.Add(errorCode);
                                    this.errorTexts.Add(errorText);
                                }

                                GlobalDataAccessor.Instance.DesktopSession.PrintTags(pItem, CurrentContext.READ_ONLY);
                                pawnItemIdx++;
                            }
                        }
                        catch (Exception eX)
                        {
                            this.errorTexts.Add(eX.Message);
                        }
                    }
                }
                var sB = new StringBuilder("Massive failure listing");
                if (CollectionUtilities.isNotEmpty(this.errorCodes) && CollectionUtilities.isNotEmpty(this.errorTexts))
                {
                    const int i = 0;
                    foreach (string s in this.errorCodes)
                    {
                        sB.AppendFormat(
                            "{0} - ErrorCode = {1} ErrorText = {2}",
                            i,
                            s,
                            this.errorTexts[i]);
                    }

                    FileLogger.Instance.logMessage(LogLevel.FATAL, this, "Beyond fatal insert statement failure...");
                    BasicExceptionHandler.Instance.AddException("Error inserting purchase data", new ApplicationException("Massive Fatal Exception"));
                    return (false);
                }
            }
            return (true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pLoans"></param>
        /// <returns></returns>
        private bool updateTeller(List<PawnLoan> pLoans)
        {
            DesktopSession cds = GlobalDataAccessor.Instance.DesktopSession;
            SiteId curSiteId = cds.CurrentSiteId;
            bool rtVal = false;
            lock (mutexIntObj)
            {
                foreach (PawnLoan pLoan in pLoans)
                {
                    string errorCode;
                    string errorText;
                    //If the service done on the loan was an extension
                    //update the teller with the extension amount
                    if (pLoan.IsExtended && pLoan.ExtensionAmount > 0 && pLoan.TempStatus == StateStatus.E)
                    {
                        try
                        {
                            rtVal = TellerProcedures.ExecuteUpdateTeller(
                                curSiteId.StoreNumber,
                                cds.FullUserName.ToLowerInvariant(),
                                true,
                                cds.CashDrawerName,
                                pLoan.ExtensionAmount,
                                "" + this.ReceiptNumber,
                                curSiteId.TerminalId,
                                ShopDateTime.Instance.ShopDate,
                                out errorCode,
                                out errorText);
                        }
                        catch (Exception eX)
                        {
                            BasicExceptionHandler.Instance.AddException(
                                "Update teller insert has failed!!",
                                new ApplicationException("Exception", eX));
                            FileLogger.Instance.logMessage(
                                LogLevel.FATAL, this, "Teller stored proc failed!!!");
                            return (false);
                        }
                    }
                    else
                    {
                        try
                        {
                            //Loan should be fully status'd before coming here
                            switch (pLoan.TempStatus)
                            {
                                case StateStatus.IP:
                                    rtVal = TellerProcedures.ExecuteUpdateTeller(
                                        curSiteId.StoreNumber,
                                        cds.FullUserName.ToLowerInvariant(),
                                        false,
                                        cds.CashDrawerName,
                                        pLoan.Amount,
                                        "" + this.ReceiptNumber,
                                        curSiteId.TerminalId,
                                        ShopDateTime.Instance.ShopDate,
                                        out errorCode,
                                        out errorText);
                                    break;
                                case StateStatus.P:
                                    rtVal = TellerProcedures.ExecuteUpdateTeller(
                                        curSiteId.StoreNumber,
                                        cds.FullUserName.ToLowerInvariant(),
                                        pLoan.PickupAmount > 0 ? true : false,
                                        cds.CashDrawerName,
                                        Math.Abs(pLoan.PickupAmount),
                                        "" + this.ReceiptNumber,
                                        curSiteId.TerminalId,
                                        ShopDateTime.Instance.ShopDate,
                                        out errorCode,
                                        out errorText);
                                    break;
                                case StateStatus.RN:
                                    rtVal = TellerProcedures.ExecuteUpdateTeller(
                                        curSiteId.StoreNumber,
                                        cds.FullUserName.ToLowerInvariant(),
                                        pLoan.RenewalAmount > 0 ? true : false,
                                        cds.CashDrawerName,
                                        Math.Abs(pLoan.RenewalAmount),
                                        "" + this.ReceiptNumber,
                                        curSiteId.TerminalId,
                                        ShopDateTime.Instance.ShopDate,
                                        out errorCode,
                                        out errorText);
                                    break;
                                case StateStatus.PD:
                                    rtVal = TellerProcedures.ExecuteUpdateTeller(
                                        curSiteId.StoreNumber,
                                        cds.FullUserName.ToLowerInvariant(),
                                        pLoan.PaydownAmount > 0 ? true : false,
                                        cds.CashDrawerName,
                                        Math.Abs(pLoan.PaydownAmount),
                                        "" + this.ReceiptNumber,
                                        curSiteId.TerminalId,
                                        ShopDateTime.Instance.ShopDate,
                                        out errorCode,
                                        out errorText);
                                    break;
                                default:
                                    return (false);
                            }
                        }
                        catch (Exception eX)
                        {
                            BasicExceptionHandler.Instance.AddException(
                                "Update teller insert has failed!!",
                                new ApplicationException("Exception", eX));
                            FileLogger.Instance.logMessage(
                                LogLevel.FATAL, this, "Teller stored proc failed!!!");
                            return (false);
                        }
                    }
                    this.retValues.Add(rtVal);
                    this.errorCodes.Add(errorCode);
                    this.errorTexts.Add(errorText);
                }
            }
            return (rtVal);
        }

        private bool beginProcessTenderTransaction(string section)
        {
            //Start transaction block
            bool finished = false;
            while (!finished)
            {
                try
                {
                    GlobalDataAccessor.Instance.DesktopSession.beginTransactionBlock();
                    finished = true;
                }
                catch (Exception eX)
                {
                    DialogResult dR = MessageBox.Show("Cannot start loan database transaction (" + section + "). Please retry or cancel", "Process Tender", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    if (dR == DialogResult.Cancel)
                    {
                        if (FileLogger.Instance.IsLogFatal)
                        {
                            FileLogger.Instance.logMessage(LogLevel.FATAL, ProcessTenderController.Instance, "User chose to cancel process tender operation (" + section + "). No loan data has been committed");
                        }
                        BasicExceptionHandler.Instance.AddException("User cancelled process tender (" + section + ").  No loan data has been committed", eX);
                        break;
                    }
                }
            }
            return (finished);
        }

        private bool commitProcessTenderTransaction(string section)
        {
            bool finished = false;
            while (!finished)
            {
                try
                {
                    GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.COMMIT);
                    finished = true;
                }
                catch (Exception eX)
                {
                    DialogResult dR = MessageBox.Show("Cannot put loan data into database (" + section + "). Please retry or cancel", "Process Tender", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    if (dR == DialogResult.Cancel)
                    {
                        GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                        if (FileLogger.Instance.IsLogFatal)
                        {
                            FileLogger.Instance.logMessage(LogLevel.FATAL, ProcessTenderController.Instance, "User chose to cancel process tender operation (" + section + "). No loan data has been committed");
                        }
                        BasicExceptionHandler.Instance.AddException("User cancelled process tender (" + section + ").  No loan data has been committed", eX);
                        break;
                    }
                }
            }
            return (finished);
        }

        /// <summary>
        /// Inserts a new pawn loan receipt record
        /// </summary>
        /// <returns></returns>
        private bool insertNewPawnLoanReceipt()
        {
            DesktopSession cds = GlobalDataAccessor.Instance.DesktopSession;
            SiteId curSiteId = cds.CurrentSiteId;
            string errorCode = string.Empty;
            string errorText = string.Empty;
            bool rtVal = false;
            long rcptNum;

            lock (mutexIntObj)
            {
                try
                {
                    // we only need 1 string per array for a new pawn loan
                    string[] refDate = new string[1];
                    string[] refNumber = new string[1];
                    string[] refType = new string[1];
                    string[] refEvent = new string[1];
                    string[] refAmount = new string[1];
                    string[] refStore = new string[1];
                    string[] refTime = new string[1];
                    UnderwritePawnLoanVO curLoanU =
                    (UnderwritePawnLoanVO)cds.ActivePawnLoan.ObjectUnderwritePawnLoanVO;

                    // ref date for new pawn loan is the date made
                    refDate[0] = curLoanU.MadeDate.Date.ToShortDateString();

                    //ref time for new pawn loan is the current time
                    refTime[0] = ShopDateTime.Instance.ShopTransactionTime;

                    // ref number for new pawn loan is the ticket number
                    refNumber[0] = "" + this.fullTicketNumber;

                    // ref type for new pawn loan is "PAWN" or "1" in the DB
                    refType[0] = "1";

                    // ref event for new pawn loan is "NEW"
                    refEvent[0] = "New";

                    // ref amount for new pawn loan is the loan amount
                    refAmount[0] = cds.ActivePawnLoan.Amount.ToString();

                    // ref store for new pawn loan is the store the receipt was printed at
                    refStore[0] = curSiteId.StoreNumber;
                    rtVal = ProcessTenderProcedures.ExecuteInsertReceiptDetails(
                        curSiteId.StoreNumber, cds.UserName, ShopDateTime.Instance.ShopDate.ToShortDateString(),
                        cds.FullUserName, refDate, refTime, refNumber, refType, refEvent, refAmount, refStore,
                        out rcptNum, out errorCode, out errorText);
                    if (rtVal)
                    {
                        this.ReceiptNumber = rcptNum;
                    }
                    else
                    {
                        BasicExceptionHandler.Instance.AddException("Could not insert pawn loan receipt", new ApplicationException("Could not insert pawn loan receipt"));
                        FileLogger.Instance.logMessage(LogLevel.FATAL, this, "Could not insert receipt");
                        return (false);
                    }
                }
                catch (Exception eX)
                {
                    BasicExceptionHandler.Instance.AddException("Insert new pawn loan receipt has failed!!", new ApplicationException("Exception", eX));
                    FileLogger.Instance.logMessage(LogLevel.FATAL, this, "Insert Receipt stored proc failed!!!");
                    return (false);
                }

                this.retValues.Add(rtVal);
                this.errorCodes.Add(errorCode);
                this.errorTexts.Add(errorText);
            }
            return (rtVal);
        }

        /// <summary>
        /// Inserts a void pawn loan receipt record
        /// </summary>
        /// <returns></returns>
        /*  private bool insertVoidNewPawnLoanReceipt(PawnLoan pVo, ref ReceiptDetailsVO rDVO)
        {
        DesktopSession cds = GlobalDataAccessor.Instance.DesktopSession;
        SiteId curSiteId = cds.CurrentSiteId;
        bool rtVal = false;
        //long rcptNum;

        lock (mutexIntObj)
        {
        string errorCode;
        string errorText;
        try
        {
        // ref date for void receipt is the date made
        rDVO.RefDates.Add(pVo.DateMade.Date.ToShortDateString());

        //add ref time
        rDVO.RefTimes.Add(pVo.MadeTime.ToString());

        // ref number for new pawn loan is the ticket number
        rDVO.RefNumbers.Add("" + pVo.TicketNumber);

        // ref type for new pawn loan is "PAWN" which is "1" in the DB
        rDVO.RefTypes.Add("1");

        // ref event for void pawn loan is "Void"
        rDVO.RefEvents.Add("Void");

        // ref amount for pawn loan is the loan amount
        rDVO.RefAmounts.Add(pVo.Amount.ToString());

        // ref store for pawn loan is the store the receipt was printed at
        rDVO.RefStores.Add(curSiteId.StoreNumber);
        rtVal = ProcessTenderProcedures.ExecuteInsertReceiptDetails(
        curSiteId.StoreNumber, cds.UserName, ShopDateTime.Instance.ShopDate.ToShortDateString(),
        cds.UserName, ref rDVO, out errorCode, out errorText);
        long.TryParse(rDVO.ReceiptNumber, out this.receiptNumber);
        if (!rtVal)
        {
        BasicExceptionHandler.Instance.AddException("Could not insert void pawn loan receipt", new ApplicationException("Could not insert void pawn loan receipt"));
        FileLogger.Instance.logMessage(LogLevel.FATAL, this, "Could not insert void receipt");
        return (false);
        }
        }
        catch (Exception eX)
        {
        BasicExceptionHandler.Instance.AddException("Insert void pawn loan receipt has failed!!", new ApplicationException("Exception", eX));
        FileLogger.Instance.logMessage(LogLevel.FATAL, this, "Insert Receipt stored proc failed!!!");
        return (false);
        }

        this.retValues.Add(rtVal);
        this.errorCodes.Add(errorCode);
        this.errorTexts.Add(errorText);
        }
        return (rtVal);
        }*/

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool insertServicePawnLoanReceipt(List<PawnLoan> pVos, ref ReceiptDetailsVO rDVO)
        {
            DesktopSession cds = GlobalDataAccessor.Instance.DesktopSession;
            SiteId curSiteId = cds.CurrentSiteId;
            List<string> processedTicket = new List<string>();
            try
            {
                rDVO.ReceiptDate = ShopDateTime.Instance.ShopDate;

                foreach (PawnLoan pVo in pVos)
                {
                    var tktfound = (from pTicket in processedTicket
                                    where pTicket == pVo.TicketNumber.ToString()
                                    select pTicket).FirstOrDefault();
                    if (tktfound == null)
                    {
                        processedTicket.Add(pVo.TicketNumber.ToString());

                        // ref date for service loan is shop date and time and the time zone
                        //rDVO.RefDates.Add(rDVO.ReceiptDate.FormatDate());
                        //ref time
                        //rDVO.RefTimes.Add(rDVO.ReceiptDate.FormatDateAsTimestampWithTimeZone());
                        rDVO.RefDates.Add(ShopDateTime.Instance.ShopDate.ToShortDateString());

                        //ref time
                        rDVO.RefTimes.Add(ShopDateTime.Instance.ShopDate.ToShortDateString() + " " +
                                          ShopDateTime.Instance.ShopTime.ToString());

                        // ref number for service pawn loan is the ticket number
                        rDVO.RefNumbers.Add("" + pVo.TicketNumber);

                        // ref type for new pawn loan is "PAWN" which is "1" in the db
                        rDVO.RefTypes.Add("1");

                        // ref event for Pickup is "Pickup"
                        switch (pVo.TempStatus)
                        {
                            case StateStatus.P:
                                rDVO.RefEvents.Add(ReceiptEventTypes.Pickup.ToString());
                                // ref amount for pickup service will be pickup amount
                                rDVO.RefAmounts.Add(pVo.PickupAmount.ToString("F"));
                                break;
                            case StateStatus.E:
                                rDVO.RefEvents.Add(ReceiptEventTypes.Extend.ToString());
                                // ref amount for pickup service will be pickup amount
                                rDVO.RefAmounts.Add(pVo.ExtensionAmount.ToString("F"));
                                break;
                            case StateStatus.RN:
                                rDVO.RefEvents.Add(ReceiptEventTypes.Renew.ToString());
                                rDVO.RefAmounts.Add(pVo.RenewalAmount.ToString("F"));
                                break;
                            case StateStatus.L:
                                break;
                            case StateStatus.PD:
                                rDVO.RefEvents.Add(ReceiptEventTypes.Paydown.ToString());
                                rDVO.RefAmounts.Add(pVo.PaydownAmount.ToString("F"));

                                break;
                            case StateStatus.PPMNT:
                                rDVO.RefEvents.Add(ReceiptEventTypes.PARTP.ToString());
                                rDVO.RefAmounts.Add(pVo.Amount.ToString("F"));
                                break;
                            case StateStatus.CH:
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }

                        // ref store for service is the store the receipt was printed at
                        rDVO.RefStores.Add(curSiteId.StoreNumber);
                    }
                }
            }
            catch (Exception eX)
            {
                BasicExceptionHandler.Instance.AddException("Creating receipt data failed!!", new ApplicationException("Exception", eX));
                return (false);
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pVo"></param>
        /// <returns></returns>
        /*  private bool insertVoid(PawnLoan pVo, ref ReceiptDetailsVO rVO)
        {
        if (!this.beginProcessTenderTransaction("Process Tender (Void Block)"))
        {
        return (false);
        }
        string errorCode;
        string errorText;
        if (!ShopProcedures.ExecuteVoidPawnLoan(
        pVo.Items[0].mStore, pVo.TicketNumber, pVo.OriginationDate,
        pVo.OriginationDate, pVo.CreatedBy, pVo.CreatedBy,
        out errorCode, out errorText))
        {
        GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
        BasicExceptionHandler.Instance.AddException("Failed to void transaction", new ApplicationException());
        return (false);
        }

        if (!this.insertVoidNewPawnLoanReceipt(pVo, ref rVO))
        {
        GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
        BasicExceptionHandler.Instance.AddException("Failed to insert void transaction receipt", new ApplicationException());
        return (false);
        }

        if (!this.commitProcessTenderTransaction("Process Tender (Void Block)"))
        {
        return (false);
        }

        return (true);
        }*/

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pawnLoans"></param>
        /// <param name="rVO"></param>
        /// <returns></returns>
        private bool insertPickupLoans(List<PawnLoan> pawnLoans, ref ReceiptDetailsVO rVO)
        {
            if (CollectionUtilities.isEmpty(pawnLoans))
            {
                FileLogger.Instance.logMessage(LogLevel.WARN, this, "Cannot pick up an empty list of loans");
                return (false);
            }

            DesktopSession cds = GlobalDataAccessor.Instance.DesktopSession;
            CustomerVO cust = cds.ActiveCustomer;

            if (!this.beginProcessTenderTransaction("Process Tender (Pickup Block)"))
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Could not start pickup transaction block");
                return (false);
            }
            string trandate = ShopDateTime.Instance.ShopDate.ToShortDateString() + " " +
                              ShopDateTime.Instance.ShopTime.ToString();

            string errorCode;
            string errorText;
            DialogResult dgr = DialogResult.Retry;
            do
            {
                bool retValue = ServiceLoanDBProcedures.ExecuteServicePawnLoan(
                    ServiceLoanDBProcedures.ServiceLoanType.PICKUP,
                    pawnLoans, cust, ProductStatus.PU,
                    cds.CurrentSiteId.StoreNumber,
                    cds.FullUserName,
                    ShopDateTime.Instance.ShopDate,
                    ref rVO,
                    out errorCode,
                    out errorText);
                if (retValue)
                {
                    ReceiptNumber = Utilities.GetIntegerValue(rVO.ReceiptNumber);
                    break;
                }

                if (errorCode == "101")
                {
                    MessageBox.Show(Commons.GetMessageString("ProcessingError") + errorText);
                    dgr = DialogResult.Cancel;
                }
                else
                    dgr = MessageBox.Show(Commons.GetMessageString("ProcessingError"), "Error", MessageBoxButtons.RetryCancel);
            } while (dgr == DialogResult.Retry);

            if (dgr == DialogResult.Cancel)
            {
                GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(
                    EndTransactionType.ROLLBACK);
                if (FileLogger.Instance.IsLogError)
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Could not pickup pawn loans");
                BasicExceptionHandler.Instance.AddException("Could not pickup pawn loans", new ApplicationException("Could not pickup pawn loans"));
                return (false);
            }

            //Insert applicable fees if necessary
            const string feeRefType = "PAWN";

            List<int> lostTicketNumbers = new List<int>();
            List<string> lostTicketStoreNumber = new List<string>();
            List<string> lostTicketFlag = new List<string>();
            foreach (PawnLoan p in pawnLoans)
            {
                if (p == null)
                    continue;
                List<string> newFeeTypes = new List<string>();
                List<string> newFeeAmount = new List<string>();
                List<string> newFeeOrigAmount = new List<string>();
                List<string> newFeeisProrated = new List<string>();
                List<string> newFeeDates = new List<string>();
                List<string> newFeeStateCodes = new List<string>();


                //Go through the list of fees in the pawn loan object and insert into fee table
                string feeOpRevCode = FeeRevOpCodes.PICKUP.ToString();

                var pfiCalculator = new PfiPickupCalculator(p, GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId, ShopDateTime.Instance.FullShopDateTime);
                pfiCalculator.Calculate();

                if (p.OriginalFees.Count > 0)
                {
                    var origFees = from fees in p.OriginalFees
                                   where fees.OriginalAmount != 0
                                   select fees;
                    decimal totalFeeAmount = 0;
                    decimal lateFeeAmount = 0;
                    decimal lateFeeFinAmount = 0;
                    decimal lateFeeServAmount = 0;
                    foreach (Fee custloanfee in origFees)
                    {

                        if (custloanfee.FeeType == FeeTypes.LATE)
                        {
                            //Get late fee from pficalculator.applicablefees
                            var calculatedlatefee = (from f in pfiCalculator.ApplicableFees
                                                     where f.FeeType == FeeTypes.LATE
                                                     select f.Value).FirstOrDefault();
                            lateFeeAmount = calculatedlatefee > 0 ? (calculatedlatefee - totalFeeAmount + lateFeeFinAmount + lateFeeServAmount) : calculatedlatefee;

                        }
                        else
                        {
                            if (custloanfee.FeeType == FeeTypes.INTEREST)
                            {
                                lateFeeFinAmount = (from f in pfiCalculator.ApplicableFees
                                                    where f.FeeType == FeeTypes.INTEREST
                                                    select f.Value).FirstOrDefault();
                                totalFeeAmount += custloanfee.Value;
                            }

                            else if ((custloanfee.FeeType == FeeTypes.STORAGE || custloanfee.FeeType == FeeTypes.SERVICE) && custloanfee.FeeRefType == FeeRefTypes.PAWN)
                            {
                                lateFeeServAmount = (from f in pfiCalculator.ApplicableFees
                                                     where (f.FeeType == FeeTypes.STORAGE || f.FeeType == FeeTypes.SERVICE)
                                                     && f.FeeRefType == FeeRefTypes.PAWN
                                                     select f.Value).FirstOrDefault();
                                totalFeeAmount += custloanfee.Value;

                            }

                        }
                        newFeeTypes.Add(custloanfee.FeeType.ToString());

                        if (custloanfee.FeeType == FeeTypes.LATE)
                        {
                            newFeeAmount.Add(lateFeeAmount.ToString());
                            newFeeOrigAmount.Add(lateFeeAmount.ToString());
                        }
                        else
                        {
                            newFeeAmount.Add(custloanfee.Value.ToString());
                            newFeeOrigAmount.Add(custloanfee.OriginalAmount.ToString());
                        }
                        if (custloanfee.Prorated)
                            newFeeisProrated.Add("1");
                        else
                            newFeeisProrated.Add("0");
                        newFeeDates.Add(trandate);
                        if (custloanfee.Waived)
                            newFeeStateCodes.Add(FeeStates.WAIVED.ToString());
                        else if (custloanfee.FeeState == FeeStates.VOID)
                            newFeeStateCodes.Add(FeeStates.VOID.ToString());
                        else
                            newFeeStateCodes.Add(FeeStates.PAID.ToString());
                    }
                    //Adding logic to add late fee if the loan did not have late fee to begin with
                    //but the pickup storage and interest is less than the original assessed value
                    //In that case, to offset the balance a late fee amount should be inserted
                    var lateFeeComp = (from f in p.OriginalFees
                                       where f.FeeType == FeeTypes.LATE
                                       select f).FirstOrDefault();
                    if ((lateFeeComp.FeeRef == 0 && lateFeeComp.OriginalAmount == 0) && (lateFeeFinAmount + lateFeeServAmount < totalFeeAmount))
                    {
                        //Get late fee from pficalculator.applicablefees
                        var calculatedlatefee = (from f in pfiCalculator.ApplicableFees
                                                 where f.FeeType == FeeTypes.LATE
                                                 select f.Value).FirstOrDefault();
                        lateFeeAmount = calculatedlatefee > 0 ? (calculatedlatefee - totalFeeAmount + lateFeeFinAmount + lateFeeServAmount) : (lateFeeFinAmount + lateFeeServAmount - totalFeeAmount);

                        newFeeTypes.Add(FeeTypes.LATE.ToString());
                        newFeeAmount.Add(lateFeeAmount.ToString());
                        newFeeOrigAmount.Add(lateFeeAmount.ToString());
                        newFeeisProrated.Add("0");
                        newFeeDates.Add(trandate);
                        newFeeStateCodes.Add(FeeStates.PAID.ToString());

                    }

                    CustLoanLostTicketFee lostFee = p.LostTicketInfo;
                    int lateFeeIdx = p.Fees.FindIndex(f => f.FeeType == FeeTypes.LATE);
                    if (lateFeeIdx >= 0)
                    {
                        Fee newLateFee = new Fee();
                        newLateFee.FeeRef = p.Fees[lateFeeIdx].FeeRef;
                        newLateFee.FeeType = FeeTypes.LATE;
                        newLateFee.FeeRefType = p.Fees[lateFeeIdx].FeeRefType;
                        newLateFee.OriginalAmount = lateFeeAmount;
                        newLateFee.Value = lateFeeAmount;
                        newLateFee.FeeState = p.Fees[lateFeeIdx].FeeState;
                        newLateFee.FeeDate = p.Fees[lateFeeIdx].FeeDate;
                        p.Fees[lateFeeIdx] = newLateFee;
                    }

                    
                    
                    if (lostFee != null && p.LostTicketInfo.TicketLost && p.TempStatus == StateStatus.P)
                    {
                        lostTicketNumbers.Add(p.TicketNumber);
                        lostTicketStoreNumber.Add(p.OrgShopNumber);
                        lostTicketFlag.Add(p.LostTicketInfo.LSDTicket);
                        if (SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled)
                        {
                            var lostTicketPrinter = new LostTicketPrinter(p, GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId);
                            lostTicketPrinter.Print();
                        }
                    }

                }

                if (newFeeTypes.Count > 0)
                {
                    if (!ProcessTenderProcedures.ExecuteInsertUpdatePawnFee(newFeeTypes,
                                                                            feeRefType, p.TicketNumber, p.OrgShopNumber,
                                                                            newFeeAmount, newFeeOrigAmount, newFeeisProrated, newFeeDates, newFeeStateCodes, cds.UserName.ToLowerInvariant(),
                                                                            feeOpRevCode, Utilities.GetLongValue(rVO.ReceiptNumber), out errorCode, out errorText))
                    {
                        GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(
                            EndTransactionType.ROLLBACK);
                        if (FileLogger.Instance.IsLogError)
                            FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Could not update or insert fee data");
                        BasicExceptionHandler.Instance.AddException(
                            "Could not pickup pawn loans - update fees failed :" + errorText + ": " + errorText,
                            new ApplicationException("Could not pickup pawn loans"));
                        return (false);
                    }
                }
            }
            //call the SP to update the DB to reflect the lost pawn ticket status

            if (lostTicketNumbers.Count > 0)
            {
                do
                {
                    bool retValue = new CustomerDBProcedures(GlobalDataAccessor.Instance.DesktopSession).updatePawnLostTicketFlag(lostTicketStoreNumber, lostTicketNumbers,
                                                                                  cds.UserName.ToLowerInvariant(), lostTicketFlag, out errorCode, out errorText);
                    if (retValue)
                    {
                        break;
                    }

                    dgr = MessageBox.Show(Commons.GetMessageString("ProcessLostTktFeeError"), "Error", MessageBoxButtons.RetryCancel);
                } while (dgr == DialogResult.Retry);

                if (dgr == DialogResult.Cancel)
                {
                    GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(
                        EndTransactionType.ROLLBACK);

                    if (FileLogger.Instance.IsLogError)
                        FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Could not update lost ticket status when processing a pickup");
                    BasicExceptionHandler.Instance.AddException("Could not update lost ticket status when processing a pickup",
                                                                new ApplicationException(
                                                                    "Error when updating the database for the ticket to update lost ticket status"));
                    return (false);
                }
            }

            //Status all of the loans
            foreach (PawnLoan p in pawnLoans)
            {
                p.LoanStatus = ProductStatus.PU;
            }

            if (!this.updateTeller(pawnLoans))
            {
                GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                if (FileLogger.Instance.IsLogError)
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Could not insert teller records!");
                return (false);
            }

            if (!this.commitProcessTenderTransaction("Process Tender (Pickup Block)"))
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Could not commit pickup transaction block");
                return (false);
            }

            return (true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool executeNewLoan()
        {
            this.resetData();
            //Get the pawn loan
            PawnLoan pLoanRef = GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan;
            if (CollectionUtilities.isNotEmpty(pLoanRef.Items))
            {
                //Analyze loan to determine item types
                this.analyzeActiveLoan();
            }
            else
            {
                //Invalid pawn items list
                return (false);
            }

            //Start transaction block
            if (!this.beginProcessTenderTransaction("Process Tender (Next Num block)"))
            {
                return (false);
            }

            if (!this.retrieveTicketNumber())
            {
                GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                BasicExceptionHandler.Instance.AddException("Failed to retrieve ticket number", new ApplicationException());
                return (false);
            }
            //Get gun numbers if necessary
            if (!this.retrieveGunNumbers(pLoanRef) && CollectionUtilities.isNotEmpty(this.gunItemNumberIndices))
            {
                GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                BasicExceptionHandler.Instance.AddException("Failed to retrieve gun numbers", new ApplicationException());
                return (false);
            }

            //Commit teller transaction
            if (!this.commitProcessTenderTransaction("Process Tender (Next Num block)"))
            {
                return (false);
            }

            //Start transaction block
            if (!this.beginProcessTenderTransaction("Process Tender (New Pawn block)"))
            {
                return (false);
            }

            //Acquire data mapping values for ticket
            if (!this.generatePawnTicketData())
            {
                GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                BasicExceptionHandler.Instance.AddException("Failed to generate tickets", new ApplicationException());
                return (false);
            }

            //Perform inserts
            if (!this.performProcessTenderNewLoanInserts())
            {
                GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                BasicExceptionHandler.Instance.AddException("Failed to insert pawn data", new ApplicationException());
                return (false);
            }

            //Status the loan
            GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.LoanStatus = ProductStatus.IP;

            // Insert receipt details
            if (!this.insertNewPawnLoanReceipt())
            {
                GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                BasicExceptionHandler.Instance.AddException("Failed to insert receipt data", new ApplicationException());
                return (false);
            }

            //SR 11/20/09 Insert Fees as applicable
            //Add fees info to the db
            PawnLoan curLoan = GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan;
            const string feeRefType = "PAWN";
            string errorCode;
            string errorText;
            List<string> feeTypes = new List<string>();
            List<string> feeOrigAmount = new List<string>();
            List<string> feeAmount = new List<string>();
            List<string> isProrated = new List<string>();
            List<string> feeDates = new List<string>();
            List<string> feeStateCodes = new List<string>();
            string feeOprevCode = FeeRevOpCodes.NEWLOAN.ToString();

            if (curLoan.Fees != null && curLoan.Fees.Count > 0)
            {
                var curFees = from fees in curLoan.Fees
                              where fees.OriginalAmount != 0
                              select fees;
                foreach (Fee custloanfee in curFees)
                {
                    feeTypes.Add(custloanfee.FeeType.ToString());
                    feeOrigAmount.Add(custloanfee.Value.ToString());
                    feeAmount.Add(custloanfee.Value.ToString());

                    if (custloanfee.Prorated)
                        isProrated.Add("1");
                    else
                        isProrated.Add("0");
                    feeDates.Add(custloanfee.FeeDate.FormatDateWithTimeZone());
                    feeStateCodes.Add(custloanfee.FeeState.ToString());
                }
            }

            if (feeTypes.Count > 0)
            {
                if (!ProcessTenderProcedures.ExecuteInsertUpdatePawnFee(feeTypes,
                                                                        feeRefType, Utilities.GetIntegerValue(this.fullTicketNumber), GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreNumber,
                                                                        feeAmount, feeOrigAmount, isProrated, feeDates, feeStateCodes, GlobalDataAccessor.Instance.DesktopSession.UserName.ToLowerInvariant(),
                                                                        feeOprevCode, this.ReceiptNumber, out errorCode, out errorText))
                {
                    if (FileLogger.Instance.IsLogError)
                        FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Process tender encountered an exception - Could not insert fee data during new loan creation");
                    BasicExceptionHandler.Instance.AddException("ProcessTender.performProcessTenderInserts.ExecuteInsertNewPawnLoanRecord",
                                                                new ApplicationException("Insert Fee during new loan creation failed"));
                    GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(
                        EndTransactionType.ROLLBACK);
                    return (false);
                }
            }

            //Update teller and inform user to disburse cash
            GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.TempStatus = StateStatus.IP;
            if (!this.updateTeller(new List<PawnLoan>() { GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan }))
            {
                GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                BasicExceptionHandler.Instance.AddException("Failed to update teller", new ApplicationException());
                return (false);
            }

            //Commit teller transaction
            if (!this.commitProcessTenderTransaction("Process Tender (New Pawn block)"))
            {
                return (false);
            }

            //Perform print document
#if !__MULTI__
            if (!this.submitPawnTicketPrintJobsAndDisplayNewLoan())
            {
                BasicExceptionHandler.Instance.AddException("Failed to print tickets and/or receipts", new ApplicationException());
                return (false);
            }
#endif

            return (true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pVo"></param>
        /// <returns></returns>
        public bool executeVoidLoanPrintReceipt(PawnLoan pVo, ReceiptDetailsVO rdVO)
        {
            if (pVo == null)
            {
                return (false);
            }
            FileLogger.Instance.logMessage(LogLevel.INFO, this, "Executing void loan");
            bool rt;

            //Print void receipt
            DesktopSession cds = GlobalDataAccessor.Instance.DesktopSession;
            rt = this.generateLoanReceipt(
                ProcessTenderMode.VOIDLOAN,
                cds.UserName,
                cds.ActiveCustomer,

                new List<PawnLoan>()
                {
                    pVo
                },
                rdVO);

            //SR 3/11/10 Not sure if the following is needed
            /*
 
            int iDx = CashlinxDesktop.Desktop.GlobalDataAccessor.Instance.DesktopSession.PawnLoanKeys.FindIndex
            (a => a.TicketNumber == pVo.TicketNumber);
            if (iDx >= 0)
            {
            CashlinxDesktop.Desktop.GlobalDataAccessor.Instance.DesktopSession.PawnLoanKeys.RemoveAt(iDx);
            }
            * */

            FileLogger.Instance.logMessage(LogLevel.INFO, this, "Void loan executed, return = " + rt);

            return (rt);
        }

        public bool executeVoidPurchasePrintReceipt(PurchaseVO pVo, ReceiptDetailsVO rdVO)
        {
            if (pVo == null)
            {
                return (false);
            }
            FileLogger.Instance.logMessage(LogLevel.INFO, this, "Executing void purchase");
            bool rt;

            //Print void receipt
            DesktopSession cds = GlobalDataAccessor.Instance.DesktopSession;
            rt = this.generatePurchaseReceipt(
                ProcessTenderMode.VOIDLOAN,
                cds.UserName,
                cds.ActiveCustomer,
                new List<PurchaseVO>()
                {
                    pVo
                },
                "",
                rdVO);

            FileLogger.Instance.logMessage(LogLevel.INFO, this, "Void purchase executed, return = " + rt);

            return (rt);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pawnLoans"></param>
        /// <returns></returns>
        private bool executeServicePawnLoan()
        {
            List<PawnLoan> pawnLoans = GlobalDataAccessor.Instance.DesktopSession.ServiceLoans;
            if (CollectionUtilities.isEmpty(pawnLoans))
            {
                return (false);
            }
            FileLogger.Instance.logMessage(LogLevel.DEBUG, this, "Servicing pawn loans in process tender");

            bool serviceFlag = false;
            bool printReceipt = false;
            bool retVal;
            DesktopSession cds = GlobalDataAccessor.Instance.DesktopSession;
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;
            SecuredCouchConnector cC = GlobalDataAccessor.Instance.CouchDBConnector;
            var receiptDetailsVO = new ReceiptDetailsVO();

            //Use lambda to find index of any pickup loan - help determine if any pickups will
            //be occurring
            List<PawnLoan> pickupLoans = (from ploan in pawnLoans
                                          where ploan.TempStatus == StateStatus.P
                                          select ploan).ToList();
            if (pickupLoans.Count > 0)
            {
                //Get the receipt details
                retVal = insertServicePawnLoanReceipt(pickupLoans, ref receiptDetailsVO);
                if (!retVal)
                    return false;
                //execute pickup
                serviceFlag = this.insertPickupLoans(pickupLoans, ref receiptDetailsVO);
                //If service flag is true, print receipt
                if (serviceFlag)
                {
                    printReceipt = true;
                    //print pick slip
                    if (SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled)
                    {
                        List<HoldData> _selectedTransactions = new List<HoldData>();
                        foreach (PawnLoan pawnLoan in pickupLoans)
                        {
                            var pickupData = new HoldData
                                                  {
                                                      StatusCode = pawnLoan.LoanStatus.ToString(),
                                                      TicketNumber = pawnLoan.TicketNumber,
                                                      RefType = ReceiptRefTypes.PAWN.ToString(),
                                                      CustomerNumber = pawnLoan.CustomerNumber,
                                                      OrgShopNumber = pawnLoan.OrgShopNumber,
                                                      UserId = pawnLoan.CreatedBy,
                                                      TransactionDate = ShopDateTime.Instance.ShopDate,
                                                      Amount = pawnLoan.Amount,
                                                      CurrentPrincipalAmount = pawnLoan.CurrentPrincipalAmount,
                                                      PrevTicketNumber = pawnLoan.PrevTicketNumber,
                                                      OrigTicketNumber = pawnLoan.OrigTicketNumber,
                                                      TempStatus = pawnLoan.TempStatus,
                                                      Items = pawnLoan.Items
                                                  };
                            _selectedTransactions.Add(pickupData);
                        }
                        if (_selectedTransactions.Count > 0)
                        {
                            GlobalDataAccessor.Instance.DesktopSession.HoldsData = _selectedTransactions;
                            var printSlip = new PickSlipPrint
                                            {
                                                PickSlipType = "P.U."
                                            };
                            printSlip.ShowDialog();
                        }

                        if (
                            new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).ShouldPrintPickupReceipt(
                                GlobalDataAccessor.Instance.CurrentSiteId))
                        {
                            foreach (var pawnLoan in pickupLoans)
                            {
                                var pickupPaymentContext = new PickupPaymentReceipt.PickupPaymentContext();
                                var pickupPaymentReceipt = new PickupPaymentReceipt(pickupPaymentContext, PdfLauncher.Instance);
                                var customer = cds.ActiveCustomer;
                                var address = customer.getHomeAddress();
                                pickupPaymentContext.CustomerName = customer.CustomerName;
                                pickupPaymentContext.CustomerAddress = address.Address1;
                                pickupPaymentContext.CustomerCityStateZip = address.City + ", " + address.State_Code + " " + address.ZipCode;
                                var primaryContact = customer.getPrimaryContact();
                                if (primaryContact != null)
                                {
                                    pickupPaymentContext.CustomerPhone = Commons.Format10And11CharacterPhoneNumberForUI(primaryContact.ContactAreaCode + primaryContact.ContactPhoneNumber); ;
                                }
                                else
                                {
                                    pickupPaymentContext.CustomerPhone = string.Empty;
                                }

                                pickupPaymentContext.EmployeeNumber = GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile.EmployeeNumber;
                                pickupPaymentContext.StoreName = GlobalDataAccessor.Instance.CurrentSiteId.StoreName;
                                pickupPaymentContext.StoreNumber = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
                                pickupPaymentContext.StoreAddress = GlobalDataAccessor.Instance.CurrentSiteId.StoreAddress1.Trim() + ", " +
                                                                    GlobalDataAccessor.Instance.CurrentSiteId.StoreCityName.Trim() + ", " +
                                                                    GlobalDataAccessor.Instance.CurrentSiteId.State.Trim() + ", " +
                                                                    GlobalDataAccessor.Instance.CurrentSiteId.StoreZipCode.Trim();
                                pickupPaymentContext.Time = ShopDateTime.Instance.FullShopDateTime;

                                var pfiCalculator = new PfiPickupCalculator(pawnLoan, GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId, ShopDateTime.Instance.FullShopDateTime);
                                pfiCalculator.Calculate();
                                pickupPaymentContext.Ticket = new PickupPaymentReceipt.PickupPaymentTicketInfo
                                                              {
                                                                  CurrentPrincipalAmount = pawnLoan.CurrentPrincipalAmount,
                                                                  PfiMailerSent = pawnLoan.PfiMailerSent,
                                                                  Interest = (from f in pfiCalculator.ApplicableFees
                                                                              where f.FeeType == FeeTypes.INTEREST
                                                                              select f.Value).Sum(),
                                                                  LateFee = (from f in pfiCalculator.ApplicableFees
                                                                             where f.FeeType == FeeTypes.LATE
                                                                             select f.Value).Sum(),
                                                                  LostTicketFee = (from f in pfiCalculator.ApplicableFees
                                                                                   where f.FeeType == FeeTypes.LOST_TICKET
                                                                                   select f.Value).Sum(),
                                                                  ServiceFee = (from f in pfiCalculator.ApplicableFees
                                                                                where f.FeeType == FeeTypes.STORAGE || f.FeeType == FeeTypes.SERVICE
                                                                                select f.Value).Sum(),
                                                                  TicketNumber = pawnLoan.TicketNumber
                                                              };

                                pickupPaymentContext.OutputPath = SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseLogPath +
                                                                  "\\PickupPaymentReceipt_" + DateTime.Now.ToString("MMddyyyyhhmmssFFFFFFF") + ".pdf";
                                pickupPaymentReceipt.Print();

                                if (FileLogger.Instance.IsLogInfo)
                                {
                                    FileLogger.Instance.logMessage(
                                        LogLevel.INFO, "ProcessTenderController", "Printing Pickup Payment Receipt on {0}",
                                        GlobalDataAccessor.Instance.DesktopSession.LaserPrinter);
                                }

                                var qty = cds.CurrentSiteId.State.Equals(States.Indiana) ? 2 : 1;
                                string strReturnMessage =
                                    PrintingUtilities.printDocument(
                                        pickupPaymentContext.OutputPath,
                                        GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.IPAddress,
                                        GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.Port, qty);
                                if (!strReturnMessage.Contains("SUCCESS"))
                                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Pickup Payment Receipt : " + strReturnMessage);

                                var pDoc = new CouchDbUtils.PawnDocInfo();
                                //Set document add calls
                                pDoc.UseCurrentShopDateTime = true;
                                pDoc.StoreNumber = cds.CurrentSiteId.StoreNumber;
                                pDoc.CustomerNumber = cds.ActiveCustomer.CustomerNumber;
                                pDoc.DocumentType = Document.DocTypeNames.PDF;
                                pDoc.DocFileName = pickupPaymentContext.OutputPath;
                                pDoc.TicketNumber = pawnLoan.TicketNumber;
                                string errText;
                                if (!CouchDbUtils.AddPawnDocument(dA, cC, cds.UserName, ref pDoc, out errText))
                                {
                                    if (FileLogger.Instance.IsLogError)
                                        FileLogger.Instance.logMessage(
                                            LogLevel.ERROR, this,
                                            "Could not store Pickup Payment Receipt in document storage: {0} - FileName: {1}", errText,
                                            pickupPaymentContext.OutputPath);
                                    BasicExceptionHandler.Instance.AddException(
                                        "Could not store Pickup Payment Receipt in document storage",
                                        new ApplicationException("Could not store receipt in document storage: " + errText));
                                }

                                this.ProcessedTickets.Add(new PairType<long, string>(pawnLoan.TicketNumber, pickupPaymentContext.OutputPath));
                                this.TicketFiles.Add(pickupPaymentContext.OutputPath);
                            }
                        }
                    }
                }
            }

            //Use lambda to find index of any renew loan - help determine if any renewals will
            //be occurring
            List<PawnLoan> renewLoans = (from ploan in pawnLoans
                                         where ploan.TempStatus == StateStatus.RN
                                         select ploan).ToList();
            //Renew loans
            if (renewLoans.Count > 0)
            {
                receiptDetailsVO = new ReceiptDetailsVO();
                //Get the receipt details
                retVal = insertServicePawnLoanReceipt(renewLoans, ref receiptDetailsVO);
                if (!retVal)
                {
                    return false;
                }
                //execute renewal
                serviceFlag = this.insertRenewLoans(renewLoans, ref receiptDetailsVO);
                if (serviceFlag)
                {
                    printReceipt = true;
                }
            }

            //Use lambda to find index of any renew loan - help determine if any renewals will
            //be occurring
            List<PawnLoan> paydownLoans = (from ploan in pawnLoans
                                           where ploan.TempStatus == StateStatus.PD
                                           select ploan).ToList();
            //Renew loans
            if (paydownLoans.Count > 0)
            {
                receiptDetailsVO = new ReceiptDetailsVO();
                //Get the receipt details
                retVal = insertServicePawnLoanReceipt(paydownLoans, ref receiptDetailsVO);
                if (!retVal)
                {
                    return false;
                }
                //execute renewal
                serviceFlag = this.insertPaydownLoans(paydownLoans, ref receiptDetailsVO);
                if (serviceFlag)
                {
                    printReceipt = true;
                }
            }

            //
            //Use lambda to find index of any extend loan - help determine if any extensions will
            //be occurring
            List<PawnLoan> extensionLoans = (from ploan in pawnLoans
                                             where ploan.TempStatus == StateStatus.E
                                             select ploan).ToList();
            if (extensionLoans.Count > 0)
            {
                receiptDetailsVO = new ReceiptDetailsVO();
                //Get the receipt details

                retVal = insertServicePawnLoanReceipt(extensionLoans, ref receiptDetailsVO);
                if (!retVal)
                    return false;
                //execute extension
                serviceFlag = this.insertExtendLoans(extensionLoans, ref receiptDetailsVO);
                if (serviceFlag && SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled)
                {
                    printReceipt = true;
                    CustomerVO currentCust = cds.ActiveCustomer;
                    //Populate the Extension Memo Info objects to pass
                    List<ExtensionMemoInfo> extnMemoInfo = new List<ExtensionMemoInfo>();
                    foreach (PawnLoan ploan in extensionLoans)
                    {
                        ExtensionMemoInfo extnInfo = new ExtensionMemoInfo();
                        //SR 04/10/2012 CQ 17065 SHow current principal
                        extnInfo.Amount = ploan.CurrentPrincipalAmount;
                        extnInfo.InterestAmount = ploan.InterestAmount;
                        if (currentCust != null)
                        {
                            if (ploan.CustomerNumber != currentCust.CustomerNumber)
                            {
                                CustomerVO extnCustomer = CustomerProcedures.getCustomerDataByCustomerNumber(GlobalDataAccessor.Instance.DesktopSession, ploan.CustomerNumber);
                                if (extnCustomer != null)
                                    extnInfo.CustomerName = extnCustomer.LastName + COMMA_SPACE + extnCustomer.FirstName + SPACE + extnCustomer.MiddleInitial;
                               
                            }
                            else
                            {
                                extnInfo.CustomerName = currentCust.LastName + COMMA_SPACE + currentCust.FirstName + SPACE + currentCust.MiddleInitial;

                            }
                        }
                        extnInfo.DailyAmount = ploan.DailyAmount;
                        extnInfo.DueDate = ploan.DueDate;
                        extnInfo.ExtensionAmount = ploan.ExtensionAmount;
                        extnInfo.NewDueDate = ploan.NewDueDate;
                        extnInfo.OldPfiEligible = ploan.LastDayOfGrace;
                        extnInfo.NewPfiEligible = ploan.NewPfiEligible;
                        extnInfo.TicketNumber = ploan.TicketNumber;
                        if (cds.CurrentSiteId.State == States.Indiana)
                        {
                            //Each month is a 30 day block
                            int totalMonthsTillMaturity = ((ploan.NewDueDate - ploan.DateMade).Days) / 30;
                            int totalMonthsTillGrace = ((ploan.NewPfiEligible - ploan.DateMade).Days) / 30;
                            int extensionPaidDays = (ploan.NewDueDate - ploan.DueDate).Days;
                            if (ploan.PartialPaymentPaid)
                            {
                                int ppmtPaidDays = (ploan.LastPartialPaymentDate - ploan.DateMade).Days;
                                int daysToPay = ((totalMonthsTillMaturity * 30) - extensionPaidDays - ppmtPaidDays);
                                extnInfo.PawnChargeAtMaturity = (ploan.DailyAmount * daysToPay);
                                extnInfo.PawnChargeAtPfi =ploan.DailyAmount * (((totalMonthsTillGrace * 30))  - extensionPaidDays - ppmtPaidDays);
                            }
                            else
                            {
                                extnInfo.PawnChargeAtMaturity = (30 * ploan.DailyAmount);
                                extnInfo.PawnChargeAtPfi = (ploan.DailyAmount * (((totalMonthsTillGrace * 30)) - extensionPaidDays));
                            }
                            extnInfo.PawnChargePaidTo = ploan.DateMade.AddDays(extensionPaidDays);
                        }
                        else
                        {
                            if (cds.CurrentSiteId.State == States.Ohio)
                            {
                                extnInfo.PawnChargeAtMaturity = (ploan.InterestAmount + ploan.ServiceCharge) * ((ploan.NewDueDate - ploan.DueDate).Days) / 30;
                                extnInfo.PawnChargeAtPfi = extnInfo.PawnChargeAtMaturity + ((ploan.InterestAmount + ploan.ServiceCharge) * (((ploan.NewPfiEligible - ploan.NewDueDate).Days)/30));
                                extnInfo.PawnChargePaidTo = ploan.DueDate;

                            }
                            else
                            {
                                extnInfo.Fees = (from f in ploan.Fees
                                                 where f.FeeType != FeeTypes.INTEREST
                                                     && f.FeeRefType == FeeRefTypes.PAWN
                                                     && f.FeeState != FeeStates.VOID
                                                     && !f.Waived
                                                 select f.OriginalAmount).Sum();
                            }
                        }
                        
                        extnInfo.ExtendedBy = (ploan.NewPfiEligible - ploan.DueDate).Days / 30;
                        extnMemoInfo.Add(extnInfo);
                    }

                    ReportObject rptObj = new ReportObject();
                    rptObj.ReportStore = GlobalDataAccessor.Instance.CurrentSiteId.StoreName;
                    rptObj.ReportStoreDesc = GlobalDataAccessor.Instance.CurrentSiteId.StoreAddress1.Trim() + ", " +
                                             GlobalDataAccessor.Instance.CurrentSiteId.StoreCityName.Trim() + ", " +
                                             GlobalDataAccessor.Instance.CurrentSiteId.State.Trim() + ", " +
                                             GlobalDataAccessor.Instance.CurrentSiteId.StoreZipCode.Trim();
                    if (extensionLoans.Count == 1)
                    {
                        cds.PrintSingleMemoOfExtension = true;
                    }
                    if (!cds.PrintSingleMemoOfExtension)
                        rptObj.ReportTitle = "Memorandum of Multiple Extension";
                    else
                        rptObj.ReportTitle = "Memorandum of Extension";
                    rptObj.ReportTempFileFullName = SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseLogPath + "\\ExtensionMemo" + DateTime.Now.ToString("MMddyyyyhhmmssFFFFFFF") + ".pdf";

                    Reports.IExtensionMemo extnmemo = null;
                    //Extension Memo for Ohio and Indiana
                    if (cds.CurrentSiteId.State == States.Ohio || cds.CurrentSiteId.State == States.Indiana)
                    {
                        extnmemo = new Reports.ExtensionMemoOhioIndiana(PdfLauncher.Instance)
                        {
                            ExtensionMemoData = extnMemoInfo,
                            Employee = cds.UserName,
                            RptObject = rptObj
                        };
                    }
                    else
                    {
                        extnmemo = new Reports.ExtensionMemo(PdfLauncher.Instance)
                        {
                            ExtensionMemoData = extnMemoInfo,
                            Employee = cds.UserName,
                            PrintMultipleInOneMemo = !cds.PrintSingleMemoOfExtension,
                            RptObject = rptObj
                        };
                    }

                    if (extnmemo.Print())
                    {
                        const string formName = "memoss.pdf";

                        if (SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled &&
                            cds.LaserPrinter.IsValid)
                        {
                            foreach (var pdfFile in extnmemo.Documents)
                            {
                                if (FileLogger.Instance.IsLogInfo)
                                {
                                    FileLogger.Instance.logMessage(LogLevel.INFO, "ProcessTenderController", "Printing extension memo on {0}",
                                                                   cds.LaserPrinter);
                                }
                                string errMsg = PrintingUtilities.printDocument(pdfFile, cds.LaserPrinter.IPAddress,
                                                                                cds.LaserPrinter.Port, 2);
                                if (errMsg.IndexOf("SUCCESS", StringComparison.OrdinalIgnoreCase) == -1)
                                {
                                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Cannot print extension memo");
                                }
                            }
                        }
                        long idx = 0;
                        foreach (PawnLoan ploan in extensionLoans)
                        {
                            var pDoc = new CouchDbUtils.PawnDocInfo();
                            string pdfFile = extnmemo.ExtensionToPdfMap[ploan.TicketNumber];
                            //Set document add calls
                            pDoc.UseCurrentShopDateTime = true;
                            pDoc.StoreNumber = cds.CurrentSiteId.StoreNumber;
                            pDoc.CustomerNumber = cds.ActiveCustomer.CustomerNumber;
                            pDoc.DocumentType = Document.DocTypeNames.PDF;
                            pDoc.DocFileName = pdfFile;
                            //pDoc.TicketNumber = cds.ActiveCustomer.c
                            //pDoc.DocumentSearchType = CouchDbUtils.DocSearchType.STORE_TICKET;
                            pDoc.TicketNumber = ploan.TicketNumber;
                            long recNumL = 0L;

                            if (long.TryParse(receiptDetailsVO.ReceiptNumber, out recNumL))
                            {
                                pDoc.ReceiptNumber = recNumL;
                            }

                            //Add this document to the pawn document registry and document storage
                            string errText;
                            if (!CouchDbUtils.AddPawnDocument(dA, cC, cds.UserName, ref pDoc, out errText))
                            {
                                if (FileLogger.Instance.IsLogError)
                                    FileLogger.Instance.logMessage(LogLevel.ERROR, this,
                                                                   "Could not store memo of extension in document storage: {0} - FileName: {1}", errText, pdfFile);
                                BasicExceptionHandler.Instance.AddException(
                                    "Could not store memo of extension in document storage",
                                    new ApplicationException("Could not store receipt in document storage: " + errText));
                            }
                            this.ProcessedTickets.Add(new PairType<long, string>(ploan.TicketNumber, pdfFile));
                            this.TicketFiles.Add(pdfFile);
                            idx++;
                        }
                        //File.Delete(rptObj.ReportTempFileFullName);
                    }
                }
            }
            List<PawnLoan> partPaymentLoans = (from ploan in pawnLoans
                                               where ploan.TempStatus == StateStatus.PPMNT
                                               select ploan).ToList();
            if (partPaymentLoans.Count() > 0)
            {
                receiptDetailsVO = new ReceiptDetailsVO();
                //Get the receipt details

                retVal = insertServicePawnLoanReceipt(partPaymentLoans, ref receiptDetailsVO);
                if (!retVal)
                    return false;
                //execute extension
                serviceFlag = this.insertPartialPaymentLoans(partPaymentLoans, ref receiptDetailsVO);

                if (serviceFlag && SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled)
                {
                    printReceipt = true;
                    string storeAddr;
                    if (!string.IsNullOrEmpty(GlobalDataAccessor.Instance.CurrentSiteId.StoreAddress2))
                        storeAddr = GlobalDataAccessor.Instance.CurrentSiteId.StoreAddress1 + "," +
                                    GlobalDataAccessor.Instance.CurrentSiteId.StoreAddress2;
                    else
                        storeAddr = GlobalDataAccessor.Instance.CurrentSiteId.StoreAddress1;
                    var rptObj = new ReportObject();
                    rptObj.ReportImage = Properties.Resources.logo;
                    rptObj.ReportNumber = 226;
                    rptObj.ReportStore = GlobalDataAccessor.Instance.CurrentSiteId.StoreName;
                    rptObj.ReportStoreDesc = storeAddr +
                                             "," + GlobalDataAccessor.Instance.CurrentSiteId.StoreCityName + "," +
                                             GlobalDataAccessor.Instance.CurrentSiteId.State + " " +
                                             GlobalDataAccessor.Instance.CurrentSiteId.StoreZipCode;
                    rptObj.ReportTitle = "Partial Payment Receipt";
                    var partialPayReciept = new PartialPaymentReceipt();
                    partialPayReciept.RptObject = rptObj;

                    string pdfFile = SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseLogPath + "\\partpayreceipt" +
                                     DateTime.Now.ToString("MMddyyyyhhmmssFFFFFFF") + ".pdf";
                    rptObj.ReportTempFileFullName = pdfFile;

                    foreach (PawnLoan pawnLoan in partPaymentLoans)
                    {
                        if (GlobalDataAccessor.Instance.CurrentSiteId.State == "IN")
                        {
                           printReceipt = PrintIndianaPartialPaymentReceipt(pawnLoan, dA, cC, receiptDetailsVO);
                        }
                        else
                        {

                           printReceipt = PrintPartialPaymentReceipt(receiptDetailsVO, pdfFile, cds, cC, dA, pawnLoan, partialPayReciept);
                        }
                    }
                }


            }

            if (printReceipt)
            {
                bool rt = false;
                try
                {
                    rt = this.generateLoanReceipt(
                        ProcessTenderMode.SERVICELOAN,
                        cds.UserName.ToLowerInvariant(),
                        cds.ActiveCustomer,
                        pawnLoans,
                        receiptDetailsVO);
                }
                catch (Exception)
                {
                    rt = false;
                }

                if (!rt)
                {
                    if (FileLogger.Instance.IsLogError)
                        FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Could not print receipt for service loans");
                    BasicExceptionHandler.Instance.AddException("Could not print receipt while servicing loans", new ApplicationException("Could not print receipt while servicing loans"));
                    return (false);
                }
            }

            //Call print receipt service pawn loan
            return (serviceFlag);
        }

        private bool PrintPartialPaymentReceipt(
            ReceiptDetailsVO receiptDetailsVO, string pdfFile, DesktopSession cds, SecuredCouchConnector cC, OracleDataAccessor dA, PawnLoan pawnLoan,
            PartialPaymentReceipt partialPayReciept)
        {
            bool retval = true;
            decimal totalPartialAmount = 0;
            partialPayReciept.PartialPaymentTktData = new List<PartialPaymentTicketData>();
            PartialPayment currentPayment = pawnLoan.PartialPayments.Find(p => p.Status_cde == "New");
            if (currentPayment != null)
            {
                var intAmt = (from f in pawnLoan.Fees
                              where f.FeeType == FeeTypes.INTEREST
                              select f.Value).FirstOrDefault();
                var storageAmt = (from f in pawnLoan.Fees
                                  where f.FeeType == FeeTypes.STORAGE
                                  select f.Value).FirstOrDefault();
                var lateFee = (from f in pawnLoan.Fees
                               where f.FeeType == FeeTypes.LATE
                               select f.Value).FirstOrDefault();

                PartialPaymentTicketData partTktRpt = new PartialPaymentTicketData();
                partTktRpt.TicketNumber = pawnLoan.TicketNumber;
                partTktRpt.PrincipalReduction = currentPayment.PMT_PRIN_AMT;
                partTktRpt.InterestAmount = intAmt; // currentPayment.PMT_INT_AMT;
                partTktRpt.StorageFee = storageAmt; // currentPayment.PMT_SERV_AMT;
                partTktRpt.LoanItems = new List<string>();
                foreach (Item itemData in pawnLoan.Items)
                {
                    partTktRpt.LoanItems.Add(itemData.TicketDescription);
                }
                partTktRpt.OtherCharges = lateFee;
                // currentPayment.PMT_AMOUNT - (currentPayment.PMT_PRIN_AMT + currentPayment.PMT_INT_AMT + currentPayment.PMT_SERV_AMT);
                partTktRpt.SubTotal = currentPayment.PMT_AMOUNT;
                partTktRpt.PFIDate = pawnLoan.PfiEligible;
                partialPayReciept.PartialPaymentTktData.Add(partTktRpt);
                totalPartialAmount = currentPayment.PMT_AMOUNT;
            }
            else
            {
                retval = false;
            }
            var partRptData = new PartialPaymentReportData
                              {
                                  EmployeeName = GlobalDataAccessor.Instance.DesktopSession.UserName,
                                  CustomerName = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.CustomerName,
                                  CustomerAddress1 = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.CustHomeAddress,
                                  TotalDueFromCustomer = totalPartialAmount
                              };

            partialPayReciept.PartialPaymentRptData = partRptData;
            long idx = 0;
            if (partialPayReciept.Print())
            {
                string errMsg = PrintingUtilities.printDocument(
                    pdfFile, cds.LaserPrinter.IPAddress,
                    cds.LaserPrinter.Port, 2);
                if (errMsg.IndexOf("SUCCESS", StringComparison.OrdinalIgnoreCase) == -1)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Cannot print partial payment receipt");
                }

                SendReceiptToCouch(dA, cC, receiptDetailsVO, pawnLoan, cds, pdfFile);
                this.ProcessedTickets.Add(new PairType<long, string>(pawnLoan.TicketNumber, pdfFile));
                this.TicketFiles.Add(pdfFile);
                //idx++;
            }
            else
            {
                retval = false;
            }

            return retval;
        }

        private bool PrintIndianaPartialPaymentReceipt(PawnLoan pawnLoan, OracleDataAccessor dA, SecuredCouchConnector cC, ReceiptDetailsVO receiptDetailsVO)
        {
            
            var dataContext = new IndianaPartialPaymentContext();
            var partialPaymentReceipt = new PartialPayment_IN(dataContext);
            var currentPayment = pawnLoan.PartialPayments.Find(p => p.Status_cde == "New");

            dataContext.PrincipalReduction = (double)currentPayment.PMT_PRIN_AMT;
            dataContext.EmployeeNumber = GlobalDataAccessor.Instance.DesktopSession.UserName;

            // customer Address information
            var addressParts = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.CustHomeAddress.Split(',').ToList();

            dataContext.CustomerAddress = addressParts[0];
            if (addressParts.Count == 5)
            {
                // there is an address 2
                dataContext.CustomerAddress += ", " + addressParts[1];
                dataContext.CustomerCity = addressParts[2];
                dataContext.CustomerState = addressParts[3];
                dataContext.CustomerZip = addressParts[4];
            }
            else
            {
                // There was no address 2
                dataContext.CustomerCity = addressParts[1];
                dataContext.CustomerState = addressParts[2];
                dataContext.CustomerZip = addressParts[3];
            }


            dataContext.CustomerName = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.CustomerName;
            dataContext.CustomerPhone = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.CustomerNumber;

            dataContext.FilePath = SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseLogPath + "\\partpayreceipt" +
                                     DateTime.Now.ToString("MMddyyyyhhmmssFFFFFFF") + ".pdf";

            dataContext.ItemDescription = pawnLoan.Items[0].TicketDescription;

            if (pawnLoan.PartialPayments.Find(p => p.Status_cde == "New") != null)
            {

                dataContext.Interest = (double)(from f in pawnLoan.Fees
                                                where f.FeeType == FeeTypes.INTEREST
                                                select f.Value).FirstOrDefault();

                dataContext.ServiceFees = (double)(from f in pawnLoan.Fees
                                                   where f.FeeType == FeeTypes.SERVICE
                                                   select f.Value).FirstOrDefault();

                dataContext.OtherCharges = (double)(from f in pawnLoan.Fees
                                                where f.FeeType == FeeTypes.LATE
                                                select f.Value).FirstOrDefault();

            }
            foreach (var itemData in pawnLoan.Items)
                dataContext.ItemDescription += itemData.TicketDescription + '\n';

            dataContext.PrintDateTime = ShopDateTime.Instance.ShopDateCurTime;
            dataContext.StoreAddress = GlobalDataAccessor.Instance.CurrentSiteId.StoreAddress1;
            dataContext.StoreCity = GlobalDataAccessor.Instance.CurrentSiteId.StoreCityName;
            dataContext.StoreName = GlobalDataAccessor.Instance.CurrentSiteId.StoreName;
            dataContext.StoreNumber = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
            dataContext.StoreState = GlobalDataAccessor.Instance.CurrentSiteId.State;
            dataContext.StoreZip = GlobalDataAccessor.Instance.CurrentSiteId.StoreZipCode;

            dataContext.TicketNumber = pawnLoan.TicketNumber;

            partialPaymentReceipt.Print();
            if (partialPaymentReceipt.Print())
            {

                var errMsg = PrintingUtilities.printDocument(
                    dataContext.FilePath, GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.IPAddress,
                    GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.Port, 2);
                if (errMsg.IndexOf("SUCCESS", StringComparison.OrdinalIgnoreCase) == -1)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Cannot print partial payment receipt");
                }

                SendReceiptToCouch(dA, cC, receiptDetailsVO, pawnLoan, GlobalDataAccessor.Instance.DesktopSession, dataContext.FilePath);
                this.ProcessedTickets.Add(new PairType<long, string>(pawnLoan.TicketNumber, dataContext.FilePath));
                this.TicketFiles.Add(dataContext.FilePath);

            }
            return true;
        }

        private void SendReceiptToCouch(
            OracleDataAccessor dA, SecuredCouchConnector cC, ReceiptDetailsVO receiptDetailsVO, PawnLoan pawnLoan, DesktopSession cds, string pdfFile)
        {
            var pDoc = new CouchDbUtils.PawnDocInfo();

            //Set document add calls
            pDoc.UseCurrentShopDateTime = true;
            pDoc.SetDocumentSearchType(CouchDbUtils.DocSearchType.PARTPAYMENT);
            pDoc.StoreNumber = cds.CurrentSiteId.StoreNumber;
            pDoc.CustomerNumber = cds.ActiveCustomer.CustomerNumber;
            pDoc.DocumentType = Document.DocTypeNames.PDF;
            pDoc.DocFileName = pdfFile;
            pDoc.TicketNumber = pawnLoan.TicketNumber;
            long recNumL = 0L;

            if (long.TryParse(receiptDetailsVO.ReceiptNumber, out recNumL))
            {
                pDoc.ReceiptNumber = recNumL;
            }

            //Add this document to the pawn document registry and document storage
            string errText;
            if (!CouchDbUtils.AddPawnDocument(dA, cC, cds.UserName, ref pDoc, out errText))
            {
                if (FileLogger.Instance.IsLogError)
                {
                    FileLogger.Instance.logMessage(
                        LogLevel.ERROR, this,
                        "Could not store partial payment document in document storage: {0} - FileName: {1}", errText, pdfFile);
                }
                BasicExceptionHandler.Instance.AddException(
                    "Could not store partial payment document in document storage",
                    new ApplicationException("Could not store partial payment document in document storage: " + errText));
            }
        }


        private bool insertExtendLoans(List<PawnLoan> pawnLoans, ref ReceiptDetailsVO receiptDetailsVO)
        {
            if (CollectionUtilities.isEmpty(pawnLoans))
            {
                FileLogger.Instance.logMessage(LogLevel.WARN, this, "Cannot extend an empty list of loans");
                return (false);
            }


            DesktopSession cds = GlobalDataAccessor.Instance.DesktopSession;
            CustomerVO cust = cds.ActiveCustomer;

            if (!this.beginProcessTenderTransaction("Process Tender (Extend Block)"))
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Could not start extend transaction block");
                return (false);
            }
            string errorCode;
            string errorText;
            //set up store number list
            List<string> storeNumber = new List<string>();
            foreach (PawnLoan ploan in pawnLoans)
                storeNumber.Add(ploan.OrgShopNumber);

            DialogResult dgr = DialogResult.Retry;
            DataTable extensionData;
            do
            {
                bool retValue = ServiceLoanDBProcedures.ExecuteServicePawnLoanExtend(
                    ServiceLoanDBProcedures.ServiceLoanType.EXTENSION,
                    pawnLoans, cust, ProductStatus.IP,
                    storeNumber,
                    cds.FullUserName,
                    ShopDateTime.Instance.ShopDate,
                    ref receiptDetailsVO,
                    GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreNumber,
                    out extensionData,
                    out errorCode,
                    out errorText);
                if (retValue)
                {
                    ReceiptNumber = Utilities.GetIntegerValue(receiptDetailsVO.ReceiptNumber);
                    break;
                }
                if (errorCode == "101")
                {
                    MessageBox.Show(Commons.GetMessageString("ProcessingError") + errorText);
                    dgr = DialogResult.Cancel;
                }
                else

                    dgr = MessageBox.Show(Commons.GetMessageString("ProcessingError"), "Error", MessageBoxButtons.RetryCancel);
            } while (dgr == DialogResult.Retry);

            if (dgr == DialogResult.Cancel)
            {
                if (FileLogger.Instance.IsLogError)
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Could not extend pawn loans");
                BasicExceptionHandler.Instance.AddException("Could not extend pawn loans", new ApplicationException("Could not extend pawn loans"));
                GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                return (false);
            }

            var loanlist = from ploan in pawnLoans
                           where ploan != null
                           select ploan;

            foreach (PawnLoan p in loanlist)
            {
                //Insert extension amount as a service charge fee
                const string feeRefType = "EXT";
                List<string> newFeeTypes = new List<string>();
                List<string> newFeeAmount = new List<string>();
                List<string> newFeeOrigAmount = new List<string>();
                List<string> newFeeisProrated = new List<string>();
                List<string> newFeeDates = new List<string>();
                List<string> newFeeStateCodes = new List<string>();

                //Get the extension ticket id for the loan
                DataRow[] extensionRow = extensionData.Select("old_loan_number=" + p.TicketNumber);
                int extTktId = 0;
                if (extensionRow != null && extensionRow.Count() > 0)
                {
                    extTktId = Utilities.GetIntegerValue(extensionRow[0]["ext_tkt_id"], 0);
                    p.ExtensionID = extTktId;
                }
                if (extTktId != 0)
                {
                    //Insert extension amount as a fee
                    string feeOpRevCode = FeeRevOpCodes.EXTENSION.ToString();
                    if (p.OriginalFees.Count > 0)
                    {
                        var origFees = from fees in p.OriginalFees
                                       where fees.OriginalAmount != 0
                                       select fees;
                        foreach (Fee custloanfee in origFees)
                        {
                            newFeeTypes.Add(custloanfee.FeeType.ToString());
                            newFeeOrigAmount.Add(custloanfee.OriginalAmount.ToString());
                            newFeeAmount.Add(custloanfee.Value.ToString());
                            if (custloanfee.Prorated)
                                newFeeisProrated.Add("1");
                            else
                                newFeeisProrated.Add("0");
                            newFeeDates.Add(custloanfee.FeeDate.FormatDateWithTimeZone());
                            if (custloanfee.Waived)
                                newFeeStateCodes.Add(FeeStates.WAIVED.ToString());
                            else
                                newFeeStateCodes.Add(FeeStates.PAID.ToString());
                        }
                    }

                    if (newFeeTypes.Count > 0)
                    {
                        if (!ProcessTenderProcedures.ExecuteInsertUpdatePawnFee(newFeeTypes,
                                                                                feeRefType, p.ExtensionID, p.OrgShopNumber,
                                                                                newFeeAmount, newFeeOrigAmount, newFeeisProrated, newFeeDates, newFeeStateCodes, cds.UserName.ToLowerInvariant(),
                                                                                feeOpRevCode, Utilities.GetLongValue(receiptDetailsVO.ReceiptNumber), out errorCode, out errorText))
                        {
                            GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(
                                EndTransactionType.ROLLBACK);
                            if (FileLogger.Instance.IsLogError)
                                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Could not insert extension amount data");
                            BasicExceptionHandler.Instance.AddException(
                                "Could not extend pawn loans - insert fees failed :" + errorText + ": " + errorText,
                                new ApplicationException("Could not extend pawn loans"));
                            return (false);
                        }
                    }
                }
            }

            //change all loans to indicate that they are extended
            foreach (PawnLoan p in pawnLoans)
            {
                p.IsExtended = true;
            }

            if (!this.updateTeller(pawnLoans))
            {
                if (FileLogger.Instance.IsLogError)
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Could not insert teller records!");
                GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                return (false);
            }

            if (!this.commitProcessTenderTransaction("Process Tender (Extend Block)"))
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Could not commit Extend transaction block");
                return (false);
            }

            return (true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pawnLoans"></param>
        /// <param name="receiptDetailsVO"></param>
        /// <returns></returns>
        private bool insertRenewLoans(List<PawnLoan> pawnLoans, ref ReceiptDetailsVO receiptDetailsVO)
        {
            if (CollectionUtilities.isEmpty(pawnLoans))
            {
                FileLogger.Instance.logMessage(LogLevel.WARN, this, "Cannot renew an empty list of loans");
                return (false);
            }

            DesktopSession cds = GlobalDataAccessor.Instance.DesktopSession;
            CustomerVO cust = cds.ActiveCustomer;

            if (!this.beginProcessTenderTransaction("Process Tender (Renew Block)"))
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Could not start renew transaction block");
                return (false);
            }

            //Set up required id number lists
            var storeNumbers = new List<string>(pawnLoans.Count);
            var oldLoanNumbers = new List<string>(pawnLoans.Count);
            var pawnAppIds = new List<string>(pawnLoans.Count);

            //Populate required id number lists
            foreach (PawnLoan ploan in pawnLoans)
            {
                storeNumbers.Add(ploan.OrgShopNumber);
                oldLoanNumbers.Add(ploan.TicketNumber.ToString());
                pawnAppIds.Add(ploan.PawnAppId);
            }
            string trandate = ShopDateTime.Instance.ShopDate.ToShortDateString() + " " +
                              ShopDateTime.Instance.ShopTime.ToString();
            //Determine if we have renewals

            if (CollectionUtilities.isNotEmpty(cds.RenewalLoans))
            {
                this.pawnTickets = new List<Hashtable>();
                DialogResult dgr = DialogResult.Retry;
                string errCode;
                string errDesc;
                int receiptNum;
                DataTable dataTable;

                do
                {
                    bool retValue = ServiceLoanDBProcedures.ExecuteServicePawnLoanRenewPaydown(
                        ServiceTypes.RENEW,
                        oldLoanNumbers,
                        pawnAppIds,
                        pawnLoans,
                        cust.CustomerNumber,
                        storeNumbers,
                        cds.CurrentSiteId.StoreNumber,
                        cds.FullUserName,
                        Utilities.GetDateTimeValue(trandate, DateTime.Now),
                        cds.TTyId,
                        cds.TTyId,
                        ref receiptDetailsVO,
                        out dataTable,
                        out receiptNum,
                        out errCode,
                        out errDesc);
                    if (retValue)
                    {
                        ReceiptNumber = Utilities.GetIntegerValue(receiptDetailsVO.ReceiptNumber);
                        break;
                    }
                    if (errCode == "101")
                    {
                        MessageBox.Show(Commons.GetMessageString("ProcessingError") + errDesc);
                        dgr = DialogResult.Cancel;
                    }
                    else

                        dgr = MessageBox.Show(Commons.GetMessageString("ProcessingError"), "Error", MessageBoxButtons.RetryCancel);
                } while (dgr == DialogResult.Retry);

                if (dgr == DialogResult.Cancel)
                {
                    if (FileLogger.Instance.IsLogError)
                        FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Could not renew pawn loans");
                    BasicExceptionHandler.Instance.AddException("Could not renew pawn loans", new ApplicationException("Could not renew pawn loans"));
                    GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                    return (false);
                }
                //Insert applicable fees if necessary
                const string feeRefType = "PAWN";
                string oldLoanOpRevCode = FeeRevOpCodes.RENEWAL.ToString();
                string newLoanOpRevCode = FeeRevOpCodes.NEWLOAN.ToString();

                List<int> lostTicketNumbers = new List<int>();
                List<string> lostTicketStoreNumber = new List<string>();
                List<string> lostTicketFlag = new List<string>();
                foreach (PawnLoan p in pawnLoans)
                {
                    if (p == null)
                        continue;
                    //Variables for the original loan
                    List<string> oldLoanfeeTypes = new List<string>();
                    List<string> oldLoanfeeAmount = new List<string>();
                    List<string> oldLoanfeeOrigAmount = new List<string>();
                    List<string> oldLoanisProrated = new List<string>();
                    List<string> oldLoanfeeDates = new List<string>();
                    List<string> oldLoanfeeStateCodes = new List<string>();

                    CustLoanLostTicketFee lostFee = p.LostTicketInfo;
                    if (lostFee != null && p.LostTicketInfo.TicketLost && p.TempStatus == StateStatus.RN)
                    {
                        lostTicketNumbers.Add(p.TicketNumber);
                        lostTicketStoreNumber.Add(p.OrgShopNumber);
                        lostTicketFlag.Add(p.LostTicketInfo.LSDTicket);
                        if (SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled)
                        {
                            var lostTicketPrinter = new LostTicketPrinter(p, GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId);
                            lostTicketPrinter.Print();
                        }
                    }

                    //variables for the new loan
                    List<string> newLoanfeeTypes = new List<string>();
                    List<string> newLoanfeeAmount = new List<string>();
                    List<string> newLoanfeeOrigAmount = new List<string>();
                    List<string> newLoanisProrated = new List<string>();
                    List<string> newLoanfeeDates = new List<string>();
                    List<string> newLoanfeeStateCodes = new List<string>();

                    //Go through the list of original fees in the pawn loan object and update the fee table
                    if (p.OriginalFees.Count > 0)
                    {
                        var originalFees = from fees in p.OriginalFees
                                           where fees.OriginalAmount != 0
                                           select fees;
                        foreach (Fee custloanfee in originalFees)
                        {
                            decimal feeAmt = custloanfee.Value;
                            if (custloanfee.FeeType == FeeTypes.LATE)
                            {
                                feeAmt = custloanfee.Value - p.ExtensionAmount;
                            }
                            oldLoanfeeTypes.Add(custloanfee.FeeType.ToString());
                            oldLoanfeeOrigAmount.Add(feeAmt.ToString());
                            oldLoanfeeAmount.Add(feeAmt.ToString());
                            if (custloanfee.Prorated)
                                oldLoanisProrated.Add("1");
                            else
                                oldLoanisProrated.Add("0");
                            oldLoanfeeDates.Add(trandate);
                            if (custloanfee.Waived)
                                oldLoanfeeStateCodes.Add(FeeStates.WAIVED.ToString());
                            else
                                oldLoanfeeStateCodes.Add(FeeStates.PAID.ToString());
                        }
                    }

                    if (oldLoanfeeTypes.Count > 0)
                    {
                        bool returnValue = ProcessTenderProcedures.ExecuteInsertUpdatePawnFee(oldLoanfeeTypes,
                                                                                              feeRefType, p.TicketNumber, p.OrgShopNumber,
                                                                                              oldLoanfeeAmount, oldLoanfeeOrigAmount, oldLoanisProrated, oldLoanfeeDates,
                                                                                              oldLoanfeeStateCodes, cds.UserName.ToLowerInvariant(),
                                                                                              oldLoanOpRevCode, Utilities.GetLongValue(receiptNum), out errCode, out errDesc);
                        if (!returnValue)
                        {
                            GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(
                                EndTransactionType.ROLLBACK);
                            if (FileLogger.Instance.IsLogError)
                                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Could not update fee data for " + p.OrigTicketNumber);
                            BasicExceptionHandler.Instance.AddException(
                                "Could not Renew pawn loans - update fees failed :" + errCode + ": " + errDesc,
                                new ApplicationException("Could not Renew Pawn Loans"));
                            return (false);
                        }
                    }
                    DataRow[] newLoanRow = dataTable.Select("old_loan_number=" + p.TicketNumber);
                    if (newLoanRow != null && newLoanRow.Count() > 0)
                    {
                        int newLoanNo = Utilities.GetIntegerValue(newLoanRow[0]["new_loan_number"], 0);
                        p.PrevTicketNumber = p.TicketNumber;
                        p.TicketNumber = newLoanNo;
                    }

                    //Go through the list of original fees in the pawn loan object and update the fee table
                    if (p.Fees.Count > 0)
                    {
                        var newFees = from fees in p.Fees
                                      where fees.OriginalAmount != 0
                                      select fees;
                        foreach (Fee custloanfee in newFees)
                        {
                            newLoanfeeTypes.Add(custloanfee.FeeType.ToString());
                            newLoanfeeOrigAmount.Add(custloanfee.OriginalAmount.ToString());
                            newLoanfeeAmount.Add(custloanfee.Value.ToString());
                            if (custloanfee.Prorated)
                                newLoanisProrated.Add("1");
                            else
                                newLoanisProrated.Add("0");
                            newLoanfeeDates.Add(custloanfee.FeeDate.FormatDateWithTimeZone());
                            newLoanfeeStateCodes.Add(FeeStates.ASSESSED.ToString());
                        }
                    }
                    if (newLoanfeeTypes.Count > 0)
                    {
                        if (!ProcessTenderProcedures.ExecuteInsertUpdatePawnFee(newLoanfeeTypes,
                                                                                feeRefType, p.TicketNumber, p.OrgShopNumber,
                                                                                newLoanfeeAmount, newLoanfeeOrigAmount, newLoanisProrated, newLoanfeeDates, newLoanfeeStateCodes, cds.UserName.ToLowerInvariant(),
                                                                                newLoanOpRevCode, Utilities.GetLongValue(receiptNum), out errCode, out errDesc))
                        {
                            GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(
                                EndTransactionType.ROLLBACK);
                            if (FileLogger.Instance.IsLogError)
                                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Could not insert fee data");
                            BasicExceptionHandler.Instance.AddException(
                                "Could not pickup pawn loans - insert fees failed :" + errCode + ": " + errDesc,
                                new ApplicationException("Could not pickup pawn loans"));
                            return (false);
                        }
                    }
                }
                //call the SP to update the DB to reflect the lost pawn ticket status

                if (lostTicketNumbers.Count > 0)
                {
                    do
                    {
                        bool retValue = new CustomerDBProcedures(GlobalDataAccessor.Instance.DesktopSession).updatePawnLostTicketFlag(lostTicketStoreNumber, lostTicketNumbers,
                                                                                      cds.UserName.ToLowerInvariant(), lostTicketFlag, out errCode, out errDesc);
                        if (retValue)
                        {
                            break;
                        }

                        dgr = MessageBox.Show(Commons.GetMessageString("ProcessLostTktFeeError"), "Error", MessageBoxButtons.RetryCancel);
                    } while (dgr == DialogResult.Retry);

                    if (dgr == DialogResult.Cancel)
                    {
                        GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(
                            EndTransactionType.ROLLBACK);

                        if (FileLogger.Instance.IsLogError)
                            FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Could not update lost ticket status when processing a renewal");
                        BasicExceptionHandler.Instance.AddException("Could not update lost ticket status when processing a renewal",
                                                                    new ApplicationException(
                                                                        "Error when updating the database for the ticket to update lost ticket status"));
                        return (false);
                    }
                }
                bool printTicket = false;
                foreach (PawnLoan ploan in pawnLoans)
                {
                    GlobalDataAccessor.Instance.DesktopSession.CurrentPawnLoan = ploan;
                    this.fullTicketNumber = ploan.TicketNumber;

                    analyzeActiveLoan();
                    if (!generatePawnTicketData())
                    {
                        if (FileLogger.Instance.IsLogError)
                            FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Could not print ticket data for " + ploan.TicketNumber.ToString());
                    }
                    else
                    {
                        //Print addendum if exists
                        if (printAddendum)
                        {
                            try
                            {
                                ReportObject rptObj = new ReportObject();
                                rptObj.ReportStore = GlobalDataAccessor.Instance.CurrentSiteId.StoreName;
                                rptObj.ReportStoreDesc = GlobalDataAccessor.Instance.CurrentSiteId.StoreAddress1 + "," +
                                                         GlobalDataAccessor.Instance.CurrentSiteId.StoreCityName + "," +
                                                         GlobalDataAccessor.Instance.CurrentSiteId.State + "," +
                                                         GlobalDataAccessor.Instance.CurrentSiteId.StoreZipCode;
                                rptObj.ReportTitle = "Addendum";
                                rptObj.ReportTempFileFullName = SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseLogPath + "\\Addendum" + DateTime.Now.ToString("MMddyyyyhhmmssFFFFFFF") + ".pdf";

                                Reports.PawnTicketAddendumDocument pDocument =
                                    new Reports.PawnTicketAddendumDocument(PdfLauncher.Instance);
                                pDocument.ReportObject = rptObj;
                                pTicketAddendum.ticketNumber = (int)ploan.TicketNumber;
                                pDocument.CreateReport(pTicketAddendum);
                                var pDoc = new CouchDbUtils.PawnDocInfo();
                                OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;
                                SecuredCouchConnector cC = GlobalDataAccessor.Instance.CouchDBConnector;

                                //Set document add calls
                                pDoc.UseCurrentShopDateTime = true;
                                pDoc.SetDocumentSearchType(CouchDbUtils.DocSearchType.TICKET_ADDENDUM);
                                pDoc.StoreNumber = cds.CurrentSiteId.StoreNumber;
                                pDoc.CustomerNumber = cds.ActiveCustomer.CustomerNumber;
                                pDoc.TicketNumber = (int)ploan.TicketNumber;
                                pDoc.DocumentType = Document.DocTypeNames.PDF;
                                pDoc.DocFileName = pDocument.ReportObject.ReportTempFileFullName;

                                //Add this document to the pawn document registry and document storage
                                string errText;
                                if (!CouchDbUtils.AddPawnDocument(dA, cC, cds.UserName, ref pDoc, out errText))
                                {
                                    if (FileLogger.Instance.IsLogError)
                                        FileLogger.Instance.logMessage(LogLevel.ERROR, this,
                                                                       "Could not store addendum document in document storage: {0} - FileName: {1}", errText, pDocument.ReportObject.ReportTempFileFullName);
                                    BasicExceptionHandler.Instance.AddException(
                                        "Could not store addendum document in document storage",
                                        new ApplicationException("Could not store addendum document in document storage: " + errText));
                                }

                                if (SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled &&
                                    cds.LaserPrinter.IsValid)
                                {

                                    if (FileLogger.Instance.IsLogInfo)
                                    {
                                        FileLogger.Instance.logMessage(LogLevel.INFO, "ProcessTenderController", "Printing extension memo on {0}",
                                                                       cds.LaserPrinter);
                                    }
                                    string errMsg = PrintingUtilities.printDocument(rptObj.ReportTempFileFullName, cds.LaserPrinter.IPAddress,
                                                                                    cds.LaserPrinter.Port, 2);
                                    if (errMsg.IndexOf("SUCCESS", StringComparison.OrdinalIgnoreCase) == -1)
                                    {
                                        FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Cannot print extension memo");
                                    }
                                }


                            }
                            catch (Exception ex)
                            {
                                if (FileLogger.Instance.IsLogError)
                                    FileLogger.Instance.logMessage(LogLevel.ERROR, this,
                                                                   "Could not print addendum document" + ex.Message);

                            }
                        }

                        printTicket = true;
                    }
                }
                if (printTicket)
                {
                    //Change for BZ # 115
                    IsRenewed = true;
                    pawnTicketPrint();


                }

                if (!this.updateTeller(pawnLoans))
                {
                    if (FileLogger.Instance.IsLogError)
                        FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Could not insert teller records!");
                    GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                    return (false);
                }
            }

            if (!this.commitProcessTenderTransaction("Process Tender (Renew Block)"))
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Could not commit Renew transaction block");
                return (false);
            }

            return (true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pawnLoans"></param>
        /// <param name="receiptDetailsVO"></param>
        /// <returns></returns>
        private bool insertPaydownLoans(List<PawnLoan> pawnLoans, ref ReceiptDetailsVO receiptDetailsVO)
        {
            string errCode;
            string errDesc;
            if (CollectionUtilities.isEmpty(pawnLoans))
            {
                FileLogger.Instance.logMessage(LogLevel.WARN, this, "Cannot paydown an empty list of loans");
                return (false);
            }

            DesktopSession cds = GlobalDataAccessor.Instance.DesktopSession;
            CustomerVO cust = cds.ActiveCustomer;

            if (!this.beginProcessTenderTransaction("Process Tender (Paydown Block)"))
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Could not start paydown transaction block");
                return (false);
            }

            //Set up required id number lists
            var storeNumbers = new List<string>(pawnLoans.Count);
            var oldLoanNumbers = new List<string>(pawnLoans.Count);
            var pawnAppIds = new List<string>(pawnLoans.Count);

            //Populate required id number lists
            foreach (PawnLoan ploan in pawnLoans)
            {
                storeNumbers.Add(ploan.OrgShopNumber);
                oldLoanNumbers.Add(ploan.PrevTicketNumber.ToString());
                pawnAppIds.Add(ploan.PawnAppId);
            }
            string trandate = ShopDateTime.Instance.ShopDate.ToShortDateString() + " " +
                              ShopDateTime.Instance.ShopTime.ToString();

            //Determine if we have paydowns
            if (CollectionUtilities.isNotEmpty(cds.PaydownLoans))
            {
                this.pawnTickets = new List<Hashtable>();
                DialogResult dgr = DialogResult.Retry;
                int paydownReceiptNum;
                DataTable paydownDataTable;
                do
                {
                    string paydownErrCode;
                    string paydownErrDesc;

                    bool retValue = ServiceLoanDBProcedures.ExecuteServicePawnLoanRenewPaydown(
                        ServiceTypes.PAYDOWN,
                        oldLoanNumbers,
                        pawnAppIds,
                        pawnLoans,
                        cust.CustomerNumber,
                        storeNumbers,
                        cds.CurrentSiteId.StoreNumber,
                        cds.FullUserName,
                        Utilities.GetDateTimeValue(trandate, DateTime.Now),
                        cds.TTyId,
                        cds.TTyId,
                        ref receiptDetailsVO,
                        out paydownDataTable,
                        out paydownReceiptNum,
                        out paydownErrCode,
                        out paydownErrDesc);
                    if (retValue)
                    {
                        ReceiptNumber = Utilities.GetIntegerValue(receiptDetailsVO.ReceiptNumber);
                        break;
                    }
                    if (paydownErrCode == "101")
                    {
                        MessageBox.Show(Commons.GetMessageString("ProcessingError") + paydownErrDesc);
                        dgr = DialogResult.Cancel;
                    }
                    else

                        dgr = MessageBox.Show(Commons.GetMessageString("ProcessingError"), "Error", MessageBoxButtons.RetryCancel);
                } while (dgr == DialogResult.Retry);

                if (dgr == DialogResult.Cancel)
                {
                    if (FileLogger.Instance.IsLogError)
                        FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Could not paydown pawn loans");
                    BasicExceptionHandler.Instance.AddException("Could not paydown pawn loans", new ApplicationException("Could not Paydown pawn loans"));
                    GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                    return (false);
                }
                //Insert applicable fees if necessary
                const string feeRefType = "PAWN";

                List<int> lostTicketNumbers = new List<int>();
                List<string> lostTicketStoreNumber = new List<string>();
                List<string> lostTicketFlag = new List<string>();
                foreach (PawnLoan p in pawnLoans)
                {
                    if (p == null)
                        continue;

                    CustLoanLostTicketFee lostFee = p.LostTicketInfo;
                    if (lostFee != null && p.LostTicketInfo.TicketLost && p.TempStatus == StateStatus.PD)
                    {
                        lostTicketNumbers.Add(p.TicketNumber);
                        lostTicketStoreNumber.Add(p.OrgShopNumber);
                        lostTicketFlag.Add(p.LostTicketInfo.LSDTicket);
                        if (SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled)
                        {
                            var lostTicketPrinter = new LostTicketPrinter(p, GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId);
                            lostTicketPrinter.Print();
                        }
                    }

                    //Variables for the original loan
                    string oldLoanFeeRevCode = FeeRevOpCodes.PAYDOWN.ToString();
                    List<string> oldLoanfeeTypes = new List<string>();
                    List<string> oldLoanfeeAmount = new List<string>();
                    List<string> oldLoanfeeOrigAmount = new List<string>();
                    List<string> oldLoanisProrated = new List<string>();
                    List<string> oldLoanfeeDates = new List<string>();
                    List<string> oldLoanfeeStateCodes = new List<string>();

                    //variables for the new loan
                    string newLoanFeeRevCode = FeeRevOpCodes.NEWLOAN.ToString();
                    List<string> newLoanfeeTypes = new List<string>();
                    List<string> newLoanfeeAmount = new List<string>();
                    List<string> newLoanfeeOrigAmount = new List<string>();
                    List<string> newLoanisProrated = new List<string>();
                    List<string> newLoanfeeDates = new List<string>();
                    List<string> newLoanfeeStateCodes = new List<string>();

                    //Go through the list of original fees in the pawn loan object and update the fee table
                    //for the original loan
                    if (p.OriginalFees.Count > 0)
                    {
                        var origFees = from fees in p.OriginalFees
                                       where fees.OriginalAmount != 0
                                       select fees;
                        foreach (Fee custloanfee in origFees)
                        {
                            decimal feeAmt = custloanfee.Value;
                            if (custloanfee.FeeType == FeeTypes.LATE)
                            {
                                feeAmt = custloanfee.Value - p.ExtensionAmount;
                            }
                            oldLoanfeeTypes.Add(custloanfee.FeeType.ToString());
                            oldLoanfeeOrigAmount.Add(feeAmt.ToString());
                            oldLoanfeeAmount.Add(feeAmt.ToString());
                            if (custloanfee.Prorated)
                                oldLoanisProrated.Add("1");
                            else
                                oldLoanisProrated.Add("0");
                            oldLoanfeeDates.Add(trandate);
                            if (custloanfee.Waived)
                                oldLoanfeeStateCodes.Add(FeeStates.WAIVED.ToString());
                            else
                                oldLoanfeeStateCodes.Add(FeeStates.PAID.ToString());
                        }
                    }

                    if (oldLoanfeeTypes.Count > 0)
                    {
                        bool returnValue = ProcessTenderProcedures.ExecuteInsertUpdatePawnFee(oldLoanfeeTypes,
                                                                                              feeRefType, p.TicketNumber, p.OrgShopNumber,
                                                                                              oldLoanfeeAmount, oldLoanfeeOrigAmount, oldLoanisProrated, oldLoanfeeDates,
                                                                                              oldLoanfeeStateCodes, cds.UserName.ToLowerInvariant(),
                                                                                              oldLoanFeeRevCode, Utilities.GetLongValue(paydownReceiptNum), out errCode, out errDesc);
                        if (!returnValue)
                        {
                            GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(
                                EndTransactionType.ROLLBACK);
                            if (FileLogger.Instance.IsLogError)
                                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Could not update fee data for " + p.OrigTicketNumber);
                            BasicExceptionHandler.Instance.AddException(
                                "Could not paydown pawn loans - update fees failed :" + errCode + ": " + errDesc,
                                new ApplicationException("Could not paydown Pawn Loans"));
                            return (false);
                        }
                    }

                    DataRow[] newLoanRow = paydownDataTable.Select("old_loan_number=" + p.TicketNumber);
                    if (newLoanRow != null && newLoanRow.Count() > 0)
                    {
                        int newLoanNo = Utilities.GetIntegerValue(newLoanRow[0]["new_loan_number"], 0);
                        p.PrevTicketNumber = p.TicketNumber;
                        p.TicketNumber = newLoanNo;
                    }

                    //Go through the list of new fees in the pawn loan object and update the fee table
                    if (p.Fees.Count > 0)
                    {
                        var newFees = from fees in p.Fees
                                      where fees.OriginalAmount != 0
                                      select fees;
                        foreach (Fee custloanfee in newFees)
                        {
                            newLoanfeeTypes.Add(custloanfee.FeeType.ToString());
                            newLoanfeeOrigAmount.Add(custloanfee.OriginalAmount.ToString());
                            newLoanfeeAmount.Add(custloanfee.Value.ToString());
                            if (custloanfee.Prorated)
                                newLoanisProrated.Add("1");
                            else
                                newLoanisProrated.Add("0");
                            newLoanfeeDates.Add(custloanfee.FeeDate.FormatDateWithTimeZone());
                            newLoanfeeStateCodes.Add(FeeStates.ASSESSED.ToString());
                        }
                    }
                    if (newLoanfeeTypes.Count > 0)
                    {
                        if (!ProcessTenderProcedures.ExecuteInsertUpdatePawnFee(newLoanfeeTypes,
                                                                                feeRefType, p.TicketNumber, p.OrgShopNumber,
                                                                                newLoanfeeAmount, newLoanfeeOrigAmount, newLoanisProrated, newLoanfeeDates, newLoanfeeStateCodes, cds.UserName.ToLowerInvariant(),
                                                                                newLoanFeeRevCode, Utilities.GetLongValue(paydownReceiptNum), out errCode, out errDesc))
                        {
                            GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(
                                EndTransactionType.ROLLBACK);
                            if (FileLogger.Instance.IsLogError)
                                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Could not insert fee data for " + p.TicketNumber.ToString());
                            BasicExceptionHandler.Instance.AddException(
                                "Could not paydown pawn loans - insert fees failed :" + errCode + ": " + errDesc,
                                new ApplicationException("Could not paydown pawn loans"));
                            return (false);
                        }
                    }
                }
                //call the SP to update the DB to reflect the lost pawn ticket status

                if (lostTicketNumbers.Count > 0)
                {
                    do
                    {
                        bool retValue = new CustomerDBProcedures(GlobalDataAccessor.Instance.DesktopSession).updatePawnLostTicketFlag(lostTicketStoreNumber, lostTicketNumbers,
                                                                                      cds.UserName.ToLowerInvariant(), lostTicketFlag, out errCode, out errDesc);
                        if (retValue)
                        {
                            break;
                        }

                        dgr = MessageBox.Show(Commons.GetMessageString("ProcessLostTktFeeError"), "Error", MessageBoxButtons.RetryCancel);
                    } while (dgr == DialogResult.Retry);

                    if (dgr == DialogResult.Cancel)
                    {
                        GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(
                            EndTransactionType.ROLLBACK);

                        if (FileLogger.Instance.IsLogError)
                            FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Could not update lost ticket status when processing a paydown");
                        BasicExceptionHandler.Instance.AddException("Could not update lost ticket status when processing a paydown",
                                                                    new ApplicationException(
                                                                        "Error when updating the database for the ticket to update lost ticket status"));
                        return (false);
                    }
                }
                bool printTicket = false;

                foreach (PawnLoan ploan in pawnLoans)
                {
                    GlobalDataAccessor.Instance.DesktopSession.CurrentPawnLoan = ploan;
                    this.fullTicketNumber = ploan.TicketNumber;
                    analyzeActiveLoan();
                    if (!generatePawnTicketData())
                    {
                        if (FileLogger.Instance.IsLogError)
                            FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Could not print ticket data for " + ploan.TicketNumber.ToString());
                    }
                    else
                    {
                        //Print addendum if exists
                        if (printAddendum)
                        {
                            try
                            {
                                ReportObject rptObj = new ReportObject();
                                rptObj.ReportStore = GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreName;
                                rptObj.ReportStoreDesc = GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreAddress1 + "," +
                                                         GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreCityName + "," +
                                                         GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.State + "," +
                                                         GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreZipCode;
                                rptObj.ReportTitle = "Addendum";
                                rptObj.ReportTempFileFullName = SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseLogPath + "\\Addendum" + DateTime.Now.ToString("MMddyyyyhhmmssFFFFFFF") + ".pdf";

                                Reports.PawnTicketAddendumDocument pDocument = new Reports.PawnTicketAddendumDocument(PdfLauncher.Instance);
                                pDocument.ReportObject = rptObj;
                                pTicketAddendum.ticketNumber = (int)ploan.TicketNumber;
                                pDocument.CreateReport(pTicketAddendum);
                                var pDoc = new CouchDbUtils.PawnDocInfo();
                                OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;
                                SecuredCouchConnector cC = GlobalDataAccessor.Instance.CouchDBConnector;

                                //Set document add calls
                                pDoc.UseCurrentShopDateTime = true;
                                pDoc.SetDocumentSearchType(CouchDbUtils.DocSearchType.TICKET_ADDENDUM);
                                pDoc.StoreNumber = cds.CurrentSiteId.StoreNumber;
                                pDoc.CustomerNumber = cds.ActiveCustomer.CustomerNumber;
                                pDoc.TicketNumber = (int)ploan.TicketNumber;
                                pDoc.DocumentType = Document.DocTypeNames.PDF;
                                pDoc.DocFileName = pDocument.ReportObject.ReportTempFileFullName;

                                //Add this document to the pawn document registry and document storage
                                string errText;
                                if (!CouchDbUtils.AddPawnDocument(dA, cC, cds.UserName, ref pDoc, out errText))
                                {
                                    if (FileLogger.Instance.IsLogError)
                                        FileLogger.Instance.logMessage(LogLevel.ERROR, this,
                                                                       "Could not store addendum document in document storage: {0} - FileName: {1}", errText, pDocument.ReportObject.ReportTempFileFullName);
                                    BasicExceptionHandler.Instance.AddException(
                                        "Could not store addendum document in document storage",
                                        new ApplicationException("Could not store addendum document in document storage: " + errText));
                                }

                                if (SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled &&
                                    cds.LaserPrinter.IsValid)
                                {

                                    if (FileLogger.Instance.IsLogInfo)
                                    {
                                        FileLogger.Instance.logMessage(LogLevel.INFO, "ProcessTenderController", "Printing extension memo on {0}",
                                                                       cds.LaserPrinter);
                                    }
                                    string errMsg = PrintingUtilities.printDocument(rptObj.ReportTempFileFullName, cds.LaserPrinter.IPAddress,
                                                                                    cds.LaserPrinter.Port, 2);
                                    if (errMsg.IndexOf("SUCCESS", StringComparison.OrdinalIgnoreCase) == -1)
                                    {
                                        FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Cannot print extension memo");
                                    }
                                }


                            }
                            catch (Exception ex)
                            {
                                if (FileLogger.Instance.IsLogError)
                                    FileLogger.Instance.logMessage(LogLevel.ERROR, this,
                                                                   "Could not print addendum document" + ex.Message);

                            }
                        }


                        printTicket = true;
                    }
                }
                if (printTicket)
                {
                    pawnTicketPrint();

                }

                if (!this.updateTeller(pawnLoans))
                {
                    if (FileLogger.Instance.IsLogError)
                        FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Could not insert teller records!");
                    GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                    return (false);
                }
            }

            if (!this.commitProcessTenderTransaction("Process Tender (Paydown Block)"))
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Could not commit paydown transaction block");
                return (false);
            }

            return (true);
        }


        private bool insertPartialPaymentLoans(List<PawnLoan> pawnLoans, ref ReceiptDetailsVO receiptDetailsVO)
        {
            if (CollectionUtilities.isEmpty(pawnLoans))
            {
                FileLogger.Instance.logMessage(LogLevel.WARN, this, "Cannot process partial payment on an empty list of loans");
                return (false);
            }


            DesktopSession cds = GlobalDataAccessor.Instance.DesktopSession;
            CustomerVO cust = cds.ActiveCustomer;

            if (!this.beginProcessTenderTransaction("Process Tender (Partial Payment Block)"))
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Could not start partial payment transaction block");
                return (false);
            }
            string errorCode;
            string errorText;
            //set up store number list
            List<string> storeNumber = new List<string>();
            foreach (PawnLoan ploan in pawnLoans)
                storeNumber.Add(ploan.OrgShopNumber);

            DialogResult dgr = DialogResult.Retry;
            DataTable extensionData;
            string tranTime = ShopDateTime.Instance.ShopDate.ToShortDateString() + " " +
                              ShopDateTime.Instance.ShopTime.ToString();
            DataTable dtPaymentIds;

            do
            {
                bool retValue = ServiceLoanDBProcedures.ExecuteServicePawnLoanPartPayment(
                    pawnLoans, cust,
                    GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                    cds.FullUserName,
                    ShopDateTime.Instance.ShopDate.ToShortDateString(),
                    tranTime,
                    GlobalDataAccessor.Instance.DesktopSession.CashDrawerName,
                    ref receiptDetailsVO,
                    out dtPaymentIds,
                    out errorCode,
                    out errorText);
                if (retValue)
                {
                    ReceiptNumber = Utilities.GetIntegerValue(receiptDetailsVO.ReceiptNumber);
                    break;
                }
                if (errorCode == "101")
                {
                    MessageBox.Show(Commons.GetMessageString("ProcessingError") + errorText);
                    dgr = DialogResult.Cancel;
                }
                else

                    dgr = MessageBox.Show(Commons.GetMessageString("ProcessingError"), "Error", MessageBoxButtons.RetryCancel);
            } while (dgr == DialogResult.Retry);

            if (dgr == DialogResult.Cancel)
            {
                if (FileLogger.Instance.IsLogError)
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Could not complete partial payment on loans");
                BasicExceptionHandler.Instance.AddException("Could not complete partial payment on loans", new ApplicationException("Could not complete partial payment on loans"));
                GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                return (false);
            }

            string trandate = ShopDateTime.Instance.ShopDate.ToShortDateString() + " " +
                               ShopDateTime.Instance.ShopTime.ToString();
            const string feeRefType = "PARTP";
            foreach (PawnLoan p in pawnLoans)
            {
                if (p == null)
                    continue;
                List<string> newFeeTypes = new List<string>();
                List<string> newFeeAmount = new List<string>();
                List<string> newFeeOrigAmount = new List<string>();
                List<string> newFeeisProrated = new List<string>();
                List<string> newFeeDates = new List<string>();
                List<string> newFeeStateCodes = new List<string>();


                //Go through the list of fees in the pawn loan object and insert into fee table
                string feeOpRevCode = FeeRevOpCodes.PARTP.ToString();
                if (p.Fees.Count > 0)
                {
                    var origFees = from fees in p.Fees
                                   where fees.Value != 0
                                   && fees.FeeState == FeeStates.ASSESSED
                                   select fees;
                    foreach (Fee custloanfee in origFees)
                    {
                        newFeeTypes.Add(custloanfee.FeeType.ToString());
                        newFeeOrigAmount.Add(custloanfee.OriginalAmount.ToString());
                        newFeeAmount.Add(custloanfee.Value.ToString());
                        if (custloanfee.Prorated)
                            newFeeisProrated.Add("1");
                        else
                            newFeeisProrated.Add("0");
                        newFeeDates.Add(trandate);
                        if (custloanfee.Waived)
                            newFeeStateCodes.Add(FeeStates.WAIVED.ToString());
                        newFeeStateCodes.Add(FeeStates.PAID.ToString());
                    }
                }
                int pmtId = 0;
                foreach (DataRow dr in
                    dtPaymentIds.Rows.Cast<DataRow>().Where(dr => Utilities.GetIntegerValue(dr["ticket_number"]) == p.TicketNumber))
                {
                    pmtId = Utilities.GetIntegerValue(dr["pmt_id"]);
                }

                if (newFeeTypes.Count > 0)
                {
                    if (!ProcessTenderProcedures.ExecuteInsertUpdatePawnFee(newFeeTypes,
                                                                            feeRefType, pmtId, p.OrgShopNumber,
                                                                            newFeeAmount, newFeeOrigAmount, newFeeisProrated, newFeeDates, newFeeStateCodes, cds.UserName.ToLowerInvariant(),
                                                                            feeOpRevCode, ReceiptNumber, out errorCode, out errorText))
                    {
                        GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(
                            EndTransactionType.ROLLBACK);
                        if (FileLogger.Instance.IsLogError)
                            FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Could not update or insert fee data");
                        BasicExceptionHandler.Instance.AddException(
                            "Could not complete partial payment on pawn loans - update fees failed :" + errorText + ": " + errorText,
                            new ApplicationException("Could not complete partial payment on pawn loans"));
                        return (false);
                    }
                }
            }


            if (!this.commitProcessTenderTransaction("Process Tender (Partial Payment Block)"))
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Could not commit Extend transaction block");
                return (false);
            }


            return (true);
        }


        #endregion

        /// <summary>
        /// 
        /// </summary>
        public ProcessTenderController()
        {
            lock (mutexIntObj)
            {
                this.gunItemNumberIndices = new List<PairType<int, long>>();
                this.jewelryItemIndices = new List<PairType<int, long>>();
                this.actualGunLineNumbers = new List<int>();
                this.ticketNumber = 0;
                this.pawnTickets = new List<Hashtable>();
                this.retValues = new List<bool>();
                this.errorCodes = new List<string>();
                this.errorTexts = new List<string>();
                this.docinfo = new DataTable();
                this.ipportinfo = new DataTable();
                this.printerinfo = new DataTable();
                this.madeDate = ShopDateTime.Instance.ShopDate;
                this.madeTime = ShopDateTime.Instance.ShopShortTime;

                this.pawnTicketFiles = new List<string>();
                this.ProcessedTickets = new List<PairType<long, string>>();
                BUSINESS_UNIT = GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.BusinessUnit;
                STORE_NUMBER = GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreNumber;
                STORE_ADDRESS = GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreAddress1;
                STORE_CITY = GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreCityName;
                STORE_STATE = GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.State;
                STORE_ZIP = GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreZipCode;
                STORE_NAME = GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreName;
                STORE_PHONE = GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StorePhoneNo;

                // country code is not implemented at this time
                //if(GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.CountryCode == "US")
                if (STORE_PHONE.Length == 12) // (817)2651234
                {
                    formattedPhone = formattedPhone = STORE_PHONE.Substring(0, 5)
                                                      + " " + STORE_PHONE.Substring(5, 3)
                                                      + "-" + STORE_PHONE.Substring(8, 4);
                }
                else
                {
                    formattedPhone = STORE_PHONE;
                }
            }
        }

        //******************************************************************
        // this method builds our subtotal detail strings and adds them to 
        // our data dictionary
        //
        //  25-mar-2010  rjm
        //  GJL 03/30/2010 - Must modify the code below, inefficient
        //                   string processing and bad coding practice
        //******************************************************************
        private void BuildSubTotalLine(Dictionary<string, string> data,
                                       decimal amount, string serviceDesc,
                                       ref int detailCount,
                                       List<ReceiptItems> receiptItemsCollection,
                                       ref int loanDataCount)
        {
            int spaceLen = 0;
            string detailLine = string.Empty;
            string detailKey = string.Empty;
            string spacer = string.Empty;

            foreach (ReceiptItems receiptItem in receiptItemsCollection)
            {
                string serviceDescData = string.Empty;
                string ticketNum = string.Empty;
                string ticketAmount = string.Empty;
                string detailToAdd = string.Empty;
                serviceDescData = receiptItem.DescriptionData;
                ticketNum = receiptItem.TicketNumber;
                ticketAmount = receiptItem.TicketAmount;
                int lengthWithoutSpaces = string.Concat(serviceDescData, loanDataCount, ticketNum).Length + 2; // add two for the extra spaces
                int ticketAmountSize = MAX_LEN - lengthWithoutSpaces;
                string finalDetailLine = string.Format("{0} {1} {2}{3," + ticketAmountSize + "}", loanDataCount, serviceDescData, ticketNum, ticketAmount);
                //spaceLen = MAX_BOLD_LEN - (serviceDescData.Length + 1 +
                //          loanDataCount.ToString().Length + 1 +
                //          ticketAmount.Length);
                //spacer = "".PadLeft(spaceLen + 1, ' ');

                //detailToAdd = "<B>" + loanDataCount + " " + serviceDescData + " " + ticketNum + spacer + ticketAmount;
                detailToAdd = "<B>" + finalDetailLine;
                detailKey = string.Format(
                    "DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                detailCount++;
                data.Add(detailKey, detailToAdd);
                detailKey = string.Format(
                    "DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                detailCount++;
                data.Add(detailKey, "<S>");
                loanDataCount++;
            }
            detailKey = string.Format(
                "DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
            ++detailCount;
            spaceLen = MAX_LEN - (serviceDesc.Length + amount.ToString("C").Length + 1);
            spacer = "".PadLeft(spaceLen, ' ');
            detailLine = "<R>" + serviceDesc + spacer + "$" + amount;
            data.Add(detailKey, detailLine);
            detailKey = string.Format(
                "DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
            ++detailCount;
            data.Add(detailKey, "<S>");
            detailKey = string.Format(
                "DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
            ++detailCount;
            data.Add(detailKey, "<S>");
        }

        //******************************************************************
        // this method builds our detail strings and adds them to our data 
        // dictionary
        //
        //  25-mar-2010  rjm
        //  GJL 03/30/2010 - Must modify the code below, inefficient
        //                   string processing and bad coding practice
        //******************************************************************
        private void BuildDetailLine(List<ReceiptItems> saleItemsArraylist,
                                     decimal amount, string serviceDesc, string ticketNum)
        {
            string TicketAmount = amount.ToString("C");
            ReceiptItems items = new ReceiptItems();
            items.DescriptionData = serviceDesc;
            items.TicketAmount = TicketAmount;
            items.TicketNumber = ticketNum;
            saleItemsArraylist.Add(items);
        }

        private void WriteSaleDetailLines(Dictionary<string, string> data,
                                          ref int detailCount,
                                          List<ReceiptItems> receiptItemsCollection,
                                          ref int salesItemsCounter, SaleVO currentSale, ProcessTenderMode mode)
        {
            string detailLine = string.Empty;
            string detailKey = string.Empty;
            try
            {
                decimal transCouponPercentage = 0.0m;
                foreach (ReceiptItems receiptItem in receiptItemsCollection)
                {
                    string descriptionData = string.Empty;
                    int qty = 0;
                    string itemAmount = string.Empty;
                    string detailToAdd = string.Empty;
                    string totalItemAmount = string.Empty;
                    decimal itemAmountDec = 0.0m;

                    string counterChar = ".";
                    descriptionData = receiptItem.DescriptionData;
                    string[] descriptData = descriptionData.Split(new char[] { ';' });
                    //receiptItem.Quantity = 1;
                    qty = receiptItem.Quantity;
                    string atSign = "@";
                    if (mode == ProcessTenderMode.RETAILREFUND)
                    {
                        itemAmountDec = receiptItem.ItemLineAmount;
                        itemAmount = itemAmountDec.ToString("C");
                        totalItemAmount = (itemAmountDec * receiptItem.Quantity).ToString("C");
                    }
                    else
                    {
                        totalItemAmount = (receiptItem.ItemAmount * receiptItem.Quantity).ToString("C");
                        itemAmount = receiptItem.ItemAmount.ToString("C");
                    }
                    int lengthWithoutSpaces = 0;
                    int leftOverLen = 0;
                    string finalDetailLineFirstLine = string.Empty;
                    bool addNextDescription = false;
                    string lineDesc = string.Empty;
                    string icn = string.Empty;
                    string icnFormatted = string.Empty;
                    string spacer1 = string.Empty;
                    string spacer2 = string.Empty;
                    string icnFirst6 = string.Empty;
                    string icnNext6 = string.Empty;
                    string icnNext1 = string.Empty;
                    string icnNext3 = string.Empty;
                    string icnLast2 = string.Empty;
                    int spaceLen;
                    //add Item Price line
                    //data.Remove(detailKey);
                    //detailCount = detailCount - 1;
                    detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                    spaceLen = MAX_LEN - (salesItemsCounter.ToString().Length + counterChar.Length + qty.ToString().Length + atSign.Length + itemAmount.ToString().Length + totalItemAmount.ToString().Length);
                    spacer1 = "".PadLeft(spaceLen / 2, ' ');
                    //spacer2 = "".PadLeft(5, ' ');
                    ++detailCount;
                    detailLine = salesItemsCounter + counterChar + spacer1 + qty + atSign + itemAmount + spacer1 + totalItemAmount;
                    data.Add(detailKey, "<R>" + detailLine);

                    //add ICN line
                    //data.Remove(detailKey);
                    //detailCount = detailCount - 1;      

                    spacer1 = "".PadLeft(2, ' ');
                    icn = receiptItem.ICN;
                    icnFirst6 = icn.Substring(0, 6);
                    icnNext6 = icn.Substring(6, 6);
                    icnNext1 = icn.Substring(12, 1);
                    icnNext3 = icn.Substring(13, 3);
                    icnLast2 = icn.Substring(icn.Length - 2, 2);
                    //icnNext6 = icnNext6;
                    //icnNext3 = icnNext3;
                    icnFormatted = spacer1 + icnFirst6 + icnNext6 + icnNext1 + icnNext3 + icnLast2;
                    detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                    detailCount++;
                    data.Add(detailKey, "<B>" + icnFormatted);
                    lineDesc = string.Empty;

                    for (int i = 0; i <= descriptData.Length - 1; i++)
                    {
                        if (addNextDescription)
                            lineDesc = lineDesc + ";" + descriptData[i];
                        else
                            lineDesc = descriptData[i];
                        if (!string.IsNullOrEmpty(lineDesc))
                        {
                            //if (i > 0)
                            //{
                            lineDesc = lineDesc.TrimStart(' ');
                            lengthWithoutSpaces = string.Concat(lineDesc).Length + 3; // add two for the extra spaces
                            leftOverLen = MAX_LEN - lengthWithoutSpaces;
                            int lenNextNode = 0;
                            if (descriptData[i + 1] != null || !string.IsNullOrEmpty(descriptData[i + 1]))
                            {
                                lenNextNode = descriptData[i + 1].Length;
                                if (lenNextNode != 0 && lenNextNode < leftOverLen)
                                    addNextDescription = true;
                                else
                                    addNextDescription = false;
                            }
                            else
                            {
                                addNextDescription = false;
                            }
                            finalDetailLineFirstLine = lineDesc;// +space3;
                            //}
                            /*else
                            {
                            space1 = string.Empty;
                            if (lineDesc.Length >= 14)
                            {
                            lineDesc = lineDesc.Substring(0, 14);
                            lineDesc = lineDesc + "..";
                            }
                            lengthWithoutSpaces = string.Concat("", salesItemsCounter, counterChar, lineDesc, qty, atSign, itemAmount, totalItemAmount).Length + 2; // add two for the extra spaces
                            leftOverLen = MAX_LEN - lengthWithoutSpaces;
                            //finalDetailLineFirstLine = salesItemsCounter + counterChar + ' ' + firstLineDesc + ' ' + qty + atSign + itemAmount + ' ' + totalItemAmount;
                            space1 = "".PadLeft(leftOverLen / 2, ' ');
                            space2 = space1 + ' ';
                            //finalDetailLineFirstLine = string.Format("{0} {1} {2}{3," + ticketAmountSize + "}", salesItemsCounter, firstLineDesc, itemAmount);
                            finalDetailLineFirstLine = salesItemsCounter + counterChar + ' ' + lineDesc + space1 + qty + atSign + itemAmount + space2 + totalItemAmount;
                            lenFinalString = finalDetailLineFirstLine.Length;
                            }*/
                            if (!addNextDescription)
                            {
                                detailToAdd = "".PadLeft(3, ' ') + finalDetailLineFirstLine;
                                detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                                detailCount++;
                                data.Add(detailKey, detailToAdd);
                                lineDesc = string.Empty;
                            }
                        }
                    }
                    //here write code to add comments to receipt if they exist
                    int CommentsLen = 39;
                    if (mode == ProcessTenderMode.RETAILSALE && !string.IsNullOrEmpty(receiptItem.Comments))
                    {
                        if (receiptItem.Comments.Length > CommentsLen)
                        {
                            string[] commentsData = receiptItem.Comments.Split(new char[] { ' ' });
                            string commentToAdd = string.Empty;
                            bool concatNextComment = false;
                            for (int i = 0; i <= commentsData.Length - 1; i++)
                            {
                                if (concatNextComment)
                                    commentToAdd = commentToAdd + " " + commentsData[i];
                                else
                                    commentToAdd = commentsData[i];

                                if (!string.IsNullOrEmpty(commentToAdd))
                                {
                                    //get the length of the next element
                                    if (commentsData.Length - 1 != i)
                                    {
                                        if (!string.IsNullOrEmpty(commentsData[i + 1]))
                                        {
                                            int lenNextElement = commentsData[i + 1].Length;
                                            int lenLeftOver = CommentsLen - commentToAdd.Length;
                                            if (lenNextElement != 0 && lenNextElement < lenLeftOver)
                                                concatNextComment = true;
                                            else
                                                concatNextComment = false;
                                        }
                                    }
                                    else
                                    {
                                        concatNextComment = false;
                                    }
                                    if (!concatNextComment)
                                    {
                                        detailToAdd = "".PadLeft(3, ' ') + commentToAdd;
                                        detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                                        detailCount++;
                                        data.Add(detailKey, detailToAdd);
                                        lineDesc = string.Empty;
                                    }

                                }
                                //get the length of the current element
                                // add the two together and subtract their total from MAX_LEN
                                //if it exceeds max len, then set addcomment to true. do not concatenate the strings. 
                            }
                        }
                        else
                        {
                            //add to data
                            detailToAdd = "".PadLeft(3, ' ') + receiptItem.Comments;
                            detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                            detailCount++;
                            data.Add(detailKey, detailToAdd);
                        }
                    }
                    //here write code to add coupon amount and coupon transaction amount
                    //if there's coupon info, add empty space, then write the coupon info. 
                    if (mode == ProcessTenderMode.RETAILSALE
                        || mode == ProcessTenderMode.LAYAWAY
                        || mode == ProcessTenderMode.RETAILVOID
                        || mode == ProcessTenderMode.LAYAWAYVOID
                        )
                    {
                        string couponAmountText;
                        string transCouponAmountText;
                        string transCouponDescrip;
                        string couponDescrip;
                        if (receiptItem.CouponAmount > 0.0m)
                        {
                            //compute the receipt coupon percentage here
                            if (mode == ProcessTenderMode.RETAILVOID)
                            {
                                if (receiptItem.ItemAmount > 0.0m && receiptItem.CouponAmount > 0.0m)
                                    receiptItem.CouponPercentage = Math.Round((receiptItem.CouponAmount * 100) / receiptItem.ItemAmount, 2);
                            }
                            detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                            detailCount++;
                            data.Add(detailKey, "<S>");

                            if (receiptItem.CouponPercentage > 0.0m)
                                couponDescrip = "Coupon " + receiptItem.CouponPercentage.ToString() + "%" + " off";
                            else
                                couponDescrip = "Coupon " + receiptItem.CouponAmount.ToString("C") + " off";
                            detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                            couponAmountText = FormatAmountToPrint(receiptItem.CouponAmount);
                            spaceLen = MAX_LEN - (couponDescrip.Length + couponAmountText.Length + 2);
                            spacer1 = "".PadLeft(spaceLen, ' ');
                            detailLine = couponDescrip + spacer1 + "(" + couponAmountText + ")";
                            ++detailCount;
                            data.Add(detailKey, "<R>" + detailLine);
                        }

                        if (receiptItem.ProratedCouponAmount > 0.0m && receiptItem.CouponAmount <= 0.0m)
                        {
                            detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                            detailCount++;
                            data.Add(detailKey, "<S>");

                            transCouponDescrip = "Transaction Level Coupon";
                            detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                            transCouponAmountText = FormatAmountToPrint(receiptItem.ProratedCouponAmount);
                            spaceLen = MAX_LEN - (transCouponDescrip.Length + transCouponAmountText.Length + 2);
                            spacer1 = "".PadLeft(spaceLen, ' ');
                            detailLine = transCouponDescrip + spacer1 + "(" + transCouponAmountText + ")";
                            ++detailCount;
                            data.Add(detailKey, "<R>" + detailLine);

                            //add new lines to describe Transaction level coupons

                            if (mode == ProcessTenderMode.RETAILVOID)
                            {
                                //compute the transaction coupon percent here
                                //transCouponPercentage
                                if (GlobalDataAccessor.Instance.DesktopSession.ActiveRetail != null)
                                {
                                    if (GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.CouponPercentage > 0)
                                    {
                                        if (transCouponPercentage <= 0.0m && receiptItem.ProratedCouponAmount > 0.0m)
                                            transCouponPercentage = Math.Round(receiptItem.ProratedCouponAmount * 100 / (receiptItem.ItemAmount), 2);
                                    }
                                }
                            }
                            else
                            {
                                if (GlobalDataAccessor.Instance.DesktopSession.ActiveRetail != null)
                                {
                                    if (GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.CouponPercentage > 0)
                                    {
                                        transCouponPercentage = GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.CouponPercentage;
                                    }
                                }
                            }
                            if (transCouponPercentage > 0.0m)
                            {
                                string couponPercentText = "(" + transCouponPercentage + "% off" + ")";
                                detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                                //spaceLen = MAX_LEN - (couponPercentText.Length);
                                spacer1 = "".PadLeft(5, ' ');
                                detailLine = couponPercentText;
                                ++detailCount;
                                data.Add(detailKey, "<R>" + detailLine);
                            }
                            else
                            {
                                //string couponPercentText = "(" + transCouponPercentage + "%" + ")";
                                detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                                //spaceLen = MAX_LEN - (transCouponAmountText.Length);
                                spacer1 = "".PadLeft(5, ' ');
                                detailLine = "($" + receiptItem.ProratedCouponAmount + " off)";
                                ++detailCount;
                                data.Add(detailKey, "<R>" + detailLine);
                            }
                        }
                        else if (receiptItem.ProratedCouponAmount > 0.0m && receiptItem.CouponAmount > 0.0m)
                        {
                            transCouponDescrip = "Transaction Level Coupon";
                            detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                            transCouponAmountText = FormatAmountToPrint(receiptItem.ProratedCouponAmount);
                            spaceLen = MAX_LEN - (transCouponDescrip.Length + transCouponAmountText.Length + 2);
                            spacer1 = "".PadLeft(spaceLen, ' ');
                            detailLine = transCouponDescrip + spacer1 + "(" + transCouponAmountText + ")";
                            ++detailCount;
                            data.Add(detailKey, "<R>" + detailLine);

                            if (mode == ProcessTenderMode.RETAILVOID)
                            {
                                if (GlobalDataAccessor.Instance.DesktopSession.ActiveRetail != null)
                                {
                                    if (GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.CouponPercentage > 0)
                                    {
                                        if (transCouponPercentage <= 0.0m && receiptItem.ProratedCouponAmount > 0.0m)
                                            transCouponPercentage = Math.Round(receiptItem.ProratedCouponAmount * 100 / (receiptItem.ItemAmount - receiptItem.CouponAmount), 2);
                                    }
                                }
                            }
                            else
                            {
                                if (GlobalDataAccessor.Instance.DesktopSession.ActiveRetail != null)
                                {
                                    if (GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.CouponPercentage > 0)
                                    {
                                        transCouponPercentage = GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.CouponPercentage;
                                    }
                                }
                            }
                            if (transCouponPercentage > 0.0m)
                            {
                                string couponPercentText = "(" + transCouponPercentage + "% off" + ")";
                                detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                                //spaceLen = MAX_LEN - (couponPercentText.Length);
                                spacer1 = "".PadLeft(5, ' ');
                                detailLine = couponPercentText;
                                ++detailCount;
                                data.Add(detailKey, "<R>" + detailLine);
                            }
                            else
                            {
                                //string couponPercentText = "(" + transCouponPercentage + "%" + ")";
                                detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                                //spaceLen = MAX_LEN - (transCouponAmountText.Length);
                                spacer1 = "".PadLeft(5, ' ');
                                detailLine = "($" + receiptItem.ProratedCouponAmount + " off)";
                                ++detailCount;
                                data.Add(detailKey, "<R>" + detailLine);
                            }
                        }
                    }
                    detailKey = string.Format("DETAIL{0}", detailCount.ToString().PadLeft(3, '0'));
                    detailCount++;
                    data.Add(detailKey, "<S>");
                    salesItemsCounter++;
                }
            }
            catch (Exception e)
            {
                return;
            }
        }

        private void AddSaleDetailLineToList(List<ReceiptItems> data,
                                             RetailItem item, ProcessTenderMode mode)
        {
            ReceiptItems items = new ReceiptItems();
            items.DescriptionData = item.TicketDescription;
            items.ICN = item.Icn;
            if (mode == ProcessTenderMode.LAYAWAY || mode == ProcessTenderMode.RETAILSALE && (!string.IsNullOrEmpty(item.UserItemComments)))
                items.Comments = "Comments: " + item.UserItemComments;
            if (mode == ProcessTenderMode.LAYAWAY || mode == ProcessTenderMode.RETAILSALE)
                items.ItemAmount += item.NegotiatedPrice;
            else
                items.ItemAmount += item.RetailPrice;
            if (item.Quantity > 0)
                items.Quantity = item.Quantity;
            else
                items.Quantity = 1;
            items.CouponAmount = item.CouponAmount;
            items.ProratedCouponAmount = item.ProratedCouponAmount;
            items.CouponPercentage = item.CouponPercentage;
            if (item.RefundQuantity > 0)
                items.ItemLineAmount = Math.Round(items.ItemAmount - ((item.CouponAmount / item.RefundQuantity) + (item.ProratedCouponAmount / item.RefundQuantity)), 2);
            else
                items.ItemLineAmount = Math.Round((items.ItemAmount - item.CouponAmount - item.ProratedCouponAmount), 2);


            data.Add(items);
        }

        public bool ExecuteProcessTender(ProcessTenderMode mode)
        {
            this.operationalMode = mode;
            if (mode == ProcessTenderMode.SERVICELOAN && CollectionUtilities.isEmpty(GlobalDataAccessor.Instance.DesktopSession.ServiceLoans))
            {
                return (false);
            }
            if ((mode == ProcessTenderMode.PURCHASE || mode == ProcessTenderMode.RETURNBUY) && CollectionUtilities.isEmpty(GlobalDataAccessor.Instance.DesktopSession.Purchases))
            {
                return (false);
            }
            if ((mode == ProcessTenderMode.RETAILSALE || mode == ProcessTenderMode.RETURNSALE) && GlobalDataAccessor.Instance.DesktopSession.ActiveRetail == null)
                return false;
            if (mode == ProcessTenderMode.LAYPAYMENT && GlobalDataAccessor.Instance.DesktopSession.ServiceLayaways == null)
                return false;
            if (mode == ProcessTenderMode.LAYPAYMENTREF && GlobalDataAccessor.Instance.DesktopSession.ActiveLayaway == null)
                return false;

            bool rt = false;
            this.ProcessedTickets = new List<PairType<long, string>>();
            switch (mode)
            {
                case ProcessTenderMode.NEWLOAN:
                    rt = this.executeNewLoan();
                    break;

                case ProcessTenderMode.SERVICELOAN:
                    rt = this.executeServicePawnLoan();
                    break;
                case ProcessTenderMode.PURCHASE:
                    rt = this.executePurchase();
                    break;
                case ProcessTenderMode.VENDORPURCHASE:
                    rt = executeVendorPurchase();
                    break;
                case ProcessTenderMode.RETURNBUY:
                    rt = this.executeBuyReturn();
                    break;
                case ProcessTenderMode.RETAILSALE:
                    rt = this.executeSale();
                    break;
                case ProcessTenderMode.RETURNSALE:
                    rt = this.executeRefundSale();
                    break;
                case ProcessTenderMode.LAYAWAY:
                    rt = this.executeLayaway();
                    break;
                case ProcessTenderMode.LAYPAYMENT:
                    rt = this.executeLayawayPayment();
                    break;
                case ProcessTenderMode.LAYPAYMENTREF:
                    rt = this.executeRefundLayawayPayment();
                    break;
                default:
                    throw new ArgumentOutOfRangeException("mode");
            }
            return (rt);
        }

        private bool executePurchase()
        {
            this.resetData();
            //Get the purchase object
            PurchaseVO purchaseObject = null;
            if (CollectionUtilities.isNotEmpty(GlobalDataAccessor.Instance.DesktopSession.Purchases))
            {
                purchaseObject = GlobalDataAccessor.Instance.DesktopSession.Purchases[0];
                //Analyze purchase to determine item types
                this.analyzeActivePurchase();
            }
            else
            {
                return false;
            }

            //Start transaction block
            if (!this.beginProcessTenderTransaction("Process Tender (Next Num block)"))
            {
                return (false);
            }

            //Get gun numbers if necessary

            if (purchaseObject != null && !this.retrieveGunNumbers(purchaseObject) && CollectionUtilities.isNotEmpty(this.gunItemNumberIndices))
            {
                GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                BasicExceptionHandler.Instance.AddException("Failed to retrieve gun numbers", new ApplicationException());
                return (false);
            }

            //Commit teller transaction
            if (!this.commitProcessTenderTransaction("Process Tender (Next Num block)"))
            {
                return (false);
            }

            //Start transaction block
            if (!this.beginProcessTenderTransaction("Process Tender (New Purchase block)"))
            {
                return (false);
            }

            //Perform inserts
            if (!this.performProcessTenderPurchaseInserts())
            {
                GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                BasicExceptionHandler.Instance.AddException("Failed to insert purchase data", new ApplicationException());
                return (false);
            }

            //Status the loan
            GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.LoanStatus = ProductStatus.PUR;

            //Add receipt details
            ReceiptDetailsVO rdVo = new ReceiptDetailsVO();
            List<string> refDate = new List<string>();
            List<string> refNumber = new List<string>();
            List<string> refType = new List<string>();
            List<string> refEvent = new List<string>();
            List<string> refAmount = new List<string>();
            List<string> refStore = new List<string>();
            List<string> refTime = new List<string>();

            // ref date for purchase is the date made
            rdVo.ReceiptDate = ShopDateTime.Instance.ShopDate;
            refDate.Add(ShopDateTime.Instance.ShopDate.FormatDate());

            //ref time for purchase is the current time

            refTime.Add(ShopDateTime.Instance.ShopTransactionTime);

            // ref number for new pawn loan is the ticket number

            refNumber.Add(purchaseNumber.ToString());

            // ref type for purchase is 2
            refType.Add("2");

            // ref event for new purchase is "PUR"
            refEvent.Add("PUR");

            // ref amount for new pawn loan is the loan amount
            refAmount.Add(GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.Amount.ToString());

            // ref store for purchase is the store the receipt was printed at
            refStore.Add(GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreNumber);

            //Update teller and insert receipt details info and inform user to disburse cash
            int rcptNumber;
            string errorCode;
            string errorText;
            GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.PurchaseTenderType = PurchaseTenderTypes.CASHOUT.ToString();
            if (!(PurchaseProcedures.InsertPurchasePaymentDetails(GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreNumber,
                                                                  GlobalDataAccessor.Instance.DesktopSession.CashDrawerName, GlobalDataAccessor.Instance.DesktopSession.FullUserName,
                                                                  ShopDateTime.Instance.ShopDate.ToShortDateString(), refNumber, refDate, refTime, refEvent, refAmount,
                                                                  ShopDateTime.Instance.ShopTransactionTime.ToString(), GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.PurchaseTenderType, out rcptNumber, out errorCode, out errorText)))
            {
                GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                BasicExceptionHandler.Instance.AddException("Failed to update teller", new ApplicationException());
                return (false);
            }

            //Commit teller transaction
            if (!this.commitProcessTenderTransaction("Process Tender (Purchases block)"))
            {
                return (false);
            }

            GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.TicketNumber = Utilities.GetIntegerValue(purchaseNumber, 0);
            List<PurchaseVO> purchases = new List<PurchaseVO>(1) { GlobalDataAccessor.Instance.DesktopSession.ActivePurchase };
            //Print receipt
            rdVo.RefNumbers = refNumber;
            rdVo.ReceiptNumber = rcptNumber.ToString();
            rdVo.RefEvents = refEvent;
            rdVo.RefStores = refStore;
            rdVo.RefTypes = refType;
            if (SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled)
            {
                bool rt = false;
                try
                {
                    rt = this.generatePurchaseReceipt(
                        ProcessTenderMode.PURCHASE,
                        GlobalDataAccessor.Instance.DesktopSession.UserName.ToLowerInvariant(),
                        GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer,
                        purchases,
                        "",
                        rdVo);
                }
                catch (Exception)
                {
                    rt = false;
                }

                if (!rt)
                {
                    if (FileLogger.Instance.IsLogError)
                        FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Could not print receipt for purchases");
                    BasicExceptionHandler.Instance.AddException("Could not print receipt for purchases", new ApplicationException("Could not print receipt while doing a customer purchase"));
                    return (false);
                }

                bool success = true;
                if (CollectionUtilities.isEmpty(this.ProcessedTickets))
                {
                    this.ProcessedTickets = new List<PairType<long, string>>();
                }
                long idx = 0;
                foreach (var curPurch in purchases)
                {
                    string fileNameGenerated;
                    bool chkStorePurchaseDocument;
                    if (GlobalDataAccessor.Instance.CurrentSiteId.State.Equals(States.Indiana))
                    {
                        chkStorePurchaseDocument = PurchaseDocumentIndiana.GeneratePurchaseDocumentIndiana(
                            GlobalDataAccessor.Instance.DesktopSession, curPurch, out fileNameGenerated);
                    }
                    else
                    {
                        chkStorePurchaseDocument = PurchaseDocumentGenerator.GeneratePurchaseDocument(
                            GlobalDataAccessor.Instance.DesktopSession, curPurch, out fileNameGenerated);
                    }
                    if (!chkStorePurchaseDocument)
                    {
                        success = false;
                    }
                    else
                    {
                        this.ProcessedTickets.Add(new PairType<long, string>(idx, fileNameGenerated));
                        this.TicketFiles.Add(fileNameGenerated);
                        ++idx;
                    }
                }
                if (!success)
                {
                    BasicExceptionHandler.Instance.AddException("Failed to print tickets and/or receipts",
                                                                new ApplicationException());
                    return (false);
                }
                if (purchaseObject != null)
                {
                    for (int i = 0; i < 2; i++) //BZ # 505
                    {
                        HandleWipeDriveCategory(purchaseObject, rcptNumber);
                    }
                }
            }

            return (true);
        }

        private bool executeSale()
        {
            this.resetData();
            bool shopCreditUsed = false;
            decimal shopCreditAmount = 0.0M;
            string trandate = ShopDateTime.Instance.ShopDate.ToShortDateString() + " " +
                              ShopDateTime.Instance.ShopTime.ToString();

            SaleVO currentSale = GlobalDataAccessor.Instance.DesktopSession.ActiveRetail;
            if (currentSale == null)
                return false;
            List<RetailItem> gunItems = (from item in GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.RetailItems
                                         where item.IsGun
                                         select item).ToList();
            List<string> icn = GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.RetailItems.Select(item => item.Icn.ToString()).ToList();
            List<string> icnRetailPrice = GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.RetailItems.Select(item => item.NegotiatedPrice.ToString()).ToList();
            List<string> icnToAdd = icn.Where(s => s.Substring(12, 1) == "8").ToList();
            List<int> qty = GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.RetailItems.Select(item => item.Quantity).ToList();
            //List<string> jewelryCase = GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.RetailItems.Select(item => item.JeweleryCaseNumber).ToList();
            List<string> jewelryCase = GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.RetailItems.Select(item => (string.IsNullOrEmpty(item.NxtComments)) ? item.JeweleryCaseNumber : item.NxtComments).ToList();


            for (var i = 0; i < jewelryCase.Count; i++)
            {
                if (jewelryCase[i] == null)
                    jewelryCase[i] = string.Empty;
            }

            string errorCode;
            string errorMessage;
            int saleTktNumber;
            List<string> addlMdseIcn = new List<string>();
            List<string> addlMdseCouponIcn = new List<string>();
            List<string> addlMdseTranIcn = new List<string>();
            List<string> addlcouponCodes = new List<string>();
            List<string> addlcouponAmounts = new List<string>();
            List<string> addltranCouponCodes = new List<string>();
            List<string> addltranCouponAmounts = new List<string>();
            List<string> couponCodes = new List<string>();
            List<string> couponAmounts = new List<string>();
            List<string> tranCouponCodes = new List<string>();
            List<string> tranCouponAmounts = new List<string>();
            List<string> infoType = new List<string>();
            List<string> addlcouponinfoType = new List<string>();
            List<string> addltraninfoType = new List<string>();
            List<string> addlMdseRetPrice = new List<string>();
            List<string> addlMdseItemRetPrice = new List<string>();
            List<string> addlMdseTranRetPrice = new List<string>();

            decimal totalCouponAmt = 0.0m;
            //Check if coupon was used for any item or for the transaction
            foreach (RetailItem rItem in GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.RetailItems)
            {

                if (rItem.CouponAmount > 0)
                {
                    addlMdseItemRetPrice.Add(rItem.NegotiatedPrice.ToString());
                    addlcouponinfoType.Add(rItem.SaleType.ToString());
                    string opCode = Commons.GetInOpCode(TenderTypes.COUPON.ToString(), TenderTypes.CREDITCARD.ToString());
                    if (GlobalDataAccessor.Instance.DesktopSession.TenderAmounts != null)
                        GlobalDataAccessor.Instance.DesktopSession.TenderAmounts.Add(rItem.CouponAmount.ToString());
                    if (GlobalDataAccessor.Instance.DesktopSession.TenderReferenceNumber != null)
                        GlobalDataAccessor.Instance.DesktopSession.TenderReferenceNumber.Add(rItem.CouponCode);
                    if (GlobalDataAccessor.Instance.DesktopSession.TenderTypes != null)
                        GlobalDataAccessor.Instance.DesktopSession.TenderTypes.Add(opCode);
                    addlMdseCouponIcn.Add(rItem.Icn);
                    addlcouponCodes.Add(rItem.CouponCode);
                    addlcouponAmounts.Add(rItem.CouponAmount.ToString());
                    totalCouponAmt += rItem.CouponAmount;

                }
            }
            if (GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.CouponAmount > 0)
            {

                string opCode = Commons.GetInOpCode(TenderTypes.COUPON.ToString(), TenderTypes.CREDITCARD.ToString());
                if (GlobalDataAccessor.Instance.DesktopSession.TenderAmounts != null)
                    GlobalDataAccessor.Instance.DesktopSession.TenderAmounts.Add(GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.CouponAmount.ToString());
                if (GlobalDataAccessor.Instance.DesktopSession.TenderReferenceNumber != null)
                    GlobalDataAccessor.Instance.DesktopSession.TenderReferenceNumber.Add(GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.CouponCode);
                if (GlobalDataAccessor.Instance.DesktopSession.TenderTypes != null)
                    GlobalDataAccessor.Instance.DesktopSession.TenderTypes.Add(opCode);
                int i = 1;
                totalCouponAmt += GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.CouponAmount;
                decimal couponAmtTotal = 0.0m;
                foreach (RetailItem retItem in GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.RetailItems)
                {
                    addltraninfoType.Add(retItem.SaleType.ToString());
                    addlMdseTranRetPrice.Add(retItem.NegotiatedPrice.ToString());
                    decimal price = (retItem.NegotiatedPrice * retItem.Quantity) - retItem.CouponAmount;
                    addlMdseTranIcn.Add(retItem.Icn);
                    if (i != GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.RetailItems.Count)
                    {
                        decimal proratedCouponAmt = Math.Round((price / (GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.Amount + GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.CouponAmount)) * GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.CouponAmount, 2);
                        proratedCouponAmt = Math.Round(proratedCouponAmt, 2);
                        retItem.ProratedCouponAmount = proratedCouponAmt;
                        addltranCouponCodes.Add(GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.CouponCode);
                        addltranCouponAmounts.Add(proratedCouponAmt.ToString());
                        couponAmtTotal += proratedCouponAmt;

                    }
                    else
                    {
                        addltranCouponCodes.Add(GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.CouponCode);
                        decimal proratedCouponAmt = GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.CouponAmount - couponAmtTotal;
                        //decimal proratedCouponAmt = Math.Round((price / (GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.Amount + GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.CouponAmount)) * GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.CouponAmount, 2);
                        addltranCouponAmounts.Add((proratedCouponAmt).ToString());
                        retItem.ProratedCouponAmount = proratedCouponAmt;
                    }
                    i++;
                }
            }
            if (addlMdseTranIcn.Count > 0)
            {

                for (var j = 0; j < addlMdseTranIcn.Count; j++)
                {
                    int index = -1;
                    for (var k = 0; k < addlMdseCouponIcn.Count; k++)
                    {
                        if (addlMdseCouponIcn[k] == addlMdseTranIcn[j])
                            index = k;
                    }

                    addlMdseIcn.Add(addlMdseTranIcn[j]);
                    addlMdseRetPrice.Add(addlMdseTranRetPrice[j]);
                    if (index >= 0)
                    {
                        couponCodes.Add(addlcouponCodes[index]);
                        couponAmounts.Add(addlcouponAmounts[index]);
                    }
                    else
                    {
                        couponCodes.Add("");
                        couponAmounts.Add("");
                    }
                    tranCouponCodes.Add(addltranCouponCodes[j]);
                    tranCouponAmounts.Add(addltranCouponAmounts[j]);
                    infoType.Add(addltraninfoType[j]);
                }
            }
            else
            {
                for (var k = 0; k < addlMdseCouponIcn.Count; k++)
                {
                    addlMdseIcn.Add(addlMdseCouponIcn[k]);
                    addlMdseRetPrice.Add(addlMdseItemRetPrice[k]);
                    couponCodes.Add(addlcouponCodes[k]);
                    couponAmounts.Add(addlcouponAmounts[k]);
                    tranCouponCodes.Add("");
                    tranCouponAmounts.Add("");
                    infoType.Add(addlcouponinfoType[k]);

                }
            }



            //Start transaction block
            GlobalDataAccessor.Instance.DesktopSession.beginTransactionBlock();

            if (icnToAdd.Count > 0)
            {
                foreach (string s in icnToAdd)
                {
                    Item pItem = (from pawnItem in GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.RetailItems
                                  where pawnItem.Icn == s
                                  select pawnItem).FirstOrDefault();
                    if (pItem != null)
                    {
                        QuickCheck pItemQInfo = pItem.QuickInformation;
                        Int64[] primaryMasks = getMasks(pItem);
                        ProKnowMatch pKMatch = pItem.SelectedProKnowMatch;
                        ProKnowData pKData;
                        ProCallData pCData;
                        if (pKMatch != null)
                        {
                            pKData = pKMatch.selectedPKData;
                            pCData = pKMatch.proCallData;
                        }
                        else
                        {
                            pKData.RetailAmount = 0.0M;
                            pKData.LoanAmount = 0.0M;
                            pCData.NewRetail = 0.0M;
                        }

                        //Insert MDSE record for this pawn item
                        //Calculate the cost amount of the item
                        //Requirement is that cost will be 65% of the amount entered as retail amount
                        decimal itemCost = COSTPERCENTAGEFROMRETAIL * pItem.ItemAmount;

                        bool curRetValue = ProcessTenderProcedures.ExecuteInsertMDSERecord(
                            pItem.mStore, pItem.mStore, pItem.mYear, pItem.mDocNumber,
                            "" + pItem.mDocType, 1, 0, pItem.CategoryCode,
                            "", itemCost,
                            0, pItemQInfo.Manufacturer,
                            pItemQInfo.Model, pItemQInfo.SerialNumber, pItemQInfo.Weight,
                            primaryMasks, pItem.TicketDescription, pItem.ItemAmount,
                            pItem.StorageFee, GlobalDataAccessor.Instance.DesktopSession.FullUserName,
                            ShopDateTime.Instance.ShopDate.FormatDate(), ShopDateTime.Instance.ShopDate.ToShortDateString() + " " + ShopDateTime.Instance.ShopTime.ToString(), "", "", pItem.mDocType, "", out errorCode, out errorMessage);
                        if (!curRetValue)
                        {
                            GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                            return false;
                        }
                    }
                }
            }

            //If there is a gun item and a background reference number was captured then the gun book needs to be 
            //updated with the reference number
            if (gunItems.Count > 0 && GlobalDataAccessor.Instance.DesktopSession.BackgroundCheckCompleted)
            {
                foreach (RetailItem rItem in gunItems)
                {
                    string sErrorCode;
                    string sErrorText;
                    ProcessTenderProcedures.ExecuteUpdateGunBookRecord(
                        rItem.mDocNumber.ToString(),
                        GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreNumber,
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "BCK",
                        GlobalDataAccessor.Instance.DesktopSession.FullUserName,
                        ShopDateTime.Instance.ShopDate.ToShortDateString(),
                        ShopDateTime.Instance.ShopTransactionTime,
                        GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.BackgroundCheckRefNumber,
                        "",
                        "",
                        "",
                        "",
                        "",
                        rItem.mStore,
                        rItem.mYear,
                        rItem.mDocNumber,
                        rItem.mDocType,
                        rItem.mItemOrder,
                        0,
                        rItem.GunNumber,
                        out sErrorCode,
                        out sErrorText);
                }
            }
            //check if one of the tender amount was coupon and if it reduced the tax component
            //Update the object's sales tax amount 
            if (GlobalDataAccessor.Instance.DesktopSession.TenderData != null)
            {
                foreach (TenderEntryVO tdata in GlobalDataAccessor.Instance.DesktopSession.TenderData)
                {

                    if (tdata.TenderType == TenderTypes.STORECREDIT)
                    {
                        shopCreditUsed = true;
                        shopCreditAmount = tdata.Amount;
                    }
                }
            }

            // EDW - CR#15166
            KeyValuePair<string, string> lastIdUsed = GlobalDataAccessor.Instance.DesktopSession.LastIdUsed;
            CustomerVO cust = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer;
            string custDispIdNum = "";
            string custDispIdType = "";
            string custDispIDCode = "";

            if (cust != null)
            {
                IdentificationVO id = cust.getIdByTypeandIssuer(lastIdUsed.Key, lastIdUsed.Value);
                if (id == null)
                {
                    id = cust.getFirstIdentity();
                }

                if (id != null)
                {
                    custDispIDCode = id.IdIssuerCode;
                    custDispIdNum = id.IdValue;
                    custDispIdType = id.IdType;
                }
            }

            //Perform inserts
            bool saleInsert = RetailProcedures.InsertSaleRecord(GlobalDataAccessor.Instance.OracleDA,
                                                                GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreNumber,
                                                                ShopDateTime.Instance.ShopDate.ToShortDateString(),
                                                                ShopDateTime.Instance.ShopTransactionTime.ToString(),
                                                                GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer != null && GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.CustomerNumber != string.Empty ? GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.CustomerNumber : GlobalDataAccessor.Instance.DesktopSession.ActiveVendor != null ? GlobalDataAccessor.Instance.DesktopSession.ActiveVendor.ID : "",
                                                                GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile.UserName,
                                                                icn,
                                                                qty,
                                                                icnRetailPrice,
                                                                GlobalDataAccessor.Instance.DesktopSession.ActiveVendor != null ? "V" : "",
                                                                "0",
                                                                "",
                                                                currentSale.SalesTaxAmount.ToString(),
                                                                GlobalDataAccessor.Instance.DesktopSession.CashDrawerName,
                                                                ProductType.SALE.ToString(),
                                                                (currentSale.Amount + totalCouponAmt).ToString(),
                                                                currentSale.ShippingHandlingCharges.ToString(),
                                                                jewelryCase,
                                                                custDispIdNum, custDispIdType, custDispIDCode,
                                                                out saleTktNumber,
                                                                out errorCode,
                                                                out errorMessage);
            if (!saleInsert)
            {
                GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                return false;
            }

            //Status the loan
            GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.LoanStatus = ProductStatus.ACT;
            //Sale Ticket Number
            GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.TicketNumber = Utilities.GetIntegerValue(saleTktNumber, 0);

            //Add receipt details
            ReceiptDetailsVO rdVo = new ReceiptDetailsVO();
            List<string> refDate = new List<string>();
            List<string> refNumber = new List<string>();
            List<string> refType = new List<string>();
            List<string> refEvent = new List<string>();
            List<string> refAmount = new List<string>();
            List<string> refStore = new List<string>();
            List<string> refTime = new List<string>();

            // ref date for sale is the date made
            rdVo.ReceiptDate = ShopDateTime.Instance.ShopDate;
            refDate.Add(ShopDateTime.Instance.ShopDate.FormatDate());

            //ref time for sale is the current time

            refTime.Add(ShopDateTime.Instance.ShopTransactionTime);

            // ref number for sale is the ticket number

            refNumber.Add(saleTktNumber.ToString());

            // ref type for sale is 3
            refType.Add("3");

            // ref event for new sale is "SALE"
            refEvent.Add(ReceiptEventTypes.SALE.ToString());

            // ref amount is the active retail sale amount
            refAmount.Add((GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.TotalSaleAmount + totalCouponAmt).ToString());

            // ref store for sale is the store the receipt was printed at
            refStore.Add(GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreNumber);

            //Update teller and insert receipt details info and inform user to disburse cash
            int rcptNumber;
            List<string> saleTenderTypes = GlobalDataAccessor.Instance.DesktopSession.TenderTypes;
            List<string> saleTenderAmounts = GlobalDataAccessor.Instance.DesktopSession.TenderAmounts;
            List<string> saleTenderAuth = GlobalDataAccessor.Instance.DesktopSession.TenderReferenceNumber;

            List<string> topsPaymentType = saleTenderTypes.Select(s => "T").ToList();
            List<string> topsDocType = saleTenderTypes.Select(s => "3").ToList();

            string errorText;
            if (saleTenderTypes == null || saleTenderAmounts == null || saleTenderAmounts.Count == 0 || saleTenderTypes.Count == 0 || saleTenderAuth.Count == 0)
            {
                MessageBox.Show("Tender amounts are not set correctly. Cannot complete transaction");
                GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                return (false);

            }
            if (!(RetailProcedures.InsertRetailPaymentDetails(GlobalDataAccessor.Instance.OracleDA,
                                                              GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreNumber,
                                                              GlobalDataAccessor.Instance.DesktopSession.CashDrawerName,
                                                              GlobalDataAccessor.Instance.DesktopSession.FullUserName,
                                                              ShopDateTime.Instance.ShopDate.ToShortDateString(),
                                                              refNumber, refDate, refTime, refEvent, refAmount,
                                                              ShopDateTime.Instance.ShopTransactionTime.ToString(),
                                                              saleTenderTypes,
                                                              saleTenderAmounts,
                                                              saleTenderAuth,
                                                              topsDocType,
                                                              topsPaymentType,
                                                              out rcptNumber, out errorCode, out errorText)))
            {
                GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                BasicExceptionHandler.Instance.AddException("Failed to update teller", new ApplicationException());
                return (false);
            }

            //Add fees
            //Insert applicable fees if necessary
            const string feeRefType = "SALE";

            List<string> newFeeTypes = new List<string>();
            List<string> newFeeAmount = new List<string>();
            List<string> newFeeOrigAmount = new List<string>();
            List<string> newFeeisProrated = new List<string>();
            List<string> newFeeDates = new List<string>();
            List<string> newFeeStateCodes = new List<string>();

            //Go through the list of fees in the pawn loan object and insert into fee table
            string feeOpRevCode = FeeRevOpCodes.SALE.ToString();
            if (currentSale.Fees.Count > 0)
            {
                var origFees = from fees in currentSale.Fees
                               where fees.OriginalAmount != 0
                               select fees;
                foreach (Fee custloanfee in origFees)
                {
                    newFeeTypes.Add(custloanfee.FeeType.ToString());
                    newFeeOrigAmount.Add(custloanfee.OriginalAmount.ToString());
                    newFeeAmount.Add(custloanfee.Value.ToString());
                    if (custloanfee.Prorated)
                        newFeeisProrated.Add("1");
                    else
                        newFeeisProrated.Add("0");
                    newFeeDates.Add(trandate);
                    if (custloanfee.Waived)
                        newFeeStateCodes.Add(FeeStates.WAIVED.ToString());
                    else if (custloanfee.FeeState == FeeStates.VOID)
                        newFeeStateCodes.Add(FeeStates.VOID.ToString());
                    else
                        newFeeStateCodes.Add(FeeStates.PAID.ToString());
                }
            }

            if (newFeeTypes.Count > 0)
            {
                if (!ProcessTenderProcedures.ExecuteInsertUpdatePawnFee(newFeeTypes,
                                                                        feeRefType, saleTktNumber, GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreNumber,
                                                                        newFeeAmount, newFeeOrigAmount, newFeeisProrated, newFeeDates, newFeeStateCodes, GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile.UserName,
                                                                        feeOpRevCode, Utilities.GetLongValue(rcptNumber), out errorCode, out errorText))
                {
                    GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(
                        EndTransactionType.ROLLBACK);
                    if (FileLogger.Instance.IsLogError)
                        FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Could not update or insert fee data");
                    BasicExceptionHandler.Instance.AddException(
                        "Could not complete sale - update fees failed :" + errorText + ": " + errorText,
                        new ApplicationException("Could not complete sale"));
                    return (false);
                }
            }

            //insert store credit data if it was used
            if (shopCreditUsed && GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer != null)
            {
                if (!RetailProcedures.MaintainStoreCredit(GlobalDataAccessor.Instance.OracleDA,
                                                          GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.CustomerNumber,
                                                          GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreNumber,
                                                          shopCreditAmount, saleTktNumber, "PBOS", saleTktNumber, "R",
                                                          GlobalDataAccessor.Instance.DesktopSession.FullUserName,
                                                          ShopDateTime.Instance.ShopDate.FormatDate(),
                                                          trandate,
                                                          out errorCode,
                                                          out errorText))
                {
                    GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                    BasicExceptionHandler.Instance.AddException("Failed to update shop credit", new ApplicationException());
                    return (false);
                }
            }

            //Insert additional mdse info if needed
            if (addlMdseIcn.Count > 0)
            {
                if (!(MerchandiseProcedures.InsertAddlMdseInfo(addlMdseIcn, GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreNumber,
                    couponCodes, couponAmounts, tranCouponCodes, tranCouponAmounts, infoType, addlMdseRetPrice, "SOLD", saleTktNumber,
                    GlobalDataAccessor.Instance.DesktopSession.FullUserName, out errorCode, out errorText)))
                {
                    GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                    BasicExceptionHandler.Instance.AddException("Failed to insert additional merchandise information", new ApplicationException());
                    return (false);
                }
            }
            else
            {
                var craigslistItems = from rItem in GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.RetailItems
                                      where rItem.SaleType == SaleType.CraigsList
                                      select rItem;
                if (craigslistItems.Count() > 0)
                {
                    foreach (RetailItem retItem in craigslistItems)
                    {
                        infoType.Add(retItem.SaleType.ToString());
                        addlMdseIcn.Add(retItem.Icn);
                        couponCodes.Add("");
                        couponAmounts.Add("");
                        tranCouponAmounts.Add("");
                        tranCouponCodes.Add("");
                    }
                    if (addlMdseIcn.Count > 0)
                    {
                        if (!(MerchandiseProcedures.InsertAddlMdseInfo(addlMdseIcn, GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreNumber,
                            couponCodes, couponAmounts, tranCouponCodes, tranCouponAmounts, infoType, addlMdseRetPrice, "SOLD", saleTktNumber,
                            GlobalDataAccessor.Instance.DesktopSession.FullUserName, out errorCode, out errorText)))
                        {
                            FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Craigslist data could not be added to additional mdse info table" + errorText);
                        }

                    }
                }
            }

            //Commit teller transaction
            GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.COMMIT);

            //Print receipt
            rdVo.RefNumbers = refNumber;
            rdVo.ReceiptNumber = rcptNumber.ToString();
            rdVo.RefEvents = refEvent;
            rdVo.RefStores = refStore;
            rdVo.RefTypes = refType;
            if (SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled)
            {
                bool rt = false;
                try
                {
                    rt = this.GenerateSaleReceipt(
                        ProcessTenderMode.RETAILSALE,
                        GlobalDataAccessor.Instance.DesktopSession.UserName.ToLowerInvariant(),
                        GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer,
                        GlobalDataAccessor.Instance.DesktopSession.ActiveRetail,
                        GlobalDataAccessor.Instance.DesktopSession.ActiveVendor != null ? GlobalDataAccessor.Instance.DesktopSession.ActiveVendor.Name : "",
                        rdVo);
                }
                catch (Exception ex)
                {
                    if (FileLogger.Instance.IsLogError)
                        FileLogger.Instance.logMessage(LogLevel.ERROR, this, ex.Message);

                    rt = false;
                }

                if (!rt)
                {
                    if (FileLogger.Instance.IsLogError)
                        FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Could not print receipt for sale");
                    BasicExceptionHandler.Instance.AddException("Could not print receipt for sale", new ApplicationException("Could not print receipt while doing a retail sale"));
                    return (false);
                }
            }
            ResetDeskTopSessionVariables();
            return (true);
        }

        private bool executeRefundSale()
        {
            this.resetData();

            string trandate = ShopDateTime.Instance.ShopDate.ToShortDateString() + " " +
                              ShopDateTime.Instance.ShopTime.ToString();

            SaleVO currentSale = GlobalDataAccessor.Instance.DesktopSession.ActiveRetail;
            if (currentSale == null)
                return false;

            List<string> icn = GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.RetailItems.Select(item => item.Icn.ToString()).ToList();
            List<string> icnRetailPrice = GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.RetailItems.Select(item => item.RetailPrice.ToString()).ToList();
            List<int> qty = GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.RetailItems.Select(item => item.Quantity).ToList();
            List<string> jewelryCase = GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.RetailItems.Select(item => item.JeweleryCaseNumber).ToList();

            for (var i = 0; i < jewelryCase.Count; i++)
            {
                if (jewelryCase[i] == null)
                    jewelryCase[i] = "";
            }

            string errorCode;
            string errorMessage;
            int saleTktNumber;
            //Get coupon amounts
            decimal totalCouponAmount = GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.RetailItems.Where(rItem => rItem.CouponAmount > 0 || rItem.ProratedCouponAmount > 0).Sum(rItem => rItem.CouponAmount + rItem.ProratedCouponAmount);
            if (totalCouponAmount > 0)
            {
                string opCode = Commons.GetOutOpCode(TenderTypes.COUPON.ToString(), CreditCardTypes.VISA.ToString());
                if (GlobalDataAccessor.Instance.DesktopSession.TenderAmounts != null)
                    GlobalDataAccessor.Instance.DesktopSession.TenderAmounts.Add(totalCouponAmount.ToString());
                if (GlobalDataAccessor.Instance.DesktopSession.TenderReferenceNumber != null)
                    GlobalDataAccessor.Instance.DesktopSession.TenderReferenceNumber.Add("");
                if (GlobalDataAccessor.Instance.DesktopSession.TenderTypes != null)
                    GlobalDataAccessor.Instance.DesktopSession.TenderTypes.Add(opCode);
            }

            // EDW - CR#15166
            KeyValuePair<string, string> lastIdUsed = GlobalDataAccessor.Instance.DesktopSession.LastIdUsed;
            CustomerVO cust = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer;
            string custDispIdNum = "";
            string custDispIdType = "";
            string custDispIDCode = "";

            if (cust != null)
            {
                IdentificationVO id = cust.getIdByTypeandIssuer(lastIdUsed.Key, lastIdUsed.Value);
                if (id == null)
                {
                    id = cust.getFirstIdentity();
                }

                if (id != null)
                {
                    custDispIDCode = id.IdIssuerCode;
                    custDispIdNum = id.IdValue;
                    custDispIdType = id.IdType;
                }
            }

            decimal refundSaleHeaderAmt = currentSale.RefNumber == 0 ? currentSale.Amount + totalCouponAmount : currentSale.Amount;

            //Start transaction block
            GlobalDataAccessor.Instance.DesktopSession.beginTransactionBlock();

            //Perform inserts
            bool refInsert = RetailProcedures.InsertSaleRecord(GlobalDataAccessor.Instance.OracleDA,
                                                               GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreNumber,
                                                               ShopDateTime.Instance.ShopDate.ToShortDateString(),
                                                               ShopDateTime.Instance.ShopTransactionTime.ToString(),
                                                               currentSale.CustomerNumber != string.Empty ? (currentSale.CustomerNumber == "0" ? GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.CustomerNumber : currentSale.CustomerNumber) : currentSale.EntityNumber,
                                                               GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile.UserName,
                                                               icn,
                                                               qty,
                                                               icnRetailPrice,
                                                               GlobalDataAccessor.Instance.DesktopSession.ActiveVendor != null ? "V" : "",
                                                               GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.TicketNumber.ToString(),
                                                               "",
                                                               currentSale.SalesTaxAmount.ToString(),
                                                               GlobalDataAccessor.Instance.DesktopSession.CashDrawerName,
                                                               "REFUND",
                                                               refundSaleHeaderAmt.ToString(),
                                                               currentSale.ShippingHandlingCharges.ToString(),
                                                               jewelryCase,
                                                               custDispIdNum, custDispIdType, custDispIDCode,
                                                               out saleTktNumber,
                                                               out errorCode,
                                                               out errorMessage);
            if (!refInsert)
            {
                GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                return false;
            }

            //Status the loan
            GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.LoanStatus = ProductStatus.REF;

            //Change the active ticket number
            GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.RefNumber = GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.TicketNumber;
            GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.TicketNumber = saleTktNumber;

            //Add receipt details
            ReceiptDetailsVO rdVo = new ReceiptDetailsVO();
            List<string> refDate = new List<string>();
            List<string> refNumber = new List<string>();
            List<string> refType = new List<string>();
            List<string> refEvent = new List<string>();
            List<string> refAmount = new List<string>();
            List<string> refStore = new List<string>();
            List<string> refTime = new List<string>();

            List<string> saleTenderTypes = GlobalDataAccessor.Instance.DesktopSession.TenderTypes ?? new List<string>();
            List<string> saleTenderAmounts = GlobalDataAccessor.Instance.DesktopSession.TenderAmounts ?? new List<string>();
            List<string> saleTenderAuth = GlobalDataAccessor.Instance.DesktopSession.TenderReferenceNumber ?? new List<string>();

            decimal totalRefundAmount = saleTenderAmounts.Sum(s => Utilities.GetDecimalValue(s, 0));

            // ref date for sale is the date made
            rdVo.ReceiptDate = ShopDateTime.Instance.ShopDate;
            refDate.Add(ShopDateTime.Instance.ShopDate.FormatDate());

            //ref time for sale is the current time

            refTime.Add(ShopDateTime.Instance.ShopTransactionTime);

            // ref number for sale is the ticket number

            refNumber.Add(saleTktNumber.ToString());

            // ref type for sale is 3
            refType.Add("3");

            // ref event for new sale is "SALE"
            refEvent.Add(ReceiptEventTypes.REF.ToString());

            // ref amount is the total refund amount
            refAmount.Add(totalRefundAmount.ToString());

            // ref store for sale is the store the receipt was printed at
            refStore.Add(GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreNumber);

            //Update teller and insert receipt details info and inform user to disburse cash
            int rcptNumber;

            List<string> topsPaymentType = saleTenderTypes.Select(s => "T").ToList();
            List<string> topsDocType = saleTenderTypes.Select(s => "3").ToList();

            string errorText;

            if (!(RetailProcedures.InsertRetailPaymentDetails(GlobalDataAccessor.Instance.OracleDA,
                                                              GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreNumber,
                                                              GlobalDataAccessor.Instance.DesktopSession.CashDrawerName,
                                                              GlobalDataAccessor.Instance.DesktopSession.FullUserName,
                                                              ShopDateTime.Instance.ShopDate.ToShortDateString(),
                                                              refNumber, refDate, refTime, refEvent, refAmount,
                                                              ShopDateTime.Instance.ShopTransactionTime.ToString(),
                                                              saleTenderTypes,
                                                              saleTenderAmounts,
                                                              saleTenderAuth,
                                                              topsDocType,
                                                              topsPaymentType,
                                                              out rcptNumber, out errorCode, out errorText)))
            {
                GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                BasicExceptionHandler.Instance.AddException("Failed to update teller", new ApplicationException());
                return (false);
            }
            rdVo.ReceiptNumber = rcptNumber.ToString();
            //Add fees
            //Insert applicable fees if necessary
            const string feeRefType = "SALE";

            List<string> newFeeTypes = new List<string>();
            List<string> newFeeAmount = new List<string>();
            List<string> newFeeOrigAmount = new List<string>();
            List<string> newFeeisProrated = new List<string>();
            List<string> newFeeDates = new List<string>();
            List<string> newFeeStateCodes = new List<string>();

            //Go through the list of fees in the pawn loan object and insert into fee table
            string feeOpRevCode = FeeRevOpCodes.SALE.ToString();
            if (currentSale.Fees.Count > 0)
            {
                var origFees = from fees in currentSale.Fees
                               where fees.OriginalAmount != 0
                               select fees;
                foreach (Fee custloanfee in origFees)
                {
                    newFeeTypes.Add(custloanfee.FeeType.ToString());
                    newFeeOrigAmount.Add(custloanfee.OriginalAmount.ToString());
                    newFeeAmount.Add(custloanfee.Value.ToString());
                    newFeeisProrated.Add("0");
                    newFeeDates.Add(trandate);
                    newFeeStateCodes.Add(FeeStates.VOID.ToString());
                }
            }

            if (newFeeTypes.Count > 0)
            {
                if (!ProcessTenderProcedures.ExecuteInsertUpdatePawnFee(newFeeTypes,
                                                                        feeRefType, GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.TicketNumber, currentSale.StoreNumber,
                                                                        newFeeAmount, newFeeOrigAmount, newFeeisProrated, newFeeDates, newFeeStateCodes, GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile.UserName,
                                                                        feeOpRevCode, Utilities.GetLongValue(rcptNumber), out errorCode, out errorText))
                {
                    GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(
                        EndTransactionType.ROLLBACK);
                    if (FileLogger.Instance.IsLogError)
                        FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Could not update or insert fee data");
                    BasicExceptionHandler.Instance.AddException(
                        "Could not complete refund - update fees failed :" + errorText + ": " + errorText,
                        new ApplicationException("Could not complete sale"));
                    return (false);
                }
            }

            //Commit teller transaction
            GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.COMMIT);

            //TODO printing of receipt
            if (SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled)
            {
                bool rt = false;
                try
                {
                    rt = this.GenerateSaleReceipt(
                        ProcessTenderMode.RETAILREFUND,
                        GlobalDataAccessor.Instance.DesktopSession.UserName.ToLowerInvariant(),
                        GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer,
                        currentSale,
                        "",
                        rdVo);
                }
                catch (Exception)
                {
                    rt = false;
                }

                if (!rt)
                {
                    if (FileLogger.Instance.IsLogError)
                        FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Could not print receipt for Retail Sale refund");
                    BasicExceptionHandler.Instance.AddException("Could not print receipt for Retail Sale", new ApplicationException("Could not print receipt for Retail Sale refund"));
                    return (false);
                }
            }
            if (!PrintRefundTags())
            {
                if (FileLogger.Instance.IsLogError)
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Could not print receipt tags for Retail Sale refund");

            }
            ResetDeskTopSessionVariables();
            return (true);
        }

        private RetailItem FindItem(List<string> searchFor, List<string> searchValues)
        {
            string errorText = null;
            string errorCode = null;
            List<RetailItem> searchItems;
            string searchFlag = "NORMAL";
            RetailItem item = null;
            RetailProcedures.SearchForItem(searchFor, searchValues, GlobalDataAccessor.Instance.DesktopSession, searchFlag, false, out searchItems, out errorCode, out errorText);

            if (searchItems.Count == 1)
            {
                item = searchItems[0];
            }
            return item;
        }

        private bool PrintRefundTags()
        {
            //first refresh retailitems since some info may have changed
            List<string> searchFor;
            List<string> searchValues;
            try
            {
                foreach (RetailItem rItem in GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.RetailItems)
                {
                    RetailItem retItem;
                    searchFor = new List<string>()
                        {
                                ""
                        };
                    searchValues = new List<string>()
                        {
                                rItem.Icn
                        };


                    retItem = FindItem(searchFor, searchValues);
                    if (rItem.IsGun)
                    {
                        rItem.GunNumber = retItem.GunNumber;
                    }
                    string docType = retItem.mDocType;
                    //rItem.RetailPrice = retItem.PreviousRetailPrice; // need to reprint the labels with the original retail price
                    retItem.PfiTags = 1;
                    if (docType != "3" && docType != "8" && docType != "5")
                    {
                        GlobalDataAccessor.Instance.DesktopSession.PrintTags(retItem, CurrentContext.READ_ONLY);

                    }
                }
            }
            catch (Exception ex)
            {
                if (FileLogger.Instance.IsLogError)
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Error in tag printing " + ex.Message);

                return false;
            }
            return true;
        }

        private bool executeLayaway()
        {
            this.resetData();
            bool shopCreditUsed = false;
            decimal shopCreditAmount = 0.0M;
            string trandate = ShopDateTime.Instance.ShopDate.FormatDate() + " " +
                              ShopDateTime.Instance.ShopTime.ToString();

            LayawayVO currentLayaway = GlobalDataAccessor.Instance.DesktopSession.ActiveLayaway;
            if (currentLayaway == null)
                return false;
            List<string> icn = GlobalDataAccessor.Instance.DesktopSession.ActiveLayaway.RetailItems.Select(item => item.Icn.ToString()).ToList();
            List<string> icnRetailPrice = GlobalDataAccessor.Instance.DesktopSession.ActiveLayaway.RetailItems.Select(item => item.NegotiatedPrice.ToString()).ToList();
            List<string> icnToAdd = icn.Where(s => s.Substring(12, 1) == "8").ToList();
            List<int> qty = GlobalDataAccessor.Instance.DesktopSession.ActiveLayaway.RetailItems.Select(item => item.Quantity).ToList();
            List<string> jewelryCase = GlobalDataAccessor.Instance.DesktopSession.ActiveLayaway.RetailItems.Select(item => item.JeweleryCaseNumber).ToList();
            for (var i = 0; i < jewelryCase.Count; i++)
            {
                if (jewelryCase[i] == null)
                    jewelryCase[i] = "";
            }

            string errorCode;
            string errorMessage;
            int layawayTktNumber;
            List<string> addlMdseIcn = new List<string>();
            List<string> addlMdseCouponIcn = new List<string>();
            List<string> addlMdseTranIcn = new List<string>();
            List<string> addlcouponCodes = new List<string>();
            List<string> addlcouponAmounts = new List<string>();
            List<string> addltranCouponCodes = new List<string>();
            List<string> addltranCouponAmounts = new List<string>();
            List<string> couponCodes = new List<string>();
            List<string> couponAmounts = new List<string>();
            List<string> tranCouponCodes = new List<string>();
            List<string> tranCouponAmounts = new List<string>();
            List<string> infoType = new List<string>();
            List<string> addlcouponinfoType = new List<string>();
            List<string> addltraninfoType = new List<string>();
            List<string> addlMdseRetPrice = new List<string>();
            List<string> addlMdseItemRetPrice = new List<string>();
            List<string> addlMdseTranRetPrice = new List<string>();

            decimal totalCouponAmt = 0.0m;
            int indx = 0;
            //Check if coupon was used for any item or for the transaction
            foreach (RetailItem rItem in GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.RetailItems)
            {

                if (rItem.CouponAmount > 0)
                {
                    addlMdseItemRetPrice.Add(rItem.NegotiatedPrice.ToString());
                    addlcouponinfoType.Add(rItem.SaleType.ToString());
                    string opCode = Commons.GetInOpCode(TenderTypes.COUPON.ToString(), TenderTypes.CREDITCARD.ToString());
                    /*if (GlobalDataAccessor.Instance.DesktopSession.TenderAmounts != null)
                        GlobalDataAccessor.Instance.DesktopSession.TenderAmounts.Add(rItem.CouponAmount.ToString());
                    if (GlobalDataAccessor.Instance.DesktopSession.TenderReferenceNumber != null)
                        GlobalDataAccessor.Instance.DesktopSession.TenderReferenceNumber.Add(rItem.CouponCode);
                    if (GlobalDataAccessor.Instance.DesktopSession.TenderTypes != null)
                        GlobalDataAccessor.Instance.DesktopSession.TenderTypes.Add(opCode);*/
                    addlMdseCouponIcn.Add(rItem.Icn);
                    addlcouponCodes.Add(rItem.CouponCode);
                    addlcouponAmounts.Add(rItem.CouponAmount.ToString());
                    totalCouponAmt += rItem.CouponAmount;
                }
                indx++;
            }
            if (GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.CouponAmount > 0)
            {

                string opCode = Commons.GetInOpCode(TenderTypes.COUPON.ToString(), TenderTypes.CREDITCARD.ToString());
                /*if (GlobalDataAccessor.Instance.DesktopSession.TenderAmounts != null)
                    GlobalDataAccessor.Instance.DesktopSession.TenderAmounts.Add(GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.CouponAmount.ToString());
                if (GlobalDataAccessor.Instance.DesktopSession.TenderReferenceNumber != null)
                    GlobalDataAccessor.Instance.DesktopSession.TenderReferenceNumber.Add(GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.CouponCode);
                if (GlobalDataAccessor.Instance.DesktopSession.TenderTypes != null)
                    GlobalDataAccessor.Instance.DesktopSession.TenderTypes.Add(opCode);*/
                int i = 1;
                totalCouponAmt += GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.CouponAmount;
                decimal couponAmtTotal = 0.0m;
                foreach (RetailItem retItem in GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.RetailItems)
                {
                    addltraninfoType.Add(retItem.SaleType.ToString());
                    addlMdseTranRetPrice.Add(retItem.NegotiatedPrice.ToString());
                    decimal price = retItem.NegotiatedPrice - retItem.CouponAmount;
                    addlMdseTranIcn.Add(retItem.Icn);
                    decimal proratedCouponAmt = 0.0m;
                    if (i != GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.RetailItems.Count)
                    {
                        //proratedCouponAmt = Math.Round((price / GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.Amount) * GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.CouponAmount, 2);
                        proratedCouponAmt = Math.Round((GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.CouponPercentage / 100) * retItem.TotalPrice, 2);
                        retItem.ProratedCouponAmount = proratedCouponAmt;
                        addltranCouponCodes.Add(GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.CouponCode);
                        addltranCouponAmounts.Add(proratedCouponAmt.ToString());
                        couponAmtTotal += proratedCouponAmt;
                    }
                    else
                    {
                        addltranCouponCodes.Add(GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.CouponCode);
                        proratedCouponAmt = GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.CouponAmount - couponAmtTotal;
                        //proratedCouponAmt = Math.Round((GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.CouponPercentage / 100) * retItem.TotalPrice, 2);
                        addltranCouponAmounts.Add(proratedCouponAmt.ToString());
                        retItem.ProratedCouponAmount = proratedCouponAmt;
                    }
                    retItem.ProratedCouponAmount = proratedCouponAmt;
                    retItem.TranCouponCode = GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.CouponCode;
                    i++;
                }
            }
            if (addlMdseTranIcn.Count > 0)
            {

                for (var j = 0; j < addlMdseTranIcn.Count; j++)
                {
                    int index = -1;
                    for (var k = 0; k < addlMdseCouponIcn.Count; k++)
                    {
                        if (addlMdseCouponIcn[k] == addlMdseTranIcn[j])
                            index = k;
                    }

                    addlMdseIcn.Add(addlMdseTranIcn[j]);
                    addlMdseRetPrice.Add(addlMdseTranRetPrice[j]);

                    if (index >= 0)
                    {
                        couponCodes.Add(addlcouponCodes[index]);
                        couponAmounts.Add(addlcouponAmounts[index]);
                    }
                    else
                    {
                        couponCodes.Add("");
                        couponAmounts.Add("");
                    }
                    tranCouponCodes.Add(addltranCouponCodes[j]);
                    tranCouponAmounts.Add(addltranCouponAmounts[j]);
                    infoType.Add(addltraninfoType[j]);
                }
            }
            else
            {
                for (var k = 0; k < addlMdseCouponIcn.Count; k++)
                {
                    addlMdseIcn.Add(addlMdseCouponIcn[k]);
                    addlMdseRetPrice.Add(addlMdseItemRetPrice[k]);
                    couponCodes.Add(addlcouponCodes[k]);
                    couponAmounts.Add(addlcouponAmounts[k]);
                    tranCouponCodes.Add("");
                    tranCouponAmounts.Add("");
                    infoType.Add(addlcouponinfoType[k]);

                }
            }

            //Start transaction block
            GlobalDataAccessor.Instance.DesktopSession.beginTransactionBlock();

            if (icnToAdd.Count > 0)
            {
                foreach (string s in icnToAdd)
                {
                    Item pItem = (from pawnItem in GlobalDataAccessor.Instance.DesktopSession.ActiveLayaway.RetailItems
                                  where pawnItem.Icn == s
                                  select pawnItem).FirstOrDefault();
                    if (pItem != null)
                    {
                        QuickCheck pItemQInfo = pItem.QuickInformation;
                        Int64[] primaryMasks = getMasks(pItem);
                        ProKnowMatch pKMatch = pItem.SelectedProKnowMatch;
                        ProKnowData pKData;
                        ProCallData pCData;
                        if (pKMatch != null)
                        {
                            pKData = pKMatch.selectedPKData;
                            pCData = pKMatch.proCallData;
                        }
                        else
                        {
                            pKData.RetailAmount = 0.0M;
                            pKData.LoanAmount = 0.0M;
                            pCData.NewRetail = 0.0M;
                        }

                        //Insert MDSE record for this pawn item
                        //Calculate the cost amount of the item
                        //Requirement is that cost will be 65% of the amount entered as retail amount
                        decimal itemCost = COSTPERCENTAGEFROMRETAIL * pItem.ItemAmount;

                        bool curRetValue = ProcessTenderProcedures.ExecuteInsertMDSERecord(
                            pItem.mStore, pItem.mStore, pItem.mYear, pItem.mDocNumber,
                            "" + pItem.mDocType, 1, 0, pItem.CategoryCode,
                            "", itemCost,
                            0, pItemQInfo.Manufacturer,
                            pItemQInfo.Model, pItemQInfo.SerialNumber, pItemQInfo.Weight,
                            primaryMasks, pItem.TicketDescription, pItem.ItemAmount,
                            pItem.StorageFee, GlobalDataAccessor.Instance.DesktopSession.FullUserName,
                            ShopDateTime.Instance.ShopDate.FormatDate(), ShopDateTime.Instance.ShopDate.ToShortDateString() + " " + ShopDateTime.Instance.ShopTime.ToString(), "", "", pItem.mDocType, "", out errorCode, out errorMessage);
                        if (!curRetValue)
                        {
                            GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                            return false;
                        }
                    }
                }
            }
            decimal couponTenderAmount = 0;
            //check if one of the tender amount was coupon and if it reduced the tax component
            //Update the object's sales tax amount 
            foreach (TenderEntryVO tdata in GlobalDataAccessor.Instance.DesktopSession.TenderData)
            {
                if (tdata.TenderType == TenderTypes.COUPON)
                {
                    decimal taxAmt = Math.Round(tdata.Amount * (GlobalDataAccessor.Instance.DesktopSession.TenderTransactionAmount.SalesTaxPercentage / 100), 2);
                    currentLayaway.SalesTaxAmount = currentLayaway.SalesTaxAmount - taxAmt;
                }
                if (tdata.TenderType == TenderTypes.STORECREDIT)
                {
                    shopCreditUsed = true;
                    shopCreditAmount = tdata.Amount;
                }
            }

            //Perform inserts
            bool layawayInsert = RetailProcedures.InsertLayawayRecord(GlobalDataAccessor.Instance.OracleDA,
                                                                      GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreNumber,
                                                                      ShopDateTime.Instance.ShopDate.FormatDate(),
                                                                      ShopDateTime.Instance.ShopTransactionTime.ToString(),
                                                                      GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer != null && GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.CustomerNumber != string.Empty ? GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.CustomerNumber : GlobalDataAccessor.Instance.DesktopSession.ActiveVendor != null ? GlobalDataAccessor.Instance.DesktopSession.ActiveVendor.ID : "",
                                                                      currentLayaway.Amount.ToString(),
                                                                      currentLayaway.NumberOfPayments,
                                                                      currentLayaway.DownPayment,
                                                                      currentLayaway.MonthlyPayment,
                                                                      currentLayaway.FirstPayment.FormatDate().ToString(),
                                                                      currentLayaway.NextPayment.FormatDate().ToString(),
                                                                      currentLayaway.LastPayment.FormatDate().ToString(),
                                                                      GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile.UserName,
                                                                      icn,
                                                                      qty,
                                                                      icnRetailPrice,
                                                                      GlobalDataAccessor.Instance.DesktopSession.ActiveVendor != null ? "V" : "",
                                                                      "",
                                                                      "",
                                                                      currentLayaway.SalesTaxAmount.ToString(),
                                                                      GlobalDataAccessor.Instance.DesktopSession.CashDrawerName,
                                                                      ProductType.LAYAWAY.ToString(),
                                                                      jewelryCase,
                                                                      out layawayTktNumber,
                                                                      out errorCode,
                                                                      out errorMessage);
            if (!layawayInsert)
            {
                GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                return false;
            }

            //Status the loan
            GlobalDataAccessor.Instance.DesktopSession.ActiveLayaway.LoanStatus = ProductStatus.LAY;
            //Layaway Ticket Number
            GlobalDataAccessor.Instance.DesktopSession.ActiveLayaway.TicketNumber = Utilities.GetIntegerValue(layawayTktNumber, 0);

            //Add fees
            //Insert applicable fees if necessary
            const string feeRefType = "LAY";

            List<string> newFeeTypes = new List<string>();
            List<string> newFeeAmount = new List<string>();
            List<string> newFeeOrigAmount = new List<string>();
            List<string> newFeeisProrated = new List<string>();
            List<string> newFeeDates = new List<string>();
            List<string> newFeeStateCodes = new List<string>();

            //Go through the list of fees in the pawn loan object and insert into fee table
            string feeOpRevCode = FeeRevOpCodes.LAY.ToString();
            decimal totalFeeAmount = 0;
            if (currentLayaway.Fees.Count > 0)
            {
                var origFees = from fees in currentLayaway.Fees
                               where fees.OriginalAmount != 0
                               select fees;
                foreach (Fee custloanfee in origFees)
                {
                    newFeeTypes.Add(custloanfee.FeeType.ToString());
                    newFeeOrigAmount.Add(custloanfee.OriginalAmount.ToString());
                    newFeeAmount.Add(custloanfee.Value.ToString());
                    if (custloanfee.Prorated)
                        newFeeisProrated.Add("1");
                    else
                        newFeeisProrated.Add("0");
                    newFeeDates.Add(trandate);
                    if (custloanfee.Waived)
                        newFeeStateCodes.Add(FeeStates.WAIVED.ToString());
                    else if (custloanfee.FeeState == FeeStates.VOID)
                        newFeeStateCodes.Add(FeeStates.VOID.ToString());
                    else
                        newFeeStateCodes.Add(FeeStates.PAID.ToString());
                    totalFeeAmount += custloanfee.OriginalAmount;
                }
            }

            //Add receipt details
            ReceiptDetailsVO rdVo = new ReceiptDetailsVO();
            List<string> refDate = new List<string>();
            List<string> refNumber = new List<string>();
            List<string> refType = new List<string>();
            List<string> refEvent = new List<string>();
            List<string> refAmount = new List<string>();
            List<string> refStore = new List<string>();
            List<string> refTime = new List<string>();

            //Update teller and insert receipt details info and inform user to disburse cash
            int rcptNumber;
            List<string> saleTenderTypes = GlobalDataAccessor.Instance.DesktopSession.TenderTypes;
            List<string> saleTenderAmounts = GlobalDataAccessor.Instance.DesktopSession.TenderAmounts;
            List<string> saleTenderAuth = GlobalDataAccessor.Instance.DesktopSession.TenderReferenceNumber;

            List<string> topsPaymentType = saleTenderTypes.Select(s => "D").ToList();
            List<string> topsDocType = saleTenderTypes.Select(s => "4").ToList();

            var idx = saleTenderTypes.FindIndex(s => s == "CDIN");
            if (idx >= 0)
                couponTenderAmount = Utilities.GetDecimalValue(saleTenderAmounts[idx], 0);
            var nextIndx = saleTenderTypes.FindLastIndex(s => s == "CDIN");
            if (nextIndx >= 0 && nextIndx != idx)
                couponTenderAmount += Utilities.GetDecimalValue(saleTenderAmounts[nextIndx], 0);


            //2 receipts are being added. one for the down payment and one for the service fee

            // ref date for sale is the date made
            rdVo.ReceiptDate = ShopDateTime.Instance.ShopDate;
            refDate.Add(ShopDateTime.Instance.ShopDate.FormatDate());

            //ref time for sale is the current time

            refTime.Add(ShopDateTime.Instance.ShopTransactionTime);

            // ref number for sale is the ticket number

            refNumber.Add(layawayTktNumber.ToString());

            // ref type for layaway is 4
            refType.Add("4");

            // ref event for layaway down payment
            refEvent.Add(ReceiptEventTypes.LAY.ToString());

            // ref amount is the active layaway amount
            //refAmount.Add((currentLayaway.DownPayment + couponTenderAmount).ToString());
            refAmount.Add((currentLayaway.DownPayment).ToString());

            // ref store for sale is the store the receipt was printed at
            refStore.Add(GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreNumber);

            //Add one more receipt data for layaway service fee
            refDate.Add(ShopDateTime.Instance.ShopDate.FormatDate());

            //ref time for sale is the current time

            refTime.Add(ShopDateTime.Instance.ShopTransactionTime);

            // ref number for sale is the ticket number

            refNumber.Add(layawayTktNumber.ToString());

            // ref type for layaway is 4
            refType.Add("4");

            // ref event for layaway service fee
            refEvent.Add(ReceiptEventTypes.LAYSF.ToString());

            // ref amount is service fee
            refAmount.Add("2");

            // ref store for sale is the store the receipt was printed at
            refStore.Add(GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreNumber);


            string errorText;
            if (saleTenderTypes == null || saleTenderAmounts == null || saleTenderAmounts.Count == 0 || saleTenderTypes.Count == 0 || saleTenderAuth.Count == 0)
            {
                MessageBox.Show("Tender amounts are not set correctly. Cannot complete transaction");
                GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                return (false);

            }

            if (!(RetailProcedures.InsertRetailPaymentDetails(GlobalDataAccessor.Instance.OracleDA,
                                                              GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreNumber,
                                                              GlobalDataAccessor.Instance.DesktopSession.CashDrawerName,
                                                              GlobalDataAccessor.Instance.DesktopSession.FullUserName,
                                                              ShopDateTime.Instance.ShopDate.ToShortDateString(),
                                                              refNumber, refDate, refTime, refEvent, refAmount,
                                                              ShopDateTime.Instance.ShopTransactionTime.ToString(),
                                                              saleTenderTypes,
                                                              saleTenderAmounts,
                                                              saleTenderAuth,
                                                              topsDocType,
                                                              topsPaymentType,
                                                              out rcptNumber, out errorCode, out errorText)))
            {
                GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                BasicExceptionHandler.Instance.AddException("Failed to update teller", new ApplicationException());
                return (false);
            }

            //Add fees
            //Insert applicable fees if necessary
            //const string feeRefType = "LAY";

            //List<string> newFeeTypes = new List<string>();
            //List<string> newFeeAmount = new List<string>();
            //List<string> newFeeOrigAmount = new List<string>();
            //List<string> newFeeisProrated = new List<string>();
            //List<string> newFeeDates = new List<string>();
            //List<string> newFeeStateCodes = new List<string>();

            //Go through the list of fees in the pawn loan object and insert into fee table
            //string feeOpRevCode = FeeRevOpCodes.SALE.ToString();

            if (newFeeTypes.Count > 0)
            {
                if (!ProcessTenderProcedures.ExecuteInsertUpdatePawnFee(newFeeTypes,
                                                                        feeRefType, layawayTktNumber, GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreNumber,
                                                                        newFeeAmount, newFeeOrigAmount, newFeeisProrated, newFeeDates, newFeeStateCodes, GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile.UserName,
                                                                        feeOpRevCode, Utilities.GetLongValue(rcptNumber), out errorCode, out errorText))
                {
                    GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(
                        EndTransactionType.ROLLBACK);
                    if (FileLogger.Instance.IsLogError)
                        FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Could not update or insert fee data");
                    BasicExceptionHandler.Instance.AddException(
                        "Could not complete layaway - update fees failed :" + errorText + ": " + errorText,
                        new ApplicationException("Could not complete layaway"));
                    return (false);
                }
            }

            //insert store credit data if it was used
            if (shopCreditUsed && GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer != null)
            {
                if (!RetailProcedures.MaintainStoreCredit(GlobalDataAccessor.Instance.OracleDA,
                                                          GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.CustomerNumber,
                                                          GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreNumber,
                                                          shopCreditAmount, layawayTktNumber, "Layaway", layawayTktNumber, "D",
                                                          GlobalDataAccessor.Instance.DesktopSession.FullUserName,
                                                          ShopDateTime.Instance.ShopDate.FormatDate(),
                                                          trandate,
                                                          out errorCode,
                                                          out errorText))
                {
                    GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                    BasicExceptionHandler.Instance.AddException("Failed to update shop credit", new ApplicationException());
                    return (false);
                }
            }

            //Insert additional mdse info if needed
            if (addlMdseIcn.Count > 0)
            {
                if (!(MerchandiseProcedures.InsertAddlMdseInfo(addlMdseIcn, GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreNumber,
                    couponCodes, couponAmounts, tranCouponCodes, tranCouponAmounts, infoType, addlMdseRetPrice, "LAY", layawayTktNumber,
                    GlobalDataAccessor.Instance.DesktopSession.FullUserName, out errorCode, out errorText)))
                {
                    GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                    BasicExceptionHandler.Instance.AddException("Failed to insert additional merchandise information", new ApplicationException());
                    return (false);
                }
            }
            else
            {
                var craigslistItems = from rItem in GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.RetailItems
                                      where rItem.SaleType == SaleType.CraigsList
                                      select rItem;
                if (craigslistItems.Count() > 0)
                {
                    foreach (RetailItem retItem in craigslistItems)
                    {
                        infoType.Add(retItem.SaleType.ToString());
                        addlMdseIcn.Add(retItem.Icn);
                        couponCodes.Add("");
                        couponAmounts.Add("");
                        tranCouponAmounts.Add("");
                        tranCouponCodes.Add("");
                    }
                    if (addlMdseIcn.Count > 0)
                    {
                        if (!(MerchandiseProcedures.InsertAddlMdseInfo(addlMdseIcn, GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreNumber,
                            couponCodes, couponAmounts, tranCouponCodes, tranCouponAmounts, infoType, addlMdseRetPrice, "LAY", layawayTktNumber,
                            GlobalDataAccessor.Instance.DesktopSession.FullUserName, out errorCode, out errorText)))
                        {
                            FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Craigslist data could not be added to additional mdse info table" + errorText);
                        }

                    }
                }
            }



            //Commit teller transaction
            GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.COMMIT);

            //Print Layaway receipt
            rdVo.RefNumbers = refNumber;
            rdVo.ReceiptNumber = rcptNumber.ToString();
            rdVo.RefEvents = refEvent;
            rdVo.RefStores = refStore;
            rdVo.RefTypes = refType;
            if (SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled)
            {
                bool rt = false;
                try
                {
                    rt = this.GenerateLayawayPaymentReceipt(
                        ProcessTenderMode.LAYAWAY,
                        GlobalDataAccessor.Instance.DesktopSession.UserName.ToLowerInvariant(),
                        GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer,
                        GlobalDataAccessor.Instance.DesktopSession.ActiveLayaway,
                        GlobalDataAccessor.Instance.DesktopSession.ActiveVendor != null ? GlobalDataAccessor.Instance.DesktopSession.ActiveVendor.Name : "",
                        rdVo);
                }
                catch (Exception)
                {
                    rt = false;
                }

                if (!rt)
                {
                    if (FileLogger.Instance.IsLogError)
                        FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Could not print receipt for Layaway");
                    BasicExceptionHandler.Instance.AddException("Could not print receipt for Layaway", new ApplicationException("Could not print receipt while doing a Layaway sale"));
                    return (false);
                }
            }

            //print contract data here 
            GenerateLayawayContractDocument(rdVo);
            ResetDeskTopSessionVariables();
            return (true);
        }

        private bool executeLayawayPayment()
        {
            this.resetData();
            string errorText;
            bool shopCreditUsed = false;
            decimal shopCreditAmount = 0.0M;
            string trandate = ShopDateTime.Instance.ShopDate.FormatDate() + " " +
                              ShopDateTime.Instance.ShopTime.ToString();

            int rcptNumber = 0;

            List<string> saleTenderTypes = GlobalDataAccessor.Instance.DesktopSession.TenderTypes;
            List<string> saleTenderAmounts = GlobalDataAccessor.Instance.DesktopSession.TenderAmounts;
            List<string> saleTenderAuth = GlobalDataAccessor.Instance.DesktopSession.TenderReferenceNumber;

            List<TenderEntryVO> tenderDetails = Utilities.CloneObject(GlobalDataAccessor.Instance.DesktopSession.TenderData);


            decimal totalServiceAmount = (from tender in tenderDetails
                                          select tender.Amount).Sum();

            //If one of the tendetr type is shop credit update the amount in the database in the storecredit table
            foreach (TenderEntryVO tdata in GlobalDataAccessor.Instance.DesktopSession.TenderData)
            {
                if (tdata.TenderType == TenderTypes.STORECREDIT)
                {
                    shopCreditUsed = true;
                    shopCreditAmount = tdata.Amount;
                }
            }

            //Add receipt details
            ReceiptDetailsVO rdVo = new ReceiptDetailsVO();
            List<string> refDate = new List<string>();
            List<string> refNumber = new List<string>();
            List<string> refType = new List<string>();
            List<string> refEvent = new List<string>();
            List<string> refAmount = new List<string>();
            List<string> refStore = new List<string>();
            List<string> refTime = new List<string>();
            List<PairType<string, int>> pickupReceipts = new List<PairType<string, int>>();
            List<PairType<string, List<TenderEntryVO>>> pickupTenderData = new List<PairType<string, List<TenderEntryVO>>>();

            string errorCode;
            int numberofLayawayPayments = GlobalDataAccessor.Instance.DesktopSession.ServiceLayaways.Count;
            int counter = 1;
            List<string> amountTendered = new List<string>();
            for (int k = 0; k < saleTenderAmounts.Count; k++)
                amountTendered.Add("0");
            foreach (LayawayVO currentLayaway in GlobalDataAccessor.Instance.DesktopSession.ServiceLayaways)
            {

                List<string> paymentTenderAmounts = new List<string>();
                rdVo = new ReceiptDetailsVO();
                refDate = new List<string>();
                refNumber = new List<string>();
                refType = new List<string>();
                refEvent = new List<string>();
                refAmount = new List<string>();
                refStore = new List<string>();
                refTime = new List<string>();

                if (currentLayaway == null)
                    return false;

                decimal totalReceiptAmount = Math.Round(currentLayaway.Payments.Sum(layPmt => layPmt.Amount), 2);

                decimal distributionPercentage = totalReceiptAmount / totalServiceAmount;

                if (saleTenderAmounts.Count > 0)
                {
                    paymentTenderAmounts.AddRange(saleTenderAmounts.Select(s => Utilities.GetDecimalValue(s, 0)).Select(amtTendered => (Math.Round((amtTendered * distributionPercentage), 2)).ToString()));
                }

                int numberOfTenderEntries = paymentTenderAmounts.Count;

                if (numberOfTenderEntries > 1)
                {
                    var totalTenderAmt = 0.0M;
                    for (int i = 0; i < numberOfTenderEntries - 1; i++)
                        totalTenderAmt += Utilities.GetDecimalValue(paymentTenderAmounts[i], 0);
                    paymentTenderAmounts.RemoveAt(numberOfTenderEntries - 1);
                    paymentTenderAmounts.Insert(numberOfTenderEntries - 1, (totalReceiptAmount - totalTenderAmt).ToString());


                }
                if (counter == numberofLayawayPayments)
                {

                    for (int j = 0; j < numberOfTenderEntries; j++)
                    {
                        paymentTenderAmounts[j] = (Utilities.GetDecimalValue(saleTenderAmounts[j]) - Utilities.GetDecimalValue(amountTendered[j])).ToString();
                    }

                }
                else
                {
                    for (int j = 0; j < numberOfTenderEntries; j++)
                    {
                        amountTendered[j] = (Utilities.GetDecimalValue(amountTendered[j]) + Utilities.GetDecimalValue(paymentTenderAmounts[j])).ToString();
                    }
                }


                int index = 0;
                List<TenderEntryVO> paymentTenders = new List<TenderEntryVO>();
                foreach (TenderEntryVO tData in tenderDetails)
                {
                    TenderEntryVO newTData = tData;
                    newTData.Amount = Utilities.GetDecimalValue(paymentTenderAmounts[index], 0);
                    paymentTenders.Add(newTData);

                    index++;
                }


                // ref date for sale is the date made

                refDate.Add(ShopDateTime.Instance.ShopDate.FormatDate());

                //ref time for sale is the current time

                refTime.Add(ShopDateTime.Instance.ShopTransactionTime);

                // ref number for sale is the ticket number

                refNumber.Add(currentLayaway.TicketNumber.ToString());

                // ref type for layaway is 4
                refType.Add("4");

                // ref event for layaway payment is "LAYPMT"
                refEvent.Add(ReceiptEventTypes.LAYPMT.ToString());

                // ref amount is the active retail sale amount
                refAmount.Add(totalReceiptAmount.ToString());

                // ref store for sale is the store the receipt was printed at
                refStore.Add(GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreNumber);

                List<string> topsPaymentType = saleTenderTypes.Select(s => "R").ToList();
                if (currentLayaway.LoanStatus == ProductStatus.PU)
                    topsPaymentType = saleTenderTypes.Select(s => "P").ToList();
                List<string> topsDocType = saleTenderTypes.Select(s => "4").ToList();
                GlobalDataAccessor.Instance.DesktopSession.beginTransactionBlock();
                if (!(RetailProcedures.InsertRetailPaymentDetails(GlobalDataAccessor.Instance.OracleDA,
                                                                  GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreNumber,
                                                                  GlobalDataAccessor.Instance.DesktopSession.CashDrawerName,
                                                                  GlobalDataAccessor.Instance.DesktopSession.FullUserName,
                                                                  ShopDateTime.Instance.ShopDate.ToShortDateString(),
                                                                  refNumber, refDate, refTime, refEvent, refAmount,
                                                                  ShopDateTime.Instance.ShopTransactionTime.ToString(),
                                                                  saleTenderTypes,
                                                                  paymentTenderAmounts,
                                                                  saleTenderAuth,
                                                                  topsDocType,
                                                                  topsPaymentType,
                                                                  out rcptNumber, out errorCode, out errorText)))
                {
                    GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                    BasicExceptionHandler.Instance.AddException("Failed to update teller", new ApplicationException());
                    return (false);
                }
                GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.COMMIT);

                if (currentLayaway.LoanStatus == ProductStatus.PU)
                {
                    pickupReceipts.Add(new PairType<string, int>(currentLayaway.TicketNumber.ToString(), rcptNumber));

                    pickupTenderData.Add(new PairType<string, List<TenderEntryVO>>(currentLayaway.TicketNumber.ToString(), Utilities.CloneObject(paymentTenders)));
                }
                else
                {


                    rdVo.ReceiptNumber = rcptNumber.ToString();
                    rdVo.RefStores.Add(GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreNumber);
                    rdVo.RefEvents.Add(ReceiptEventTypes.LAYPMT.ToString());
                    rdVo.RefTypes.Add("4");
                    rdVo.RefNumbers.Add(currentLayaway.TicketNumber.ToString());
                    rdVo.RefTimes.Add(ShopDateTime.Instance.ShopTransactionTime);
                    rdVo.RefDates.Add(ShopDateTime.Instance.ShopDate.FormatDate());
                    rdVo.ReceiptDate = ShopDateTime.Instance.ShopDate;
                    if (SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled)
                    {
                        bool rt;
                        try
                        {
                            GlobalDataAccessor.Instance.DesktopSession.TenderData = paymentTenders;
                            rt = this.GenerateLayawayPaymentReceipt(
                                ProcessTenderMode.LAYPAYMENT,
                                GlobalDataAccessor.Instance.DesktopSession.UserName.ToLowerInvariant(),
                                GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer,
                                currentLayaway,
                                GlobalDataAccessor.Instance.DesktopSession.ActiveVendor != null ? GlobalDataAccessor.Instance.DesktopSession.ActiveVendor.Name : "",
                                rdVo);

                        }
                        catch (Exception)
                        {
                            rt = false;
                        }

                        if (!rt)
                        {
                            if (FileLogger.Instance.IsLogError)
                                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Could not print receipt for Layaway" + currentLayaway.TicketNumber.ToString());
                        }
                    }
                }
                counter++;

            }
            //Check if any of the layaways are set to pickup
            List<LayawayVO> pickupLayaways = (from layaway in GlobalDataAccessor.Instance.DesktopSession.ServiceLayaways
                                              where layaway.LoanStatus == ProductStatus.PU
                                              select layaway).ToList();
            if (pickupLayaways.Count > 0)
            {
                for (int i = 0; i < pickupLayaways.Count; i++)
                {
                    int saleTicketNumber;
                    rdVo = new ReceiptDetailsVO();
                    List<string> icn = pickupLayaways[i].RetailItems.Select(item => item.Icn.ToString()).ToList();
                    GlobalDataAccessor.Instance.DesktopSession.beginTransactionBlock();
                    bool retValue = RetailProcedures.ProcessLayawayPickup(
                        GlobalDataAccessor.Instance.OracleDA, GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreNumber,
                        pickupLayaways[i].CustomerNumber,
                        pickupLayaways[i].TicketNumber.ToString(),
                        ShopDateTime.Instance.ShopDate.FormatDate(),
                        ShopDateTime.Instance.ShopTransactionTime.ToString(),
                        GlobalDataAccessor.Instance.DesktopSession.FullUserName,
                        icn,
                        GlobalDataAccessor.Instance.DesktopSession.CashDrawerName,
                        GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.BackgroundCheckRefNumber,
                        out saleTicketNumber,
                        out errorCode,
                        out errorText);
                    if (!retValue)
                    {
                        GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                        FileLogger.Instance.logMessage(LogLevel.ERROR, this, errorText);
                        return false;
                    }
                    //Insert receipt for the sale
                    decimal totalReceiptAmount = Math.Round(pickupLayaways[i].Payments.Sum(layPmt => layPmt.Amount), 2);
                    List<string> recptDate = new List<string>();
                    List<string> recptNumber = new List<string>();
                    List<string> recptType = new List<string>();
                    List<string> recptEvent = new List<string>();
                    List<string> recptAmount = new List<string>();
                    List<string> recptStore = new List<string>();
                    List<string> recptTime = new List<string>();
                    // ref date for sale is the date made
                    rdVo.ReceiptDate = ShopDateTime.Instance.ShopDate;
                    recptDate.Add(ShopDateTime.Instance.ShopDate.FormatDate());
                    rdVo.RefDates.Add(ShopDateTime.Instance.ShopDate.FormatDate());
                    //ref time for sale is the current time

                    recptTime.Add(ShopDateTime.Instance.ShopTransactionTime);
                    rdVo.RefTimes.Add(ShopDateTime.Instance.ShopTransactionTime);
                    // ref number for sale is the ticket number

                    recptNumber.Add(saleTicketNumber.ToString());
                    rdVo.RefNumbers.Add(saleTicketNumber.ToString());
                    // ref type for sale is 3
                    recptType.Add("3");
                    rdVo.RefTypes.Add("3");

                    // ref event for sale
                    recptEvent.Add(ReceiptEventTypes.SALE.ToString());
                    rdVo.RefEvents.Add(ReceiptEventTypes.SALE.ToString());

                    // ref amount is the active retail sale amount
                    recptAmount.Add(totalReceiptAmount.ToString());

                    // ref store for sale is the store the receipt was printed at
                    recptStore.Add(GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreNumber);
                    rdVo.RefStores.Add(GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreNumber);
                    var rNumber = from pickupRcpt in pickupReceipts
                                  where pickupRcpt.Left == pickupLayaways[i].TicketNumber.ToString()
                                  select pickupRcpt.Right;
                    foreach (int rNum in rNumber)
                        rdVo.ReceiptNumber = rNum.ToString();

                    bool rtVal = RetailProcedures.InsertReceiptDetail(
                        GlobalDataAccessor.Instance.OracleDA,
                        GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreNumber, GlobalDataAccessor.Instance.DesktopSession.UserName, ShopDateTime.Instance.ShopDate.ToShortDateString(),
                        GlobalDataAccessor.Instance.DesktopSession.FullUserName, recptDate, recptTime, recptNumber, recptType, recptEvent, recptAmount, recptStore,
                        rNumber.First(), out errorCode, out errorText);
                    if (!rtVal)
                    {
                        GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                        BasicExceptionHandler.Instance.AddException("Could not insert sale receipt", new ApplicationException("Could not insert sale receipt"));
                        FileLogger.Instance.logMessage(LogLevel.FATAL, this, "Could not insert receipt");
                        return (false);
                    }

                    //Add fees
                    //Insert applicable fees if necessary
                    const string feeRefType = "LAY";

                    List<string> newFeeTypes = new List<string>();
                    List<string> newFeeAmount = new List<string>();
                    List<string> newFeeOrigAmount = new List<string>();
                    List<string> newFeeisProrated = new List<string>();
                    List<string> newFeeDates = new List<string>();
                    List<string> newFeeStateCodes = new List<string>();

                    //Go through the list of fees in the pawn loan object and insert into fee table
                    string feeOpRevCode = FeeRevOpCodes.LAYPICKUP.ToString();
                    if (pickupLayaways[i].Fees.Count > 0)
                    {
                        var origFees = from fees in pickupLayaways[i].Fees
                                       where fees.OriginalAmount != 0
                                       select fees;
                        foreach (Fee custloanfee in origFees)
                        {
                            if (custloanfee.FeeState == FeeStates.PAID)
                                continue;
                            newFeeTypes.Add(custloanfee.FeeType.ToString());
                            newFeeOrigAmount.Add(custloanfee.OriginalAmount.ToString());
                            newFeeAmount.Add(custloanfee.Value.ToString());
                            newFeeisProrated.Add(custloanfee.Prorated ? "1" : "0");
                            newFeeDates.Add(trandate);
                            if (custloanfee.Waived)
                                newFeeStateCodes.Add(FeeStates.WAIVED.ToString());
                            else if (custloanfee.FeeState == FeeStates.VOID)
                                newFeeStateCodes.Add(FeeStates.VOID.ToString());
                            else
                                newFeeStateCodes.Add(FeeStates.PAID.ToString());
                        }
                    }

                    if (newFeeTypes.Count > 0)
                    {
                        if (!ProcessTenderProcedures.ExecuteInsertUpdatePawnFee(newFeeTypes,
                                                                                feeRefType, pickupLayaways[i].TicketNumber, GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreNumber,
                                                                                newFeeAmount, newFeeOrigAmount, newFeeisProrated, newFeeDates, newFeeStateCodes, GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile.UserName,
                                                                                feeOpRevCode, Utilities.GetLongValue(rNumber.First()), out errorCode, out errorText))
                        {
                            GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(
                                EndTransactionType.ROLLBACK);
                            if (FileLogger.Instance.IsLogError)
                                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Could not update or insert fee data for " + pickupLayaways[i].TicketNumber);
                            BasicExceptionHandler.Instance.AddException("Failed to update/insert fees", new ApplicationException());
                            return (false);

                        }
                    }


                    GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.COMMIT);
                    var tData = from pickupRcpt in pickupTenderData
                                where pickupRcpt.Left == pickupLayaways[i].TicketNumber.ToString()
                                select pickupRcpt.Right;
                    foreach (List<TenderEntryVO> tendData in tData)
                        GlobalDataAccessor.Instance.DesktopSession.TenderData = tendData;
                    if (SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled)
                    {
                        bool rt;
                        try
                        {
                            rt = this.GenerateLayawayPaymentReceipt(
                                ProcessTenderMode.LAYPICKUP,
                                GlobalDataAccessor.Instance.DesktopSession.UserName.ToLowerInvariant(),
                                GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer,
                                pickupLayaways[i],
                                GlobalDataAccessor.Instance.DesktopSession.ActiveVendor != null ? GlobalDataAccessor.Instance.DesktopSession.ActiveVendor.Name : "",
                                rdVo);
                            GenerateLayawayPickingSlip(pickupLayaways[i]);
                        }
                        catch (Exception)
                        {
                            rt = false;
                        }

                        if (!rt)
                        {
                            if (FileLogger.Instance.IsLogError)
                                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Could not print receipt for layaway paid out" + pickupLayaways[i].TicketNumber.ToString());
                        }
                    }
                }
            }

            if (shopCreditUsed && GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer != null)
            {
                foreach (LayawayVO currentLayaway in GlobalDataAccessor.Instance.DesktopSession.ServiceLayaways)
                {
                    GlobalDataAccessor.Instance.DesktopSession.beginTransactionBlock();
                    decimal amount = Math.Round(shopCreditAmount / totalServiceAmount * currentLayaway.Payments[0].Amount, 2);
                    //insert store credit data if it was used
                    if (!RetailProcedures.MaintainStoreCredit(GlobalDataAccessor.Instance.OracleDA,
                                                              GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.CustomerNumber,
                                                              GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreNumber,
                                                              amount, currentLayaway.TicketNumber, "LAYPMT", currentLayaway.TicketNumber, "D",
                                                              GlobalDataAccessor.Instance.DesktopSession.FullUserName,
                                                              ShopDateTime.Instance.ShopDate.FormatDate(),
                                                              trandate,
                                                              out errorCode,
                                                              out errorText))
                    {
                        GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                        BasicExceptionHandler.Instance.AddException("Failed to update shop credit", new ApplicationException());
                        return (false);
                    }
                    GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.COMMIT);
                }
            }

            ResetDeskTopSessionVariables();
            return (true);
        }

        private bool executeRefundLayawayPayment()
        {
            this.resetData();
            LayawayVO currentLayaway = GlobalDataAccessor.Instance.DesktopSession.ActiveLayaway;

            string trandate = ShopDateTime.Instance.ShopDate.FormatDate() + " " +
                              ShopDateTime.Instance.ShopTime.ToString();
            ReceiptDetailsVO rdVo = new ReceiptDetailsVO();
            rdVo.ReceiptDate = ShopDateTime.Instance.ShopDate;
            List<string> refundTenderTypes = GlobalDataAccessor.Instance.DesktopSession.TenderTypes;
            List<string> refundTenderAmounts = GlobalDataAccessor.Instance.DesktopSession.TenderAmounts;
            List<string> refundTenderAuth = GlobalDataAccessor.Instance.DesktopSession.TenderReferenceNumber;

            List<TenderEntryVO> tenderDetails = GlobalDataAccessor.Instance.DesktopSession.TenderData;

            decimal refundAmount = (from tender in tenderDetails
                                    select tender.Amount).Sum();

            int rcptNumber;
            string errorCode;
            string errorText;

            List<string> refDate = new List<string>();
            List<string> refNumber = new List<string>();
            List<string> refType = new List<string>();
            List<string> refEvent = new List<string>();
            List<string> refAmount = new List<string>();
            List<string> refStore = new List<string>();
            List<string> refTime = new List<string>();

            refDate.Add(ShopDateTime.Instance.ShopDate.FormatDate());

            //ref time for layaway is the current time
            refTime.Add(ShopDateTime.Instance.ShopTransactionTime);

            // ref number for layaway is the ticket number
            refNumber.Add(currentLayaway.TicketNumber.ToString());

            // ref type for layaway is 4
            refType.Add("4");

            // ref event for refund layaway payment is "LAYREF"
            string rcptRefEvent = ReceiptEventTypes.LAYREF.ToString();
            var receiptEvent = (from rcpt in currentLayaway.Receipts
                                where rcpt.ReceiptNumber == GlobalDataAccessor.Instance.DesktopSession.ReceiptToRefund
                                      && rcpt.RefNumber == currentLayaway.TicketNumber.ToString()
                                select rcpt.Event).FirstOrDefault();
            if (receiptEvent == "LAY")
                rcptRefEvent = ReceiptEventTypes.LAYDOWNREF.ToString();

            refEvent.Add(rcptRefEvent);

            // ref amount is the refund amount
            refAmount.Add(refundAmount.ToString());

            // ref store for layaway is the store the refund is being processed
            refStore.Add(GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreNumber);
            GlobalDataAccessor.Instance.DesktopSession.beginTransactionBlock();
            bool retValue = RetailProcedures.RefundLayawayPayment(
                GlobalDataAccessor.Instance.OracleDA, GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreNumber,
                GlobalDataAccessor.Instance.DesktopSession.CashDrawerName,
                GlobalDataAccessor.Instance.DesktopSession.FullUserName,
                ShopDateTime.Instance.ShopDate.FormatDate(),
                GlobalDataAccessor.Instance.DesktopSession.ReceiptToRefund,
                currentLayaway.TicketNumber,
                refDate, refTime, refEvent, refAmount, ShopDateTime.Instance.ShopTransactionTime.ToString(),
                refundTenderTypes,
                refundTenderAmounts,
                refundTenderAuth,
                out rcptNumber,
                out errorCode,
                out errorText);
            if (!retValue)
            {
                GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                if (FileLogger.Instance.IsLogError)
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Could not insert refund layaway payment details for " + currentLayaway.TicketNumber + " " + errorText);

                return false;
            }

            //Go through the list of fees in the pawn loan object and insert into fee table
            const string feeRefType = "LAY";

            List<string> newFeeTypes = new List<string>();
            List<string> newFeeAmount = new List<string>();
            List<string> newFeeOrigAmount = new List<string>();
            List<string> newFeeisProrated = new List<string>();
            List<string> newFeeDates = new List<string>();
            List<string> newFeeStateCodes = new List<string>();

            string feeOpRevCode = FeeRevOpCodes.REFUNDLAYAWAY.ToString();
            if (currentLayaway.Fees.Count > 0)
            {
                var origFees = from fees in currentLayaway.Fees
                               where fees.OriginalAmount != 0
                               select fees;
                foreach (Fee custloanfee in origFees)
                {
                    newFeeTypes.Add(custloanfee.FeeType.ToString());
                    newFeeOrigAmount.Add(custloanfee.OriginalAmount.ToString());
                    newFeeAmount.Add(custloanfee.Value.ToString());
                    newFeeisProrated.Add("0");
                    newFeeDates.Add(trandate);
                    newFeeStateCodes.Add(custloanfee.FeeState.ToString());
                }
            }

            if (newFeeTypes.Count > 0)
            {
                if (!ProcessTenderProcedures.ExecuteInsertUpdatePawnFee(newFeeTypes,
                                                                        feeRefType, currentLayaway.TicketNumber, currentLayaway.StoreNumber,
                                                                        newFeeAmount, newFeeOrigAmount, newFeeisProrated, newFeeDates, newFeeStateCodes, GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile.UserName,
                                                                        feeOpRevCode, Utilities.GetLongValue(rcptNumber), out errorCode, out errorText))
                {
                    GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(
                        EndTransactionType.ROLLBACK);
                    if (FileLogger.Instance.IsLogError)
                        FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Could not update or insert fee data");
                    BasicExceptionHandler.Instance.AddException(
                        "Could not complete refund - update fees failed :" + errorText + ": " + errorText,
                        new ApplicationException("Could not complete layaway refund"));
                    return (false);
                }
            }

            GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.COMMIT);
            //here add code to print receipt
            rdVo.RefNumbers = refNumber;
            rdVo.ReceiptNumber = rcptNumber.ToString();
            rdVo.RefEvents = refEvent;
            rdVo.RefStores = refStore;
            rdVo.RefTypes = refType;
            retValue = this.GenerateLayawayPaymentReceipt(
                ProcessTenderMode.LAYPAYMENTREF,
                GlobalDataAccessor.Instance.DesktopSession.UserName.ToLowerInvariant(),
                GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer,
                currentLayaway,
                GlobalDataAccessor.Instance.DesktopSession.ActiveVendor != null ? GlobalDataAccessor.Instance.DesktopSession.ActiveVendor.Name : "",
                rdVo);
            if (!retValue)
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Error in Layaway refund payment " + errorText);
                return false;
            }
            return (true);
        }

        private void GenerateLayawayPickingSlip(LayawayVO currentLayaway)
        {
            LayawayReportObject rptObj = new LayawayReportObject();
            Reports.Layaway.LayawayPickingSlip lcRpt = new Reports.Layaway.LayawayPickingSlip(PdfLauncher.Instance);
            rptObj.CurrentLayaway = currentLayaway;
            if (GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer != null)
            {
                rptObj.CustomerName = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.LastName + ", " + GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.FirstName;
                ContactVO cVo = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.getPrimaryContact();
                if (cVo != null)
                {
                    rptObj.ContactNumber = Commons.FormatPhoneNumberForUI(cVo.ContactAreaCode + cVo.ContactPhoneNumber);
                }
                else
                    rptObj.ContactNumber = "";
            }
            else
            {
                rptObj.CustomerName = "";
                rptObj.ContactNumber = "";
            }

            rptObj.ReportTempFileFullName = SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseLogPath + "\\LayawayPickingSlip" + DateTime.Now.ToString("MMddyyyyhhmmssFFFFFFF") + ".pdf";
            //rptObj.ReportTempFileFullName = "C:\\LayawayContract.pdf";
            rptObj.ReportTitle = "Layaway Picking Slip";
            rptObj.ReportNumber = (int)LayawayReportIDs.LayawayPickingSlip;
            rptObj.ReportEmployee = GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile.EmployeeNumber;
            rptObj.ReportTempFile = SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseLogPath;
            rptObj.ReportStore = GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreName;
            rptObj.ReportError = string.Empty;
            rptObj.ReportErrorLevel = 0;
            lcRpt.ReportObject = rptObj;

            if (lcRpt.CreateReport())
            {
                //GenerateLayawayPickingSlip(receiptDetailsVO);
                const string formName = "LayawayPickingSlip.pdf";

                if (SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled &&
                    GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.IsValid)
                {
                    if (FileLogger.Instance.IsLogInfo)
                    {
                        FileLogger.Instance.logMessage(LogLevel.INFO, "ProcessTenderController",
                                                       "Printing layaway picking slip on printer {0}");
                    }
                    string errMsg =
                        PrintingUtilities.printDocument(
                            rptObj.ReportTempFileFullName,
                            GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.IPAddress,
                            GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.Port, 1);

                    if (errMsg.IndexOf("SUCCESS") == -1)
                    {
                        if (FileLogger.Instance.IsLogError)
                        {
                            FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Cannot print Layaway Picking Slip");
                        }
                    }
                }
                /* var pDoc = new CouchDbUtils.PawnDocInfo();

                //Set document add calls
                pDoc.UseCurrentShopDateTime = true;
                pDoc.StoreNumber = GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreNumber;
                pDoc.CustomerNumber = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.CustomerNumber;
                pDoc.DocumentType = Document.DocTypeNames.PDF;
                pDoc.DocFileName = rptObj.ReportTempFileFullName;
                //pDoc.TicketNumber = cds.ActiveCustomer.c
                //pDoc.DocumentSearchType = CouchDbUtils.DocSearchType.STORE_TICKET;
                pDoc.TicketNumber = GlobalDataAccessor.Instance.DesktopSession.ServiceLayaways.Last().TicketNumber;
                long recNumL = 0L;
                if (long.TryParse(receiptDetailsVO.ReceiptNumber, out recNumL))
                {
                pDoc.ReceiptNumber = recNumL;
                }

                //Add this document to the pawn document registry and document storage
                string errText;
                if (!CouchDbUtils.AddPawnDocument(dA, cC, cds.UserName, ref pDoc, out errText))
                {
                if (FileLogger.Instance.IsLogError)
                FileLogger.Instance.logMessage(LogLevel.ERROR, this,
                "Could not store memo of extension in document storage: {0} - FileName: {1}", errText, rptObj.ReportTempFileFullName);
                BasicExceptionHandler.Instance.AddException(
                "Could not store memo of extension in document storage",
                new ApplicationException("Could not store receipt in document storage: " + errText));
                }*/

                File.Delete(rptObj.ReportTempFileFullName);
            }
        }

        private void GenerateLayawayContractDocument(ReceiptDetailsVO receiptDetailsVO)
        {
            DesktopSession cds = GlobalDataAccessor.Instance.DesktopSession;
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;
            SecuredCouchConnector cC = GlobalDataAccessor.Instance.CouchDBConnector;

            LayawayVO currentLayaway = GlobalDataAccessor.Instance.DesktopSession.ActiveLayaway;
            LayawayReportObject rptObj = new LayawayReportObject();
            Reports.Layaway.LayawayContractReport lcRpt = new Reports.Layaway.LayawayContractReport(PdfLauncher.Instance);
            rptObj.ReportStore = GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreName;
            rptObj.ReportStoreDesc1 = GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreAddress1;
            rptObj.ReportStoreDesc2 = GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreCityName + "," + GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.State + ", " + GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreZipCode;
            rptObj.CurrentLayaway = currentLayaway;
            if (GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer != null)
            {
                rptObj.CustomerName = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.LastName + ", " + GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.FirstName;
                ContactVO cVo = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.getPrimaryContact();
                if (cVo != null)
                {
                    rptObj.ContactNumber = Commons.Format10And11CharacterPhoneNumberForUI(cVo.ContactAreaCode + cVo.ContactPhoneNumber);
                }
                else
                    rptObj.ContactNumber = "";
            }
            else
            {
                rptObj.CustomerName = "";
                rptObj.ContactNumber = "";
            }

            rptObj.ReportTempFileFullName = SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseLogPath + "\\LayawayContract" + DateTime.Now.ToString("MMddyyyyhhmmssFFFFFFF") + ".pdf";
            //rptObj.ReportTempFileFullName = "C:\\LayawayContract.pdf";
            rptObj.ReportTitle = "Layaway Contract";
            rptObj.ReportNumber = (int)LayawayReportIDs.LayawayContract;
            rptObj.ReportEmployee = GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile.EmployeeNumber;
            rptObj.ReportTempFile = SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseLogPath;
            rptObj.ReportStore = GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreName;
            rptObj.ReportError = string.Empty;
            rptObj.ReportErrorLevel = 0;
            lcRpt.ReportObject = rptObj;

            //lcRpt.CreateReport();
            //GenerateLayawayContractDocument(receiptDetailsVO);

            if (lcRpt.CreateReport())
            {
                const string formName = "LayawayContract.pdf";

                if (SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled &&
                    cds.LaserPrinter.IsValid)
                {
                    if (FileLogger.Instance.IsLogInfo)
                    {
                        FileLogger.Instance.logMessage(LogLevel.INFO, "ProcessTenderController", "Printing Layaway contract on {0}",
                                                       cds.LaserPrinter);
                    }

                    string errMsg =
                        PrintingUtilities.printDocument(
                            rptObj.ReportTempFileFullName,
                            cds.LaserPrinter.IPAddress,
                            cds.LaserPrinter.Port, 3);

                    if (errMsg.IndexOf("SUCCESS", StringComparison.OrdinalIgnoreCase) == -1)
                    {
                        if (FileLogger.Instance.IsLogError)
                        {
                            FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Cannot print Layaway Contract");
                        }
                    }
                }
                var pDoc = new CouchDbUtils.PawnDocInfo();
                //Set document add calls
                pDoc.UseCurrentShopDateTime = true;
                pDoc.StoreNumber = GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreNumber;
                pDoc.CustomerNumber = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.CustomerNumber;
                pDoc.DocumentType = Document.DocTypeNames.PDF;
                pDoc.DocFileName = rptObj.ReportTempFileFullName;
                //pDoc.TicketNumber = cds.ActiveCustomer.c
                //pDoc.DocumentSearchType = CouchDbUtils.DocSearchType.STORE_TICKET;
                pDoc.TicketNumber = currentLayaway.TicketNumber;
                long recNumL = 0L;
                if (long.TryParse(receiptDetailsVO.ReceiptNumber, out recNumL))
                {
                    pDoc.ReceiptNumber = recNumL;
                }

                //Add this document to the pawn document registry and document storage
                string errText;
                if (!CouchDbUtils.AddPawnDocument(dA, cC, cds.UserName, ref pDoc, out errText))
                {
                    if (FileLogger.Instance.IsLogError)
                        FileLogger.Instance.logMessage(LogLevel.ERROR, this,
                                                       "Could not store Layaway Contract in document storage: {0} - FileName: {1}", errText, rptObj.ReportTempFileFullName);
                    BasicExceptionHandler.Instance.AddException(
                        "Could not store Layaway Contract in document storage",
                        new ApplicationException("Could not store receipt in document storage: " + errText));
                }

                //add this document to to ticketfiles
                //File.Delete(rptObj.ReportTempFileFullName);
                this.TicketFiles.Add(rptObj.ReportTempFileFullName);
            }
        }

        private bool executeBuyReturn()
        {
            this.resetData();
            //Get the purchase object
            if (CollectionUtilities.isEmpty(GlobalDataAccessor.Instance.DesktopSession.Purchases))
            {
                return false;
            }

            //Start transaction block
            if (!this.beginProcessTenderTransaction("Process Tender (Return Purchase block)"))
            {
                return (false);
            }

            //Amount of return
            var amtReturned = (from item in GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.Items
                               select item.ItemAmount).Sum();

            //Perform inserts
            if (!this.performReturnPurchaseInserts())
            {
                GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                BasicExceptionHandler.Instance.AddException("Failed to insert return purchase data", new ApplicationException());
                return (false);
            }

            //Status the loan
            GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.LoanStatus = ProductStatus.RET;

            //Add receipt details
            List<string> refDate = new List<string>();
            List<string> refNumber = new List<string>();
            List<string> refType = new List<string>();
            List<string> refEvent = new List<string>();
            List<string> refAmount = new List<string>();
            List<string> refStore = new List<string>();
            List<string> refTime = new List<string>();

            // ref date for purchase is the date made
            refDate.Add(ShopDateTime.Instance.ShopDate.FormatDate());

            //ref time for purchase is the current time
            refTime.Add(ShopDateTime.Instance.ShopTransactionTime);

            // ref number for new pawn loan is the ticket number
            refNumber.Add(this.purchaseNumber.ToString());

            // ref type for purchase is 2
            refType.Add("2");

            // ref event for return purchase is "RET"
            refEvent.Add("RET");

            // ref amount for return purchase is the purchase amount
            refAmount.Add(amtReturned.ToString());

            // ref store for purchase is the store the receipt was printed at
            refStore.Add(GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreNumber);

            //Update teller and insert receipt details info and inform user to disburse cash
            int rcptNumber;
            string errorCode;
            string errorText;
            string vendorName = "";
            if (GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger.Equals("returncustomerbuy", StringComparison.OrdinalIgnoreCase))
                GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.PurchaseTenderType = PurchaseTenderTypes.CASHIN.ToString();
            else
            {
                vendorName = GlobalDataAccessor.Instance.DesktopSession.ActiveVendor.Name;
                if (GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.PurchaseTenderType == PurchaseTenderTypes.BILLTOAP.ToString())
                    GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.PurchaseTenderType = PurchaseTenderTypes.RBILLTOAP.ToString();
                else
                    GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.PurchaseTenderType = PurchaseTenderTypes.CASHIN.ToString();

            }

            if (!(PurchaseProcedures.InsertPurchasePaymentDetails(GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreNumber,
                                                                  GlobalDataAccessor.Instance.DesktopSession.CashDrawerName, GlobalDataAccessor.Instance.DesktopSession.FullUserName,
                                                                  ShopDateTime.Instance.ShopDate.ToShortDateString(), refNumber, refDate, refTime, refEvent, refAmount,
                                                                  ShopDateTime.Instance.ShopTransactionTime.ToString(), GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.PurchaseTenderType, out rcptNumber, out errorCode, out errorText)))
            {
                GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                BasicExceptionHandler.Instance.AddException("Failed to update teller", new ApplicationException());
                return (false);
            }

            //Commit teller transaction
            if (!this.commitProcessTenderTransaction("Process Tender (Return Purchases block)"))
            {
                return (false);
            }

            GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.TicketNumber = Utilities.GetIntegerValue(purchaseNumber, 0);
            List<PurchaseVO> purchases = new List<PurchaseVO>(1) { GlobalDataAccessor.Instance.DesktopSession.ActivePurchase };
            //Print receipt
            ReceiptDetailsVO rdVo = new ReceiptDetailsVO();
            rdVo.RefNumbers = refNumber;
            rdVo.ReceiptNumber = rcptNumber.ToString();
            rdVo.RefEvents = refEvent;
            rdVo.RefStores = refStore;
            rdVo.RefTypes = refType;
            if (SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled)
            {
                bool rt = false;
                try
                {
                    rt = this.generatePurchaseReceipt(
                        ProcessTenderMode.RETURNBUY,
                        GlobalDataAccessor.Instance.DesktopSession.UserName.ToLowerInvariant(),
                        GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer,
                        purchases,
                        vendorName,
                        rdVo);
                }
                catch (Exception)
                {
                    rt = false;
                }

                if (!rt)
                {
                    if (FileLogger.Instance.IsLogError)
                        FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Could not print receipt for purchase return");
                    BasicExceptionHandler.Instance.AddException("Could not print receipt for purchase return", new ApplicationException("Could not print receipt while servicing loans"));
                    return (false);
                }

                bool success = true;
                long idx = 0;
                foreach (var curPurch in purchases)
                {
                    string fileNameGenerated;
                    bool chkStorePurchaseDocument;
                    if (GlobalDataAccessor.Instance.CurrentSiteId.State.Equals(States.Indiana))
                    {
                        chkStorePurchaseDocument = PurchaseDocumentIndiana.GeneratePurchaseDocumentIndiana(
                            GlobalDataAccessor.Instance.DesktopSession, curPurch, out fileNameGenerated);
                    }
                    else
                    {
                        chkStorePurchaseDocument = PurchaseDocumentGenerator.GeneratePurchaseDocument(
                            GlobalDataAccessor.Instance.DesktopSession, curPurch, out fileNameGenerated);
                    }
                    if (!chkStorePurchaseDocument)
                    {
                        success = false;
                    }
                    else
                    {
                        this.ProcessedTickets.Add(new PairType<long, string>(idx, fileNameGenerated));
                        this.TicketFiles.Add(fileNameGenerated);
                        ++idx;
                    }
                }
                if (!success)
                {
                    BasicExceptionHandler.Instance.AddException("Failed to print tickets and/or receipts",
                                                                new ApplicationException());
                    return (false);
                }
            }

            return (true);
        }

        private bool executeVendorPurchase()
        {
            this.resetData();
            //Get the purchase object
            PurchaseVO purchaseObject = null;
            if (CollectionUtilities.isNotEmpty(GlobalDataAccessor.Instance.DesktopSession.Purchases))
            {
                purchaseObject = GlobalDataAccessor.Instance.DesktopSession.Purchases[0];
                //Analyze purchase to determine item types
                this.analyzeActivePurchase();
            }
            else
            {
                return false;
            }

            //Start transaction block
            if (!this.beginProcessTenderTransaction("Process Tender (Next Num block)"))
            {
                return (false);
            }

            //Get gun numbers if necessary

            if (purchaseObject != null && !this.retrieveGunNumbers(purchaseObject) && CollectionUtilities.isNotEmpty(this.gunItemNumberIndices))
            {
                GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                BasicExceptionHandler.Instance.AddException("Failed to retrieve gun numbers", new ApplicationException());
                return (false);
            }

            //Commit teller transaction
            if (!this.commitProcessTenderTransaction("Process Tender (Next Num block)"))
            {
                return (false);
            }

            //Start transaction block
            if (!this.beginProcessTenderTransaction("Process Tender (New Vendor Purchase block)"))
            {
                return (false);
            }

            //Perform inserts
            if (!this.performProcessTenderVendorPurchaseInserts())
            {
                GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                BasicExceptionHandler.Instance.AddException("Failed to insert vendor purchase data", new ApplicationException());
                return (false);
            }

            //Status the loan
            GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.LoanStatus = ProductStatus.PFI;

            //Add receipt details
            List<string> refDate = new List<string>();
            List<string> refNumber = new List<string>();
            List<string> refType = new List<string>();
            List<string> refEvent = new List<string>();
            List<string> refAmount = new List<string>();
            List<string> refStore = new List<string>();
            List<string> refTime = new List<string>();

            // ref date for purchase is the date made
            refDate.Add(ShopDateTime.Instance.ShopDate.FormatDate());

            //ref time for purchase is the current time
            refTime.Add(ShopDateTime.Instance.ShopTransactionTime);

            // ref number for vendor purchase is the new purchase number
            refNumber.Add(purchaseNumber.ToString());

            // ref type for purchase is 2
            refType.Add("2");

            // ref event for new vendor purchase is "PUR"
            refEvent.Add("PURV");

            // ref amount for new pawn loan is the loan amount
            refAmount.Add(GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.Amount.ToString());

            // ref store for purchase is the store the receipt was printed at
            refStore.Add(GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreNumber);

            //Update teller and insert receipt details info and inform user to disburse cash
            int rcptNumber;
            string errorCode;
            string errorText;
            if (!(PurchaseProcedures.InsertPurchasePaymentDetails(GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreNumber,
                                                                  GlobalDataAccessor.Instance.DesktopSession.CashDrawerName, GlobalDataAccessor.Instance.DesktopSession.FullUserName,
                                                                  ShopDateTime.Instance.ShopDate.ToShortDateString(), refNumber, refDate, refTime, refEvent, refAmount,
                                                                  ShopDateTime.Instance.ShopTransactionTime.ToString(), GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.PurchaseTenderType.ToString(),
                                                                  out rcptNumber, out errorCode, out errorText)))
            {
                GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                BasicExceptionHandler.Instance.AddException("Failed to update teller", new ApplicationException());
                return (false);
            }

            //Commit teller transaction
            if (!this.commitProcessTenderTransaction("Process Tender (End Purchases block)"))
            {
                return (false);
            }

            //Initiate PFI/Transfer on this vendor purchase

            PfiProcedures.ExecuteVendorPFI(Utilities.GetIntegerValue(purchaseNumber), GlobalDataAccessor.Instance.DesktopSession.ActivePurchase,
                                           GlobalDataAccessor.Instance.DesktopSession.ActiveVendor, out errorCode, out errorText);
            if (errorCode != "0")
                return false;

            //Perform print document
#if !__MULTI__
#endif

            GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.TicketNumber = Utilities.GetIntegerValue(purchaseNumber, 0);
            List<PurchaseVO> purchases = new List<PurchaseVO>(1) { GlobalDataAccessor.Instance.DesktopSession.ActivePurchase };
            //Print receipt
            ReceiptDetailsVO rdVo = new ReceiptDetailsVO();
            rdVo.RefNumbers = refNumber;
            rdVo.ReceiptNumber = rcptNumber.ToString();
            rdVo.RefEvents = refEvent;
            rdVo.RefStores = refStore;
            rdVo.RefTypes = refType;
            if (SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled)
            {
                bool rt = false;
                try
                {
                    rt = this.generatePurchaseReceipt(
                        ProcessTenderMode.VENDORPURCHASE,
                        GlobalDataAccessor.Instance.DesktopSession.UserName.ToLowerInvariant(),
                        GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer,
                        purchases,
                        GlobalDataAccessor.Instance.DesktopSession.ActiveVendor.Name,
                        rdVo);
                }
                catch (Exception)
                {
                    rt = false;
                }

                if (!rt)
                {
                    if (FileLogger.Instance.IsLogError)
                        FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Could not print receipt for purchase return");
                    BasicExceptionHandler.Instance.AddException("Could not print receipt for purchase return", new ApplicationException("Could not print receipt while servicing loans"));
                    return (false);
                }

                bool success = true;
                long idx = 0;
                foreach (var curPurch in purchases)
                {
                    string fileNameGenerated;
                    bool chkStorePurchaseDocument;
                    if (GlobalDataAccessor.Instance.CurrentSiteId.State.Equals(States.Indiana))
                    {
                        chkStorePurchaseDocument = PurchaseDocumentIndiana.GeneratePurchaseDocumentIndiana(
                            GlobalDataAccessor.Instance.DesktopSession, curPurch, out fileNameGenerated);
                    }
                    else
                    {
                        chkStorePurchaseDocument = PurchaseDocumentGenerator.GeneratePurchaseDocument(
                            GlobalDataAccessor.Instance.DesktopSession, curPurch, out fileNameGenerated);
                    }

                    if (!chkStorePurchaseDocument)
                    {
                        success = false;
                    }
                    else
                    {
                        this.ProcessedTickets.Add(new PairType<long, string>(idx, fileNameGenerated));
                        this.TicketFiles.Add(fileNameGenerated);
                        ++idx;
                    }
                }
                if (!success)
                {
                    BasicExceptionHandler.Instance.AddException("Failed to print tickets and/or receipts",
                                                                new ApplicationException());
                    return (false);
                }
            }

            return (true);
        }

        private bool performProcessTenderVendorPurchaseInserts()
        {
            DesktopSession cds = GlobalDataAccessor.Instance.DesktopSession;
            SiteId curSiteId = cds.CurrentSiteId;
            bool retVal = false;

            lock (mutexIntObj)
            {
                //Get purchase object
                PurchaseVO curPurchase = GlobalDataAccessor.Instance.DesktopSession.Purchases[0];
                if (curPurchase == null ||
                    CollectionUtilities.isEmpty(curPurchase.Items))
                {
                    BasicExceptionHandler.Instance.AddException("Active purchase object is invalid", new ApplicationException());
                    FileLogger.Instance.logMessage(LogLevel.FATAL, this, "Active purchase object is invalid");
                    return (false);
                }

                VendorVO vend = cds.ActiveVendor;
                OracleDataAccessor ods = GlobalDataAccessor.Instance.OracleDA;
                if (ods == null || ods.Initialized == false)
                {
                    BasicExceptionHandler.Instance.AddException("Oracle data accessor is invalid", new ApplicationException());
                    FileLogger.Instance.logMessage(LogLevel.FATAL, this, "Oracle data accessor is invalid");
                    return (false);
                }

                //Execute purchase header insert
                string errorCode;
                string errorText;
                int purchaseTktNumber;
                try
                {
                    string pfiDate = ShopDateTime.Instance.ShopDate.FormatDate();

                    decimal netAmount = 0.0M;
                    var expItems = (curPurchase.Items.Where(expItem => expItem.IsExpenseItem));
                    decimal expAmount = expItems.Sum(eItem => eItem.ItemAmount * eItem.Quantity);
                    if (expAmount > 0)
                        netAmount = curPurchase.Amount - expAmount;
                    else
                        netAmount = curPurchase.Amount;

                    retVal = PurchaseProcedures.InsertPurchaseRecord(PurchaseTypes.VENDOR.ToString(), GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreNumber,
                                                                     "", vend.ID, "V", pfiDate, curPurchase.Amount, netAmount, ProductStatus.PFI.ToString(),
                                                                     ShopDateTime.Instance.ShopDate.ToShortDateString(), ShopDateTime.Instance.ShopTransactionTime.ToString(),
                                                                     cds.UserName, cds.CashDrawerName, ShopDateTime.Instance.ShopDate.ToShortDateString(),
                                                                     curPurchase.Amount, curPurchase.Items.Count, curPurchase.PurchaseOrderNumber, "",
                                                                     curPurchase.Freight, curPurchase.SalesTax, curPurchase.ManualTicketNumber, curPurchase.MiscFlags,
                                                                     cds.TTyId, "", "", "", "", out purchaseTktNumber,
                                                                     out errorCode, out errorText);
                    this.retValues.Add(retVal);

                    if (!retVal)
                    {
                        this.errorCodes.Add(errorCode);
                        this.errorTexts.Add(errorText);
                        BasicExceptionHandler.Instance.AddException("ProcessTender.performProcessTenderInserts.InsertPurchaseRecord",
                                                                    new ApplicationException(errorText));
                        FileLogger.Instance.logMessage(LogLevel.FATAL, this, "Process tender encountered an error when inserting the purchase header record:" + errorText);

                        return false;
                    }
                    purchaseNumber = purchaseTktNumber;
                }
                catch (Exception eX)
                {
                    BasicExceptionHandler.Instance.AddException("ProcessTender.performProcessTenderInserts.InsertPurchaseRecord",
                                                                new ApplicationException("Exception thrown", eX));
                    FileLogger.Instance.logMessage(LogLevel.FATAL, this, "Process tender encountered an exception when inserting the purchase header record:" + eX.Message + ", " + eX.StackTrace);

                    return (false);
                }

                //Execute MDSE insert (also MDSEREV and OTHERDSC if necessary)
                if (CollectionUtilities.isEmpty(curPurchase.Items))
                {
                    FileLogger.Instance.logMessage(LogLevel.FATAL, this, "Process tender cannot print data with out any purchase items");
                    return (false);
                }

                int pawnItemIdx = 1;
                foreach (Item pItem in curPurchase.Items)
                {
                    bool isGunItem = false;
                    bool isJewelryItem = false;
                    for (int i = 0; i < pItem.Quantity; i++)
                    {
                        Int64 gunNumber = 0L;
                        gunNumber = GetGunNumberForPawnItem(curPurchase, pItem, pawnItemIdx - 1, this.gunItemNumberIndices, ref isGunItem);
                        if (!isGunItem)
                        {
                            if (pItem.IsJewelry)
                                isJewelryItem = true;
                        }

                        try
                        {
                            QuickCheck pItemQInfo = pItem.QuickInformation;
                            Int64[] primaryMasks = getMasks(pItem);
                            ProKnowMatch pKMatch = pItem.SelectedProKnowMatch;
                            ProKnowData pKData;
                            ProCallData pCData;
                            if (pKMatch != null)
                            {
                                pKData = pKMatch.selectedPKData;
                                pCData = pKMatch.proCallData;
                            }
                            else
                            {
                                pKData.RetailAmount = 0.0M;
                                pKData.LoanAmount = 0.0M;
                                pCData.NewRetail = 0.0M;
                                pKData.PurchaseAmount = 0.0M;
                            }

                            //Insert MDSE record for this pawn item
                            //Add as many items as there are quantity
                            string itemSerialNumber = "";

                            if (!isJewelryItem && pItem.Quantity > 1 && pItem.SerialNumber != null && pItem.SerialNumber.Count > 0)
                            {
                                itemSerialNumber = pItem.SerialNumber[i];
                                Item itemCopy = Utilities.CloneObject(pItem);
                                string sItemPrefix;
                                string sDescription;
                                //Get updated mdse description for each serial number
                                Item.RemoveSerialNumberFromDescription(ref itemCopy, out sItemPrefix, out sDescription);
                                pItem.Attributes = itemCopy.Attributes;
                                pItem.TicketDescription = sDescription;
                            }

                            bool curRetValue = ProcessTenderProcedures.ExecuteInsertMDSERecord(
                                pItem.mStore, pItem.mStore, pItem.mYear, purchaseTktNumber,
                                pItem.mDocType, pawnItemIdx, 0, pItem.CategoryCode,
                                "", pItem.ItemAmount,
                                (isGunItem ? gunNumber : 0L), pItemQInfo.Manufacturer,
                                pItemQInfo.Model, pItem.Quantity == 1 ? pItem.SerialNumber[0] : itemSerialNumber, pItemQInfo.Weight,
                                primaryMasks, pItem.TicketDescription, pItem.RetailPrice,
                                pItem.StorageFee, cds.UserName,
                                ShopDateTime.Instance.ShopDate.ToShortDateString(), ShopDateTime.Instance.ShopTransactionTime.ToString(),
                                "V", vend.ID, pItem.mDocType, pItem.IsExpenseItem ? "Y" : "", out errorCode, out errorText);
                            this.retValues.Add(curRetValue);
                            if (!curRetValue)
                            {
                                this.errorCodes.Add(errorCode);
                                this.errorTexts.Add(errorText);
                                return (false);
                            }
                            if (curRetValue)
                            {
                                curPurchase.GunNumbers.Add(isGunItem ? Utilities.GetIntegerValue(gunNumber, 0) : 0);
                                pItem.Icn = Utilities.IcnGenerator(pItem.mStore, pItem.mYear, purchaseTktNumber,
                                                                   pItem.mDocType, pawnItemIdx, 0);
                                pItem.mDocNumber = purchaseTktNumber;
                                pItem.mItemOrder = pawnItemIdx;

                                if (isGunItem)
                                {
                                    //Insert gun book record
                                    string truncGunType = pItemQInfo.GunType;
                                    if (!string.IsNullOrEmpty(truncGunType))
                                    {
                                        if (truncGunType.Length > 10)
                                        {
                                            truncGunType = truncGunType.Substring(0, 10);
                                        }
                                    }
                                    pItem.GunNumber = gunNumber;
                                    bool gunRetVal = ProcessTenderProcedures.ExecuteInsertGunBookRecord(
                                        curSiteId.TerminalId, ShopDateTime.Instance.ShopDate, cds.UserName,
                                        0L, curSiteId.StoreNumber, pItem.mStore, pItem.mYear,
                                        purchaseTktNumber, pItem.mDocType.ToString(), pawnItemIdx, 0,
                                        pItemQInfo.Manufacturer, pItemQInfo.Importer, pItem.Quantity == 1 ? pItem.SerialNumber[0] : itemSerialNumber,
                                        pItemQInfo.Caliber, truncGunType, pItemQInfo.Model,
                                        vend.Name, "",
                                        "",
                                        "",
                                        vend.Address1, vend.City, vend.State, !string.IsNullOrEmpty(vend.Zip4) ? vend.ZipCode + "-" + vend.Zip4 : vend.ZipCode, !string.IsNullOrEmpty(vend.Ffl) ? "FFL" : "",
                                        "", vend.Ffl,
                                        Utilities.GetDateTimeValue(ShopDateTime.Instance.ShopTransactionTime, DateTime.Now),
                                        cds.UserName, pItem.HasGunLock ? 1 : 0,
                                        ProductStatus.PFI.ToString(),
                                        out errorCode, out errorText);
                                    this.retValues.Add(gunRetVal);
                                    if (!gunRetVal)
                                    {
                                        this.errorCodes.Add(errorCode);
                                        this.errorTexts.Add(errorText);
                                    }
                                }

                                //Check answers on main pawn item for otherdsc insert
                                foreach (ItemAttribute iAttr in pItem.Attributes)
                                {
                                    if (iAttr.Answer.AnswerCode == OTHERDSC_CODE)
                                    {
                                        string attrValue = "";
                                        if (iAttr.Description == SERIALNUMBERATTRIBUTE)
                                            attrValue = pItem.Quantity == 1 ? pItem.SerialNumber[0] : itemSerialNumber;
                                        else
                                            attrValue = iAttr.Answer.AnswerText;

                                        bool otherDscVal = ProcessTenderProcedures.ExecuteInsertOtherDscRecord(
                                            GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                                            pItem.mStore, pItem.mYear, purchaseTktNumber,
                                            pItem.mDocType, pawnItemIdx, 0, iAttr.MaskOrder,
                                            attrValue, cds.UserName, out errorCode, out errorText);
                                        this.retValues.Add(otherDscVal);
                                        if (!otherDscVal)
                                        {
                                            this.errorCodes.Add(errorCode);
                                            this.errorTexts.Add(errorText);
                                        }
                                    }
                                }

                                //Check comment
                                if (!string.IsNullOrEmpty(pItem.Comment))
                                {
                                    bool otherDscVal = ProcessTenderProcedures.ExecuteInsertOtherDscRecord(
                                        GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                                        pItem.mStore, pItem.mYear, purchaseTktNumber,
                                        pItem.mDocType, pawnItemIdx, 0, OTHERDSC_CODE,
                                        pItem.Comment, cds.UserName, out errorCode, out errorText);
                                    this.retValues.Add(otherDscVal);
                                    if (!otherDscVal)
                                    {
                                        this.errorCodes.Add(errorCode);
                                        this.errorTexts.Add(errorText);
                                    }
                                }

                                //If jewelry item, execute the MDSE insert for all jewelry pieces
                                if (isJewelryItem)
                                {
                                    //Perform jewelry set inserts
                                    List<JewelrySet> jSets = pItem.Jewelry;

                                    if (jSets.Count > 0)
                                    {
                                        for (int subItemNum = 1; subItemNum <= jSets.Count; ++subItemNum)
                                        {
                                            JewelrySet curJSet = jSets[subItemNum - 1];
                                            if (curJSet == null)
                                            {
                                                continue;
                                            }

                                            Int64[] curJSetMasks = getMasks(curJSet);
                                            //Fix to prevent additional mdse record for jewelry with only one sub item
                                            if (curJSetMasks.Length > 0 && curJSetMasks[0] == 0)
                                                continue;
                                            bool jRetVal = false;
                                            jRetVal = ProcessTenderProcedures.ExecuteInsertMDSERecord(
                                                pItem.mStore, pItem.mStore, pItem.mYear, purchaseTktNumber,
                                                pItem.mDocType, pawnItemIdx, subItemNum, pItem.CategoryCode,
                                                "", pItem.ItemAmount,
                                                0L, pItemQInfo.Manufacturer,
                                                pItemQInfo.Model, itemSerialNumber, pItemQInfo.Weight,
                                                curJSetMasks, curJSet.TicketDescription,
                                                Utilities.GetDecimalValue(pKData.RetailAmount, 0.0M),
                                                pItem.StorageFee, cds.UserName,
                                                ShopDateTime.Instance.ShopDate.ToShortDateString(), ShopDateTime.Instance.ShopTransactionTime.ToString(),
                                                "V", vend.ID, pItem.mDocType, pItem.IsExpenseItem ? "Y" : "", out errorCode, out errorText);
                                            this.retValues.Add(jRetVal);
                                            if (!jRetVal)
                                            {
                                                this.errorCodes.Add(errorCode);
                                                this.errorTexts.Add(errorText);
                                            }

                                            foreach (ItemAttribute iAttr in curJSet.ItemAttributeList)
                                            {
                                                if (iAttr.Answer.AnswerCode == OTHERDSC_CODE)
                                                {
                                                    string attrValue = "";
                                                    if (iAttr.Description == SERIALNUMBERATTRIBUTE)
                                                        attrValue = itemSerialNumber;
                                                    else
                                                        attrValue = iAttr.Answer.AnswerText;
                                                    bool otherJDscVal = ProcessTenderProcedures.ExecuteInsertOtherDscRecord(
                                                        GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                                                        pItem.mStore, pItem.mYear, purchaseTktNumber,
                                                        pItem.mDocType, pawnItemIdx, subItemNum, iAttr.MaskOrder,
                                                        attrValue, cds.UserName, out errorCode, out errorText);
                                                    this.retValues.Add(otherJDscVal);
                                                    if (!otherJDscVal)
                                                    {
                                                        this.errorCodes.Add(errorCode);
                                                        this.errorTexts.Add(errorText);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                //Insert merchandise revision record for purchase price
                                if (!pItem.IsExpenseItem)
                                {
                                    bool mdseRevVal = MerchandiseProcedures.InsertMerchandiseRevision(GlobalDataAccessor.Instance.DesktopSession,
                                        GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreNumber,
                                        pItem.mYear,
                                        purchaseTktNumber,
                                        pItem.mDocType,
                                        pawnItemIdx,
                                        0,
                                        pItem.mStore,
                                        purchaseTktNumber.ToString(),
                                        pItem.mDocType,
                                        PURCHASEMDSEREVMRDESC,
                                        pItem.ItemAmount * pItem.QuickInformation.Quantity,
                                        VENDPURCHASEMDSEREVMRTYPE,
                                        "I",
                                        PURCHASEMDSEREVMRDESC,
                                        cds.UserName,
                                        out errorCode,
                                        out errorText);
                                    if (!mdseRevVal)
                                    {
                                        this.errorCodes.Add(errorCode);
                                        this.errorTexts.Add(errorText);
                                    }
                                }
                            }

                            //Madhu Veldanda 01/17/2011 fix to address BZ defect 78
                            if (!pItem.IsExpenseItem)
                                GlobalDataAccessor.Instance.DesktopSession.PrintTags(pItem, CurrentContext.VENDOR_PURCHASE);

                            pawnItemIdx++;
                        }
                        catch (Exception eX)
                        {
                            this.errorTexts.Add(eX.Message);
                        }
                    }
                }
                var sB = new StringBuilder("Massive failure listing");
                if (CollectionUtilities.isNotEmpty(this.errorCodes) && CollectionUtilities.isNotEmpty(this.errorTexts))
                {
                    int i = 0;
                    foreach (string s in this.errorCodes)
                    {
                        sB.AppendFormat(
                            "{0} - ErrorCode = {1} ErrorText = {2}",
                            i,
                            s,
                            this.errorTexts[i]);
                    }
                    FileLogger.Instance.logMessage(LogLevel.FATAL, this, "Beyond fatal insert statement failure...");
                    BasicExceptionHandler.Instance.AddException("Error inserting purchase data", new ApplicationException("Massive Fatal Exception"));
                    return (false);
                }
            }
            return (true);
        }

        private bool performReturnPurchaseInserts()
        {
            DesktopSession cds = GlobalDataAccessor.Instance.DesktopSession;
            bool retVal = false;
            lock (mutexIntObj)
            {
                //Get purchase object
                PurchaseVO curPurchase = GlobalDataAccessor.Instance.DesktopSession.Purchases[0];
                if (curPurchase == null ||
                    CollectionUtilities.isEmpty(curPurchase.Items))
                {
                    BasicExceptionHandler.Instance.AddException("Active purchase object is invalid", new ApplicationException());
                    FileLogger.Instance.logMessage(LogLevel.FATAL, this, "Active purchase object is invalid");
                    return (false);
                }
                string custDispIdNum = "";
                string custDispIdType = "";
                string custDispIDCode = "";

                CustomerVO cust = cds.ActiveCustomer;
                VendorVO vend = cds.ActiveVendor;

                if (cust != null && !string.IsNullOrEmpty(cust.CustomerNumber))
                {
                    // EDW - CR#15278
                    KeyValuePair<string, string> lastIdUsed = GlobalDataAccessor.Instance.DesktopSession.LastIdUsed;
                    IdentificationVO id = cust.getIdByTypeandIssuer(lastIdUsed.Key, lastIdUsed.Value) ?? cust.getFirstIdentity();


                    if (id != null)
                    {
                        custDispIDCode = id.IdIssuerCode;
                        custDispIdNum = id.IdValue;
                        custDispIdType = id.IdType;
                    }
                }
                else
                {
                    if (vend != null && !string.IsNullOrEmpty(vend.Name))
                    {
                        custDispIdType = CustomerIdTypes.FFL.ToString();
                        custDispIdNum = vend.Ffl;
                        custDispIDCode = "";
                    }
                }

                OracleDataAccessor ods = GlobalDataAccessor.Instance.OracleDA;
                if (ods == null || ods.Initialized == false)
                {
                    BasicExceptionHandler.Instance.AddException("Oracle data accessor is invalid", new ApplicationException());
                    FileLogger.Instance.logMessage(LogLevel.FATAL, this, "Oracle data accessor is invalid");
                    return (false);
                }

                //Execute purchase return insert
                try
                {
                    string errorCode;
                    string errorText;
                    int purchaseTktNumber;
                    decimal returnAmount = 0.0M;
                    List<string> returnICN = new List<string>();
                    returnICN = (from item in curPurchase.Items
                                 select item.Icn).ToList();
                    returnAmount = (from item in curPurchase.Items select item.ItemAmount).Sum();
                    retVal = PurchaseProcedures.InsertReturnRecord("CUSTOMER", GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreNumber,
                                                                   cust.CustomerNumber, curPurchase.EntityNumber, curPurchase.EntityType, ShopDateTime.Instance.ShopDate.FormatDate(), returnAmount, ProductStatus.RET.ToString(),
                                                                   ShopDateTime.Instance.ShopDate.ToShortDateString(), ShopDateTime.Instance.ShopTransactionTime.ToString(),
                                                                   cds.UserName, cds.CashDrawerName, ShopDateTime.Instance.ShopDate.ToShortDateString(),
                                                                   returnAmount, curPurchase.Items.Count, curPurchase.PurchaseOrderNumber, curPurchase.TicketNumber.ToString(),
                                                                   curPurchase.Freight, curPurchase.SalesTax, curPurchase.ManualTicketNumber, curPurchase.MiscFlags,
                                                                   cds.TTyId, "", "", "", returnICN, curPurchase.ReturnReason, GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.BackgroundCheckRefNumber,
                                                                   custDispIdNum, custDispIdType, custDispIDCode,
                                                                   out purchaseTktNumber, out errorCode, out errorText);
                    this.retValues.Add(retVal);

                    if (!retVal)
                    {
                        this.errorCodes.Add(errorCode);
                        this.errorTexts.Add(errorText);
                        BasicExceptionHandler.Instance.AddException("ProcessTender.performProcessTenderInserts.InsertReturnRecord",
                                                                    new ApplicationException(errorText));
                        FileLogger.Instance.logMessage(LogLevel.FATAL, this, "Process tender encountered an error when inserting the purchase header record:" + errorText);

                        return false;
                    }
                    int pawnItemIdx = 1;
                    foreach (Item pItem in curPurchase.Items)
                    {
                        //Insert merchandise revision record
                        MerchandiseProcedures.InsertMerchandiseRevision(GlobalDataAccessor.Instance.DesktopSession,
                            GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreNumber,
                            pItem.mYear,
                            curPurchase.TicketNumber,
                            pItem.mDocType,
                            pItem.mItemOrder,
                            0,
                            pItem.mStore,
                            purchaseTktNumber.ToString(),
                            pItem.mDocType,
                            "",
                            pItem.ItemAmount,
                            RETURNMDSEREVMRTYPE,
                            "",
                            pItem.ItemAmount.ToString(),
                            cds.UserName,
                            out errorCode,
                            out errorText);
                        pawnItemIdx++;
                    }
                    this.purchaseNumber = purchaseTktNumber;
                    return true;
                }
                catch (Exception eX)
                {
                    BasicExceptionHandler.Instance.AddException("ProcessTender.performProcessTenderInserts.InsertReturnRecord",
                                                                new ApplicationException("Exception thrown", eX));
                    FileLogger.Instance.logMessage(LogLevel.FATAL, this, "Process tender encountered an exception when inserting the record for purchase return:" + eX.Message + ", " + eX.StackTrace);

                    return (false);
                }
            }
        }

        private void analyzeActivePurchase()
        {
            lock (mutexIntObj)
            {
                PurchaseVO purchObj;
                purchObj = GlobalDataAccessor.Instance.DesktopSession.ActivePurchase;
                if (purchObj == null || CollectionUtilities.isEmpty(purchObj.Items))
                {
                    return;
                }
                this.gunItemNumberIndices.Clear();
                for (int i = 0; i < purchObj.Items.Count; ++i)
                {
                    Item pItem = purchObj.Items[i];
                    if (pItem == null)
                    {
                        continue;
                    }
                    for (int j = 1; j <= pItem.Quantity; j++)
                    {
                        QuickCheck pItemQc = pItem.QuickInformation;
                        this.gunItemNumberIndices.Add(new PairType<int, long>(-1, -1L));

                        if (pItem.IsJewelry)
                        {
                            this.jewelryItemIndices.Add(new PairType<int, long>(i, -1L));
                            continue;
                        }
                        if (!pItem.IsGun)
                            continue;
                        this.gunItemNumberIndices.Last().Left = j;
                        this.gunItemNumberIndices.Last().Right = pItem.GunNumber;
                    }
                }
            }
        }
    }

    public class ReceiptItems
    {
        public string ICN { get; set; }

        public string TicketAmount { get; set; }

        public string DescriptionData { get; set; }

        public string TicketNumber { get; set; }

        public int Quantity { get; set; }

        public decimal ItemAmount { get; set; }

        public decimal TotalItemAmount { get; set; }

        public decimal CouponPercentage { get; set; }

        public decimal CouponAmount { get; set; }

        public decimal ProratedCouponAmount { get; set; }

        public string TranCouponCode { get; set; }

        public string CouponCode { get; set; }

        public decimal ItemLineAmount { get; set; }

        public string Comments { get; set; }
    }
}
