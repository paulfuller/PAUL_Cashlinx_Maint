using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Common.Libraries.Objects.Rules.Data;
using Common.Libraries.Objects.Rules.Structure;
using Common.Libraries.Utility.Shared;

namespace Common.Controllers.Rules.Data
{
    /// <summary>
    /// Used as a helper to serialize and deserialize rules from XML.
    /// </summary>
    public static class RulesSerializationHelper
    {

        #region Public Methods

        /*public static void SerializeRules(Dictionary<string, BusinessRuleVO> rules, List<BusinessRuleComponentVO> components)
        {
            try
            {

                XmlTextWriter writer = new XmlTextWriter("c:\\rules.xml", null);
                List<string> distinctCompRefs = new List<string>();

                writer.WriteStartElement("BusinessRules");


                //****************************************************
                //BUILD XML FOR BUSINESS RULES AND COMPONENT REFERENCE
                //****************************************************

                foreach (string k in rules.Keys)
                {
                    if (k == null) continue;
                    BusinessRuleVO bvo = rules[k];
                    if (bvo == null) continue;
                    writer.WriteStartElement("BusinessRule");
                    writer.WriteAttributeString("name", bvo.Code);
                    foreach (string bKey in bvo.Keys)
                    {
                        if (bKey == null) continue;
                        BusinessRuleComponentVO bvco = bvo[bKey];
                        if (bvco == null) continue;

                        writer.WriteStartElement("BusinessRuleCompRef");
                        writer.WriteAttributeString("name", bvco.Code);
                        writer.WriteElementString("ComponentType", BusinessRuleComponentVO.GetRuleValueTypeString(bvco.ValueType));
                        writer.WriteEndElement();  //BusinessRuleCompRef
                    }

                    writer.WriteEndElement();   //BusinessRule
                }

                //****************************************************
                //BUILD XML FOR BUSINESS RULE COMPONENT VALUES
                //****************************************************
                foreach (BusinessRuleComponentVO bvco in components)
                {

                    string valType =
                        BusinessRuleComponentVO.GetRuleValueTypeString(bvco.ValueType);

                    writer.WriteStartElement("BusinessRuleComponent");
                    writer.WriteAttributeString("name", bvco.Code);

                    writer.WriteElementString("FromDate", bvco.FromDate.FormatDate());
                    writer.WriteElementString("ToDate", bvco.ToDate.FormatDate());
                    writer.WriteElementString("ComponentType", valType);

                    switch (bvco.ValueType)
                    {
                        case BusinessRuleComponentVO.RuleValueType.PARAM:
                            ParamVO pVo = bvco.ParamValue;
                            writer.WriteStartElement("Param");
                            if (pVo != null)
                            {
                                writer.WriteElementString("Alias", pVo.Alias.Code);
                                writer.WriteElementString("DataType", pVo.DataType.Code);
                                //TODO: Is this the best way to hand "converting" an object to XML?
                                writer.WriteElementString(
                                    "Value", pVo.Value == null ? null : pVo.Value.ToString());
                                writer.WriteElementString("StoreNumber", pVo.StoreNumber);
                                writer.WriteElementString("State", pVo.State);
                                writer.WriteElementString("Company", pVo.Company);
                                writer.WriteElementString(
                                    "Cacheable", XmlConvert.ToString(pVo.Cacheable));
                            }
                            writer.WriteEndElement(); //Param
                            break;
                        case BusinessRuleComponentVO.RuleValueType.INTEREST:
                            InterestVO iVo = bvco.InterestValue;
                            writer.WriteStartElement("Interest");
                            if (iVo != null)
                            {
                                writer.WriteElementString("Alias", iVo.Alias.Code);
                                writer.WriteElementString(
                                    "MinAmount", XmlConvert.ToString(iVo.MinAmount));
                                writer.WriteElementString(
                                    "MaxAmount", XmlConvert.ToString(iVo.MaxAmount));
                                writer.WriteElementString(
                                    "InterestRate", XmlConvert.ToString(iVo.InterestRate));
                                writer.WriteElementString(
                                    "InterestAmount", XmlConvert.ToString(iVo.InterestAmount));
                                writer.WriteElementString(
                                    "ServiceAmount", XmlConvert.ToString(iVo.ServiceAmount));
                                writer.WriteElementString(
                                    "ServiceRate", XmlConvert.ToString(iVo.ServiceRate));
                                writer.WriteElementString("InterestLevel", iVo.InterestLevel);
                                writer.WriteElementString("InterestType", iVo.InterestType);
                            }

                            writer.WriteEndElement(); //Interest
                            break;
                        case BusinessRuleComponentVO.RuleValueType.FEES:
                            FeesVO fVo = bvco.FeesValue;
                            writer.WriteStartElement("Fee");
                            if (fVo != null)
                            {
                                writer.WriteElementString("Alias", fVo.Alias.Code);
                                writer.WriteElementString("FeeType", fVo.FeeType.Code);
                                writer.WriteElementString("FeeLookupType", fVo.FeeLookupType.Code);
                                writer.WriteElementString("Value", fVo.Value);
                            }
                            writer.WriteEndElement(); //Interest
                            break;
                    }

                    writer.WriteEndElement(); //BusinessRuleComponent
                }

                writer.WriteEndElement(); //BusinessRules

                //Close the writer!
                writer.Close();
            }
            catch (Exception eX)
            {
                throw new ApplicationException("Could not serialize rule set", eX);
            }

        }
        */

