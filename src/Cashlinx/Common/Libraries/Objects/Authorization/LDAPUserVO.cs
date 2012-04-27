using System.ComponentModel;

namespace Common.Libraries.Objects.Authorization
{
    public class LDAPUserVO
    {
        [Category("Credentials")]
        [Description("Employee's Login Name")]
        [Browsable(true)]
        public string UserName
        {
            set;
            get;
        }
        [Category("Credentials")]
        [Browsable(true)]
        [Description("Employee's Login Password")]
        [PasswordPropertyText(true)]
        public string Password
        {
            set;
            get;
        }
        [Category("Employee Data")]
        [Description("Employee name to display")]
        [Browsable(true)]
        public string DisplayName
        {
            set;
            get;
        }
        [Category("Employee Data")]
        [Description("Official company employee number")]
        [Browsable(true)]
        public string EmployeeNumber
        {
            set;
            get;
        }
        [Category("Employee Data")]
        [Description("Title of employee's position")]
        [Browsable(true)]
        public string EmployeeType
        {
            set;
            get;
        }

        [Category("Employee Data")]
        [Description("Employee's Home Store Number")]
        [Browsable(true)]
        public string EmployeeHomeStore
        {
            set; 
            get;
        }

        public LDAPUserVO()
        {
            this.UserName = string.Empty;
            this.Password = string.Empty;
            this.DisplayName = string.Empty;
            this.EmployeeNumber = string.Empty;
            this.EmployeeType = string.Empty;
            this.EmployeeHomeStore = string.Empty;
            //this.RoleId = string.Empty;
        }
    }
}
