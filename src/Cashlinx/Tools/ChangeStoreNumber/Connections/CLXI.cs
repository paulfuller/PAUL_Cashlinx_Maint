
namespace ChangeStoreNumber.Connections
{
    public class CLXI : DatabaseConnection
    {
        public CLXI()
        {
            this.DisplayName = "CLXI";
            this.PawnSecConnectionString = "user id=pawnsec;password=pawnsec08;data source=clxdbdev.casham.com:1521/clxi.casham.com";
        }
    }
}