        /*
        /// <summary>
        /// Loads business rules from XML data cache.
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, BusinessRuleVO> DeserializeRules()
        {
            try
            {
                Dictionary<string, BusinessRuleVO> rules = new Dictionary<string, BusinessRuleVO>();

                XDocument rulesXML = XDocument.Load(@"C:\rules.xml");
                List<XElement> busRules = (from r in rulesXML.Descendants("BusinessRule")
                                           select r).ToList();
                //Before loading rules, load the components for lookup and association
                //to the business rules.
                Dictionary<string, BusinessRuleComponentVO> componentsLookup = LoadRuleComponentsFromXML();

                foreach (XElement el in busRules)
                {
                    BusinessRuleVO brvo = new BusinessRuleVO(el.Attribute("name").Value, Guid.NewGuid());

                    List<XElement> compRefs = (from r in el.Descendants("BusinessRuleCompRef")
                                               select r).ToList<XElement>();

                    foreach (XElement el2 in compRefs)
                    {
                        string compRefName = el2.Attribute("name").Value;
                        //BusinessRuleComponentVO comp = (from c in componentsLookup
                        //                                where c.Code == compRefName
                        //                                select c).First<BusinessRuleComponentVO>();

                        BusinessRuleComponentVO comp = componentsLookup[compRefName];

                        brvo.addComponent(comp);
                        //brvo.addComponent(new BusinessRuleComponentVO(compRefName, type));
                    }

                    rules.Add(brvo.Code, brvo);
                }

                return rules;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Could not de-serialize business rule set", ex);
            }
        }
        */
        //public static void CompareBusinessRules(Dictionary<string, BusinessRuleVO> busRulesFromXML, Dictionary<string, BusinessRuleVO> busRulesHardCoded)
        //{
        //    Console.WriteLine("Total Rules from XML: " + busRulesFromXML.Count);
        //    Console.WriteLine("Total Rules from HardCoded: " + busRulesHardCoded.Count);
        //    int foundCount = 0;

        //    foreach (KeyValuePair<string, BusinessRuleVO> brValuePair in busRulesHardCoded)
        //    {
        //        BusinessRuleVO ruleHardCoded = brValuePair.Value;
        //        BusinessRuleVO ruleXML = null;

        //        if (busRulesFromXML.ContainsKey(brValuePair.Key))
        //        {
        //            ruleXML = busRulesFromXML[brValuePair.Key];
        //            foundCount++;
        //        }
        //        else
        //        {
        //            Console.WriteLine("Could not find rule: " + brValuePair.Key + " for " + brValuePair.Value.Code);
        //        }

        //        if(ruleXML.Keys.Count != ruleHardCoded.Keys.Count)
        //        {
        //            Console.WriteLine("Count of XML keys (!=): " + ruleXML.Keys.Count + "");
        //            Console.WriteLine("Count of Hard Coded keys (!=): " + ruleHardCoded.Keys.Count + "");
        //        }

        //        //For each business rule 
        //        foreach (string bKey in brValuePair.Value.Keys)
        //        {
        //            if (bKey == null) continue;
        //            BusinessRuleComponentVO bvco = brValuePair.Value[bKey];
        //            if (bvco == null) continue;

        //            if (!ruleXML.ContainsKey(bvco.Code))
        //            {
        //                Console.WriteLine("Missing component: " + bvco.Code);
        //            }
        //            else
        //            {
        //                BusinessRuleComponentVO bvcoXML = ruleXML[bvco.Code];

