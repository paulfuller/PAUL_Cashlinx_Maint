/************************************************************************
* Namespace:       CommonUI.Desktop
* Class:           CategoryNode
* 
* Description      The class keeps the information of a category for 
*                  merchandise information stored in the Catalog.xml
*                  file saved in the Resources folder
* 
* History
* David D Wise, 3-10-2009, Initial Development
* 
* **********************************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Xml;

namespace Common.Libraries.Objects
{
    public class CategoryNode
    {
        /// <summary>
        /// If the allowed flag is false then don't allow user to select this category
        /// </summary>
        public bool CatAllowed { get; set; }

        /// <summary>
        /// Keeps the flag that indicates this category node is the leaf node
        /// </summary>
        public bool CatComplete { get; set; }

        /// <summary>
        /// Keeps the category code
        /// </summary>
        public int CategoryCode { get; set; }

        /// <summary>
        /// Keeps the next CategoryNode
        /// </summary>
        public List<CategoryNode> NextNode { get; set; }

        /// <summary>
        /// Keeps the description of the category and this will be also used as the label
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Keeps the level which will be displayed along with the description
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Points to the mask table which is used to find all the entries in the mask
        /// </summary>
        public int Masks { get; set; }

        /// <summary>
        /// Number of merchandise items in XML
        /// </summary>
        public int MerchandiseCount { get; set; }

        /// <summary>
        /// Keeps the reference to the parent node -- root node will point to null
        /// </summary>
        public CategoryNode ParentNode { get; set; }

        /// <summary>
        /// Flag indicating if event provided existing return of information
        /// </summary>
        public bool Exists { get; set; }

        /// <summary>
        /// Provide flag indicating an error occurred
        /// </summary>
        public bool Error { get; set; }

        /// <summary>
        /// Text message of error if occurred
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// During instantiation of CategoryNode, set up any local variables
        /// and/or properties
        /// </summary>
        public CategoryNode()
        {
            MerchandiseCount = 0;
        }

        /// <summary>
        /// Load in the XML information
        /// </summary>
        public void Setup()
        {
            Exists = false;
            Error = false;
            ErrorMessage = "";

            try
            {
                var resourceStream = Common.Properties.Resources.ResourceManager.GetObject("category");
                if (resourceStream == null)
                {
                    throw new ApplicationException("Resource stream for category.xml is null");
                }


                XmlTextReader resourceXmlTextReader = new XmlTextReader(new StringReader(resourceStream.ToString()));

                // Build a XmlDocument to load the XML information into
                XmlDocument docXML = new XmlDocument();
                docXML.Load(resourceXmlTextReader);

                // Establish a XmlNamespaceManager to pull the nso prefix method parameter into
                XmlNamespaceManager nsManager = new XmlNamespaceManager(docXML.NameTable);
                nsManager.AddNamespace("ns0", "http://casham.com/esb/adapters/proknow");

                // Pull the main NodeList out of the XML file
                XmlNodeList nlXMLData = docXML.SelectNodes("//ns0:categoryList/ns0:childCat", nsManager);

                // Only build out the Category Nodes if it exist in the XML information.
                if (nlXMLData.Count > 0)
                {
                    NextNode = new List<CategoryNode>();
                    foreach (XmlNode childNode in nlXMLData)
                    {
                        NextNode.Add(AddNodeChildren(childNode, null, nsManager));
                        MerchandiseCount++;
                    }
                    Level = 0;
                    Description = "Top Level";                    
                    Exists = true;
                }
            }
            catch (Exception exp)
            {
                Error = true;
                ErrorMessage = exp.Message;
            }
        }

        /// <summary>
        /// Parse the current Xml Node for Manufacture Category information.  It will check for children nodes
        /// and continue calling itself until all children are accounted for.
        /// </summary>
        /// <param name="currentXmlNode"></param>
        /// <param name="cnParentNode"></param>
        /// <param name="nsManager"></param>
        /// <param name="iMerchandiseCount"></param>
        /// <returns></returns>
        public CategoryNode AddNodeChildren(XmlNode currentXmlNode, CategoryNode cnParentNode, XmlNamespaceManager nsManager)
        {
            CategoryNode cnNewCategoryNode = new CategoryNode
            {
                Level = GetCategoryLevelDepth(currentXmlNode["ns0:catCode"].InnerText),
                CategoryCode = Convert.ToInt32(currentXmlNode["ns0:catCode"].InnerText),
                Description = currentXmlNode["ns0:catDescription"].InnerText,
                CatComplete = currentXmlNode["ns0:catComplete"].InnerText == "N" ? false : true,
                CatAllowed = currentXmlNode["ns0:catAllowed"].InnerText == "N" ? false : true,
                Masks = Convert.ToInt32(currentXmlNode["ns0:catMaskPointer"].InnerText),
                ParentNode = cnParentNode,
                Exists = true
            };

            if (cnNewCategoryNode.CatComplete)
                return cnNewCategoryNode;

            XmlNodeList nlXMLData = currentXmlNode.SelectNodes("ns0:childCat", nsManager);
            if (nlXMLData.Count > 0)
            {
                cnNewCategoryNode.NextNode = new List<CategoryNode>();
                foreach (XmlNode childNode in nlXMLData)
                {
                    cnNewCategoryNode.NextNode.Add(AddNodeChildren(childNode, cnNewCategoryNode, nsManager));
                    MerchandiseCount++;
                }
            }
            return cnNewCategoryNode;
        }

        /// <summary>
        /// Method to derive Category Level Depth.
        /// </summary>
        /// <param name="sCategoryCode">Category Code passed as a string</param>
        /// <returns>Returns Category Level Depth</returns>
        public int GetCategoryLevelDepth(string sCategoryCode)
        {
            // Start depth at Level 1 (1 to 4)
            int iCategoryLevelDepth = 1;
            // Check each digit of Category Code to derive Depth Level
            for (int i = 1; i < sCategoryCode.Length; i++)
            {
                if (Convert.ToInt32(sCategoryCode.Substring(i, 1)) > 0)
                    iCategoryLevelDepth++;
            }
            // Return derived Depth Level
            return iCategoryLevelDepth;
        }

        public int GetCategoryMask(int iCategoryCode)
        {
            if (NextNode == null)
                return 0;
            return GetMerchandiseCategory(iCategoryCode.ToString()).Masks;
        }

        /// <summary>
        /// Dynamic Loop to retrieve Category Node from Category Code entered
        /// </summary>
        /// <param name="sCategoryCode"></param>
        /// <param name="cnNodes"></param>
        /// <param name="cnNode"></param>
        /// <param name="sLevelCategory"></param>
        /// <param name="bDone"></param>
        public void MerchandiseCategoryRetriever(int iCategoryCode, List<CategoryNode> cnNodes, ref CategoryNode cwNode, ref string sLevelCategory, ref bool bExist)
        {
            foreach (CategoryNode cnTmpNode1 in cnNodes)
            {
                if (cnTmpNode1.CategoryCode == iCategoryCode)
                {
                    sLevelCategory = "|" + cnTmpNode1.CategoryCode + "#" + cnTmpNode1.Description;
                    cwNode = cnTmpNode1;
                    bExist = true;
                    break;
                }
                if (cnTmpNode1.NextNode != null)
                {
                    MerchandiseCategoryRetriever(iCategoryCode, cnTmpNode1.NextNode, ref cwNode, ref sLevelCategory, ref bExist);
                    if (bExist)
                    {
                        sLevelCategory = "|" + cnTmpNode1.CategoryCode + "#" + cnTmpNode1.Description + sLevelCategory;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Pull Category Node by CategoryCode entered
        /// </summary>
        /// <param name="sCategoryCode">Category Code passed as a string</param>
        /// <returns>CategoryNode</returns>
        public CategoryNode GetMerchandiseCategory(string sCategoryCode)
        {
            Exists = false;
            Error = false;
            ErrorMessage = "";

            CategoryNode cwNode = new CategoryNode();

            if (NextNode.Count > 0)
            {
                string sLevelCategory = "";
                cwNode = GetMerchandiseCategory(sCategoryCode, ref sLevelCategory);
            }
            else
            {
                Error = true;
                ErrorMessage = "CategoryNodes List not populated.";
            }

            return cwNode;
        }

        /// <summary>
        /// Pull Category Node by CategoryCode entered.  Over-ride Method in case calling page needs Navigation Nest
        /// </summary>
        /// <param name="sCategoryCode">Category Code passed as a string</param>
        /// <returns>CategoryNode</returns>
        public CategoryNode GetMerchandiseCategory(string sCategoryCode, ref string sLevelCategory)
        {
            Exists = false;
            Error = false;
            ErrorMessage = "";

            CategoryNode cwNode = new CategoryNode();

            // The Level Category will be appended to while looping the ForLoops below.  A "|" is to
            // separate each Category Level while the "#" is to separate out Category Code and Description.
            // 
            sLevelCategory = "";

            // Testing dynamic looping
            if (NextNode.Count > 0)
            {
                bool bExist = false;
                MerchandiseCategoryRetriever(Convert.ToInt32(sCategoryCode), NextNode, ref cwNode, ref sLevelCategory, ref bExist);
                Exists = bExist;
                sLevelCategory = "0#CATEGORY:" + sLevelCategory;
            }
            else
            {
                Error = true;
                ErrorMessage = "CategoryNodes List not populated.";
            }

            return cwNode;
        }
    }
}