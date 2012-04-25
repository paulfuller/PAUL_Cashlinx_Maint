using System;
using System.Collections.Generic;
using Common.Controllers.Application;
using Common.Controllers.Database.Procedures;
using Common.Controllers.Security;
using Common.Libraries.Objects.Business;
using Common.Libraries.Utility.Shared;

namespace Audit.Procedures
{
    public class AuditProcedures
    {
        public static readonly int MAX_MASKS = 15;
        public static readonly int OTHERDSC_CODE = 999;

        public static bool ProcessChargeonNewItems(DesktopSession desktopSession,List<Item> tempItems,string storeNumber)
        {
            foreach (Item pItem in tempItems)
            {
                if (pItem != null)
                {
                    QuickCheck pItemQInfo = pItem.QuickInformation;
                    Int64[] primaryMasks = getMasks(pItem);

                    //Insert MDSE record for this pawn item
                    //Calculate the cost amount of the item
                    //Requirement is that cost will be 65% of the amount entered as retail amount
                    //decimal itemCost = Utilities.GetDecimalValue(.65) * Utilities.GetDecimalValue(pItem.ItemAmount);
                    decimal itemCost = pItem.ItemAmount;
                    string errorCode;
                    string errorMessage;
                    bool curRetValue = ProcessTenderProcedures.ExecuteInsertMDSERecord(
                        pItem.mStore, pItem.mStore, pItem.mYear, pItem.mDocNumber,
                        "" + pItem.mDocType, 1, 0, pItem.CategoryCode,
                        "", itemCost,
                        0, pItemQInfo.Manufacturer,
                        pItemQInfo.Model, pItemQInfo.SerialNumber, pItemQInfo.Weight,
                        primaryMasks, pItem.TicketDescription, pItem.ItemAmount,
                        pItem.StorageFee, "AUDIT",
                        ShopDateTime.Instance.ShopDate.FormatDate(), ShopDateTime.Instance.ShopDate.ToShortDateString() + " " + ShopDateTime.Instance.ShopTime.ToString(), "", "", pItem.mDocType, "", out errorCode, out errorMessage);
                    if (!curRetValue)
                    {
                        return false;
                    }

                    foreach (ItemAttribute iAttr in pItem.Attributes)
                    {
                        if (iAttr.Answer.AnswerCode == OTHERDSC_CODE)
                        {
                            bool otherDscVal = ProcessTenderProcedures.ExecuteInsertOtherDscRecord(
                                storeNumber, pItem.mStore, pItem.mYear, pItem.mDocNumber,
                                "" + pItem.mDocType, 1, 0, iAttr.MaskOrder,
                                iAttr.Answer.AnswerText, "AUDIT", out errorCode, out errorMessage);
                            if (!otherDscVal)
                            {
                                return false;
                            }

                        }
                    }

                    MerchandiseProcedures.UpdateMerchandiseRetailPrice(desktopSession,
                                                                       new Icn(pItem.Icn), pItem.RetailPrice, out errorCode, out errorMessage, storeNumber);
                    if (errorCode != "0")
                        return false;
                    desktopSession.PrintTags(pItem, CurrentContext.AUDITCHARGEON);

                }
            }

            return true;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private static Int64[] getMasks(Item p)
        {
            if (p == null)
            {
                return (null);
            }

            var rt = new Int64[MAX_MASKS];
            //Initialize array with all zeroes or 
            //the proper mask value
            for (int i = 1; i <= MAX_MASKS; ++i)
            {
                rt[i - 1] = 0L;
                int i1 = i;
                int attrIdx = p.Attributes.FindIndex(itemAttrib => itemAttrib.MaskOrder == i1);
                if (attrIdx < 0)
                {
                    continue;
                }
                ItemAttribute pAttrib = p.Attributes[attrIdx];
                Int64 answerCode = pAttrib.Answer.AnswerCode;
                rt[i - 1] = answerCode;
            }
            return (rt);
        }

    }
}
