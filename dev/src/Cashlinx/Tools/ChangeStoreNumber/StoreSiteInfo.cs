
namespace ChangeStoreNumber
{
    public class StoreSiteInfo
    {
        public int AppVersionId { get; set; }
        public int Id { get; set; }
        public string StoreNumber { get; set; }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(StoreNumber))
            {
                return base.ToString();
            }

            return StoreNumber;
        }
    }
}
