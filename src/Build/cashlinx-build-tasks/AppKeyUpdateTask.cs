using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Cashlinx.Build.Tasks.Config;
using NAnt.Core;
using NAnt.Core.Attributes;

namespace Cashlinx.Build.Tasks
{
    [TaskName("appkeyupdate")]
    public class AppKeyUpdateTask : Task
    {
        public AppKeyUpdateTask()
        {
            Application = ClxApplication.Cashlinx;
        }

        private ClxApplication Application { get; set; }

        [TaskAttribute("sqlfilename")]
        public string SqlFileName { get; set; }

        [TaskAttribute("configkey", Required = true)]
        public ConfigurationKey ConfigKey { get; set; }

        protected override void ExecuteTask()
        {
            if (string.IsNullOrWhiteSpace(SqlFileName))
            {
                throw new BuildException("SqlFileName cannot be null or empty.");
            }

            if (!File.Exists(SqlFileName))
            {
                throw new BuildException("File does not exist: " + SqlFileName);
            }

            string fileContents = File.ReadAllText(SqlFileName, Encoding.ASCII);

            if (string.IsNullOrWhiteSpace(fileContents))
            {
                throw new BuildException("Invalid file: " + SqlFileName);
            }

            ConnectionStringLoader loader = ConnectionStringLoader.GetInstance();
            DatabaseConnectionStrings connectionStrings = loader.GetConnectionString(Application, ConfigKey);

            fileContents = Regex.Replace(fileContents, ";\\W+COMMIT;\\W+", string.Empty, RegexOptions.Multiline);

            Database database = new Database(connectionStrings.PawnSecConnectionInfo.ConnectionString);
            database.ExecuteNonQuery(fileContents);
        }
    }
}
