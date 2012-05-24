using System.Collections.Generic;

namespace ChangeStoreNumber
{
    public class StoreSiteInfoContext
    {
        public StoreSiteInfoContext()
        {
            AppVersions = new Dictionary<string, int>();
        }

        private Dictionary<string, int> AppVersions { get; set; }

        public void AddAppVersionId(string storeNumber, int id)
        {
            if (!AppVersions.ContainsKey(storeNumber))
            {
                AppVersions.Add(storeNumber, 0);
            }

            AppVersions[storeNumber]++;
        }

        public bool HasMultipleAppVersions(string storeNumber)
        {
            return AppVersions.ContainsKey(storeNumber) && AppVersions[storeNumber] > 1;
        }
    }
}
