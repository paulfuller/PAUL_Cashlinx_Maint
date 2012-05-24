using System.Collections.Generic;
using Common.Controllers.Application;
using Common.Controllers.Database.Oracle;
using System.Data;
using System.Collections;
using Common.Controllers.Database.Procedures;

namespace Common.Controllers.Database
{
    public class PrintUtilities
    {
        public Dictionary<string, string> GetPrintDeviceData(string printFormName)
        {
            string errorCode = "";
            string errorMessage = "";

            string terminalId = GetTerminalId();

            var inParams = new List<OracleProcParam>
                                       {
                                           new OracleProcParam("p_terminalid", terminalId),
                                           new OracleProcParam("p_form_name", printFormName),
                                           new OracleProcParam("p_store_number", GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber)
                                       };

            DataTable documentInfo;
            DataTable ipportInfo;
            DataTable printerInfo;

            Hashtable eDeviceDataHash = GenerateDocumentsProcedures.
                    GenerateDocumentsEssentialInformation(
                    inParams,
                    out documentInfo,
                    out printerInfo,
                    out ipportInfo,
                    out errorCode,
                    out errorMessage);

            Dictionary<string, string> eDeviceData = new Dictionary<string, string>();
            foreach (string s in eDeviceDataHash.Keys)
            {
                if (string.IsNullOrEmpty(s))
                    continue;
                object hashVal = eDeviceDataHash[s];
                if (hashVal == null)
                    continue;
                eDeviceData.Add(s, hashVal.ToString());
            }

            return eDeviceData;
        }

        private string GetTerminalId()
        {
            return GlobalDataAccessor.Instance.CurrentSiteId.TerminalId;
            
        }
    }
}
