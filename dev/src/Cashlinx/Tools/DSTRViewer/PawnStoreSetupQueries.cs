using Common.Libraries.Forms.Components;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Application;

namespace DSTRViewer
{
    public static class PawnStoreSetupQueries
    {
        //Inserts a record into the client registry table
        //This table is the starting point for all clients in pawn sec
        //A client must exist in this table before it can be mapped
        //to a particular site and/or store
        public const string INSERT_CLIENTREGISTRY =
                "insert into clientregistry (id, machinename, ipaddress, macaddress, " +
                "isallowed, isconnected, adobeoverride, ghostscriptoverride, " +
                "creationdate, createdby, lastupdatedate, updatedby) values " +
                "('?CR_ID1?', '?CR_MACHINENAME?', '?CR_IPADDRESS?', '?CR_MACADDRESS?', " +
                "'?CR_ISALLOWED?', '?CR_ISCONNECTED?', '?CR_ADOBEOVERRIDE?', " +
                "'?CR_GHOSTSCRIPTOVERRIDE?', sysdate, 'admin', sysdate, 'admin')";

        public const string INSERT_STOREAPPVERSION =
                "insert into storeappversion (id, appversion, description, " +
                "creationdate, createdby, lastupdatedate, updatedby) " +
                "values ('?SAV_ID1?', '?SAV_APPVERSION?', '?SAV_APPVERSIONDESC?', " +
                "sysdate, 'admin', sysdate, 'admin')";

        public const string INSERT_STORESITEINFO =
                "insert into storesiteinfo (id, storenumber, state, companynumber, " +
                "aliascode, appversionid, companyname, creationdate, createdby, " +
                "lastupdatedate, updatedby) values ('?SSI_ID1?', '?SSI_STORENUMBER?', " +
                "'?SSI_STATE?', '?SSI_COMPANYNUMBER?', '?SSI_ALIAS?', '?SAV_ID1?', " +
                "'?SSI_COMPANYNAME?', sysdate, 'admin', sysdate, 'admin')";

        public const string INSERT_STORECONFIG =
                "insert into storeconfig (id, metalsfile, stonesfile, timezone, " +
                "fetchszmx, millisecondoffset, secondoffset, minuteoffset, houroffset, " +
                "dayoffset, monthoffset, yearoffset, creationdate, createdby, " +
                "lastupdatedate, updatedby) values ('?SC_ID1?', '?SC_METALSFILE?', " +
                "'?SC_STONESFILE?', '?SC_TIMEZONE?', '?SC_FETCHSZMX?', '?SC_MILLIOFF?', " +
                "'?SC_SECONDOFF?', '?SC_MINUTEOFF?', '?SC_HOUROFF?', '?SC_DAYOFF?', " +
                "'?SC_MONTHOFF?', '?SC_YEAROFF?', sysdate, 'admin', sysdate, 'admin')";

        public const string INSERT_STORECLIENTCONFIG =
                "insert into storeclientconfig (id, workstationid, terminalnumber, " +
                "loglevel, tracelevel, printenabled, creationdate, createdby, " +
                "lastupdatedate, updatedby) values ('?SCC_ID1?', '?SCC_WORKSTATIONID?', " +
                "'?SCC_TERMINALNUMBER?', '?SCC_LOGLEVEL?', '?SCC_TRACELEVEL?', " +
                "'?SCC_PRINTENABLED?', sysdate, 'admin', sysdate, 'admin')";

        public const string INSERT_GLOBALCONFIG =
                "insert into globalconfig (id, version, basetemplatepath, baselogpath, " +
                "basemediapath, adobereaderpath, ghostscriptpath, datapublickey, " +
                "creationdate, createdby, lastupdatedate, updatedby) values ('?GC_ID1?', " +
                "'?GC_VERSION?', '?GC_BASETEMPLATEPATH?', '?GC_BASELOGPATH?', " +
                "'?GC_BASEMEDIAPATH?', '?GC_ADOBEREADERPATH?', '?GC_GHOSTSCRIPTPATH?', " +
                "'?GC_PUBLICKEY?', sysdate, 'admin', sysdate, 'admin')";

        public const string INSERT_ORACLESERVICE =
                "insert into databaseservice (id, name, servicetype, server, port, " +
                "schema, dbuser, dbuserpwd, auxinfo, enabled, creationdate, createdby, " +
                "lastupdatedate, updatedby) values ('?DS_ORACLEID1?', '?DS_ORACLENAME1?', " +
                "'ORACLE', '?DS_ORACLESERVER1?', '?DS_ORACLEPORT1?', '?DS_ORACLESCHEMA1?', " +
                "'?DS_ORACLEUSER1?', '?DS_ORACLEPWD1?', '?DS_ORACLEAUXINFO1?', " +
                "'1', sysdate, 'admin', sysdate, 'admin')";

        public const string INSERT_LDAPSERVICE =
                "insert into databaseservice (id, name, servicetype, server, port, schema, " +
                "dbuser, dbuserpwd, auxinfo, enabled, creationdate, createdby, " +
                "lastupdatedate, updatedby) values ('?DS_LDAPID1?', '?DS_LDAPNAME1?', " +
                "'LDAP', '?DS_LDAPSERVER1?', '?DS_LDAPPORT1?', '?DS_LDAPSCHEMA1?', " +
                "'?DS_LDAPUSER1?', '?DS_LDAPPWD1?', '?DS_LDAPAUXINFO1?', " +
                "'1', sysdate, 'admin', sysdate, 'admin')";

        public const string INSERT_COUCHSERVICE =
                "insert into databaseservice (id, name, servicetype, server, port, schema, " +
                "dbuser, dbuserpwd, auxinfo, enabled, creationdate, createdby, " +
                "lastupdatedate, updatedby) values ('?DS_COUCHDBID1?', '?DS_COUCHDBNAME1?', " +
                "'COUCHDB', '?DS_COUCHDBSERVER1?', '?DS_COUCHDBPORT1?', '?DS_COUCHDBCHEMA1?', " +
                "'?DS_COUCHDBUSER1?', '?DS_COUCHDBPWD1?', '?DS_COUCHDBAUXINFO1?', " +
                "'1', sysdate, 'admin', sysdate, 'admin')";

        public const string INSERT_ESBSERVICE =
                "insert into esbservice (id, name, server, port, domain, uri, " +
                "endpointname, clientbinding, httpbinding, creationdate, createdby, " +
                "lastupdatedate, updatedby) values ('?ES_ID1?', '?ES_NAME1?', " +
                "'?ES_SERVER1?', '?ES_PORT1?', '?ES_DOMAIN1?', '?ES_URI1?', " +
                "'?ES_ENDPOINT1?', '?ES_CLIENTBINDING1?','?ES_HTTPBINDING1?', " +
                "sysdate, 'admin', sysdate, 'admin')";


        //Map table inserts
        public const string INSERT_DATASERVICEMAP =
                "insert into databaseservicestoremap (id, databaseserviceid, storeconfigid, " + 
                "creationdate, createdby, lastupdatedate, updatedby) values ('?DSSM_ID1?', " +
                "'?DS_DATAID1?', '?SC_ID1?', sysdate, 'admin', sysdate, 'admin')";

        public const string INSERT_CLIENTSTOREMAP =
                "insert into clientstoremap (id, clientregistryid, storesiteid, " + 
                "storeclientconfigid, storeconfigid, creationdate, createdby, " + 
                "lastupdatedate, updatedby) values ('?CSM_ID1?', '?CR_ID1?', '?SSI_ID1?', " + 
                "'?SCC_ID1?', '?SC_ID1?', sysdate, 'admin', sysdate, 'admin')";

