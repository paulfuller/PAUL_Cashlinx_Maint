using System;
using System.Collections.Generic;
using System.Data;
using Oracle.DataAccess.Client;
using ChangeStoreNumber.Connections;

namespace ChangeStoreNumber
{
    public static class Db
    {
        public static ConnectionFactory ConnectionFactory
        {
            get { return ConnectionFactory.GetInstance(); }
        }

        public static Conversion Conversion
        {
            get { return Conversion.GetInstance(); }
        }

        public static List<AppVersion> GetAppVersions()
        {
            List<AppVersion> appVersions = new List<AppVersion>();

            OracleConnection connection = new OracleConnection(ConnectionFactory.ActiveConnection.PawnSecConnectionString);
            connection.Open();
            OracleCommand command = new OracleCommand();
            command.Connection = connection;
            command.CommandText = "Select ID,description FROM pawnsec.storeappversion order by id";
            command.CommandType = CommandType.Text;
            OracleDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                AppVersion appVersion = new AppVersion();
                appVersion.Id = Conversion.ToInt32(dr["id"]);
                appVersion.Description = Conversion.ToString(dr["description"]);
                appVersions.Add(appVersion);
            }
            connection.Dispose();

            return appVersions;
        }

        public static List<ClientRegistry> GetClientRegistries()
        {
            List<ClientRegistry> registries = new List<ClientRegistry>();

            OracleConnection connection = new OracleConnection(ConnectionFactory.ActiveConnection.PawnSecConnectionString);
            connection.Open();
            OracleCommand command = new OracleCommand();
            command.Connection = connection;
            command.CommandText = "Select cr.Id, cr.MachineName,StoreSiteId FROM pawnsec.clientstoremap sm left join pawnsec.clientregistry cr on sm.ClientRegistryId = cr.id order by cr.machinename";
            command.CommandType = CommandType.Text;
            OracleDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                ClientRegistry cr = new ClientRegistry();
                cr.Id = Conversion.ToInt32(dr["id"]);
                cr.FullMachineName = Conversion.ToString(dr["machinename"]);
                cr.CurrentStoreId = Conversion.ToInt32(dr["StoreSiteId"]);
                registries.Add(cr);
            }
            connection.Dispose();

