
namespace ChangeStoreNumber.Connections
{
    public class CLXD2 : DatabaseConnection
    {
        public CLXD2()
        {
            this.DisplayName = "CLXD2";
            this.PawnSecConnectionString = "user id=pawnsec;password=pawnsec08;data source=clxdbdev.casham.com:1521/clxd2.casham.com";
        }
    }
}