        public const string INSERT_ESBSERVICEMAP =
                "insert into esbservicestoremap (id, esbserviceid, storeconfigid, " + 
                "creationdate, createdby, lastupdatedate, updatedby) values " + 
                "('?ESSM_ID1?', '?ES_ID1?', '?SC_ID1?', sysdate, 'admin', sysdate, 'admin')";
             

        //Retrieves the next id in any table that contains an id column
        //For larger tables it would be much more efficient to utilize
        //the next value of a sequence!!
        //ID_COLUMN_NAME, TABLE_NAME
        public const string SELECT_NEXTID = 
            "select max(?ID_COLUMN_NAME?) + 1 as ID from ?TABLE_NAME?";

        public const string SELECT_NEXTID_STR =
            "select to_nchar(max(to_number(?ID_COLUMN_NAME?))+1) as ID from ?TABLE_NAME?";        
       

        public const string SELECT_PAWNSEC_MACHINES = 
            "select " + 
            "clr.id as ID, " + 
            "clr.ipaddress as IPADDRESS, " + 
            "clr.machinename as MACHINENAME, " + 
            "clr.macaddress as MACADDRESS, " + 
            "clr.isallowed as ISALLOWED, " + 
            "clr.isconnected as ISCONNECTED, " +
            "clr.adobeoverride as ADOBEOVERRIDE, " +
            "clr.ghostscriptoverride as GHOSTOVERRIDE, " +
            "clsm.id as CLIENTSTOREMAPID, " +
            "clsm.clientregistryid as CLIENTREGISTRYID, " +
            "clsm.storesiteid as STORESITEID, " +
            "clsm.storeclientconfigid as STORECLIENTCONFIGID, " +
            "clsm.storeconfigid as STORECONFIGID, " +
            "stcc.workstationid as WORKSTATIONID, " +
            "stcc.terminalnumber as TERMINALNUMBER, " +
            "stcc.loglevel as LOGLEVEL, " +
            "stcc.tracelevel as TRACELEVEL, " +
            "stcc.printenabled as PRINTENABLED, " +
            "stsi.storenumber as STORENUMBER " +
            "from clientregistry clr " + 
            "inner join clientstoremap clsm on clr.id = clsm.clientregistryid " +
            "inner join storeclientconfig stcc on clsm.storeclientconfigid = stcc.id " +
            "inner join storesiteinfo stsi on clsm.storesiteid = stsi.id " +
            "where stsi.storenumber = '?STORENUMBER?' order by clr.id";

        public const string SELECT_PAWNSEC_STORE =
            "select " +
            "stc.id as ID, " +
            "stc.metalsfile as METALSFILE, " +
            "stc.stonesfile as STONESFILE, " +
            "stc.timezone as TIMEZONE, " +
            "stc.fetchszmx as FETCHSZMX, " +
            "stc.dayoffset as DAYOFFSET, " +
            "stc.monthoffset as MONTHOFFSET, " +
            "ssi.id as STORESITEINFOID, " +
            "ssi.storenumber as STORENUMBER, " +
            "ssi.state as STATE, " +
            "ssi.companynumber as COMPANYNUMBER, " +
            "ssi.companyname as COMPANYNAME, " +
            "ssi.aliascode as ALIASCODE, " +
            "ssi.appversionid as APPVERSIONID, " +
            "stav.appversion as APPVERSION " +
            "from storeconfig stc, storesiteinfo ssi " +
            "inner join storeappversion stav on stav.id = ssi.appversionid " +
            "where stc.id = '?STORECONFIGID?' and ssi.id = '?STORESITEINFOID?'";

        public const string SELECT_PAWNSEC_ESB =
            "select " +
            "ess.id as ID, " +
            "ess.name as NAME, " +
            "ess.server as SERVER, " +
            "ess.port as PORT, " +
            "ess.domain as DOMAIN, " +
            "ess.uri as URI, " +
            "ess.enabled as ENABLED, " +
            "ess.endpointname as ENDPOINTNAME, " +          
            "essm.id as ESBMAPID, " +
            "essm.storeconfigid as ESBMAPSTORECONFIGID " +
            "from esbservice ess " + 
            "inner join esbservicestoremap essm on ess.id = essm.esbserviceid " +
            "where essm.storeconfigid = '?STORECONFIGID?' " +
            "order by ess.id";

        public const string SELECT_PAWNSEC_DB =
            "select " +
            "dbs.id as ID, " +
            "dbs.name as NAME, " +
            "dbs.servicetype as SERVICETYPE, " +
            "dbs.server as SERVER, " +
            "dbs.port as PORT, " +
            "dbs.schema as SCHEMA, " +
            "dbs.dbuser as DBUSER, " +
            "dbs.dbuserpwd as DBUSERPWD, " +
            "dbs.auxinfo as AUXINFO, " +
            "dbs.enabled as ENABLED, " +
            "dssm.id as DATABASEMAPID, " +
            "dssm.storeconfigid as DATABASEMAPSTORECONFIGID " +
            "from databaseservice dbs " +
            "inner join databaseservicestoremap dssm on dbs.id = dssm.databaseserviceid " +
            "where dssm.storeconfigid = '?STORECONFIGID?' " +
            "order by dbs.id ";

        public const string SELECT_PAWNSEC_LDAP =
            "select " +
            "dbs.id as ID, " +
            "dbs.name as NAME, " +
            "dbs.servicetype as SERVICETYPE, " +
            "dbs.server as SERVER, " +
            "dbs.port as PORT, " +
            "dbs.schema as SCHEMA, " +
            "dbs.dbuser as DBUSER, " +
            "dbs.dbuserpwd as DBUSERPWD, " +
            "dbs.auxinfo as AUXINFO, " +
            "dbs.enabled as ENABLED " +
            "from databaseservice dbs " +
            "where dbs.servicetype = 'LDAP'";

        public const string SELECT_PAWNSEC_EXISTESB =
            "select " +
            "ess.id as ID, " +
            "ess.name as NAME, " +
            "ess.server as SERVER, " +
            "ess.port as PORT, " +
            "ess.domain as DOMAIN, " +
            "ess.uri as URI, " +
            "ess.enabled as ENABLED " +
            "from esbservice ess";

        
        public const string SELECT_PAWNSEC_GLOBAL =
            "select " +
            "id as ID, " +
            "version as APPVERSIONID, " +
            "basetemplatepath as TEMPLATEPATH, " +
            "baselogpath as LOGPATH, " +
            "basemediapath as MEDIAPATH, " +
            "adobereaderpath as ADOBEPATH, " +
            "ghostscriptpath as GHOSTPATH, " +
            "datapublickey as PUBLICKEY " +
            "from globalconfig " +
            "where version = '?APPVERSION?'";


        ///////////////////////////////////////////////////////////////////////////
        // CCSOWNER MERGE SQL CODE
        //WKSP_ID, WKSP_PRID, WKSP_WKID
        public const string MERGE_CCSOWNER_WORKSTATIONPERIPHERALS =
             "merge into ccsowner.workstationperipherals a using " +
             "(select " +
             "'?WKSP_ID?' as storeperipheralid, " +
             "'?WKSP_PRID?' as peripheralid, " +
             "'?WKSP_WKID?' as workstationid " +
             "from dual) b " +
            "on (a.storeperipheralid = b.storeperipheralid or ( a.peripheralid = b.peripheralid and a.workstationid = b.workstationid )) " +
            "when not matched then " +
            "insert ( " +
              "a.storeperipheralid, a.peripheralid, a.ipaddress, a.userid, a.creationdate, " +
              "a.lastupdatedate, a.workstationid, a.preferenceorder, a.networkname) " +
            "values ( " +
              "'?WKSP_ID?', '?WKSP_PRID?', '', '', sysdate, " +
              "sysdate, '?WKSP_WKID?', '1', '1160') " +
            "when matched then " +
            "update set " +
              "a.peripheralid = '?WKSP_PRID?', " +
              "a.lastupdatedate = sysdate, " +
              "a.workstationid = '?WKSP_WKID?'";


