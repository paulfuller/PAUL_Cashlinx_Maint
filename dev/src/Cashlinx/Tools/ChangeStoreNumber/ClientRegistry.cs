
namespace ChangeStoreNumber
{
    public class ClientRegistry
    {
        public int CurrentStoreId { get; set; }
        public int Id { get; set; }
        public string FullMachineName { get; set; }
        public string MachineName
        {
            get { return GetMachineName(); }
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(FullMachineName))
            {
                return base.ToString();
            }

            return FullMachineName;
        }

        private string GetMachineName()
        {
            if (string.IsNullOrEmpty(FullMachineName))
            {
                return null;
            }

            int idx = FullMachineName.IndexOf('.');

            if (idx == -1)
            {
                return null;
            }

            return FullMachineName.Substring(0, idx);
        }
    }
}
