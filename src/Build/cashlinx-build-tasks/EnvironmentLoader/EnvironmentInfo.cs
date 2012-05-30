using System;
using System.IO;
using System.Net;
using NAnt.Core;

namespace Cashlinx.Build.Tasks.EnvironmentLoader
{
    public class EnvironmentInfo : Singleton<EnvironmentInfo>
    {
        public EnvironmentInfo()
        {
            AuditProjectEnvironment = new ProjectEnvironment();
            AuditQueriesProjectEnvironment = new ProjectEnvironment();
            CommonProjectEnvironment = new ProjectEnvironment();
            DstrViewerProjectEnvironment = new ProjectEnvironment();
            PawnProjectEnvironment = new ProjectEnvironment();
            SupportProjectEnvironment = new ProjectEnvironment();
            DeploymentType = DeploymentType.Unknown;
        }

        public void LoadEnvironmentInfo(Project project, string nantAppDirectory, string nantProjectDirectory, string distRootParent)
        {
            Project = project;
            NantAppDirectory = nantAppDirectory;
            NantProjectDirectory = nantProjectDirectory;
            DistRootParent = distRootParent;

            DeploymentType = GetAutomaticDeploymentType();

            BranchBuildDirectory = Path.Combine(Directory.GetParent(nantProjectDirectory).Parent.FullName, @"src\build");
            BranchBuildFilesDirectory = Path.Combine(BranchBuildDirectory, @"build-files");

            ToolsDirectory = Directory.GetParent(NantAppDirectory).Parent.Parent.FullName;
            BranchName = Directory.GetParent(NantProjectDirectory).Parent.Name;
            MachineName = Dns.GetHostName().ToUpper();
            NantContribDirectory = Path.Combine(ToolsDirectory, @"nant\nant-contrib\bin");
            NUnitDirectory = Path.Combine(ToolsDirectory, @"nunit");
            WorkspaceName = GetWorkspaceName();
            SqlVersionTemplatePath = Path.Combine(BranchBuildDirectory, @"etc\version_template.sql");

            AuditProjectEnvironment.BuildFile = Path.Combine(BranchBuildFilesDirectory, "Audit.build");
            AuditQueriesProjectEnvironment.BuildFile = Path.Combine(BranchBuildFilesDirectory, "AuditQueries.build");
            CommonProjectEnvironment.BuildFile = Path.Combine(BranchBuildFilesDirectory, "Common.build");
            DstrViewerProjectEnvironment.BuildFile = Path.Combine(BranchBuildFilesDirectory, "DstrViewer.build");
            PawnProjectEnvironment.BuildFile = Path.Combine(BranchBuildFilesDirectory, "Pawn.build");
            SupportProjectEnvironment.BuildFile = Path.Combine(BranchBuildFilesDirectory, "Support.build");

            AuditProjectEnvironment.BuildFileExists = AuditProjectEnvironment.DoesBuildFileExist();
            AuditQueriesProjectEnvironment.BuildFileExists = AuditQueriesProjectEnvironment.DoesBuildFileExist();
            DstrViewerProjectEnvironment.BuildFileExists = DstrViewerProjectEnvironment.DoesBuildFileExist();
            PawnProjectEnvironment.BuildFileExists = PawnProjectEnvironment.DoesBuildFileExist();
            SupportProjectEnvironment.BuildFileExists = SupportProjectEnvironment.DoesBuildFileExist();

            AuditProjectEnvironment.SourceDirectory = Path.Combine(NantProjectDirectory, "Audit");
            AuditProjectEnvironment.BinDirectory = Path.Combine(AuditProjectEnvironment.SourceDirectory, @"bin");

            AuditQueriesProjectEnvironment.SourceDirectory = Path.Combine(NantProjectDirectory, "AuditQueries");
            AuditQueriesProjectEnvironment.BinDirectory = Path.Combine(AuditQueriesProjectEnvironment.SourceDirectory, @"bin");

            PawnProjectEnvironment.SourceDirectory = Path.Combine(NantProjectDirectory, "Pawn");
            PawnProjectEnvironment.BinDirectory = Path.Combine(PawnProjectEnvironment.SourceDirectory, @"bin");

            SupportProjectEnvironment.SourceDirectory = Path.Combine(NantProjectDirectory, "Support");
            SupportProjectEnvironment.BinDirectory = Path.Combine(SupportProjectEnvironment.SourceDirectory, @"bin");

            DstrViewerProjectEnvironment.SourceDirectory = Path.Combine(NantProjectDirectory, "Tools\\DstrViewer");
            DstrViewerProjectEnvironment.BinDirectory = Path.Combine(DstrViewerProjectEnvironment.SourceDirectory, @"bin");

            PawnProjectEnvironment.UnitTestsDirectory = Path.Combine(NantProjectDirectory, "PawnTests");
            PawnProjectEnvironment.UnitTestsFile = "PawnTests.nunit";

            CommonProjectEnvironment.UnitTestsDirectory = Path.Combine(NantProjectDirectory, "CommonTests");
            CommonProjectEnvironment.UnitTestsFile = "CommonTests.nunit";

            DistBuildDirectory = Path.Combine(DistRoot, "Build");
            DistResultsDirectory = Path.Combine(DistRoot, "Results");
            DistOracleDirectory = Path.Combine(DistRoot, "Oracle");
            DistOraclePackagesDirectory = Path.Combine(DistOracleDirectory, "Packages");

            AuditProjectEnvironment.DistDirectory = Path.Combine(DistBuildDirectory, @"c\Program Files\AuditApp");
            AuditProjectEnvironment.DistLogsDirectory = Path.Combine(AuditProjectEnvironment.DistDirectory, "logs");
            AuditProjectEnvironment.DistMediaDirectory = Path.Combine(AuditProjectEnvironment.DistDirectory, "media");
            AuditProjectEnvironment.DistTemplatesDirectory = Path.Combine(AuditProjectEnvironment.DistDirectory, "templates");

            AuditQueriesProjectEnvironment.DistDirectory = Path.Combine(DistBuildDirectory, @"c\Program Files\AuditQueriesApp");
            AuditQueriesProjectEnvironment.DistLogsDirectory = Path.Combine(AuditQueriesProjectEnvironment.DistDirectory, "logs");
            AuditQueriesProjectEnvironment.DistMediaDirectory = Path.Combine(AuditQueriesProjectEnvironment.DistDirectory, "media");
            AuditQueriesProjectEnvironment.DistTemplatesDirectory = Path.Combine(AuditQueriesProjectEnvironment.DistDirectory, "templates");

            PawnProjectEnvironment.DistDirectory = Path.Combine(DistBuildDirectory, @"c\Program Files\Phase2App");
            PawnProjectEnvironment.DistLogsDirectory = Path.Combine(PawnProjectEnvironment.DistDirectory, "logs");
            PawnProjectEnvironment.DistMediaDirectory = Path.Combine(PawnProjectEnvironment.DistDirectory, "media");
            PawnProjectEnvironment.DistTemplatesDirectory = Path.Combine(PawnProjectEnvironment.DistDirectory, "templates");

            SupportProjectEnvironment.DistDirectory = Path.Combine(DistBuildDirectory, @"c\Program Files\SupportApp");
            SupportProjectEnvironment.DistLogsDirectory = Path.Combine(SupportProjectEnvironment.DistDirectory, "logs");
            SupportProjectEnvironment.DistMediaDirectory = Path.Combine(SupportProjectEnvironment.DistDirectory, "media");
            SupportProjectEnvironment.DistTemplatesDirectory = Path.Combine(SupportProjectEnvironment.DistDirectory, "templates");

            DstrViewerProjectEnvironment.DistDirectory = Path.Combine(DistBuildDirectory, @"c\Program Files\DSTRViewer");
            DstrViewerProjectEnvironment.DistLogsDirectory = Path.Combine(DstrViewerProjectEnvironment.DistDirectory, "logs");
            DstrViewerProjectEnvironment.DistMediaDirectory = Path.Combine(DstrViewerProjectEnvironment.DistDirectory, "media");
            DstrViewerProjectEnvironment.DistTemplatesDirectory = Path.Combine(DstrViewerProjectEnvironment.DistDirectory, "templates");

            if (Project.Properties.Contains("devmode") && Project.Properties["devmode"].Equals("true"))
            {
                DevMode = true;
                AuditProjectEnvironment.StagingDirectory = @"C:\tmp\Staging\Audit\Incoming";
                AuditQueriesProjectEnvironment.StagingDirectory = @"C:\tmp\Staging\AuditQueries\Incoming";
                PawnProjectEnvironment.StagingDirectory = @"C:\tmp\Staging\Pawn\Incoming";
                SupportProjectEnvironment.StagingDirectory = @"C:\tmp\Staging\Support\Incoming";
                DstrViewerProjectEnvironment.StagingDirectory = @"C:\tmp\Staging\DstrViewer\Incoming";
            }
            else
            {
                DevMode = false;
                AuditProjectEnvironment.StagingDirectory = @"\\FTW2.casham.com\clx_audit_deploy$\Incoming";
                AuditQueriesProjectEnvironment.StagingDirectory = @"\\ftw2.casham.com\audit_query_deploy$\Incoming";
                PawnProjectEnvironment.StagingDirectory = @"\\FTW2.casham.com\clx_deploy$\Incoming";
                SupportProjectEnvironment.StagingDirectory = @"\\FTW2.casham.com\clx_support_deploy$\Incoming";
                DstrViewerProjectEnvironment.StagingDirectory = @"\\FTW2.casham.com\dstr_deploy$\Incoming";
            }

            StagingEmailListTo = "jmunta@casham.com;lfranks@casham.com";

            UpdateBranchEmailListTo = "jmunta@casham.com;pbccr@casham.com;tmcconnell@casham.com;amadueke@casham.com;dgilden@casham.com;srengarajan@casham.com;dstandley@casham.com;ewaltmon@casham.com;hkollipara@casham.com;eguillory@casham.com;ascribner@casham.com;jyandell@casham.com;mholder@casham.com;bmcvey@casham.com;mveldanda@casham.com;rbrickler@casham.com;nsiddaiah@casham.com";
            UpdateBranchEmailListCc = "mmoorman@casham.com;tlepage@casham.com;glepage@casham.com";

            if (DeploymentType == DeploymentType.MaintDev)
            {
                StagingEmailListTo += ";tmcconnell@casham.com";
            }
        }