            return registries;
        }

        public static List<StoreSiteInfo> GetStoreSites()
        {
            List<StoreSiteInfo> storeSites = new List<StoreSiteInfo>();

            OracleConnection connection = new OracleConnection(ConnectionFactory.ActiveConnection.PawnSecConnectionString);
            connection.Open();
            OracleCommand command = new OracleCommand();
            command.Connection = connection;
            command.CommandText = "Select ID,storenumber,appversionid FROM pawnsec.storesiteinfo order by storenumber";
            command.CommandType = CommandType.Text;
            OracleDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                StoreSiteInfo s = new StoreSiteInfo();
                s.Id = Conversion.ToInt32(dr["id"]);
                s.StoreNumber = Conversion.ToString(dr["storenumber"]);
                s.AppVersionId = Conversion.ToInt32(dr["appversionid"]);
                storeSites.Add(s);
            }
            connection.Dispose();

            return storeSites;
        }

        public static Dictionary<string, Store> GetStores()
        {
            Dictionary<string, Store> stores = new Dictionary<string, Store>();

            OracleConnection connection = new OracleConnection(ConnectionFactory.ActiveConnection.CcsOwnerConnectionString);
            connection.Open();
            OracleCommand command = new OracleCommand();
            command.Connection = connection;
            command.CommandText = "select storeid,storenumber from ccsowner.store order by storenumber";
            command.CommandType = CommandType.Text;
            OracleDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                Store s = new Store();
                s.StoreId = Conversion.ToString(dr["storeid"]);
                s.StoreNumber = Conversion.ToString(dr["storenumber"]);
                stores.Add(s.StoreNumber, s);
            }
            connection.Dispose();

            return stores;
        }

        public static bool ChangeStore(ClientRegistry cr, StoreSiteInfo si, Store store, AppVersion appVersion)
        {
            bool success = false;

            OracleTransaction pawnSecTransaction = null;
            OracleTransaction cdOwnerTransaction = null;

            try
            {
                int result = UpdatePawnSec(cr, si, out pawnSecTransaction);

                if (result == 0)
                {
                    pawnSecTransaction.Rollback();
                    return false;
                }

                if (appVersion.UsesCashDrawer)
                {
                    result = UpdateCdOwner(cr, store, out cdOwnerTransaction);

                    if (result == 0)
                    {
                        cdOwnerTransaction.Rollback();
                        return false;
                    }
                }

                pawnSecTransaction.Commit();

                if (appVersion.UsesCashDrawer)
                {
                    cdOwnerTransaction.Commit();
                }

                success = true;
            }
            catch (Exception)
            {
                if (pawnSecTransaction != null)
                {
                    pawnSecTransaction.Rollback();
                }
                if (cdOwnerTransaction != null)
                {
                    cdOwnerTransaction.Rollback();
                }
                success = false;
            }

            return success;
        }

        public static string GetCcsOwnerConnectionString(DatabaseConnection databaseConnection)
        {
            string connectionString = null;

            var connection = new OracleConnection(databaseConnection.PawnSecConnectionString);
            connection.Open();
            var command = new OracleCommand();
            command.Connection = connection;
            command.CommandText = "select server,port,schema,dbuser,dbuserpwd,auxinfo from databaseservice where servicetype = 'ORACLE' and schema = 'cDM5ibgmG0p+WQ+huCsNng=='";
            command.CommandType = CommandType.Text;
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                string server = EncryptionUtil.Decrypt(Conversion.ToString(reader["server"]), EncryptionUtil.DEFAULT_PUBLIC_KEY, true);
                string port = EncryptionUtil.Decrypt(Conversion.ToString(reader["port"]), EncryptionUtil.DEFAULT_PUBLIC_KEY, true);
                string schema = EncryptionUtil.Decrypt(Conversion.ToString(reader["schema"]), EncryptionUtil.DEFAULT_PUBLIC_KEY, true);
                string dbuser = EncryptionUtil.Decrypt(Conversion.ToString(reader["dbuser"]), EncryptionUtil.DEFAULT_PUBLIC_KEY, true);
                string dbuserpwd = EncryptionUtil.Decrypt(Conversion.ToString(reader["dbuserpwd"]), EncryptionUtil.DEFAULT_PUBLIC_KEY, true);
                string auxinfo = EncryptionUtil.Decrypt(Conversion.ToString(reader["auxinfo"]), EncryptionUtil.DEFAULT_PUBLIC_KEY, true);

                connectionString = string.Format("user id={0};password={1};data source={2}:{3}/{4}", dbuser, dbuserpwd, server, port, auxinfo);
            }
            connection.Dispose();

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ApplicationException("Unable to read connection string from PAWNSEC");
            }

            return connectionString;
        }

        private static int UpdateCdOwner(ClientRegistry cr, Store store, out OracleTransaction transaction)
        {
            OracleConnection connection = new OracleConnection(ConnectionFactory.ActiveConnection.CcsOwnerConnectionString);
            connection.Open();
            transaction = connection.BeginTransaction();
            OracleCommand command = new OracleCommand();
            command.Connection = connection;
            command.CommandText = string.Format("UPDATE cdowner.cd_workstation SET branchid = '{0}' WHERE name = '{1}'", store.StoreId, cr.MachineName);
            command.CommandType = CommandType.Text;
            command.Transaction = transaction;
            return command.ExecuteNonQuery();
        }

        private static int UpdatePawnSec(ClientRegistry cr, StoreSiteInfo si, out OracleTransaction transaction)
        {
            OracleConnection connection = new OracleConnection(ConnectionFactory.ActiveConnection.PawnSecConnectionString);
            connection.Open();
            transaction = connection.BeginTransaction();
            OracleCommand command = new OracleCommand();
            command.Connection = connection;
            command.CommandText = string.Format("UPDATE pawnsec.clientstoremap SET storesiteid = {0} WHERE clientregistryid = {1} and storesiteid = {2}", si.Id, cr.Id, cr.CurrentStoreId);
            command.CommandType = CommandType.Text;
            command.Transaction = transaction;
            return command.ExecuteNonQuery();
        }
    }
}
