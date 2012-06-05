using System.IO;
using System.Xml;
using Cashlinx.Build.Tasks.EnvironmentLoader;
using NAnt.Core;
using System.Text;

namespace Cashlinx.Build.Tasks.Config
{
    public class ConfigurationReader : Singleton<ConfigurationReader>
    {
        public string GetConfigurationContents(ClxApplication application, ConfigurationKey key)
        {
            string appConfigPath = GetSourceConfigPath(application);

            XmlDocument configDoc = new XmlDocument();
            configDoc.Load(appConfigPath);

            ConfigurationSettings settings = ConfigurationSettingsLoader.GetInstance().GetConfigurationSettings(application, key);

            string settingsNodeName = configDoc.SelectSingleNode("/configuration/configSections").FirstChild.FirstChild.Attributes["name"].Value;

            XmlNode appSettingsNode = configDoc.SelectSingleNode("/configuration/applicationSettings/" + settingsNodeName);
            foreach (XmlNode settingsNode in appSettingsNode.ChildNodes)
            {
                if (settingsNode.Attributes["name"].Value == "PawnSecDBHost")
                {
                    settingsNode.FirstChild.InnerText = settings.PawnSecDBHost;
                }
                else if (settingsNode.Attributes["name"].Value == "PawnSecDBPassword")
                {
                    settingsNode.FirstChild.InnerText = settings.PawnSecDBPassword;
                }
                else if (settingsNode.Attributes["name"].Value == "PawnSecDBPort")
                {
                    settingsNode.FirstChild.InnerText = settings.PawnSecDBPort;
                }
                else if (settingsNode.Attributes["name"].Value == "PawnSecDBSchema")
                {
                    settingsNode.FirstChild.InnerText = settings.PawnSecDBSchema;
                }
                else if (settingsNode.Attributes["name"].Value == "PawnSecDBService")
                {
                    settingsNode.FirstChild.InnerText = settings.PawnSecDBService;
                }
                else if (settingsNode.Attributes["name"].Value == "PawnSecDBUser")
                {
                    settingsNode.FirstChild.InnerText = settings.PawnSecDBUser;
                }
                else if (settingsNode.Attributes["name"].Value == "prodEnv")
                {
                    settingsNode.FirstChild.InnerText = settings.DstrProdEnv;
                }
            }

            StringBuilder sb = null;
            using (var stringWriter = new StringWriter())
            using (var xmlTextWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings() { Indent = true }))
            {
                configDoc.WriteTo(xmlTextWriter);
                sb = stringWriter.GetStringBuilder();
            }

            return sb.ToString();
        }

        private string GetSourceConfigPath(ClxApplication application)
        {
            EnvironmentInfo env = EnvironmentInfo.GetInstance();

            switch (application)
            {
                case ClxApplication.Audit: return Path.Combine(env.AuditProjectEnvironment.SourceDirectory, "app.config");
                case ClxApplication.AuditQueries: return Path.Combine(env.AuditQueriesProjectEnvironment.SourceDirectory, "app.config");
                case ClxApplication.Cashlinx: return Path.Combine(env.PawnProjectEnvironment.SourceDirectory, "app.config");
                case ClxApplication.DSTRViewer: return Path.Combine(env.DstrViewerProjectEnvironment.SourceDirectory, "app.config");
                case ClxApplication.Support: return Path.Combine(env.SupportProjectEnvironment.SourceDirectory, "app.config");
                default:
                    throw new BuildException("Application not supported: " + application);
            }
        }
    }
}
