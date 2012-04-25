using Common.Libraries.Objects.Business;

namespace Common.Libraries.Objects.Audit
{
    public class TrakkerItem
    {
        private const string DEFAULT_TRAC_LOC_CODE = "X";

        public TrakkerItem()
        {
            LocationCode = DEFAULT_TRAC_LOC_CODE;
        }

        public int CatCode { get; set; }
        public string ChargeOffReason { get; set; }
        public string Description { get; set; }
        public int DispDoc { get; set; }
        public long GunNumber { get; set; }
        public int HomeOffice { get; set; }
        public Icn Icn { get; set; }
        public bool IsGun { get; set; }
        public bool IsJewelry { get; set; }
        public string Location { get; set; }
        public string LocationCode { get; set; }
        public string MerchandiseType { get; set; }
        public decimal PfiAmount { get; set; }
        public string PreviousLocation { get; set; }
        public int Quantity { get; set; }
        public int RecordNumber { get; set; }
        public decimal RetailAmount { get; set; }
        public int RfbNo { get; set; }
        public int SequenceNumber { get; set; }
        public string Status { get; set; }
        public string TempStatus { get; set; }
        public int TrakkerId { get; set; }
        public char TrakFlag { get; set; }
        public char TrakNewFlag { get; set; }
        public Icn XIcn { get; set; }

        public void CreateTrackFlag()
        {
            switch (Status)
            {
                case "PUR":
                case "LREC":
                case "LAY":
                case "OS":
                    TrakFlag = 'F';
                    break;
                default:
                    TrakFlag = 'N';
                    break;
            }
        }

        public string CreateTrackLocation()
        {
            return string.Format("{0,-4}{1,6:000000}", Status, DispDoc);
        }

        public ProcessMissingIndicator GetProcessMissingIndicator()
        {
            if (TrakNewFlag.Equals(TrakFlagConstants.FOUND))
            {
                if (string.IsNullOrWhiteSpace(TempStatus))
                {
                    return ProcessMissingIndicator.Found;
                }
                else
                {
                    return ProcessMissingIndicator.Reconciled;
                }
            }
            else if (TempStatus.Equals("COFF"))
            {
                return ProcessMissingIndicator.ChargedOff;
            }
            else
            {
                return ProcessMissingIndicator.Missing;
            }
        }

        public ProcessUnexpectedIndicator GetProcessUnexpectedIndicator()
        {
            if (TrakNewFlag.Equals('C') && TempStatus.Equals("PFI"))
            {
                return ProcessUnexpectedIndicator.Reactivated;
            }
            else if (Status.Equals("CON"))
            {
                return ProcessUnexpectedIndicator.ChargedOn;
            }
            else if (TrakNewFlag.Equals('N'))
            {
                return ProcessUnexpectedIndicator.Unscanned;
            }
            return ProcessUnexpectedIndicator.Unexpected;
        }
    }
}
