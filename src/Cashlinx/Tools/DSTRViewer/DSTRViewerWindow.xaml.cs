using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Common.Controllers.Application;
using Common.Controllers.Database.Couch;
using Common.Controllers.Database.Couch.Impl;
using Common.Controllers.Database.DataAccessLayer;
using Common.Libraries.Forms;
using Common.Libraries.Objects.Authorization;
using Common.Libraries.Objects.Config;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Collection;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.String;
using Common.Libraries.Utility.Type;
using Document = Common.Libraries.Objects.Doc.Document;
using MessageBox = System.Windows.MessageBox;

namespace DSTRViewer
{
    /// <summary>
    /// Interaction logic for DSTRViewerWindow.xaml
    /// </summary>
    public partial class DSTRViewerWindow : Window
    {
//        public const string PROD_ENV = "CLXP";
        private const string TIME_FILE = ".donotdelete";
        private const int MIN_REJECT_LEN = 3;
        public EncryptedConfigContainer EncryptedConfig { get; set; }
        public DatabaseServiceVO CouchServer { get; set; }
        public DatabaseServiceVO DatabaseServer { get; set; }
        public PawnSecVO PawnSecData { get; set; }
        public Credentials PwnSecCred { set; get; }
        public DataAccessTools PwnSecDataTools { set; get; }
        public Credentials CshLnxCred { set; get; }
        public DataAccessTools CshLnxDataTools { set; get; }
        public bool Finished { set; get; }
        private Dictionary<long, List<PairType<string, Document>>> dateStorageMap;
        private List<string> storeList; 
        private DataReturnSet storeDataSet;
        private DataReturnSet dstrStorageData;
        private DateTime selectedDate;
        private Document selectedDocument;
        private SecuredCouchConnector couchConnector;
        //private Cookie couchSessionCookie;
        private string selectedStore;
        private string curEnvString;
        private string curUserName;
        private bool initialized;

        private void checkInitialized()
        {
            initialized = true;
            if (string.IsNullOrEmpty(curEnvString) ||
                string.IsNullOrEmpty(curUserName))
            {
                initialized = false;
            }
            if (EncryptedConfig == null) initialized = false;
            if (initialized && CouchServer == null) initialized = false;
            if (initialized && DatabaseServer == null) initialized = false;
            if (initialized && PawnSecData == null) initialized = false;
            if (initialized && PwnSecCred == null) initialized = false;
            if (initialized && PwnSecDataTools == null) initialized = false;
            if (initialized && CshLnxCred == null) initialized = false;
            if (initialized && CshLnxDataTools == null) initialized = false;
        }

        public DSTRViewerWindow(string envStr, string usrName)
        {
            InitializeComponent();
            this.initialized = false;
            this.curEnvString = envStr;
            this.curUserName = usrName;
            this.selectedStore = string.Empty;
            this.dateStorageMap = new Dictionary<long, List<PairType<string, Document>>>();
            this.storeList = new List<string>();
            this.selectedDate = DateTime.MinValue;
            this.couchConnector = null;
            this.selectedDocument = null;            
            this.Finished = false;
        }
        
