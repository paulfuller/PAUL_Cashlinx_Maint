using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Libraries.Objects.Authorization;
using Common.Libraries.Utility.Collection;
using Common.Libraries.Utility.Logger;

namespace Common.Libraries.Objects.Config
{
    public class PawnSecVO
    {
        /// <summary>
        /// 
        /// </summary>
        public class ClientStoreMapVO
        {
            public ulong Id
            {
                set; get;
            }

            public ulong ClientRegistryId
            {
                set; get;
            }

            public ulong StoreSiteId
            {
                set; get;
            }

            public ulong StoreClientConfigId
            {
                set; get;
            }

            public ulong StoreConfigId
            { 
                set; get;
            }

            public string StoreNumber
            {
                set; get;
            }

            public ClientStoreMapVO()
            {
                Id = 0L;
                ClientRegistryId = 0L;
                StoreSiteId = 0L;
                StoreClientConfigId = 0L;
                StoreConfigId = 0L;
                StoreNumber = string.Empty;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public class ESBServiceStoreMapVO
        {
            public ulong Id
            {
                set; get;
            }

            public ulong ESBServiceId
            {
                set; get;
            }

            public ulong StoreConfigId
            {
                set; get;
            }

            public ESBServiceStoreMapVO()
            {
                Id = 0L;
                ESBServiceId = 0L;
                StoreConfigId = 0L;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public class DatabaseServiceStoreMapVO
        {
            public ulong Id
            {
                set; get;
            }

            public ulong DatabaseServiceId
            {
                set; get;
            }

            public ulong StoreConfigId
            {
                set; get;
            }

            public DatabaseServiceStoreMapVO()
            {
                this.Id = 0L;
                this.DatabaseServiceId = 0L;
                this.StoreConfigId = 0L;
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public class ClientPawnSecMachineVO
        {
            public PawnSecMachineVO Machine
            {
                private set; get;
            }

            public StoreClientConfigVO StoreMachine
            {
                private set; get;
            }

            public ClientPawnSecMachineVO()
            {
                Machine = new PawnSecMachineVO();
                StoreMachine = new StoreClientConfigVO();
            }
        }

        public class StoreMachineVO
        {
            public string MachineName
            {
                private set; get;
            }

            public bool IsAllowed
            {
                private set; get;
            }

            public bool IsConnected
            {
                set; get;
            }

            public StoreMachineVO(string mName, bool iAllow, bool iConn)
            {
                this.MachineName = mName;
                this.IsAllowed = iAllow;
                this.IsConnected = iConn;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public class PawnSecStoreVO
        {
            public StoreAppVersionVO AppVersion
            {
                set; get;
            }

            public StoreConfigVO StoreConfiguration
            {
                set; get;
            }

            public SiteId StoreSite
            {
                set; get;
            }

            public ulong StoreSiteId
            {
                set; get;
            }

            public PawnSecStoreVO(string appVer, string appVerId)
            {
                AppVersion = new StoreAppVersionVO(appVer, appVerId);
                StoreConfiguration = new StoreConfigVO();
                StoreSite = null;
                StoreSiteId = 0L;
            }

            public PawnSecStoreVO()
            {
                AppVersion = new StoreAppVersionVO();
            }

        }

        public class PawnSecNextIdVO
        {
            public enum SELECTOR
            {
                CLIREG_ID,
                CLISTOMAP_ID,
                DATSERV_ID,
                DATSERVMAP_ID,
                ESBSERV_ID,
                ESBSERVMAP_ID,
                GLOBCFG_ID,
                STOAPPVER_ID,
                STOCLICFG_ID,
                STOCFG_ID,
                STOSITINF_ID
            }

            private ulong clientRegistryId;
            private ulong clientStoreMapId;
            private ulong databaseServiceId;
            private ulong databaseServiceStoreMapId;
            private ulong esbServiceId;
            private ulong esbServiceStoreMapId;
            private ulong globalConfigId;
            private ulong storeAppVersionId;
            private ulong storeClientConfigId;
            private ulong storeConfigId;
            private ulong storeSiteInfoId;

            /// <summary>
            /// Sets the current maximums of each index value.
            /// These values have already been used and will
            /// be incremented prior to output in the main get function
            /// </summary>
            /// <param name="cRId"></param>
            /// <param name="cSMId"></param>
            /// <param name="dSId"></param>
            /// <param name="dSSMId"></param>
            /// <param name="eSId"></param>
            /// <param name="eSSMId"></param>
            /// <param name="gCId"></param>
            /// <param name="sAVId"></param>
            /// <param name="sCCId"></param>
            /// <param name="sCId"></param>
            /// <param name="sSIId"></param>
            public PawnSecNextIdVO(
                ulong cRId,
                ulong cSMId,
                ulong dSId,
                ulong dSSMId,
                ulong eSId,
                ulong eSSMId,
                ulong gCId,
                ulong sAVId,
                ulong sCCId,
                ulong sCId,
                ulong sSIId)
            {
                this.clientRegistryId = cRId;
                this.clientStoreMapId = cSMId;
                this.databaseServiceId = dSId;
                this.databaseServiceStoreMapId = dSSMId;
                this.esbServiceId = eSId;
                this.esbServiceStoreMapId = eSSMId;
                this.globalConfigId = gCId;
                this.storeAppVersionId = sAVId;
                this.storeClientConfigId = sCCId;
                this.storeConfigId = sCId;
                this.storeSiteInfoId = sSIId;
                this.logCurrentValues("Initial Value Set", true, default(SELECTOR));
            }

            private void logCurrentValues(string s, bool all, SELECTOR e)
            {
                var flog = FileLogger.Instance;
                if (flog != null)
                {
                    if (all)
                    {
                        flog.logMessage(LogLevel.DEBUG,
                                        "PawnSecNextIdVO",
                                        (s ?? "(null())") + 
                                        "{0}" + 
                                        "cliRegId={1}" + Environment.NewLine +
                                        "cliStoMap={2}" + Environment.NewLine +
                                        "datServId={3}" + Environment.NewLine +
                                        "datServStoMapId={4}" + Environment.NewLine +
                                        "esbServId={5}" + Environment.NewLine +
                                        "esbServStoMapId={6}" + Environment.NewLine +
                                        "globCfgId={7}" + Environment.NewLine +
                                        "stoAppVerId={8}" + Environment.NewLine +
                                        "stoCliCfgId={9}" + Environment.NewLine +
                                        "stoCfgId={10}" + Environment.NewLine +
                                        "stoSiteInfoId={11}",
                                        Environment.NewLine,
                                        this.clientRegistryId,
                                        this.clientStoreMapId,
                                        this.databaseServiceId,
                                        this.databaseServiceStoreMapId,
                                        this.esbServiceId,
                                        this.esbServiceStoreMapId,
                                        this.globalConfigId,
                                        this.storeAppVersionId,
                                        this.storeClientConfigId,
                                        this.storeConfigId,
                                        this.storeSiteInfoId);
                    }
                    else
                    {
                        var sB = new StringBuilder(64);
                        sB.Append((s ?? "null") + e);
                        switch(e)
                        {
                            case SELECTOR.CLIREG_ID:
                                sB.Append(" ClientRegistryId   = " + this.clientRegistryId);
                                break;
                            case SELECTOR.CLISTOMAP_ID:
                                sB.Append(" ClientStoreMapId   = " + this.clientStoreMapId);
                                break;
                            case SELECTOR.DATSERV_ID:
                                sB.Append(" DatabaseServiceId  = " + this.databaseServiceId);
                                break;
                            case SELECTOR.DATSERVMAP_ID:
                                sB.Append(" DatabaseServiceMapId = " + this.databaseServiceStoreMapId);
                                break;
                            case SELECTOR.ESBSERV_ID:
                                sB.Append(" ESBServiceId = " + this.esbServiceId);
                                break;
                            case SELECTOR.ESBSERVMAP_ID:
                                sB.Append(" ESBServiceMapId = " + this.esbServiceStoreMapId);
                                break;
                            case SELECTOR.GLOBCFG_ID:
                                sB.Append(" GlobalConfigid = " + this.globalConfigId);
                                break;
                            case SELECTOR.STOAPPVER_ID:
                                sB.Append(" StoreAppVerId = " + this.storeAppVersionId);
                                break;
                            case SELECTOR.STOCLICFG_ID:
                                sB.Append(" StoreClientConfigId = " + this.storeClientConfigId);
                                break;
                            case SELECTOR.STOCFG_ID:
                                sB.Append(" StoreConfigId = " + this.storeConfigId);
                                break;
                            case SELECTOR.STOSITINF_ID:
                                sB.Append(" StoreSiteInfoId = " + this.storeSiteInfoId);
                                break;
                        }
                        sB.AppendLine();
                        flog.logMessage(LogLevel.DEBUG, "PawnNextIdVO", sB.ToString());
                    }
                }
            }

            /// <summary>
            /// Retrieves the next id values based on the SELECTOR
            /// Pushes to the output variable and then
            /// increments the specified id
            /// </summary>
            /// <param name="sel"></param>
            /// <param name="nextIdVal"></param>
            /// <param name="stu"></param>
            public void GetNextIds(
                SELECTOR sel,
                ref ulong nextIdVal, out string stu)
            {
                stu = string.Empty;
                ulong valToPull = 1;
                this.logCurrentValues("- Before ", false, sel);
                switch (sel)
                {
                    case SELECTOR.CLIREG_ID:
                        valToPull = this.clientRegistryId;
                        break;
                    case SELECTOR.CLISTOMAP_ID:
                        valToPull = this.clientStoreMapId;
                        break;
                    case SELECTOR.DATSERV_ID:
                        valToPull = this.databaseServiceId;
                        break;
                    case SELECTOR.DATSERVMAP_ID:
                        valToPull = this.databaseServiceStoreMapId;
                        break;
                    case SELECTOR.ESBSERV_ID:
                        valToPull = this.esbServiceId;
                        break;
                    case SELECTOR.ESBSERVMAP_ID:
                        valToPull = this.esbServiceStoreMapId;
                        break;
                    case SELECTOR.GLOBCFG_ID:
                        valToPull = this.globalConfigId;
                        break;
                    case SELECTOR.STOAPPVER_ID:
                        valToPull = this.storeAppVersionId;
                        break;
                    case SELECTOR.STOCLICFG_ID:
                        valToPull = this.storeClientConfigId;
                        break;
                    case SELECTOR.STOCFG_ID:
                        valToPull = this.storeConfigId;
                        break;
                    case SELECTOR.STOSITINF_ID:
                        valToPull = this.storeSiteInfoId;
                        break;
                    default:
                        valToPull = 1;
                        break;
                }

                //Set the outgoing variable
                nextIdVal = valToPull;

                //Increment values
                this.incrementValues(sel);
                this.logCurrentValues("- After ", false, sel);
            }


            private void incrementValues(SELECTOR sel)
            {
                switch (sel)
                {
                    case SELECTOR.CLIREG_ID:
                        this.clientRegistryId += 1;
                        break;
                    case SELECTOR.CLISTOMAP_ID:
                        this.clientStoreMapId += 1;
                        break;
                    case SELECTOR.DATSERV_ID:
                        this.databaseServiceId += 1;
                        break;
                    case SELECTOR.DATSERVMAP_ID:
                        this.databaseServiceStoreMapId += 1;
                        break;
                    case SELECTOR.ESBSERV_ID:
                        this.esbServiceId += 1;
                        break;
                    case SELECTOR.ESBSERVMAP_ID:
                        this.esbServiceStoreMapId += 1;
                        break;
                    case SELECTOR.GLOBCFG_ID:
                        this.globalConfigId += 1;
                        break;
                    case SELECTOR.STOAPPVER_ID:
                        this.storeAppVersionId += 1;
                        break;
                    case SELECTOR.STOCLICFG_ID:
                        this.storeClientConfigId += 1;
                        break;
                    case SELECTOR.STOCFG_ID:
                        this.storeConfigId += 1;
                        break;
                    case SELECTOR.STOSITINF_ID:
                        this.storeSiteInfoId += 1;
                        break;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public GlobalConfigVO GlobalConfiguration
        {
            private set; get;
        }

        public List<ClientPawnSecMachineVO> ClientMachines
        {
            private set; get;
        }

        public List<PawnSecStoreVO> Stores
        {
            private set; get;
        }

        public List<ClientStoreMapVO> ClientStoreMapList
        {
            private set; get;
        }

        public List<ESBServiceStoreMapVO> ESBServiceMapList
        {
            private set; get;
        }

        public List<EsbServiceVO> ESBServiceList
        {
            private set; get;
        }

        public List<DatabaseServiceVO> DatabaseServiceList
        {
            private set; get;
        }

        public List<DatabaseServiceStoreMapVO> DatabaseServiceMapList
        {
            private set; get;
        }

        public Dictionary<ClientPawnSecMachineVO, PawnSecStoreVO> ClientToStoreMap        
        { 
            private set; get;
        }
    
        public Dictionary<PawnSecStoreVO, List<DatabaseServiceVO>> StoreToDatabaseServiceMap
        {
            private set; get;
        }

        public Dictionary<PawnSecStoreVO, List<EsbServiceVO>> StoreToEsbServiceMap
        {
            private set; get;
        }

        public PawnSecNextIdVO NextIdSet
        {
            private set; get;
        }

        public List<StoreMachineVO> MachineList
        {
            private set; get;
        }

        public string StoreNumber
        {
            set; get;
        }

        public bool MapsValid
        {
            private set; get;
        }

        public PawnSecVO(string publicKey, string storeNumber)
        {
            this.StoreNumber = storeNumber;
            this.GlobalConfiguration = new GlobalConfigVO(publicKey);
            this.ClientMachines = new List<ClientPawnSecMachineVO>(8);
            this.ClientStoreMapList = new List<ClientStoreMapVO>(8);
            this.ESBServiceMapList = new List<ESBServiceStoreMapVO>(16);
            this.DatabaseServiceMapList = new List<DatabaseServiceStoreMapVO>(16);
            this.ClientToStoreMap = new Dictionary<ClientPawnSecMachineVO, PawnSecStoreVO>(8);
            this.StoreToDatabaseServiceMap = new Dictionary<PawnSecStoreVO, List<DatabaseServiceVO>>(16);
            this.StoreToEsbServiceMap = new Dictionary<PawnSecStoreVO, List<EsbServiceVO>>(16);
            this.ESBServiceList = new List<EsbServiceVO>(8);
            this.DatabaseServiceList = new List<DatabaseServiceVO>(8);
            this.Stores = new List<PawnSecStoreVO>(1);
            this.MachineList = new List<StoreMachineVO>(1);
            this.MapsValid = false;
            this.NextIdSet = null;
        }

        public PawnSecVO()
        {
            this.StoreNumber = string.Empty;
            this.GlobalConfiguration = new GlobalConfigVO(string.Empty);
            this.ClientMachines = new List<ClientPawnSecMachineVO>(8);
            this.ClientStoreMapList = new List<ClientStoreMapVO>(8);
            this.ESBServiceMapList = new List<ESBServiceStoreMapVO>(16);
            this.DatabaseServiceMapList = new List<DatabaseServiceStoreMapVO>(16);
            this.ClientToStoreMap = new Dictionary<ClientPawnSecMachineVO, PawnSecStoreVO>(8);
            this.StoreToDatabaseServiceMap = new Dictionary<PawnSecStoreVO, List<DatabaseServiceVO>>(16);
            this.StoreToEsbServiceMap = new Dictionary<PawnSecStoreVO, List<EsbServiceVO>>(16);
            this.ESBServiceList = new List<EsbServiceVO>(8);
            this.DatabaseServiceList = new List<DatabaseServiceVO>(8);
            this.Stores = new List<PawnSecStoreVO>(1);
            this.MachineList = new List<StoreMachineVO>(1);
            this.MapsValid = false;
            this.NextIdSet = null;
        }

        /// <summary>        
        /// Retrieve pawn sec store vo based on set store number
        /// </summary>
        /// <returns>PawnSecStoreVO</returns>
        public PawnSecStoreVO GetStore()
        {
            if (!string.IsNullOrEmpty(this.StoreNumber) && 
                CollectionUtilities.isNotEmpty(this.Stores))
            {
                var fndStore = this.Stores.Find(x => x.StoreSite.StoreNumber.Equals(this.StoreNumber));
                return (fndStore);
            }

            return (null);
        }

        public void GenerateMaps()
        {
            //Clear ClientToStoreMap
            this.ClientToStoreMap.Clear();
            this.StoreToDatabaseServiceMap.Clear();
            this.StoreToEsbServiceMap.Clear();
            this.MachineList.Clear();

            //Generate machine to store map
            if (ClientMachines.Count > 0 && 
                Stores.Count > 0 && 
                ClientStoreMapList.Count > 0)
            {
                foreach(var machine in ClientMachines)
                {
                    if (machine == null) continue;
                    ulong machineId = machine.Machine.ClientId;
                    var mapEntry =
                            ClientStoreMapList.Find(
                                    x =>
                                    (x.ClientRegistryId == machineId &&
                                     x.StoreNumber.Equals(this.StoreNumber)));
                    if (mapEntry == null) continue;
                    var pwnSecStore =
                            this.Stores.Find(
                                    x => x.StoreSite.StoreNumber.Equals(mapEntry.StoreNumber));
                    if (pwnSecStore == null) continue;
                    //Add client to store map entry
                    ClientToStoreMap.Add(machine, pwnSecStore);
                    MachineList.Add(new StoreMachineVO(machine.Machine.MachineName,
                                                       machine.Machine.IsAllowed,
                                                       machine.Machine.IsConnected));
                    //Create entry for database service and esb service if and only if the store
                    //does not already exist as a key in the list
                    if (StoreToDatabaseServiceMap.ContainsKey(pwnSecStore)) continue;
                    var sConfig = pwnSecStore.StoreConfiguration;
                    var sConfigDbServMapList = DatabaseServiceMapList.FindAll(x => x.StoreConfigId == sConfig.Id);
                    if (CollectionUtilities.isNotEmpty(sConfigDbServMapList))
                    {
                        var dbServList =
                                from dbServ in DatabaseServiceList
                                join dbServMap in sConfigDbServMapList
                                        on dbServ.Id equals dbServMap.DatabaseServiceId
                                select dbServ;
                        StoreToDatabaseServiceMap.Add(pwnSecStore, new List<DatabaseServiceVO>(dbServList));
                    }
                    if (StoreToEsbServiceMap.ContainsKey(pwnSecStore)) continue;

                    var sConfigEsbServMapList = ESBServiceMapList.FindAll(x => x.StoreConfigId == sConfig.Id);
                    if (CollectionUtilities.isNotEmpty(sConfigEsbServMapList))
                    {
                        var esbServList =
                                from esbServ in ESBServiceList
                                join esbServMap in sConfigEsbServMapList
                                        on esbServ.Id equals esbServMap.ESBServiceId
                                select esbServ;
                        StoreToEsbServiceMap.Add(pwnSecStore, new List<EsbServiceVO>(esbServList));
                    }
                }
            }

            //If these three conditions are true, the maps are valid
            if (CollectionUtilities.isNotEmpty(ClientToStoreMap) &&
                CollectionUtilities.isNotEmpty(StoreToDatabaseServiceMap) &&
                CollectionUtilities.isNotEmpty(StoreToEsbServiceMap) &&
                CollectionUtilities.isNotEmpty(MachineList))
            {
                this.MapsValid = true;
            }
        }

        public void InitializeNextIdSet(PawnSecNextIdVO nextIdVO)
        {
            this.NextIdSet = nextIdVO;
        }
    }
}
