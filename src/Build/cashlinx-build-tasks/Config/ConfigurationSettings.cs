using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cashlinx.Build.Tasks.Config
{
    public class ConfigurationSettings
    {
        public string DstrProdEnv { get; set; }
        public string PawnSecDBUser { get; set; }
        public string PawnSecDBPassword { get; set; }
        public string PawnSecDBSchema { get; set; }
        public string PawnSecDBPort { get; set; }
        public string PawnSecDBHost { get; set; }
        public string PawnSecDBService { get; set; }
    }
}
