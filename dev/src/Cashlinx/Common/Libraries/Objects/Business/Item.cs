/************************************************************************
 * Namespace:       PawnObjects.Pawn
 * Class:           PawnItem
 * 
 * Description      The class keeps the information of merchandise
 * 
 * History
 * David D Wise, 4-07-2009, Initial Development
 * PWNU00000677 SMurphy 4/28/2010 restricted length of TicketDescription to 200 char
 * SR 8/12/2010 Changed name from PawnItem to Item
 * **********************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using Common.Libraries.Objects.Retail;
using System.Text;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Shared;

namespace Common.Libraries.Objects.Business
{
    [Serializable]
    public class Item : IItem
    {
        public List<ItemAttribute>  Attributes                  {get; set;} //
        public int                  CaccLevel                   {get; set;} //
        public int                  CategoryCode                {get; set;}
        public int                  CategoryMask                {get; set;} //
        public string               CategoryDescription         {get; set;} //
        public string               Comment                     {get; set;} //
        public bool IsExpenseItem { get; set; }
        public Int64                GunNumber                   {get; set;}
        public bool                 HasGunLock                  {get; set;} //
        public string               HoldDesc                    {get; set;}
        public string               HoldType                    {get; set;}
        public string               Icn                         {get; set;}
        public bool                 IsJewelry                   {get; set;} //
        public bool                 IsGun                       {get; set;}
        public ItemReason           ItemReason                  {get; set;}
        public List<JewelrySet>     Jewelry                     {get; set;} //
        public decimal              ItemAmount                  {get; set;} //
        public decimal              ItemAmount_Original         {get; set;} // For ReDescribe
        public string               Location                    {get; set;}
        public bool                 Location_Assigned           {get; set;}
        public string               Location_Aisle              {get; set;}
        public string               Location_Shelf              {get; set;}
        public DateTime             Md_Date                     { get; set; }
        public int                  mDocNumber                  {get; set;} //
        public string               mDocType                    {get; set;} //
        public string               MerchandiseType             {get; set;} //
        public int                  mItemOrder                  {get; set;} //
        public int                  mStore                      {get; set;} //
        public int                  mYear                       {get; set;} //
        public ProductStatus       ItemStatus                  {get; set;}
        public decimal PfiAmount { get; set; }
        public PfiAssignment        PfiAssignmentType           {get; set;}
        public DateTime             PfiDate                     { get; set; }
        public int                  PfiTags                     {get; set;}
        public bool                 PfiVerified                 {get; set;}
        public int Quantity { get; set; }
        public int                  RefurbNumber                {get; set;}
        public decimal              RetailPrice                 {get; set;}
        public ProKnowMatch         SelectedProKnowMatch        {get; set;} //
        public DateTime             StatusDate                  {get; set;}
        public string               Tag                         {get; set;}
        public StateStatus          TempStatus                  {get; set;}
        public string JeweleryCaseNumber { get; set; }
        public string               TicketDescription                      
        {
            get{ return this.ticketDescription; }
            set
            {
                try
                { this.ticketDescription = value.Length > 200 ? value.Substring(0, 200) : value; }
                catch
                { this.ticketDescription = ""; }
            }
        }
        public decimal              TotalLoanGoldValue          {get; set;} //
        public decimal              TotalLoanStoneValue         {get; set;} //
        public decimal              StorageFee                  {get; set;} //
        public QuickCheck           QuickInformation            {get; set;}
        public List<string> SerialNumber { get; set; }
        public static string ItemLockedMessage 
        { 
            get
            {
                return "This record is being updated by another Cashlinx process. Please try again later."; 
            }
        }
        private string ticketDescription;
        /// <summary>
        /// 
        /// </summary>
        public Item()
        {
            this.CaccLevel = -1;
        }

        public static bool ItemLocked(Item item)
        {
            if (item.TempStatus == StateStatus.LSALE)
                return true;
            else
                return false;
        }

        public bool IsChargedOff()
        {
            return ItemReasonFactory.Instance.IsChargeOffCode(ItemReason);
        }

        public bool IsHandGun()
        {
            return IsGun && MerchandiseType == "H";
        }

        public bool IsLongGun()
        {
            return IsGun && MerchandiseType == "L";
        }

        public static void PawnItemMerge(ref RetailItem pawnItemStored, Item pawnItemDefaults, bool bIsReadOnly)
        {
            Item pawnItem = (Item)pawnItemStored;
            PawnItemMerge(ref pawnItem, pawnItemDefaults, bIsReadOnly);
        }

        public static void PawnItemMerge(ref Item pawnItemStored, Item pawnItemDefaults, bool bIsReadOnly)
        {
            if (pawnItemStored.Attributes != null)
            {
                foreach (ItemAttribute itemAttribute in pawnItemStored.Attributes)
                {
                    Answer answerSaved = itemAttribute.Answer;

                    int iDx = pawnItemDefaults.Attributes.FindIndex(delegate(ItemAttribute i)
                                                                    {
                                                                        return i.MaskOrder == itemAttribute.MaskOrder;
                                                                    });

                    if (iDx >= 0)
                    {
                        ItemAttribute foundItemAttribute = pawnItemDefaults.Attributes[iDx];
                        foundItemAttribute.Answer = answerSaved;
                        if(bIsReadOnly)
                            foundItemAttribute.IsPreAnswered = true;
                        else
                        {
                            foundItemAttribute.IsPreAnswered = (foundItemAttribute.AttributeCode == 1
                                                                || foundItemAttribute.AttributeCode == 3
                                                                || Utilities.GetStringValue(foundItemAttribute.Description).ToLower().EndsWith("manufacturer")
                                                                || Utilities.GetStringValue(foundItemAttribute.Description).ToLower().EndsWith("mfg.")
                                                                || Utilities.GetStringValue(foundItemAttribute.Description).ToLower().EndsWith("mfgr")
                                                                || Utilities.GetStringValue(foundItemAttribute.Description).ToLower().EndsWith("model")
                                                               );
                        }
                        pawnItemDefaults.Attributes.RemoveAt(iDx);
                        pawnItemDefaults.Attributes.Insert(iDx, foundItemAttribute);
                    }
                }
            }

            pawnItemDefaults.CaccLevel = pawnItemStored.CaccLevel;
            pawnItemDefaults.CategoryCode = pawnItemStored.CategoryCode;
            pawnItemDefaults.CategoryDescription = pawnItemStored.CategoryDescription;
//            pawnItemDefaults.CategoryMask = pawnItemStored.CategoryMask;
            pawnItemDefaults.PfiDate = pawnItemStored.PfiDate;
            pawnItemDefaults.Comment = pawnItemStored.Comment;
            pawnItemDefaults.HasGunLock = pawnItemStored.HasGunLock;
            pawnItemDefaults.HoldDesc = pawnItemStored.HoldDesc;
            pawnItemDefaults.HoldType = pawnItemStored.HoldType;
            pawnItemDefaults.Icn = pawnItemStored.Icn;
            pawnItemDefaults.IsGun = pawnItemStored.IsGun;
            pawnItemDefaults.GunNumber = pawnItemStored.GunNumber;
            pawnItemDefaults.IsJewelry = pawnItemStored.IsJewelry;
            pawnItemDefaults.Tag = pawnItemStored.Tag;
            pawnItemDefaults.PfiAmount = pawnItemStored.PfiAmount;//10.7 NAM:Added this line of code to cater for BZ 1290

            if (pawnItemStored.Jewelry != null)
            {
                if (pawnItemDefaults.Jewelry == null)
                    pawnItemDefaults.Jewelry = new List<JewelrySet>();

                for (int i = 0; i < pawnItemStored.Jewelry.Count; i++)
                {
                    if (i > 0)
                        pawnItemDefaults.Jewelry.Add(pawnItemDefaults.Jewelry[0]);

                    JewelrySet jewelrySet = new JewelrySet();//pawnItemDefaults.Jewelry[i];
                    List<ItemAttribute> itemAttributeList = new List<ItemAttribute>();
                    if (i > 0)
                    {
                        for (int k = 0; k < pawnItemDefaults.Jewelry[0].ItemAttributeList.Count; ++k)
                        {
                            ItemAttribute itemAttribute = new ItemAttribute();
                            itemAttribute.IsIncludedInDescription = pawnItemDefaults.Jewelry[0].ItemAttributeList.ElementAt(k).IsIncludedInDescription;
                            itemAttribute.Description = pawnItemDefaults.Jewelry[0].ItemAttributeList.ElementAt(k).Description;
                            itemAttribute.AttributeCode = pawnItemDefaults.Jewelry[0].ItemAttributeList.ElementAt(k).AttributeCode;
                            itemAttribute.MaskOrder = pawnItemDefaults.Jewelry[0].ItemAttributeList.ElementAt(k).MaskOrder;
                            itemAttributeList.Add(itemAttribute);
                        }
                        jewelrySet.ItemAttributeList = itemAttributeList;//pawnItemDefaults.Jewelry[0].ItemAttributeList;
                    }
                    else
                    {
                        jewelrySet.ItemAttributeList = pawnItemDefaults.Jewelry[0].ItemAttributeList;
                    }
                    
                    jewelrySet.CaccLevel = pawnItemStored.Jewelry[i].CaccLevel;
                    jewelrySet.Category = pawnItemStored.Jewelry[i].Category;
                    jewelrySet.CategoryDescription = pawnItemStored.Jewelry[i].CategoryDescription;
                    jewelrySet.Icn = pawnItemStored.Jewelry[i].Icn;
                    jewelrySet.SubItemNumber = pawnItemStored.Jewelry[i].SubItemNumber;
                    jewelrySet.TicketDescription = pawnItemStored.Jewelry[i].TicketDescription;
                    jewelrySet.TotalStoneValue = pawnItemStored.Jewelry[i].TotalStoneValue;

                    pawnItemDefaults.Jewelry.RemoveAt(i);
                    pawnItemDefaults.Jewelry.Insert(i, jewelrySet);

                    foreach (ItemAttribute itemAttribute in pawnItemStored.Jewelry[i].ItemAttributeList)
                    {
                        Answer answerSaved = itemAttribute.Answer;

                        int iDx = pawnItemDefaults.Jewelry[0].ItemAttributeList.FindIndex(delegate(ItemAttribute ia)
                        {
                            return ia.MaskOrder == itemAttribute.MaskOrder;
                        });

                        if (iDx >= 0)
                        {
                            ItemAttribute foundItemAttribute = pawnItemDefaults.Jewelry[i].ItemAttributeList[iDx];
                            foundItemAttribute.Answer = answerSaved;
                            pawnItemDefaults.Jewelry[i].ItemAttributeList.RemoveAt(iDx);
                            pawnItemDefaults.Jewelry[i].ItemAttributeList.Insert(iDx, foundItemAttribute);
                        }
                    }
                }
            }

            pawnItemDefaults.ItemReason = pawnItemStored.ItemReason;
            pawnItemDefaults.ItemAmount = pawnItemStored.ItemAmount;
            pawnItemDefaults.ItemAmount_Original = pawnItemStored.ItemAmount_Original;
            pawnItemDefaults.Location = pawnItemStored.Location;
            pawnItemDefaults.Location_Aisle = pawnItemStored.Location_Aisle;
            pawnItemDefaults.Location_Assigned = pawnItemStored.Location_Assigned;
            pawnItemDefaults.Location_Shelf = pawnItemStored.Location_Shelf;
            pawnItemDefaults.mDocNumber = pawnItemStored.mDocNumber;
            pawnItemDefaults.mDocType = pawnItemStored.mDocType;
  //          pawnItemDefaults.MerchandiseType = pawnItemStored.MerchandiseType;
            pawnItemDefaults.mItemOrder = pawnItemStored.mItemOrder;
            pawnItemDefaults.mStore = pawnItemStored.mStore;
            pawnItemDefaults.mYear = pawnItemStored.mYear;
            pawnItemDefaults.QuickInformation = pawnItemStored.QuickInformation;
            pawnItemDefaults.SelectedProKnowMatch = pawnItemStored.SelectedProKnowMatch;
            pawnItemDefaults.ItemStatus = pawnItemStored.ItemStatus;
            pawnItemDefaults.PfiAssignmentType = pawnItemStored.PfiAssignmentType;
            pawnItemDefaults.PfiTags = pawnItemStored.PfiTags;
            pawnItemDefaults.PfiVerified = pawnItemStored.PfiVerified;
            pawnItemDefaults.QuickInformation = pawnItemStored.QuickInformation;
            pawnItemDefaults.RefurbNumber = pawnItemStored.RefurbNumber;
            pawnItemDefaults.RetailPrice = pawnItemStored.RetailPrice;
            pawnItemDefaults.SelectedProKnowMatch = pawnItemStored.SelectedProKnowMatch;
            pawnItemDefaults.StatusDate = pawnItemStored.StatusDate;
            pawnItemDefaults.StorageFee = pawnItemStored.StorageFee;
            pawnItemDefaults.TempStatus = pawnItemStored.TempStatus;
            pawnItemDefaults.TicketDescription = pawnItemStored.TicketDescription;
            pawnItemDefaults.TotalLoanGoldValue = pawnItemStored.TotalLoanGoldValue;
            pawnItemDefaults.TotalLoanStoneValue = pawnItemStored.TotalLoanStoneValue;

            pawnItemStored = pawnItemDefaults;
            
        }

        public static void GenerateTicketDescription(ref Item pawnItem, out string sItemNumberPrefix, out string sDescriptionLabel)
        {
            string tmpDescriptionLabel = "";

            // Placeholder logic until connected to Oracle
            sDescriptionLabel = "";
            sItemNumberPrefix = "[" + pawnItem.mItemOrder.ToString() + "] ";
            sDescriptionLabel = pawnItem.CategoryDescription + ";";

            pawnItem.Attributes.ForEach(delegate(ItemAttribute foreachPawnItem)
            {

                if (foreachPawnItem.IsIncludedInDescription)
                {
                    
                    tmpDescriptionLabel += GetAttributeInfo(foreachPawnItem);
                }
            });

            if (pawnItem.IsJewelry)
            {
                pawnItem.Jewelry.ForEach(delegate(JewelrySet foreachJewelrySet)
                {
                    string sJewelryDescriptionLabel = "";

                    foreachJewelrySet.ItemAttributeList.ForEach(delegate(ItemAttribute foreachPawnItem)
                    {
                        if (foreachPawnItem.IsIncludedInDescription)
                        {
                            // FTN 3.3.a.ii.4.a.iii.1
                            if (!string.IsNullOrEmpty(foreachPawnItem.Answer.AnswerText))
                            {
                                if (foreachPawnItem.Prefix != "")
                                {
                                    tmpDescriptionLabel += " " + foreachPawnItem.Prefix;
                                    sJewelryDescriptionLabel += " " + foreachPawnItem.Prefix;
                                }

                                tmpDescriptionLabel += " " + foreachPawnItem.Answer.AnswerText;
                                sJewelryDescriptionLabel += " " + foreachPawnItem.Answer.AnswerText;

                                if (foreachPawnItem.Suffix != "")
                                {
                                    tmpDescriptionLabel += " " + foreachPawnItem.Suffix;
                                    sJewelryDescriptionLabel += " " + foreachPawnItem.Suffix;
                                }

                                tmpDescriptionLabel += ";";
                                sJewelryDescriptionLabel += ";";
                            }
                        }
                    });
                    if (sJewelryDescriptionLabel != "")
                    {
                        sJewelryDescriptionLabel = sJewelryDescriptionLabel.Replace("  ", " ");
                        foreachJewelrySet.TicketDescription = sJewelryDescriptionLabel;
                    }
                });
            }

            if (!string.IsNullOrEmpty(pawnItem.Comment))
                tmpDescriptionLabel += " " + pawnItem.Comment + ";";

            sDescriptionLabel += tmpDescriptionLabel;
            sDescriptionLabel = sDescriptionLabel.Replace("  ", " ");

            if (sDescriptionLabel != "")
            {
                pawnItem.TicketDescription = sDescriptionLabel;
            }
        }

        public static void RemoveSerialNumberFromDescription(ref Item _item, out string sItemPrefix,out string sDescriptionLabel)
        {
            List<ItemAttribute> itemAttribs=_item.Attributes;
            ItemAttribute itemAttribSerialNumber = itemAttribs.Find(
                            ia => ia.Description == "Serial Number");
                int iSerialNumberIdx = itemAttribs.FindIndex(a => a.MdseField == 'S');
                if (iSerialNumberIdx >= 0)
                {
                    itemAttribSerialNumber.Answer.AnswerText = "";
                    itemAttribs.RemoveAt(iSerialNumberIdx);
                    itemAttribs.Insert(iSerialNumberIdx, itemAttribSerialNumber);
                }
                _item.Attributes = itemAttribs;
            
            GenerateTicketDescription(ref _item, out sItemPrefix, out sDescriptionLabel);
        }

        public static void GenerateShortTicketDescription(ref Item pawnItem, out string sItemNumberPrefix, out string sDescriptionLabel)
        {
            string tmpDescriptionLabel = "";

            // Placeholder logic until connected to Oracle
            sDescriptionLabel = "";
            sItemNumberPrefix = "[" + pawnItem.mItemOrder.ToString() + "] ";
            sDescriptionLabel = pawnItem.CategoryDescription + ";";

            if (!pawnItem.IsJewelry)
            {
                int iManufacturerIdx = pawnItem.Attributes.FindIndex(a => a.MdseField == 'M');
                int iModelIdx = pawnItem.Attributes.FindIndex(a => a.MdseField == 'D');
                int iSerialNumberIdx = pawnItem.Attributes.FindIndex(a => a.MdseField == 'S');

                if (iManufacturerIdx >= 0)
                    tmpDescriptionLabel += GetAttributeInfo(pawnItem.Attributes[iManufacturerIdx]);
                if (iModelIdx >= 0)
                    tmpDescriptionLabel += GetAttributeInfo(pawnItem.Attributes[iModelIdx]);
                if (iSerialNumberIdx >= 0)
                    tmpDescriptionLabel += GetAttributeInfo(pawnItem.Attributes[iSerialNumberIdx]);
            }
            else
            {
                int iKaratIdx = pawnItem.Attributes.FindIndex(a => a.AttributeCode == 9);
                int iGramsIdx = pawnItem.Attributes.FindIndex(a => a.AttributeCode == 10);

                if (iKaratIdx >= 0)
                    tmpDescriptionLabel += GetAttributeInfo(pawnItem.Attributes[iKaratIdx]);
                if (iGramsIdx >= 0)
                    tmpDescriptionLabel += GetAttributeInfo(pawnItem.Attributes[iGramsIdx]);

                decimal dTotalPoints = 0;
                string sPrefix = "";
                string sSuffix = "";

                pawnItem.Jewelry.ForEach(delegate(JewelrySet foreachJewelrySet)
                {
                    int iPointsIdx = foreachJewelrySet.ItemAttributeList.FindIndex(a => a.AttributeCode == 14);
                    if (iPointsIdx >= 0)
                    {
                        sPrefix = foreachJewelrySet.ItemAttributeList[iPointsIdx].Prefix;
                        dTotalPoints += Utilities.GetDecimalValue(foreachJewelrySet.ItemAttributeList[iPointsIdx].Answer.AnswerText, 0);
                        sSuffix = foreachJewelrySet.ItemAttributeList[iPointsIdx].Suffix;
                    }
                });

                if (dTotalPoints > 0)
                {
                    if (sPrefix != "")
                        tmpDescriptionLabel += " " + sPrefix;
                    tmpDescriptionLabel += " " + dTotalPoints.ToString();
                    if (sSuffix != "")
                        tmpDescriptionLabel += " " + sSuffix;
                }
            }

            sDescriptionLabel += tmpDescriptionLabel;
            sDescriptionLabel = sDescriptionLabel.Replace("  ", " ");

            if (sDescriptionLabel != "")
            {
                pawnItem.TicketDescription = sDescriptionLabel;
            }
        }

        private static string GetAttributeInfo(ItemAttribute itemAttribute)
        {
            string sAttributeInfo = "";

            if (!string.IsNullOrEmpty(itemAttribute.Answer.AnswerText))
            {
                if (itemAttribute.Prefix != "")
                    sAttributeInfo += " " + itemAttribute.Prefix;

                sAttributeInfo += " " + itemAttribute.Answer.AnswerText;

                if (itemAttribute.Suffix != "")
                    if (itemAttribute.Suffix.StartsWith("\""))
                        sAttributeInfo += itemAttribute.Suffix;
                    else
                        sAttributeInfo += " " + itemAttribute.Suffix;

                sAttributeInfo += ";";
            }
            return sAttributeInfo;
        }

        public string GetFullLocation()
        {
            StringBuilder location = new StringBuilder();
            if (!string.IsNullOrEmpty(Location_Aisle))
            {
                location.AppendFormat("{0} ", Location_Aisle);
            }

            if (!string.IsNullOrEmpty(Location_Shelf))
            {
                location.AppendFormat("{0} ", Location_Shelf);
            }

            if (!string.IsNullOrEmpty(Location))
            {
                location.AppendFormat("{0} ", Location);
            }

            return location.ToString();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IItem
    {
        //Added this interface so that ScrapItem could be
        //used in certain method calls as well that required
        //an Item parameter -- it can now require an IItem.

        List<ItemAttribute> Attributes { get; set; } //
        int CaccLevel { get; set; } //
        int CategoryCode { get; set; }
        int CategoryMask { get; set; } //
        string CategoryDescription { get; set; } //
        string Comment { get; set; } //
        bool IsExpenseItem { get; set; }
        Int64 GunNumber { get; set; }
        bool HasGunLock { get; set; } //
        string HoldDesc { get; set; }
        string HoldType { get; set; }
        string Icn { get; set; }
        bool IsJewelry { get; set; } //
        bool IsGun { get; set; }
        ItemReason ItemReason { get; set; }
        List<JewelrySet> Jewelry { get; set; } //
        decimal ItemAmount { get; set; } //
        decimal ItemAmount_Original { get; set; } // For ReDescribe
        string Location { get; set; }
        bool Location_Assigned { get; set; }
        string Location_Aisle { get; set; }
        string Location_Shelf { get; set; }
        int mDocNumber { get; set; } //
        string mDocType { get; set; } //
        string MerchandiseType { get; set; } //
        int mItemOrder { get; set; } //
        int mStore { get; set; } //
        int mYear { get; set; } //
        ProductStatus ItemStatus { get; set; }
        PfiAssignment PfiAssignmentType { get; set; }
        int PfiTags { get; set; }
        bool PfiVerified { get; set; }
        int Quantity { get; set; }
        int RefurbNumber { get; set; }
        decimal RetailPrice { get; set; }
        ProKnowMatch SelectedProKnowMatch { get; set; } //
        DateTime StatusDate { get; set; }
        string Tag { get; set; }
        StateStatus TempStatus { get; set; }
        string TicketDescription
        {
            get;
            set;
        }
        decimal TotalLoanGoldValue { get; set; } //
        decimal TotalLoanStoneValue { get; set; } //
        decimal StorageFee { get; set; } //
        QuickCheck QuickInformation { get; set; }
        List<string> SerialNumber { get; set; }

        //string ticketDescription;

        bool IsChargedOff();
    }

    [Serializable]
    public class ScrapItem : Item
    {
        #region Private Members

        private string _typeOfMetal = String.Empty;
        private string _approxKarats = String.Empty;
        private string _approxGrams = String.Empty;

        private const string _TYPE_OF_METAL = "TYPE OF METAL";
        private const string _APPROX_KARATS = "APPROX. KARAT";
        private const string _APPROX_GRAMS = "APPROX. GRAMS";
        
        #endregion Private Members

        #region Public Properties

        public string JewelryCaseNumber { get; set; } //For transfers.
        public string ReferencedTicketNumber { get; set; }
        public string Comments { get; set; }
        public int TransferNumber { get; set; }
        public int TicketNumber { get; set; }
        public string StoreNumber { get; set; }

        /// <summary>
        /// Type of metal pulled from the items attributes.
        /// </summary>
        public string TypeOfMetal
        {
            get
            {
                if (_typeOfMetal == String.Empty)
                {
                    foreach (ItemAttribute i in this.Attributes)
                    {
                        if (i.Description.ToUpper() == _TYPE_OF_METAL)
                        {
                            _typeOfMetal = i.Answer.AnswerText;
                            break;
                        }
                    }
                }

                return _typeOfMetal;
            }

        }

        /// <summary>
        /// Approximate number of karats for the metal pulled from the 
        /// attributes, if any exists.
        /// </summary>
        public string ApproximateKarats
        {
            get
            {
                if (_approxKarats == string.Empty)
                {
                    foreach (ItemAttribute i in this.Attributes)
                    {
                        if (i.Description.ToUpper() == _APPROX_KARATS)
                        {
                            _approxKarats = i.Answer.AnswerText;
                            break;
                        }
                    }

                }
                return _approxKarats;
            }
        }

        /// <summary>
        /// Approximate weight in grams for the metal pulled from the 
        /// attributes of the item. 
        /// </summary>
        public string ApproximateWeight
        {
            get
            {
                if (_approxGrams == string.Empty)
                {
                    foreach (ItemAttribute i in this.Attributes)
                    {
                        if (i.Description.ToUpper() == _APPROX_GRAMS)
                        {
                            _approxGrams = i.Answer.AnswerText;
                            break;
                        }
                    }

                }
                return _approxGrams;
            }

        }

        #endregion Public Properties
    }
}
