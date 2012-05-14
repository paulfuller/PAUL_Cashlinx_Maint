using NAnt.Core;

namespace Cashlinx.Build.Tasks.Config
{
    public class ConfigurationSettingsLoader : Singleton<ConfigurationSettingsLoader>
    {
        public ConfigurationSettings GetConfigurationSettings(ClxApplication application, ConfigurationKey key)
        {
            switch (key)
            {
                case ConfigurationKey.CLXD:
                    return new ConfigurationSettings()
                    {
                        PawnSecDBHost = "1xaOn6Ot6HRjXbIPl7E2WJ3Bs9SmWpEy",
                        PawnSecDBPassword = "jZekk5GlbvfnRVTll7RpCw==",
                        PawnSecDBPort = "c5oa+iWxTPs=",
                        PawnSecDBSchema = "Ny2VIxVYqnA=",
                        PawnSecDBService = "q7wvRn4eb3cRrLvSkAHG8w==",
                        PawnSecDBUser = "Ny2VIxVYqnA="
                    };
                case ConfigurationKey.CLXD2:
                    return new ConfigurationSettings()
                    {
                        PawnSecDBHost = "5HYH35IsmBLxuFKgDA0deV4cSI9w/aeE",
                        PawnSecDBPassword = "jZekk5GlbvfnRVTll7RpCw==",
                        PawnSecDBPort = "c5oa+iWxTPs=",
                        PawnSecDBSchema = "Ny2VIxVYqnA=",
                        PawnSecDBService = "zZCXW9Ci9FGVpj7+YIrMdxtI3uI0kDL1",
                        PawnSecDBUser = "Ny2VIxVYqnA="
                    };
                case ConfigurationKey.CLXD3:
                    return new ConfigurationSettings()
                    {
                        PawnSecDBHost = "5HYH35IsmBLxuFKgDA0deV4cSI9w/aeE",
                        PawnSecDBPassword = "jZekk5GlbvfnRVTll7RpCw==",
                        PawnSecDBPort = "c5oa+iWxTPs=",
                        PawnSecDBSchema = "Ny2VIxVYqnA=",
                        PawnSecDBService = "tN2vG1Y6pleVpj7+YIrMdxtI3uI0kDL1",
                        PawnSecDBUser = "Ny2VIxVYqnA="
                    };
                case ConfigurationKey.CLXI:
                    return new ConfigurationSettings()
                    {
                        PawnSecDBHost = "5HYH35IsmBLxuFKgDA0deV4cSI9w/aeE",
                        PawnSecDBPassword = "jZekk5GlbvfnRVTll7RpCw==",
                        PawnSecDBPort = "c5oa+iWxTPs=",
                        PawnSecDBSchema = "Ny2VIxVYqnA=",
                        PawnSecDBService = "7GS8RS4GC4MRrLvSkAHG8w==",
                        PawnSecDBUser = "Ny2VIxVYqnA="
                    };
                case ConfigurationKey.CLXP:
                    return new ConfigurationSettings()
                    {
                        PawnSecDBHost = "bIyV3M7QftbxuFKgDA0deV4cSI9w/aeE",
                        PawnSecDBPassword = "jZekk5GlbvfnRVTll7RpCw==",
                        PawnSecDBPort = "07s4aRvDFLs=",
                        PawnSecDBSchema = "Ny2VIxVYqnA=",
                        PawnSecDBService = "ZisF3qmLAEMRrLvSkAHG8w==",
                        PawnSecDBUser = "Ny2VIxVYqnA="
                    };
                case ConfigurationKey.CLXPG:
                    return new ConfigurationSettings()
                    {
                        PawnSecDBHost = "WTzPYAETxojxuFKgDA0deV4cSI9w/aeE",
                        PawnSecDBPassword = "jZekk5GlbvfnRVTll7RpCw==",
                        PawnSecDBPort = "c5oa+iWxTPs=",
                        PawnSecDBSchema = "Ny2VIxVYqnA=",
                        PawnSecDBService = "xWlbtRgC7x+Vpj7+YIrMdxtI3uI0kDL1",
                        PawnSecDBUser = "Ny2VIxVYqnA="
                    };
                case ConfigurationKey.CLXPP:
                    return new ConfigurationSettings()
                    {
                        PawnSecDBHost = "UzeafzXCKVFUiOK4kbZnXSPfSmxEso8X7kK+s12DlYA=",
                        PawnSecDBPassword = "jZekk5GlbvfnRVTll7RpCw==",
                        PawnSecDBPort = "qHuK02GJ/XY=",
                        PawnSecDBSchema = "Ny2VIxVYqnA=",
                        PawnSecDBService = "OFifF/KYo4eVpj7+YIrMdxtI3uI0kDL1",
                        PawnSecDBUser = "Ny2VIxVYqnA="
                    };
                case ConfigurationKey.CLXQ:
                    return new ConfigurationSettings()
                    {
                        PawnSecDBHost = "bYtNZ/PbgPkj30psRLKPF+5CvrNdg5WA",
                        PawnSecDBPassword = "jZekk5GlbvfnRVTll7RpCw==",
                        PawnSecDBPort = "07s4aRvDFLs=",
                        PawnSecDBSchema = "Ny2VIxVYqnA=",
                        PawnSecDBService = "7fOnJxxDY7oRrLvSkAHG8w==",
                        PawnSecDBUser = "Ny2VIxVYqnA="
                    };
                case ConfigurationKey.CLXR2:
                    return new ConfigurationSettings()
                    {
                        PawnSecDBHost = "O2krnbvpiHLxuFKgDA0deV4cSI9w/aeE",
                        PawnSecDBPassword = "jZekk5GlbvfnRVTll7RpCw==",
                        PawnSecDBPort = "c5oa+iWxTPs=",
                        PawnSecDBSchema = "Ny2VIxVYqnA=",
                        PawnSecDBService = "aEa/5aTGcl2Vpj7+YIrMdxtI3uI0kDL1",
                        PawnSecDBUser = "Ny2VIxVYqnA="
                    };
                case ConfigurationKey.CLXT:
                    return new ConfigurationSettings()
                    {
                        PawnSecDBHost = "bYtNZ/PbgPkj30psRLKPF+5CvrNdg5WA",
                        PawnSecDBPassword = "jZekk5GlbvfnRVTll7RpCw==",
                        PawnSecDBPort = "07s4aRvDFLs=",
                        PawnSecDBSchema = "Ny2VIxVYqnA=",
                        PawnSecDBService = "BBBxdZodCA0RrLvSkAHG8w==",
                        PawnSecDBUser = "Ny2VIxVYqnA="
                    };
                case ConfigurationKey.CLXT2:
                    return new ConfigurationSettings()
                    {
                        PawnSecDBHost = "bYtNZ/PbgPkj30psRLKPF+5CvrNdg5WA",
                        PawnSecDBPassword = "jZekk5GlbvfnRVTll7RpCw==",
                        PawnSecDBPort = "07s4aRvDFLs=",
                        PawnSecDBSchema = "Ny2VIxVYqnA=",
                        PawnSecDBService = "Ny/sG2mylyCVpj7+YIrMdxtI3uI0kDL1",
                        PawnSecDBUser = "Ny2VIxVYqnA="
                    };
                default: throw new BuildException("Configuration Settings not implemented: " + application + " - " + key);
            }
        }
    }
}
