using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Common.Libraries.Objects.Authorization
{
    public class UserVO
    {
        [Browsable(false)]
        public string UserID
        {
            get;
            set;
        }
        [Browsable(true)]
        public string UserName
        {
            get;
            set;
        }
        [Browsable(true)]
        public string UserFirstName
        {
            get;
            set;
        }
        [Browsable(true)]
        public string UserLastName
        {
            get;
            set;
        }
        [Browsable(false)]
        public string StoreNumber
        {
            get;
            set;
        }
        [Browsable(false)]
        public List<ResourceVO> UserResources
        {
            get;
            set;
        }
        [Browsable(false)]
        public RoleVO UserRole
        {
            get;
            set;
        }
        [Browsable(false)]
        public List<LimitsVO> UserLimits
        {
            get;
            set;
        }
        [Browsable(true)]
        public bool ShopLevelUser
        {
            get;
            set;
        }
        [Browsable(false)]
        public string UserCurrentPassword
        {
            get;
            set;
        }
        [Browsable(false)]
        public List<string> UserPasswordHistory
        {
            get;
            set;
        }
        [Browsable(false)]
        public string UserDN
        {
            get;
            set;
        }
        [Browsable(true)]
        public string FacNumber
        {
            get;
            set;
        }
        [Browsable(true)]
        public string EmployeeNumber
        {
            get;
            set;
        }
        [Browsable(false)]
        public DateTime LastUpdatedDate
        {
            get;
            set;
        }
        [Browsable(false)]
        public List<string> ProfileStores
        {
            get;
            set;
        }



        public UserVO()
        {
            UserLimits = new List<LimitsVO>();
            UserResources = new List<ResourceVO>();
            UserPasswordHistory = new List<string>();
        }

        

        
    }

 
}