        ///////////////////////////////////////////////////////////////////////////
        // CCSOWNER MERGE SQL CODE
        //WKSP_ID, WKSP_PRID, WKSP_WKID
        public const string MERGE_CCSOWNER_PAWNWORKSTATIONPERIPHERALS =
             "merge into ccsowner.pawnworkstationperipherals a using " +
             "(select " +
             "'?WKSP_ID?' as storeperipheralid, " +
             "'?WKSP_PRID?' as peripheralid, " +
             "'?WKSP_WKID?' as workstationid " +
             "from dual) b " +
            "on (a.storeperipheralid = b.storeperipheralid or ( a.peripheralid = b.peripheralid and a.workstationid = b.workstationid )) " +
            "when not matched then " +
            "insert ( " +
              "a.storeperipheralid, a.peripheralid, a.ipaddress, a.userid, a.creationdate, " +
              "a.lastupdatedate, a.workstationid, a.preferenceorder, a.networkname) " +
            "values ( " +
              "'?WKSP_ID?', '?WKSP_PRID?', '', '', sysdate, " +
              "sysdate, '?WKSP_WKID?', '1', '1160') " +
            "when matched then " +
            "update set " +
              "a.peripheralid = '?WKSP_PRID?', " +
              "a.lastupdatedate = sysdate, " +
              "a.workstationid = '?WKSP_WKID?'"; 

        /*public const string MERGE_CCSOWNER_WORKSTATIONPERIPHERALS =
             "MERGE INTO CCSOWNER.WORKSTATIONPERIPHERALS A USING " +
             "(SELECT " +
             "'?WKSP_ID?' AS STOREPERIPHERALID, " +
             "'?WKSP_PRID?' AS PERIPHERALID, " +
             "'?WKSP_WKID?' AS WORKSTATIONID " +
             "FROM DUAL) B " +
            "ON (A.STOREPERIPHERALID = B.STOREPERIPHERALID OR ( A.PERIPHERALID = B.PERIPHERALID AND A.WORKSTATIONID = B.WORKSTATIONID )) " +
            "WHEN NOT MATCHED THEN " + 
            "INSERT ( " +
              "A.STOREPERIPHERALID, A.PERIPHERALID, A.IPADDRESS, A.USERID, A.CREATIONDATE, " + 
              "A.LASTUPDATEDATE, A.WORKSTATIONID, A.PREFERENCEORDER, A.NETWORKNAME) " +
            "VALUES ( " +
              "'?WKSP_ID?', '?WKSP_PRID?', '', '', sysdate, " + 
              "sysdate, '?WKSP_WKID?', '1', '1160') " +
            "WHEN MATCHED THEN " +
            "UPDATE SET " + 
              "A.PERIPHERALID = '?WKSP_PRID?', " +
              "A.LASTUPDATEDATE = sysdate, " +
              "A.WORKSTATIONID = '?WKSP_WKID?'";
        */
        //Madhu
        //PER_ID, PER_TID, PER_IP, PER_PT, PER_STOID, PER_NAME, PER_MDID, IS_PRI, PER_PRFID
        public const string MERGE_CCSOWNER_PERIPHERALS_NEW =
              "merge into ccsowner.peripherals a using " +
              "(select " +
              "'?PER_ID?' as peripheralid, '?PER_TID?' as peripheraltypeid, " +
              "'?PER_IP?' as ipaddress, '?PER_PT?' as portnumber, '?PER_STOID?' as storeid " +
              "from dual) b " +
              "on (a.peripheralid = b.peripheralid or (a.peripheraltypeid = b.peripheraltypeid and a.ipaddress = b.ipaddress and a.portnumber = 	b.portnumber and a.storeid = b.storeid)) " +
              "when not matched then " +
              "insert ( " +
              "a.peripheralid, a.peripheralname, a.serialnumber, a.userid, a.creationdate, " +
              "a.lastupdatedate, a.peripheraltypeid, a.ipaddress, a.portnumber, a.modelid, " +
              "a.storeid, a.isprimary, a.description, a.peripheral_pref_order) " +
              "values ( " +
              "'?PER_ID?', '?PER_NAME?', '', '', sysdate, " +
              "sysdate, '?PER_TID?', '?PER_IP?', '?PER_PT?', '?PER_MDID?', " +
              "'?PER_STOID?', '?IS_PRI?', '', '?PER_PRFID?') " +
              "when matched then " +
              "update set " +
              "a.peripheralname = '?PER_NAME?', " +
              "a.lastupdatedate = sysdate, " +
              "a.peripheraltypeid = '?PER_TID?', " +
              "a.ipaddress = '?PER_IP?', " +
              "a.portnumber = '?PER_PT?', " +
              "a.modelid = '?PER_MDID?', " +
              "a.storeid = '?PER_STOID?', " +
              "a.isprimary = '?IS_PRI?', " +
              "a.peripheral_pref_order = '?PER_PRFID?' ";
        
