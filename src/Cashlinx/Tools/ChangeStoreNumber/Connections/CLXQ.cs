
namespace ChangeStoreNumber.Connections
{
    public class CLXQ : DatabaseConnection
    {
        public CLXQ()
        {
            this.DisplayName = "CLXQ";
            this.PawnSecConnectionString = "user id=pawnsec;password=pawnsec08;data source=clxdbqa.casham.com:1523/CLXQ.casham.com";
        }
    }
}
