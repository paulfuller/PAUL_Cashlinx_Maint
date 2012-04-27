using System;
using System.Data;

namespace Common.Controllers.Database
{
    public static class PawnDataAccessUtilities
    {
        public const string OUTPUTNAME = "OUTPUT";

        /// <summary>
        /// Validate the output table data set
        /// </summary>
        /// <param name="outputDataSet"></param>
        /// <param name="outputDataTable"></param>
        /// <returns></returns>
        public static bool IsOutputTableValid(DataSet outputDataSet, out DataTable outputDataTable)
        {
            var rt = false;
            outputDataTable = null;
            if (outputDataSet != null &&
                outputDataSet.Tables.Count > 0 &&
                outputDataSet.Tables.Contains(OUTPUTNAME))
            {
                outputDataTable = outputDataSet.Tables[OUTPUTNAME];
                if (outputDataTable != null)
                {
                    rt = true;
                }
            }

            return (rt);
        }

        /// <summary>
        /// Validate the named table data set
        /// </summary>
        /// <param name="dataSet"> </param>
        /// <param name="tableName"></param>
        /// <param name="dataTable"> </param>
        /// <returns></returns>
        public static bool IsTableValid(DataSet dataSet, string tableName, out DataTable dataTable)
        {
            dataTable = null;
            if (string.IsNullOrEmpty(tableName)) return (false);
            var rt = false;
            if (dataSet != null &&
                dataSet.Tables.Count > 0 &&
                dataSet.Tables.Contains(tableName))
            {
                dataTable = dataSet.Tables[OUTPUTNAME];
                if (dataTable != null)
                {
                    rt = true;
                }
            }

            return (rt);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="outputDataTable"></param>
        /// <param name="rowIdx"></param>
        /// <param name="outputObject"></param>
        /// <returns></returns>
        public static bool RetrieveObjectFromOutputTable(
            DataTable outputDataTable, 
            int rowIdx, 
            out object outputObject)
        {
            var rt = false;
            outputObject = null;

            if (outputDataTable.IsInitialized &&
                outputDataTable.Rows != null &&
                outputDataTable.Rows.Count > rowIdx)
            {
                DataRow dR = outputDataTable.Rows[rowIdx];
                if (dR != null && !dR.HasErrors &&
                    dR.ItemArray.Length > 1)
                {
                    outputObject = dR.ItemArray.GetValue(1);
                    if (outputObject != null)
                    {
                        rt = true;
                    }
                }
            }

            return (rt);
        }

        public static bool ValidateOutputTableAndRetrieveObject(
            DataSet outputDataSet, 
            int rowIdx, 
            out DataTable outputDataTable,
            out object outputDataObject)
        {
            var rt = false;
            outputDataTable = null;
            outputDataObject = null;

            if (IsOutputTableValid(outputDataSet, out outputDataTable))
            {
                if (RetrieveObjectFromOutputTable(outputDataTable, rowIdx, out outputDataObject))
                {
                    rt = true;
                }
            }

            return (rt);
        }

        public static string PadStringValue(
            string iString, 
            string iPad)
        {
            return string.Format("{0} -- {1}", iString, iPad);
        }

        public static bool GetProcedureOutput(DataSet dataSet, int rowIdx, out string outString)
        {
            bool returnValue = false;
            outString = string.Empty;
            if (dataSet != null)
            {
                if (dataSet.Tables.Count > 0)
                {
                    if (dataSet.Tables.Contains(OUTPUTNAME))
                    {
                        DataTable outputDataTable = dataSet.Tables[OUTPUTNAME];
                        if (outputDataTable.IsInitialized && outputDataTable.Rows != null &&
                            outputDataTable.Rows.Count > 0)
                        {
                            DataRow dr = outputDataTable.Rows[rowIdx];
                            if (dr != null && !dr.HasErrors && dr.ItemArray.Length > 0)
                            {
                                var drValue = dr.ItemArray.GetValue(1);
                                if (drValue != null)
                                {
                                    outString = Convert.ToString(drValue);
                                    returnValue = true;
                                }
                            }
                        }
                    }
                }
            }
            return (returnValue);
        }

        public static bool GetProcedureOutput(DataSet dataSet, int rowIdx, out Int64 outNum)
        {
            bool returnValue = false;
            outNum = Int64.MinValue;
            if (dataSet != null)
            {
                if (dataSet.Tables.Count > 0 && dataSet.Tables.Contains(OUTPUTNAME))
                {
                    DataTable outputDataTable = dataSet.Tables[OUTPUTNAME];
                    if (outputDataTable.IsInitialized && 
                        outputDataTable.Rows != null &&
                        outputDataTable.Rows.Count > 0)
                    {
                        DataRow dr = outputDataTable.Rows[rowIdx];
                        if (dr != null && 
                            !dr.HasErrors && 
                            dr.ItemArray.Length > 0)
                        {
                            var drValue = dr.ItemArray.GetValue(1);
                            if (drValue != null)
                            {
                                outNum = Convert.ToInt64(drValue);
                                returnValue = true;
                            }
                        }
                    }
                }
            }
            return (returnValue);
        }

        public static bool GetProcedureOutput(DataSet dataSet, Int32 rowIdx, out Int32 outNum)
        {
            bool returnValue = false;
            outNum = Int32.MinValue;
            if (dataSet != null)
            {
                if (dataSet.Tables.Count > 0 && dataSet.Tables.Contains(OUTPUTNAME))
                {
                    DataTable outputDataTable = dataSet.Tables[OUTPUTNAME];
                    if (outputDataTable.IsInitialized && outputDataTable.Rows != null &&
                        outputDataTable.Rows.Count > 0)
                    {
                        DataRow dr = outputDataTable.Rows[rowIdx];
                        if (dr != null && !dr.HasErrors && dr.ItemArray.Length > 0)
                        {
                            var drvalue = dr.ItemArray.GetValue(1);
                            if (drvalue != null)
                            {
                                outNum = Convert.ToInt32(drvalue);
                                returnValue = true;
                            }
                        }
                    }
                }
            }
            return (returnValue);
        }

        public static bool GetProcedureOutput(DataSet dataSet, Int32 ctr, out DateTime outDate)
        {
            bool ReturnValue = false;
            bool rt;
            outDate = DateTime.MinValue;

            if (dataSet != null)
            {
                if (dataSet.Tables.Count > 0)
                {
                    if (dataSet.Tables.Contains(OUTPUTNAME))
                    {
                        DataTable OutputDataTable = dataSet.Tables[OUTPUTNAME];
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
                                    rt = DateTime.TryParse(DateTimeStr, out outDate);
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
