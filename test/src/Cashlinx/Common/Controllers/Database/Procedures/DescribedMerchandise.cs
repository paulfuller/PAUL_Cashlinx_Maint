/************************************************************************
* Namespace:       CashlinxDesktop.Panels.Includes
* Class:           DescribedMerchandise
* 
* Description      Class is responsible to retrieve the data and 
*                  populate the CategoryAttribute
* 
* History
* David D Wise, 3-13-2009, Initial Development
* 
* **********************************************************************/

using System;
using System.Collections.Generic;
using System.Data;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Business;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Shared;

namespace Common.Controllers.Database.Procedures
{
    /// <summary>
    /// Class is responsible to retrieve the data and populate the PawnItem object
    /// </summary>
    public class DescribedMerchandise
    {
        /// <summary>
        /// Retrieved Pawn Item
        /// </summary>
        public Item SelectedPawnItem { get; set; }

        /// <summary>
        /// Keeps the category code
        /// </summary>
        public int CaccLevel { get; set; }

        /// <summary>
        /// Keeps the category mask code
        /// </summary>
        public int MaskLevel { get; set; }

        /// <summary>
        /// Flag indicating existence
        /// </summary>
        public bool Exists { get; private set; }

        /// <summary>
        /// Provide flag indicating an Error occurred
        /// </summary>
        public bool Error { get; private set; }

        /// <summary>
        /// Text message of Error if occurred
        /// </summary>
        public string ErrorMessage { get; private set; }

        public DescribedMerchandise()
        {
            Exists = false;
            Error = false;
            ErrorMessage = "";
        }

