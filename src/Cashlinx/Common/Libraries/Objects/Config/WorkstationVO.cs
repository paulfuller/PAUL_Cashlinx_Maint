using System.ComponentModel;

namespace Common.Libraries.Objects.Config
{
    public class WorkstationVO
    {
        [Browsable(true)]
        public string Name
        {
            set;
            get;
        }
        [Browsable(false)]
        public string Id
        {
            set;
            get;
        }
        [Browsable(false)]
        public string StoreId
        {
            private set;
            get;
        }
        [Browsable(true)]
        public string StoreNumber
        {
            set; get;
        }

        public WorkstationVO(string storeId)
        {
            Name = string.Empty;
            Id = string.Empty;
            StoreId = storeId;
            StoreNumber = string.Empty;
        }
    }
}
