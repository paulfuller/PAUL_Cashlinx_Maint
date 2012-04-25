using System;
using System.Collections.Generic;
using Common.Libraries.Objects.Authorization;
using Common.Libraries.Utility.Shared;

namespace Common.Libraries.Objects.Business
{
    public class ItemReasonFactory : MarshalByRefObject
    {
        # region Singleton Code

        private static ItemReasonFactory itemReasonFactory;
        private static readonly object padlock = new object();

        public static ItemReasonFactory Instance
        {
            get
            {
                lock (padlock)
                {
                    if (itemReasonFactory == null)
                    {
                        itemReasonFactory = new ItemReasonFactory();
                    }
                    return itemReasonFactory;
                }
            }
        }

        static ItemReasonFactory()
        {
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }

        # endregion

        # region Constructors

        public ItemReasonFactory()
        {
            AllReasonCodes = new List<ItemReasonCode>();
            AllReasonCodes.Add(new ItemReasonCode(ItemReason.ADDD, "Added"));
            AllReasonCodes.Add(new ItemReasonCode(ItemReason.COFFBRKN, "Broken") { ChargeOff = true, AllowedApplications = PawnSecApplication.Audit | PawnSecApplication.Cashlinx });
            AllReasonCodes.Add(new ItemReasonCode(ItemReason.COFFBURGROBB, "Burglary / Robbery") { ChargeOff = true, AllowedApplications = PawnSecApplication.Audit, FirearmReason = true });
            AllReasonCodes.Add(new ItemReasonCode(ItemReason.BLNK, ""));
            AllReasonCodes.Add(new ItemReasonCode(ItemReason.CACC, "CACC"));
            AllReasonCodes.Add(new ItemReasonCode(ItemReason.COFFCASUALTY, "Casualty") { ChargeOff = true, AllowedApplications = PawnSecApplication.Audit, FirearmReason = true });
            AllReasonCodes.Add(new ItemReasonCode(ItemReason.COFFLOSTTRNS, "Lost in Transit") { ChargeOff = true, AllowedApplications = PawnSecApplication.Audit, FirearmReason = true });
            AllReasonCodes.Add(new ItemReasonCode(ItemReason.COFFDESTROY, "Destroyed") { ChargeOff = true, AllowedApplications = PawnSecApplication.Audit, FirearmReason = true });
            AllReasonCodes.Add(new ItemReasonCode(ItemReason.COFFDONATION, "Donation") { ChargeOff = true, AllowedApplications = PawnSecApplication.Audit, FirearmReason = true });
            AllReasonCodes.Add(new ItemReasonCode(ItemReason.COFFINTTHEFT, "Internal Theft") { ChargeOff = true, AllowedApplications = PawnSecApplication.Audit, FirearmReason = true });
            AllReasonCodes.Add(new ItemReasonCode(ItemReason.HPFI, "Police Hold"));
            AllReasonCodes.Add(new ItemReasonCode(ItemReason.MERGED, "Merge"));
            AllReasonCodes.Add(new ItemReasonCode(ItemReason.COFFMISSING, "Missing") { ChargeOff = true, AllowedApplications = PawnSecApplication.Audit, FirearmReason = true });
            AllReasonCodes.Add(new ItemReasonCode(ItemReason.NOMD, "No Merchandise"));
            AllReasonCodes.Add(new ItemReasonCode(ItemReason.COFFNXT, "NXT") { ChargeOff = true, AllowedApplications = PawnSecApplication.Cashlinx });
            AllReasonCodes.Add(new ItemReasonCode(ItemReason.COFFREPLPROP, "Replacement of Property") { ChargeOff = true, AllowedApplications = PawnSecApplication.None, FirearmReason = true });
            //AllReasonCodes.Add(new ItemReasonCode(ItemReason.COFFSTLN, "Stolen") { ChargeOff = true, AuditOnly = true });
            AllReasonCodes.Add(new ItemReasonCode(ItemReason.COFFSTRU, "Store Use") { ChargeOff = true, AllowedApplications = PawnSecApplication.Cashlinx });
        }

        # endregion

        # region Properties

        public List<ItemReasonCode> AllReasonCodes { get; private set; }

        # endregion

        # region Public Methods

        public ItemReasonCode FindByDescription(string description)
        {
            return AllReasonCodes.Find(rc => rc.Description == description);
        }

        public ItemReasonCode FindByReason(ItemReason itemReason)
        {
            return AllReasonCodes.Find(rc => rc.Reason == itemReason);
        }

        public ItemReasonCode FindByReason(string itemReason)
        {
            return AllReasonCodes.Find(rc => rc.Reason.ToString().Equals(itemReason));
        }

        public List<ItemReasonCode> GetChargeOffCodes(PawnSecApplication application)
        {
            return GetChargeOffCodes(application, false);
        }

        public List<ItemReasonCode> GetChargeOffCodes(PawnSecApplication application, bool includeFirearmReasonsOnly)
        {
            List<ItemReasonCode> reasons;
            reasons = AllReasonCodes.FindAll(rc => rc.ChargeOff && rc.IsApplicationAllowed(application));

            if (includeFirearmReasonsOnly)
            {
                return reasons.FindAll(rc => rc.FirearmReason);
            }

            return reasons;
        }

        # endregion


        public bool IsChargeOffCode(ItemReason itemReason)
        {
            ItemReasonCode reason = AllReasonCodes.Find(rc => rc.Reason == itemReason);

            if (reason == null)
            {
                return false;
            }

            return reason.ChargeOff;
        }
    }
}
