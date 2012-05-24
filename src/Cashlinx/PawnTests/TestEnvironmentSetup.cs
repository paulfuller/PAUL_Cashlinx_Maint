using System;
using System.IO;
using Common.Controllers.Application;
using Common.Controllers.Rules.Data;
using Common.Libraries.Objects.Config;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.String;
using NUnit.Framework;
using PawnTests.TestEnvironment;

namespace PawnTests
{
    [SetUpFixture]
    public class TestEnvironmentSetup
    {
        [SetUp]
        protected void RunBeforeAnyTests()
        {
            TestEnvironmentInfo.Instance.Initialize(Environment.CurrentDirectory);
            RulesHelper.SetWorkingDirectory(TestEnvironmentInfo.Instance.OutputDirectory);
            SetupDesktopSession();
            SetupFileLogger();
        }

        [TearDown]
        protected void RunAfterAnyTests()
        {
            if (FileLogger.Instance != null)
            {
                FileLogger.Instance.Dispose();
            }
        }

        private void SetupFileLogger()
        {
            var fileLogger = FileLogger.Instance;
            var logDirectory = TestEnvironmentInfo.Instance.LogDirectory;

            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }

            var logFileName = string.Format("{0}\\{1}_{2}", logDirectory, "PawnTests", FileLogger.FILENAME);
            fileLogger.initializeLogger(
                logFileName,
                DefaultLoggerHandlers.defaultLogLevelCheckHandler,
                DefaultLoggerHandlers.defaultLogLevelGenerator,
                DefaultLoggerHandlers.defaultDateStampGenerator,
                DefaultLoggerHandlers.defaultLogMessageHandler,
                DefaultLoggerHandlers.defaultLogMessageFormatHandler);
            fileLogger.setLogLevel(LogLevel.INFO);
            var asteriskString = StringUtilities.fillString("*", 150);
            fileLogger.logMessage(LogLevel.INFO, this, asteriskString);
            fileLogger.logMessage(
                LogLevel.INFO, this, "TestFileLogger Initialized at {0} - {1} for {2}",
                DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), "NUnit");
            fileLogger.logMessage(LogLevel.INFO, this, asteriskString);
            fileLogger.flush();
        }

        private void SetupDesktopSession()
        {
            var ds = GlobalDataAccessor.Instance.DesktopSession = new TestDesktopSession();
            ds.CurrentSiteId = TestSiteIds.Store00152;
            ds.LaserPrinter = new StorePrinterVO(StorePrinterVO.StorePrinterType.LASER, "192.168.116.49", 9100);
            ds.Setup(null);
        }
    }
}