using System.Data;
using Common.Libraries.Utility.Type;
using Oracle.DataAccess.Client;

namespace Common.Controllers.Database.Oracle
{
    public sealed class OracleDbTypeConstants
    {
        public readonly static PairType<SqlDbType, OracleDbType>[] SqlToOracleTypeArray = 
        {
            new PairType<SqlDbType, OracleDbType>(SqlDbType.BigInt,         OracleDbType.Int64),
            new PairType<SqlDbType, OracleDbType>(SqlDbType.Binary,         OracleDbType.Blob),
            new PairType<SqlDbType, OracleDbType>(SqlDbType.Char,           OracleDbType.Char),
            new PairType<SqlDbType, OracleDbType>(SqlDbType.Date,           OracleDbType.Date),
            new PairType<SqlDbType, OracleDbType>(SqlDbType.DateTime,       OracleDbType.TimeStamp),
            new PairType<SqlDbType, OracleDbType>(SqlDbType.DateTimeOffset, OracleDbType.TimeStampTZ),
            new PairType<SqlDbType, OracleDbType>(SqlDbType.Decimal,        OracleDbType.Decimal),
            new PairType<SqlDbType, OracleDbType>(SqlDbType.Float,          OracleDbType.Double),
            new PairType<SqlDbType, OracleDbType>(SqlDbType.Int,            OracleDbType.Int32),
            new PairType<SqlDbType, OracleDbType>(SqlDbType.Money,          OracleDbType.Decimal),
            new PairType<SqlDbType, OracleDbType>(SqlDbType.NChar,          OracleDbType.NChar),
            new PairType<SqlDbType, OracleDbType>(SqlDbType.NText,          OracleDbType.NClob),
            new PairType<SqlDbType, OracleDbType>(SqlDbType.NVarChar,       OracleDbType.NVarchar2),
            new PairType<SqlDbType, OracleDbType>(SqlDbType.Real,           OracleDbType.Double),
            new PairType<SqlDbType, OracleDbType>(SqlDbType.SmallDateTime,  OracleDbType.TimeStamp),
            new PairType<SqlDbType, OracleDbType>(SqlDbType.SmallInt,       OracleDbType.Int16),
            new PairType<SqlDbType, OracleDbType>(SqlDbType.SmallMoney,     OracleDbType.Decimal),
            new PairType<SqlDbType, OracleDbType>(SqlDbType.Structured,     OracleDbType.Raw),
            new PairType<SqlDbType, OracleDbType>(SqlDbType.Text,           OracleDbType.Clob),
            new PairType<SqlDbType, OracleDbType>(SqlDbType.Time,           OracleDbType.TimeStamp),
            new PairType<SqlDbType, OracleDbType>(SqlDbType.Timestamp,      OracleDbType.TimeStamp),
            new PairType<SqlDbType, OracleDbType>(SqlDbType.TinyInt,        OracleDbType.Byte),
            new PairType<SqlDbType, OracleDbType>(SqlDbType.VarBinary,      OracleDbType.Blob),
            new PairType<SqlDbType, OracleDbType>(SqlDbType.VarChar,        OracleDbType.Varchar2),
            new PairType<SqlDbType, OracleDbType>(SqlDbType.Variant,        OracleDbType.Raw),
            new PairType<SqlDbType, OracleDbType>(SqlDbType.Xml,            OracleDbType.XmlType)
        };
    }
}
