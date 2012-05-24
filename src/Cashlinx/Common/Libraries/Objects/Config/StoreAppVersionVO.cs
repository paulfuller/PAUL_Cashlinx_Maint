namespace Common.Libraries.Objects.Config
{
    public class StoreAppVersionVO
    {
        public string AppVersion { get; set; }
        public string AppVersionId
        {
            get; set;
        }
        public string Description { get; set; }

        public StoreAppVersionVO(string appVers, string appVersId)
        {
            this.AppVersion = appVers;
            this.AppVersionId = appVersId;
            this.Description = string.Empty;
        }

        public StoreAppVersionVO()
        {
            this.AppVersion = string.Empty;
            this.AppVersionId = string.Empty;
            this.Description = string.Empty;
        }
    }
}
