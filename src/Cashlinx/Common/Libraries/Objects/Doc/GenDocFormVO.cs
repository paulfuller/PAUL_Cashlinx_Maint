using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Collection;
using Common.Libraries.Utility.Logger;

namespace Common.Libraries.Objects.Doc
{
    public class GenDocFormVO
    {
        public class FormDetail
        {
            public string AddendumNeeded { set; get; }
            public string Category { set; get; }
            public string CountryCode { set; get; }
            public string CreatedBy { set; get; }
            public string EffectiveDate { set; get; }
            public string ElectronicSigRequired { set; get; }
            public string ExpireDate { set; get; }
            public string TemplateFileName { set; get; }
            public string FormLocation { set; get; }
            public string GenDocId { set; get; }
            public string LastUpdateDate { set; get; }
            public string NumberCopies { set; get; }
            public string PaperType { set; get; }
            public string SignatureRequired { set; get; }
            public string StateCode { set; get; }
            public string Status { set; get; }
            public string TrayPull { set; get; }
            public string UpdatedBy { set; get; }
            public string BarCodePosition { set; get; }

            public FormDetail()
            {
                AddendumNeeded = string.Empty;
                Category = string.Empty;
                CountryCode = string.Empty;
                CreatedBy = string.Empty;
                EffectiveDate = string.Empty;
                ElectronicSigRequired = string.Empty;
                ExpireDate = string.Empty;
                TemplateFileName = string.Empty;
                FormLocation = string.Empty;
                GenDocId = string.Empty;
                LastUpdateDate = string.Empty;
                NumberCopies = string.Empty;
                PaperType = string.Empty;
                SignatureRequired = string.Empty;
                StateCode = string.Empty;
                Status = string.Empty;
                TrayPull = string.Empty;
                UpdatedBy = string.Empty;
                BarCodePosition = string.Empty;
            }
        }

        public class IPPortDetails
        {
            public string IPAddress { set; get; }
            public string PortNumber { set; get; }
            public string PeripheralId { set; get; }
            public string PeripheralName { set; get; }
            public string PeripheralTypeId { set; get; }
            public string ModelId { set; get; }
            public string StoreId { set; get; }
            public string PrefOrder { set; get; }

            public IPPortDetails()
            {
                IPAddress = string.Empty;
                PortNumber = string.Empty;
                PeripheralId = string.Empty;
                PeripheralName = string.Empty;
                PeripheralTypeId = string.Empty;
                ModelId = string.Empty;
                StoreId = string.Empty;
                PrefOrder = string.Empty;
            }
        }

        public class WorkstationPeripheralMapping
        {
            public string WorkstationId { set; get; }
            public string PeripheralId { set; get; }
            public string PreferenceOrder { set; get; }
            public string IpAddress { set; get; }
            public string PortNumber { set; get; }

            public WorkstationPeripheralMapping()
            {
                WorkstationId = string.Empty;
                PreferenceOrder = string.Empty;
                IpAddress = string.Empty;
                PortNumber = string.Empty;
                PeripheralId = string.Empty;
            }
        }

        private List<FormDetail> formDetails;
        private List<IPPortDetails> ipPortDetails;
        private List<WorkstationPeripheralMapping> workstationMappings;
        private string workstationId;
        private string formName;
        private string storeId;

        private Hashtable computedHashTable;
        public Hashtable ComputedHashTable
        {
            get
            {
                return (this.computedHashTable);
            }
        }

        private WorkstationPeripheralMapping foundWorkstation;
        public WorkstationPeripheralMapping FoundWorkstation
        {
            get
            {
                return (foundWorkstation);
            }
        }
        private IPPortDetails foundPeripheral;
        public IPPortDetails FoundPeripheral
        {
            get
            {
                return (foundPeripheral);
            }
        }

        private FormDetail foundForm;
        public FormDetail FoundForm
        {
            get
            {
                return (foundForm);
            }
        }

