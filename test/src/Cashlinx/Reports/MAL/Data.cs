using System;
using System.Collections.Generic;
using System.Data;
using Common.Controllers.Database.DataAccessLayer;
using Common.Controllers.Database.Oracle;
using Common.Libraries.Objects.Authorization;
using Common.Libraries.Utility.Type;
using Oracle.DataAccess.Client;

namespace Reports.MAL
{
    public class Data
    {
        private string _errCode;
        private string _errTxt;
        private string _storeName;
        private string _storeNum;
        private DataSetOutput _reportData;
        private DateTime _runDate;
        private DateTime _runEndDate;
        private Credentials _Credentials;

        public string StoreName
        {
            get { return _storeName; }
        }

        public DateTime RunDate
        {
            get { return _runDate; }
            set { _runDate = value; }
        }

        public DateTime RunEndDate
        {
            get { return _runEndDate; }
            set { _runEndDate = value; }
        }

        public string StoreNum
        {
            get { return _storeNum; }
        }

        public DataSetOutput ReportData
        {
            get { return _reportData; }
        }
        public string ErrCode
        {
            get { return _errCode; }
        }

        public string ErrTxt
        {
            get { return _errTxt; }
        }

        //***************************************************************
        /// <summary>
        /// One of 2 constructors: This is default contructor
        /// accepting all of the needed parameters for data 
        /// gathering. 
        /// </summary>
        //***************************************************************
        public Data(DateTime RunDate
                              , DateTime RunEndDate
                              , string storeNum
                              , Credentials credentials)
        {
            _errTxt = string.Empty;
            _errCode = "OK";
            _runDate = RunDate;
            _runEndDate = RunEndDate;
            _storeNum = storeNum;
            _Credentials = credentials;
            _storeName = storeNum;

        }

        //***************************************************************
        /// <summary>
        /// One of 2 constructors: This is one uses todays date
        /// for the rundate
        ///
        /// </summary>
        //***************************************************************
        public Data(string storeNum, Credentials credentials)
        {
            _errTxt = string.Empty;
            _errCode = "OK";
            _runDate = DateTime.Now;
            _storeNum = storeNum;
            _Credentials = credentials;
            _storeName = storeNum;
        }

        //*********************************************************************
        //** Date created: Tuesday, Jan 19, 2010
        //** Created by  : PAWN\rmcbai1
        //*********************************************************************
        /// <summary>
        ///    build the datasets needed to generate the report
        /// </summary>
        ///
        /// <returns> the data needed to build the report ( DataSet)</returns>
        //*********************************************************************
        public DataSetOutput BuildDataset()
        {
            _reportData = null;

            try
            {
                DataAccessTools dataAccessTools = DataAccessService.CreateDataAccessTools();

                DataAccessService.Connect(_Credentials,
                                          DataAccessTools.ConnectMode.SINGLE,
                                          DataAccessTools.LogMode.DEBUG,
                                          ref dataAccessTools);


                List<OracleProcParam> inParams = new List<OracleProcParam>();

                inParams.Add(new OracleProcParam("store_num", _storeNum));
                string date_in = _runDate.ToString("d");
                inParams.Add(new OracleProcParam("date_in", date_in));

                string date_out = _runEndDate.ToString("d");
                inParams.Add(new OracleProcParam("date_out", date_out));

                List<PairType<string, string>> refCursors = new List<PairType<string, string>>();
                refCursors.Add(new PairType<string, string>("o_disp_data", "DISP_DATA"));

                inParams.Add(new OracleProcParam("o_store_name", OracleDbType.Varchar2, _storeName, ParameterDirection.Output, 100));

                bool retval = DataAccessService.ExecuteStoredProc(_Credentials.DBSchema
                                                    , "PAWN_REPORTS"
                                                    , "getData"
                                                    , inParams
                                                    , null
                                                    , refCursors
                                                    , "o_error_code"
                                                    , "o_error_text"
                                                    , null
                                                    , out _errCode
                                                    , out _errTxt
                                                    , out _reportData
                                                    , ref dataAccessTools);

                if (!retval)
                {
                    retval = DataAccessService.ExecuteStoredProc(_Credentials.DBSchema
                                                                        , "PAWN_REPORTS"
                                                                        , "getData"
                                                                        , inParams
                                                                        , null
                                                                        , refCursors
                                                                        , "o_error_code"
                                                                        , "o_error_text"
                                                                        , null
                                                                        , out _errCode
                                                                        , out _errTxt
                                                                        , out _reportData
                                                                        , ref dataAccessTools);
                }


                if (!retval)
                {
                    _errCode = "1";
                    _errTxt = "An error occurred retrieving the report data.  Please try again, if this error persists then contact support.";
                }
            }
            catch (Exception ex)
            {
                _reportData = null;
                _errCode = "1";
                _errTxt = ex.Message;
            }


            return _reportData;
        }
    }
}