        public ProjectEnvironment AuditProjectEnvironment { get; private set; }
        public ProjectEnvironment AuditQueriesProjectEnvironment { get; private set; }
        public ProjectEnvironment CommonProjectEnvironment { get; private set; }
        public ProjectEnvironment DstrViewerProjectEnvironment { get; private set; }
        public ProjectEnvironment PawnProjectEnvironment { get; private set; }
        public ProjectEnvironment SupportProjectEnvironment { get; private set; }

        public string DistRootParent { get; set; }
        public string NantAppDirectory { get; private set; }
        public string NantProjectDirectory { get; private set; }
        public Project Project { get; private set; }

        public string BranchBuildDirectory { get; private set; }
        public string BranchBuildFilesDirectory { get; private set; }
        public string BranchName { get; set; }
        public DeploymentType DeploymentType { get; set; }
        public string DistRoot
        {
            get { return Path.Combine(DistRootParent, BranchName); }
        }
        public string DistBuildDirectory { get; set; }
        public string DistOracleDirectory { get; set; }
        public string DistOraclePackagesDirectory { get; set; }
        public string DistResultsDirectory { get; set; }
        public string MachineName { get; set; }
        public string NantContribDirectory { get; set; }
        public string NUnitDirectory { get; set; }
        public string SqlVersionTemplatePath { get; set; }
        public bool DevMode { get; set; }
        public string ToolsDirectory { get; set; }
        public string ToolsEtcDirectory
        {
            get { return Path.Combine(ToolsDirectory, "Etc"); }
        }
        public string WorkspaceName { get; set; }

