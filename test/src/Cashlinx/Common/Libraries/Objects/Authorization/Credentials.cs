using System.Text;

namespace Common.Libraries.Objects.Authorization
{
    /// <summary>
    /// Simple class to hold all necessary database credentials
    /// </summary>
    public class Credentials
    {
        public string UserName { set; get; }
        public string PassWord { set; get; }
        public string DBHost { set; get; }
        public string DBPort { set; get; }
        public string DBService { set; get; }
        public string DBSchema { set; get; }

        public Credentials()
        {
            this.UserName = string.Empty;
            this.PassWord = string.Empty;
            this.DBHost = string.Empty;
            this.DBPort = string.Empty;
            this.DBService = string.Empty;
            this.DBSchema = string.Empty;
        }

        public override string ToString()
        {
            var sb = new StringBuilder(64);
            sb.Append("User: " + this.UserName);
#if DEBUG
            sb.Append(", Pass: " + this.PassWord);
#endif
            sb.Append("Host: " + this.DBHost);
            sb.Append("Port: " + this.DBPort);
            sb.Append("Service: " + this.DBService);
            sb.AppendLine("Schema : " + this.DBSchema);
            return (sb.ToString());
        }
    }
}
