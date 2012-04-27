using System;
using System.Data;
using Common.Libraries.Utility.Type;

namespace Common.Libraries.Objects.Mapper
{
    public sealed class DataTypeConstants
    {
        public enum PawnDataType
        {
            WHOLENUMBER = 0,
            REALNUMBER  = 1,
            PERCENTAGE  = 2,
            FRACTION    = 3,
            STRING      = 4,
            DATE        = 5,
            DATETIME    = 6,
            LISTSTRING  = 7,
            LISTINT     = 8,
            LISTFLOAT   = 9,
            LISTTIMESTAMP = 10,
            LOANDESC    = 11,
            NUMBERDAYS  = 12,
            WEEKDAYNAME = 13,
            DAYOFMONTH  = 14,
            DAYOFWEEK   = 15,
            NUMBERWEEKS = 16,
            WEEKOFMONTH = 17,
            WEEKOFYEAR  = 18,
            MONTHNAME   = 19,
            MONTHOFYEAR = 20,
            NUMBERYEARS = 21,
            YEAR        = 22,
            NULLSTRING  = 23,
            NULLNUMBER  = 24,
            NULLDATE    = 25,
            CLOB        = 26,
            TIMESTAMP   = 27,
            DECIMAL     = 28,
            INVALID     = 29
        }

        public static readonly string[] DATATYPESTRINGS =
        {
            "WHOLENUMBER",
            "REALNUMBER",
            "PERCENTAGE",
            "FRACTION",
            "STRING",
            "DATE",
            "DATETIME",
            "LISTSTRING",
            "LISTINT",
            "LISTFLOAT",
            "LISTTIMESTAMP",
            "LOANDESC",
            "NUMBERDAYS",
            "WEEKDAYNAME",
            "DAYOFMONTH",
            "DAYOFWEEK",
            "NUMBERWEEKS",
            "WEEKOFMONTH",
            "WEEKOFYEAR",
            "MONTHNAME",
            "MONTHOFYEAR",
            "NUMBERYEARS",
            "YEAR",
            "NULLSTRING",
            "NULLNUMBER",
            "NULLDATE",
            "CLOB",
            "TIMESTAMP",
            "DECIMAL"
        };

