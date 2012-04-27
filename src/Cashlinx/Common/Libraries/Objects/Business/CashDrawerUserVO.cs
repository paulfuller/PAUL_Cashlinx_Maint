namespace Common.Libraries.Objects.Business
{
    public class CashDrawerUserVO
    {
        private string id;
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

        private int userId;
        public int UserId
        {
            get
            {
                return this.userId;
            }
            set
            {
                this.userId = value;
            }
        }

        private string userName;
        public string UserName
        {
            get
            {
                return this.userName;
            }
            set
            {
                this.userName = value;
            }
        }

        private string branchId;
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

        private string connectedId;
        public string ConnectedId
        {
            get
            {
                return (this.connectedId);
            }
            set
            {
                this.connectedId = value;
            }
        }

        private string registerId;
        public string RegisterId
        {
            get
            {
                return (this.registerId);
            }
            set
            {
                this.registerId = value;
            }
        }

        public CashDrawerUserVO()
        {
            this.id = string.Empty;
            this.userId = int.MinValue;
            this.userName = string.Empty;
            this.branchId = string.Empty;
            this.netName = string.Empty;
            this.connectedId = string.Empty;
        }

        public CashDrawerUserVO(string id, int userId, string userName,
            string branchId, string netName)
        {
            this.id = id;
            this.userId = userId;
            this.userName = userName;
            this.branchId = branchId;
            this.netName = netName;
            this.connectedId = string.Empty;
        }
    }
}