        public GenDocFormVO(string workstatId, string formNam, string storId)
        {
            this.formDetails = new List<FormDetail>(1);
            this.ipPortDetails = new List<IPPortDetails>(1);
            this.workstationMappings = new List<WorkstationPeripheralMapping>(1);
            this.computedHashTable = new Hashtable(30);
            this.foundWorkstation = null;
            this.foundPeripheral = null;
            this.foundForm = null;
            this.workstationId = workstatId;
            this.formName = formNam;
            this.storeId = storId;
        }


        public bool GetDataAndCompute(
            DataTable formTable,
            DataTable wkstMapTable,
            DataTable ipPortTable)
        {
            if (string.IsNullOrEmpty(workstationId) ||
                string.IsNullOrEmpty(formName) ||
                string.IsNullOrEmpty(storeId))
            {
                return (false);
            }

            try
            {
                if (formTable == null || wkstMapTable == null || ipPortTable == null || formTable.HasErrors || wkstMapTable.HasErrors || ipPortTable.HasErrors || formTable.Rows == null || wkstMapTable.Rows == null || ipPortTable.Rows == null || formTable.Rows.Count <= 0 ||
                    wkstMapTable.Rows.Count <= 0 || ipPortTable.Rows.Count <= 0)
                {
                    return (false);
                }

                //Clear computed hash table
                this.computedHashTable.Clear();
                this.formDetails.Clear();
                this.workstationMappings.Clear();
                this.ipPortDetails.Clear();

                //Find form
                foreach (DataRow r in formTable.Rows)
                {
                    if (r == null) continue;
                    string fmName = Utilities.GetStringValue(r["form"], string.Empty);
                    if (!string.IsNullOrEmpty(fmName) && fmName.Equals(this.formName, StringComparison.OrdinalIgnoreCase))
                    {
                        var fmDetail = new FormDetail();
                        fmDetail.AddendumNeeded = Utilities.GetStringValue(r["addendum_needed"], string.Empty);
                        fmDetail.Category = Utilities.GetStringValue(r["category"], string.Empty);
                        fmDetail.CountryCode = Utilities.GetStringValue(r["country_code"], string.Empty);
                        fmDetail.CreatedBy = Utilities.GetStringValue(r["createdby"], string.Empty);
                        fmDetail.EffectiveDate = Utilities.GetStringValue(r["effective_date"], string.Empty);
                        fmDetail.TemplateFileName = Utilities.GetStringValue(r["form"], string.Empty);
                        fmDetail.NumberCopies = Utilities.GetStringValue(r["copies"], string.Empty);
                        fmDetail.FormLocation = Utilities.GetStringValue(r["form_location"], string.Empty);

                        //Add form to list
                        this.formDetails.Add(fmDetail);
                        break;
                    }
                }

                //If form details were not found, exit
                if (CollectionUtilities.isEmpty(this.formDetails)) return (false);

                //Get form detail object
                FormDetail fmObj = this.formDetails.First();

                //Ensure that form location is valid
                if (fmObj == null || string.IsNullOrEmpty(fmObj.FormLocation)) return (false);

                //Find peripherals matching form location from form details above
                foreach (DataRow r in ipPortTable.Rows)
                {
                    if (r == null) continue;

                    var ipDetail = new IPPortDetails();
                    ipDetail.IPAddress = Utilities.GetStringValue(r["ipaddress"], string.Empty);
                    ipDetail.PortNumber = Utilities.GetStringValue(r["portnumber"], string.Empty);
                    ipDetail.ModelId = Utilities.GetStringValue(r["modelid"], string.Empty);
                    ipDetail.PeripheralId = Utilities.GetStringValue(r["peripheralid"], string.Empty);
                    ipDetail.PeripheralName = Utilities.GetStringValue(r["peripheralname"], string.Empty);
                    ipDetail.PeripheralTypeId = Utilities.GetStringValue(r["peripheraltypeid"], string.Empty);
                    ipDetail.StoreId = Utilities.GetStringValue(r["storeid"], string.Empty);

                    //Match the type of the peripheral with the form location - the form's required peripheral type id
                    if (!string.IsNullOrEmpty(ipDetail.PeripheralTypeId) && 
                        !string.IsNullOrEmpty(ipDetail.StoreId) && 
                        ipDetail.PeripheralTypeId.Equals(fmObj.FormLocation, StringComparison.OrdinalIgnoreCase) && 
                        ipDetail.StoreId.Equals(this.storeId, StringComparison.OrdinalIgnoreCase))
                    {
                        this.ipPortDetails.Add(ipDetail);
                    }
                }

                //Ensure that at least one peripheral was found
                if (CollectionUtilities.isEmpty(this.ipPortDetails)) return (false);

                //Find workstation peripheral mappings
                foreach (DataRow r in wkstMapTable.Rows)
                {
                    if (r == null) continue;

                    var wkstDetail = new WorkstationPeripheralMapping();
                    wkstDetail.WorkstationId = Utilities.GetStringValue(r["workstationid"], string.Empty);
                    wkstDetail.PeripheralId = Utilities.GetStringValue(r["peripheralid"], string.Empty);

                    //Add the workstation mapping
                    if (!string.IsNullOrEmpty(wkstDetail.WorkstationId) && 
                        !string.IsNullOrEmpty(wkstDetail.PeripheralId))
                    {
                        this.workstationMappings.Add(wkstDetail);
                    }
                }

                //Ensure that at least one workstation mapping was found
                if (CollectionUtilities.isEmpty(this.workstationMappings)) return (false);

                //Find the peripheral to workstation mapping relationship
                foreach (var wkst in this.workstationMappings)
                {
                    if (wkst == null) continue;

                    var wkstPerId = wkst.PeripheralId;
                    var ipPortObjFnd = this.ipPortDetails.Find(
                        x => x.PeripheralId.Equals(wkstPerId, 
                            StringComparison.OrdinalIgnoreCase));

                    if (ipPortObjFnd != null)
                    {
                        this.foundPeripheral = ipPortObjFnd;
                        this.foundForm = fmObj;
                        this.foundWorkstation = wkst;
                        break;
                    }
                }

                //Generate found form hash details
                if (this.foundForm != null)
                {
                    this.computedHashTable["##TEMPLATEFILENAME##"] = this.foundForm.TemplateFileName;
                    this.computedHashTable["##HOWMANYCOPIES##"] = this.foundForm.NumberCopies;
                }

                //Generate output hash map
                if (this.foundPeripheral != null && 
                    this.foundForm != null && 
                    this.foundWorkstation != null)
                {
                    this.computedHashTable["##IPADDRESS00##"] = this.foundPeripheral.IPAddress;
                    this.computedHashTable["##IPADDRESS01##"] = this.foundPeripheral.IPAddress;
                    this.computedHashTable["##PORTNUMBER00##"] = this.foundPeripheral.PortNumber;
                    this.computedHashTable["##PORTNUMBER01##"] = this.foundPeripheral.PortNumber;
                }
                else
                {
                    //As a backup, if there are no workstation specific mappings,
                    //try to choose a peripheral that matches the type specified
                    //by the form
                    if (CollectionUtilities.isNotEmpty(this.ipPortDetails))
                    {
                        var fndPrinter = 
                            this.ipPortDetails.Find(
                                x => x.PeripheralTypeId.Equals(
                                    fmObj.FormLocation, StringComparison.OrdinalIgnoreCase));
                        if (fndPrinter != null)
                        {
                            this.computedHashTable["##IPADDRESS00##"] = fndPrinter.IPAddress;
                            this.computedHashTable["##IPADDRESS01##"] = fndPrinter.IPAddress;
                            this.computedHashTable["##PORTNUMBER00##"] = fndPrinter.PortNumber;
                            this.computedHashTable["##PORTNUMBER01##"] = fndPrinter.PortNumber;
                            return (true);
                        }
                    }
                    return (false);
                }
            }
            catch (Exception ex)
            {
                if (FileLogger.Instance.IsLogFatal)
                {
                    FileLogger.Instance.logMessage(LogLevel.FATAL, null, "Population of generate documents map failed: {0}: {1} ", ex.Message, ex.StackTrace);
                }
                return (false);
            }

            return (true);
        }

    }
}
