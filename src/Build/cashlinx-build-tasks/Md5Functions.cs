using System.Security.Cryptography;
using System.Text;
using NAnt.Core;
using NAnt.Core.Attributes;
using System;
using System.IO;
using System.Linq;

namespace Cashlinx.Build.Tasks
{
    [FunctionSet("Cashlinx", "Cashlinx")]
    public class Md5Functions : FunctionSetBase
    {
        private const string FULLHEX_CODE = "x2";

        public Md5Functions(Project project, PropertyDictionary properties)
            : base(project, properties)
        {
        }

        [Function("GenerateHash")]
        public string GenerateHash(string input)
        {
            Encoding encoder = Encoding.ASCII;

            var hashmd5 = new MD5CryptoServiceProvider();
            byte[] keyArray = hashmd5.ComputeHash(encoder.GetBytes(input));
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < keyArray.Length; i++)
            {
                sb.Append(keyArray[i].ToString(FULLHEX_CODE));
            }

            return sb.ToString();
        }

        [Function("GenerateHashFileList")]
        public string GenerateHashFileList(string directory, string searchPattern, bool recursive)
        {
            return GenerateHashFileList(directory, string.Empty, searchPattern, recursive);
        }

        [Function("GenerateHashFileList")]
        public string GenerateHashFileList(string directory, string relativeToPath, string searchPattern, bool recursive)
        {
            DirectoryInfo dir = new DirectoryInfo(directory);

            FileInfo[] files = null;

            if (recursive)
            {
                files = dir.GetFiles(searchPattern, SearchOption.AllDirectories);
            }
            else
            {
                files = dir.GetFiles(searchPattern);
            }

            StringBuilder sb = new StringBuilder();

            foreach (FileInfo fi in files.OrderBy(f => f.FullName))
            {
                sb.Append(GenerateHashFromFile(fi.FullName));
                sb.Append("  ");
                string relativeName = fi.FullName;
                if (!string.IsNullOrEmpty(relativeToPath))
                {
                    relativeName = relativeName.Replace(relativeToPath, string.Empty);
                }
                sb.AppendLine(relativeName);
            }

            return sb.ToString();
        }

        [Function("GenerateHashFromFile")]
        public string GenerateHashFromFile(string fileName)
        {
            return GenerateHashForFile(fileName);
        }

        public static string GenerateHashForFile(string fileName)
        {
            FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] retVal = md5.ComputeHash(file);
            file.Close();

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