        /*public const string MERGE_CCSOWNER_PERIPHERALS_NEW =
              "MERGE INTO CCSOWNER.PERIPHERALS A USING " +
              "(SELECT " +
              "'?PER_ID?' AS PERIPHERALID, '?PER_TID?' AS PERIPHERALTYPEID, " +
              "'?PER_IP?' AS IPADDRESS, '?PER_PT?' AS PORTNUMBER, '?PER_STOID?' AS STOREID " +
              "FROM DUAL) B " +
              "ON (A.PERIPHERALID = B.PERIPHERALID OR (A.PERIPHERALTYPEID = B.PERIPHERALTYPEID AND A.IPADDRESS = B.IPADDRESS AND A.PORTNUMBER = B.PORTNUMBER AND A.STOREID = B.STOREID)) " +
              "WHEN NOT MATCHED THEN " +
              "INSERT ( " +
              "A.PERIPHERALID, A.PERIPHERALNAME, A.SERIALNUMBER, A.USERID, A.CREATIONDATE, " +
              "A.LASTUPDATEDATE, A.PERIPHERALTYPEID, A.IPADDRESS, A.PORTNUMBER, A.MODELID, " +
              "A.STOREID, A.ISPRIMARY, A.DESCRIPTION, A.PERIPHERAL_PREF_ORDER) " +
              "VALUES ( " +
              "'?PER_ID?', '?PER_NAME?', '', '', sysdate, " +
              "sysdate, '?PER_TID?', '?PER_IP?', '?PER_PT?', '?PER_MDID?', " +
              "'?PER_STOID?', '?IS_PRI?', '', '?PER_PRFID?') " +
              "WHEN MATCHED THEN " +
              "UPDATE SET " +
              "A.PERIPHERALNAME = '?PER_NAME?', " +
              "A.LASTUPDATEDATE = sysdate, " +
              "A.PERIPHERALTYPEID = '?PER_TID?', " +
              "A.IPADDRESS = '?PER_IP?', " +
              "A.PORTNUMBER = '?PER_PT?', " +
              "A.MODELID = '?PER_MDID?', " +
              "A.STOREID = '?PER_STOID?' " +
              "A.ISPRIMARY = '?IS_PRI?', " +
              "A.PERIPHERAL_PREF_ORDER = '?PER_PRFID?' ";
        */
        //PER_ID, PER_TID, PER_IP, PER_PT, PER_STOID, PER_NAME, PER_MDID
        public const string MERGE_CCSOWNER_PERIPHERALS =   
              "MERGE INTO CCSOWNER.PERIPHERALS A USING " +
              "(SELECT " +
              "'?PER_ID?' AS PERIPHERALID, '?PER_TID?' AS PERIPHERALTYPEID, " +
              "'?PER_IP?' AS IPADDRESS, '?PER_PT?' AS PORTNUMBER, '?PER_STOID?' AS STOREID " +
              "FROM DUAL) B " +
              "ON (A.PERIPHERALID = B.PERIPHERALID OR (A.PERIPHERALTYPEID = B.PERIPHERALTYPEID AND A.IPADDRESS = B.IPADDRESS AND A.PORTNUMBER = B.PORTNUMBER AND A.STOREID = B.STOREID)) " +
              "WHEN NOT MATCHED THEN " + 
              "INSERT ( " +
              "A.PERIPHERALID, A.PERIPHERALNAME, A.SERIALNUMBER, A.USERID, A.CREATIONDATE, " + 
              "A.LASTUPDATEDATE, A.PERIPHERALTYPEID, A.IPADDRESS, A.PORTNUMBER, A.MODELID, " + 
              "A.STOREID, A.ISPRIMARY, A.DESCRIPTION, A.PERIPHERAL_PREF_ORDER) " +
              "VALUES ( " +
              "'?PER_ID?', '?PER_NAME?', '', '', sysdate, " + 
              "sysdate, '?PER_TID?', '?PER_IP?', '?PER_PT?', '?PER_MDID?', " + 
              "'?PER_STOID?', '', '', '') " +
              "WHEN MATCHED THEN " +
              "UPDATE SET " + 
              "A.PERIPHERALNAME = '?PER_NAME?', " +
              "A.LASTUPDATEDATE = sysdate, " +
              "A.PERIPHERALTYPEID = '?PER_TID?', " +
              "A.IPADDRESS = '?PER_IP?', " +
              "A.PORTNUMBER = '?PER_PT?', " +
              "A.MODELID = '?PER_MDID?', " +
              "A.STOREID = '?PER_STOID?'";

        //UR_ID
        public const string MERGE_CCSOWNER_USERGROUP =
            "Insert into CCSOWNER.USERGROUP(USERID, GROUPID) Values('?UR_ID?', '1')";
        
        //UR_ID, UR_RID
        public const string MERGE_CCSOWNER_USERROLES =
            "MERGE INTO CCSOWNER.USERROLES A USING " +
            "(SELECT '?UR_ID?' AS USERID FROM DUAL) B " +
            "ON (A.USERID = B.USERID) " +
            "WHEN NOT MATCHED THEN " +
            "INSERT ( A.USERID, A.ROLEID) " +
            "VALUES ( '?UR_ID?', '?UR_RID?') " +
            "WHEN MATCHED THEN " +
            "UPDATE SET A.ROLEID = '?UR_RID?'";

        //UID_ID, UID_USID, UID_STONUM, UID_EMNUM
        public const string MERGE_CCSOWNER_USERINFODETAIL =
            "MERGE INTO CCSOWNER.USERINFODETAIL A USING " +
            "(SELECT " +
            "'?UID_ID?' AS USERINFODETAILID, '?UID_USID?' AS USERID, '?UID_STONUM?' AS FACNUMBER " +
            "FROM DUAL) B " +
            "ON (A.USERINFODETAILID = B.USERINFODETAILID OR (A.USERID = B.USERID AND A.FACNUMBER = B.FACNUMBER)) " +
            "WHEN NOT MATCHED THEN " + 
            "INSERT ( " +
            "A.USERINFODETAILID, A.USERID, A.EMPLOYEENUMBER, A.CSRSIGNATUREUPDATED, A.CSRSIGNATURE, " + 
            "A.CREATIONDATE, A.LASTUPDATEDATE, A.CREATEDBY, A.UPDATEDBY, A.FACNUMBER) " +
            "VALUES ( " +
            "'?UID_ID?', '?UID_USID?', '?UID_EMNUM?', '', '', " + 
            "sysdate, sysdate, 'admin', 'admin', '?UID_STONUM?') " +
            "WHEN MATCHED THEN " +
            "UPDATE SET " + 
            "A.USERID = '?UID_USID?', " +
            "A.EMPLOYEENUMBER = '?UID_EMNUM?', " +
            "A.LASTUPDATEDATE = sysdate, " +
            "A.UPDATEDBY = 'admin', " +
            "A.FACNUMBER = '?UID_STONUM?'";

        //UI_ID, UI_NAME, UI_FNAME, UI_LNAME, UI_STONUM
        public const string MERGE_CCSOWNER_USERINFO =
            "MERGE INTO CCSOWNER.USERINFO A USING " +
            "(SELECT " +
            "'?UI_NAME?' AS NAME, " +
            "'?UI_STONUM?' AS LOCATION, ?UI_ID? AS USERID " +
            "FROM DUAL) B " +
            "ON (A.USERID = B.USERID) " +
            "WHEN NOT MATCHED THEN " +
            "INSERT ( " +
            "A.ACTIVATED, A.NAME, A.PASSWORD, A.FNAME, A.LNAME, " + 
            "A.AUTHID, A.COOKIE, A.CREDITS, A.CRLIMIT, A.PREFERENCES, " + 
            "A.SELFREG, A.LOCATION, A.PHONE, A.LOCALE, A.CREATIONDATE, " + 
            "A.LASTUPDATEDATE, A.CSRSIGNATUREUPDATED, A.USERID, A.OBJECTID, A.LASTLOGIN, " + 
            "A.OGRPID, A.REGDATE, A.CREATEUSERNAME, A.MODIFIEDDATE, A.MODIFIEDUSERNAME) " +
            "VALUES ( " +
            "1, '?UI_NAME?', 'NA', '?UI_FNAME?', '?UI_LNAME?', " + 
            "1, '', 0, 0, 0, " + 
            "0, '?UI_STONUM?', '', 'en_US', sysdate, " + 
            "sysdate, sysdate, ?UI_ID?, '', null, " + 
            "null, null, 'admin', sysdate, 'admin') " +
            "WHEN MATCHED THEN " +
            "UPDATE SET " + 
            "A.ACTIVATED = 1, " +
            "A.NAME = '?UI_NAME?', " +
            "A.FNAME = '?UI_FNAME?', " +
            "A.LNAME = '?UI_LNAME?', " +
            "A.LOCATION = '?UI_STONUM?', " +
            "A.LASTUPDATEDATE = sysdate, " +
            "A.MODIFIEDDATE = sysdate, " +
            "A.MODIFIEDUSERNAME = 'admin'";


        //?CD_ID?, ?SAFE_ID?
        public const string UPDATE_CDOWNER_CASHDRAWER = "UPDATE CDOWNER.CD_CASHDRAWER SET REGISTERUSERID = '?CD_ID?', MODIFYUSERID='?CD_ID?' WHERE ID = '?SAFE_ID?'";

