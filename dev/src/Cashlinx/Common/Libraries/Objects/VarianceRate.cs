/************************************************************************
 * Namespace:       CashlinxDesktop.Desktop
 * Class:           VarianceRates
 * 
 * Description      The class keeps the Variances retrieved from the 
 *                  System Resource file, Variance.xml
 * 
 * History
 * David D Wise, 4-16-2009, Initial Development
 * 
 * **********************************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;
using Common.Libraries.Utility.Shared;

namespace Common.Libraries.Objects
{
    public class VarianceRate
    {
        public List<Variance> VarianceRates { get; set; }         // 
        public bool Exists { get; set; }         // Flag indicating if event provided existing return of information
        public bool Error { get; private set; } // Provide flag indicating an error occurred
        public string ErrorMessage { get; private set; } // Text message of error if occurred

        public VarianceRate()
        {

        }
        /// <summary>
        /// Load in the XML information
        /// </summary>
        public void Setup()
        {
            Exists = false;
            Error = false;
            ErrorMessage = "";

            VarianceRates = new List<Variance>();

            try
            {
                var resourceStream = Common.Properties.Resources.ResourceManager.GetObject("variance");

                if (resourceStream == null)
                {
                    throw new ApplicationException("Resource stream for variance.xml is null");
                }
                // Build a XmlDocument to load the XML information into
                XmlTextReader resourceXmlTextReader = new XmlTextReader(new StringReader(resourceStream.ToString()));

                // Build a XmlDocument to load the XML information into
                XmlDocument docXML = new XmlDocument();
                docXML.Load(resourceXmlTextReader);


                XmlNodeList nlXMLData = docXML.SelectNodes("//variances/variance_record");

                // Only build out the Category Nodes if it exist in the XML information.
                if (nlXMLData.Count > 0)
                {
                    foreach (XmlNode xmlNode in nlXMLData)
                    {
                        Variance variance = new Variance();
                        variance.DocType = Convert.ToChar(xmlNode["var_doc_type"].InnerText);
                        variance.MaxAmount = Convert.ToDecimal(xmlNode["var_max_amount"].InnerText);
                        variance.MinAmount = Convert.ToDecimal(xmlNode["var_min_amount"].InnerText);
                        variance.Percent = Convert.ToDecimal(xmlNode["var_percent"].InnerText);

                        VarianceRates.Add(variance);
                    }
                    Exists = true;
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
