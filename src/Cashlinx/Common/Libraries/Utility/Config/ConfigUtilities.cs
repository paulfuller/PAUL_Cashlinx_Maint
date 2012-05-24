using System.IO;
using Common.Libraries.Utility.String;

namespace Common.Libraries.Utility.Config
{
    public static class ConfigUtilities
    {
        public const string FILE_ENTRY = "[{0}]={1}";
        public const string LBRACKET = "[";
        public const string RBRACKET_EQ = "]=";


        public static void WriteConfigEntry(StreamWriter stream, string key, string data)
        {
            if (stream == null || string.IsNullOrEmpty(key) || string.IsNullOrEmpty(data))
            {
                return;
            }
            stream.WriteLine(FILE_ENTRY, key, data);
        }

        public static string ReadConfigEntry(StreamReader stream, string key)
        {
            if (stream == null || string.IsNullOrEmpty(key))
            {
                return (string.Empty);
            }
            string data = string.Empty;
            string readData = stream.ReadLine();
            if (!string.IsNullOrEmpty(readData))
            {
                string keyEntry = LBRACKET + key + RBRACKET_EQ;
                int idx = readData.IndexOf(keyEntry, System.StringComparison.OrdinalIgnoreCase);
                if (idx == 0 && readData.Length > keyEntry.Length)
                {
                    data = readData.Substring(keyEntry.Length);
                }
            }
            return (data);
        }

        public static void WriteEncConfigEntry(StreamWriter stream, string key, string data, string encKey)
        {
            if (stream == null ||
                string.IsNullOrEmpty(key) ||
                string.IsNullOrEmpty(encKey) ||
                string.IsNullOrEmpty(data))
            {
                return;
            }
            var entry = StringUtilities.Encrypt(data, encKey, true);
            WriteConfigEntry(stream, key, entry);
        }

        public static string ReadEncConfigEntry(StreamReader stream, string key, string encKey)
        {
            if (stream == null ||
                string.IsNullOrEmpty(key) ||
                string.IsNullOrEmpty(encKey))
            {
                return (string.Empty);
            }
            var preData = ReadConfigEntry(stream, key);
            if (!string.IsNullOrEmpty(preData))
            {
                return (StringUtilities.Decrypt(preData, encKey, true));
            }
            return (string.Empty);
        }

    }
}
