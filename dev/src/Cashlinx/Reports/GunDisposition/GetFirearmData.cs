//***************************************************************
// class_name   :  GetFirearmData
// created by   : rmcbai1
// date created : 12/15/2009 10:54:09 AM
//
// Copyright  2009 Cash America International
//---------------------------------------------------------------
//   description:  this class will gather up all the data
//                 needed for creating the Report #201 (MULTIPLE  
//                 SALE  OR  OTHER  DISPOSITION  OF  PISTOLS  AND  
//                 REVOLVERS)  It will create a dataset that is used
//                 by the DLL that creates the actual PDF file.  
//
//----------------------------------------------------------------
// change history
//
//****************************************************************

using System;
using System.Collections.Generic;
using System.Data;
using Common.Controllers.Database.Oracle;
using Common.Libraries.Objects.Mapper;
using Common.Libraries.Utility.Type;

//using Oracle.DataAccess.Client;

namespace Reports.GunDisposition
{
    public class GetFirearmData
    {

        private string _storeName;
        private bool _forwards;       // determines which way the date range runs 
        // from _initialDate
        private int _dateRange;     //  # of business days the report covers
        private int _errCode;
        private string _errTxt;
        private string _storeNum;
        private DateTime _runDate;
        private DateTime _initialDate;    //  used with date range to get the days the report
        //  is to cover.
        private string _finalDate;      // depending on the _forward flag, this 
        // could be  larger or smaller than  _initialDate
        private DataSet _reportData;
        private readonly OracleDataAccessor _dA;

        public string storeName
        {
            get { return _storeName; }
        }

        public string finalDate
        {
            get{return _finalDate;}
        }

        public bool Forwards
        {
            get { return _forwards;}
            set { _forwards = value;}
        }

        public int DateRange
        {
            get { return _dateRange;}
            set { _dateRange = value;}
        }
        public string StoreNum
        {
            get { return _storeNum;}
            set { _storeNum = value;}
        }
            
        public DataSet ReportData
        {
            get { return _reportData;}
        }

        public DateTime InitialDate
        {
            get { return _initialDate;}
            set { _initialDate = value; }
        }
      
        public DateTime RunDate
        {
            get { return _runDate;}
        }

        public int ErrCode
        {
            get { return _errCode;}
        }
      
        public string ErrTxt
        {
            get { return _errTxt;}
        }

        //********************************************************
        /// <summary>
        /// One of four constructors: This is the mimimal contructor
        /// accepting just two of the needed parapters for data 
        /// gathering. As such, it uses the current date for the inital day,
        /// 5 for the number of business days, and false to indicate
        /// we want the previous 5 days.
        /// </summary>
        //********************************************************
        public GetFirearmData(string storeNum, OracleDataAccessor dataAccessor)
        {
            _dA = dataAccessor;
            SetDefaults(DateTime.Now, storeNum, 5,false);
        }
        //********************************************************
        /// <summary>
        ///  one of four constructors: This is the next level contructor
        /// accepting three of the needed parameters for data 
        /// gathering. As such, it uses the current date for the 
        /// inital day and false to indicate we want the previous 
        /// dateRange number of days
        /// </summary>
        //********************************************************
        public GetFirearmData(string storeNum, OracleDataAccessor dataAccessor, int dateRange)
        {
            _dA = dataAccessor;
            SetDefaults(DateTime.Now, storeNum, dateRange, false);         
        }

        //********************************************************
        /// <summary>
        ///  one of four constructors: This constructor has only 
        ///  one default indicating we want the previous 
        /// dateRange number of days
        ///  
        /// </summary>
        //********************************************************
        public GetFirearmData(DateTime initialDate, string storeNum, OracleDataAccessor dataAccessor, int dateRange)
        {
            _dA = dataAccessor;
            SetDefaults(initialDate, storeNum, dateRange,false);
        }

        //********************************************************
        /// <summary>
        ///  one of four constructors: this one takes all the values
        /// needed to create our data
        ///  
        /// </summary>
        //********************************************************
        public GetFirearmData(DateTime initialDate, string storeNum, 
                              OracleDataAccessor dataAccessor, int dateRange, bool forwards)
        {
            _dA = dataAccessor;
            SetDefaults(initialDate, storeNum, dateRange, forwards);
        }

        //*************************************************
        //** Date created: Tuesday, December 15, 2009
        //** Created by  : PAWN\rmcbai1
        //*************************************************
        /// <summary>
        ///    Set up the values needed for the call to 
        ///    the stored proc
        /// </summary>
        ///
        //*************************************************
        private void SetDefaults(DateTime initialDate, string storeNum, int dateRange, bool forwards)
        {
            _forwards    = forwards;
            _dateRange   = dateRange;
            _runDate     = DateTime.Now;
            _initialDate = initialDate;
            _storeNum    = storeNum;
            _reportData  = null;
            _errCode     = 0;
            _errTxt      = string.Empty;
            _finalDate   = string.Empty; 
            return;
        }

        //*********************************************************************
        //** Date created: Tuesday, December 15, 2009
        //** Created by  : PAWN\rmcbai1
        //*********************************************************************
        /// <summary>
        ///    build the datasets needed to generate the report
        /// </summary>
        ///
        /// <returns> the data needed to build the report ( DataSet)</returns>
        //*********************************************************************
        public DataSet BuildDataset()
        {
            bool retVal;
            int iFlag; // we are not using the Oracle data type of BOOLEAN, 
            // hence this mapping

            iFlag = (_forwards ?  1 :  0);

            var inParams = new List<OracleProcParam>
                           {
                               new OracleProcParam("day_cnt", _dateRange),
                               new OracleProcParam("init_day", _initialDate),
                               new OracleProcParam("forwards", iFlag),
                               new OracleProcParam("store_num", _storeNum),
                               new OracleProcParam(
                                   "o_final_day",
                                   DataTypeConstants.PawnDataType.STRING,
                                   _finalDate,
                                   ParameterDirection.Output,
                                   12),
                               new OracleProcParam(
                                   "o_store_name",
                                   DataTypeConstants.PawnDataType.STRING,
                                   _storeName,
                                   ParameterDirection.Output,
                                   256)
                           };

            var refCursors = new List<PairType<string, string>>
                             {
                                 new PairType<string, string>("o_gun_detail", "GUN_DETAIL"),
                                 new PairType<string, string>("o_header", "GUN_HEADER")
                             };

            try
            {
                retVal = _dA.issueSqlStoredProcCommand("ccsowner", "gun_disp_rpt_201", "get_rpt_data",
                                                       inParams, refCursors, "o_error_code", "o_error_text", out _reportData);
                _finalDate = _reportData.Tables["OUTPUT"].Rows[0][1].ToString();
                _storeName = _reportData.Tables["OUTPUT"].Rows[1][1].ToString();
            }
            catch(Exception ex)
            {
                _reportData = null;
                _errCode = 1;
                _errTxt = ex.Message;
                throw;
            }
            return _reportData;
        }
    }
}