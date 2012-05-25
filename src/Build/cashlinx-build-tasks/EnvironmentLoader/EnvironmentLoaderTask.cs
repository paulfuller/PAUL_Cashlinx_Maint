using Cashlinx.Build.Tasks.EnvironmentLoader;
using NAnt.Core;
using NAnt.Core.Attributes;
using System;

namespace Cashlinx.Build.Tasks
{
    [TaskName("environmentloader")]
    public class EnvironmentLoaderTask : Task
    {
        [TaskAttribute("distrootparent", Required = true)]
        public string DistRootParent { get; set; }

        [TaskAttribute("nantappdirectory", Required = true)]
        public string NantAppDirectory { get; set; }

        protected override void ExecuteTask()
        {
            EnvironmentInfo env = EnvironmentInfo.GetInstance();
            env.LoadEnvironmentInfo(Project, NantAppDirectory, Project.BaseDirectory, DistRootParent);
            env.Log(this, Level.Info);

            SetProperty("buildfile.audit", env.AuditProjectEnvironment.BuildFile);
            SetProperty("buildfile.audit.queries", env.AuditQueriesProjectEnvironment.BuildFile);
            SetProperty("buildfile.common", env.CommonProjectEnvironment.BuildFile);
            SetProperty("buildfile.dstr.viewer", env.DstrViewerProjectEnvironment.BuildFile);
            SetProperty("buildfile.pawn", env.PawnProjectEnvironment.BuildFile);
            SetProperty("buildfile.support", env.SupportProjectEnvironment.BuildFile);
            SetProperty("buildfile.audit.exists", false);
            SetProperty("buildfile.audit.queries.exists", env.AuditQueriesProjectEnvironment.BuildFileExists);
            SetProperty("buildfile.common.exists", env.CommonProjectEnvironment.BuildFileExists);
            SetProperty("buildfile.dstr.viewer.exists", false);
            SetProperty("buildfile.pawn.exists", env.PawnProjectEnvironment.BuildFileExists);
            SetProperty("buildfile.support.exists", false);

            SetProperty("branch.build.dir", env.BranchBuildDirectory);
            SetProperty("branchName", env.BranchName);
            SetProperty("deployment.type", env.DeploymentType.ToString());
            SetProperty("deploy.audit.staging.dir", env.AuditProjectEnvironment.StagingDirectory);
            SetProperty("deploy.audit.queries.staging.dir", env.AuditQueriesProjectEnvironment.StagingDirectory);
            SetProperty("deploy.dstr.viewer.staging.dir", env.DstrViewerProjectEnvironment.StagingDirectory);
            SetProperty("deploy.pawn.staging.dir", env.PawnProjectEnvironment.StagingDirectory);
            SetProperty("deploy.support.staging.dir", env.SupportProjectEnvironment.StagingDirectory);
            SetProperty("dev.mode", env.DevMode);
            SetProperty("dist.build.dir", env.DistBuildDirectory);
            SetProperty("dist.audit.dir", env.AuditProjectEnvironment.DistDirectory);
            SetProperty("dist.audit.logs.dir", env.AuditProjectEnvironment.DistLogsDirectory);
            SetProperty("dist.audit.templates.dir", env.AuditProjectEnvironment.DistTemplatesDirectory);
            SetProperty("dist.audit.queries.dir", env.AuditQueriesProjectEnvironment.DistDirectory);
            SetProperty("dist.audit.queries.logs.dir", env.AuditQueriesProjectEnvironment.DistLogsDirectory);
            SetProperty("dist.audit.queries.templates.dir", env.AuditQueriesProjectEnvironment.DistTemplatesDirectory);
            SetProperty("dist.dstr.viewer.dir", env.DstrViewerProjectEnvironment.DistDirectory);
            SetProperty("dist.dstr.viewer.logs.dir", env.DstrViewerProjectEnvironment.DistLogsDirectory);
            SetProperty("dist.dstr.viewer.templates.dir", env.DstrViewerProjectEnvironment.DistTemplatesDirectory);
            SetProperty("dist.pawn.dir", env.PawnProjectEnvironment.DistDirectory);
            SetProperty("dist.pawn.logs.dir", env.PawnProjectEnvironment.DistLogsDirectory);
            SetProperty("dist.pawn.media.dir", env.PawnProjectEnvironment.DistMediaDirectory);
            SetProperty("dist.pawn.templates.dir", env.PawnProjectEnvironment.DistTemplatesDirectory);
            SetProperty("dist.support.dir", env.SupportProjectEnvironment.DistDirectory);
            SetProperty("dist.support.logs.dir", env.SupportProjectEnvironment.DistLogsDirectory);
            SetProperty("dist.support.media.dir", env.SupportProjectEnvironment.DistMediaDirectory);
            SetProperty("dist.support.templates.dir", env.SupportProjectEnvironment.DistTemplatesDirectory);
            
            SetProperty("dist.oracle.dir", env.DistOracleDirectory);
            SetProperty("dist.oracle.packages.dir", env.DistOraclePackagesDirectory);
            SetProperty("dist.results.dir", env.DistResultsDirectory);
            SetProperty("dist.root.dir", env.DistRoot);
            SetProperty("machine.name", env.MachineName);
            SetProperty("nant.contrib.app.dir", env.NantContribDirectory);
            SetProperty("p4.client.name", env.WorkspaceName);
            SetProperty("src.audit.dir", env.AuditProjectEnvironment.SourceDirectory);
            SetProperty("src.audit.bin.dir", env.AuditProjectEnvironment.BinDirectory);
            SetProperty("src.audit.queries.dir", env.AuditQueriesProjectEnvironment.SourceDirectory);
            SetProperty("src.audit.queries.bin.dir", env.AuditQueriesProjectEnvironment.BinDirectory);
            SetProperty("src.dstr.viewer.dir", env.DstrViewerProjectEnvironment.SourceDirectory);
            SetProperty("src.dstr.viewer.bin.dir", env.DstrViewerProjectEnvironment.BinDirectory);
            SetProperty("src.pawn.dir", env.PawnProjectEnvironment.SourceDirectory);
            SetProperty("src.pawn.bin.dir", env.PawnProjectEnvironment.BinDirectory);
            SetProperty("src.support.dir", env.SupportProjectEnvironment.SourceDirectory);
            SetProperty("src.support.bin.dir", env.SupportProjectEnvironment.BinDirectory);
            SetProperty("src.pawn.unit.tests.dir", env.PawnProjectEnvironment.UnitTestsDirectory);
            SetProperty("src.pawn.unit.tests.file", env.PawnProjectEnvironment.UnitTestsFile);
            SetProperty("src.common.unit.tests.dir", env.CommonProjectEnvironment.UnitTestsDirectory);
            SetProperty("src.common.unit.tests.file", env.CommonProjectEnvironment.UnitTestsFile);
            SetProperty("src.p2app.dir", env.NantProjectDirectory);
            SetProperty("thirdparty.nunit.basedir", env.NUnitDirectory);
            SetProperty("tools.etc.dir", env.ToolsEtcDirectory);
            SetProperty("version_template.sql.file", env.SqlVersionTemplatePath);
        }

        private void SetProperty(string name, string value)
        {
            if (!Project.Properties.Contains(name))
            {
                Project.Properties.Add(name, value);
            }
            else
            {
                try
                {
                    Project.Properties[name] = value;
                }
                catch (Exception exc)
                {
                    Log(Level.Warning, "WARNING: " + exc.Message);
                }
            }
        }

        private void SetProperty(string name, bool value)
        {
            SetProperty(name, value.ToString().ToLower());
        }
    }
}
