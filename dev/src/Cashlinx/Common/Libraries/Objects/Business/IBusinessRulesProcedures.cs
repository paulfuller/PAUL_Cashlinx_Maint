namespace Common.Libraries.Objects.Business
{
    public interface IBusinessRulesProcedures
    {
        bool GetMaxLoanLimit(SiteId siteID, out decimal maxLoanLimit);
    }
}
