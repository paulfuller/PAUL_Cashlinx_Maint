using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CashlinxDesktop.Desktop;
using CashlinxDesktop.DesktopForms;
using CashlinxDesktop.DesktopForms.Pawn.Customer;
using CashlinxDesktop.DesktopProcedures;
using CommonUI.DesktopProcedures;
using PawnDataAccess.Oracle;
using PawnObjects.Pawn;
using PawnObjects.VO.Business;
using PawnObjects.VO.Customer;
using PawnUtilities.Collection;
using PawnUtilities.Shared;
using PawnUtilities.Type;

namespace CashlinxDesktopLoadTester.Scenarios.Impl
{
    public class ExecuteNewLoanExistCustomer : LoadTestScenario
    {
        public static readonly string NAME = "ExecuteNewLoanExistCustomer";

        private ProcessTenderController tender;
        private CashlinxDesktopSession cds;
        private TupleType<int, LoadTestInputVO, List<TupleType<int, string, double>>> input;
        //Pawn loan and underwrite objects
        //private PawnLoan pawnLoan;
        //private UnderwritePawnLoanVO pawnUnderwrite;
        int[] genCategories = {
                                      2222, 7110, 2221
                                  };
        int[] jewCategories = {
                                      1110, 1340, 1330
                                  };
        int[] gunCategories = {
                                      4320, 4110, 4120
                                  };


        public ExecuteNewLoanExistCustomer(
            ProcessTenderController pTender,
            CashlinxDesktopSession c,
            TupleType<int, LoadTestInputVO, List<TupleType<int, string, double>>> tInput) : base(NAME)
        {
            this.tender = pTender;
            this.cds = c;
            this.input = tInput;
        }

        public TupleType<int, LoadTestInputVO, List<TupleType<int, string, double>>> DataInput
        {
            get
            {
                return (this.input);
            }
        }

