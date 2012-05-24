using System.IO;
using System.Reflection;

namespace Cashlinx.Build.Tasks
{
    public class Utilities
    {
        public static Stream GetResourceAsStream(string name)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream(name);
        }

        public static byte[] GetResourceAsByteArray(string name)
        {
            MemoryStream ms = new MemoryStream();
            GetResourceAsStream(name).CopyTo(ms);
            byte[] b = ms.ToArray();
            ms.Dispose();
            return b;
        }

        public static string GetResourceAsString(string name)
        {
            using (StreamReader streamReader = new StreamReader(GetResourceAsStream(name)))
            {
                return streamReader.ReadToEnd();
            }
        }
    }
}
