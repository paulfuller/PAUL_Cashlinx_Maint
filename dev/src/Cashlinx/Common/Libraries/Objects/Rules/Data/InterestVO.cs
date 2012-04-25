using System;

namespace Common.Libraries.Objects.Rules.Data
{
    [Serializable()]
    public class InterestVO
    {
        public AliasVO Alias         { set; get; }
        public decimal MinAmount      { set; get; }
        public decimal MaxAmount      { set; get; }
        public decimal InterestRate   { set; get; }
        public decimal InterestAmount { set; get; }
        public decimal ServiceRate    { set; get; }
        public decimal ServiceAmount  { set; get; }
        public string InterestLevel  { set; get; }
        public string InterestType   { set; get; }

        public InterestVO()
        {
            this.Alias = new AliasVO();
            this.MinAmount = 0.0M;
            this.MaxAmount = 0.0M;
            this.InterestRate = 0.0M;
            this.InterestAmount = 0.0M;
            this.ServiceAmount = 0.0M;
            this.ServiceRate = 0.0M;
            this.InterestLevel = string.Empty;
            this.InterestType = string.Empty;
        }
    }
}
