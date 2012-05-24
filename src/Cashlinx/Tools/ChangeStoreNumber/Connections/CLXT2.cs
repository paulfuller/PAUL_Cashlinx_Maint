
namespace ChangeStoreNumber.Connections
{
    public class CLXT2 : DatabaseConnection
    {
        public CLXT2()
        {
            this.DisplayName = "CLXT2";
            this.PawnSecConnectionString = "user id=pawnsec;password=pawnsec08;data source=clxdbqa.casham.com:1523/clxt2.casham.com";
        }
    }
}
