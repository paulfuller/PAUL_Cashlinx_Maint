
namespace ChangeStoreNumber.Connections
{
    public class BlankConnection : DatabaseConnection
    {
        public BlankConnection()
        {
            this.DisplayName = string.Empty;
            this.Selectable = false;
        }
    }
}