        public static readonly PairType<PawnDataType, PairType<TypeCode, SqlDbType>>[] DATATYPES =
        {        
            new PairType<PawnDataType, PairType<TypeCode, SqlDbType>>(PawnDataType.WHOLENUMBER,
                new PairType<TypeCode, SqlDbType>(TypeCode.Int32, SqlDbType.Int)),
              
            new PairType<PawnDataType, PairType<TypeCode, SqlDbType>>(PawnDataType.REALNUMBER, 
                new PairType<TypeCode, SqlDbType>(TypeCode.Double, SqlDbType.Float)),
                
            new PairType<PawnDataType, PairType<TypeCode, SqlDbType>>(PawnDataType.PERCENTAGE, 
                new PairType<TypeCode, SqlDbType>(TypeCode.Double, SqlDbType.Float)),
        
            new PairType<PawnDataType, PairType<TypeCode, SqlDbType>>(PawnDataType.FRACTION, 
                new PairType<TypeCode, SqlDbType>(TypeCode.Double, SqlDbType.Float)),

            new PairType<PawnDataType, PairType<TypeCode, SqlDbType>>(PawnDataType.STRING, 
                new PairType<TypeCode, SqlDbType>(TypeCode.String, SqlDbType.VarChar)),

            new PairType<PawnDataType, PairType<TypeCode, SqlDbType>>(PawnDataType.DATE,
                new PairType<TypeCode, SqlDbType>(TypeCode.DateTime, SqlDbType.Date)),
                    
            new PairType<PawnDataType, PairType<TypeCode, SqlDbType>>(PawnDataType.DATETIME, 
                new PairType<TypeCode, SqlDbType>(TypeCode.DateTime, SqlDbType.DateTime)),

            new PairType<PawnDataType, PairType<TypeCode, SqlDbType>>(PawnDataType.LISTSTRING, 
                new PairType<TypeCode, SqlDbType>(TypeCode.String, SqlDbType.VarChar)),

            new PairType<PawnDataType, PairType<TypeCode, SqlDbType>>(PawnDataType.LISTINT, 
                new PairType<TypeCode, SqlDbType>(TypeCode.Int32, SqlDbType.Int)),

            new PairType<PawnDataType, PairType<TypeCode, SqlDbType>>(PawnDataType.LISTFLOAT, 
                new PairType<TypeCode, SqlDbType>(TypeCode.Double, SqlDbType.Float)),
        
            new PairType<PawnDataType, PairType<TypeCode, SqlDbType>>(PawnDataType.LOANDESC, 
                new PairType<TypeCode, SqlDbType>(TypeCode.String, SqlDbType.VarChar)),
        
            new PairType<PawnDataType, PairType<TypeCode, SqlDbType>>(PawnDataType.NUMBERDAYS, 
                new PairType<TypeCode, SqlDbType>(TypeCode.Int32, SqlDbType.Int)),
        
            new PairType<PawnDataType, PairType<TypeCode, SqlDbType>>(PawnDataType.WEEKDAYNAME, 
                new PairType<TypeCode, SqlDbType>(TypeCode.String, SqlDbType.VarChar)),
        
            new PairType<PawnDataType, PairType<TypeCode, SqlDbType>>(PawnDataType.DAYOFMONTH, 
                new PairType<TypeCode, SqlDbType>(TypeCode.Byte, SqlDbType.TinyInt)),
        
            new PairType<PawnDataType, PairType<TypeCode, SqlDbType>>(PawnDataType.DAYOFWEEK, 
                new PairType<TypeCode, SqlDbType>(TypeCode.Byte, SqlDbType.TinyInt)),

            new PairType<PawnDataType, PairType<TypeCode, SqlDbType>>(PawnDataType.NUMBERWEEKS, 
                new PairType<TypeCode, SqlDbType>(TypeCode.Byte, SqlDbType.TinyInt)),

            new PairType<PawnDataType, PairType<TypeCode, SqlDbType>>(PawnDataType.WEEKOFMONTH, 
                new PairType<TypeCode, SqlDbType>(TypeCode.Byte, SqlDbType.TinyInt)),

            new PairType<PawnDataType, PairType<TypeCode, SqlDbType>>(PawnDataType.WEEKOFYEAR, 
                new PairType<TypeCode, SqlDbType>(TypeCode.Byte, SqlDbType.TinyInt)),

            new PairType<PawnDataType, PairType<TypeCode, SqlDbType>>(PawnDataType.MONTHNAME, 
                new PairType<TypeCode, SqlDbType>(TypeCode.String, SqlDbType.VarChar)),

            new PairType<PawnDataType, PairType<TypeCode, SqlDbType>>(PawnDataType.MONTHOFYEAR, 
                new PairType<TypeCode, SqlDbType>(TypeCode.Byte, SqlDbType.TinyInt)),

            new PairType<PawnDataType, PairType<TypeCode, SqlDbType>>(PawnDataType.NUMBERYEARS, 
                new PairType<TypeCode, SqlDbType>(TypeCode.Int32, SqlDbType.Int)),

            new PairType<PawnDataType, PairType<TypeCode, SqlDbType>>(PawnDataType.YEAR, 
                new PairType<TypeCode, SqlDbType>(TypeCode.Int32, SqlDbType.Int)),

            new PairType<PawnDataType, PairType<TypeCode, SqlDbType>>(PawnDataType.NULLSTRING,
                new PairType<TypeCode, SqlDbType>(TypeCode.String, SqlDbType.VarChar)),

            new PairType<PawnDataType, PairType<TypeCode, SqlDbType>>(PawnDataType.NULLNUMBER, 
                new PairType<TypeCode, SqlDbType>(TypeCode.Int32, SqlDbType.Int)),

            new PairType<PawnDataType, PairType<TypeCode, SqlDbType>>(PawnDataType.NULLDATE, 
                new PairType<TypeCode, SqlDbType>(TypeCode.DateTime, SqlDbType.Date)),

            new PairType<PawnDataType, PairType<TypeCode, SqlDbType>>(PawnDataType.CLOB,
                new PairType<TypeCode, SqlDbType>(TypeCode.Object, SqlDbType.Text)),

            new PairType<PawnDataType, PairType<TypeCode, SqlDbType>>(PawnDataType.TIMESTAMP,
                new PairType<TypeCode, SqlDbType>(TypeCode.DateTime, SqlDbType.DateTime)),

            new PairType<PawnDataType, PairType<TypeCode, SqlDbType>>(PawnDataType.DECIMAL,
                new PairType<TypeCode, SqlDbType>(TypeCode.Decimal, SqlDbType.Decimal)),

            new PairType<PawnDataType, PairType<TypeCode, SqlDbType>>(PawnDataType.INVALID, 
                new PairType<TypeCode, SqlDbType>(TypeCode.DBNull, SqlDbType.Udt))
       };
    }
}
