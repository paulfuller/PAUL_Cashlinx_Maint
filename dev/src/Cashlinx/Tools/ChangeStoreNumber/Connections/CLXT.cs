
namespace ChangeStoreNumber.Connections
{
    public class CLXT : DatabaseConnection
    {
        public CLXT()
        {
            this.DisplayName = "CLXT";
            this.PawnSecConnectionString = "user id=pawnsec;password=pawnsec08;data source=clxdbqa.casham.com:1523/clxt.casham.com";
        }
    }
}
