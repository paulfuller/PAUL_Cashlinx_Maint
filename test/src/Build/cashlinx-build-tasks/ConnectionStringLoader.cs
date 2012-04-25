using System.Xml;
using Cashlinx.Build.Tasks.Config;

namespace Cashlinx.Build.Tasks
{
    public class ConnectionStringLoader : Singleton<ConnectionStringLoader>
    {
        public DatabaseConnectionStrings GetConnectionString(ClxApplication application, ConfigurationKey key)
        {
            DatabaseConnectionStrings connectionStrings = new DatabaseConnectionStrings();

            connectionStrings.PawnSecConnectionInfo = GetPawnSecDatabaseConfigurationInfo(application, key);
            connectionStrings.CcsOwnerConnectionInfo = GetCcsOwnerDatabaseConfigurationInfo(connectionStrings.PawnSecConnectionInfo);

            return connectionStrings;
        }

        private DatabaseConnectionInfo GetCcsOwnerDatabaseConfigurationInfo(DatabaseConnectionInfo pawnSecConnectionInfo)
        {
            Database database = new Database(pawnSecConnectionInfo.ConnectionString);
            DatabaseConnectionInfo encryptedInfo = database.GetCcsOwnerConnectionDetails();
            DatabaseConnectionInfo decryptedInfo = new DatabaseConnectionInfo();
            decryptedInfo.Host = EncryptionUtil.Decrypt(encryptedInfo.Host, Resources.PublicKey, true);
            decryptedInfo.Password = EncryptionUtil.Decrypt(encryptedInfo.Password, Resources.PublicKey, true);
            decryptedInfo.Port = EncryptionUtil.Decrypt(encryptedInfo.Port, Resources.PublicKey, true);
            decryptedInfo.Schema = EncryptionUtil.Decrypt(encryptedInfo.Schema, Resources.PublicKey, true);
            decryptedInfo.Service = EncryptionUtil.Decrypt(encryptedInfo.Service, Resources.PublicKey, true);
            decryptedInfo.UserId = EncryptionUtil.Decrypt(encryptedInfo.UserId, Resources.PublicKey, true);

            return decryptedInfo;
        }

        private DatabaseConnectionInfo GetPawnSecDatabaseConfigurationInfo(ClxApplication application, ConfigurationKey key)
        {
            string configValue = ConfigurationReader.GetInstance().GetConfigurationContents(application, key);

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(configValue);

            XmlNodeList list = xmlDocument.GetElementsByTagName("setting");

            DatabaseConnectionInfo info = new DatabaseConnectionInfo();

            foreach (XmlNode n in list)
            {
                string decryptedValue = EncryptionUtil.Decrypt(n.ChildNodes[0].InnerText, Resources.PrivateKey, true);
                
                switch (n.Attributes["name"].Value)
                {
                    case "PawnSecDBUser":
                        info.UserId = decryptedValue;
                        break;
                    case "PawnSecDBPassword":
                        info.Password = decryptedValue;
                        break;
                    case "PawnSecDBSchema":
                        info.Schema = decryptedValue;
                        break;
                    case "PawnSecDBPort":
                        info.Port = decryptedValue;
                        break;
                    case "PawnSecDBHost":
                        info.Host = decryptedValue;
                        break;
                    case "PawnSecDBService":
                        info.Service = decryptedValue;
                        break;
                }
            }

            return info;
        }
    }
}