        ///////////////////////////////////////////////////////////////////////////
        // CDOWNER MERGE SQL CODE
        //CD_ID, CD_NAME, CD_MGRID, CD_OTYPE, CD_STONUM, CD_BRID
        public const string MERGE_CDOWNER_CASHDRAWER =
            "MERGE INTO CDOWNER.CD_CASHDRAWER A USING " +
             "( SELECT " +
              "'?CD_ID?' AS ID, '?CD_NAME?' AS NAME, '?CD_OTYPE?' AS OBJECTTYPE, '?CD_BRID?' AS BRANCHID " +
              "FROM DUAL ) B " +
            "ON (A.ID = B.ID OR (A.NAME = B.NAME AND A.OBJECTTYPE = B.OBJECTTYPE AND A.BRANCHID = B.BRANCHID)) " +
            "WHEN NOT MATCHED THEN " +
            "INSERT ( " +
              "A.ID, A.NAME, A.DESCRIPTION, A.LASTCONNECTDATE, A.ACCOUNTINGDATE, " + 
              "A.LOWERLIMIT, A.UPPERLIMIT, A.REGISTERUSERID, A.REGISTERDATE, A.UPDATEDATE, " + 
              "A.MODIFYUSERID, A.LATEFLAG, A.OBJECTTYPE, A.BANKID, A.BRANCHID, " + 
              "A.NETNAME, A.OPENFLAG) " +
            "VALUES ( " +
              "'?CD_ID?', '?CD_NAME?', '?CD_NAME?', sysdate, sysdate, " + 
              "-100000, 100000, '?CD_MGRID?', sysdate, sysdate, " + 
              "'?CD_MGRID?', '0', '?CD_OTYPE?', '?CD_STONUM?', '?CD_BRID?', " + 
              "'1160', '0') " +
            "WHEN MATCHED THEN " +
            "UPDATE SET " + 
              "A.NAME = '?CD_NAME?', " +
              "A.DESCRIPTION = '?CD_NAME?', " +
              "A.REGISTERUSERID = '?CD_MGRID?', " +
              "A.REGISTERDATE = sysdate, " +
              "A.UPDATEDATE = sysdate, " +
              "A.MODIFYUSERID = '?CD_MGRID?', " +
              "A.BANKID = '?CD_STONUM?', " +
              "A.BRANCHID = '?CD_BRID?', " +
              "A.NETNAME = '1160', " +
              "A.OPENFLAG = '0'";

        //CDU_ID, CDU_UID, CDU_NAME, CDU_STONUM, CDU_BRID
        public const string MERGE_CDOWNER_CASHDRAWERUSER =
            "MERGE INTO CDOWNER.CD_CASHDRAWERUSER A USING " +
             "(SELECT " +
              "'?CDU_ID?' AS ID, '?CDU_UID?' AS USERID, '?CDU_NAME?' AS USERNAME, '?CDU_BRID?' AS BRANCHID " +              
              "FROM DUAL) B " +
            "ON (A.ID = B.ID OR (A.USERID = B.USERID AND A.USERNAME = B.USERNAME AND A.BRANCHID = B.BRANCHID)) " +
            "WHEN NOT MATCHED THEN " + 
            "INSERT ( " +
              "A.ID, A.USERID, A.USERNAME, A.BANKID, A.BRANCHID, " +
              "A.NETNAME, A.ISACTIVE, A.CREATEDBY, A.UPDATEDBY, A.CREATIONDATE, A.LASTUPDATEDATE) " +
            "VALUES ( " +
              "'?CDU_ID?', '?CDU_UID?', '?CDU_NAME?', '?CDU_STONUM?', '?CDU_BRID?', " +
              "'1160', 'Y', 'admin', 'admin', sysdate, sysdate) " +
            "WHEN MATCHED THEN " +
            "UPDATE SET " +
              "A.USERID = '?CDU_UID?', " +
              "A.USERNAME = '?CDU_NAME?', " +
              "A.BANKID = '?CDU_STONUM?', " +
              "A.BRANCHID = '?CDU_BRID?', " +
              "A.NETNAME = '1160', " +
              "A.ISACTIVE = 'Y', " +
              "A.UPDATEDBY = 'admin', " +
              "A.LASTUPDATEDATE = sysdate";

        //CDW_ID, CDW_NAME, CDW_BRID, CDW_STONUM, CDW_BRID
        public const string MERGE_CDOWNER_WORKSTATION =
            "merge into cdowner.cd_workstation a using " +
             "(select " +
              "'?CDW_ID?' as id, '?CDW_NAME?' as name, '?CDW_BRID?' as branchid " +
              "from dual ) b " +
            "on (a.id = b.id or (a.name = b.name and a.branchid = b.branchid)) " +
            "when not matched then " +
            "insert ( " +
              "a.id, a.name, a.bankid, a.branchid, a.netname) " +
            "values ( " +
              "'?CDW_ID?', '?CDW_NAME?', '?CDW_STONUM?', '?CDW_BRID?', '1160') " +
            "when matched then " +
            "update set " +
              "a.name = '?CDW_NAME?', " +
              "a.bankid = '?CDW_STONUM?', " +
              "a.branchid = '?CDW_BRID?'," +
              "a.netname = '1160'";

/*        public const string MERGE_CDOWNER_WORKSTATION =
            "MERGE INTO CDOWNER.CD_WORKSTATION A USING " +
             "(SELECT " +
              "'?CDW_ID?' AS ID, '?CDW_NAME?' AS NAME, '?CDW_BRID?' AS BRANCHID " +
              "FROM DUAL ) B " +
            "ON (A.ID = B.ID OR (A.NAME = B.NAME AND A.BRANCHID = B.BRANCHID)) " +
            "WHEN NOT MATCHED THEN " + 
            "INSERT ( " +
              "A.ID, A.NAME, A.BANKID, A.BRANCHID, A.NETNAME) " +
            "VALUES ( " +
              "'?CDW_ID?', '?CDW_NAME?', '?CDW_STONUM?', '?CDW_BRID?', '1160') " +
            "WHEN MATCHED THEN " +
            "UPDATE SET " + 
              "A.NAME = '?CDW_NAME?', " +
              "A.BANKID = '?CDW_STONUM?', " +
              "A.BRANCHID = '?CDW_BRID?'," +
              "A.NETNAME = '1160'";                
*/
        ///////////////////////////////////////////////////////////////////////////
        //PAWNSEC MERGE SQL CODE 
        //CR_ID1, CR_MACHINENAME, CR_IPADDRESS, CR_MACADDRESS, CR_ISALLOWED, CR_ISCONNECTED, CR_ADOBEOVERRIDE, CR_GHOSTSCRIPTOVERRIDE
        public const string MERGE_PAWNSEC_CLIENTREGISTRY =
            "merge into clientregistry a " +
            "using ( " +
                "select '?CR_ID1?' as id " +
                "from dual ) b " +
            "on (a.id = b.id) " +
            "when matched then " +
                "update set a.machinename = '?CR_MACHINENAME?', a.ipaddress = '?CR_IPADDRESS?', a.macaddress = '?CR_MACADDRESS?', a.isallowed = '?CR_ISALLOWED?', a.isconnected = '?CR_ISCONNECTED?', " +
                    "a.adobeoverride = '?CR_ADOBEOVERRIDE?', a.ghostscriptoverride = '?CR_GHOSTSCRIPTOVERRIDE?', a.lastupdatedate = sysdate, a.updatedby = 'admin' " +
            "when not matched then " +
                "insert (a.id, a.machinename, a.ipaddress, a.macaddress, a.isallowed, a.isconnected, a.adobeoverride, a.ghostscriptoverride, a.creationdate, a.createdby, a.lastupdatedate, a.updatedby) " +
                "values ('?CR_ID1?', '?CR_MACHINENAME?', '?CR_IPADDRESS?', '?CR_MACADDRESS?', '?CR_ISALLOWED?', '?CR_ISCONNECTED?', '?CR_ADOBEOVERRIDE?', '?CR_GHOSTSCRIPTOVERRIDE?', sysdate, 'admin', sysdate, 'admin')";