        /// <summary>
        /// Retrieves the data from the database
        /// </summary>
        /// <param name="iMaskPointer"></param>
        /// <param name="iCategoryCode"></param>
        /// <returns></returns>
        public DescribedMerchandise(int iMaskPointer)
        {
            Exists = false;
            Error = false;
            ErrorMessage = "";
            MaskLevel = iMaskPointer;

            if (iMaskPointer == 0)
            {
                Error = true;
                ErrorMessage = "Valid MaskPointer was not passed by calling Method.";
            }

            try
            {
                DataSet generalCategoryDataSet = new DataSet(MerchandiseProcedures.GENERAL);

                DataTable dtCategory = new DataTable(MerchandiseProcedures.CAT_GENERAL);
                DataTable dtMasks = new DataTable(MerchandiseProcedures.CAT_MASK_GENERAL);
                DataTable dtAttribs = new DataTable(MerchandiseProcedures.CAT_MASK_ATTR_GENERAL);
                DataTable dtAva = new DataTable(MerchandiseProcedures.CAT_MASK_ATTR_AVA_GENERAL);
                DataTable dtAnswers = new DataTable(MerchandiseProcedures.CAT_MASK_ATTR_AVA_ANSWER_GENERAL);

                DataTable dtCategory_Jewelry = new DataTable(MerchandiseProcedures.CAT_JEWELRY);
                DataTable dtMasks_Jewelry = new DataTable(MerchandiseProcedures.CAT_MASK_JEWELRY);
                DataTable dtAttribs_Jewelry = new DataTable(MerchandiseProcedures.CAT_MASK_ATTR_JEWELRY);
                DataTable dtAva_Jewelry = new DataTable(MerchandiseProcedures.CAT_MASK_ATTR_AVA_JEWELRY);
                DataTable dtAnswers_Jewelry = new DataTable(MerchandiseProcedures.CAT_MASK_ATTR_AVA_ANSWER_JEWELRY);

                //[FTN:  3.c]       Create ItemAttribute and Populate    
                SelectedPawnItem = new Item();
                bool bHasJewelry = false;
                string sErrorCode = "";
                string sErrorMessage = "";

                // Call to class for CAT5 Stored Procedure
                if (MerchandiseProcedures.ExecuteGetCat5Info(MaskLevel, out generalCategoryDataSet, out bHasJewelry, out sErrorCode, out sErrorMessage))
                {
                    if (sErrorCode != "")
                    {
                        dtCategory = generalCategoryDataSet.Tables[MerchandiseProcedures.CAT_GENERAL];
                        dtMasks = generalCategoryDataSet.Tables[MerchandiseProcedures.CAT_MASK_GENERAL];
                        dtAttribs = generalCategoryDataSet.Tables[MerchandiseProcedures.CAT_MASK_ATTR_GENERAL];
                        dtAva = generalCategoryDataSet.Tables[MerchandiseProcedures.CAT_MASK_ATTR_AVA_GENERAL];
                        dtAnswers = generalCategoryDataSet.Tables[MerchandiseProcedures.CAT_MASK_ATTR_AVA_ANSWER_GENERAL];

                        if (bHasJewelry)
                        {
                            dtCategory_Jewelry = generalCategoryDataSet.Tables[MerchandiseProcedures.CAT_JEWELRY];
                            dtMasks_Jewelry = generalCategoryDataSet.Tables[MerchandiseProcedures.CAT_MASK_JEWELRY];
                            dtAttribs_Jewelry = generalCategoryDataSet.Tables[MerchandiseProcedures.CAT_MASK_ATTR_JEWELRY];
                            dtAva_Jewelry = generalCategoryDataSet.Tables[MerchandiseProcedures.CAT_MASK_ATTR_AVA_JEWELRY];
                            dtAnswers_Jewelry = generalCategoryDataSet.Tables[MerchandiseProcedures.CAT_MASK_ATTR_AVA_ANSWER_JEWELRY];
                        }

                        if (dtCategory != null)
                        {
                            SelectedPawnItem.CategoryMask = Utilities.GetIntegerValue(dtCategory.Rows[0]["MASK_POINTER"], 0);
                            SelectedPawnItem.CategoryDescription = Utilities.GetStringValue(dtCategory.Rows[0]["CAT_DESC2"]);
                            SelectedPawnItem.CaccLevel = Utilities.GetIntegerValue(dtCategory.Rows[0]["CACC_LEV"], -1);
                            SelectedPawnItem.IsJewelry = bHasJewelry;
                            SelectedPawnItem.MerchandiseType = Utilities.GetStringValue(dtCategory.Rows[0]["MD_TYPE"]);
                        }

                        //[FTN:  3.c.v]     Create new ItemAttribute List
                        SelectedPawnItem.Attributes = new List<ItemAttribute>();

                        #region FTN 3.e
                        if (dtMasks != null)
                        {
                            foreach (DataRow drMaskRow in dtMasks.Rows)
                            {
                                //[FTN:  3.e.i]             Create ItemAttribute and Populate    
                                ItemAttribute maskItemAttribute = new ItemAttribute();
                                maskItemAttribute.AttributeCode = Utilities.GetIntegerValue(drMaskRow["ATTRIBUTE"], 0);
                                maskItemAttribute.DescriptionOrder = Utilities.GetIntegerValue(drMaskRow["DESC_SEQ"], 0);
                                maskItemAttribute.LoanOrder = Utilities.GetIntegerValue(drMaskRow["LOAN_SEQ"], 0);
                                maskItemAttribute.MaskLevel = Utilities.GetIntegerValue(drMaskRow["MASK_LEVEL"], 0);
                                maskItemAttribute.MaskOrder = Utilities.GetIntegerValue(drMaskRow["MASK_SEQ"], 0);
                                maskItemAttribute.MaskDefault = Utilities.GetStringValue(drMaskRow["MASK_DEFAULT"]);
                                maskItemAttribute.IsPreAnswered = false;
                                maskItemAttribute.ValidationDataType = Utilities.GetStringValue(drMaskRow["ANSWER_TYPE"]);
                                maskItemAttribute.IsIncludedInDescription = Utilities.GetStringValue(drMaskRow["INCL_DESC"]) == "Y" ? true : false;
                                maskItemAttribute.IsRequired = Utilities.GetStringValue(drMaskRow["ANS_REQUIRED"]) == "Y" ? true : false;
                                maskItemAttribute.IsRestricted = Utilities.GetStringValue(drMaskRow["ANS_RESTRICT"]) == "Y" ? true : false;

                                if (dtAttribs != null)
                                {
                                    //[FTN:  3.e.i.13]              Retrieve Attribute Description 
                                    DataRow[] drsGeneralAttribs = dtAttribs.Select("ATTR_ID=" + maskItemAttribute.AttributeCode.ToString());

                                    if (drsGeneralAttribs.Length > 0)
                                    {
                                        maskItemAttribute.Description = Utilities.GetStringValue(drsGeneralAttribs[0]["ATTR_DESC"]);
                                        maskItemAttribute.Prefix = Utilities.GetStringValue(drsGeneralAttribs[0]["PREFIX"]);
                                        maskItemAttribute.Suffix = Utilities.GetStringValue(drsGeneralAttribs[0]["SUFFIX"]);
                                        maskItemAttribute.InputType = Utilities.GetStringValue(drsGeneralAttribs[0]["INPUT_TYPE"]);
                                        maskItemAttribute.MdseField = Utilities.GetCharValue(drsGeneralAttribs[0]["MDSE_FIELD"], ' ');

                                        //[FTN:  3.e.i.14]                      Create List<Answer> to populate from AVA Table 
                                        maskItemAttribute.AnswerList = new List<Answer>();

                                        if (dtAva != null)
                                        {
                                            //[FTN:  3.e.i.15]                      Filter to only matching Answers.  
                                            DataRow[] drsAvaTable = dtAva.Select("CATG=" + SelectedPawnItem.CategoryMask.ToString() + " AND ATTR_ID=" + maskItemAttribute.AttributeCode.ToString());

                                            foreach (DataRow drAvaTable in drsAvaTable)
                                            {
                                                //[FTN:  3.e.i.15.a]                        Create Answer object and populate  
                                                Answer attributeAnswer = new Answer();
                                                attributeAnswer.AnswerCode = Utilities.GetIntegerValue(drAvaTable["ANS_ID"], 0);
                                                attributeAnswer.DisplayOrder = Utilities.GetIntegerValue(drAvaTable["DISP_SORT"], 0);

                                                if (dtAnswers != null)
                                                {
                                                    //[FTN:  3.e.i.15.b]                            Filter to only matching Answer Code record from ANSWERS
                                                    DataRow[] drsAnswerTable = dtAnswers.Select("ANS_ID=" + attributeAnswer.AnswerCode.ToString());

                                                    if (drsAnswerTable.Length > 0)
                                                    {
                                                        attributeAnswer.AnswerText = Utilities.GetStringValue(drsAnswerTable[0]["ANS_DESC"]);
                                                        attributeAnswer.InputKey = Utilities.GetStringValue(drsAnswerTable[0]["INPUT_KEY"]);
                                                        attributeAnswer.OutputKey = Utilities.GetStringValue(drsAnswerTable[0]["OUTPUT_KEY"]);
                                                    }
                                                }
                                                //[FTN:  3.e.i.15.c]                        Add attributeAnswer to ItemAttribute
                                                maskItemAttribute.AnswerList.Add(attributeAnswer);
                                            }
                                        }
                                    }
                                }

                                //[FTN:  3.e.i.12]          Assign Control Type  
                                if (maskItemAttribute.AnswerList.Count == 0 && !maskItemAttribute.IsRestricted)
                                    maskItemAttribute.InputControl = ControlType.TEXTFIELD;
                                else if (maskItemAttribute.AnswerList.Count > 0 && !maskItemAttribute.IsRestricted)
                                    maskItemAttribute.InputControl = ControlType.COMBOBOX_TEXT_ENABLED;
                                else if (maskItemAttribute.AnswerList.Count > 0 && maskItemAttribute.IsRestricted)
                                    maskItemAttribute.InputControl = ControlType.COMBOBOX_ONLY;

                                //[FTN:  3.e.ii]            Add maskItemAttribute to PawnItem
                                SelectedPawnItem.Attributes.Add(maskItemAttribute);
                            }
                        }
                        #endregion

                        #region FTN 3.f
                        if (SelectedPawnItem.IsJewelry)
                        {
                            SelectedPawnItem.Jewelry = new List<JewelrySet>();
                            JewelrySet itemJewelrySet = new JewelrySet();

                            if (dtCategory_Jewelry != null)
                            {
                                itemJewelrySet.Category = Utilities.GetIntegerValue(dtCategory_Jewelry.Rows[0]["MASK_POINTER"], 0);
                                itemJewelrySet.CategoryDescription = Utilities.GetStringValue(dtCategory_Jewelry.Rows[0]["CAT_DESC2"]);
                                itemJewelrySet.CaccLevel = Utilities.GetIntegerValue(dtCategory_Jewelry.Rows[0]["CACC_LEV"], -1);
                            }

                            itemJewelrySet.ItemAttributeList = new List<ItemAttribute>();

                            foreach (DataRow drMask_JewelryRow in dtMasks_Jewelry.Rows)
                            {
                                //[FTN:  3.f.ii.1]                      Create ItemAttribute and Populate    
                                ItemAttribute maskItemAttribute = new ItemAttribute();
                                maskItemAttribute.AttributeCode = Utilities.GetIntegerValue(drMask_JewelryRow["ATTRIBUTE"], 0);
                                maskItemAttribute.DescriptionOrder = Utilities.GetIntegerValue(drMask_JewelryRow["DESC_SEQ"], 0);
                                maskItemAttribute.LoanOrder = Utilities.GetIntegerValue(drMask_JewelryRow["LOAN_SEQ"], 0);
                                maskItemAttribute.MaskLevel = Utilities.GetIntegerValue(drMask_JewelryRow["MASK_LEVEL"], 0);
                                maskItemAttribute.MaskOrder = Utilities.GetIntegerValue(drMask_JewelryRow["MASK_SEQ"], 0);
                                maskItemAttribute.MaskDefault = Utilities.GetStringValue(drMask_JewelryRow["MASK_DEFAULT"]);
                                maskItemAttribute.ValidationDataType = Utilities.GetStringValue(drMask_JewelryRow["ANSWER_TYPE"]);
                                maskItemAttribute.IsIncludedInDescription = Utilities.GetStringValue(drMask_JewelryRow["INCL_DESC"]) == "Y" ? true : false;
                                maskItemAttribute.IsRequired = Utilities.GetStringValue(drMask_JewelryRow["ANS_REQUIRED"]) == "Y" ? true : false;
                                maskItemAttribute.IsRestricted = Utilities.GetStringValue(drMask_JewelryRow["ANS_RESTRICT"]) == "Y" ? true : false;

                                if (dtAttribs_Jewelry != null)
                                {
                                    //[FTN:  3.f.ii.2]                  Retrieve Attribute Description 
                                    DataRow[] drsGeneralAttribs = dtAttribs_Jewelry.Select("ATTR_ID=" + maskItemAttribute.AttributeCode.ToString());

                                    if (drsGeneralAttribs.Length > 0)
                                    {
                                        maskItemAttribute.Description = Utilities.GetStringValue(drsGeneralAttribs[0]["ATTR_DESC"]);
                                        maskItemAttribute.Prefix = Utilities.GetStringValue(drsGeneralAttribs[0]["PREFIX"]);
                                        maskItemAttribute.Suffix = Utilities.GetStringValue(drsGeneralAttribs[0]["SUFFIX"]);
                                        maskItemAttribute.InputType = Utilities.GetStringValue(drsGeneralAttribs[0]["INPUT_TYPE"]);
                                        maskItemAttribute.MdseField = Utilities.GetCharValue(drsGeneralAttribs[0]["MDSE_FIELD"], ' ');

                                        //[FTN:  3.f.ii.3]                      Create List<Answer> to populate from AVA Table 
                                        maskItemAttribute.AnswerList = new List<Answer>();

                                        if (dtAva_Jewelry != null)
                                        {
                                            //[FTN:  3.f.ii.4]                      Filter to only matching Answers.  
                                            DataRow[] drsAvaTable = dtAva_Jewelry.Select("CATG=" + itemJewelrySet.Category.ToString() + " AND ATTR_ID=" + maskItemAttribute.AttributeCode.ToString());

                                            foreach (DataRow drAvaTable in drsAvaTable)
                                            {
                                                //[FTN:  3.f.ii.4.a]                        Create Answer object and populate  
                                                Answer attributeAnswer = new Answer();
                                                attributeAnswer.AnswerCode = Utilities.GetIntegerValue(drAvaTable["ANS_ID"], 0);
                                                attributeAnswer.DisplayOrder = Utilities.GetIntegerValue(drAvaTable["DISP_SORT"], 0);

                                                if (dtAnswers_Jewelry != null)
                                                {
                                                    //[FTN:  3.f.ii.4.b]                            Filter to only matching Answer Code record from ANSWERS
                                                    DataRow[] drsAnswerTable = dtAnswers_Jewelry.Select("ANS_ID=" + attributeAnswer.AnswerCode.ToString());

                                                    if (drsAnswerTable.Length > 0)
                                                    {
                                                        attributeAnswer.AnswerText = Utilities.GetStringValue(drsAnswerTable[0]["ANS_DESC"]);
                                                        attributeAnswer.InputKey = Utilities.GetStringValue(drsAnswerTable[0]["INPUT_KEY"]);
                                                        attributeAnswer.OutputKey = Utilities.GetStringValue(drsAnswerTable[0]["OUTPUT_KEY"]);
                                                    }
                                                }
                                                //[FTN:  3.f.ii.4.c]                        Add attributeAnswer to ItemAttribute
                                                maskItemAttribute.AnswerList.Add(attributeAnswer);
                                            }
                                        }
                                    }
                                }

                                //[FTN:  3.f.ii.1.l...n]    Assign Control Type  
                                if (maskItemAttribute.AnswerList.Count == 0 && !maskItemAttribute.IsRestricted)
                                    maskItemAttribute.InputControl = ControlType.TEXTFIELD;
                                else if (maskItemAttribute.AnswerList.Count > 0 && !maskItemAttribute.IsRestricted)
                                    maskItemAttribute.InputControl = ControlType.COMBOBOX_TEXT_ENABLED;
                                else if (maskItemAttribute.AnswerList.Count > 0 && maskItemAttribute.IsRestricted)
                                    maskItemAttribute.InputControl = ControlType.COMBOBOX_ONLY;

                                //[FTN:  3.f.iii]           Add maskItemAttribute to JewelrySet object
                                itemJewelrySet.ItemAttributeList.Add(maskItemAttribute);
                            }

                            //[FTN:  3.f.iv]        Add JewelrySet to PawnItem
                            SelectedPawnItem.Jewelry.Add(itemJewelrySet);
                        }
                        #endregion

                        SelectedPawnItem.PfiAssignmentType = SelectedPawnItem.IsJewelry ? PfiAssignment.Scrap : PfiAssignment.Normal;
                        SelectedPawnItem.IsGun = Utilities.IsGun(SelectedPawnItem.GunNumber, SelectedPawnItem.CategoryMask, SelectedPawnItem.IsJewelry, SelectedPawnItem.MerchandiseType);

                        Exists = true;
                    }
                    else
                    {
                        Error = true;
                        ErrorMessage = sErrorMessage + " [" + sErrorCode + "]";
                    }
                }
                else
                {
                    Error = true;
                    ErrorMessage = sErrorMessage + " [" + sErrorCode + "]";
                }
            }
            catch (Exception exp)
            {
                Error = true;
                ErrorMessage = exp.Message;
            }
        }
    }
}