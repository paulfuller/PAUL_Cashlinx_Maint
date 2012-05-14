using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Common.Libraries.Objects.Authorization;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Collection;
using Common.Libraries.Utility.String;

namespace Common.Libraries.Objects.Config
{
    public class EncryptedConfigContainer
    {
        public const string ERRMSG = "Cannot access pawn application config data";
        public const string ERRMSGSERV = ERRMSG + ": Cannot find service: ";
        public const string DBKEY = "DB";
        public const string ESBKEY = "ESB";
        public const string FTPKEY = "FTP";
        public const string ADDRESS_SERVICE = "Address Service";
        public const string PROKNOW_SERVICE = "ProKnow Service";
        public const string MDSETRANS_SERVICE = "MDSE Transfer Service";
        public const string ORACLEKEY = "ORACLE";
        public const string LDAPKEY = "LDAP";
        public const string COUCHDBKEY = "COUCHDB";
        public const string URLKEY = "URL";
        public const int LDAPENTRIESCNT = 6;
        public const string LDAPSPLITKEY = "=";
        public const string STOREMODE_NORMAL = "0";
        public const string STOREMODE_MAINT = "1";
        public const string STOREMODE_BETA = "2";

        public static readonly string[] LDAPSPLIT =
        {
            "|"
        };

        public const string LDAPPREFIX = "LDAP";
        public const string LDAPLOGINDN = LDAPPREFIX + "LoginDN";
        public const string LDAPSEARCHDN = LDAPPREFIX + "SearchDN";
        public const string LDAPUSERIDKEY = LDAPPREFIX + "UserIdKey";
        public const string LDAPUSERPWD = LDAPPREFIX + "Password";
        public const string LDAPPWDPOLICYCN = LDAPPREFIX + "PwdPolicyCN";
        public const string LDAPUSERNAME = LDAPPREFIX + "UserName";

        private readonly string _privateKey;
        private string _publicKey;
        private string _decryptKey;
        private ClientConfigVO clientConfig;
        private PawnSecVO pwnSecData;
        private string storeNumber;
        private List<PawnSecVO.StoreMachineVO> machineList;

        #region Public Methods
        /// <summary>
        /// Utilize to decrypt data stored within the pawnsec database excluding the
        /// global key - uses full key
        /// </summary>
        /// <param name="encValue"></param>
        /// <returns></returns>
        public string DecryptValue(string encValue)
        {
            if (string.IsNullOrEmpty(this._decryptKey))throw new ApplicationException(ERRMSG);
            return (StringUtilities.Decrypt(encValue, this._decryptKey, true));
        }

        /// <summary>
        /// Utilize to decrypt pawnsec schema connection config info and the pawnsec
        /// global key - uses private key
        /// </summary>
        /// <param name="encValue"></param>
        /// <returns></returns>        
        public string DecryptPublicValue(string encValue)
        {
            if (string.IsNullOrEmpty(this._publicKey))throw new ApplicationException(ERRMSG);
            return (StringUtilities.Decrypt(encValue, this._privateKey, true));
        }

        /// <summary>
        /// Utilize to encrypt data stored within the pawnsec database excluding the
        /// global key - uses full key
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public string EncryptValue(string strValue)
        {
            if (string.IsNullOrEmpty(this._decryptKey))throw new ApplicationException(ERRMSG);
            return (StringUtilities.Encrypt(strValue, this._decryptKey, true));
        }

        /// <summary>
        /// Utilize to encrypt pawnsec schema connection config info and the pawnsec
        /// global key - uses private key
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public string EncryptPublicValue(string strValue)
        {
            if (string.IsNullOrEmpty(this._publicKey))throw new ApplicationException(ERRMSG);
            return (StringUtilities.Encrypt(strValue, this._privateKey, true));
        }
        #endregion

        #region Public Accessors
        public bool Initialized
        {
            get; private set;
        }
        public bool Created
        {
            get; private set;
        }

        public ClientConfigVO ClientConfig
        {
            get
            {
                if (clientConfig == null)
                {
                    throw new ApplicationException(ERRMSG);
                }
                return (this.clientConfig);
            }
        }

        public List<PawnSecVO.StoreMachineVO> MachineList
        {
            get
            {
                if (clientConfig == null ||
                    CollectionUtilities.isEmpty(machineList))
                {
                    throw new ApplicationException(ERRMSG);
                }
                return (this.machineList);
            }
        }

        public PawnSecApplication AppType
        {
            set; get;
        }

        #endregion

        #region Public Methods
        public DatabaseServiceVO GetFTP()
        {
            if (!this.Initialized)
                return (null);
            for (var i = 0; i < this.clientConfig.DatabaseServices.Count; ++i)
            {
                var curDS = this.GetDBService(i);
                if (curDS == null)
                    continue;
                if (curDS.ServiceType.Equals(FTPKEY, StringComparison.OrdinalIgnoreCase))
                {
                    return (curDS);
                }
            }
            throw new ApplicationException(ERRMSG + ": Cannot find FTP Service");
        }

