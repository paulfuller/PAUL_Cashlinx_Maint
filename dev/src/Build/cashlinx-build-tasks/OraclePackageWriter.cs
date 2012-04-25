using System;
using System.IO;
using NAnt.Core;
using NAnt.Core.Attributes;
using System.Collections.Generic;
using Cashlinx.Build.Tasks.OraclePackageWriter;

namespace Cashlinx.Build.Tasks.Config
{
    [TaskName("packagewriter")]
    public class OraclePackageWriter : Task
    {
        public OraclePackageWriter()
        {
            Application = ClxApplication.Cashlinx;
        }

        private ClxApplication Application { get; set; }

        [TaskAttribute("outputdirectory", Required = true)]
        public string OutputDirectory { get; set; }

        [TaskAttribute("configkey", Required = true)]
        public ConfigurationKey ConfigKey { get; set; }

        protected override void ExecuteTask()
        {
            ConnectionStringLoader loader = ConnectionStringLoader.GetInstance();
            DatabaseConnectionStrings connectionStrings = loader.GetConnectionString(Application, ConfigKey);

            if (ConfigKey == ConfigurationKey.CLXD3)
            {
                connectionStrings.CcsOwnerConnectionInfo.UserId = "ccsowner";
                connectionStrings.CcsOwnerConnectionInfo.Password = "prime98s";
            }

            WritePackages(connectionStrings.PawnSecConnectionInfo, "PAWNSEC");
            WritePackages(connectionStrings.CcsOwnerConnectionInfo, "CCSOWNER");
        }

        private void WritePackages(DatabaseConnectionInfo databaseConnectionInfo, string owner)
        {
            Database pawnSecDatabase = new Database(databaseConnectionInfo.ConnectionString);
            List<Package> packages = pawnSecDatabase.GetPackages(owner);

            foreach (Package package in packages)
            {
                this.Log(Level.Info, "Writing " + package.Name);

                List<Source> packageSources = pawnSecDatabase.GetPackageSources(package);
                List<Source> packageBodySources = pawnSecDatabase.GetPackageBodySources(package);

                string directory = Path.Combine(OutputDirectory, ConfigKey.ToString());
                string path = Path.Combine(directory, package.Owner + "." + package.Name + " (SPEC).sql");

                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                using (StreamWriter sw = new StreamWriter(path, false))
                {
                    foreach (Source s in packageSources)
                    {
                        sw.Write(s.Text);
                    }
                }

                path = Path.Combine(directory, package.Owner + "." + package.Name + " (BODY).sql");

                using (StreamWriter sw = new StreamWriter(path, false))
                {
                    foreach (Source s in packageBodySources)
                    {
                        sw.Write(s.Text);
                    }
                }
            }
        }

        
    }
}
