using Common.Libraries.Objects.Authorization;
using System.Data;

namespace Common.Libraries.Objects
{
    public class CommonDatabaseContext
    {
        public CommonDatabaseContext()
        {
            ErrorCode = string.Empty;
            ErrorText = string.Empty;
        }

        public SiteId CurrentSiteId { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorText { get; set; }
        public string FullUserName { set; get; }
        public DataSet OutputDataSet1 { get; set; }
        public DataSet OutputDataSet2 { get; set; }
        public bool Result { get; set; }
        public UserVO User { get; set; }
        public string UserName { set; get; }
    }
}