        public DatabaseServiceVO GetOracleDBService()
        {
            if (!this.Initialized) return (null);
            for (int i = 0; i < this.clientConfig.DatabaseServices.Count; ++i)
            {
                var curDS = this.GetDBService(i);
                if (curDS == null) continue;
                if (curDS.ServiceType.Equals(ORACLEKEY, StringComparison.OrdinalIgnoreCase))
                {
                    return (curDS);
                }
            }
            throw new ApplicationException(ERRMSG + ": Cannot find Oracle Service");           
        }

        public DatabaseServiceVO GetURL()
        {
            if (!this.Initialized)
                return (null);
            for (int i = 0; i < this.clientConfig.DatabaseServices.Count; ++i)
            {
                var curDS = this.GetDBService(i);
                if (curDS == null)
                    continue;
                if (curDS.ServiceType.Equals(URLKEY, StringComparison.OrdinalIgnoreCase))
                {
                    return (curDS);
                }
            }
            throw new ApplicationException(ERRMSG + ": Cannot find URL for PDA");           
        }

        public DatabaseServiceVO GetLDAPService(
            out string loginDN, 
            out string searchDN,
            out string userIdKey,
            out string userPwd,
            out string pwdPolicy)
        {
            loginDN = string.Empty;
            searchDN = string.Empty;
            userIdKey = string.Empty;
            userPwd = string.Empty;
            pwdPolicy = string.Empty;
            //userName = string.Empty;
            if (!this.Initialized) return (null);

            for (int i = 0; i < this.clientConfig.DatabaseServices.Count; ++i)
            {
                var curDS = this.GetDBService(i);
                if (curDS == null) continue;
                if (curDS.ServiceType.Equals(LDAPKEY, StringComparison.OrdinalIgnoreCase))
                {
                    string auxInfo = this.DecryptValue(curDS.AuxInfo);
                    string[] auxParams = auxInfo.Split(LDAPSPLIT, StringSplitOptions.None);
                    if (CollectionUtilities.isNotEmpty(auxParams) && 
                        auxParams.Length == LDAPENTRIESCNT)
                    {
                        for (int j = 0; j < auxParams.Length; ++j)
                        {
                            string curEntry = auxParams[j];
                            int eqIdx = curEntry.IndexOf(LDAPSPLITKEY, StringComparison.Ordinal);
                            if (eqIdx <= -1)
                            {
                                continue;
                            }
                            var subValue = curEntry.Substring(eqIdx+1);
                            if (curEntry.StartsWith(LDAPLOGINDN))
                            {
                                loginDN = subValue;
                            }
                            else if (curEntry.StartsWith(LDAPSEARCHDN))
                            {
                                searchDN = subValue;
                            }
                            else if (curEntry.StartsWith(LDAPUSERIDKEY))
                            {
                                userIdKey = subValue;
                            }
                            else if (curEntry.StartsWith(LDAPUSERPWD))
                            {
                                userPwd = subValue;
                            }
                            else if (curEntry.StartsWith(LDAPPWDPOLICYCN))
                            {
                                pwdPolicy = subValue;
                            }
                            else if (curEntry.StartsWith(LDAPUSERNAME))
                            {
                                //userName = subValue;
                            }
                        }
                    }
                    return (curDS);
                }
            }
            throw new ApplicationException(ERRMSG + ": Cannot find LDAP service");
        }

        public static string ComputeUnEncryptedAuxInfo(            
            string loginDN,
            string searchDN,
            string userIdKey,
            string userPwd,
            string pwdPolicy)
        {

            //Construct aux info string
            var auxInfoStr =
                string.Format("{0}{1}{2}{3}{4}{5}{2}{3}{6}{7}{2}{3}{8}{9}{2}{3}{10}{11}{2}{3}{12} ", 
                LDAPLOGINDN, loginDN, 
                LDAPSPLITKEY, LDAPSPLIT[0], 
                LDAPSEARCHDN, searchDN, 
                LDAPUSERIDKEY, userIdKey, 
                LDAPUSERPWD, userPwd, 
                LDAPPWDPOLICYCN, pwdPolicy, 
                LDAPUSERNAME);

            return (auxInfoStr);            
        }

