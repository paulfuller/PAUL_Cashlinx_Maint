using System.IO;

namespace PawnTests.TestEnvironment
{
    public class TestEnvironmentInfo
    {
        private static TestEnvironmentInfo instance = new TestEnvironmentInfo();

        public static TestEnvironmentInfo Instance
        {
            get { return instance; }
        }

        public string LogDirectory { get; private set; }
        public string OutputDirectory { get; private set; }
        public string ProjectDirectory { get; private set; }

        public void Initialize(string currentWorkingDirectory)
        {
            var directoryInfo = new DirectoryInfo(currentWorkingDirectory);

            if (!directoryInfo.Exists)
            {
                return;
            }

            OutputDirectory = directoryInfo.FullName;
            LogDirectory = Path.Combine(OutputDirectory, "Logs");
            ProjectDirectory = directoryInfo.Parent.Parent.FullName;
        }
    }
}