        //                //Could possibly write more to compare fees, etc.
        //                //Call short date time string,for some reason the XML sets the time to 12:01 AM
        //                //instead of 12:00 AM, which only affects the compare.  
        //                if (bvco.Code != bvcoXML.Code
        //                    || bvco.FromDate.ToShortDateString() != bvcoXML.FromDate.ToShortDateString()
        //                    || bvco.ToDate.ToShortDateString() != bvcoXML.ToDate.ToShortDateString()
        //                    || bvco.ValueType.ToString() != bvcoXML.ValueType.ToString())
        //                {
        //                    Console.WriteLine("Component values inconsistent: " + bvco.Code);
        //                }

        //            }
        //        }

        //    }

        //    Console.WriteLine("Found " + foundCount + " items out of " + busRulesHardCoded.Count + " in XML.");
        //}

        #endregion Public Methods

        #region Private Methods

        /*private static Dictionary<string, BusinessRuleComponentVO> LoadRuleComponentsFromXML()
        {
            try
            {
                //PawnRules rules = new PawnRules();
                var componentsList = new Dictionary<string, BusinessRuleComponentVO>();

                XDocument rulesXML = XDocument.Load(@"C:\rules.xml");
                List<XElement> ruleCompElems = (from r in rulesXML.Descendants("BusinessRuleComponent")
                                                select r).ToList();

                foreach (XElement el in ruleCompElems)
                {
                    string compName = el.Attribute("name").Value;
                    var type =
                        (BusinessRuleComponentVO.RuleValueType)Enum.Parse(typeof(BusinessRuleComponentVO.RuleValueType),
                                               el.Element("ComponentType").Value);
                    DateTime fromDate = XmlConvert.ToDateTime(el.Element("FromDate").Value, "mm/dd/yyyy");
                    DateTime toDate = XmlConvert.ToDateTime(el.Element("ToDate").Value, "mm/dd/yyyy");

                    var component = new BusinessRuleComponentVO(compName, type, fromDate, toDate);

                    switch (type)
                    {
                        case BusinessRuleComponentVO.RuleValueType.PARAM:

                            //Instantiate ParamValue and populate since we know that's what's in XML                            
                            XElement paramElem = el.Element("Param");

                            component.ParamValue = new ParamVO();
                            component.ParamValue.Code = paramElem.Element("Alias").Value;
                            component.ParamValue.DataType.Code = paramElem.Element("DataType").Value;
                            component.ParamValue.Value = paramElem.Element("Value").Value;
                            component.ParamValue.Company = paramElem.Element("Company").Value;
                            component.ParamValue.State = paramElem.Element("State").Value;
                            component.ParamValue.StoreNumber = paramElem.Element("StoreNumber").Value;
                            component.ParamValue.Cacheable =
                                XmlConvert.ToBoolean(paramElem.Element("Cacheable").Value);

                            break;
                        case BusinessRuleComponentVO.RuleValueType.INTEREST:

                            //Instantiate Interest and populate since we know that's what's in XML                            
                            XElement intElem = el.Element("Interest");

                            component.InterestValue = new InterestVO();
                            component.InterestValue.Alias.Code = intElem.Element("Alias").Value;
                            component.InterestValue.MinAmount =
                                XmlConvert.ToDecimal(intElem.Element("MinAmount").Value);
                            component.InterestValue.MaxAmount =
                                XmlConvert.ToDecimal(intElem.Element("MaxAmount").Value);
                            component.InterestValue.InterestRate =
                                XmlConvert.ToDecimal(intElem.Element("InterestRate").Value);
                            component.InterestValue.InterestAmount =
                                XmlConvert.ToDecimal(intElem.Element("InterestAmount").Value);
                            component.InterestValue.InterestLevel =
                                intElem.Element("InterestLevel").Value;
                            component.InterestValue.InterestType =
                                intElem.Element("InterestType").Value;
                            break;
                        case BusinessRuleComponentVO.RuleValueType.FEES:
                            //Instantiate Fees and populate since we know that's what's in XML 
                            XElement feeElem = el.Element("Fee");

                            component.FeesValue = new FeesVO();
                            component.FeesValue.Alias.Code = feeElem.Element("Alias").Value;
                            component.FeesValue.FeeType.Code = feeElem.Element("FeeType").Value;
                            component.FeesValue.FeeLookupType.Code =
                                feeElem.Element("FeeLookupType").Value;
                            component.FeesValue.Value = feeElem.Element("Value").Value;

                            break;
                    }

                    componentsList.Add(compName, component);
                }

                return componentsList;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Could not de-serialize pawn rule set", ex);
            }
        }*/

        #endregion Private Methods

    }
}
