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
            "from globalconfig";
    }
}
