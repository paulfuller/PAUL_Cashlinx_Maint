/*
 *RB:  added line below for BZ 485 for backup functionality of the tool to pickup the Logpath.  Previously defaulting to hard coded value 
*/
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Common.Controllers.Database.DataAccessLayer;
using Common.Controllers.Database.Oracle;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Authorization;
using Common.Libraries.Objects.Config;
using Common.Libraries.Objects.Mapper;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Collection;
using Common.Libraries.Utility.String;
using Common.Libraries.Utility.Type;
using Common.Libraries.Utility.Logger;

namespace PawnStoreSetupTool.Data
{
    public static class PawnStoreProcedures
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dA"></param>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static bool GenerateGUID(
            ref DataAccessTools dA,
            out string guid)
        {
            guid = string.Empty;
            var oraParams = new List<OracleProcParam>
                            {
                                    new OracleProcParam("o_guid",
                                                        DataTypeConstants.PawnDataType.STRING,
                                                        DBNull.Value,
                                                        ParameterDirection.Output,
                                                        256)
                            };

            string errCode, errText;
            DataSetOutput dataSetOutput;
            bool retVal = DataAccessService.ExecuteStoredProc(
                "ccsowner",
                "pawn_gen_procs",
                "generate_guid", oraParams, null, null,
                "o_return_code", "o_return_text", PawnStoreSetupForm.CCSOWNER,
                out errCode, out errText, out dataSetOutput, ref dA);

            if (retVal)
            {
                DataTable outputTable;
                if (dataSetOutput.GetOutputTable(out outputTable))
                {
                    DataRow dR = outputTable.Rows[0];
                    if (dR != null && dR.ItemArray.Length > 0)
                    {
                        object guidObj = dR[1];
                        if (guidObj != null)
                        {
                            guid = guidObj.ToString();
                            return (true);
                        }
                    }
                }
            }

            return (false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dA"></param>
        /// <param name="idColumnName"></param>
        /// <param name="tableName"></param>
        /// <param name="nextId"></param>
        /// <returns></returns>
        public static bool GetNextId(
            ref DataAccessTools dA,
            string idColumnName,
            string tableName,
            out ulong nextId)
        {
            nextId = 1;

            if (string.IsNullOrEmpty(idColumnName) ||
                string.IsNullOrEmpty(tableName))
            {
                return (false);
            }
            //ID_COLUMN_NAME, TABLE_NAME
            var parmArr = new PairType<string, string>[2];
            parmArr[0] = new PairType<string, string>("ID_COLUMN_NAME", idColumnName);
            parmArr[1] = new PairType<string, string>("TABLE_NAME", tableName);
            DataReturnSet dataReturnSet;
            //NOTE: Is keyed off of the PAWNSEC database connection, not the CCSOWNER connection
            if (!DataAccessService.ExecuteVariableQuery(false,
                    PawnStoreSetupQueries.SELECT_NEXTID, tableName,
                    PawnStoreSetupForm.PAWNSEC, out dataReturnSet, ref dA, parmArr))
            {
                dA.log(LogLevel.ERROR, "Could not acquire the next valid id from the {0} column of the {1} table",
                    idColumnName, tableName);
                return (false);
            }

            if (dataReturnSet == null || dataReturnSet.NumberRows < 0)
            {
                dA.log(LogLevel.ERROR, "Data set returned for next id was empty");
                return (false);
            }

            ulong ulongVal = 0;
            for (int i = 0; i < dataReturnSet.NumberRows; ++i)
            {
                DataReturnSetRow dR;
                if (!dataReturnSet.GetRow(i, out dR)) continue;

                string nextIdStr = Utilities.GetStringValue(dR.GetData("ID"));
                if (!string.IsNullOrEmpty(nextIdStr))
                {
                    if (ulong.TryParse(nextIdStr, out ulongVal))
                    {
                        break;
                    }
                }
            }
            if (ulongVal >= 0)
            {
                nextId = ulongVal + 1;
                return (true);
            }

            return (false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dA"></param>
        /// <param name="idColumnName"></param>
        /// <param name="tableName"></param>
        /// <param name="nextId"></param>
        /// <returns></returns>
        public static bool GetNextIdStr(
            ref DataAccessTools dA,
            string idColumnName,
            string tableName,
            out ulong nextId)
        {
            nextId = 1;

            if (string.IsNullOrEmpty(idColumnName) ||
                string.IsNullOrEmpty(tableName))
            {
                return (false);
            }
            //ID_COLUMN_NAME, TABLE_NAME
            var parmArr = new PairType<string, string>[2];
            parmArr[0] = new PairType<string, string>("ID_COLUMN_NAME", idColumnName);
            parmArr[1] = new PairType<string, string>("TABLE_NAME", tableName);
            DataReturnSet dataReturnSet;
            //NOTE: Is keyed off of the CCSOWNER database connection, not the PAWNSEC connection
            if (!DataAccessService.ExecuteVariableQuery(false,
                    PawnStoreSetupQueries.SELECT_NEXTID_STR, tableName,
                    PawnStoreSetupForm.CCSOWNER, out dataReturnSet, ref dA, parmArr))
            {
                dA.log(LogLevel.ERROR, "Could not acquire the next valid id from the {0} column of the {1} table",
                    idColumnName, tableName);
                return (false);
            }

            if (dataReturnSet == null || dataReturnSet.NumberRows < 0)
            {
                dA.log(LogLevel.ERROR, "Data set returned for next id was empty");
                return (false);
            }

            ulong ulongVal = 0;
            for (int i = 0; i < dataReturnSet.NumberRows; ++i)
            {
                DataReturnSetRow dR;
                if (!dataReturnSet.GetRow(i, out dR))
                    continue;

                string nextIdStr = Utilities.GetStringValue(dR.GetData("ID"));
                if (!string.IsNullOrEmpty(nextIdStr))
                {
                    if (ulong.TryParse(nextIdStr, out ulongVal))
                    {
                        break;
                    }
                }
            }
            if (ulongVal >= 0)
            {
                nextId = ulongVal + 1;
                return (true);
            }

            return (false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="wksTable"></param>
        /// <param name="workstations"></param>
        public static void ProcessWorkstationTable(string storeId, DataTable wksTable, out List<WorkstationVO> workstations)
        {
            workstations = null;

            if (wksTable == null || !wksTable.IsInitialized || wksTable.Rows == null ||
                wksTable.Rows.Count <= 0)
            {
                return;
            }
            workstations = new List<WorkstationVO>(1);
            foreach (DataRow dR in wksTable.Rows)
            {
                if (dR == null) continue;
                var wks = new WorkstationVO(storeId)
                {
                    Id = Utilities.GetStringValue(dR["id"]),
                    Name = Utilities.GetStringValue(dR["name"]),
                    StoreNumber = Utilities.GetStringValue(dR["storenumber"])
                };
                workstations.Add(wks);
            }
        }

        public static bool IsStoreSiteInfoPopulated(
            ref DataAccessTools dA,
            string StoreNum)
        {
            if (dA == null) return false;

            string query = "SELECT   ID " +
                            " FROM   STORESITEINFO " +
                            " WHERE   STORENUMBER = '{0}'";

            DataReturnSet returnSet;
            if (!DataAccessService.ExecuteQuery(false,
                                           string.Format(query, StoreNum),
                                           "storesiteinfo",
                                           PawnStoreSetupForm.PAWNSEC,
                                           out returnSet,
                                           ref dA))
            {
                MessageBox.Show("Could not retrieve storesiteinfo for the Store -->" + StoreNum, PawnStoreSetupForm.SETUPALERT_TXT);
                return false;
            }
            if (returnSet == null || returnSet.NumberRows <= 0)
            {
                return false;
            }
            else
                return true;
        }

        public static bool IsNextNumPopulated(
        ref DataAccessTools dA,
        string StoreNum, string nextNumType)
        {
            if (dA == null) return false;

            string query = "SELECT   NEXTNUM_ID " +
                            " FROM   CCSOWNER.NEXTNUM " +
                            " WHERE   STORENUMBER = '{0}' AND NEXTNUM_TYPE = '{1}'";

            DataReturnSet returnSet;
            if (!DataAccessService.ExecuteQuery(false,
                                           string.Format(query, StoreNum, nextNumType),
                                           "NEXTNUM",
                                           PawnStoreSetupForm.CCSOWNER,
                                           out returnSet,
                                           ref dA))
            {
                MessageBox.Show("Could not retrieve CCSOWNER.NEXTNUM for the Store -->" + StoreNum, PawnStoreSetupForm.SETUPALERT_TXT);
                return false;
            }
            if (returnSet == null || returnSet.NumberRows <= 0)
            {
                return false;
            }
            else
                return true;
        }

        public static void RetrieveSafe(
        ref DataAccessTools dA,
        string safeName,
        ref string safeId, ref string registeredUserId)
        {
            if (dA == null) return;

            string query = "SELECT   ID, REGISTERUSERID " +
                            " FROM   CDOWNER.CD_CASHDRAWER " +
                              " WHERE   NAME ='{0}'";

            DataReturnSet returnSet;
            if (!DataAccessService.ExecuteQuery(false,
                                           string.Format(query, safeName),
                                           "CD_CASHDRAWER",
                                           PawnStoreSetupForm.CCSOWNER,
                                           out returnSet,
                                           ref dA))
            {
                MessageBox.Show("Could not retrieve Safe for -->" + safeName, PawnStoreSetupForm.SETUPALERT_TXT);
                return;
            }
            if (returnSet == null || returnSet.NumberRows <= 0)
            {
                MessageBox.Show("Safe not available for the Store-->" + safeName, PawnStoreSetupForm.SETUPALERT_TXT);
                return;
            }

            //Create model types list
            for (int j = 0; j < returnSet.NumberRows; ++j)
            {
                DataReturnSetRow dRow;
                if (!returnSet.GetRow(j, out dRow)) continue;

                safeId = Utilities.GetStringValue(dRow.GetData("ID"));
                registeredUserId = Utilities.GetStringValue(dRow.GetData("REGISTERUSERID"));
            }
        }

        public static bool IsCashdrawerUserExist(
        ref DataAccessTools dA,
        string registeredUserId)
        {
            if (dA == null) return false;

            string query = "SELECT   ID " +
                            " FROM   CDOWNER.CD_CASHDRAWERUSER " +
                            " WHERE   ID = '{0}'";

            DataReturnSet returnSet;
            if (!DataAccessService.ExecuteQuery(false,
                                           string.Format(query, registeredUserId),
                                           "CD_CASHDRAWERUSER",
                                           PawnStoreSetupForm.CCSOWNER,
                                           out returnSet,
                                           ref dA))
            {
                MessageBox.Show("Could not retrieve cashdraweruser for the user-->" + registeredUserId, PawnStoreSetupForm.SETUPALERT_TXT);
                return false;
            }
            if (returnSet == null || returnSet.NumberRows <= 0)
            {
                return false;
            }
            else
                return true;
        }
        public static bool IsStoreTypeBootStraped(
            ref DataAccessTools dA,
            string StoreNum)
        {
            if (dA == null) return false;

            string query = "SELECT   ID " +
                            " FROM   STORETYPE " +
                            " WHERE   STORENUMBER = '{0}'";

            DataReturnSet returnSet;
            if (!DataAccessService.ExecuteQuery(false,
                                           string.Format(query, StoreNum),
                                           "storetype",
                                           PawnStoreSetupForm.CCSOWNER,
                                           out returnSet,
                                           ref dA))
            {
                MessageBox.Show("Could not retrieve storetypes for the Store -->" + StoreNum, PawnStoreSetupForm.SETUPALERT_TXT);
                return false;
            }
            if (returnSet == null || returnSet.NumberRows <= 0)
            {
                return false;
            }
            else
                return true;
        }

        public static void RetrieveNextNum(
        ref DataAccessTools dA,
        string StoreNum,
        ref List<string> nextNums)
        {
            if (dA == null) return;

            string query = "SELECT   NEXTNUM_TYPE " +
                            " FROM   CCSOWNER.NEXTNUM " +
                              " WHERE   STORENUMBER ='{0}'";

            DataReturnSet returnSet;
            if (!DataAccessService.ExecuteQuery(false,
                                           string.Format(query, StoreNum),
                                           "NextNum",
                                           PawnStoreSetupForm.CCSOWNER,
                                           out returnSet,
                                           ref dA))
            {
                MessageBox.Show("Could not retrieve NextNum for the Store -->" + StoreNum, PawnStoreSetupForm.SETUPALERT_TXT);
                return;
            }
            if (returnSet == null || returnSet.NumberRows <= 0)
            {
                MessageBox.Show("No NextNum records available for the Store-->" + StoreNum, PawnStoreSetupForm.SETUPALERT_TXT);
                return;
            }

            //Create model types list
            for (int j = 0; j < returnSet.NumberRows; ++j)
            {
                DataReturnSetRow dRow;
                if (!returnSet.GetRow(j, out dRow)) continue;

                nextNums.Add(Utilities.GetStringValue(dRow.GetData("NEXTNUM_TYPE")));
            }
        }

        public static void RetrieveSecurityMasks(
            ref DataAccessTools dA,
            string principalid,
            ref List<PairType<string, string>> masks)
        {
            if (dA == null) return;

            string query = "SELECT OBJECTID, SECURITYMASK " +
                            " FROM   ccsowner.ace " +
                            " WHERE   objecttype = 21 and principaltype = 3 " +
                            "and principalid = {0}";

            DataReturnSet returnSet;
            if (!DataAccessService.ExecuteQuery(false,
                                           string.Format(query, principalid),
                                           "ace",
                                           PawnStoreSetupForm.CCSOWNER,
                                           out returnSet,
                                           ref dA))
            {
                MessageBox.Show("Could not retrieve masks for the principle id -->" + principalid, PawnStoreSetupForm.SETUPALERT_TXT);
                return;
            }
            if (returnSet == null || returnSet.NumberRows <= 0)
            {
                MessageBox.Show("No masks available for the principalid-->" + principalid, PawnStoreSetupForm.SETUPALERT_TXT);
                return;
            }

            //Create model types list
            for (int j = 0; j < returnSet.NumberRows; ++j)
            {
                DataReturnSetRow dRow;
                if (!returnSet.GetRow(j, out dRow)) continue;
                string objectid = Utilities.GetStringValue(dRow.GetData("OBJECTID"));
                string securitymask = Utilities.GetStringValue(dRow.GetData("SECURITYMASK"));
                PairType<string, string> pType = new PairType<string, string>(objectid, securitymask);
                masks.Add(pType);
            }
        }


        public static void RetrieveSecurityResources(
            ref DataAccessTools dA,
            string principalid,
            ref List<PairType<string, string>> resources)
        {
            if (dA == null) return;

            string query = "SELECT rolelimits.LIMIT as LIMIT, rolelimits.prodofferingid as PRODOFFERINGID " +
                            "FROM ccsowner.ace LEFT OUTER JOIN ccsowner.rolelimits ON ace.principalid = rolelimits.roleid " +
                            " LEFT OUTER JOIN ccsowner.productofferings ON rolelimits.prodofferingid = " +
                            " productofferings.prodofferingid WHERE ace.objecttype = 21 " +
                            " AND ace.principaltype = 3 " +
                            " AND ace.objectid = productofferings.serviceoffering " +
                            " and ace.principalid = {0}";

            DataReturnSet returnSet;
            if (!DataAccessService.ExecuteQuery(false,
                                           string.Format(query, principalid),
                                           "ace",
                                           PawnStoreSetupForm.CCSOWNER,
                                           out returnSet,
                                           ref dA))
            {
                MessageBox.Show("Could not retrieve resources for the principle id -->" + principalid, PawnStoreSetupForm.SETUPALERT_TXT);
                return;
            }
            if (returnSet == null || returnSet.NumberRows <= 0)
            {
                MessageBox.Show("No resources available for the principalid-->" + principalid, PawnStoreSetupForm.SETUPALERT_TXT);
                return;
            }

            //Create model types list
            for (int j = 0; j < returnSet.NumberRows; ++j)
            {
                DataReturnSetRow dRow;
                if (!returnSet.GetRow(j, out dRow)) continue;
                string limit = Utilities.GetStringValue(dRow.GetData("LIMIT"));
                string prodofferingid = Utilities.GetStringValue(dRow.GetData("PRODOFFERINGID"));
                PairType<string, string> pType = new PairType<string, string>(limit, prodofferingid);
                resources.Add(pType);
            }
        }

        public static void RetrieveStoreProducts(
            ref DataAccessTools dA,
            string StoreNum,
            ref List<PairType<string, string>> storeProdcuts)
        {
            if (dA == null) return;

            string query = "SELECT   PRODUCT_ID, " +
                            "STORE_PRODUCT_MENU_ID " +
                            " FROM   storeproducts " +
                            " WHERE   STORE_TYPE_ID = (SELECT   id" +
                               " FROM   storetype" +
                              " WHERE   storenumber ='{0}')";

            DataReturnSet returnSet;
            if (!DataAccessService.ExecuteQuery(false,
                                           string.Format(query, StoreNum),
                                           "storeproducts",
                                           PawnStoreSetupForm.CCSOWNER,
                                           out returnSet,
                                           ref dA))
            {
                MessageBox.Show("Could not retrieve store products for the Store -->" + StoreNum, PawnStoreSetupForm.SETUPALERT_TXT);
                return;
            }
            if (returnSet == null || returnSet.NumberRows <= 0)
            {
                MessageBox.Show("No Store Products available for the Store-->" + StoreNum, PawnStoreSetupForm.SETUPALERT_TXT);
                return;
            }

            //Create model types list
            for (int j = 0; j < returnSet.NumberRows; ++j)
            {
                DataReturnSetRow dRow;
                if (!returnSet.GetRow(j, out dRow)) continue;
                string productId = Utilities.GetStringValue(dRow.GetData("PRODUCT_ID"));
                string menuId = Utilities.GetStringValue(dRow.GetData("STORE_PRODUCT_MENU_ID"));
                PairType<string, string> pType = new PairType<string, string>(productId, menuId);
                storeProdcuts.Add(pType);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dA"></param>
        /// <param name="peripheralTypes"></param>
        /// <param name="peripheralModels"></param>
        public static void AcquirePeripheralTypeModel(
            ref DataAccessTools dA,
            ref List<PeripheralTypeVO> peripheralTypes,
            ref List<PeripheralModelVO> peripheralModels)
        {
            if (dA == null) return;
            DataReturnSet returnSet;
            if (!DataAccessService.ExecuteQuery(false,
                                           "select * from ccsowner.model",
                                           "model",
                                           PawnStoreSetupForm.CCSOWNER,
                                           out returnSet,
                                           ref dA))
            {
                MessageBox.Show("Could not acquire peripheral models", PawnStoreSetupForm.SETUPALERT_TXT);
                return;
            }
            if (returnSet == null || returnSet.NumberRows <= 0)
            {
                MessageBox.Show("No peripheral models returned", PawnStoreSetupForm.SETUPALERT_TXT);
                return;
            }

            //Create model types list
            for (int j = 0; j < returnSet.NumberRows; ++j)
            {
                DataReturnSetRow dRow;
                if (!returnSet.GetRow(j, out dRow)) continue;
                string modelId = Utilities.GetStringValue(dRow.GetData("MODELID"));
                if (string.IsNullOrEmpty(modelId)) continue;

                //Before adding see if this model id already exists
                if (peripheralModels.Exists(x => x.Id.Equals(modelId)))
                {
                    continue;
                }

                string peripheralTypeId = Utilities.GetStringValue(dRow.GetData("PERIPHERALTYPEID"));
                string modelName = Utilities.GetStringValue(dRow.GetData("MODELNAME"));
                string makeName = Utilities.GetStringValue(dRow.GetData("PERIPHERALMAKE"));
                string desc = Utilities.GetStringValue(dRow.GetData("MODELDESC"));


                var pMod = new PeripheralModelVO
                {
                    Id = modelId,
                    PeripheralTypeId = peripheralTypeId,
                    Name = modelName,
                    Desc = desc,
                    Make = makeName
                };
                peripheralModels.Add(pMod);
            }

            DataReturnSet perReturnSet;
            if (!DataAccessService.ExecuteQuery(false,
                                           "select peripheraltypeid, peripheraldesc from ccsowner.peripheraltype",
                                           "peripheraltype",
                                           PawnStoreSetupForm.CCSOWNER,
                                           out perReturnSet,
                                           ref dA))
            {
                MessageBox.Show("Could not acquire peripheral types", PawnStoreSetupForm.SETUPALERT_TXT);
                return;
            }
            if (perReturnSet == null || perReturnSet.NumberRows <= 0)
            {
                MessageBox.Show("No peripheral types returned", PawnStoreSetupForm.SETUPALERT_TXT);
                return;
            }

            //Create peripheral types list
            for (int j = 0; j < perReturnSet.NumberRows; ++j)
            {
                DataReturnSetRow dRow;
                if (!perReturnSet.GetRow(j, out dRow)) continue;
                string peripheralTypeId = Utilities.GetStringValue(dRow.GetData("PERIPHERALTYPEID"));
                if (string.IsNullOrEmpty(peripheralTypeId)) continue;

                //Check to see if the peripheral type already exists
                if (peripheralTypes.Exists(x => x.PeripheralTypeId.Equals(peripheralTypeId)))
                {
                    continue;
                }
                string peripheralDesc = Utilities.GetStringValue(dRow.GetData("PERIPHERALDESC"));
                var pTypeVO = new PeripheralTypeVO
                {
                    PeripheralTypeId = peripheralTypeId,
                    PeripheralTypeName = peripheralDesc
                };
                foreach (var pModel in peripheralModels)
                {
                    if (pModel == null) continue;
                    if (!pModel.PeripheralTypeId.Equals(peripheralTypeId, StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }
                    pTypeVO.PeripheralModel = new PeripheralModelVO();
                    var newPModel = pTypeVO.PeripheralModel;
                    newPModel.Id = pModel.Id;
                    newPModel.Make = pModel.Make;
                    newPModel.Name = pModel.Name;
                    newPModel.PeripheralTypeId = pModel.PeripheralTypeId;
                    newPModel.Desc = pModel.Desc;
                    break;
                }
                peripheralTypes.Add(pTypeVO);
            }
        }

        public static void AcquirePeripheralData(
            ref DataAccessTools dA,
            string storeId,
            List<PeripheralTypeVO> pTypes,
            ref List<PeripheralVO> peripherals)
        {
            if (dA == null || string.IsNullOrEmpty(storeId) || CollectionUtilities.isEmpty(pTypes)) return;

            DataReturnSet data;
            //Retrieve available peripherals in the store
            if (!DataAccessService.ExecuteQuery(false,
                                                string.Format(
                                                "select * from ccsowner.peripherals where storeid = '{0}'", storeId),
                                                "peripherals",
                                                PawnStoreSetupForm.CCSOWNER,
                                                out data,
                                                ref dA))
            {
                MessageBox.Show("Error occurred while acquiring peripherals.", PawnStoreSetupForm.SETUPALERT_TXT);
                return;
            }


            if (data == null || data.NumberRows <= 0)
            {
                MessageBox.Show("No peripherals returned.", PawnStoreSetupForm.SETUPALERT_TXT);
                return;
            }

            //Load the peripheral types for storage in the peripheral
            List<string> perTypeNames = new List<string>();
            foreach (var s in pTypes)
            {
                if (s == null) continue;
                perTypeNames.Add(s.PeripheralTypeName);
            }

            //Iterate through data rows of peripherals
            for (int j = 0; j < data.NumberRows; ++j)
            {
                DataReturnSetRow dRow;
                if (!data.GetRow(j, out dRow)) continue;
                string id = Utilities.GetStringValue(dRow.GetData("PERIPHERALID"));
                if (string.IsNullOrEmpty(id)) continue;

                //Ensure that we do not already have this peripheral loaded
                if (peripherals.Exists(x => x.Id.Equals(id)))
                {
                    continue;
                }
                string name = Utilities.GetStringValue(dRow.GetData("PERIPHERALNAME"));
                string ptId = Utilities.GetStringValue(dRow.GetData("PERIPHERALTYPEID"));
                string ipAddr = Utilities.GetStringValue(dRow.GetData("IPADDRESS"));
                string portNum = Utilities.GetStringValue(dRow.GetData("PORTNUMBER"));
                int prefOrder = Utilities.GetIntegerValue(dRow.GetData("PERIPHERAL_PREF_ORDER"));

                var pvo = new PeripheralVO
                {
                    Name = name,
                    IPAddress = ipAddr,
                    Port = portNum,
                    PrefOrder = prefOrder,
                    StoreId = storeId,
                    PeriphType = new PeripheralTypeVO()
                };
                pvo.SetId(id);
                pvo.SetPeripheralTypeId(ptId);
                pvo.SetPeripheralTypeNames(perTypeNames);
                pvo.CalculatePeripheralTypeFromTypeId(pTypes);
                pvo.CalculateLogicalTypeFromPeripheralType();

                //Get peripheral type vo););
                /*foreach(var ptvo in pTypes)
                {
                    if (ptvo == null) continue;
                    if (ptvo.PeripheralTypeId.Equals(ptId, StringComparison.OrdinalIgnoreCase))
                    {
                        pvo.PeriphType = ptvo;
                        //pvo.UpdatePeripheral();
                        break;
                    }
                }*/

                //Add peripheral to list
                peripherals.Add(pvo);
            }

        }

        public static void GetWorkstationPeripheralMapping(
            ref DataAccessTools dA,
            string storeId,
            List<WorkstationVO> workstations,
            List<PeripheralVO> peripherals,
            ref Dictionary<string, List<PairType<string, PeripheralVO>>> wkspMapping)
        {
            DataReturnSet wkData;
            //Retrieve workstation peripheral mappings
            if (!DataAccessService.ExecuteQuery(false,
                string.Format("select STOREPERIPHERALID, PERIPHERALID, WORKSTATIONID from ccsowner.pawnworkstationperipherals " +
                "where peripheralid in (select peripheralid from " +
                "ccsowner.peripherals where storeid = '{0}')", storeId),
                "pawnworkstationperipherals",
                "ccsowner",
                out wkData,
                ref dA))
            {
                MessageBox.Show("Error occurred while acquiring peripherals!", PawnStoreSetupForm.SETUPALERT_TXT);
                return;
            }

            if (wkData == null || wkData.NumberRows <= 0)
            {
                MessageBox.Show("No workstation peripheral mappings returned.", PawnStoreSetupForm.SETUPALERT_TXT);
                return;
            }

            //Create empty mapping
            wkspMapping.Clear();

            //Iterate through the results
            for (int j = 0; j < wkData.NumberRows; ++j)
            {
                DataReturnSetRow curRow;
                if (!wkData.GetRow(j, out curRow)) continue;
                string sPerId = Utilities.GetStringValue(curRow.GetData("STOREPERIPHERALID"));
                string wksId = Utilities.GetStringValue(curRow.GetData("WORKSTATIONID"));
                string perId = Utilities.GetStringValue(curRow.GetData("PERIPHERALID"));

                //Get workstation
                WorkstationVO wkVo = workstations.Find(x => (x.Id.Equals(wksId, StringComparison.OrdinalIgnoreCase)));
                //Get peripheral
                PeripheralVO prVo = peripherals.Find(y => (y.Id.Equals(perId, StringComparison.OrdinalIgnoreCase)));

                if (wkVo == null || prVo == null) continue;
                string wkName = wkVo.Name;
                if (wkspMapping.ContainsKey(wkName))
                {
                    wkspMapping[wkName].Add(new PairType<string, PeripheralVO>(sPerId, prVo));
                }
                else
                {
                    var newList = new List<PairType<string, PeripheralVO>>
                                  {
                                        new PairType<string, PeripheralVO>(sPerId, prVo)
                                  };
                    wkspMapping.Add(wkName, newList);
                }
            }
        }

        public static void GetAllUsers(
            ref DataAccessTools dA,
            string facFilter,
            ref List<QuadType<UserVO, LDAPUserVO, string, string>> users)
        {
            if (string.IsNullOrEmpty(facFilter)) return;
            DataReturnSet retSetUsrInf;
            if (!DataAccessService.ExecuteQuery(false,
                                                string.Format("select uinf.activated as ACTIVATED, " +
                                                              "uinf.name as NAME, uinf.fname as FNAME, " +
                                                              "uinf.lname as LNAME, uinf.location as LOCATION, " +
                                                              "uinf.userid as USERID, uidet.userinfodetailid as USERINFODETAILID, " +
                                                              "uidet.employeenumber as EMPLOYEENUMBER, " +
                                                              "uidet.facnumber as FACNUMBER, uroles.roleid as ROLEID " +
                                                              "from ccsowner.userinfo uinf " +
                                                              "inner join ccsowner.userinfodetail uidet on uidet.userid = uinf.userid " +
                                                              "inner join ccsowner.userroles uroles on uinf.userid = uroles.userid " +
                                                              "where uidet.facnumber = '{0}'", facFilter),
                                                "ccsowner.userinfo",
                                                PawnStoreSetupForm.CCSOWNER,
                                                out retSetUsrInf,
                                                ref dA))
            {
                MessageBox.Show("Could not retrieve users from cashlinx database!",
                                PawnStoreSetupForm.SETUPALERT_TXT);
                return;
            }

            if (retSetUsrInf == null || retSetUsrInf.NumberRows <= 0)
            {
                MessageBox.Show("No users found for facility " + facFilter, PawnStoreSetupForm.SETUPALERT_TXT);
                return;
            }

            for (int i = 0; i < retSetUsrInf.NumberRows; ++i)
            {
                DataReturnSetRow dR;
                if (!retSetUsrInf.GetRow(i, out dR)) continue;
                string activated = Utilities.GetStringValue(dR.GetData("ACTIVATED"));
                string name = Utilities.GetStringValue(dR.GetData("NAME"));
                string fname = Utilities.GetStringValue(dR.GetData("FNAME"));
                string lname = Utilities.GetStringValue(dR.GetData("LNAME"));
                string location = Utilities.GetStringValue(dR.GetData("LOCATION"));
                string userid = Utilities.GetStringValue(dR.GetData("USERID"));
                string userinfodetid = Utilities.GetStringValue(dR.GetData("USERINFODETAILID"));
                string employeenumber = Utilities.GetStringValue(dR.GetData("EMPLOYEENUMBER"));
                string facnumber = Utilities.GetStringValue(dR.GetData("FACNUMBER"));
                string roleid = Utilities.GetStringValue(dR.GetData("ROLEID"));
                name = (!string.IsNullOrEmpty(name) ? name : string.Empty);
                var userVO = new UserVO
                {
                    EmployeeNumber = employeenumber,
                    FacNumber = facnumber,
                    StoreNumber = location,
                    UserFirstName = fname,
                    UserLastName = lname,
                    UserID = userid,
                    UserName = name,
                    UserRole = new RoleVO
                    {
                        RoleId = roleid
                    }
                };
                var ldapVO = new LDAPUserVO();
                ldapVO.UserName = name;
                ldapVO.EmployeeNumber = employeenumber;
                ldapVO.DisplayName = fname + " " + lname;

                var usrT = new QuadType<UserVO, LDAPUserVO, string, string>(userVO, ldapVO, activated, userinfodetid);
                users.Add(usrT);
            }
        }


        public static bool getGlobalConfig(
            ref DataAccessTools dA,
            ref PawnSecVO pwnSec,
            ref PawnSecVO.PawnSecStoreVO pStore,
            out bool changedPublicKey)
        {
            //Set changed public key to false
            changedPublicKey = false;

            //Get global configuration
            DataReturnSet globalConfig;
            var globPairs = new PairType<string, string>[1];
            globPairs[0] = new PairType<string, string>("APPVERSION",
                                                        pStore.AppVersion.AppVersionId);
            if (!DataAccessService.ExecuteVariableQuery(false,
                                                        PawnStoreSetupQueries.SELECT_PAWNSEC_GLOBAL,
                                                        "globalconfig",
                                                        PawnStoreSetupForm.PAWNSEC,
                                                        out globalConfig,
                                                        ref dA,
                                                        globPairs))
            {
                MessageBox.Show("Could not query global configuration from pawn sec",
                                PawnStoreSetupForm.SETUPALERT_TXT);
                return (false);
            }

            if (globalConfig == null || globalConfig.NumberRows <= 0 || globalConfig.NumberRows > 1)
            {
                MessageBox.Show("Invalid or unexpected number of global config rows returned",
                                PawnStoreSetupForm.SETUPALERT_TXT);
                return (false);
            }

            //Retrieve the data row
            DataReturnSetRow globDr;
            if (!globalConfig.GetRow(0, out globDr))
            {
                MessageBox.Show("Could not retrieve first row of the global config data table",
                                PawnStoreSetupForm.SETUPALERT_TXT);
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
            string globPubKeyEnc = Utilities.GetStringValue(globDr.GetData("PUBLICKEY"));
            string globPubKeyDec =
                    StringUtilities.Decrypt(globPubKeyEnc,
                                            Common.Properties.Resources.PrivateKey,
                                            true);
            //Ensure public keys are the same, if not, prompt to change
            if (!string.Equals(globPubKeyDec, pwnSec.GlobalConfiguration.DataPublicKey))
            {
                var res =
                        MessageBox.Show(
                                "The public key stored in the database does not match " +
                                Environment.NewLine +
                                "the public key generated in this setup session.  Are you sure you want to change the " +
                                Environment.NewLine +
                                "public key?",
                                PawnStoreSetupForm.SETUPALERT_TXT,
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    if (pwnSec.GlobalConfiguration.ModifyPublicKey(globPubKeyDec))
                    {
                        changedPublicKey = true;
                    }
                }
            }

            //If we made it here, we were successful in acquiring global configuration
            return (true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dA"></param>
        /// <param name="storeNumber"></param>
        /// <param name="storeInfo"></param>
        /// <param name="publicKeyDecrypt"></param>
        /// <param name="pwnSec"></param>
        /// <param name="changedPublicKey"></param>
        /// <returns></returns>
        public static bool GetAllPawnSecData(
            ref DataAccessTools dA,
            string storeNumber,
            SiteId storeInfo,
            string publicKeyDecrypt,
            ref PawnSecVO pwnSec,
            out bool changedPublicKey)
        {
            changedPublicKey = false;
            if (pwnSec == null || string.IsNullOrEmpty(storeNumber) ||
                string.IsNullOrEmpty(publicKeyDecrypt))
            {
                MessageBox.Show("Data passed into method is invalid",
                                PawnStoreSetupForm.SETUPALERT_TXT);
                return (false);
            }
            //Get next id set
            ulong clientRegId;
            GetNextId(ref dA, "ID", "CLIENTREGISTRY", out clientRegId);
            ulong clientStoreMapId;
            GetNextId(ref dA, "ID", "CLIENTSTOREMAP", out clientStoreMapId);
            ulong databaseServiceId;
            GetNextId(ref dA, "ID", "DATABASESERVICE", out databaseServiceId);
            ulong databaseServiceStoreMapId;
            GetNextId(ref dA, "ID", "DATABASESERVICESTOREMAP", out databaseServiceStoreMapId);
            ulong esbServiceId;
            GetNextId(ref dA, "ID", "ESBSERVICE", out esbServiceId);
            ulong esbServiceStoreMapId;
            GetNextId(ref dA, "ID", "ESBSERVICESTOREMAP", out esbServiceStoreMapId);
            ulong globalConfigId;
            GetNextId(ref dA, "ID", "GLOBALCONFIG", out globalConfigId);
            ulong storeAppVersionId;
            GetNextId(ref dA, "ID", "STOREAPPVERSION", out storeAppVersionId);
            ulong storeClientConfigId;
            GetNextId(ref dA, "ID", "STORECLIENTCONFIG", out storeClientConfigId);
            ulong storeConfigNxId;
            GetNextId(ref dA, "ID", "STORECONFIG", out storeConfigNxId);
            ulong storeSiteInfoNxId;
            GetNextId(ref dA, "ID", "STORESITEINFO", out storeSiteInfoNxId);

            //Initialize next id vo
            var nvo = new PawnSecVO.PawnSecNextIdVO(
                clientRegId, clientStoreMapId, databaseServiceId,
                databaseServiceStoreMapId, esbServiceId, esbServiceStoreMapId,
                globalConfigId, storeAppVersionId, storeClientConfigId,
                storeConfigNxId, storeSiteInfoNxId);
            pwnSec.InitializeNextIdSet(nvo);

            //Generate initial store object
            var pStore = new PawnSecVO.PawnSecStoreVO("1.0.0", "0");
            pStore.StoreSite = storeInfo;
            pStore.StoreConfiguration.CompanyName = storeInfo.Company;
            pStore.StoreConfiguration.CompanyNumber = storeInfo.CompanyNumber;
            pwnSec.Stores.Add(pStore);

            DataReturnSet pwnSecMachines;
            var ptVars = new PairType<string, string>[1];
            ptVars[0] = new PairType<string, string>("STORENUMBER", storeNumber);

            if (!DataAccessService.ExecuteVariableQuery(false,
                                                        PawnStoreSetupQueries.
                                                        SELECT_PAWNSEC_MACHINES,
                                                        "clientregistry",
                                                        PawnStoreSetupForm.PAWNSEC,
                                                        out pwnSecMachines,
                                                        ref dA,
                                                        ptVars))
            {
                MessageBox.Show("Could not query the clientregistry");
                return (false);
            }

            if (pwnSecMachines == null || pwnSecMachines.NumberRows <= 0)
            {
                //Madhu BZ # 238
                if (!PawnStoreSetupForm.batchMode)
                    MessageBox.Show("Could not find any client machine data in pawnsec",
                                PawnStoreSetupForm.SETUPALERT_TXT);
                //return (false);
            }


            //Formulate decryption key
            string dKey = publicKeyDecrypt + Common.Properties.Resources.PrivateKey;

            //Push all retrieved machines into the proper VO lists
            if (pwnSecMachines != null && pwnSecMachines.NumberRows > 0)
            {
                for (int i = 0; i < pwnSecMachines.NumberRows; ++i)
                {
                    DataReturnSetRow dR;
                    if (!pwnSecMachines.GetRow(i, out dR))
                        continue;
                    var pvo = new PawnSecVO.ClientPawnSecMachineVO();
                    var pRegMap = new PawnSecVO.ClientStoreMapVO();
                    pvo.Machine.ClientId = Utilities.GetULongValue(dR.GetData("ID"));
                    pvo.Machine.IPAddress = Utilities.GetStringValue(dR.GetData("IPADDRESS"));
                    pvo.Machine.MachineName = Utilities.GetStringValue(dR.GetData("MACHINENAME"));
                    pvo.Machine.MACAddress = Utilities.GetStringValue(dR.GetData("MACADDRESS"));
                    string isAllowStr = Utilities.GetStringValue(dR.GetData("ISALLOWED"));
                    pvo.Machine.IsAllowed = (!string.IsNullOrEmpty(isAllowStr) && isAllowStr.Equals("1"));
                    string isConnStr = Utilities.GetStringValue(dR.GetData("ISCONNECTED"));
                    pvo.Machine.IsConnected = (!string.IsNullOrEmpty(isConnStr) && isConnStr.Equals("1"));
                    pvo.Machine.AdobeOverride = Utilities.GetStringValue(dR.GetData("ADOBEOVERRIDE"));
                    pvo.Machine.GhostOverride =
                            Utilities.GetStringValue(dR.GetData("GHOSTOVERRIDE"));
                    pRegMap.Id = Utilities.GetULongValue(dR.GetData("CLIENTSTOREMAPID"));
                    pRegMap.ClientRegistryId = Utilities.GetULongValue(dR.GetData("CLIENTREGISTRYID"));
                    pRegMap.StoreSiteId = Utilities.GetULongValue(dR.GetData("STORESITEID"));
                    pRegMap.StoreClientConfigId =
                            Utilities.GetULongValue(dR.GetData("STORECLIENTCONFIGID"));
                    pRegMap.StoreConfigId = Utilities.GetULongValue(dR.GetData("STORECONFIGID"));
                    pvo.StoreMachine.WorkstationId =
                            Utilities.GetStringValue(dR.GetData("WORKSTATIONID"));
                    pvo.Machine.WorkstationName = pvo.StoreMachine.WorkstationId;
                    pvo.StoreMachine.Id = pRegMap.StoreClientConfigId;
                    pvo.StoreMachine.TerminalNumber =
                            Utilities.GetIntegerValue(dR.GetData("TERMINALNUMBER"));
                    pvo.StoreMachine.LogLevel = Utilities.GetStringValue(dR.GetData("LOGLEVEL"));
                    pvo.StoreMachine.TraceLevel = Utilities.GetIntegerValue(dR.GetData("TRACELEVEL"));
                    string printEn = Utilities.GetStringValue(dR.GetData("PRINTENABLED"));
                    pvo.StoreMachine.PrintEnabled = (!string.IsNullOrEmpty(printEn) &&
                                                     printEn.Equals("1"));
                    pRegMap.StoreNumber = Utilities.GetStringValue(dR.GetData("STORENUMBER"));

                    //Add machine
                    pwnSec.ClientMachines.Add(pvo);
                    //Add mapping
                    pwnSec.ClientStoreMapList.Add(pRegMap);
                }


                //Find the first reg map with a store number matching the input
                var regMapVO = pwnSec.ClientStoreMapList.Find(x => x.StoreNumber.Equals(storeNumber));
                if (regMapVO == null)
                {
                    MessageBox.Show(
                            "Cannot find any map entries that correspond with input store",
                            PawnStoreSetupForm.SETUPALERT_TXT);
                    return (false);
                }

                //Prepare the next query parameters
                var ptStoVars = new PairType<string, string>[2];
                ptStoVars[0] = new PairType<string, string>("STORECONFIGID",
                                                            regMapVO.StoreConfigId.ToString());
                ptStoVars[1] = new PairType<string, string>("STORESITEINFOID",
                                                            regMapVO.StoreSiteId.ToString());

                //Query the database for the store information
                DataReturnSet pwnSecStore;
                if (!DataAccessService.ExecuteVariableQuery(
                        false,
                        PawnStoreSetupQueries.
                        SELECT_PAWNSEC_STORE,
                        "storeconfigid",
                        PawnStoreSetupForm.PAWNSEC,
                        out pwnSecStore,
                        ref dA,
                        ptStoVars))
                {
                    MessageBox.Show("Could not query the database for pawn sec store information",
                                    PawnStoreSetupForm.SETUPALERT_TXT);
                    return (false);
                }

                if (pwnSecStore == null || pwnSecStore.NumberRows <= 0 || pwnSecStore.NumberRows > 1)
                {
                    MessageBox.Show(
                            "No results found when querying the database for pawn sec store, or too many results found",
                            PawnStoreSetupForm.SETUPALERT_TXT);
                    return (false);
                }

                DataReturnSetRow dRSto;
                if (!pwnSecStore.GetRow(0, out dRSto))
                {
                    MessageBox.Show("Invalid data/structure in pawn sec store row",
                                    PawnStoreSetupForm.SETUPALERT_TXT);
                    return (false);
                }

                ulong storeConfigId = Utilities.GetULongValue(dRSto.GetData("ID"));
                ulong storeSiteInfoId = Utilities.GetULongValue(dRSto.GetData("STORESITEINFOID"));
                string aliasCode = Utilities.GetStringValue(dRSto.GetData("ALIASCODE"));
                string stateCode = Utilities.GetStringValue(dRSto.GetData("STATE"));
                string storeNum = Utilities.GetStringValue(dRSto.GetData("STORENUMBER"));
                //Verify key store data
                if (storeConfigId < 0L ||
                    storeSiteInfoId < 0L ||
                    string.IsNullOrEmpty(aliasCode) ||
                    string.IsNullOrEmpty(stateCode) ||
                    string.IsNullOrEmpty(storeNum))
                {
                    MessageBox.Show(
                            "Invalid data contained in pawn sec database for store " + storeNumber,
                            PawnStoreSetupForm.SETUPALERT_TXT);
                    return (false);
                }

                //Store object already created and added, just complete the population
                pStore.AppVersion.AppVersionId = Utilities.GetStringValue(dRSto.GetData("APPVERSIONID"));
                pStore.AppVersion.AppVersion = Utilities.GetStringValue(dRSto.GetData("APPVERSION"));

                if (storeConfigId == regMapVO.StoreConfigId &&
                    storeSiteInfoId == regMapVO.StoreSiteId &&
                    storeNum.Equals(regMapVO.StoreNumber))
                {
                    storeInfo.Alias = aliasCode;
                    storeInfo.State = stateCode;
                    pStore.StoreConfiguration.Id = storeConfigId;
                    pStore.StoreConfiguration.MetalsFile =
                            Utilities.GetStringValue(dRSto.GetData("METALSFILE"));
                    pStore.StoreConfiguration.StonesFile =
                            Utilities.GetStringValue(dRSto.GetData("STONESFILE"));
                    pStore.StoreConfiguration.TimeZone =
                            Utilities.GetStringValue(dRSto.GetData("TIMEZONE"));
                    pStore.StoreConfiguration.FetchSizeMultiplier =
                            Utilities.GetULongValue(dRSto.GetData("FETCHSZMX"));
                    pStore.StoreConfiguration.DayOffset =
                            Utilities.GetLongValue(dRSto.GetData("DAYOFFSET"));
                    pStore.StoreConfiguration.CompanyName =
                            Utilities.GetStringValue(dRSto.GetData("COMPANYNAME"));
                    pStore.StoreConfiguration.CompanyNumber =
                            Utilities.GetStringValue(dRSto.GetData("COMPANYNUMBER"));
                    pStore.StoreSite = storeInfo;
                    pStore.StoreSiteId = storeSiteInfoId;
                }
                else
                {
                    MessageBox.Show(
                            "Could not validate store data returned from pawnsec - a data integrity problem exists for store " +
                            storeNumber,
                            PawnStoreSetupForm.SETUPALERT_TXT);
                    return (false);
                }

                //Get global configuration
                bool publicKeyChanged;
                if (!getGlobalConfig(ref dA, ref pwnSec, ref pStore, out publicKeyChanged))
                {
                    MessageBox.Show("Could not acquire global configuration", PawnStoreSetupForm.SETUPALERT_TXT);
                }

                //Setup store config as the single parameter needed for the next few queries
                var ptStoCfg = new PairType<string, string>[1];
                ptStoCfg[0] = new PairType<string, string>("STORECONFIGID", storeConfigId.ToString());

                //Load esb services and map
                DataReturnSet esbServices;
                if (!DataAccessService.ExecuteVariableQuery(false,
                                                            PawnStoreSetupQueries.SELECT_PAWNSEC_ESB,
                                                            "esbservice",
                                                            PawnStoreSetupForm.PAWNSEC,
                                                            out esbServices,
                                                            ref dA,
                                                            ptStoCfg))
                {
                    MessageBox.Show("Could not execute query against the esb service table",
                                    PawnStoreSetupForm.SETUPALERT_TXT);
                    return (false);
                }

                if (esbServices == null || esbServices.NumberRows <= 0)
                {
                    MessageBox.Show("Could not find any esb services in the pawnsec database",
                                    PawnStoreSetupForm.SETUPALERT_TXT);
                    return (false);
                }

                for (int i = 0; i < esbServices.NumberRows; ++i)
                {
                    DataReturnSetRow dR;
                    if (!esbServices.GetRow(i, out dR))
                        continue;

                    var enabFlag = Utilities.GetStringValue(dR.GetData("ENABLED"));
                    var enabFlagBool = (!string.IsNullOrEmpty(enabFlag) && enabFlag.Equals("1"));
                    var esbServiceVo = new EsbServiceVO(enabFlagBool)
                    {
                        Id = Utilities.GetULongValue(dR.GetData("ID")),
                        Name = Utilities.GetStringValue(dR.GetData("NAME")),
                        Server = Utilities.GetStringValue(dR.GetData("SERVER")),
                        Port = Utilities.GetStringValue(dR.GetData("PORT")),
                        Domain = Utilities.GetStringValue(dR.GetData("DOMAIN")),
                        Uri = Utilities.GetStringValue(dR.GetData("URI")),
                        EndPointName = Utilities.GetStringValue(dR.GetData("ENDPOINTNAME"))
                    };
                    var esbMapVo = new PawnSecVO.ESBServiceStoreMapVO
                    {
                        Id = Utilities.GetULongValue(dR.GetData("ESBMAPID")),
                        StoreConfigId = Utilities.GetULongValue(dR.GetData("ESBMAPSTORECONFIGID")),
                        ESBServiceId = esbServiceVo.Id
                    };

                    //Add vo objects to lists
                    pwnSec.ESBServiceList.Add(esbServiceVo);
                    pwnSec.ESBServiceMapList.Add(esbMapVo);
                }


                //Load db services and map
                DataReturnSet dbServices;
                if (!DataAccessService.ExecuteVariableQuery(false,
                                                            PawnStoreSetupQueries.SELECT_PAWNSEC_DB,
                                                            "databaseservice",
                                                            PawnStoreSetupForm.PAWNSEC,
                                                            out dbServices,
                                                            ref dA,
                                                            ptStoCfg))
                {
                    MessageBox.Show("Could not execute query against the database service table",
                                    PawnStoreSetupForm.SETUPALERT_TXT);
                    return (false);
                }

                if (dbServices == null || dbServices.NumberRows <= 0)
                {
                    MessageBox.Show("Could not find any database services in the pawnsec database",
                                    PawnStoreSetupForm.SETUPALERT_TXT);
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
            /*else
            {
                //See if the user wants to load the global config that exists (if one does)
                //Get global configuration
                DataReturnSet globalConfig;
                var globPairs = new PairType<string, string>[1];
                globPairs[0] = new PairType<string, string>("APPVERSION",
                                                            "1");
                DataAccessService.ExecuteVariableQuery(false,
                                                       PawnStoreSetupQueries.SELECT_PAWNSEC_GLOBAL,
                                                       "globalconfig",
                                                       PawnStoreSetupForm.PAWNSEC,
                                                       out globalConfig,
                                                       ref dA,
                                                       globPairs);
                if (globalConfig != null && globalConfig.NumberRows > 0)
                {
                    var gres = MessageBox.Show(
                            "Retrieved existing global configuration. Would you like to associate this to the current store?",
                            PawnStoreSetupForm.SETUPALERT_TXT,
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);
                    if (gres == DialogResult.Yes)
                    {
                        //Retrieve the data row
                        DataReturnSetRow globDr;
                        if (!globalConfig.GetRow(0, out globDr))
                        {
                            MessageBox.Show("Could not associate existing global config with this store.",
                                            PawnStoreSetupForm.SETUPALERT_TXT);
                        }
                        else
                        {
                            pwnSec.GlobalConfiguration.Version =
                                    Utilities.GetStringValue(globDr.GetData("APPVERSIONID"));
                            pwnSec.GlobalConfiguration.BaseTemplatePath =
                                    Utilities.GetStringValue(globDr.GetData("TEMPLATEPATH"));
                            pwnSec.GlobalConfiguration.BaseMediaPath =
                                    Utilities.GetStringValue(globDr.GetData("MEDIAPATH"));
                            pwnSec.GlobalConfiguration.AdobeReaderPath =
                                    Utilities.GetStringValue(globDr.GetData("ADOBEPATH"));
                            pwnSec.GlobalConfiguration.GhostScriptPath =
                                    Utilities.GetStringValue(globDr.GetData("GHOSTPATH"));
                            string globPubKeyEnc = Utilities.GetStringValue(globDr.GetData("PUBLICKEY"));
                            string globPubKeyDec =
                                    StringUtilities.Decrypt(globPubKeyEnc,
                                                            CashlinxDesktop.Properties.Resources.PrivateKey,
                                                            true);
                            //Ensure public keys are the same, if not, prompt to change
                            if (!string.Equals(globPubKeyDec, pwnSec.GlobalConfiguration.DataPublicKey))
                            {
                                var res =
                                        MessageBox.Show(
                                                "The public key stored in the database does not match " +
                                                Environment.NewLine +
                                                "the public key generated in this setup session.  Are you sure you want to change the " +
                                                Environment.NewLine +
                                                "public key?",
                                                PawnStoreSetupForm.SETUPALERT_TXT,
                                                MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Question);
                                if (res == DialogResult.Yes)
                                {
                                    if (pwnSec.GlobalConfiguration.ModifyPublicKey(globPubKeyDec))
                                    {
                                        changedPublicKey = true;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    return (false);
                }

                //See if there is an existing LDAP setup that could be utilized
                DataReturnSet ldapServ;
                DataAccessService.ExecuteQuery(
                        false,
                        PawnStoreSetupQueries.SELECT_PAWNSEC_LDAP,
                        "databaseservice",
                        PawnStoreSetupForm.PAWNSEC,
                        out ldapServ,
                        ref dA);
                if (ldapServ != null && ldapServ.NumberRows > 0)
                {
                    var lRes =
                            MessageBox.Show(
                                    "Retrieved existing LDAP configuration.  Would you like to associate it with this store?",
                                    PawnStoreSetupForm.SETUPALERT_TXT,
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question);
                    if (lRes == DialogResult.Yes)
                    {
                        DataReturnSetRow dR;
                        if (!ldapServ.GetRow(0, out dR))
                        {
                            MessageBox.Show("Could not associate existing LDAP configuration with this store.",
                                PawnStoreSetupForm.SETUPALERT_TXT);
                        }
                        else
                        {
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

                            pwnSec.DatabaseServiceList.Add(dbServiceVo);                            
                        }
                    }
                }

                //See if there are existing ESB services that can be utilized
                DataReturnSet esbServ;
                DataAccessService.ExecuteQuery(false,
                                               PawnStoreSetupQueries.SELECT_PAWNSEC_EXISTESB,
                                               "esbservice",
                                               PawnStoreSetupForm.PAWNSEC,
                                               out esbServ,
                                               ref dA);
                if (esbServ != null && esbServ.NumberRows > 0)
                {
                    var eRes =
                            MessageBox.Show(
                                    "Retrieved existing ESB configuration.  Would you like to associate them with this store?",
                                    PawnStoreSetupForm.SETUPALERT_TXT,
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question);
                    if (eRes == DialogResult.Yes)
                    {
                        for (int i = 0; i < esbServ.NumberRows; ++i)
                        {
                            DataReturnSetRow dR;
                            if (!esbServ.GetRow(i, out dR))
                                continue;

                            var enabFlag = Utilities.GetStringValue(dR.GetData("ENABLED"));
                            var enabFlagBool = (!string.IsNullOrEmpty(enabFlag) && enabFlag.Equals("1"));
                            var esbServiceVo = new EsbServiceVO(enabFlagBool)
                                               {
                                                       Id = Utilities.GetULongValue(dR.GetData("ID")),
                                                       Name = Utilities.GetStringValue(dR.GetData("NAME")),
                                                       Server = Utilities.GetStringValue(dR.GetData("SERVER")),
                                                       Port = Utilities.GetStringValue(dR.GetData("PORT")),
                                                       Domain = Utilities.GetStringValue(dR.GetData("DOMAIN")),
                                                       Uri = Utilities.GetStringValue(dR.GetData("URI"))
                                               };

                            //Add vo objects to lists
                            pwnSec.ESBServiceList.Add(esbServiceVo);
                        }
                    }
                }

                return (true);
            }*/
            return (false);
        }
    }
}
