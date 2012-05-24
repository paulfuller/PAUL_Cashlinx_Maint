using Common.Libraries.Utility.Shared;

namespace Common.Libraries.Objects.Business
{
    public class TenderEntryVO
    {
        public TenderTypes TenderType { get; set; }
        public string ReferenceNumber { get; set; }
        public decimal Amount { get; set; }
        public string CardTypeString { get; set; }
        public CreditCardTypes CreditCardType { get; set; }
        public DebitCardTypes DebitCardType { get; set; }

        public TenderEntryVO()
        {
            TenderType = TenderTypes.CASHIN;
            ReferenceNumber = string.Empty;
            Amount = 0.0M;
            CreditCardType = CreditCardTypes.VISA;
            DebitCardType = DebitCardTypes.VISA;
            CardTypeString = string.Empty;
        }
    }
}
