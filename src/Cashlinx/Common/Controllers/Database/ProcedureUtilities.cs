#region FileHeaderRegion

// /************************************************************************
//  * Namespace: CashlinxDesktop.DesktopProcedures
//  * Class: ProcedureUtilities
//  * 
//  * Description: Common methods utilized throughout stored procedures
//  *              in the CashlinxDesktop
//  * 
//  * History:
//  * Date                Author            Reason                                 
//  *------------------------------------------------------------------
//  * 09/13/2009          GJL               Initial creation
//  * 
//  * **********************************************************************/

#endregion

using System;
using System.Collections.Generic;
using System.Data;
using Common.Controllers.Database.Oracle;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Mapper;
using Oracle.DataAccess.Client;

namespace Common.Controllers.Database
{
    public static class ProcedureUtilities
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="inParams"></param>
        /// <param name="refDatesPName"></param>
        /// <param name="refTimesPName"></param>
        /// <param name="refNumbersPName"></param>
        /// <param name="refTypesPName"></param>
        /// <param name="refEventsPName"></param>
        /// <param name="refAmountsPName"></param>
        /// <param name="refStoresPName"></param>
        /// <param name="recNumberPName"></param>
        /// <param name="receiptDetailsVO"></param>        
        public static void addReceiptDetailsToOraParamList(
            ref List<OracleProcParam> inParams,
            string refDatesPName,
            string refTimesPName,
            string refNumbersPName,
            string refTypesPName,
            string refEventsPName,
            string refAmountsPName,
            string refStoresPName,
            string recNumberPName,
            ReceiptDetailsVO receiptDetailsVO)
        {
            if (receiptDetailsVO != null)
                addReceiptDetailsToOraParamList(
                    ref inParams,
                    refDatesPName,
                    receiptDetailsVO.RefDates.ToArray(),
                    refTimesPName,
                    receiptDetailsVO.RefTimes.ToArray(),
                    refNumbersPName,
                    receiptDetailsVO.RefNumbers.ToArray(),
                    refTypesPName,
                    receiptDetailsVO.RefTypes.ToArray(),
                    refEventsPName,
                    receiptDetailsVO.RefEvents.ToArray(),
                    refAmountsPName,
                    receiptDetailsVO.RefAmounts.ToArray(),
                    refStoresPName,
                    receiptDetailsVO.RefStores.ToArray(),
                    recNumberPName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inParams"></param>
        /// <param name="refDatesPName"></param>
        /// <param name="refDates"></param>
        /// <param name="refTimesPName"></param>
        /// <param name="refTimes"></param>
        /// <param name="refNumbersPName"></param>
        /// <param name="refNumbers"></param>
        /// <param name="refTypesPName"></param>
        /// <param name="refTypes"></param>
        /// <param name="refEventsPName"></param>
        /// <param name="refEvents"></param>
        /// <param name="refAmountsPName"></param>
        /// <param name="refAmounts"></param>
        /// <param name="refStoresPName"></param>
        /// <param name="refStores"></param>
        /// <param name="recNumberPName"></param>
        public static void addReceiptDetailsToOraParamList(
            ref List<OracleProcParam> inParams,
            string refDatesPName,
            string[] refDates,
            string refTimesPName,
            string[] refTimes,
            string refNumbersPName,
            string[] refNumbers,
            string refTypesPName,
            string[] refTypes,
            string refEventsPName,
            string[] refEvents,
            string refAmountsPName,
            string[] refAmounts,
            string refStoresPName,
            string[] refStores,
            string recNumberPName)
        {
            if (!string.IsNullOrEmpty(refDatesPName))
            {
                var orpmRefDate = new OracleProcParam(
                    ParameterDirection.Input,
                    DataTypeConstants.PawnDataType.LISTSTRING, refDatesPName, refDates.Length);
                for (int i = 0; i < refDates.Length; i++)
                {
                    orpmRefDate.AddValue(refDates[i]);
                }
                inParams.Add(orpmRefDate);
            }

            if (!string.IsNullOrEmpty(refTimesPName))
            {
                var orpmRefTime = new OracleProcParam(
                    ParameterDirection.Input,
                    DataTypeConstants.PawnDataType.LISTSTRING, refTimesPName, refTimes.Length);
                for (int i = 0; i < refTimes.Length; i++)
                {
                    orpmRefTime.AddValue(refTimes[i]);
                }
                inParams.Add(orpmRefTime);
            }

            if (!string.IsNullOrEmpty(refNumbersPName))
            {
                var orpmRefNumber = new OracleProcParam(
                    ParameterDirection.Input,
                    DataTypeConstants.PawnDataType.LISTSTRING, refNumbersPName, refNumbers.Length);
                for (int i = 0; i < refNumbers.Length; i++)
                {
                    orpmRefNumber.AddValue(refNumbers[i]);
                }
                inParams.Add(orpmRefNumber);
            }

            if (!string.IsNullOrEmpty(refTypesPName))
            {
                var orpmRefType = new OracleProcParam(
                    ParameterDirection.Input,
                    DataTypeConstants.PawnDataType.LISTSTRING, refTypesPName, refTypes.Length);
                for (int i = 0; i < refTypes.Length; i++)
                {
                    orpmRefType.AddValue(refTypes[i]);
                }
                inParams.Add(orpmRefType);
            }

            if (!string.IsNullOrEmpty(refEventsPName))
            {
                var orpmRefEvent = new OracleProcParam(
                    ParameterDirection.Input,
                    DataTypeConstants.PawnDataType.LISTSTRING, refEventsPName, refEvents.Length);
                for (int i = 0; i < refEvents.Length; i++)
                {
                    orpmRefEvent.AddValue(refEvents[i]);
                }
                inParams.Add(orpmRefEvent);
            }

            if (!string.IsNullOrEmpty(refAmountsPName))
            {
                var orpmRefAmt = new OracleProcParam(
                    ParameterDirection.Input,
                    DataTypeConstants.PawnDataType.LISTSTRING, refAmountsPName, refAmounts.Length);
                for (int i = 0; i < refAmounts.Length; i++)
                {
                    orpmRefAmt.AddValue(refAmounts[i]);
                }
                inParams.Add(orpmRefAmt);
            }

            if (!string.IsNullOrEmpty(refStoresPName))
            {
                var orpmRefStore = new OracleProcParam(
                    ParameterDirection.Input,
                    DataTypeConstants.PawnDataType.LISTSTRING, refStoresPName, refStores.Length);
                for (int i = 0; i < refStores.Length; i++)
                {
                    orpmRefStore.AddValue(refStores[i]);
                }
                inParams.Add(orpmRefStore);
            }

            if (!string.IsNullOrEmpty(recNumberPName))
            {
                //Add output parameter
                inParams.Add(new OracleProcParam(recNumberPName,
                                                 OracleDbType.Decimal, 
                                                 DBNull.Value, ParameterDirection.Output, 1));
            }

        }

    }
}