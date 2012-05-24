using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Controllers.Application;
using Common.Libraries.Objects;
using Common.Libraries.Utility.Shared;

namespace Reports
{
    public abstract class AbstractExtensionMemo : ReportBase, IExtensionMemo
    {
        protected AbstractExtensionMemo(IPdfLauncher pdfLauncher)
            : base(pdfLauncher)
        {
            Documents = new List<string>();
            ExtensionToPdfMap = new Dictionary<int, string>();
        }

        public List<string> Documents { get; private set; }
        public Dictionary<int, string> ExtensionToPdfMap { get; private set; }
        public ReportObject RptObject { get; set; }
        public string Employee { get; set; }
        public List<ExtensionMemoInfo> ExtensionMemoData { get; set; }

        public abstract bool Print();

        protected void AddDocument(ExtensionMemoInfo extensionMemoInfo, string pdfFile)
        {
            if (!Documents.Contains(pdfFile))
            {
                Documents.Add(pdfFile);
            }

            if (!ExtensionToPdfMap.ContainsKey(extensionMemoInfo.TicketNumber))
            {
                ExtensionToPdfMap.Add(extensionMemoInfo.TicketNumber, pdfFile);
            }
        }
    }
}
