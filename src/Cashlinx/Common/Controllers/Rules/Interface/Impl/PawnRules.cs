using System.Collections.Generic;
using Common.Libraries.Utility.String;

namespace Common.Controllers.Rules.Interface.Impl
{
    public class PawnRules
    {
        private Dictionary<string, Dictionary<string, List<PawnRule>>> rules;



        /// <summary>
        /// 
        /// </summary>
        public PawnRules()
        {
            this.rules = new Dictionary<string, Dictionary<string, List<PawnRule>>>();
        }

        public Dictionary<string, List<PawnRule>> getRuleList(string alias)
        {
            if (StringUtilities.isEmpty(alias)) return (null);
            if (!this.rules.ContainsKey(alias)) return (null);
            return (this.rules[alias]);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="alias"></param>
        /// <param name="ruleKey"></param>
        /// <returns></returns>
        public List<PawnRule> getRule(string alias, string ruleKey)
        {
            if (StringUtilities.isEmpty(alias) || StringUtilities.isEmpty(ruleKey)) return (null);
            List<PawnRule> rt = null;

            if (this.rules.ContainsKey(alias))
            {
                Dictionary<string, List<PawnRule>> innerRules = this.rules[alias];
                if (innerRules.ContainsKey(ruleKey))
                {
                    rt = innerRules[ruleKey];
                }
            }
            return (rt);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="alias"></param>
        /// <param name="ruleKey"></param>
        /// <param name="rule"></param>
        public void addRule(string alias, string ruleKey, PawnRule rule)
        {
            if (StringUtilities.isEmpty(alias) ||
                StringUtilities.isEmpty(ruleKey) ||
                rule == null)return;
            Dictionary<string, List<PawnRule>> innerRules = null;
            bool createdInner = false;
            bool createdInnerList = false;

            //Get/Create inner rule set for given alias
            if (this.rules.ContainsKey(alias))
            {
                innerRules = this.rules[alias];
            }
            if (innerRules == null)
            {
                innerRules = new Dictionary<string,List<PawnRule>>();
                createdInner = true;
            }

            //Get/Create inner rule list for given rule key
            List<PawnRule> innerRuleList = null;
            if (innerRules.ContainsKey(ruleKey))
            {
                innerRuleList = innerRules[ruleKey];
            }
            if (innerRuleList == null)
            {
                innerRuleList = new List<PawnRule>();
                createdInnerList = true;
            }

            //Add rule
            innerRuleList.Add(rule);

            //Ensure we populate parent lists and maps appropriately
            if (createdInner)
            {
                if (createdInnerList)
                {
                    innerRules.Add(ruleKey, innerRuleList);
                    this.rules.Add(alias, innerRules);
                }
                else
                {
                    this.rules.Add(alias, innerRules);
                }
            }
            else if (createdInnerList)
            {
                innerRules.Add(ruleKey, innerRuleList);
            }
        }
    }
}
