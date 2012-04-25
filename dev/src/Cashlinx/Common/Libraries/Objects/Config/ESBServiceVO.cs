namespace Common.Libraries.Objects.Config
{
    public class EsbServiceVO
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public string Server { get; set; }
        public string Port { get; set; }
        public string Domain { get; set; }
        public string Uri { get; set; }
        public string EndPointName { get; set; }
        public string ClientBinding { get; set; }
        public string HttpBinding { get; set; }
        public bool Enabled { get; private set; }

        public EsbServiceVO(bool enabled)
        {
            this.Id = 0L;
            this.Name = string.Empty;
            this.Server = string.Empty;
            this.Port = string.Empty;
            this.Domain = string.Empty;
            this.Uri = string.Empty;
            this.EndPointName = string.Empty;
            this.ClientBinding = string.Empty;
            this.HttpBinding = string.Empty;
            this.Enabled = enabled;
        }
    }
}
