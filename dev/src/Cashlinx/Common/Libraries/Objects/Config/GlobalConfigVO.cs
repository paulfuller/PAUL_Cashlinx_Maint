namespace Common.Libraries.Objects.Config
{
    public class GlobalConfigVO
    {
        public string AdobeReaderPath { get; set; }
        public string GhostScriptPath { get; set; }
        public string BaseTemplatePath { get; set; }
        public string BaseLogPath { get; set; }
        public string BaseMediaPath { get; set; }
        public string DataPublicKey { get; private set; }
        public string Version { get; set; }

        public GlobalConfigVO(string dataPublicKey)
        {
            this.AdobeReaderPath = string.Empty;
            this.GhostScriptPath = string.Empty;
            this.BaseTemplatePath = string.Empty;
            this.BaseLogPath = string.Empty;
            this.BaseMediaPath = string.Empty;
            this.DataPublicKey = dataPublicKey;
            this.Version = string.Empty;
        }

        public bool ModifyPublicKey(string newDataPublicKey)
        {
            if (!string.Equals(this.DataPublicKey, newDataPublicKey))
            {
                this.DataPublicKey = newDataPublicKey;
                return (true);
            }
            return (false);
        }
    }
}
