using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAnt.Core;
using NAnt.Core.Attributes;
using System.IO;

namespace Cashlinx.Build.Tasks
{
    [TaskName("md5Validator")]
    public class Md5ValidatorTask : Task
    {
        [TaskAttribute("basepath", Required = true)]
        public string BasePath { get; set; }

        [TaskAttribute("skipextrafilescheck")]
        public bool SkipExtraFilesCheck { get; set; }

        [TaskAttribute("hashfile", Required = true)]
        public string HashFile { get; set; }

        protected override void ExecuteTask()
        {
            Console.WriteLine();
            bool hasError = false;
            List<HashFile> files = new List<HashFile>();

            using (StreamReader sr = new StreamReader(HashFile))
            {
                while (sr.Peek() != -1)
                {
                    HashFile file = new Tasks.HashFile();

                    string line = sr.ReadLine();
                    file.CurrentHash = line.Substring(0, 32).Trim();
                    file.Path = Path.Combine(BasePath, line.Substring(33).Trim());

                    files.Add(file);
                }
            }

            if (files.Count == 0)
            {
                this.Log(Level.Warning, "No files in hash.");
                return;
            }

            string topMostDirectoryForExtraFiles = string.Empty;

            foreach (HashFile file in files)
            {
                HashFileStatus status = file.IsValid();

                if (file.Path.EndsWith(".exe", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (!string.IsNullOrWhiteSpace(topMostDirectoryForExtraFiles))
                    {
                        throw new BuildException("Unabled to determine top most hash directory.  Multiple exe files found in hash file");
                    }

                    topMostDirectoryForExtraFiles = Path.GetDirectoryName(file.Path);
                }

                if (status != HashFileStatus.Valid)
                {
                    hasError = true;
                }

                this.Log(Level.Info, "{0} {1}", status, file.Path.Replace(BasePath, string.Empty));
            }

            if (SkipExtraFilesCheck)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(topMostDirectoryForExtraFiles))
            {
                throw new BuildException("Unable to determine top most directory without an exe in the hashfile.  Consider setting skipextrafilescheck = true");
            }

            string[] remotefiles = Directory.GetFiles(topMostDirectoryForExtraFiles, "*.*", SearchOption.AllDirectories);
            
            foreach (string file in remotefiles)
            {
                if (!files.Any(f => f.Path == file))
                {
                    hasError = true;
                    this.Log(Level.Info, "{0} {1}", "Extra File", file.Replace(BasePath, string.Empty));
                }
            }


            if (hasError)
            {
                throw new BuildException("Hash validation failed.");
            }
        }
    }
}
