
namespace ChangeStoreNumber.Connections
{
    public class DatabaseConnection
    {
        public DatabaseConnection()
        {
            Selectable = true;
        }

        public string DisplayName { get; set; }
        public string CcsOwnerConnectionString
        {
            get { return GetCcsOwnerConnectionString(); }
        }

        public string PawnSecConnectionString { get; set; }
        public bool Selectable { get; set; }

        private string CachedCcsOwnerConnectionString { get; set; }

        private string GetCcsOwnerConnectionString()
        {
            if (!string.IsNullOrEmpty((CachedCcsOwnerConnectionString)))
            {
                return CachedCcsOwnerConnectionString;
            }

            CachedCcsOwnerConnectionString = Db.GetCcsOwnerConnectionString(this);
            return CachedCcsOwnerConnectionString;
        }
    }
}