        public bool IsLDAPExistent(string hostname, string port)
        {
            if (!this.Initialized) return (false);

            for (int i = 0; i < this.clientConfig.DatabaseServices.Count; ++i)
            {
                var curDS = this.GetDBService(i);
                if (curDS == null) continue;
                if (curDS.ServiceType.Equals(LDAPKEY, StringComparison.OrdinalIgnoreCase))
                {
                    string cHost = DecryptValue(curDS.Server);
                    string cPort = DecryptValue(curDS.Port);

                    if (!string.IsNullOrEmpty(cHost) && !string.IsNullOrEmpty(cPort) &&
                        !string.IsNullOrEmpty(hostname) && !string.IsNullOrEmpty(port))
                    {
                        return (string.Equals(cHost, hostname, StringComparison.OrdinalIgnoreCase) &&
                                string.Equals(cPort, port, StringComparison.OrdinalIgnoreCase));
                    }
                    
                }
            }

            return (false);
        }

        public bool SetLDAPService(
            ref DatabaseServiceVO ldap,
            string loginDN,
            string searchDN,
            string userIdKey,
            string userPwd,
            string pwdPolicy,
            bool encryptServerPort)
        {
            if (!this.Initialized)
                return (false);
            
            if (ldap == null || string.IsNullOrEmpty(ldap.ServiceType) ||
                !ldap.ServiceType.Equals(LDAPKEY))
            {
                return (false);
            }

            if (this.clientConfig.DatabaseServices == null)
            {
                this.clientConfig.DatabaseServices = new Dictionary<string, DatabaseServiceVO>(3);
            }

            //If an LDAP service exists, fetch it first
            var exists = false;
            var curLdapVo = this.clientConfig.DatabaseServices.Values.First(
                    x => x.ServiceType.Equals(LDAPKEY));
            if (curLdapVo != null) exists = true;

            if (!exists)
            {
                curLdapVo = ldap;
                curLdapVo.Name = this.EncryptValue(ldap.Name);
                if (encryptServerPort)
                {
                    curLdapVo.Port = this.EncryptValue(ldap.Port);
                    curLdapVo.Server = this.EncryptValue(ldap.Server);
                }
                else
                {
                    curLdapVo.Port = ldap.Port;
                    curLdapVo.Server = ldap.Server;
                }
            }
            else
            {
                curLdapVo.Name = (!string.IsNullOrEmpty(ldap.Name)) ? ldap.Name : curLdapVo.Name;
                if (encryptServerPort)
                {
                    curLdapVo.Port = this.EncryptValue(ldap.Port);
                    curLdapVo.Server = this.EncryptValue(ldap.Server);
                }
                else
                {
                    curLdapVo.Port = ldap.Port;
                    curLdapVo.Server = ldap.Server;
                }
            }

            //Construct aux info string
            var auxInfoStr = 
                ComputeUnEncryptedAuxInfo(
                    loginDN, searchDN, userIdKey, userPwd, pwdPolicy);

            //Encrypt the aux info string and set it
            curLdapVo.AuxInfo = this.EncryptValue(auxInfoStr);

            //Set the ref field again
            ldap = curLdapVo;

            //If the entry did not exist previously, add it to the list
            if (!exists)
            {
                var servCnt = this.clientConfig.DatabaseServices.Count;
                this.clientConfig.DatabaseServices.Add(DBKEY + servCnt, ldap);
            }

            return (true);
        }


        public EsbServiceVO GetAddressESBService()
        {
            if (!this.Initialized) return (null);
            for (var i = 0; i < this.clientConfig.EsbServices.Count; ++i)
            {
                var curES = this.GetESBService(i);
                if (curES == null) continue;
                var decName = this.DecryptValue(curES.Name);
                if (decName.Equals(ADDRESS_SERVICE, StringComparison.OrdinalIgnoreCase))
                {
                    return (curES);
                }
            }
            throw new ApplicationException(ERRMSGSERV + ADDRESS_SERVICE);
        }

        public EsbServiceVO GetProKnowESBService()
        {
            if (!this.Initialized) return (null);
            for (var i = 0; i < this.clientConfig.EsbServices.Count; ++i)
            {
                var curES = this.GetESBService(i);
                if (curES == null) continue;
                var decName = this.DecryptValue(curES.Name);
                if (decName.Equals(PROKNOW_SERVICE, StringComparison.OrdinalIgnoreCase))
                {
                    return (curES);
                }
            }
            throw new ApplicationException(ERRMSGSERV + PROKNOW_SERVICE);
        }

        public EsbServiceVO GetMDSETransferService()
        {
            if (!this.Initialized) return (null);
            for (var i = 0; i < this.clientConfig.EsbServices.Count; ++i)
            {
                var curES = this.GetESBService(i);
                if (curES == null) continue;
                var decName = this.DecryptValue(curES.Name);
                if (decName.Equals(MDSETRANS_SERVICE, StringComparison.OrdinalIgnoreCase))
                {
                    return (curES);
                }
            }
            throw new ApplicationException(ERRMSGSERV + MDSETRANS_SERVICE);
        }