        //STORE_TYPEID, PRODUCT_MENUID, PRODUCTID
        public const string MERGE_CCSOWNER_STOREPRODUCTS = "INSERT INTO CCSOWNER.STOREPRODUCTS ("+
                                       "STORE_PRODUCT_ID, STORE_TYPE_ID,"+
                                       "PRODUCT_ID, STORE_PRODUCT_MENU_ID, CREATIONDATE, CREATEDBY ) VALUES   ("+
               "(SELECT   MAX (STORE_PRODUCT_ID) + 1"+
                  " FROM   CCSOWNER.STOREPRODUCTS),"+
               "'?STORE_TYPEID?', '?PRODUCTID?', '?PRODUCT_MENUID?', sysdate, 'SETUP_TOOL')";

        //NEXTNUM_TYPE, STORENUMBER
        public const string MERGE_CCSOWNER_NEXTNUM = "INSERT INTO CCSOWNER.NEXTNUM (" +
            "NEXTNUM_ID, NEXTNUM_TYPE, STORENUMBER, NEXT_NUMBER, MAX_NUMBER, "+
            "INCREMENTNUM, CREATIONDATE, LASTUPDATEDATE, CREATEDBY, UPDATEDBY ) VALUES (" +
            "(SELECT MAX(NEXTNUM_ID)+1 FROM CCSOWNER.NEXTNUM), '?NEXTNUM_TYPE?', '?STORENUMBER?', 1000, 999999, 1, sysdate, sysdate, 'SETUP_TOOL', 'SETUP_TOOL')";
                                       


        //STORE TYPE CREATION
        //ST_ID1, ST_STORENUM, IS_TPS_SAFE, IS_INTEG, PAWN_PRIM, TOPS_EXIST
        public const string MERGE_CCSOWNER_STORETYPE =
                        "merge into CCSOWNER.STORETYPE a " +
            "using ( " +
                "select '?ST_ID1?' as id " +
                "from dual ) b " +
            "on (a.id = b.id) " +
            "when matched then " +
                "update set a.storenumber = '?ST_STORENUM?', a.createdby = 'SETUP_TOOL', a.updatedby = 'SETUP_TOOL', a.creationdate = sysdate, " +
                    "a.lastupdatedate = sysdate, a.is_tops_safe='?IS_TPS_SAFE?', a.is_integrated='?IS_INTEG?', a.is_pawn_primary='?PAWN_PRIM?', a.is_tops_exist='?TOPS_EXIST?'" +
            "when not matched then " +
                "insert (a.id, a.storenumber, a.createdby, a.updatedby, a.creationdate, a.lastupdatedate, a.is_tops_safe, a.is_integrated, a.is_pawn_primary, a.is_tops_exist) " +
                "values ('?ST_ID1?', '?ST_STORENUM?', 'SETUP_TOOL', 'SETUP_TOOL', sysdate, sysdate, '?IS_TPS_SAFE?', '?IS_INTEG?', '?PAWN_PRIM?', '?TOPS_EXIST?')";
        //SAV_ID1, SAV_APPVERSION, SAV_APPVERSIONDESC
        public const string MERGE_PAWNSEC_STOREAPPVERSION =
            "merge into storeappversion a " +
            "using ( " +
                "select '?SAV_ID1?' as id " +
                "from dual ) b " +
            "on (a.id = b.id) " +
            "when matched then " +
                "update set a.appversion = '?SAV_APPVERSION?', a.description = '?SAV_APPVERSIONDESC?', a.lastupdatedate = sysdate, a.updatedby = 'admin' " +
            "when not matched then " +
                "insert (a.id, a.appversion, a.description, a.creationdate, a.createdby, a.lastupdatedate, a.updatedby) " +
                "values ('?SAV_ID1?', '?SAV_APPVERSION?', '?SAV_APPVERSIONDESC?', sysdate, 'admin', sysdate, 'admin')";

        //SSI_ID1, SSI_STORENUMBER, SSI_STATE, SSI_COMPANYNUMBER, SSI_ALIAS, SAV_ID1, SSI_COMPANYNAME
        public const string MERGE_PAWNSEC_STORESITEINFO =
            "merge into storesiteinfo a " +
            "using ( " +
                "select '?SSI_ID1?' as id " +
                "from dual) b " +
            "on (a.id = b.id) " +
            "when matched then " +
                "update set a.storenumber = '?SSI_STORENUMBER?', a.state = '?SSI_STATE?', a.companynumber = '?SSI_COMPANYNUMBER?', a.aliascode = '?SSI_ALIAS?', " +
                    "a.appversionid = '?SAV_ID1?', a.companyname = '?SSI_COMPANYNAME?', a.lastupdatedate = sysdate, a.updatedby = 'admin' " +
            "when not matched then " +
                "insert (a.id, a.storenumber, a.state, a.companynumber, a.aliascode, a.appversionid, a.companyname, a.creationdate, a.createdby, a.lastupdatedate, a.updatedby) " +
                "values ('?SSI_ID1?', '?SSI_STORENUMBER?', '?SSI_STATE?', '?SSI_COMPANYNUMBER?', '?SSI_ALIAS?', '?SAV_ID1?', '?SSI_COMPANYNAME?', sysdate, 'admin', sysdate, 'admin')";

        //SC_ID1, SC_METALSFILE, SC_STONESFILE, SC_TIMEZONE, SC_FETCHSZMX, SC_MILLIOFF, SC_SECONDOFF, SC_MINUTEOFF, SC_HOUROFF, SC_DAYOFF, SC_MONTHOFF, SC_YEAROFF
        public const string MERGE_PAWNSEC_STORECONFIG =
            "merge into storeconfig a " +
            "using ( " +
                "select '?SC_ID1?' as id " +
                "from dual) b " +
            "on (a.id = b.id) " +
            "when matched then " +
                "update set a.metalsfile = '?SC_METALSFILE?', a.stonesfile = '?SC_STONESFILE?', a.timezone = '?SC_TIMEZONE?', a.fetchszmx = '?SC_FETCHSZMX?', " +
                    "a.millisecondoffset = '?SC_MILLIOFF?', a.secondoffset = '?SC_SECONDOFF?', a.minuteoffset = '?SC_MINUTEOFF?', a.houroffset = '?SC_HOUROFF?', " +
                    "a.dayoffset = '?SC_DAYOFF?', a.monthoffset = '?SC_MONTHOFF?', a.yearoffset = '?SC_YEAROFF?', a.lastupdatedate = sysdate, a.updatedby = 'admin' " +
            "when not matched then " +
                "insert (a.id, a.metalsfile, a.stonesfile, a.timezone, a.fetchszmx, a.millisecondoffset, a.secondoffset, a.minuteoffset, a.houroffset, a.dayoffset, a.monthoffset, a.yearoffset, " +
                    "a.creationdate, a.createdby, a.lastupdatedate, a.updatedby) " +
                "values ('?SC_ID1?', '?SC_METALSFILE?', '?SC_STONESFILE?', '?SC_TIMEZONE?', '?SC_FETCHSZMX?', '?SC_MILLIOFF?', '?SC_SECONDOFF?', '?SC_MINUTEOFF?', " +
                    "'?SC_HOUROFF?', '?SC_DAYOFF?', '?SC_MONTHOFF?', '?SC_YEAROFF?', sysdate, 'admin', sysdate, 'admin')";

