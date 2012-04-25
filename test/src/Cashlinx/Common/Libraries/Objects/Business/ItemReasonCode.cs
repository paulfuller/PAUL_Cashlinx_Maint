using Common.Libraries.Objects.Authorization;
using Common.Libraries.Utility.Shared;

namespace Common.Libraries.Objects.Business
{
    public class ItemReasonCode
    {
        public ItemReasonCode()
            : this(ItemReason.BLNK, string.Empty)
        {

        }

        public ItemReasonCode(ItemReason itemReason, string description)
        {
            AllowedApplications = PawnSecApplication.None;
            Reason = itemReason;
            Description = description;
        }

        public PawnSecApplication AllowedApplications { get; set; }
        public bool ChargeOff { get; set; }
        public string Description { get; set; }
        public bool FirearmReason { get; set; }
        public ItemReason Reason { get; set; }

        public bool IsApplicationAllowed(PawnSecApplication application)
        {
            return (AllowedApplications & application) != 0;
        }
    }
}
