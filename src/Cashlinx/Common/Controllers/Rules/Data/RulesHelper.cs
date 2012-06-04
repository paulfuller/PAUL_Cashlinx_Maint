using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Rules.Data;
using Common.Libraries.Objects.Rules.Structure;
using System.Collections.ObjectModel;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;

namespace Common.Controllers.Rules.Data
{
    /// <summary>
    /// Used as a helper to serialize and deserialize rules from XML.
    /// </summary>
    public static class RulesHelper
    {
        static RulesHelper()
        {
            SetWorkingDirectory(System.Windows.Forms.Application.StartupPath);
        }

        #region Private Members
        
        //TODO: Load from a config file?
        private static string _WORKING_DIR;
        private static string _XML_RULES_FILE_PATH;
        private static string _BACKUP_DIRECTORY;
        private static string _WORKING_XML_FILE_PATH;
        private static bool _workFromTempFile = false;

        #endregion Private Members

        #region Public Properties

        /// <summary>
        /// Set this property so that the rules helper class knows 
        /// to work off of the temporary rules file.
        /// </summary>
        public static bool WorkFromTempFile
        {
            get { return RulesHelper._workFromTempFile; }
            set { RulesHelper._workFromTempFile = value; }
        }

        #endregion Public Properties

        #region Public Events

        public delegate void ChangesCommittedHandler();
        public static event ChangesCommittedHandler DataCommitted;

        #endregion Public Events

        #region Public Methods

        /// <summary>
        /// Loads the rules xml from the file specified into the 
        /// systems memory for useage through out the application.
        /// </summary>
        /// <param name="filename"></param>
        public static void LoadFromFile(string filename)
        {
            //Make the selected file the new "working" file.
            CreateWorkableTempFile(filename);

            //Force a document reload to ensure right data.            
            RulesData.Instance.LoadRules();
        }

        /// <summary>
        /// Tries to create a backup of the XML file.
        /// </summary>
        public static void CreateBackup()
        {
            try
            {
                if (!Directory.Exists(_BACKUP_DIRECTORY))
                {
                    Directory.CreateDirectory(_BACKUP_DIRECTORY);
                }
                //Maybe do a GUID next time?
                File.Copy(
                    _XML_RULES_FILE_PATH,
                    _BACKUP_DIRECTORY + @"\Rules_"
                    + DateTime.Now.Month.ToString() + "-"
                    + DateTime.Now.Day.ToString() + "-"
                    + DateTime.Now.Year.ToString() + "-"
                    + "_"
                    + DateTime.Now.Hour.ToString()
                    + DateTime.Now.Minute.ToString()
                    + DateTime.Now.Second.ToString() + ".xml", true);
            }catch(Exception ex)
            {
                Console.WriteLine("Problem creating a backup: " + ex.Message);
            }

        }