        private void showError(string errTxt)
        {
            if (FileLogger.Instance.IsLogError)
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, errTxt);

            }
            MessageBox.Show(errTxt, "FAILURE");
        }

        private string getOracleError(DataAccessTools dTools)
        {
            if (dTools == null) return ("DataAccessTools object is null");
            if (dTools.OracleDA == null) return ("OracleDataAccessor object is null");
            if (!dTools.OracleDA.Initialized) return ("OracleDataAccessor's data connection is not initialized");
            if (!string.IsNullOrEmpty(dTools.OracleDA.ErrorCode) &&
                !string.IsNullOrEmpty(dTools.OracleDA.ErrorDescription))
            {
                return (string.Format(
                    "OracleDataAccessor error: Code={0} Description={1}",
                    dTools.OracleDA.ErrorCode,
                    dTools.OracleDA.ErrorDescription));
            }
            return ("DataAccessTools operation failed");
        }

        private void storeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.storeComboBox.SelectedIndex > 0)
            {
                var comboItem = this.storeComboBox.Items[this.storeComboBox.SelectedIndex];
                if (comboItem != null)
                {
                    var itemStr = comboItem.ToString();
                    if (!string.IsNullOrEmpty(itemStr))
                    {
                        var colonIdx = itemStr.LastIndexOf(@":", StringComparison.Ordinal);
                        if (colonIdx > -1)
                        {
                            this.selectedStore = itemStr.Substring(colonIdx + 1).TrimStart(' ');
                            if (!string.IsNullOrEmpty(this.selectedStore))
                            {
                                this.submitStoreButton.IsEnabled = true;
                                return;
                            }
                        }
                    }
                }
            }
            if (this.submitStoreButton != null)
            {
                this.submitStoreButton.IsEnabled = false;
            }
        }

        private void DSTRViewerWindowForm_Loaded(object sender, RoutedEventArgs e)
        {
            TupleType<long, long, string> timeData;
            if (IsRestrictedEnvironment())
            {
                var timeLimitValue = DSTRViewer.Properties.Settings.Default.timeLimiter;
                if (string.IsNullOrEmpty(timeLimitValue)) timeLimitValue = "300000";
                var timeLimitAct = Utilities.GetLongValue(timeLimitValue, 300000L);
                //Convert from milliseconds to minutes
                timeLimitAct = timeLimitAct / 1000 / 60;

                var res = MessageBox.Show(
                    "*************** WARNING ***************" + Environment.NewLine +
                    "  You are accessing a live production" + Environment.NewLine +
                    "  system. Your activity is being     " + Environment.NewLine +
                    "  logged and your access will be     " + Environment.NewLine +
                    "  limited to one document retrieval  " + Environment.NewLine +
                    "  every " + timeLimitAct + " minutes. If you do not  " + Environment.NewLine +
                    "  agree with this policy, click      " + Environment.NewLine +
                    "  Cancel. By clicking OK you are     " + Environment.NewLine +
                    "  bound to this time limit policy.   ",
                    "Time Limit Policy For Cashlinx Production Systems",
                    MessageBoxButton.OKCancel, MessageBoxImage.Stop);
                if (res == MessageBoxResult.Cancel)
                {
                    if (FileLogger.Instance.IsLogFatal)
                    {
                        FileLogger.Instance.logMessage(LogLevel.FATAL, this, "User {0} clicked cancel when presented with the production time limit policy.", this.curUserName);
                    }

                    MessageBox.Show("*** Application Exiting ***");
                    Application.Current.Shutdown();
                    this.Close();
                    return;
                }
                else if (res == MessageBoxResult.OK)
                {
                    try
                    {
                        //Determine if the file exists
                        //                        var filesExisting = Directory.GetFiles(".", TIME_FILE, SearchOption.TopDirectoryOnly);
                        //                        var fileExists = filesExisting.Length > 0;
                        if (TimeFileExists() && WithinTimeLimit(out timeData))
                        {

                            //Warn about time limit, then exit
                            MessageBox.Show(
                                "You are still in the waiting period to fetch a document from production." + Environment.NewLine +                                
                                "Exiting the application.",
                                "*** TIME LIMIT WARNING ***",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                            //Exit the app
                            Application.Current.Shutdown();
                            this.Close();
                            return;
                        }
                    }
                    catch (Exception eX)
                    {
                        if (FileLogger.Instance.IsLogError)
                        {
                            FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Could not read time limit file data: {0}", eX.Message);
                        }
                    }
                }
            }
            checkInitialized();
            if (!initialized)
            {
                MessageBox.Show("DSTR Viewer is not initialized");
                this.Close();
            }


            //Check file time - we do not want to hit production if the time limit is still in effect
            if (TimeFileExists() && WithinTimeLimit(out timeData))
            {
                var diffTime = timeData.Right;
                MessageBox.Show("You are still within the time restriction limit.  The application will now close.", "*** TIME LIMIT WARNING ***");
                Application.Current.Shutdown();
                this.Close();
                return;
            }

            //Populate store drop down
            DataReturnSet dSet;
            string errTxt;
            var dTools = this.CshLnxDataTools;
            if (!DataAccessService.ExecuteQuery(
                false,
                "select storenumber from ccsowner.store where alias_id is not null and pawn_conversion is not null",
                "ccsowner.store",
                PawnStoreProcedures.CCSOWNER,
                out dSet,
                ref dTools))
            {
                errTxt = string.Format(
                    "Could not find any stores! Store query failed.");
                showError(errTxt);
                this.Close();
                return;
            }

            if (dSet == null || dSet.NumberRows <= 0)
            {
                errTxt = string.Format(
                    "Invalid result sets from store query!");
                showError(errTxt);
                this.Close();
                return;
            }
            //Extract the stores from the data set            
            for (var j = 0; j < dSet.NumberRows; ++j)
            {
                DataReturnSetRow dRow;
                if (!dSet.GetRow(j, out dRow)) continue;
                if (dRow == null) continue;
                var storeNum = Utilities.GetStringValue(dRow.GetData(0), string.Empty);
                if (!string.IsNullOrEmpty(storeNum))
                {
                    this.storeList.Add(storeNum);    
                }                
            }

            //Order them to enhance search speed
            this.storeList.Sort();
            /* No need to do this anymore with text box submit
             * //Order the stores
            orderedShopList.Sort();

            //Put the stores into the combo box
            foreach(var s in orderedShopList)
            {
                var storeNum = s;
                if (storeNum != null)
                {
                    var newComboItem = new ComboBoxItem
                                       {
                                           Content = storeNum
                                       };
                    this.storeComboBox.Items.Add(newComboItem);
                }
            }*/
        }

        private void DSTRViewerWindowForm_Initialized(object sender, EventArgs e)
        {

        }
        /*
         * select storenumber, storage_id, storage_date, storage_time from ccsowner.pawndocumentregistry pdr
            where PDR.STORENUMBER = '02030' 
            and   PDR.DOC_TYPE = 'PDF'
            and   PDR.RECEIPTDETAIL_NUMBER = 0
            and   PDR.TICKET_NUMBER = 0
            and   PDR.CUSTOMERNUMBER is null
            and   PDR.STORAGE_DATE > (sysdate-90);
         * 
         */

        private void submitStoreButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.storeCalendar != null)
            {
                this.storeCalendar.IsEnabled = false;
            }
            //Check file time - we do not want to hit production if the time limit is still in effect
            TupleType<long, long, string> timeData;
            if (TimeFileExists() && WithinTimeLimit(out timeData))
            {
                var diffTime = timeData.Right;
                MessageBox.Show("You are still within the time restriction limit. "+ Environment.NewLine + " The application will now close.", "*** TIME LIMIT WARNING ***");
                Application.Current.Shutdown();
                this.Close();
                return;
            }
            if (!string.IsNullOrEmpty(this.selectedStore))
            {
                //Check to see if the store is in the list
                if (this.storeList.BinarySearch(this.selectedStore) < 0)
                {
                    MessageBox.Show("That store has not yet been converted to Cashlinx.  Please enter a different store number.");
                    return;
                }
                string errTxt;
                var dTools = this.CshLnxDataTools;
                this.submitStoreButton.IsEnabled = false;
                var ninetyDaysAgo = DateTime.Now.Date.Subtract(new TimeSpan(90, 0, 0, 0));
                var toDateConstruct = string.Format(
                    "to_date('{0}/{1}/{2}', 'MM/DD/YYYY')",
                    ninetyDaysAgo.Date.Month.ToString().PadLeft(2, '0'),
                    ninetyDaysAgo.Date.Day.ToString().PadLeft(2, '0'),
                    ninetyDaysAgo.Date.Year.ToString().PadLeft(4, '0'));

                if (!InitCouchDB())
                {
                    MessageBox.Show("Cannot connect to couch server! Exiting...", "Exit Warning");
                    Application.Current.Shutdown();
                    this.DialogResult = false;
                    this.Close();
                }
                var procMsg = new ProcessingMessage("*** PLEASE WAIT - FINDING DOCUMENTS ***");
                procMsg.Show();
                if (!DataAccessService.ExecuteQuery(
                    false,
                    string.Format(
                        "select storage_id, storage_time from ccsowner.pawndocumentregistry where storenumber = '{0}' " + 
                        "and receiptdetail_number = 0 and ticket_number = 0 and doc_type = 'PDF' " + 
                        "and customernumber is null and storage_date > {1} order by storage_time desc", 
                        this.selectedStore, toDateConstruct),
                    "ccsowner.pawndocumentregistry",
                    PawnStoreProcedures.CCSOWNER,
                    out dstrStorageData,
                    ref dTools))
                {
                    procMsg.Hide();
                    errTxt = string.Format(
                        "Could not find any DSTR documents for that store");
                    showError(errTxt);
                    this.submitStoreButton.IsEnabled = true;
                    return;
                }
                if (dstrStorageData == null || dstrStorageData.NumberRows <= 0)
                {
                    procMsg.Hide();
                    errTxt = string.Format(
                        "Could not find any DSTR documents to view");
                    showError(errTxt);
                    this.submitStoreButton.IsEnabled = true;
                    return;
                }

                //Collect dates into a temporary map
                this.dateStorageMap.Clear();
                bool foundValidDoc = false;
                for (var j = 0; j < dstrStorageData.NumberRows; ++j)
                {
                    DataReturnSetRow dRow;
                    if (!dstrStorageData.GetRow(j, out dRow))
                    {
                        continue;
                    }
                    var dRowDate = Utilities.GetDateTimeValue(dRow.GetData("STORAGE_TIME"), DateTime.Now.Date);
                    List<PairType<string, Document>> storageIds;
                    var ticksKey = dRowDate.Date.Ticks;
                    if (CollectionUtilities.isNotEmptyContainsKey(this.dateStorageMap, ticksKey))
                    {
                        storageIds = this.dateStorageMap[ticksKey];
                    }
                    else
                    {
                        storageIds = new List<PairType<string, Document>>();
                        this.dateStorageMap.Add(ticksKey, storageIds);
                    }
                    //Get the storage id first
                    var storageId = Utilities.GetStringValue(dRow.GetData("STORAGE_ID"), string.Empty);
                    Document doc;
                    if (IsDocumentDSTR(storageId, out doc))
                    {
                        storageIds.Add(new PairType<string, Document>(storageId, doc));
                        foundValidDoc = true;
                    }
                }

                procMsg.Hide();

                if (!foundValidDoc)
                {
                    procMsg.Hide();
                    errTxt = string.Format(
                        "Could not find any DSTR documents to view for this store");
                    showError(errTxt);
                    this.submitStoreButton.IsEnabled = true;
                    return;
                }
                

                //Update calendar
                var today = DateTime.Now.Date;
                var tomorrowDate = DateTime.Now.Date.Add(new TimeSpan(1, 0, 0, 0));
                //Clear calendar black out dates
                this.storeCalendar.BlackoutDates.Clear();
                //Black out calendar from start of time to 90 days ago
                this.storeCalendar.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue.Date, ninetyDaysAgo.Date));
                //Black out calendar from tomorrow to the end of time
                this.storeCalendar.BlackoutDates.Add(new CalendarDateRange(tomorrowDate.Date, DateTime.MaxValue.Date));
                //Ensure store calendar is set to single date mode
                this.storeCalendar.SelectionMode = CalendarSelectionMode.SingleDate;
                //Ensure calendar is on today's date
                this.storeCalendar.SelectedDate = null;
                //Go through the ninety day range to today and black out dates we do not have storage ids for...
                var curDay = ninetyDaysAgo.Date;
                while(curDay.Date.CompareTo(today.Date) < 0)
                {
                    if (!CollectionUtilities.isNotEmptyContainsKey(this.dateStorageMap, curDay.Date.Ticks))
                    {
                        this.storeCalendar.BlackoutDates.Add(new CalendarDateRange(curDay.Date));
                        if (FileLogger.Instance.IsLogDebug)
                        {
                            FileLogger.Instance.logMessage(LogLevel.DEBUG, this, "Blackout date found = " + curDay.Date.ToLongDateString() + ", Ticks = " + curDay.Date.Ticks);
                        }
                    }
                    else
                    {
                        var idList = this.dateStorageMap[curDay.Date.Ticks];
                        if (CollectionUtilities.isEmpty(idList))
                        {
                            this.storeCalendar.BlackoutDates.Add(new CalendarDateRange(curDay.Date));
                            if (FileLogger.Instance.IsLogWarn)
                            {
                                FileLogger.Instance.logMessage(LogLevel.WARN, this, "Date with no DSTR found = " + curDay.Date.ToLongDateString() + ", Ticks = " + curDay.Date.Ticks);
                            }
                        }
                        else
                        {
                            if (FileLogger.Instance.IsLogDebug)
                            {
                                FileLogger.Instance.logMessage(LogLevel.DEBUG, this, "Valid date found = " + curDay.Date.ToLongDateString() + ", Ticks = " + curDay.Date.Ticks);
                            }
                        }
                    }
                    //Increment date
                    curDay = curDay.Date.Add(new TimeSpan(1, 0, 0, 0));
                }
                //Enable calendar
                this.storeCalendar.IsEnabled = true;
            }
        }

        private void storeCalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.viewButton != null)this.viewButton.IsEnabled = false;
            if (this.storeCalendar.SelectedDate != null)
            {
                this.selectedDate = this.storeCalendar.SelectedDate.Value;
                this.viewButton.IsEnabled = true;
                this.viewButton.Focus();
                this.viewButton.UpdateLayout();
            }
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            var res = MessageBox.Show("Are you sure you want to exit?", "Exit Warning", MessageBoxButton.YesNo);
            if (res == MessageBoxResult.Yes)
            {
                this.DialogResult = false;
                Application.Current.Shutdown(0);
                this.Close();
                return;
            }
        }

        private bool InitCouchDB()
        {
            if (this.CouchServer == null) return (false);
            if (this.couchConnector != null && this.couchConnector.Error == false) return (true);
            this.couchConnector = 
                new SecuredCouchConnector
                    (
                     this.EncryptedConfig.DecryptValue(this.CouchServer.Server),
                        this.EncryptedConfig.DecryptValue(this.CouchServer.Port),
                        DesktopSession.SSL_PORT,
                        this.EncryptedConfig.DecryptValue(this.CouchServer.Schema),
                        this.EncryptedConfig.DecryptValue(this.CouchServer.DbUser),
                        this.EncryptedConfig.DecryptValue(this.CouchServer.DbUserPwd),
                        DesktopSession.SECURE_COUCH_CONN);

            if (this.couchConnector.Error)
            {
                return (false);
            }

            return (true);
        }

        private bool IsRestrictedEnvironment()
        {
            if (Properties.Settings.Default.prodRestrict &&
                !string.IsNullOrEmpty(Properties.Settings.Default.prodEnv) &&
                this.curEnvString.Equals(Properties.Settings.Default.prodEnv, StringComparison.OrdinalIgnoreCase))
            {
                return (true);
            }
            return (false);
        }

        private bool TimeFileExists()
        {
            if (!IsRestrictedEnvironment())
            {
                return (true);
            }
            try
            {
                //Determine if the file exists
                var filesExisting = Directory.GetFiles(".", TIME_FILE, SearchOption.TopDirectoryOnly);
                return (filesExisting.Length > 0);
            }
            catch (Exception eX)
            {
                if (FileLogger.Instance.IsLogError)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Time file does not exist: {0}", eX.Message);
                }
            }
            return (false);
        }

        private bool GetTimeLimitAndCurrentFileStamp(out TupleType<long,long,string> timeData )
        {
            timeData = null;
            if (!IsRestrictedEnvironment())
            {
                return (true);
            }

            timeData = new TupleType<long, long, string>(0, 0, string.Empty);
            if (TimeFileExists())
            {
                try
                {
                    var fs = new FileStream(TIME_FILE, FileMode.Open, FileAccess.Read);
                    TextReader tRead = new StreamReader(fs);
                    //Read the contents
                    var data = tRead.ReadLine();
                    var lData = Utilities.GetLongValue(data, 0L);
                    if (lData > 0L)
                    {
                        //Init read time with Ticks count from file
                        var now = DateTime.Now;
                        var readTime = new DateTime(lData);
                        //var diffMinutes = now.Minute - readTime.Minute;
                        var diffTime = now - readTime;
                        //var diffMinsTime = diffTime.Minutes;
                        long limit;
                        if (!Int64.TryParse(Properties.Settings.Default.timeLimiter, out limit))
                        {
                            //Default to one minute wait if value is not right
                            limit = 60000L;
                        }
                        else
                        {
                            if (limit < 60000L)
                            {
                                //Do not allow times less than one minute
                                limit = 60000L;
                            }
                        }


                        var totalTime = (long)Math.Ceiling(Math.Abs(diffTime.TotalMilliseconds));
                        timeData.Left = limit;
                        timeData.Mid = totalTime;
                        timeData.Right = "You are not able to retrieve a document at this time. The time limit policy is in effect.";
                        fs.Close();
                        return (true);
                    }
                    //First initialized file has zero
                    else if (lData == 0)
                    {
                        fs.Close();
                        return (true);
                    }
                    fs.Close();
                }
                catch (IOException iEx)
                {
                    if (FileLogger.Instance.IsLogError)
                    {
                        FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Could not read time limit file data: {0}", iEx.Message);
                    }
                }
                catch (Exception eX)
                {
                    if (FileLogger.Instance.IsLogError)
                    {
                        FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Error occurred while reading time limit file data: {0}", eX.Message);
                    }
                }
            }
            else
            {
                //Create the file for the first time
                if (WriteTimeLimit())
                {
                    return (true);
                }
            }
            return (false);
        }

        private bool WriteTimeLimit()
        {
            if (!IsRestrictedEnvironment())
            {
                return (true);
            }

            try
            {
                //Ensure that we are good in terms of time
                var fs = new FileStream(TIME_FILE, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                TextWriter tWrite = new StreamWriter(fs);
                if (fs.CanWrite)
                {
                    tWrite.WriteLine(DateTime.Now.Ticks);
                    tWrite.Flush();
                }
                fs.Close();
            }
            catch (IOException iEx)
            {
                if (FileLogger.Instance.IsLogError)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Could not write time limit file data: {0}", iEx.Message);
                }
            }
            catch (Exception eX)
            {
                //Issue with time file fetch, do not allow view
                if (FileLogger.Instance.IsLogFatal)
                {
                    FileLogger.Instance.logMessage(LogLevel.FATAL, this, "Cannot write / update time file: {0}", eX.Message);
                }
                return (false);
            }
            return (true);
        }

        private bool WithinTimeLimit(out TupleType<long, long, string> timeData)
        {
            timeData = null;
            if (!IsRestrictedEnvironment())
            {
                return (false);
            }

            try
            {
                if (!GetTimeLimitAndCurrentFileStamp(out timeData))
                {
                    //If the read was not successful or the file didnt have data
                    //we have to assume that the time limit is still in effect
                    return (true);
                }
                var timeLimit = timeData.Left;
                var timeTotal = timeData.Mid;
                if (timeTotal < timeLimit)
                {
                    //We are still in the time limit
                    return (true);
                }
            }
            catch(Exception eX)
            {
                if (FileLogger.Instance.IsLogError)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Time file does not exist: {0}", eX.Message);
                }
                //If the time file does not exist, we have to assume the limit is still in effect
                return (true);
            }
            return (false);
        }


        private bool IsDocumentDSTR(string id, out Document doc)
        {
            var rt = false;
            doc = null;
            if (!string.IsNullOrEmpty(id))
            {
                //Fetch document meta-data
                string errMsg;
                if (CouchDbUtils.GetDocument(id, this.couchConnector, out doc, out errMsg, true))
                {
                    if (doc != null && !string.IsNullOrEmpty(doc.FileName) && 
                        doc.FileName.IndexOf("dstr", StringComparison.OrdinalIgnoreCase) > -1)
                    {
                        rt = true;
                    }
                }
            }

            return (rt);
        }


        private bool GetShowPrintCouchDocument(string id, Document doc, out string errMsg)
        {
            errMsg = string.Empty;
            if (!string.IsNullOrEmpty(id) && doc != null)
            {
                //Retrieve the document data
                byte[] dataArray;

                if (!doc.GetSourceData(out dataArray))
                {
                    errMsg = "Cannot retrieve document file data with id of " + id;
                    return (false);
                }

                //Write to a file
                try
                {
                    var fStream = new FileStream("curDstr.pdf", FileMode.Create, FileAccess.ReadWrite);
                    fStream.Write(dataArray, 0, dataArray.Length);
                    fStream.Flush();
                    fStream.Close();
                }
                catch (IOException ieX)
                {
                    errMsg = "IOException thrown when trying to write temp file: " + ieX.Message;
                    return (false);
                }

                var procHandle = new Process
                {
                    StartInfo =
                    {
                        WorkingDirectory = Environment.CurrentDirectory,
                        WindowStyle = ProcessWindowStyle.Normal,
                        CreateNoWindow = false,
                        FileName = "curDstr.pdf"
                    }
                };

                try
                {
                    if (procHandle.Start())
                    {
                        procHandle.WaitForExit();
                    }
                    if (!WriteTimeLimit())
                    {
                        errMsg = "Could not update time limiter file";
                        return (false);
                    }
                }
                catch (Exception ex)
                {
                    errMsg = "Exception thrown while showing PDF file! Message = " + ex.Message;
                    return (false);
                }                    
            }
            else
            {
                errMsg = "Could not find document with an ID of " + id;
                return (false);
            }
            return (true);
        }
    

        private void viewButton_Click(object sender, RoutedEventArgs e)
        {
            //Check file time - we do not want to hit production if the time limit is still in effect
            TupleType<long, long, string> timeData;
            bool restrict = false;
            if (TimeFileExists() && WithinTimeLimit(out timeData))
            {
                var diffTime = timeData.Right;
                MessageBox.Show("You are still within the time restriction limit.  The application will now close.", "*** TIME LIMIT WARNING ***");
                Application.Current.Shutdown();
                restrict = true;
                this.DialogResult = false;
                this.Close();
                return;
            }


            if (this.storeCalendar == null ||
                this.storeCalendar.SelectedDate == null)
            {
                MessageBox.Show("Please select a date from the calendar above prior to viewing and printing.", 
                    "DSTRViewer Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                this.DialogResult = true;
                //this.Close();
                return;
            }
            this.storeCalendar.IsEnabled = false;
            this.viewButton.IsEnabled = false;
            var selTicks = this.storeCalendar.SelectedDate.Value.Date.Ticks;
            if (CollectionUtilities.isNotEmptyContainsKey(this.dateStorageMap, selTicks))
            {
                var storageIds = this.dateStorageMap[selTicks];
                if (storageIds.Count <= 0)
                {
                    var res = MessageBox.Show(
                        "No documents found for that date/store combination.  Would you like to select another?",
                        "No Documents Found", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (res == MessageBoxResult.Yes)
                    {
                        this.viewButton.IsEnabled = true;
                        this.submitStoreButton.IsEnabled = true;
                        return;
                    }
                    this.DialogResult = false;
                    this.Finished = false;
                    this.Close();
                }
                var success = false;
                var cnt = 0;
                foreach (var id in storageIds)
                {
                    if (string.IsNullOrEmpty(id.Left))
                    {
                        ++cnt;
                        continue;
                    }
                    string errMsg;

                    //As soon as retrieval is successful
                    if (GetShowPrintCouchDocument(id.Left, id.Right, out errMsg))
                    {
                        //Successfully showed document
                        success = true;
                        break;
                    }
                    if (cnt < storageIds.Count - 1)
                    {
                            ++cnt;
                            continue;
                    }
                    var res = MessageBox.Show(
                        "No DSTR documents available to view from this date/store combination. " + 
                        "Would you like to select another store/date combination?",
                        "No Documents To Show", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                    if (res == MessageBoxResult.Yes)
                    {
                        this.viewButton.IsEnabled = true;
                        this.submitStoreButton.IsEnabled = true;
                        this.storeCalendar.IsEnabled = false;
                        return;
                    }
                    Application.Current.Shutdown(1);
                    this.Close();
                    return;
                }

                if (success)
                {
                    this.viewButton.IsEnabled = true;
                    this.submitStoreButton.IsEnabled = true;
                    var res = MessageBox.Show("Would you like to continue using this viewer?", "Exit Question", MessageBoxButton.YesNo);
                    if (res == MessageBoxResult.No)
                    {
                        this.DialogResult = true;
                        Application.Current.Shutdown();
                        this.Finished = true;
                        this.Close();
                        return;
                    }
                    else
                    {
                        this.viewButton.IsEnabled = false;
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Could not find any DSTR documents! Exiting...", "Exit Warning");
                    Application.Current.Shutdown();
                    this.DialogResult = false;
                    this.Finished = true;
                    this.Close();
                    return;
                }
            }
            else
            {
                this.viewButton.IsEnabled = true;
                this.submitStoreButton.IsEnabled = true;
                MessageBox.Show("Could not find any documents at the selected date! Please select another date and/or store.");
            }
        }

        private void storeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.storeTextBox.Text))
            {
                //Ensure that the text box only contains numbers
                var stoTxt = this.storeTextBox.Text;
                var stoTxtLen = this.storeTextBox.Text.Length;
                var allDigits = true;
                this.storeNumErrLabel.Content = string.Empty;
                foreach(var c in stoTxt)
                {
                    if (!Char.IsDigit(c))
                    {
                        allDigits = false;
                        break;
                    }
                }

                if (allDigits && stoTxtLen >= MIN_REJECT_LEN)
                {
                    //Pad left if needed
                    this.selectedStore = stoTxt.PadLeft(5, '0');
                    if (this.storeList.BinarySearch(this.selectedStore) >= 0)
                    {
                        this.submitStoreButton.IsEnabled = true;
                    }
                    else
                    {
                        this.submitStoreButton.IsEnabled = false;
                        this.storeNumErrLabel.Content = "Not a Cashlinx Store";
                    }
                }
                else if (!allDigits)
                {
                    //Show error message if store number is invalid
                    this.showError("Please enter a valid store number. A valid store number contains digits only (0-9).");
                    this.submitStoreButton.IsEnabled = false;
                    this.viewButton.IsEnabled = false;
                    this.storeCalendar.IsEnabled = false;
                }
            }
            else
            {
                this.storeNumErrLabel.Content = string.Empty;
            }
        }
    }
}