        public string UpdateBranchEmailListCc { get; set; }
        public string StagingEmailListTo { get; set; }
        public string UpdateBranchEmailListTo { get; set; }

        public void Log(Task task, Level level)
        {
            var utilities = new UtilityFunctions(task.Project, task.Project.Properties);

            task.Log(level, "Branch Build Directory: " + BranchBuildDirectory);
            task.Log(level, "Tools Directory: " + ToolsDirectory);
            task.Log(level, "Nant App Directory: " + NantAppDirectory);
            task.Log(level, "Nant Project Directory: " + NantProjectDirectory);
            task.Log(level, "Branch Name: " + BranchName);
            task.Log(level, "Dist root: " + DistRoot);
            task.Log(level, "Machine Name: " + MachineName);
            task.Log(level, "P4 Workspace: " + WorkspaceName);
            task.Log(level, "NUnit Directory: " + NUnitDirectory);
            task.Log(level, "Pawn Staging Directory: " + PawnProjectEnvironment.StagingDirectory);
            task.Log(level, "Audit Staging Directory: " + AuditProjectEnvironment.StagingDirectory);
            task.Log(level, "Audit Queries Staging Directory: " + AuditQueriesProjectEnvironment.StagingDirectory);
            task.Log(level, "Support Staging Directory: " + SupportProjectEnvironment.StagingDirectory);
            task.Log(level, "DSTR Viewer Staging Directory: " + DstrViewerProjectEnvironment.StagingDirectory);
            task.Log(level, "Deployment Type: " + DeploymentType.ToString());
            task.Log(level, "Full Name: " + utilities.GetUserFullName());
            task.Log(level, "Email Address: " + utilities.GetUserEmailAddress());
            if (DevMode)
            {
                task.Log(level, "**** DEV MODE ****");
            }
            Console.WriteLine();
        }