        public static void CreateWorkableTempFile()
        {
            try
            {
                //Maybe do a GUID next time?
                File.Copy(
                    _XML_RULES_FILE_PATH, _WORKING_XML_FILE_PATH, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem creating a backup: " + ex.Message);
            }

        }

        /// <summary>
        /// Create a temp file to work off of until an explicit save has been made.
        /// </summary>
        public static void CreateWorkableTempFile(string copyFromFileName)
        {
            try
            {
                //Maybe do a GUID next time?
                //File.Copy(
                //    _XML_RULES_FILE_PATH, _WORKING_XML_FILE_PATH, true);
                File.Copy(
                    copyFromFileName, _WORKING_XML_FILE_PATH, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem creating a backup: " + ex.Message);
            }

        }

        /// <summary>
        /// Delete the working temp file.
        /// </summary>
        public static void DeleteWorkingTempFile()
        {
            try
            {
                File.Delete(_WORKING_XML_FILE_PATH);
            }catch(Exception ex)
            {
                Console.WriteLine(
                    "There was an issue trying to delete the temp file: " 
                    + ex.Message
                    + Environment.NewLine + " File: "
                    + _WORKING_XML_FILE_PATH);
            }
        }

        /// <summary>
        /// Returns a dictionary of filtered rules based on criteria
        /// that's supplied in the calls arguments.
        /// </summary>
        /// <param name="site"></param>
        /// <param name="reSaveFile"></param>
        /// <returns></returns>
        public static Dictionary<string, BusinessRuleVO> BuildBusinessRules(SiteId site, bool reSaveFile = false)
        {
            //ToDo: Handle more gracefully.
            if (!File.Exists(_XML_RULES_FILE_PATH))
            {
                throw new Exception("Rules.xml could not be found at " + _XML_RULES_FILE_PATH);
            }

            try
            {
                Dictionary<string, BusinessRuleVO> rules = new Dictionary<string, BusinessRuleVO>();
                RulesData data = RulesData.Instance;

                List<BusinessRuleCompLookupVO> lookups = (from l in data.RulesVO
                                                          select l.Value).ToList
                                                            <BusinessRuleCompLookupVO>();
                
                foreach (BusinessRuleCompLookupVO lookup in lookups)
                {
                    BusinessRuleVO rule = new BusinessRuleVO(lookup.Code, lookup.Id);
                    FillRuleComponents(site, ref rule, lookup.ComponentCodes);
                    rules.Add(rule.Code, rule);
                }

                if (reSaveFile)
                {
                    var altFileName = string.Format("c:\\rules_{0}.xml", DateTime.Now.Ticks);
                    RulesData.Instance.SerializeRulesToXML(BuildBusinessRuleMap(lookups), RulesData.Instance.RulesComponents, altFileName, false);
                }

                return rules;
            }catch(Exception ex)
            {
                throw new ApplicationException("Error building business rules. ", ex);
            }
        }

        /// <summary>
        /// Not a full list of business rules, just one comp reference 
        /// per business rule.  Used to save comp references to a
        /// business rule.
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, BusinessRuleNodeVO> BuildBusinessRuleMap(
            List<BusinessRuleCompLookupVO> altLookups = null,
            List<BusinessRuleComponentVO> altComps = null)
        {
            Dictionary<string, BusinessRuleNodeVO> rules = new Dictionary<string, BusinessRuleNodeVO>();
            try
            {
                RulesData data = RulesData.Instance;
                List<BusinessRuleCompLookupVO> lookups = altLookups;
                if (lookups == null)
                {
                    lookups = (from l in data.RulesVO
                               select l.Value).ToList<BusinessRuleCompLookupVO>();
                }

                foreach (BusinessRuleCompLookupVO lookup in lookups)
                {
                    //Create the new business rule.
                    BusinessRuleNodeVO rule = new BusinessRuleNodeVO(lookup.Code, lookup.Id);

                    foreach (ComponentDetails detail in lookup.ComponentCodes)
                    {
                        IEnumerable<BusinessRuleComponentVO> components = null;
                        ComponentDetails detail1 = detail;
                        if (altComps == null)
                        {
                            
                            components = (from c in data.RulesComponents
                             where c.Code.Equals(detail1.ComponentCode, StringComparison.OrdinalIgnoreCase)
                                select c);
                        }
                        else
                        {
                            components = (from c in altComps
                                          where c.Code.Equals(detail1.ComponentCode, StringComparison.OrdinalIgnoreCase)
                                          select c);
                        }

                        //Filters should narrow component down to correct one.
                        //If not then log it.
                        if (components.Count() > 0)
                        {
                            if (rule.ComponentList == null)
                                rule.ComponentList =
                                    new ObservableCollection<BusinessRuleComponentVO>();

                            rule.ComponentList.Add(components.FirstOrDefault());                            
                        }
                        else
                        {
                            Console.WriteLine("Could not find a filtered component.");
                        }
                    }

                    rules.Add(rule.Code, rule);
                }

                return rules;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error building business rules. ", ex);
            }
        }

        /// <summary>
        /// Serialize the ruleset to XML format for a local cache of busines rules.
        /// </summary>
        /// <param name="rules"></param>
        public static void SaveRules(List<BusinessRuleNodeVO> changedBusinessRules, List<BusinessRuleComponentVO> changedComponents)
        {
            //Transform rules to a dictionary.
            Dictionary<string, BusinessRuleNodeVO> rules = BuildBusinessRuleMap();
            //List of all components to be added, edited or removed.
            List<BusinessRuleComponentVO> components = RulesData.Instance.RulesComponents;

            if (changedBusinessRules != null)
            {                   
                //creates the nodes into a dictionary to be saved.
                foreach (BusinessRuleNodeVO node in changedBusinessRules)
                {
                    //If id exists then modify the code.  Everything
                    //else is handled through the components (relationships, etc.)
                    if ((from l in rules
                         where l.Value.Id == node.Id
                         select l.Value).Any())
                    {
                        //Find by node id rather than code since 
                        //code can be changed.
                        BusinessRuleNodeVO r = (from l in rules
                                            where l.Value.Id == node.Id
                                            select l.Value).First();

                        rules.Remove(r.Code);

                        if (!node.Deleted)
                        {
                            //Add back the changed node.  Would replace,
                            //but changing the code changes the key...
                            //and the key needs to be changed which requires
                            //an add/remove.
                            rules.Add(node.Code, node);
                        }
                    }
                    else
                    {
                        //If it couldn't be found then it is a new rule.
                        rules.Add(node.Code, node);
                    }

                }
            }

            if (changedComponents != null)
            {
                //Change list as components have changed.
                foreach (BusinessRuleComponentVO comp in changedComponents)
                {
                    //Delete
                    if (comp.Deleted)
                    {
                        var results = (from r in components
                                       where r.Id == comp.Id
                                       select r);

                        //In the instance that 
                        if(results.Count() > 0)
                            components.Remove(results.First<BusinessRuleComponentVO>());
                    //Add
                    }else if(!components.Exists(c => c.Id == comp.Id))
                    {
                        components.Add(comp);
                    }
                    //Update
                    else
                    {
                        //Find the index of the component and then set it
                        //to the old component with the new component.
                        int idx = components.FindIndex(c => c.Id == comp.Id);                        
                        components[idx] = (BusinessRuleComponentVO)comp;
                    }


                }
            }
            //var altFileName = string.Format("c:\\rules_{0}.xml", DateTime.Now.Ticks);
            var altFileName = _WORKING_XML_FILE_PATH;
            RulesData.Instance.SerializeRulesToXML(rules, components,altFileName,true);
        }

        /// <summary>
        /// Commite the rules to the permanent rules.xml file.
        /// </summary>
        public static bool CommitRules()
        {
            try
            {                
                //Maybe do a GUID next time?
                File.Copy(
                    _WORKING_XML_FILE_PATH, _XML_RULES_FILE_PATH, true);
                
                if(DataCommitted != null) DataCommitted();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem creating a backup: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Returns a non-filtered hierarchy of business rules and their 
        /// components in a parent child relationship.
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<BusinessRuleNodeVO> GetRulesHierarchy()
        {

            ObservableCollection<BusinessRuleNodeVO> rules = new ObservableCollection<BusinessRuleNodeVO>();
            RulesData data = RulesData.Instance;
            
            //Get rules and alphabetize.
            List<BusinessRuleCompLookupVO> lookups = (from l in data.RulesVO
                                                      orderby l.Key
                                                      select l.Value).ToList<BusinessRuleCompLookupVO>();            
            
            foreach (BusinessRuleCompLookupVO lookup in lookups)
            {
                BusinessRuleNodeVO rule = new BusinessRuleNodeVO(lookup.Code, lookup.Id);
                FillRuleComponentsHierarchy(ref rule, lookup.ComponentCodes);               
                rules.Add(rule);
            }

            return rules;
        }

        /// <summary>
        /// Returns a flat list of all components.
        /// </summary>
        /// <returns></returns>
        public static List<BusinessRuleComponentVO> GetComponentList()
        {
            return RulesData.Instance.RulesComponents;
        }

        /// <summary>
        /// Passed in component will be populated with its children recursively.
        /// </summary>
        /// <param name="child">Component to be populated.</param>
        public static void PopulateComponentHeirarchy(BusinessRuleComponentVO child)
        {
            AddComponentsChildren(child);
        }

        public static  void SetWorkingDirectory(string workingDir)
        {
            _WORKING_DIR = workingDir;
            _XML_RULES_FILE_PATH = _WORKING_DIR + @"\rules.xml";
            _BACKUP_DIRECTORY = _WORKING_DIR + @"\RulesArchive";
            _WORKING_XML_FILE_PATH = _WORKING_DIR + @"\rules-tmp.xml";
        }

        #endregion Public Methods

        #region Private Methods


        private static void AddComponentsChildren(BusinessRuleComponentVO child)
        {
            //This gets a list of all components who are children
            //to the child.  
            var components = (from c in RulesData.Instance.RulesComponents
                              where c.ParentId == child.Id
                              select c);

            if (components.Count() > 0)
            {
                foreach (BusinessRuleComponentVO b in components)
                {
                    if (child.Children == null)
                    {
                        child.Children = new ObservableCollection<BusinessRuleComponentVO>();
                    }

                    //Only add child ids where they have not already
                    //been added previously or they will appear 
                    //multiple times on the tree.
                    if (!(from c in child.Children
                          where c.Id == b.Id
                          select c).Any())
                    {
                        //Set the child's parent ID.
                        b.ParentId = child.Id;
                        b.Children = new ObservableCollection<BusinessRuleComponentVO>();

                        child.Children.Add(
                            Utilities.DeepCopy<BusinessRuleComponentVO>(b));

                        AddComponentsChildren(b);
                    }
                }
            }

        }

        private static void FillRuleComponentsHierarchy(ref BusinessRuleNodeVO rule, List<ComponentDetails> details)
        {
            try{

                RulesData data = RulesData.Instance;

                //Init the component list -- it's important that this 
                //has at least zero elements for binding to xaml
                //in the hierarchicaldatatemplate
                if (rule.ComponentList == null)
                {
                    rule.ComponentList = new ObservableCollection<BusinessRuleComponentVO>();
                }

                foreach (ComponentDetails detail in details)
                {
                    //Get all components with no parent -- this would
                    //be the top level of the components.
                    ComponentDetails detail1 = detail;
                    var components = (from c in data.RulesComponents
                                      where c.Code.Equals(detail1.ComponentCode, StringComparison.OrdinalIgnoreCase) 
                                      && (c.ParentId == Guid.Empty)
                                          select c);


                    //Filters should narrow component down to correct one.
                    //If not then log it.
                    if (components.Count() > 0)
                    {       
                        foreach (BusinessRuleComponentVO c in components)
                        {
                            AddComponentsChildren(c);
                            rule.ComponentList.Add(Utilities.DeepCopy<BusinessRuleComponentVO>(c));
                        }
                    }
                    else
                    {
                        Console.WriteLine("Could not find a filtered component.");
                    }
                }

            }
            catch (Exception eX)
            {
                throw new ApplicationException("Could not serialize rule set", eX);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="site"></param>
        /// <param name="rule"></param>
        /// <param name="details"></param>
        private static void FillRuleComponents(SiteId site, ref BusinessRuleVO rule, List<ComponentDetails> details)
        {
            RulesData data = RulesData.Instance;
            StringBuilder sbDbg=null, sbWarn=null;
            bool isDbg = false, isWarn = false, loggedWarn = false, loggedDebug = false;
            if (FileLogger.Instance != null && FileLogger.Instance.IsLogDebug)
            {
                sbDbg = new StringBuilder(512);
                isDbg = true;
                sbDbg.AppendFormat("Business rule {0}{1}", rule.Code, Environment.NewLine);
            }
            if (FileLogger.Instance != null && FileLogger.Instance.IsLogWarn)
            {
                sbWarn = new StringBuilder(512);
                isWarn = true;
                sbWarn.AppendFormat("Business rule {0}{1}", rule.Code,Environment.NewLine);
                loggedWarn = false;
            }
            foreach (ComponentDetails detail in details)
            {
                DateTime siteDT = site.Date.Date;

                //GJL 7/3/2011 - Only filter components out by component code and date at this point
                var components = (from c in data.RulesComponents
                                  where c.Code.Equals(detail.ComponentCode, StringComparison.OrdinalIgnoreCase)
                                  && c.FromDate.CompareTo(siteDT) <= 0 && c.ToDate.CompareTo(siteDT) >= 0
                                  select c);

                if (site.LoanAmount > 0.00M && data.CheckLoanRange(detail.ComponentCode))
                {
                   var intComponents = from c in components
                                 where site.LoanAmount >= c.InterestValue.MinAmount
                                 && site.LoanAmount <= c.InterestValue.MaxAmount
                                 select c;
                    if (components.Count() <= 0)
                    {
                        if (isDbg)
                        {
                            sbDbg.AppendFormat("Could not find rule component {0} using alias = {1}||{2}",
                                                 detail.ComponentCode, site.Alias, Environment.NewLine);
                            //loggedWarn = true;
                        }
                    }
                }

                //GJL 7/3/2011 - Filter fix for alias
                //- Must conform to alias hierarchy (alias -> store number -> state -> ALL)
                //- Examples:  Custom alias = ALMO_GROUP
                //-            Store number = TX_1_02030 or TX_02030 or 02030
                //-            State        = TX or TX_1
                //-            All          = All or ALL
                //If site alias finds nothing, default to state
                //If state finds nothing, default to all

                //Determine proper component set by alias
                var compCount = components.Count();
                
                if (compCount > 0)
                {
                    var fndComps = from comp in components
                                   where comp.Alias.Equals(site.Alias, StringComparison.OrdinalIgnoreCase)
                                   select comp;
                    //
                    if (fndComps.Count() <= 0)
                    {
                        if (isDbg)
                        {
                            sbDbg.AppendFormat("Could not find rule component {0} using alias = {1}||{2}",
                                                 detail.ComponentCode, site.Alias, Environment.NewLine);
                            loggedDebug = true;
                        }
                    }
                    else
                    {
                        //var foundComponent1 = fndComps.FirstOrDefault();
                        //Add component to rule and return
                        foreach (BusinessRuleComponentVO bvo in fndComps)
                            rule.addComponent(bvo);
                        //Log component found
                        //if (isDbg)
                        //{
                        //    sbDbg.AppendFormat("Found with alias = {0}, guid = {1}{2}",
                        //        site.Alias,
                        //        foundComponent1.Id,
                        //        Environment.NewLine);                            
                        //}
                        //Move to next detail value for this rule
                        continue;
                    }

                    var fndAliasCompStoreNumberComps = from comp in components
                                                  where comp.Alias.Equals(site.State + "_" + site.CompanyNumber + "_" + site.StoreNumber, StringComparison.OrdinalIgnoreCase)
                                                  select comp;

                    if (fndAliasCompStoreNumberComps.Count() <= 0)
                    {
                        if (isDbg)
                        {
                            sbDbg.AppendFormat("Could not find rule component {0} using alias and company number and storenumber = {1}||{2}",
                                                 detail.ComponentCode, site.Alias, Environment.NewLine);
                            loggedDebug = true;
                        }
                    }
                    else
                    {
                        //var foundComponent1 = fndComps.FirstOrDefault();
                        //Add component to rule and return
                        foreach (BusinessRuleComponentVO bvo in fndAliasCompStoreNumberComps)
                            rule.addComponent(bvo);
                        //Log component found
                        //if (isDbg)
                        //{
                        //    sbDbg.AppendFormat("Found with alias = {0}, guid = {1}{2}",
                        //        site.Alias,
                        //        foundComponent1.Id,
                        //        Environment.NewLine);                            
                        //}
                        //Move to next detail value for this rule
                        continue;
                    }

                    //if (isDbg)
                    //{
                    //    sbDbg.AppendFormat("Rule component {0}||", detail.ComponentCode);
                    //}
                    //Add the rule which matches the Alias code
                    var fndAliasCompNumComps = from comp in components
                                               where comp.Alias.Equals(site.State + "_" + site.CompanyNumber, StringComparison.OrdinalIgnoreCase)
                                               select comp;
                    if (fndAliasCompNumComps.Count() <= 0)
                    {
                        if (isDbg)
                        {
                            sbDbg.AppendFormat("OR using state and company number {0}||{1}", site.CompanyNumber, Environment.NewLine);
                            loggedDebug = true;
                        }
                    }
                    else
                    {
                        var foundComponent2 = fndAliasCompNumComps.FirstOrDefault();
                        //Add component to rule and return
                        rule.addComponent(foundComponent2);
                        //Log component found
                        //if (isDbg)
                        //{
                        //    sbDbg.AppendFormat("Found with store number = {0}, guid = {1}{2}",
                        //        site.StoreNumber, 
                        //        foundComponent2.Id,
                        //        Environment.NewLine);
                        //}
                        continue;
                    }
                    var fndStateNumComps = from comp in components
                                           where comp.Alias.IndexOf(site.State, StringComparison.OrdinalIgnoreCase) != -1
                                           select comp;

                    if (fndStateNumComps.Count() <= 0)
                    {
                        if (isDbg)
                        {
                            sbDbg.AppendFormat("OR using state {0}||{1}", site.State, Environment.NewLine);
                            loggedDebug = true;
                        }
                    }
                    else
                    {
                        var foundComponent3 = fndStateNumComps.FirstOrDefault();
                        //Add component to rule and return
                        rule.addComponent(foundComponent3);
                        //Log component found
                        //if (isDbg)
                        //{
                        //    sbDbg.AppendFormat("Found with state = {0}, guid = {1}{2}",
                        //        site.State,
                        //        foundComponent3.Id,
                        //        Environment.NewLine);
                        //}
                        continue;
                    }

                    var fndAllComps = from comp in components
                                      where comp.Alias.Equals("All", StringComparison.OrdinalIgnoreCase)
                                      select comp;

                    //If we cannot find the all component, then something is wrong with the rules data
                    if (fndAllComps.Count() <= 0)
                    {
                        if (isWarn)
                        {
                            FileLogger.Instance.logMessage(LogLevel.WARN, "RulesHelper", "Warning Messages:{0}{1}", Environment.NewLine, sbWarn.ToString());
                        }
                        string errMsg = string.Format("Could not find rule component {0} with ALL alias!", detail.ComponentCode);
                        if (FileLogger.Instance != null && FileLogger.Instance.IsLogFatal)
                        {
                            FileLogger.Instance.logMessage(LogLevel.FATAL, "RulesHelper", errMsg);
                        }
                        var appEx = new ApplicationException(errMsg);
                        BasicExceptionHandler.Instance.AddException(errMsg,appEx);
                        //If we get to this point we must stop execution of current transaction
                        throw appEx;
                    }
                    var foundComponent = fndAllComps.FirstOrDefault();
                    //Add component to rule and return
                    rule.addComponent(foundComponent);
                    //Log component found
                    //if (isDbg)
                    //{
                    //    sbDbg.AppendFormat("Found with alias = All, guid = {0}{1}",                            
                    //        foundComponent.Id, Environment.NewLine);
                    //}
                    continue;
                }
                else
                {
                    if (isWarn)
                    {
                        FileLogger.Instance.logMessage(LogLevel.WARN, "RulesHelper", "Warning Messages:{0}{1}", Environment.NewLine, sbWarn.ToString());
                    }
                    var errMsg2 = string.Format("Could not find a rule component with code = {0}", detail.ComponentCode);
                    if (FileLogger.Instance != null && FileLogger.Instance.IsLogFatal)
                    {
                        FileLogger.Instance.logMessage(LogLevel.ERROR, "RulesHelper", errMsg2);
                    }
                    var appEx2 = new ApplicationException(errMsg2);
                    BasicExceptionHandler.Instance.AddException(errMsg2, appEx2);
                    //If we get to this point we must stop execution of current transaction
                    throw appEx2;
                }
            }

          //  if (loggedWarn)
          //  {
          //      FileLogger.Instance.logMessage(LogLevel.WARN, "RulesHelper", "Warning Messages:{0}{1}", Environment.NewLine, sbWarn.ToString());                
          //  }
          //  if (isDbg)
          //  {
          //      FileLogger.Instance.logMessage(LogLevel.DEBUG, "RulesHelper", "{0}{1}", Environment.NewLine, sbDbg.ToString());                
          //  }
        }

        private static BusinessRuleComponentVO GetFilteredComponent(string compRefName, ref List<BusinessRuleComponentVO> components)
        {
            List<BusinessRuleComponentVO> filteredComps = (from c in components
                                                           where c.Code.Equals(compRefName, StringComparison.OrdinalIgnoreCase)
                                                           select c).ToList<BusinessRuleComponentVO>();


            return null;
        }

        #endregion Private Methods

        #region Class

        /// <summary>
        /// Singleton just to ensure only one XML file is opened and
        /// loaded into memory.  This may need to change in the future
        /// for refreshes, etc. 
        /// </summary>        
        public sealed class RulesXML : MarshalByRefObject, IDisposable
        {
            static readonly RulesXML instance = new RulesXML();
            public override object InitializeLifetimeService()
            {
                return null;
            }

            static RulesXML()
            {                
            }

            public static RulesXML Instance
            {
                get
                {
                    return (instance);
                }
            }

            //private static volatile RulesXML _instance;
            //private static object _syncRoot = new Object();
            private XDocument _document = null;

            /// <summary>
            /// Using a singleton to ensure the document is only loaded once.
            /// </summary>
            public XDocument Document
            {
                get { return this._document; }
            }

            public RulesXML()
            {
                if (_document != null)
                    return;
                if (_workFromTempFile)
                {
                    _document = XDocument.Load(_WORKING_XML_FILE_PATH);
                }
                else
                {
                    _document = XDocument.Load(_XML_RULES_FILE_PATH);
                }

            }
            public void ReloadDoc()
            {
                if (_workFromTempFile)
                {
                    //No longer a singleton really.
                    _document = XDocument.Load(_WORKING_XML_FILE_PATH);
                }
                else
                {
                    _document = XDocument.Load(_XML_RULES_FILE_PATH);
                }
            }

            public void Dispose()
            {
                _document = null;
            }
        }

        public sealed class RulesData : MarshalByRefObject, IDisposable
        {
            static readonly RulesData instance = new RulesData();
            public override object InitializeLifetimeService()
            {
                return null;
            }

            static RulesData()
            {                
            }

            public static RulesData Instance
            {
                get
                {
                    return (instance);
                }
            }
            #region Private Members
            
            private List<BusinessRuleComponentVO> _rulesComponentsVO = null;
            private Dictionary<string, BusinessRuleCompLookupVO> _rulesVO = null;
            private List<string> _checkLoanRangeComps = null;
            
            #endregion Private Members

            #region Public Properties

            /// <summary>
            /// Dictionary of all business rules.
            /// </summary>
            public Dictionary<string, BusinessRuleCompLookupVO> RulesVO
            {
                get { return this._rulesVO; }
            }

            /// <summary>
            /// List of all business rules components.
            /// </summary>
            public List<BusinessRuleComponentVO> RulesComponents
            {
                get { return this._rulesComponentsVO; }
            }

            #endregion Public Properties

            private RulesData()
            {
                LoadRules();
            }


            #region Public Methods

            /// <summary>
            /// Sets up the rules and components in the instance
            /// </summary>
            public void LoadRules()
            {
                RulesXML.Instance.ReloadDoc();

                this._checkLoanRangeComps = LoadLoanRangeRefsFromXML();
                this._rulesComponentsVO = LoadRuleComponentsFromXML();
                this._rulesVO = LoadRuleLookupsFromXML();
            }

/*            public void DumpRules()
            {
                var altFileName = string.Format("c:\\rules_{0}_{1}.xml", DateTime.Now.Date.FormatDate().Replace("/", "_"),
                                                DateTime.Now.Ticks);
                this.SerializeRulesToXML(RulesHelper.BuildBusinessRuleMap(), RulesHelper.GetComponentList(), altFileName, false);
            }*/

            public void SerializeRulesToXML(Dictionary<string, BusinessRuleNodeVO> rules, List<BusinessRuleComponentVO> components, string altFileName = "", bool reloadAtEnd = true)
            {

                try
                {
                    //Modified to ensure that the xml file is human readable 
                    //XmlTextWriter writer = new XmlTextWriter(_WORKING_XML_FILE_PATH, null);
                    XmlWriterSettings xSet = new XmlWriterSettings();
                    xSet.IndentChars = "  ";
                    xSet.Indent = true;
                    xSet.NewLineChars = "" + System.Environment.NewLine;
                    xSet.NewLineHandling = NewLineHandling.Entitize;
                    if (string.IsNullOrEmpty(altFileName))
                    {
                        altFileName = _WORKING_XML_FILE_PATH;
                    }
                    XmlWriter writer = XmlWriter.Create(altFileName, xSet);

                    List<String> addedComps = new List<string>();

                    writer.WriteStartElement("BusinessRules");


                    //****************************************************
                    //BUSINESS RULES AND COMPONENT REFERENCE
                    //****************************************************
                    foreach (string k in rules.Keys)
                    {
                        if (k == null) continue;
                        BusinessRuleNodeVO bvo = rules[k];
                        if (bvo == null) continue;
                        writer.WriteStartElement("BusinessRule");
                        writer.WriteAttributeString("name", bvo.Code);
                        writer.WriteAttributeString("id", XmlConvert.ToString(bvo.Id));
                        if (bvo.ComponentList != null)
                        {
                            foreach (BusinessRuleComponentVO bvco in bvo.ComponentList)
                            {
                                writer.WriteStartElement("BusinessRuleCompRef");
                                writer.WriteAttributeString("name", bvco.Code);
                                writer.WriteElementString(
                                    "ComponentType",
                                    BusinessRuleComponentVO.GetRuleValueTypeString(bvco.ValueType));
                                writer.WriteEndElement(); //BusinessRuleCompRef
                            }
                        }

                        writer.WriteEndElement();   //BusinessRule
                    }


                    //****************************************************
                    //BUSINESS RULE COMPONENT VALUES
                    //****************************************************
                    foreach (BusinessRuleComponentVO bvco in components)
                    {

                        string valType =
                            BusinessRuleComponentVO.GetRuleValueTypeString(bvco.ValueType);

                        writer.WriteStartElement("BusinessRuleComponent");
                        writer.WriteAttributeString("name", bvco.Code);
                        writer.WriteAttributeString("ParentAlias", bvco.ParentAlias);
                        writer.WriteAttributeString("ParentId", XmlConvert.ToString(bvco.ParentId));
                        writer.WriteAttributeString("Id", XmlConvert.ToString(bvco.Id));                        
                        writer.WriteElementString("Alias", bvco.Alias);
                        writer.WriteElementString("FromDate", bvco.FromDate.FormatDate());
                        writer.WriteElementString("ToDate", bvco.ToDate.FormatDate());
                        writer.WriteElementString("ComponentType", valType);
                        writer.WriteElementString("IsEditable", XmlConvert.ToString(bvco.IsEditable));
                        
                        switch (bvco.ValueType)
                        {
                            case BusinessRuleComponentVO.RuleValueType.PARAM:
                                ParamVO pVo = bvco.ParamValue;
                                writer.WriteStartElement("Param");
                                if (pVo != null)
                                {
                                    //writer.WriteElementString("Alias", pVo.Alias.Code);
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
                                    //writer.WriteElementString("Alias", iVo.Alias.Code);
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
                                    //writer.WriteElementString("Alias", fVo.Alias.Code);
                                    writer.WriteElementString("FeeType", fVo.FeeType.Code);
                                    writer.WriteElementString("FeeLookupType", fVo.FeeLookupType.Code);
                                    writer.WriteElementString("Value", fVo.Value);
                                }
                                writer.WriteEndElement(); //Interest
                                break;
                            case BusinessRuleComponentVO.RuleValueType.METHOD:
                                MethodVO methodVo = bvco.MethodValue;
                                writer.WriteStartElement("Method");
                                if (methodVo != null)
                                {
                                    
                                    writer.WriteElementString("MethodName", methodVo.MethodName);
                                    writer.WriteElementString("ReturnTypeName", methodVo.ReturnTypeName);
                                    writer.WriteStartElement("Params");
                                    if (methodVo.Params != null)
                                    {
                                        foreach (MethodParamVO p in methodVo.Params)
                                        {
                                            writer.WriteStartElement("Param");
                                            writer.WriteElementString("TypeName", p.TypeName);
                                            writer.WriteElementString("IsOutParam", XmlConvert.ToString(p.IsOutParam));
                                            writer.WriteElementString("KeyName", p.KeyName);
                                            writer.WriteElementString("ParamValue",  p.ParamValue);
                                            writer.WriteEndElement();

                                        }
                                    }
                                    writer.WriteEndElement();
                                }
                                writer.WriteEndElement(); //Interest

                                break;
                        }

                        writer.WriteEndElement(); //BusinessRuleComponent
                    }



                    writer.WriteStartElement("CheckLoanRangeRef");

                    //****************************************************
                    //EXTRA CRITERIA FILTERING
                    //****************************************************
                    //Add check loan range filter content to serialization.
                    foreach (BusinessRuleComponentVO bvco in components)
                    {

                        if (CheckLoanRange(bvco.Code))
                        {
                            //Just add the component one time in case it's in the list
                            //various times.
                            if (!addedComps.Exists(s => s.Equals(bvco.Code)))
                            {
                                writer.WriteElementString("ComponentName", bvco.Code);
                                addedComps.Add(bvco.Code);
                            }
                        }

                    }
                    writer.WriteEndElement();   //CheckLoanRangeRef

                    ////Do not see that there are any check loan amounts
                    //// at this time.
                    //writer.WriteStartElement("CheckLoanAmountRef");
                    //foreach (BusinessRuleComponentVO bvco in components)
                    //{
                    //    if (bvco.CheckLoanAmount)
                    //    {
                    //        writer.WriteElementString("ComponentName", bvco.Code);
                    //    }

                    //}
                    //writer.WriteEndElement();   //CheckLoanAmountRef

                    writer.WriteEndElement(); //BusinessRules

                    //Close the writer!
                    writer.Close();

                    //After save reload rules into memory.
                    if (reloadAtEnd)
                    {
                        LoadRules();
                    }
                }
                catch (Exception eX)
                {
                    throw new ApplicationException("Could not serialize rule set: " + eX.Message, eX);
                }
            }

            /// <summary>
            /// Checks to see if the component should 
            /// have its loan range checked when loaded.
            /// </summary>
            /// <param name="compCode"></param>
            /// <returns></returns>
            public bool CheckLoanRange(string compCode)
            {
                return _checkLoanRangeComps.Exists(s => s.Equals(compCode));
            }

            #endregion Public Methods

            #region Private Methods

            private List<BusinessRuleComponentVO> LoadRuleComponentsFromXML()
            {
                try
                {
                    //PawnRules rules = new PawnRules();
                    List<BusinessRuleComponentVO> componentsList = new List<BusinessRuleComponentVO>();
                    
                    XDocument rulesXML = RulesXML.Instance.Document;

                    List<XElement> ruleCompElems = (from r in rulesXML.Descendants("BusinessRuleComponent")
                                                    select r).ToList();
                    foreach (XElement el in ruleCompElems)
                    {
                        Guid id = XmlConvert.ToGuid(el.Attribute("Id").Value);
                        Guid parentId = XmlConvert.ToGuid(el.Attribute("ParentId").Value);
                        string compName = el.Attribute("name").Value;
                        string parentAlias = el.Attribute("ParentAlias").Value;
                        BusinessRuleComponentVO.RuleValueType type =
                            (BusinessRuleComponentVO.RuleValueType)Enum.Parse(typeof(BusinessRuleComponentVO.RuleValueType),
                                                   el.Element("ComponentType").Value);
                        DateTime fromDate = XmlConvert.ToDateTime(el.Element("FromDate").Value, "MM/dd/yyyy");
                        DateTime toDate = XmlConvert.ToDateTime(el.Element("ToDate").Value, "MM/dd/yyyy");

                        string alias = el.Element("Alias").Value;
                        bool isEditable = el.Element("IsEditable") == null
                                              ? true
                                              : XmlConvert.ToBoolean(el.Element("IsEditable").Value);
                        bool checkLoanRange = this.CheckLoanRange(compName);
                        BusinessRuleComponentVO component = new BusinessRuleComponentVO(id, compName, type, isEditable,
                            fromDate, toDate, alias, parentId, parentAlias);

                        switch (type)
                        {
                            case BusinessRuleComponentVO.RuleValueType.PARAM:

                                //Instantiate ParamValue and populate since we know that's what's in XML                            
                                XElement paramElem = el.Element("Param");

                                component.ParamValue = new ParamVO();
                                //component.ParamValue.Code = paramElem.Element("Alias").Value;
                                component.ParamValue.DataType.Code =
                                    paramElem.Element("DataType").Value;
                                component.ParamValue.Value = paramElem.Element("Value").Value;
                                component.ParamValue.Company = paramElem.Element("Company").Value;
                                component.ParamValue.State = paramElem.Element("State").Value;
                                component.ParamValue.StoreNumber =
                                    paramElem.Element("StoreNumber").Value;
                                component.ParamValue.Cacheable =
                                    XmlConvert.ToBoolean(paramElem.Element("Cacheable").Value);

                                break;
                            case BusinessRuleComponentVO.RuleValueType.INTEREST:

                                //Instantiate Interest and populate since we know that's what's in XML                            
                                XElement intElem = el.Element("Interest");

                                component.InterestValue = new InterestVO();
                                //component.InterestValue.Alias.Code = intElem.Element("Alias").Value;
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
                                //component.FeesValue.Alias.Code = feeElem.Element("Alias").Value;
                                component.FeesValue.FeeType.Code = feeElem.Element("FeeType").Value;
                                component.FeesValue.FeeLookupType.Code =
                                    feeElem.Element("FeeLookupType").Value;
                                component.FeesValue.Value = feeElem.Element("Value").Value;

                                break;
                            case BusinessRuleComponentVO.RuleValueType.METHOD:
                                XElement methodElem = el.Element("Method");
                                component.MethodValue = new MethodVO();
                                component.MethodValue.MethodName =
                                    methodElem.Element("MethodName").Value;
                                component.MethodValue.ReturnTypeName =
                                    methodElem.Element("ReturnTypeName").Value;

                                component.MethodValue.Params = new List<MethodParamVO>();

                                XElement methodParams = methodElem.Element("Params");
                                if (methodParams.HasElements)
                                {
                                
                                    List<XElement> paramList =
                                        (from r in methodParams.Descendants("Param")
                                         select r).ToList();
                                    component.MethodValue.Params = new List<MethodParamVO>();
                                    foreach (XElement paramEl in paramList)
                                    {
                                        MethodParamVO mp = new MethodParamVO();
                                        mp.IsOutParam =
                                            XmlConvert.ToBoolean(paramEl.Element("IsOutParam").Value);
                                        mp.KeyName = paramEl.Element("KeyName").Value;
                                        mp.TypeName = paramEl.Element("TypeName").Value;
                                        mp.ParamValue = paramEl.Element("ParamValue").Value;
                                    }
                                }

                        break;
                        }

                        componentsList.Add(component);
                    }

                    return (from c in componentsList
                                orderby c.Code
                                select c).ToList();
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Could not de-serialize pawn rule set", ex);
                }
            }

            /// <summary>
            /// Loads business rules from XML data cache.
            /// </summary>
            /// <returns></returns>
            private static Dictionary<string, BusinessRuleCompLookupVO> LoadRuleLookupsFromXML()
            {
                try
                {
                    Dictionary<string, BusinessRuleCompLookupVO> rules = new Dictionary<string, BusinessRuleCompLookupVO>();

                    XDocument rulesXML = RulesXML.Instance.Document;

                    List<XElement> busRules = (from r in rulesXML.Descendants("BusinessRule")
                                               select r).ToList();
                    //Before loading rules, load the components for lookup and association
                    //to the business rules.
                    //List<BusinessRuleComponentVO> componentsLookup = LoadRuleComponentsFromXML();

                    foreach (XElement el in busRules)
                    {
                        string code = el.Attribute("name").Value;

                        //If missing and id then create a new one.
                        Guid id = el.Attribute("id") == null ? Guid.NewGuid() :
                                    XmlConvert.ToGuid(el.Attribute("id").Value);

                        List<ComponentDetails> compCodes = new List<ComponentDetails>();                        

                        List<XElement> compRefs = (from r in el.Descendants("BusinessRuleCompRef")
                                                   select r).ToList<XElement>();

                        foreach (XElement el2 in compRefs)
                        {
                            ComponentDetails detail = new ComponentDetails
                                                      {
                                                          ComponentCode = el2.Attribute("name").Value,
                                                          ComponentType = el2.Element("ComponentType").Value
                                                      };

                            //Do not add duplicate comp codes it will cause
                            //the hierarchy tree to show multiples...
                            if((!(from c in compCodes
                                    where c.ComponentCode.Equals(detail.ComponentCode, StringComparison.OrdinalIgnoreCase)
                                    select c).Any()))
                            {
                                compCodes.Add(detail);
                            }
                        }

                        BusinessRuleCompLookupVO lookup = new BusinessRuleCompLookupVO(code, compCodes, id);
                       
                        rules.Add(lookup.Code, lookup);
                    }

                    return rules;
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Could not de-serialize business rule set", ex);
                }
            }

            private List<string> LoadLoanRangeRefsFromXML()
            {
                List<string> refs = new List<string>();
                XDocument rulesXML = RulesXML.Instance.Document;

                List<XElement> refElems = (from r in rulesXML.Descendants("CheckLoanRangeRef")
                                           select r).ToList();

                foreach (XElement el in refElems)
                {
                    refs.Add(el.Element("ComponentName").Value);
                }

                return refs;
            }


            #endregion Private Methods

            public void Dispose()
            {
                //Dispose some crap
                if (_rulesComponentsVO != null)
                {
                    _rulesComponentsVO.Clear();
                    _rulesComponentsVO = null;
                }
                if (_rulesVO != null)
                {
                    _rulesVO.Clear();
                    _rulesVO = null;
                }

                if (_checkLoanRangeComps != null)
                {
                    _checkLoanRangeComps.Clear();
                    _checkLoanRangeComps = null;
                }
            }
        }

        #endregion

    }
}
