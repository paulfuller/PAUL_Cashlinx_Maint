using System.Collections.Generic;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Rules.Structure;

namespace Common.Controllers.Rules.Interface
{
    public interface PawnRulesSystemInterface
    {
        /// <summary>
        /// Begin site method - call prior to getParameters
        /// </summary>
        /// <param name="caller">Calling class object</param>
        /// <param name="siteId">SiteId specifier object</param>
        /// <param name="ruleIds">List of Business rule strings</param>
        /// <param name="rules">List of BusinessRuleVO objects - Output param</param>
        /// <returns>PawnRulesSystemReturnCode</returns>
        PawnRulesSystemReturnCode beginSite(
            object caller, 
            SiteId siteId, 
            List<string> ruleIds, 
            out Dictionary<string, BusinessRuleVO> rules);

        /// <summary>
        /// End site method - call after done with getParameters
        /// </summary>
        /// <param name="caller">Calling class object</param>
        /// <param name="siteId">SiteId specifier object</param>
        /// <returns>PawnRulesSystemReturnCode</returns>
        PawnRulesSystemReturnCode endSite(
            object caller, 
            SiteId siteId);

        /// <summary>
        /// Get parameters method - call between beginSite and endSite
        /// </summary>
        /// <param name="caller">Calling class object</param>
        /// <param name="siteId">SiteId specifier object</param>
        /// <param name="rules" >List of BusinessRuleVO objects - </param>
        /// <returns></returns>
        PawnRulesSystemReturnCode getParameters(
            object caller, 
            SiteId siteId, 
            ref Dictionary<string, BusinessRuleVO> rules);

        /// <summary>
        /// Get parameter method - call between beginSite and endSite
        /// </summary>
        /// <param name="caller">Calling class object</param>
        /// <param name="siteId">SiteId specifier object</param>
        /// <param name="paramKey">Non-business rule param key</param>
        /// <param name="values">List of param values with associated aliases</param>
        /// <returns></returns>
        PawnRulesSystemReturnCode getParameter(
            object caller,
            SiteId siteId,
            string paramKey,
            ref Dictionary<string, string> values);
    }
}
