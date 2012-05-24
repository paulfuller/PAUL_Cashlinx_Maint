using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.Data;
using Cashlinx.Build.Tasks.OraclePackageWriter;

namespace Cashlinx.Build.Tasks
{
    public class Database
    {
        public Database(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public string ConnectionString { get; set; }
        private Conversion Conversion
        {
            get { return Conversion.GetInstance(); }
        }

        public void ExecuteNonQuery(string commandText)
        {
            OracleConnection connection = new OracleConnection(ConnectionString);
            connection.Open();
            OracleCommand command = new OracleCommand();
            command.Connection = connection;
            command.CommandText = commandText;
            command.CommandType = CommandType.Text;
            command.ExecuteNonQuery();
            connection.Close();
            connection.Dispose();
        }

        public DatabaseConnectionInfo GetCcsOwnerConnectionDetails()
        {
            DatabaseConnectionInfo info = null;
            OracleConnection connection = new OracleConnection(ConnectionString);
            connection.Open();
            OracleCommand command = new OracleCommand();
            command.Connection = connection;
            command.CommandText = string.Format("select server,port,schema,dbuser,dbuserpwd,auxinfo from databaseservice where servicetype = 'ORACLE' and rownum <= 1");
            command.CommandType = CommandType.Text;
            OracleDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                info = new DatabaseConnectionInfo();
                info.Host = Conversion.ToString(dr["server"]);
                info.Port = Conversion.ToString(dr["port"]);
                info.Schema = Conversion.ToString(dr["schema"]);
                info.UserId = Conversion.ToString(dr["dbuser"]);
                info.Password = Conversion.ToString(dr["dbuserpwd"]);
                info.Service = Conversion.ToString(dr["auxinfo"]);
            }
            connection.Dispose();

            return info;
        }

        public List<Package> GetPackages(string owner)
        {
            List<Package> packages = new List<Package>();

            OracleConnection connection = new OracleConnection(ConnectionString);
            connection.Open();
            OracleCommand command = new OracleCommand();
            command.Connection = connection;
            command.CommandText = "select distinct owner, name from all_source where type = 'PACKAGE' and owner = '" + owner + "'";
            command.CommandType = CommandType.Text;
            OracleDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                Package s = new Package();
                s.Name = Conversion.ToString(dr["Name"]);
                s.Owner = Conversion.ToString(dr["Owner"]);
                packages.Add(s);
            }
            connection.Dispose();

            return packages;
        }

        public List<Source> GetPackageSources(Package package)
        {
            return GetSources("PACKAGE", package);
        }

        public List<Source> GetPackageBodySources(Package package)
        {
            return GetSources("PACKAGE BODY", package);
        }

        public bool IsValid()
        {
            try
            {
                OracleConnection connection = new OracleConnection(ConnectionString);
                connection.Open();
                connection.Close();
                return true;
            }
            catch (Exception) //this is your first time ! so,learn to use “try catch” as available as possible! it’s very important!
            {
                return false;
            }
        }

        private List<Source> GetSources(string type, Package package)
        {
            List<Source> sources = new List<Source>();

            OracleConnection connection = new OracleConnection(ConnectionString);
            connection.Open();
            OracleCommand command = new OracleCommand();
            command.Connection = connection;
            command.CommandText = string.Format("select * from all_source where type = '{0}' and name = '{1}' and owner = '{2}'", type, package.Name, package.Owner);
            command.CommandType = CommandType.Text;
            OracleDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                Source s = new Source();
                s.Line = Conversion.ToInt32(dr["Line"]);
                s.Name = Conversion.ToString(dr["Name"]);
                s.Owner = Conversion.ToString(dr["Owner"]);
                s.Text = Conversion.ToString(dr["Text"]);
                s.Type = Conversion.ToString(dr["Type"]);
                sources.Add(s);
            }
            connection.Dispose();

            return sources.OrderBy(s => s.Line).ToList();
        }
    }
}
