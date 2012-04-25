using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Common.Libraries.Objects.Mapper;
using Common.Libraries.Utility.Collection;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace Common.Controllers.Database.Oracle
{
    public class OracleProcParam
    {
        private readonly DataTypeConstants.PawnDataType dataTypeName;
        private readonly List<object> parameterValues;
        private readonly int arraySize;
        private readonly bool paramIsClob;

        public ParameterDirection Direction { get; private set; }

        public enum TimeStampType
        {
            TIMESTAMP,
            TIMESTAMP_TZ,
            TIMESTAMP_LTZ
        }

        public OracleDbType OracleType { get; private set; }

        public DataTypeConstants.PawnDataType DataName
        {
            get
            {
                return (this.dataTypeName);
            }
        }

        public string Name { get; private set; }

        /// <summary>
        /// OracleProcParam ctor to setup for the add of any kind of array
        /// </summary>
        /// <param name="pDir"></param>
        /// <param name="pDName"></param>
        /// <param name="pName"></param>
        /// <param name="pArrSize"></param>
        public OracleProcParam(
            ParameterDirection pDir,
            DataTypeConstants.PawnDataType pDName,
            string pName,
            int pArrSize)
        {
            this.Direction = pDir;
            this.paramIsClob = false;
            this.dataTypeName = pDName;
            this.Name = pName;
            this.parameterValues = new List<object>(1);
            this.arraySize = pArrSize;
            this.ComputeDataType();
        }

        /// <summary>
        /// Oracle proc param constructor to add string array
        /// </summary>
        /// <param name="pName"></param>
        /// <param name="isArrayParam"></param>
        /// <param name="arrayStringData"></param>
        public OracleProcParam(
// ReSharper disable UnusedParameter.Local
            string pName, bool isArrayParam,
// ReSharper restore UnusedParameter.Local
            List<string> arrayStringData)
        {
            this.Direction = ParameterDirection.Input;
            this.paramIsClob = false;
            this.dataTypeName = DataTypeConstants.PawnDataType.LISTSTRING;
            this.Name = pName;
            this.parameterValues = new List<object>(1);
            this.arraySize = arrayStringData.Count;
            this.ComputeDataType();

            //Add values
            for (int j = 0; j < this.arraySize; ++j)
            {
                this.AddValue(arrayStringData[j]);
            }
        }

        /// <summary>
        /// OracleProcParam ctor to add integer array
        /// </summary>
        /// <param name="pName"></param>
        /// <param name="isArrayParam"></param>
        /// <param name="arrayIntegerData"></param>
// ReSharper disable UnusedMember.Global
        public OracleProcParam(
// ReSharper restore UnusedMember.Global
// ReSharper disable UnusedParameter.Local
            string pName, bool isArrayParam,
// ReSharper restore UnusedParameter.Local
            List<int> arrayIntegerData)
        {
            this.Direction = ParameterDirection.Input;
            this.paramIsClob = false;
            this.dataTypeName = DataTypeConstants.PawnDataType.LISTINT;
            this.Name = pName;
            this.parameterValues = new List<object>(1);
            this.arraySize = arrayIntegerData.Count;
            this.ComputeDataType();

            //Add values
            for (var j = 0; j < this.arraySize; ++j)
                this.AddValue(arrayIntegerData[j]);
        }

        /// <summary>
        /// Input default direction, string data constructor with ability to specify first value
        /// </summary>
        /// <param name="pName"></param>
        /// <param name="val"></param>
        public OracleProcParam(string pName, decimal val)
        {
            this.Direction = ParameterDirection.Input;
            this.paramIsClob = false;
            this.dataTypeName = DataTypeConstants.PawnDataType.REALNUMBER;
            this.Name = pName;
            this.parameterValues = new List<object>(1);
            this.OracleType = OracleDbType.Decimal;

            //Add value
            this.AddValue(val);
        }

        /// <summary>
        /// Input default direction, string data constructor with ability to specify first value
        /// </summary>
        /// <param name="pName"></param>
        /// <param name="val"></param>
        public OracleProcParam(string pName, string val)
        {
            this.Direction = ParameterDirection.Input;
            this.paramIsClob = false;
            this.dataTypeName = DataTypeConstants.PawnDataType.STRING;
            this.Name = pName;
            this.parameterValues = new List<object>(1);
            this.ComputeDataType();

            //Add value
            if (!string.IsNullOrEmpty(val))
            {
                this.AddValue(val);
            }
            else
            {
                this.dataTypeName = DataTypeConstants.PawnDataType.NULLSTRING;
                this.ComputeDataType();
                this.AddValue(string.Empty);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pName"></param>
        /// <param name="val"></param>
        /// <param name="isClob"></param>
        public OracleProcParam(string pName, string val, bool isClob)
        {
            this.Direction = ParameterDirection.Input;
            this.paramIsClob = isClob;
            this.dataTypeName = (isClob) ?
                DataTypeConstants.PawnDataType.CLOB :
                DataTypeConstants.PawnDataType.STRING;
            this.Name = pName;
            this.parameterValues = new List<object>(1);
            this.ComputeDataType();

            //Add value
            if (!string.IsNullOrEmpty(val))
            {
                this.AddValue(val);
            }
            else
            {
                this.dataTypeName = DataTypeConstants.PawnDataType.NULLSTRING;
                this.ComputeDataType();
                this.AddValue(string.Empty);
            }
        }

  

        /// <summary>
        /// Input default direction, int data constructor with ability to specify first value
        /// </summary>
        /// <param name="pName"></param>
        /// <param name="val"></param>
        public OracleProcParam(string pName, Int32 val)
        {
            this.paramIsClob = false;
            this.Direction = ParameterDirection.Input;
            this.dataTypeName = DataTypeConstants.PawnDataType.WHOLENUMBER;
            this.Name = pName;
            this.parameterValues = new List<object>(1);
            this.ComputeDataType();

            //Add value
            if (val != Int32.MaxValue)
            {
                //this.oracleDataType = OracleDbType.Decimal;
                this.AddValue(val.ToString());
            }
            else
            {
                this.dataTypeName = DataTypeConstants.PawnDataType.NULLNUMBER;
                this.ComputeDataType();
                this.AddValue(string.Empty);
            }
        }

        /// <summary>
        /// Input default direction, int data constructor with ability to specify first value
        /// </summary>
        /// <param name="pName"></param>
        /// <param name="val"></param>
        public OracleProcParam(string pName, Int64 val)
        {
            this.paramIsClob = false;
            this.Direction = ParameterDirection.Input;
            this.dataTypeName = DataTypeConstants.PawnDataType.WHOLENUMBER;
            this.Name = pName;
            this.parameterValues = new List<object>(1);
//            this.OracleType = OracleDbType.Int64;
//            this.arraySize = 1;
            this.ComputeDataType();

            //Add value
            if (val != Int64.MaxValue)
            {
                this.AddValue(val.ToString());
            }
            else
            {
                this.dataTypeName = DataTypeConstants.PawnDataType.NULLNUMBER;
                this.ComputeDataType();
                this.AddValue(string.Empty);
            }
        }

        /// <summary>
        /// Input default direction, date data constructor with ability to specify first value
        /// </summary>
        /// <param name="pName"></param>
        /// <param name="val"></param>
        public OracleProcParam(string pName, DateTime val)
        {
            this.paramIsClob = false;
            this.Direction = ParameterDirection.Input;
            this.dataTypeName = DataTypeConstants.PawnDataType.DATE;
            this.Name = pName;
            this.parameterValues = new List<object>(1);
            this.ComputeDataType();

            //Add value
            if (val != DateTime.MaxValue)
            {
                this.AddValue(OracleDataAccessor.GenerateOracleDateString(val));
            }
            else
            {
                this.dataTypeName = DataTypeConstants.PawnDataType.NULLDATE;
                this.ComputeDataType();
                this.AddValue(string.Empty);
            }
        }

        /// <summary>
        /// Input default direction, date data constructor with ability to specify first value
        /// </summary>
        /// <param name="pName"></param>
        /// <param name="val"></param>
        /// <param name="tType"></param>
        public OracleProcParam(string pName, DateTime val, TimeStampType tType)
        {
            this.paramIsClob = false;
            this.Direction = ParameterDirection.Input;
            this.dataTypeName = DataTypeConstants.PawnDataType.DATE;
            this.Name = pName;
            this.parameterValues = new List<object>(1);
            this.arraySize = 1;
            string tZVal = string.Empty;
            switch (tType)
            {
                case TimeStampType.TIMESTAMP:
                    this.OracleType = OracleDbType.TimeStamp;
                    tZVal = OracleDataAccessor.GenerateOracleTimestampString(val);
                    break;
                case TimeStampType.TIMESTAMP_TZ:
                    this.OracleType = OracleDbType.TimeStampTZ;
                    tZVal = OracleDataAccessor.GenerateOracleTimestampTZString(val);
                    break;
                case TimeStampType.TIMESTAMP_LTZ:
                    this.OracleType = OracleDbType.TimeStampLTZ;
                    tZVal = OracleDataAccessor.GenerateOracleTimestampLTZString(val);
                    break;
            }

            //Add value
            if (val != DateTime.MaxValue)
            {
                this.AddValue(tZVal);
            }
            else
            {
                this.dataTypeName = DataTypeConstants.PawnDataType.NULLDATE;
                this.ComputeDataType();
                this.AddValue(string.Empty);
            }
        }

        /// <summary>
        /// Overriding type constructor for input parameters
        /// </summary>
        /// <param name="pName"></param>
        /// <param name="oDb"></param>
        /// <param name="val"></param>
        public OracleProcParam(string pName, OracleDbType oDb, string val)
        {
            if (oDb == OracleDbType.Clob)
            {
                this.paramIsClob = true;
            }
            this.Direction = ParameterDirection.Input;
            this.dataTypeName = DataTypeConstants.PawnDataType.WHOLENUMBER;
            this.Name = pName;
            this.parameterValues = new List<object>(1);
            this.OracleType = oDb;
            this.arraySize = 1;
            this.AddValue(val);
        }

        /// <summary>
        /// Overriding type constructor for any direction parameters
        /// </summary>
        /// <param name="pName"></param>
        /// <param name="oDb"></param>
        /// <param name="val"></param>
        /// <param name="pDir"></param>
        /// <param name="sZ"></param>
        public OracleProcParam(string pName, OracleDbType oDb, object val, ParameterDirection pDir, int sZ)
        {
            if (oDb == OracleDbType.Clob)
            {
                this.paramIsClob = true;
            }
            this.Direction = pDir;
            this.dataTypeName = DataTypeConstants.PawnDataType.WHOLENUMBER;
            this.Name = pName;
            this.parameterValues = new List<object>(1);
            this.OracleType = oDb;
            if (this.Direction == ParameterDirection.Input && val != null)
            {
                if (oDb == OracleDbType.TimeStamp || oDb == OracleDbType.TimeStampLTZ ||
                    oDb == OracleDbType.TimeStampTZ)
                {
                    this.AddValue(val);
                }
                else
                {
                    this.AddValue(val.ToString());
                }
            }
            else if (val != null)
            {
                this.AddValue(DBNull.Value);
            }
            this.arraySize = sZ;
        }

        /// <summary>
        /// Overriding type constructor for any direction parameters
        /// </summary>
        /// <param name="pName"></param>
        /// <param name="oDb"></param>
        /// <param name="val"></param>
        /// <param name="pDir"></param>
        /// <param name="sZ"></param>
        /// <param name="preserveType"></param>
// ReSharper disable UnusedMember.Global
        public OracleProcParam(string pName, OracleDbType oDb, object val, ParameterDirection pDir, int sZ, bool preserveType)
// ReSharper restore UnusedMember.Global
        {
            if (oDb == OracleDbType.Clob)
            {
                this.paramIsClob = true;
            }
            this.Direction = pDir;
            this.dataTypeName = DataTypeConstants.PawnDataType.WHOLENUMBER;
            this.Name = pName;
            this.parameterValues = new List<object>(1);
            this.OracleType = oDb;
            if (this.Direction == ParameterDirection.Input && val != null)
            {
                if (preserveType)
                {
                    this.AddValue(val);
                }
                else
                {
                    this.AddValue(val.ToString());
                }
            }
            else if (val != null)
            {
                this.AddValue(DBNull.Value);
            }
            this.arraySize = sZ;
        }

        /// <summary>
        /// Overriding type constructor for any direction parameters
        /// </summary>
        /// <param name="pName"></param>
        /// <param name="pDt"></param>
        /// <param name="val"></param>
        /// <param name="pDir"></param>
        /// <param name="sZ"></param>
        public OracleProcParam(string pName, DataTypeConstants.PawnDataType pDt, object val, ParameterDirection pDir, int sZ)
        {
            if (pDt == DataTypeConstants.PawnDataType.CLOB)
            {
                this.paramIsClob = true;
            }
            this.Direction = pDir;
            this.dataTypeName = pDt;
            this.Name = pName;
            this.parameterValues = new List<object>(1);
            this.ComputeDataType();
            if (this.Direction == ParameterDirection.Input && val != null)
            {
                if (pDt == DataTypeConstants.PawnDataType.TIMESTAMP)
                {
                    this.AddValue(val);
                }
                else
                {
                    this.AddValue(val.ToString());
                }
            }
            else if (val != null)
            {
                this.AddValue(DBNull.Value);
            }
            this.arraySize = sZ;
        }

        /// <summary>
        /// Overriding type constructor for any direction parameters
        /// </summary>
        /// <param name="pName"></param>
        /// <param name="pDt"></param>
        /// <param name="val"></param>
        /// <param name="pDir"></param>
        /// <param name="sZ"></param>
        /// <param name="preserveType"></param>
// ReSharper disable UnusedMember.Global
        public OracleProcParam(string pName, DataTypeConstants.PawnDataType pDt, object val, ParameterDirection pDir, int sZ, bool preserveType)
// ReSharper restore UnusedMember.Global
        {
            if (pDt == DataTypeConstants.PawnDataType.CLOB)
            {
                this.paramIsClob = true;
            }
            this.Direction = pDir;
            this.dataTypeName = pDt;
            this.Name = pName;
            this.parameterValues = new List<object>(1);
            this.ComputeDataType();
            if (this.Direction == ParameterDirection.Input && val != null)
            {
                if (preserveType)
                {
                    this.AddValue(val);
                }
                else
                {
                    this.AddValue(val.ToString());
                }
            }
            else if (val != null)
            {
                this.AddValue(DBNull.Value);
            }
            this.arraySize = sZ;
        }


        private void ComputeDataType()
        {
            //Initialize oracle data type
            var dtVOMapper = DataTypeVOMapper.Instance;
            var orSQMapper = OracleSqlDbTypeMapper.Instance;
            var sqlType = dtVOMapper[this.dataTypeName].Right;
            this.OracleType = orSQMapper[sqlType];
        }

        public void AddValue(string val)
        {
            this.parameterValues.Add(val);
        }

        public void AddValue(object val)
        {
            this.parameterValues.Add(val);
        }

        public void AddToCommand(ref OracleCommand oraCom)
        {
            if (oraCom == null)
                return;

            if (this.Direction == ParameterDirection.Output)
            {
                if (this.OracleType != OracleDbType.Varchar2)
                {
                    oraCom.Parameters.Add(
                        this.Name,
                        this.OracleType,
                        DBNull.Value,
                        this.Direction);
                }
                else
                {
                    oraCom.Parameters.Add(
                        this.Name,
                        this.OracleType,
                        this.arraySize,
                        DBNull.Value,
                        this.Direction);
                }
                return;
            }

            if (this.IsNullPawnDataType)
            {
                oraCom.Parameters.Add(
                    this.Name,
                    this.OracleType,
                    DBNull.Value,
                    this.Direction);
                return;
            }

            if (this.paramIsClob)
            {
                OracleParameter oraClobParam = oraCom.Parameters.Add(
                    this.Name,
                    this.OracleType,
                    this.Direction);

                //SR 5/13/2010 Pass the data as clob

                OracleClob clobColumn = new OracleClob(oraCom.Connection,false,false);
                clobColumn.Write(((string)this.parameterValues[0]).ToCharArray(), 0, ((string)this.parameterValues[0]).Length);
                oraClobParam.Value = clobColumn;
                return;
            }

            //Setup standard oracle parameter
            OracleParameter oraParam =
                oraCom.Parameters.Add(
                    this.Name,
                    this.OracleType,
                    this.Direction);
            //Check if we are to setup an array type
            if (this.IsArray)
            {
                //Set standard array type parameter values
                oraParam.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
                oraParam.Size = this.parameterValues.Count;

                switch (this.OracleType)
                {
                    case OracleDbType.Int32:
                        {
                            //Convert string array into int32 array
                            var paramValuesInt = new int[this.parameterValues.Count];
                            for (int i = 0; i < this.parameterValues.Count; ++i)
                            {
                                paramValuesInt[i] = Int32.Parse(this.parameterValues[i].ToString());
                            }
                            //Set value of param to the array
                            oraParam.Value = paramValuesInt;
                        }
                        break;
                    case OracleDbType.Int64:
                        {
                            //Convert string array into int64 array
                            var paramValuesInt64 = new Int64[this.parameterValues.Count];
                            for (int i = 0; i < this.parameterValues.Count; ++i)
                            {
                                paramValuesInt64[i] = Int64.Parse(this.parameterValues[i].ToString());
                            }
                            //Set value of param to the array
                            oraParam.Value = paramValuesInt64;
                        }
                        break;
                    case OracleDbType.Double:
                        {
                            //Parse double based parameter strings into their
                            //double equivalents
                            var paramValuesDouble = new double[this.parameterValues.Count];
                            for (int i = 0; i < parameterValues.Count; ++i)
                            {
                                paramValuesDouble[i] = Double.Parse(this.parameterValues[i].ToString());
                            }
                            //Set value of param to the array
                            oraParam.Value = paramValuesDouble;
                        }
                        break;
                    case OracleDbType.Decimal:
                        {
                            //Parse decimal based parameter strings into their
                            //decimal equivalents
                            var paramValuesDecimal = new decimal[this.parameterValues.Count];
                            for (int i = 0; i < parameterValues.Count; ++i)
                            {
                                paramValuesDecimal[i] = Decimal.Parse(this.parameterValues[i].ToString());
                            }
                            //Set value of param to the array
                            oraParam.Value = paramValuesDecimal;
                        }
                        break;
				    case OracleDbType.TimeStampTZ:
                    case OracleDbType.TimeStamp:
                            //Just put the timestamp string values into the oracle param
                            var paramTZValuesString = new string[this.parameterValues.Count];
                            for (int i = 0; i < parameterValues.Count; ++i)
                            {
                                paramTZValuesString[i] = (string)this.parameterValues[i];
                            }
                            //Set value of param to the array
                            oraParam.Value = paramTZValuesString;
					    break;
                    default:
                        if (this.parameterValues != null && this.parameterValues.Count > 0)
                        {
                            var paramValuesString = new string[this.parameterValues.Count];
                            for (int i = 0; i < parameterValues.Count; ++i)
                            {
                                paramValuesString[i] = (this.parameterValues[i] == null ? string.Empty : this.parameterValues[i].ToString());
                            }
                            oraParam.Value = paramValuesString;
                        }
                        else
                        {
                            var paramValuesString = new string[1];
                            paramValuesString[0] = string.Empty;
                            oraParam.Value = paramValuesString;
                        }
                        break;
                }
            }
            //If this is not an array, just set the value to the first string / converted value
            else
            {
                if (CollectionUtilities.isEmpty(this.parameterValues))
                {
                    oraParam.Value = DBNull.Value;
                    return;
                }

                switch (OracleType)
                {
                    case OracleDbType.Int32:
                        oraParam.Value = Int32.Parse(this.parameterValues[0].ToString());
                        break;
                    case OracleDbType.Int64:
                        oraParam.Value = Int64.Parse(this.parameterValues[0].ToString());
                        break;
                    case OracleDbType.Double:
                        oraParam.Value = Double.Parse(this.parameterValues[0].ToString());
                        break;
                    case OracleDbType.Decimal:
                        oraParam.Value = Decimal.Parse(this.parameterValues[0].ToString());
                        break;
                    default:
                        oraParam.Value = this.parameterValues[0];
                        break;
                }
            }
            return;
        }

        public string this[int i]
        {
            get
            {
                if (i < 0 || i >= this.parameterValues.Count)
                    return (string.Empty);
                if (this.parameterValues[i] != null)
                {
                    return (this.parameterValues[i].ToString());
                }
                return ("null");
            }
        }

        public bool IsNullPawnDataType
        {
            get
            {
                return ((this.dataTypeName == DataTypeConstants.PawnDataType.NULLDATE) ||
                        (this.dataTypeName == DataTypeConstants.PawnDataType.NULLNUMBER) ||
                        (this.dataTypeName == DataTypeConstants.PawnDataType.NULLSTRING));
            }
        }

        public bool IsListPawnDataType
        {
            get
            {
                return (
                       (this.dataTypeName == DataTypeConstants.PawnDataType.LISTFLOAT) ||
                       (this.dataTypeName == DataTypeConstants.PawnDataType.LISTINT) ||
                       (this.dataTypeName == DataTypeConstants.PawnDataType.LISTSTRING ||
                        this.dataTypeName == DataTypeConstants.PawnDataType.LISTTIMESTAMP));
            }
        }

        public bool IsArray
        {
            get
            {
                return ((this.IsListPawnDataType) && (this.parameterValues.Count >= 1));
            }
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        public override string ToString()
        {
            if (string.IsNullOrEmpty(this.Name))
            {
                return ("OracleProcParam: null");
            }
            var sB = new StringBuilder();
            sB.Append("OracleProcParam: Name: ");
            sB.Append(this.Name ?? "null" + ", Type: ");
            sB.Append(this.OracleType + ", IsClob: ");
            sB.Append(this.paramIsClob + ", Size: ");
            sB.Append(this.arraySize + ", Direction: ");
            sB.Append(this.Direction + ", DataName: ");
            sB.Append(this.DataName);
            sB.AppendLine();
            return (sB.ToString());
        }
    }
}