        //SCC_ID1, SCC_WORKSTATIONID, SCC_TERMINALNUMBER, SCC_LOGLEVEL, SCC_TRACELEVEL, SCC_PRINTENABLED 
        public const string MERGE_PAWNSEC_STORECLIENTCONFIG =
            "merge into storeclientconfig a " +
            "using ( " +
                "select '?SCC_ID1?' as id " +
                "from dual ) b " +
            "on (a.id = b.id) " +
            "when matched then " +
                "update set a.workstationid = '?SCC_WORKSTATIONID?', a.terminalnumber = '?SCC_TERMINALNUMBER?', a.loglevel = '?SCC_LOGLEVEL?', a.tracelevel = '?SCC_TRACELEVEL?', " +
                "a.printenabled = '?SCC_PRINTENABLED?', a.lastupdatedate = sysdate, a.updatedby = 'admin' " +
            "when not matched then " +
                "insert (a.id, a.workstationid, a.terminalnumber, a.loglevel, a.tracelevel, a.printenabled, a.creationdate, a.createdby, a.lastupdatedate, a.updatedby) " +
                "values ('?SCC_ID1?', '?SCC_WORKSTATIONID?', '?SCC_TERMINALNUMBER?', '?SCC_LOGLEVEL?', '?SCC_TRACELEVEL?', '?SCC_PRINTENABLED?', sysdate, 'admin', sysdate, 'admin')";

        //GC_ID1, GC_VERSION, GC_BASETEMPLATEPATH, GC_BASELOGPATH, GC_BASEMEDIAPATH, GC_ADOBEREADERPATH, GC_CHOSTSCRIPTPATH, GC_PUBLICKEY
        public const string MERGE_PAWNSEC_GLOBALCONFIG =
            "merge into globalconfig a " +
            "using ( " +
                "select '?GC_ID1?' as id " +
                "from dual ) b " +
            "on (a.id = b.id) " +
            "when matched then " +
                "update set a.version = '?GC_VERSION?', a.basetemplatepath = '?GC_BASETEMPLATEPATH?', a.baselogpath = '?GC_BASELOGPATH?', a.basemediapath = '?GC_BASEMEDIAPATH?', " +
                "a.adobereaderpath = '?GC_ADOBEREADERPATH?', a.ghostscriptpath = '?GC_GHOSTSCRIPTPATH?', a.datapublickey = '?GC_PUBLICKEY?', " +
                "a.lastupdatedate = sysdate, a.updatedby = 'admin' " +
            "when not matched then " +
                "insert (a.id, a.version, a.basetemplatepath, a.baselogpath, a.basemediapath, a.adobereaderpath, a.ghostscriptpath, a.datapublickey, a.creationdate, a.createdby, a.lastupdatedate, a.updatedby) " +
                "values ('?GC_ID1?', '?GC_VERSION?', '?GC_BASETEMPLATEPATH?', '?GC_BASELOGPATH?', '?GC_BASEMEDIAPATH?', '?GC_ADOBEREADERPATH?', '?GC_GHOSTSCRIPTPATH?', " +
                    "'?GC_PUBLICKEY?', sysdate, 'admin', sysdate, 'admin')";

        //DS_ORACLEID1, DS_ORACLENAME1, DS_ORACLESERVER1, DS_ORACLEPORT1, DS_ORACLESCHEMA1, DS_ORACLEUSER1, DS_ORACLEPWD1, DS_ORACLEAUXINFO1, DS_ORACLEENABLED1
        public const string MERGE_PAWNSEC_DATABASESERVICE_ORACLE =
            "merge into databaseservice a " +
            "using ( " +
                "select '?DS_ORACLEID1?' as id, '?DS_ORACLENAME1?' as name, 'ORACLE' as servicetype, '?DS_ORACLESERVER1?' as server " +
                "from dual ) b " +
            "on (a.id = b.id or (a.name = b.name and a.servicetype = b.servicetype and a.server = b.server)) " +
            "when matched then " +
                "update set a.port = '?DS_ORACLEPORT1?', a.schema = '?DS_ORACLESCHEMA1?', a.dbuser = '?DS_ORACLEUSER1?', " +
                    "a.dbuserpwd = '?DS_ORACLEPWD1?', a.auxinfo = '?DS_ORACLEAUXINFO1?', a.enabled = '?DS_ORACLEENABLED1?', a.lastupdatedate = sysdate, a.updatedby = 'admin' " +
            "when not matched then " +
                "insert (a.id, a.name, a.servicetype, a.server, a.port, a.schema, a.dbuser, a.dbuserpwd, a.auxinfo, a.enabled, a.creationdate, a.createdby, a.lastupdatedate, a.updatedby) " +
                "values ('?DS_ORACLEID1?', '?DS_ORACLENAME1?', 'ORACLE', '?DS_ORACLESERVER1?', '?DS_ORACLEPORT1?', '?DS_ORACLESCHEMA1?', '?DS_ORACLEUSER1?', '?DS_ORACLEPWD1?', '?DS_ORACLEAUXINFO1?', " +
                    "'1', sysdate, 'admin', sysdate, 'admin')";

        //DS_LDAPID1, DS_LDAPNAME1, DS_LDAPSERVER1, DS_LDAPPORT1, DS_LDAPSCHEMA1, DS_LDAPUSER1, DS_LDAPPWD1, DS_LDAPAUXINFO1, DS_LDAPENABLED1
        public const string MERGE_PAWNSEC_DATABASESERVICE_LDAP =
            "merge into databaseservice a " +
            "using ( " +
                "select '?DS_LDAPID1?' as id, '?DS_LDAPNAME1?' as name, 'LDAP' as servicetype, '?DS_LDAPSERVER1?' as server " +
                "from dual ) b " +
            "on (a.id = b.id or (a.name = b.name and a.servicetype = b.servicetype and a.server = b.server)) " +
            "when matched then " +
                "update set a.port = '?DS_LDAPPORT1?', a.schema = '?DS_LDAPSCHEMA1?', a.dbuser = '?DS_LDAPUSER1?', a.dbuserpwd = '?DS_LDAPPWD1?', a.auxinfo = '?DS_LDAPAUXINFO1?', " +
                    "a.enabled = '1', a.lastupdatedate = sysdate, a.updatedby = 'admin' " +
            "when not matched then " +
                "insert (a.id, name, a.servicetype, a.server, a.port, a.schema, a.dbuser, a.dbuserpwd, a.auxinfo, a.enabled, a.creationdate, a.createdby, a.lastupdatedate, a.updatedby) " +
                "values ('?DS_LDAPID1?', '?DS_LDAPNAME1?', 'LDAP', '?DS_LDAPSERVER1?', '?DS_LDAPPORT1?', '?DS_LDAPSCHEMA1?', '?DS_LDAPUSER1?', '?DS_LDAPPWD1?', '?DS_LDAPAUXINFO1?', " +
                    "'1', sysdate, 'admin', sysdate, 'admin')";

