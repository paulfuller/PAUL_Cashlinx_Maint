namespace Common.Libraries.Objects.Config
{
    public class DatabaseServiceVO
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public string ServiceType { get; set; }
        public string Server { get; set; }
        public string Port { get; set; }
        public string Schema { get; set; }
        public string DbUser { get; private set; }
        public string DbUserPwd { get; private set; }
        public string AuxInfo { get; set; }
        public bool Enabled { get; private set; }

        public DatabaseServiceVO(string dbuser, string dbpwd, bool enabled)
        {
            this.Id = 0L;
            this.Name = string.Empty;
            this.ServiceType = string.Empty;
            this.Server = string.Empty;
            this.Port = string.Empty;
            this.Schema = string.Empty;
            this.DbUser = dbuser;
            this.DbUserPwd = dbpwd;
            this.Enabled = enabled;
            this.AuxInfo = string.Empty;
        }

        public override string ToString()
        {
            var rt = string.Format("DatabaseServiceVO ({0}, {1}, {2}, {3}, {4}, {5}, {6}) - Enabled: {7}", Name, ServiceType, Server, Port,
                                   Schema, DbUser, AuxInfo, Enabled);
            return (rt);
        }
    }
}
