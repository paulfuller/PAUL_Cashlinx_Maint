using Common.Libraries.Utility.Shared;

namespace Common.Libraries.Objects.Authorization
{
    public class ResourceVO
    {
        public string StoreNumber { get; set; }
        public string ResourceID { get; set; }
        public string ResourceName { get; set; }
        public string ResourceMask { get; set; }
        public ResourceSecurityMask SecurityMask { get; set; }
        public string Assigned { get; set; }
    }
}