        public DatabaseServiceVO GetCouchDBService()
        {
            if (!this.Initialized) return (null);
            for (var i = 0; i < this.clientConfig.DatabaseServices.Count; ++i)
            {
                DatabaseServiceVO curDS = this.GetDBService(i);
                if (curDS == null) continue;
                if (curDS.ServiceType.Equals(COUCHDBKEY, StringComparison.OrdinalIgnoreCase))
                {
                    return (curDS);
                }
            }

            throw new ApplicationException(ERRMSGSERV + COUCHDBKEY);
        }
        
        #endregion

        #region Private Methods
        private DatabaseServiceVO GetDBService(int sIdx)
        {
            if (!this.Initialized) return (null);
            if (sIdx < 0 || sIdx >= this.clientConfig.DatabaseServices.Count)
            {
                return (null);
            }
            var dbKey = DBKEY + sIdx;
            if (CollectionUtilities.isNotEmptyContainsKey(this.clientConfig.DatabaseServices, dbKey))
            {
                return (this.clientConfig.DatabaseServices[dbKey]);
            }
            return (null);
        }

        private EsbServiceVO GetESBService(int sIdx)
        {
            if (!this.Initialized) return (null);
            if (sIdx < 0 || sIdx >= this.clientConfig.EsbServices.Count)
            {
                return (null);
            }
            var esbKey = ESBKEY + sIdx;
            if (CollectionUtilities.isNotEmptyContainsKey(this.clientConfig.EsbServices, esbKey))
            {
                return (this.clientConfig.EsbServices[esbKey]);
            }
            return (null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientData"></param>
        /// <param name="encPubKey"></param>
        /// <returns></returns>
        private bool getEncryptedPublicKey(DataTable clientData, out string encPubKey)
        {
            encPubKey = string.Empty;
            if (clientData != null && clientData.Rows.Count > 0)
            {
                //Get the first data row
                var dRow = clientData.Rows[0];

                //Get the public key from this row
                encPubKey = Utilities.GetStringValue(dRow["datapublickey"], string.Empty);

                //Verify that the key is valid
                if (!string.IsNullOrEmpty(encPubKey))
                {
                    return (true);
                }
            }
            return (false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientData"></param>
        /// <returns></returns>
        private bool acquirePublicKey(DataTable clientData)
        {
            this._publicKey = string.Empty;
            string encryptedPublicKey;
            if (this.getEncryptedPublicKey(clientData, out encryptedPublicKey))
            {
                //Public key can only be decrypted by the private key
                this._publicKey = StringUtilities.Decrypt(
                    encryptedPublicKey, this._privateKey, true);
            }
            return (!string.IsNullOrEmpty(this._publicKey));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientData"></param>
        /// <returns></returns>
        private bool initializeClientData(DataTable clientData)
        {
            if (clientData == null || clientData.Rows == null || clientData.Rows.Count <= 0)
            {
                return (false);
            }

            //Get the first data row
            var dataRow = clientData.Rows[0];

            //Get app version
            var appVer = Utilities.GetStringValue(dataRow["appversion"], string.Empty);

            //Create and set client config
            this.clientConfig = new ClientConfigVO(appVer, this._publicKey);
            this.clientConfig.IpAddress = Utilities.GetStringValue(
                dataRow["ipaddress"], string.Empty);
            this.clientConfig.MacAddress = Utilities.GetStringValue(
                dataRow["macaddress"], string.Empty);
            this.clientConfig.MachineName = Utilities.GetStringValue(
                dataRow["machinename"], string.Empty);
            this.clientConfig.StoreSite.Alias = Utilities.GetStringValue(
                dataRow["aliascode"], string.Empty);
            this.clientConfig.StoreSite.CompanyNumber = Utilities.GetStringValue(
                dataRow["companynumber"], string.Empty);
            this.clientConfig.StoreSite.State = Utilities.GetStringValue(
                dataRow["state"], string.Empty);
            this.clientConfig.StoreSite.StoreNumber = Utilities.GetStringValue(
                dataRow["storenumber"], string.Empty);
            this.clientConfig.StoreConfiguration.CompanyName = Utilities.GetStringValue(
                dataRow["companyname"], string.Empty);
            var adobePathOvr = Utilities.GetStringValue(
                dataRow["adobeoverride"], string.Empty);
            var ghostPathOvr = Utilities.GetStringValue(
                dataRow["ghostscriptoverride"], string.Empty);

            //Get store config values
            var storeConfigVO = this.clientConfig.StoreConfiguration;
            storeConfigVO.Id = Utilities.GetULongValue(
                dataRow["id"], 0L);
            storeConfigVO.MetalsFile = Utilities.GetStringValue(
                dataRow["metalsfile"], string.Empty);
            storeConfigVO.StonesFile = Utilities.GetStringValue(
                dataRow["stonesfile"], string.Empty);
            storeConfigVO.FetchSizeMultiplier = Utilities.GetULongValue(
                dataRow["fetchszmx"], 0L);
            storeConfigVO.TimeZone = Utilities.GetStringValue(
                dataRow["timezone"], string.Empty);
            storeConfigVO.MillisecondOffset = Utilities.GetLongValue(
                dataRow["millisecondoffset"], 0L);
            storeConfigVO.SecondOffset = Utilities.GetLongValue(
                dataRow["secondoffset"], 0L);
            storeConfigVO.MinuteOffset = Utilities.GetLongValue(
                dataRow["minuteoffset"], 0L);
            storeConfigVO.HourOffset = Utilities.GetLongValue(
                dataRow["houroffset"], 0L);
            storeConfigVO.DayOffset = Utilities.GetLongValue(
                dataRow["dayoffset"], 0L);
            storeConfigVO.MonthOffset = Utilities.GetLongValue(
                dataRow["monthoffset"], 0L);
            storeConfigVO.YearOffset = Utilities.GetLongValue(
                dataRow["yearoffset"], 0L);
            storeConfigVO.StoreMode = Utilities.GetStringValue(
                dataRow["storemode"], "0");

            //Get global config values
            var globalConfigVO = this.clientConfig.GlobalConfiguration;
            if (string.IsNullOrEmpty(adobePathOvr))
            {
                globalConfigVO.AdobeReaderPath = Utilities.GetStringValue(
                    dataRow["adobereaderpath"], string.Empty);
            }
            else
            {
                globalConfigVO.AdobeReaderPath = adobePathOvr;
            }
            globalConfigVO.BaseLogPath = Utilities.GetStringValue(
                dataRow["baselogpath"], string.Empty);
            globalConfigVO.BaseMediaPath = Utilities.GetStringValue(
                dataRow["basemediapath"], string.Empty);
            globalConfigVO.BaseTemplatePath = Utilities.GetStringValue(
                dataRow["basetemplatepath"], string.Empty);
            if (string.IsNullOrEmpty(ghostPathOvr))
            {
                globalConfigVO.GhostScriptPath = Utilities.GetStringValue(
                    dataRow["ghostscriptpath"], string.Empty);
            }
            else
            {
                globalConfigVO.GhostScriptPath = ghostPathOvr;                
            }

            //Get store client config)
            var storeClientConfigVO = clientConfig.ClientConfiguration;
            storeClientConfigVO.LogLevel = Utilities.GetStringValue(
                dataRow["loglevel"], string.Empty);
            storeClientConfigVO.TerminalNumber = Utilities.GetIntegerValue(
                dataRow["terminalnumber"], 0);
            storeClientConfigVO.TraceLevel = Utilities.GetIntegerValue(
                dataRow["tracelevel"], 0);
            storeClientConfigVO.WorkstationId = Utilities.GetStringValue(
                dataRow["workstationid"], string.Empty);
            //Get print enabled string and set boolean accordingly
            this.clientConfig.StoreSite.TerminalId = storeClientConfigVO.WorkstationId;
            string printEnStr = Utilities.GetStringValue(dataRow["printenabled"], "0");
            storeClientConfigVO.PrintEnabled = (printEnStr.Equals("1"));

            return (true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbData"></param>
        /// <returns></returns>
        private bool initializeEsbData(DataTable dbData)
        {
            if (dbData == null || dbData.Rows == null || dbData.Rows.Count <= 0) return (false);

            int idx = 0;
            foreach (DataRow dR in dbData.Rows)
            {
                if (dR == null ||
                    dR.ItemArray.Length <= 0) continue;

                //Get critical fields first
                string dbEnStr = Utilities.GetStringValue(dR["ENABLED"], "0");
                bool dbEnabled = (dbEnStr.Equals("1"));

                //Create esb service vo object and initialize
                EsbServiceVO eVO = new EsbServiceVO(dbEnabled);
                eVO.Id = Utilities.GetULongValue(dR["ID"], 0L);
                eVO.Name = Utilities.GetStringValue(dR["NAME"], string.Empty);
                eVO.Server = Utilities.GetStringValue(dR["SERVER"], string.Empty);
                eVO.Port = Utilities.GetStringValue(dR["PORT"], string.Empty);
                eVO.Domain = Utilities.GetStringValue(dR["DOMAIN"], string.Empty);
                eVO.Uri = Utilities.GetStringValue(dR["URI"], string.Empty);
                eVO.EndPointName = Utilities.GetStringValue(dR["ENDPOINTNAME"], string.Empty);
                eVO.ClientBinding = Utilities.GetStringValue(dR["CLIENTBINDING"], string.Empty);
                eVO.HttpBinding = Utilities.GetStringValue(dR["HTTPBINDING"], string.Empty);

                //Add esb service to store client config
                string key = ESBKEY + idx;
                this.clientConfig.EsbServices.Add(key, eVO);

                //Increment index
                ++idx;
            }

            return (idx > 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbData"></param>
        /// <returns></returns>
        private bool initializeDbData(DataTable dbData)
        {            
            if (dbData == null || dbData.Rows == null || dbData.Rows.Count <= 0) return (false);

            int idx = 0;
            foreach(DataRow dR in dbData.Rows)
            {
                if (dR == null || dR.ItemArray.Length <= 0) continue;

                //Get critical fields first
                var dbEnStr = Utilities.GetStringValue(dR["ENABLED"], "0");
                var dbEnabled = (dbEnStr.Equals("1"));
                var dbUserEnc = Utilities.GetStringValue(dR["DBUSER"], string.Empty);
                var dbUserPwdEnc = Utilities.GetStringValue(dR["DBUSERPWD"], string.Empty);

                //Create database vo object and initialize
                var dVO = new DatabaseServiceVO(dbUserEnc, dbUserPwdEnc, dbEnabled);
                dVO.Id = Utilities.GetULongValue(dR["ID"], 0L);
                dVO.Name = Utilities.GetStringValue(dR["NAME"], string.Empty);
                dVO.ServiceType = Utilities.GetStringValue(dR["SERVICETYPE"], string.Empty);
                dVO.Server = Utilities.GetStringValue(dR["SERVER"], string.Empty);
                dVO.Port = Utilities.GetStringValue(dR["PORT"], string.Empty);
                dVO.Schema = Utilities.GetStringValue(dR["SCHEMA"], string.Empty);
                dVO.AuxInfo = Utilities.GetStringValue(dR["AUXINFO"], string.Empty);
                                
                //Add database service to store client config
                string key = DBKEY + idx;
                this.clientConfig.DatabaseServices.Add(key, dVO);

                //Increment index
                ++idx;
            }

            return (idx > 0);
        }

        private bool initializeMachineList(DataTable macData)
        {
            if (macData == null)
            {
                //If data table is null, there is an error
                return (false);
            }

            this.machineList = new List<PawnSecVO.StoreMachineVO>(1);
            if (macData.Rows == null || macData.Rows.Count == 0)
            {
                //Initialize an empty machine list, this is currently the only
                //machine in the shop, not an error condition
                return (true);
            }

            foreach(DataRow dR in macData.Rows)
            {
                if (dR == null || dR.ItemArray.Length <= 0)
                    continue;

                var macNamStr = Utilities.GetStringValue(dR["MACHINENAME"], string.Empty);
                var macConStr = Utilities.GetStringValue(dR["ISCONNECTED"], "0");
                var macConnected = (macConStr.Equals("1"));
                var macAllStr = Utilities.GetStringValue(dR["ISALLOWED"], "0");
                var macAllowed = (macAllStr.Equals("1"));

                var storeMacVo = new PawnSecVO.StoreMachineVO(macNamStr, macAllowed, macConnected);
                this.machineList.Add(storeMacVo);
            }

            return (true);
        }

        /// <summary>
        /// Formulate the decryption key
        /// </summary>
        /// <returns>Success of operation</returns>
        private bool formulateDecryptionKey()
        {
            if (string.IsNullOrEmpty(this._publicKey) ||
                string.IsNullOrEmpty(this._privateKey))
            {
                return (false);
            }
            //Decryption key is the concatenation of the public key
            //plus the private key
            this._decryptKey = string.Concat(this._publicKey, this._privateKey);
            return (true);
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pKey"></param>
        /// <param name="clientData"></param>
        /// <param name="databaseData"></param>
        /// <param name="esbData"></param>
        /// <param name="macData"></param>
        /// <param name="app"> </param>
        public EncryptedConfigContainer(
            string pKey,
            DataTable clientData,
            DataTable databaseData,
            DataTable esbData,
            DataTable macData,
            PawnSecApplication app)
        {
            this.Initialized = false;
            this.Created = false;
            if (clientData == null || clientData.HasErrors || clientData.Rows == null || clientData.Rows.Count <= 0 ||
                databaseData == null || databaseData.HasErrors || databaseData.Rows == null || databaseData.Rows.Count <= 0 ||
                esbData == null || esbData.HasErrors || esbData.Rows == null || esbData.Rows.Count <= 0 ||
                macData == null || macData.HasErrors || string.IsNullOrEmpty(pKey)) return;
            this._privateKey = pKey;

            //Acquire the public key from the client data table
            if (!this.acquirePublicKey(clientData))
            {
                throw new ApplicationException("Cannot retrieve public key from client data");
            }
            //Formulate the decrypt key
            if (!this.formulateDecryptionKey())
            {
                throw new ApplicationException("Cannot create decryption key");
            }

            //Set creation stage success
            this.Created = true;

            //Initialize the client data based on the client data table
            if (!this.initializeClientData(clientData))
            {
                throw new ApplicationException("Cannot initialize client data retrieved from pawnsec");
            }
            //Initialize the database entries from the database data table
            if (!this.initializeDbData(databaseData))
            {
                throw new ApplicationException("Cannot initialize database data retrieved from pawnsec");
            }
            //Initialize the ESB entries from the ESB data table
            if (!this.initializeEsbData(esbData))
            {
                throw new ApplicationException("Cannot initialize esb data retrieved from pawnsec");
            }
            //Initialize the machine list from the machine data table
            if (!this.initializeMachineList(macData))
            {
                throw new ApplicationException(
                        "Cannot initialize machine list data retrieved from pawnsec");
            }
            this.Initialized = true;
            this.AppType = app;
        }

        /// <summary>
        /// Populate client information after object initialization
        /// </summary>
        /// <returns></returns>
        private bool populateClientData()
        {
            this.clientConfig = new ClientConfigVO(this.pwnSecData.GlobalConfiguration.Version, 
                this.pwnSecData.GlobalConfiguration.DataPublicKey);
            PawnSecVO.ClientPawnSecMachineVO machine = null;
            PawnSecVO.ClientStoreMapVO stoCliMap;
            if (CollectionUtilities.isNotEmpty(this.pwnSecData.ClientMachines) &&
                CollectionUtilities.isNotEmpty(this.pwnSecData.ClientStoreMapList))
            {
                stoCliMap = 
                    this.pwnSecData.ClientStoreMapList.Find(x => x.StoreNumber.Equals(this.storeNumber));
                if (stoCliMap != null)
                {
                    machine = 
                        this.pwnSecData.ClientMachines.Find(x => x.Machine.ClientId.Equals(stoCliMap.ClientRegistryId));
                    if (machine != null)
                    {
                        this.clientConfig.IpAddress = machine.Machine.IPAddress;
                        this.clientConfig.MacAddress = machine.Machine.MACAddress;
                        this.clientConfig.MachineName = machine.Machine.MachineName;
                    }
                }
            }
            else
            {
                return (false);
            }

            if (stoCliMap != null && CollectionUtilities.isNotEmpty(this.pwnSecData.Stores))
            {
                var pSecStore = this.pwnSecData.Stores.Find(x => x.StoreConfiguration.Id == stoCliMap.StoreConfigId);
                if (pSecStore != null)
                {
                    this.clientConfig.StoreConfiguration = pSecStore.StoreConfiguration;
                    this.clientConfig.StoreSite = pSecStore.StoreSite;
                    this.clientConfig.AppVersion = pSecStore.AppVersion;
                    if (machine != null)
                    {
                        //Get print enabled string and set boolean accordingly
                        this.clientConfig.StoreSite.TerminalId =
                                machine.StoreMachine.WorkstationId;
                        this.clientConfig.ClientConfiguration.PrintEnabled =
                                machine.StoreMachine.PrintEnabled;
                        this.clientConfig.ClientConfiguration.CPNHSEnabled =
                                machine.StoreMachine.CPNHSEnabled;
                    }
                }
            }
            else
            {
                return (false);
            }
            this.clientConfig.GlobalConfiguration = this.pwnSecData.GlobalConfiguration;
            return (true);
        }

        /// <summary>
        /// Populate database information after object initialization
        /// </summary>
        /// <returns></returns>
        private bool populateDbData(bool onlyDbData = false)
        {
            if (this.pwnSecData == null) return(false);
            if (CollectionUtilities.isNotEmpty(this.pwnSecData.DatabaseServiceMapList) &&
                CollectionUtilities.isNotEmpty(this.pwnSecData.DatabaseServiceList))
            {
                List<DatabaseServiceVO> dbList = null;
                if (this.pwnSecData.MapsValid == false)
                {
                    List<PawnSecVO.DatabaseServiceStoreMapVO> dbStoMapEntryList;
                    if (!onlyDbData)
                    {
                        dbStoMapEntryList =
                            this.pwnSecData.DatabaseServiceMapList.FindAll(
                                x => x.StoreConfigId.Equals(this.clientConfig.StoreConfiguration.Id));
                    }
                    else
                    {
                        dbStoMapEntryList =
                            this.pwnSecData.DatabaseServiceMapList;
                    }
                    if (CollectionUtilities.isNotEmpty(dbStoMapEntryList))
                    {
                        var dbListE = from dbServ in this.pwnSecData.DatabaseServiceList
                                      join dbServMap in dbStoMapEntryList
                                        on dbServ.Id equals dbServMap.DatabaseServiceId
                                      select dbServ;
                        dbList = new List<DatabaseServiceVO>(dbListE);
                    }
                }
                else
                {
                    KeyValuePair<PawnSecVO.PawnSecStoreVO, List<DatabaseServiceVO>> pSecStoreDbListKeyPair;
                    if (!onlyDbData)
                    {
                        pSecStoreDbListKeyPair =
                            this.pwnSecData.StoreToDatabaseServiceMap.First(
                                x => x.Key.StoreSite.StoreNumber.Equals(this.storeNumber));
                    }
                    else
                    {
                        pSecStoreDbListKeyPair =
                            this.pwnSecData.StoreToDatabaseServiceMap.First();
                    }

                    dbList = pSecStoreDbListKeyPair.Value;
                }

                if (dbList != null && CollectionUtilities.isNotEmpty(dbList))
                {
                    if (onlyDbData && this.clientConfig == null)
                    {
                        this.clientConfig = new ClientConfigVO("1", this._publicKey);
                    }
                    var idx = 0;
                    foreach(var curDb in dbList)
                    {
                        if (curDb == null) continue;
                        //Add database service to store client config
                        var key = DBKEY + idx;
                        this.clientConfig.DatabaseServices.Add(key, curDb);
                        ++idx;
                    }
                    return (true);
                }
            }
            return (false);
        }

        /// <summary>
        /// Populate esb information after object initialization
        /// </summary>
        /// <returns></returns>
        private bool populateEsbData()
        {
            if (this.pwnSecData == null)
                return (false);
            if (CollectionUtilities.isNotEmpty(this.pwnSecData.ESBServiceMapList) &&
                CollectionUtilities.isNotEmpty(this.pwnSecData.ESBServiceList))
            {
                List<EsbServiceVO> esbList = null;
                if (this.pwnSecData.MapsValid == false)
                {
                    var esbStoMapEntryList =
                            this.pwnSecData.ESBServiceMapList.FindAll(
                                    x => x.StoreConfigId.Equals(this.clientConfig.StoreConfiguration.Id));
                    if (CollectionUtilities.isNotEmpty(esbStoMapEntryList))
                    {
                        var esbListE = from esbServ in this.pwnSecData.ESBServiceList
                                      join esbServMap in esbStoMapEntryList
                                        on esbServ.Id equals esbServMap.ESBServiceId
                                      select esbServ;
                        esbList = new List<EsbServiceVO>(esbListE);
                    }
                }
                else
                {
                    var pSecStoreDbListKeyPair =
                            this.pwnSecData.StoreToEsbServiceMap.First(
                                    x => x.Key.StoreSite.StoreNumber.Equals(this.storeNumber));

                    esbList = pSecStoreDbListKeyPair.Value;
                }

                if (esbList != null && CollectionUtilities.isNotEmpty(esbList))
                {
                    var idx = 0;
                    foreach (var curEsb in esbList)
                    {
                        if (curEsb == null)
                            continue;
                        //Add esb service to store client config
                        var key = ESBKEY + idx;
                        this.clientConfig.EsbServices.Add(key, curEsb);
                        ++idx;
                    }
                    return (true);
                }
            }
            return (false);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool populateMachineData()
        {
            if (this.pwnSecData == null)
                return (false);
            if (CollectionUtilities.isNotEmpty(this.pwnSecData.ClientMachines))
            {
                if (this.pwnSecData.MapsValid)
                {
                                        
                }
                else
                {
                    foreach (var cMac in this.pwnSecData.ClientMachines)
                    {

                    }
                }
            }
            return (false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pKey"></param>
        /// <param name="pubKey"></param>
        /// <param name="stoNum"></param>
        /// <param name="pSecData"></param>
        /// <param name="pApp"> </param>
        /// <param name="onlyDbData"> </param>
        public EncryptedConfigContainer(
            string pKey,
            string pubKey,
            string stoNum,
            PawnSecVO pSecData,
            PawnSecApplication pApp = PawnSecApplication.None,
            bool onlyDbData = false)
        {
            this.Initialized = false;
            this.Created = false;
            if (string.IsNullOrEmpty(pKey) || string.IsNullOrEmpty(pubKey)) return;
            this._privateKey = pKey;
            this._publicKey = pubKey;
            if (!this.formulateDecryptionKey())
            {
                throw new ApplicationException("Cannot create decryption key");                
            }
            this.Created = true;
            this.Initialized = this.Refresh(stoNum, pSecData, onlyDbData);
            if (this.Initialized)
            {
                this.AppType = pApp;
            }
        }

        public bool Refresh(string stoNum, PawnSecVO pSecData, bool onlyDbData = false)
        {
            if (pSecData == null) return(false);
            if (!onlyDbData && string.IsNullOrEmpty(stoNum)) return(false);
            this.pwnSecData = pSecData;
            if (!onlyDbData)this.storeNumber = stoNum;
            if (!onlyDbData && !this.populateClientData()) return(false);
            if (!this.populateDbData(onlyDbData)) return (false);
            if (!onlyDbData && !this.populateEsbData()) return (false);
            this.Initialized = true;
            return (true);
        }
    }
}
