using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Cashlinx.Build.Tasks
{
    public enum HashFileStatus
    {
        Valid,
        Missing,
        Mismatch,
    }

    public class HashFile
    {
        public string Path { get; set; }
        public string CurrentHash { get; set; }
        public string FileHash { get; set; }

        public HashFileStatus IsValid()
        {
            if (!File.Exists(Path))
            {
                return HashFileStatus.Missing;
            }

            FileHash = Md5Functions.GenerateHashForFile(Path);

            if (!CurrentHash.Equals(FileHash))
            {
                return HashFileStatus.Mismatch;
            }

            return HashFileStatus.Valid;
        }
    }
}
