using System;
using System.Data;

namespace Support.Logic
{
    class OracleUtilities
    {

        public static string PadStringValue(string i_string, string i_pad)
        {
            return string.Format("{0} -- {1}", i_string, i_pad);
        }

        public static bool GetProcedureOutput(DataSet MyDataSet, Int32 ctr, out string OutString)
        {
            bool ReturnValue = false;
            OutString = string.Empty;
            if (MyDataSet != null)
            {
                if (MyDataSet.Tables.Count > 0)
                {
                    if (MyDataSet.Tables.Contains("OUTPUT"))
                    {
                        DataTable OutputDataTable = MyDataSet.Tables["OUTPUT"];
                        if (OutputDataTable.IsInitialized && OutputDataTable.Rows != null &&
                           OutputDataTable.Rows.Count > 0)
                        {
                            DataRow dr = OutputDataTable.Rows[ctr];
                            if (dr != null && !dr.HasErrors && dr.ItemArray.Length > 0)
                            {
                                var drvalue = dr.ItemArray.GetValue(1);
                                if (drvalue != null)
                                {
                                    OutString = Convert.ToString(drvalue);
                                    ReturnValue = true;
                                }
                            }
                        }
                    }
                }
            }
            return (ReturnValue);
        }

        public static bool GetProcedureOutput(DataSet MyDataSet, Int32 ctr, out Int64 OutNum)
        {
            bool ReturnValue = false;
            OutNum = Int64.MinValue;
            if (MyDataSet != null)
            {
                if (MyDataSet.Tables.Count > 0)
                {
                    if (MyDataSet.Tables.Contains("OUTPUT"))
                    {
                        DataTable OutputDataTable = MyDataSet.Tables["OUTPUT"];
                        if (OutputDataTable.IsInitialized && OutputDataTable.Rows != null &&
                           OutputDataTable.Rows.Count > 0)
                        {
                            DataRow dr = OutputDataTable.Rows[ctr];
                            if (dr != null && !dr.HasErrors && dr.ItemArray.Length > 0)
                            {
                                var drvalue = dr.ItemArray.GetValue(1);
                                if (drvalue != null)
                                {
                                    OutNum = Convert.ToInt64(drvalue);
                                    ReturnValue = true;
                                }
                            }
                        }
                    }
                }
            }
            return (ReturnValue);
        }

        public static bool GetProcedureOutput(DataSet MyDataSet, Int32 ctr, out Int32 OutNum)
        {
            bool ReturnValue = false;
            OutNum = Int32.MinValue;
            if (MyDataSet != null)
            {
                if (MyDataSet.Tables.Count > 0)
                {
                    if (MyDataSet.Tables.Contains("OUTPUT"))
                    {
                        DataTable OutputDataTable = MyDataSet.Tables["OUTPUT"];
                        if (OutputDataTable.IsInitialized && OutputDataTable.Rows != null &&
                           OutputDataTable.Rows.Count > 0)
                        {
                            DataRow dr = OutputDataTable.Rows[ctr];
                            if (dr != null && !dr.HasErrors && dr.ItemArray.Length > 0)
                            {
                                var drvalue = dr.ItemArray.GetValue(1);
                                if (drvalue != null)
                                {
                                    OutNum = Convert.ToInt32(drvalue);
                                    ReturnValue = true;
                                }
                            }
                        }
                    }
                }
            }
            return (ReturnValue);
        }

        public static bool GetProcedureOutput(DataSet MyDataSet, Int32 ctr, out DateTime OutDate)
        {
            bool ReturnValue = false;
            bool rt;
            OutDate = DateTime.MinValue;

            if (MyDataSet != null)
            {
                if (MyDataSet.Tables.Count > 0)
                {
                    if (MyDataSet.Tables.Contains("OUTPUT"))
                    {
                        DataTable OutputDataTable = MyDataSet.Tables["OUTPUT"];
                        if (OutputDataTable.IsInitialized && OutputDataTable.Rows != null &&
                           OutputDataTable.Rows.Count > 0)
                        {
                            DataRow dr = OutputDataTable.Rows[ctr];
                            if (dr != null && !dr.HasErrors && dr.ItemArray.Length > 0)
                            {
                                var drvalue = dr.ItemArray.GetValue(1);
                                if (drvalue != null)
                                {
                                    var DateTimeStr = Convert.ToString(drvalue);
                                    rt = DateTime.TryParse(DateTimeStr, out OutDate);
                                    ReturnValue = true;
                                }
                            }
                        }
                    }
                }
            }
            return (ReturnValue);
        }
    }
}
