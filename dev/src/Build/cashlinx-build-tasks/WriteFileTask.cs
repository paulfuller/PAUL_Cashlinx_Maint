using System;
using System.Collections.Generic;
using System.Text;
using NAnt.Core;
using NAnt.Core.Attributes;
using System.IO;

namespace Cashlinx.Build.Tasks
{
    [TaskName("writefile")]
    public class WriteFileTask : Task
    {
        [TaskAttribute("append", Required = false)]
        public bool Append { get; set; }
        
        [TaskAttribute("file", Required = true)]
        public string FileName { get; set; }

        [TaskAttribute("value", Required = true)]
        public string Value { get; set; }

        protected override void ExecuteTask()
        {
            if (Value == null)
            {
                throw new BuildException("Cannot write a null value");
            }

            using (StreamWriter sw = new StreamWriter(FileName, Append))
            {
                sw.Write(Value);
            }
        }
    }
}
