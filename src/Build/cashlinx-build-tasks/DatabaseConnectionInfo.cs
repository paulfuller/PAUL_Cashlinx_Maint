
namespace Cashlinx.Build.Tasks
{
    public class DatabaseConnectionInfo
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public string Schema { get; set; }
        public string Port { get; set; }
        public string Host { get; set; }
        public string Service { get; set; }

        public string ConnectionString
        {
            get
            {
                return string.Format("user id={0};password={1};data source={2}:{3}/{4}",
                    UserId, Password, Host, Port, Service);
            }
        }
    }
}