        # region Helper Methods

        private DeploymentType GetAutomaticDeploymentType()
        {
            if (Project.Properties.Contains("deployment.type"))
            {
                if (Project.Properties["deployment.type"].Equals(DeploymentType.NewDev.ToString()))
                {
                    return DeploymentType.NewDev;
                }
                if (Project.Properties["deployment.type"].Equals(DeploymentType.MaintDev.ToString()))
                {
                    return DeploymentType.MaintDev;
                }
            }

            if (string.IsNullOrWhiteSpace(NantProjectDirectory))
            {
                return DeploymentType.Unknown;
            }

            if (NantProjectDirectory.ToLower().EndsWith(@"dev\src\cashlinx"))
            {
                return DeploymentType.NewDev;
            }
            if (NantProjectDirectory.ToLower().EndsWith(@"dev-maint\src\cashlinx"))
            {
                return DeploymentType.MaintDev;
            }
            if (NantProjectDirectory.ToLower().EndsWith(@"test\src\cashlinx"))
            {
                return DeploymentType.NewDev;
            }
            if (NantProjectDirectory.ToLower().EndsWith(@"test-maint\src\cashlinx"))
            {
                return DeploymentType.MaintDev;
            }
            if (NantProjectDirectory.ToLower().EndsWith(@"champ\src\cashlinx"))
            {
                return DeploymentType.NewDev;
            }
            if (NantProjectDirectory.ToLower().EndsWith(@"chuff\src\cashlinx"))
            {
                return DeploymentType.MaintDev;
            }
            return DeploymentType.Unknown;
        }

        private string GetNunitDirectory()
        {
            const string suffix = @"NUnit 2.5.10\bin\net-2.0";
            string programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            string programFilesX86 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);

            if (Directory.Exists(Path.Combine(programFiles, suffix)))
            {
                return Path.Combine(programFiles, suffix);
            }
            if (Directory.Exists(Path.Combine(programFilesX86, suffix)))
            {
                return Path.Combine(programFilesX86, suffix);
            }
            if (Directory.Exists(Path.Combine(ToolsDirectory, suffix.Replace(" ", "_"))))
            {
                return Path.Combine(ToolsDirectory, suffix.Replace(" ", "_"));
            }

            throw new BuildException("Unable to locate NUnit installation.");
        }

        private string GetWorkspaceName()
        {
            if (MachineName.Equals("A7136"))
            {
                return "workspace";
            }
            if (MachineName.Equals("A7335"))
            {
                return "new_depot_wkspace";
            }
            return "build_d00d_wk";
        }

        # endregion
    }
}
