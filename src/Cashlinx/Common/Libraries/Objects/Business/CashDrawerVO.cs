using System.ComponentModel;

namespace Common.Libraries.Objects.Business
{
    public class CashDrawerVO
    {
        private string id;
        [Browsable(false)]
        public string Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        private string name;
        [Browsable(true)]
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        private string openFlag;
        [Browsable(true)]
        public string OpenFlag
        {
            get
            {
                return this.openFlag;
            }
            set
            {
                this.openFlag = value;
            }
        }

        private string registerUserId;
        [Browsable(false)]
        public string RegisterUserId
        {
            get
            {
                return this.registerUserId;
            }
            set
            {
                this.registerUserId = value;
            }
        }

        private string branchId;
        [Browsable(false)]
        public string BranchId
        {
            get
            {
                return this.branchId;
            }
            set
            {
                this.branchId = value;
            }
        }

        private string netName;
        [Browsable(true)]
        public string NetName
        {
            get
            {
                return this.netName;
            }
            set
            {
                this.netName = value;
            }
        }

        public CashDrawerVO()
        {
            this.id = string.Empty;
            this.name = string.Empty;
            this.openFlag = string.Empty;
            this.registerUserId = string.Empty;
            this.netName = string.Empty;
        }

        public CashDrawerVO(string id, string name, string openFlag, string registerUserId,
            string netName)
        {
            this.id = id;
            this.name = name;
            this.openFlag = openFlag;
            this.registerUserId = registerUserId;
            this.netName = netName;
        }
    }
}
