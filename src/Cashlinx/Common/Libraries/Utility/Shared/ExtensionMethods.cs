using System;
using System.Collections.Generic;
using System.Linq;
using Common.Controllers.Database.Oracle;
using System.Data;

namespace Common.Libraries.Utility.Shared
{
    public static class ExtensionMethods
    {
        public static bool IsNullOrEmpty(this DataSet ds)
        {
            return ds == null ||
                !ds.IsInitialized ||
                ds.Tables.Count <= 0 ||
                ds.Tables[0].Rows.Count == 0;
        }

        public static Decimal RoundUp(this Decimal value, int precision)
        {
            if (precision <= 0)
            {
                return Math.Round(value);
            }

            var sValue = value.ToString(string.Format("f{0}", (precision + 1)));

            if (sValue[sValue.Length - 1] != '0')
            {
                sValue = sValue.Substring(0, sValue.Length - 1);
                sValue += "9";
            }

            return Math.Round(Convert.ToDecimal(sValue), precision);
        }

        public static void AddValues(this OracleProcParam opp, IEnumerable<object> values)
        {
            foreach (var o in values)
            {
                opp.AddValue(o);
            }
        }

        public static List<string> ToStringList(this IEnumerable<int> list)
        {
            return (from v in list
                    select v.ToString()).ToList();
        }

        public static List<string> ToStringList(this IEnumerable<decimal> list)
        {
            return (from v in list
                    select v.ToString()).ToList();
        }
    }
}
