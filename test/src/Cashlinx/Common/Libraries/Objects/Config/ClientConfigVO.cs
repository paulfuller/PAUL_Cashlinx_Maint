using System.Collections.Generic;

namespace Common.Libraries.Objects.Config
{
    // Use SiteId as well
    public class ClientConfigVO
    {
        // contains Dictionary keyed off of name for ESB Services, same for DB Services
        // store info from CLIENTREGISTRY in here
        public GlobalConfigVO GlobalConfiguration { get; set;}
        public StoreAppVersionVO AppVersion { get; set; }
        public StoreClientConfigVO ClientConfiguration { get; set;}
        public StoreConfigVO StoreConfiguration  { get; set;}
        public SiteId StoreSite { get; set; }
        public Dictionary<string, DatabaseServiceVO> DatabaseServices { get; set; }
        public Dictionary<string, EsbServiceVO> EsbServices { get; set; }
        public string IpAddress { get; set;}
        public string MacAddress { get; set; }
        public string MachineName { get; set; }

        public ClientConfigVO(string appVer, string publicKey)
        {
            this.GlobalConfiguration = new GlobalConfigVO(publicKey);
            this.AppVersion = new StoreAppVersionVO(appVer,"1");
            this.ClientConfiguration = new StoreClientConfigVO();
            this.StoreConfiguration = new StoreConfigVO();
            this.StoreSite = new SiteId();
            this.DatabaseServices = new Dictionary<string, DatabaseServiceVO>(1);
            this.EsbServices = new Dictionary<string, EsbServiceVO>(1);
            this.IpAddress = string.Empty;
            this.MacAddress = string.Empty;
            this.MachineName = string.Empty;
        }
    }
}
