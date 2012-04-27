using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Controllers.Application;
using Common.Controllers.Database.Procedures;
using NUnit.Framework;
using Common.Libraries.Objects;

namespace PawnTests.BusinessRules
{
    public abstract class BusinessRulesBaseTest
    {
        protected BusinessRulesProcedures BusinessRulesProcedures { get; private set; }

        protected SiteId CurrentSiteId
        {
            get { return GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId; }
        }

        [SetUp]
        public void BaseSetup()
        {
            Setup();
            BusinessRulesProcedures = new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession);
        }

        [TearDown]
        public void BaseTeardown()
        {
            Teardown();
        }

        protected abstract void Setup();
        protected abstract void Teardown();
    }
}
