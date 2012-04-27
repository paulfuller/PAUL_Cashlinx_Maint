using System.IO;

namespace Cashlinx.Build.Tasks
{
    public class ProjectEnvironment
    {
        public string BinDirectory { get; set; }
        public string BuildFile { get; set; }
        public bool BuildFileExists { get; set; }
        public string DistDirectory { get; set; }
        public string DistLogsDirectory { get; set; }
        public string DistMediaDirectory { get; set; }
        public string DistTemplatesDirectory { get; set; }
        public string SourceDirectory { get; set; }
        public string StagingDirectory { get; set; }
        public string UnitTestsDirectory { get; set; }
        public string UnitTestsFile { get; set; }

        public bool DoesBuildFileExist()
        {
            return File.Exists(BuildFile);
        }
    }
}