        public void execute()
        {
            //Perform lookup customer            
            /*this.AddLoadForm("LookupCustomer", new LookupCustomer(), 1, 
                new Dictionary<string, string>()
                {
                    {
                        "lookupCustomerLastName",
                        this.input.LastName
                    },
                    {
                        "lookupCustomerFirstName",
                        this.input.FirstName
                    }
                },
                new Dictionary<string, TupleType<Control, ControlType, ControlTriggerType>>()
                {
                    {
                        "lookupCustomerFindButton",
                        new TupleType<Control, ControlType, ControlTriggerType>(null, ControlType.BUTTON, ControlTriggerType.CLICK)
                    
                    }
                });
            this.SetFieldsOnForm("LookupCustomer");
            this.TriggerControlOnForm("LookupCustomer", "lookupCustomerFindButton");*/
            //Get the site id
            SiteId curSite = cds.CurrentSiteId;

            //Perform customer lookup
            DateTime dtFullStart = DateTime.Now;
            DateTime custLookupStart = DateTime.Now;
            DataTable customerTable, customerIds, customerContacts, customerAddress, customerEmails, customerNotes, customerStoreCredit;
            string errorCode, errorMesg;
            var dbProcedures = new CustomerDBProcedures(this.cds);
            //CustomerProcedures custProcedures = new CustomerProcedures();
            try
            {
                bool retVal = dbProcedures.ExecuteLookupCustomer(this.input.Mid.FirstName, this.input.Mid.LastName,
                                                           "", "", "", "", "", "", "", "", "", "", out customerTable,
                                                           out customerIds, out customerContacts, out customerAddress,
                                                           out customerEmails, out customerNotes, out customerStoreCredit, out errorCode,
                                                           out errorMesg);
                if (!retVal || customerTable == null || customerTable.Rows == null || customerTable.Rows.Count <= 0)
                {
                    input.Right.Add(new TupleType<int, string, double>(input.Left, "CUSTOMER", 0.0d));
                    input.Right.Add(new TupleType<int, string, double>(input.Left, "PAWNAPPL", 0.0d));
                    input.Right.Add(new TupleType<int, string, double>(input.Left, "PAWNTEND", 0.0d));
                    input.Right.Add(new TupleType<int, string, double>(input.Left, "TOTALNEW", 0.0d));
                    return;
                }
            }
            catch (Exception eX)
            {
                input.Right.Add(new TupleType<int, string, double>(input.Left, "CUSTOMER", 0.0d));
                input.Right.Add(new TupleType<int, string, double>(input.Left, "PAWNAPPL", 0.0d));
                input.Right.Add(new TupleType<int, string, double>(input.Left, "PAWNTEND", 0.0d));
                input.Right.Add(new TupleType<int, string, double>(input.Left, "TOTALNEW", 0.0d));
                return;
            }
            DateTime custLookupStop = DateTime.Now;
            DateTime managePawnAppStart = DateTime.Now;
            //Choose first customer returned
            DataRow chosenCust = customerTable.Rows[0];
            var partyId = (string)chosenCust["party_id"];
            CustomerVO cust = null;
            try
            {
                cust = CustomerProcedures.getCustomerDataInObject(partyId, customerIds, customerContacts,
                                                                  customerAddress, customerEmails, customerNotes,
                                                                  customerStoreCredit, chosenCust);
            }
            catch (Exception eX)
            {
                TimeSpan custLookupTimeEx = custLookupStop - custLookupStart;
                input.Right.Add(new TupleType<int, string, double>(input.Left, "CUSTOMER", custLookupTimeEx.TotalSeconds));
                input.Right.Add(new TupleType<int, string, double>(input.Left, "PAWNAPPL", 0.0d));
                input.Right.Add(new TupleType<int, string, double>(input.Left, "PAWNTEND", 0.0d));
                input.Right.Add(new TupleType<int, string, double>(input.Left, "TOTALNEW", 0.0d));
                return;                
            }
            if (cust == null)
            {
                TimeSpan custLookupTimeEx = custLookupStop - custLookupStart;
                input.Right.Add(new TupleType<int, string, double>(input.Left, "CUSTOMER", custLookupTimeEx.TotalSeconds));
                input.Right.Add(new TupleType<int, string, double>(input.Left, "PAWNAPPL", 0.0d));
                input.Right.Add(new TupleType<int, string, double>(input.Left, "PAWNTEND", 0.0d));
                input.Right.Add(new TupleType<int, string, double>(input.Left, "TOTALNEW", 0.0d));
                return;
            }

            //Create pawn application
            cds.ActiveCustomer = cust;
            string pawnAppId = "1";
            try
            {
                IdentificationVO curId = cust.getFirstIdentity();
                bool createdPawnApp = dbProcedures.InsertPawnApplication(
                    cust.CustomerNumber,
                    curSite.StoreNumber,
                    " ",
                    " ",
                    curId.IdType,
                    curId.IdValue,
                    curId.IdIssuer,
                    curId.IdExpiryData.Date.ToShortDateString(),
                    cds.UserName,
                    out pawnAppId,
                    out errorCode,
                    out errorMesg);

                if (!createdPawnApp || string.IsNullOrEmpty(pawnAppId))
                {
                    TimeSpan custLookupTimeEx = custLookupStop - custLookupStart;
                    input.Right.Add(new TupleType<int, string, double>(input.Left, "CUSTOMER", custLookupTimeEx.TotalSeconds));
                    input.Right.Add(new TupleType<int, string, double>(input.Left, "PAWNAPPL", 0.0d));
                    input.Right.Add(new TupleType<int, string, double>(input.Left, "PAWNTEND", 0.0d));
                    input.Right.Add(new TupleType<int, string, double>(input.Left, "TOTALNEW", 0.0d));
                    return;
                }
            }
            catch (Exception eX)
            {
                TimeSpan custLookupTimeEx = custLookupStop - custLookupStart;
                input.Right.Add(new TupleType<int, string, double>(input.Left, "CUSTOMER", custLookupTimeEx.TotalSeconds));
                input.Right.Add(new TupleType<int, string, double>(input.Left, "PAWNAPPL", 0.0d));
                input.Right.Add(new TupleType<int, string, double>(input.Left, "PAWNTEND", 0.0d));
                input.Right.Add(new TupleType<int, string, double>(input.Left, "TOTALNEW", 0.0d));
                return;                
            }
            DateTime managePawnAppStop = DateTime.Now;

            //Start building pawn loan
            DateTime pawnLoanTenderStart = DateTime.Now;
            PawnLoan curLoan = cds.ActivePawnLoan;
            curLoan.OrgShopNumber = curSite.StoreNumber;
            curLoan.PawnAppId = pawnAppId;
            cds.CurPawnAppId = pawnAppId;
            cds.Clothing = " ";
            cds.TTyId = "1";
            //Choose what will be on the loan (max 3) (one of each, or a combo of general, jewelry, and/or gun)
            var randClass = new Random();
            int numberOfLoanItems = 1 + (int) (randClass.NextDouble() * 2.0d);
            int randGenCat = 1 + (int)(randClass.NextDouble() * 2.0d);
            int randJewCat = 1 + (int)(randClass.NextDouble() * 2.0d);
            int randGunCat = 1 + (int)(randClass.NextDouble() * 2.0d);



            try
            {
                //We have one loan item
                int finalGenCat = randGenCat - 1;
                if (finalGenCat < 0 || finalGenCat > 2)
                    finalGenCat = 0;
                int finalJewCat = randJewCat - 1;
                if (randJewCat < 0 || randJewCat > 2)
                    randJewCat = 0;
                int finalGunCat = randGunCat - 1;
                if (randGunCat < 0 || randGunCat > 2)
                    randGunCat = 0;

                curLoan.Fees = new List<Fee>(numberOfLoanItems);
                if (numberOfLoanItems == 1)
                {
                    int catG = genCategories[randGenCat - 1];
                    var descMerc = new DescribedMerchandise(catG);
                    curLoan.Items.Add(descMerc.SelectedPawnItem);
                    curLoan.Items[0].TicketDescription = "TestGenDesc";
                    Item firstItem = curLoan.Items[0];
                    firstItem.mStore = 6016;
                    firstItem.CategoryCode = genCategories[randGenCat - 1];

                }
                //We have two loan items
                else if (numberOfLoanItems == 2)
                {
                    int catGenG = genCategories[randGenCat - 1];
                    int catJewG = jewCategories[randJewCat - 1];
                    var descGenMerc = new DescribedMerchandise(catGenG);
                    var descJewMerc = new DescribedMerchandise(catJewG);
                    curLoan.Items.Add(descGenMerc.SelectedPawnItem);
                    curLoan.Items.Add(descJewMerc.SelectedPawnItem);
                    curLoan.Items[0].TicketDescription = "TestGenDesc";
                    curLoan.Items[1].TicketDescription = "TestJewDesc";
                    Item firstItem = curLoan.Items[0];
                    firstItem.mStore = 6016;
                    firstItem.CategoryCode = genCategories[randGenCat - 1];
                    Item secItem = curLoan.Items[1];
                    secItem.mStore = 6016;
                    secItem.CategoryCode = jewCategories[randJewCat - 1];
                }
                //We have 3 loan items
                else
                {
                    int catGenG = genCategories[randGenCat - 1];
                    int catJewG = jewCategories[randJewCat - 1];
                    int catGunG = gunCategories[randGunCat - 1];
                    var descGenMerc = new DescribedMerchandise(catGenG);
                    var descJewMerc = new DescribedMerchandise(catJewG);
                    var descGunMerc = new DescribedMerchandise(catGunG);
                    curLoan.Items.Add(descGenMerc.SelectedPawnItem);
                    curLoan.Items.Add(descJewMerc.SelectedPawnItem);
                    curLoan.Items.Add(descGunMerc.SelectedPawnItem);
                    curLoan.Items[0].TicketDescription = "TestGenDesc";
                    curLoan.Items[1].TicketDescription = "TestJewDesc";
                    curLoan.Items[2].TicketDescription = "GUNTestGunDesc";
                    Item firstItem = curLoan.Items[0];
                    firstItem.mStore = 6016;
                    firstItem.CategoryCode = genCategories[randGenCat - 1];
                    Item secItem = curLoan.Items[1];
                    secItem.mStore = 6016;
                    secItem.CategoryCode = jewCategories[randJewCat - 1];
                    Item thdItem = curLoan.Items[2];
                    thdItem.mStore = 6016;
                    thdItem.CategoryCode = gunCategories[randGunCat - 1];
                }
            }
            catch (Exception eX)
            {
                TimeSpan custLookupTimeEx = custLookupStop - custLookupStart;
                TimeSpan mngAppTimeEx = managePawnAppStop - managePawnAppStart;
                input.Right.Add(new TupleType<int, string, double>(input.Left, "CUSTOMER", custLookupTimeEx.TotalSeconds));
                input.Right.Add(new TupleType<int, string, double>(input.Left, "PAWNAPPL", mngAppTimeEx.TotalSeconds));
                input.Right.Add(new TupleType<int, string, double>(input.Left, "PAWNTEND", 0.0d));
                input.Right.Add(new TupleType<int, string, double>(input.Left, "TOTALNEW", 0.0d));
                return;
            }

            //Call underwrite
            try
            {
                //Set loan amount prior to underwriting
                decimal tryAmt;
                curLoan.Amount = 60;
                if (Decimal.TryParse(""+(60 * (1 + (uint)Math.Floor(randClass.NextDouble() * 4))), out tryAmt))
                {
                    curLoan.Amount = tryAmt;
                }
                var upwUtil = new UnderwritePawnLoanUtility(this.cds);
                upwUtil.RunUWP(curSite);
                curLoan.ObjectUnderwritePawnLoanVO = upwUtil.PawnLoanVO;
                //Call process tender execute
                this.tender.ExecuteProcessTender(
                    ProcessTenderController.ProcessTenderMode.NEWLOAN);

                //cleanup once done
                this.cds.ClearCustomerList();
                this.cds.ClearPawnLoan();
                curLoan = null;
            }
            catch (Exception eX)
            {
                //MessageBox.Show("Exception thrown during process tender: " + eX.Message + ", " + eX.StackTrace);
                TimeSpan custLookupTimeEx = custLookupStop - custLookupStart;
                TimeSpan mngAppTimeEx = managePawnAppStop - managePawnAppStart;
                input.Right.Add(new TupleType<int, string, double>(input.Left, "CUSTOMER", custLookupTimeEx.TotalSeconds));
                input.Right.Add(new TupleType<int, string, double>(input.Left, "PAWNAPPL", mngAppTimeEx.TotalSeconds));
                input.Right.Add(new TupleType<int, string, double>(input.Left, "PAWNTEND", 0.0d));
                input.Right.Add(new TupleType<int, string, double>(input.Left, "TOTALNEW", 0.0d));
                return;
            }
            DateTime pawnLoanTenderStop = DateTime.Now;

            DateTime dtFullFinish = DateTime.Now;
            //Thread finished

            //Compute times
            TimeSpan finishTime = dtFullFinish - dtFullStart;
            TimeSpan custLookupTime = custLookupStop - custLookupStart;
            TimeSpan mngAppTime = managePawnAppStop - managePawnAppStart;
            TimeSpan pwnLoanTender = pawnLoanTenderStop - pawnLoanTenderStart;
            input.Right.Add(new TupleType<int, string, double>(input.Left, "CUSTOMER", custLookupTime.TotalSeconds));
            input.Right.Add(new TupleType<int, string, double>(input.Left, "PAWNAPPL", mngAppTime.TotalSeconds));
            input.Right.Add(new TupleType<int, string, double>(input.Left, "PAWNTEND", pwnLoanTender.TotalSeconds));
            input.Right.Add(new TupleType<int, string, double>(input.Left, "TOTALNEW", finishTime.TotalSeconds));
        }
    }
}
