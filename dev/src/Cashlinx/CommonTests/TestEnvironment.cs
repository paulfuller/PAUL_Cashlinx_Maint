using NUnit.Framework;

namespace CommonTests
{
    [SetUpFixture]
    public class TestEnvironment
    {
        [SetUp]
        protected void RunBeforeAnyTests()
        {
            // This is used if you need to setup the test environment for entire test suite.
        }

        [TearDown]
        protected void RunAfterAnyTests()
        {
            // This is used if you need to teardown the test environment for entire test suite.
        }
    }
}
