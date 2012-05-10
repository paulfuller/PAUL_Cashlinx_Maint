/*
 *RB:  added line below for BZ 485 for backup functionality of the tool to pickup the Logpath.  Previously defaulting to hard coded value 
*/
using Common.Libraries.Forms.Components;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Application;
using Common.Controllers.Database.DataAccessLayer;
using Common.Libraries.Objects.Config;
using Common.Libraries.Utility;
using Common.Libraries.Utility.String;
using Common.Libraries.Utility.Type;
using MessageBox = System.Windows.MessageBox;

namespace DSTRViewer
{
    public static class PawnStoreProcedures
    {
        public const string CCSOWNER = "CCSOWNER";
        public const string PAWNSEC = "PAWNSEC";


        public static bool GetGlobalConfig(
            ref DataAccessTools dA,
            ref PawnSecVO pwnSec,
            out string decryptKey)
        {
            //Set changed public key to false
            decryptKey = string.Empty;

            //Get global configuration
            DataReturnSet globalConfig;
            if (!DataAccessService.ExecuteQuery(false,
                                                PawnStoreSetupQueries.SELECT_PAWNSEC_GLOBAL,
                                                "globalconfig",
                                                PAWNSEC,
                                                out globalConfig,
                                                ref dA))
            {
                MessageBox.Show("Could not query global configuration from pawn sec",
                                "Alert");
                return (false);
            }

            if (globalConfig == null || globalConfig.NumberRows <= 0)
            {
                MessageBox.Show("Invalid or unexpected number of global config rows returned",
                                "Alert");
                return (false);
            }

            //Retrieve the data row
            DataReturnSetRow globDr;
            if (!globalConfig.GetRow(0, out globDr))
            {
                MessageBox.Show("Could not retrieve first row of the global config data table",
                                "Alert");
                return (false);
            }

            pwnSec.GlobalConfiguration.Version =
                    Utilities.GetStringValue(globDr.GetData("APPVERSIONID"));
            pwnSec.GlobalConfiguration.BaseTemplatePath =
                    Utilities.GetStringValue(globDr.GetData("TEMPLATEPATH"));
            //BZ 485: RB:  The following line needs to be added to pickup the base log path and keep it from getting hard coded.
            pwnSec.GlobalConfiguration.BaseLogPath =
                    Utilities.GetStringValue(globDr.GetData("LOGPATH"));
            pwnSec.GlobalConfiguration.BaseMediaPath =
                    Utilities.GetStringValue(globDr.GetData("MEDIAPATH"));
            pwnSec.GlobalConfiguration.AdobeReaderPath =
                    Utilities.GetStringValue(globDr.GetData("ADOBEPATH"));
            pwnSec.GlobalConfiguration.GhostScriptPath =
                    Utilities.GetStringValue(globDr.GetData("GHOSTPATH"));
            //Decrypt the global public key
            var globPubKeyEnc = Utilities.GetStringValue(globDr.GetData("PUBLICKEY"));
            var globPubKeyDec =
                    StringUtilities.Decrypt(globPubKeyEnc,
                                            Common.Properties.Resources.PrivateKey,
                                            true);
            //Set the global public key if we do not have one
            if (string.IsNullOrEmpty(pwnSec.GlobalConfiguration.DataPublicKey))
            {
                pwnSec.GlobalConfiguration.DataPublicKey = globPubKeyDec;
            }

            //If we made it here, we were successful in acquiring global configuration
            decryptKey = globPubKeyDec + Common.Properties.Resources.PrivateKey;
            return (true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dA"></param>
        /// <param name="pwnSec"></param>
        /// <param name="decryptKey"> </param>
        /// <returns></returns>
        public static bool GetAllPawnSecData(
            ref DataAccessTools dA,            
            ref PawnSecVO pwnSec,
            out string decryptKey)
        {
            decryptKey = string.Empty;
            if (pwnSec == null)
            {
                MessageBox.Show("Data passed into method is invalid",
                                "Alert");
                return (false);
            }
            //Get global configuration
            if (!GetGlobalConfig(ref dA, ref pwnSec, out decryptKey))
            {
                MessageBox.Show("Could not acquire global configuration", "Alert");
            }

            //Setup store config as the single parameter needed for the next few queries
            var ptStoCfg = new PairType<string, string>[1];
            ptStoCfg[0] = new PairType<string, string>("STORECONFIGID", "1");

            //Load db services and map
            DataReturnSet dbServices;
            if (!DataAccessService.ExecuteVariableQuery(false,
                                                        PawnStoreSetupQueries.SELECT_PAWNSEC_DB,
                                                        "databaseservice",
                                                        PAWNSEC,
                                                        out dbServices,
                                                        ref dA,
                                                        ptStoCfg))
            {
                MessageBox.Show("Could not execute query against the database service table",
                                "Alert");
                return (false);
            }

            if (dbServices == null || dbServices.NumberRows <= 0)
            {
                MessageBox.Show("Could not find any database services in the pawnsec database",
                                "Alert");
                return (false);
            }

            for (int i = 0; i < dbServices.NumberRows; ++i)
            {
                DataReturnSetRow dR;
                if (!dbServices.GetRow(i, out dR))
                    continue;

                var enabFlag = Utilities.GetStringValue(dR.GetData("ENABLED"));
                var enabFlagBool = (!string.IsNullOrEmpty(enabFlag) && enabFlag.Equals("1"));
                var userName = Utilities.GetStringValue(dR.GetData("DBUSER"));
                var userPwd = Utilities.GetStringValue(dR.GetData("DBUSERPWD"));

                var dbServiceVo = new DatabaseServiceVO(userName, userPwd, enabFlagBool)
                {
                    Id = Utilities.GetULongValue(dR.GetData("ID")),
                    Name = Utilities.GetStringValue(dR.GetData("NAME")),
                    Server = Utilities.GetStringValue(dR.GetData("SERVER")),
                    Port = Utilities.GetStringValue(dR.GetData("PORT")),
                    Schema = Utilities.GetStringValue(dR.GetData("SCHEMA")),
                    AuxInfo = Utilities.GetStringValue(dR.GetData("AUXINFO")),
                    ServiceType = Utilities.GetStringValue(dR.GetData("SERVICETYPE"))
                };
                var dbMapVo = new PawnSecVO.DatabaseServiceStoreMapVO
                {
                    Id = Utilities.GetULongValue(dR.GetData("DATABASEMAPID")),
                    StoreConfigId =
                            Utilities.GetULongValue(dR.GetData("DATABASEMAPSTORECONFIGID")),
                    DatabaseServiceId = dbServiceVo.Id
                };

                //Add vo objects to lists
                pwnSec.DatabaseServiceList.Add(dbServiceVo);
                pwnSec.DatabaseServiceMapList.Add(dbMapVo);
            }

            return (true);
        }
    }
}
