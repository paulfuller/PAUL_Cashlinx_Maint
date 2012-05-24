
namespace ChangeStoreNumber.Connections
{
    public class CLXD3 : DatabaseConnection
    {
        public CLXD3()
        {
            this.DisplayName = "CLXD3";
            this.PawnSecConnectionString = "user id=pawnsec;password=pawnsec08;data source=clxdbdev.casham.com:1521/clxd3.casham.com";
        }
    }
}
