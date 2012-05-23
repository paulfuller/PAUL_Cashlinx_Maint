using System;
using System.IO;
using NAnt.Core;
using NAnt.Core.Attributes;

namespace Cashlinx.Build.Tasks.Config
{
    [TaskName("configwriter")]
    public class CashlinxConfigurationWriter : Task
    {
        public CashlinxConfigurationWriter()
        {
            Application = ClxApplication.Cashlinx;
        }

        [TaskAttribute("app")]
        public string App { get; set; }

        [TaskAttribute("application")]
        public ClxApplication Application { get; set; }

        [TaskAttribute("file", Required = true)]
        public string FileName { get; set; }

        [TaskAttribute("configkey", Required = true)]
        public ConfigurationKey ConfigKey { get; set; }

        protected override void ExecuteTask()
        {
            if (!string.IsNullOrWhiteSpace(App))
            {
                if (App.Equals("audit", StringComparison.InvariantCultureIgnoreCase))
                {
                    Application = ClxApplication.Audit;
                }
                else if (App.Equals("auditqueries", StringComparison.InvariantCultureIgnoreCase))
                {
                    Application = ClxApplication.AuditQueries;
                }
                else if (App.Equals("cashlinx", StringComparison.InvariantCultureIgnoreCase))
                {
                    Application = ClxApplication.Cashlinx;
                }
                else if (App.Equals("dstrviewer", StringComparison.InvariantCultureIgnoreCase))
                {
                    Application = ClxApplication.DSTRViewer;
                }
                else if (App.Equals("support", StringComparison.InvariantCultureIgnoreCase))
                {
                    Application = ClxApplication.Support;
                }
            }

            this.Log(Level.Info, "Writing config file: " + ConfigKey.ToString());

            string configValue = ConfigurationReader.GetInstance().GetConfigurationContents(Application, ConfigKey);

            using (StreamWriter sw = new StreamWriter(FileName, false))
            {
                sw.Write(configValue);
            }
        }

        
    }
}