        //DS_COUCHDBID1, DS_COUCHDBNAME1, DS_COUCHDBSERVER1, DS_COUCHDBPORT1, DS_COUCHDBSCHEMA1, DS_COUCHDBUSER1, DS_COUCHDBPWD1, DS_COUCHDBAUXINFO1, DS_COUCHDBENABLED1
        public const string MERGE_PAWNSEC_DATABASESERVICE_COUCHDB =
            "merge into databaseservice a " +
            "using ( " +
                "select '?DS_COUCHDBID1?' as id, '?DS_COUCHDBNAME1?' as name, 'COUCHDB' as servicetype, '?DS_COUCHDBSERVER1?' as server " +
                "from dual ) b " +
            "on (a.id = b.id or (a.name = b.name and a.servicetype = b.servicetype and a.server = b.server)) " +
            "when matched then " +
            "update set a.port = '?DS_COUCHDBPORT1?', a.schema = '?DS_COUCHDBSCHEMA1?', a.dbuser = '?DS_COUCHDBUSER1?', a.dbuserpwd = '?DS_COUCHDBPWD1?', a.auxinfo = '?DS_COUCHDBAUXINFO1?', " +
                "a.enabled = '1', a.lastupdatedate = sysdate, a.updatedby = 'admin' " +
            "when not matched then " +
                "insert (a.id, a.name, a.servicetype, a.server, a.port, a.schema, a.dbuser, a.dbuserpwd, a.auxinfo, a.enabled, a.creationdate, a.createdby, a.lastupdatedate, a.updatedby) " +
                "values ('?DS_COUCHDBID1?', '?DS_COUCHDBNAME1?', 'COUCHDB', '?DS_COUCHDBSERVER1?', '?DS_COUCHDBPORT1?', '?DS_COUCHDBSCHEMA1?', '?DS_COUCHDBUSER1?', '?DS_COUCHDBPWD1?', '?DS_COUCHDBAUXINFO1?', " +
                    "'1', sysdate, 'admin', sysdate, 'admin')";

        //ES_ID1, ES_NAME1, ES_SERVER1, ES_PORT1, ES_DOMAIN1, ES_URI1, ES_ENDPOINT1, ES_CLIENTBINDING1, ES_HTTPBINDING1
        public const string MERGE_PAWNSEC_ESBSERVICE =
            "merge into esbservice a " +
            "using ( " +
                "select '?ES_ID1?' as id, '?ES_NAME1?' as name " +
                "from dual ) b " +
            "on (a.id = b.id and a.name = b.name) " +
            "when matched then " +
                "update set a.server = '?ES_SERVER1?', a.port = '?ES_PORT1?', a.domain = '?ES_DOMAIN1?', a.uri = '?ES_URI1?', a.endpointname = '?ES_ENDPOINT1?', a.enabled = '1', " +
                    "a.clientbinding = '?ES_CLIENTBINDING1?', a.httpbinding = '?ES_HTTPBINDING1?', a.lastupdatedate = sysdate, a.updatedby = 'admin' " +
            "when not matched then " +
                "insert (a.id, a.name, a.server, a.port, a.domain, a.uri, a.endpointname, a.clientbinding, a.httpbinding, a.enabled, a.creationdate, a.createdby, a.lastupdatedate, a.updatedby) " +
                "values ('?ES_ID1?', '?ES_NAME1?', '?ES_SERVER1?', '?ES_PORT1?', '?ES_DOMAIN1?', '?ES_URI1?', " +
                    "'?ES_ENDPOINT1?', '?ES_CLIENTBINDING1?', " +
                    "'?ES_HTTPBINDING1?', '1', sysdate, 'admin', sysdate, 'admin')";

        //CSM_ID1, CR_ID1, SSI_ID1, SCC_ID1, SC_ID1
        public const string MERGE_PAWNSEC_CLIENTSTOREMAP =
            "merge into clientstoremap a " +
            "using ( " +
                "select '?CSM_ID1?' as id, '?CR_ID1?' as clientregistryid " +
                "from dual) b " +
            "on (a.id = b.id or a.clientregistryid = b.clientregistryid) " +
            "when matched then " +
                "update set a.storesiteid = '?SSI_ID1?', a.storeclientconfigid = '?SCC_ID1?', a.storeconfigid = '?SC_ID1?', a.lastupdatedate = sysdate, a.updatedby = 'admin' " +
            "when not matched then " +
                "insert (a.id, a.clientregistryid, a.storesiteid, a.storeclientconfigid, a.storeconfigid, a.creationdate, a.createdby, a.lastupdatedate, a.updatedby) " +
                "values ('?CSM_ID1?', '?CR_ID1?', '?SSI_ID1?', '?SCC_ID1?', '?SC_ID1?', sysdate, 'admin', sysdate, 'admin')";

        //DSSM_ID1, DS_ID1, SC_ID1
        public const string MERGE_PAWNSEC_DATABASESERVICESTOREMAP =
            "merge into databaseservicestoremap a " +
            "using ( " +
                "select '?DSSM_ID1?' as id, '?DS_ID1?' as databaseserviceid, '?SC_ID1?' as storeconfigid " +
                "from dual ) b " +
            "on (a.id = b.id or (a.databaseserviceid = b.databaseserviceid and a.storeconfigid = b.storeconfigid)) " +
            "when matched then " +
                "update set a.databaseserviceid = '?DS_ID1?', a.storeconfigid = '?SC_ID1?', a.lastupdatedate = sysdate, a.updatedby = 'admin' " +
            "when not matched then " +
                "insert (a.id, a.databaseserviceid, a.storeconfigid, a.creationdate, a.createdby, a.lastupdatedate, a.updatedby) " +
                "values ('?DSSM_ID1?', '?DS_ID1?', '?SC_ID1?', sysdate, 'admin', sysdate, 'admin')";

        //ES_ID1, SC_ID1, ESSM_ID1
        public const string MERGE_PAWNSEC_ESBSERVICESERVICESTOREMAP =
            "merge into esbservicestoremap a " +
            "using ( " +
                "select '?ESSM_ID1?' as id, '?ES_ID1?' as esbserviceid, '?SC_ID1?' as storeconfigid " +
                "from dual) b " +
            "on (a.id = b.id or (a.esbserviceid = b.esbserviceid and a.storeconfigid = b.storeconfigid)) " +            
            "when matched then " +
                "update set a.esbserviceid = '?ES_ID1?', a.storeconfigid = '?SC_ID1?', a.lastupdatedate = sysdate, a.updatedby = 'admin' " +
            "when not matched then " +
                "insert (a.id, a.esbserviceid, a.storeconfigid, a.creationdate, a.createdby, a.lastupdatedate, a.updatedby) " +
                "values ('?ESSM_ID1?', '?ES_ID1?', '?SC_ID1?', sysdate, 'admin', sysdate, 'admin')";


        ///////////////////////////////////////////////////////////////////////////
        //CCSOWNER DELETE SQL CODE 

        //WP_ID
        public const string DELETE_CCSOWNER_WORKSTATIONPERIPHERALS =
                "delete from ccsowner.pawnworkstationperipherals where storeperipheralid = '?WP_ID?'";

        //P_ID
        public const string DELETE_CCSOWNER_PERIPHERAL =
                    "delete from ccsowner.peripherals where peripheralid = '?P_ID?'";

        ///////////////////////////////////////////////////////////////////////////
        //CDOWNER DELETE SQL CODE 
        public const string DELETE_CDOWNER_WORKSTATION_BY_ID =
                "delete from cdowner.cd_workstation where id = '?W_ID?'";

        public const string DELETE_CDCONNECTEDCDUSER_BY_WORKSTATIONID =
                "delete from cdowner.cd_connectedcduser ccdu where ccdu.workstationid = '?W_ID?'";

        public const string DELETE_CDCONNECTEDCDUSER_BY_STORE =
                "delete from cdowner.cd_connectedcduser ccdu where ccdu.workstationid in ( " +
                "select id from cdowner.cd_workstation where branchid = '?STO_ID?')";

    }
}
