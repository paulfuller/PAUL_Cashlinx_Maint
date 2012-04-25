using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace file
 
{
    public class SimpleHash
    {
        public static string convertToSHA1(string pass)
        {
            string dataString = pass;
            SHA1 hash = SHA1CryptoServiceProvider.Create();
            byte[] plainTextBytes = Encoding.ASCII.GetBytes(dataString);
            byte[] hashBytes = hash.ComputeHash(plainTextBytes);
            string localChecksum = BitConverter.ToString(hashBytes)
            .Replace("-", "").ToLowerInvariant();
            return localChecksum;
        }

        public static string CreateSaltedPassword(string salt, string password)
        {
            SHA1CryptoServiceProvider SHA1 = null;

            SHA1 = new SHA1CryptoServiceProvider();

            // Convert the string into an array of bytes
            byte[] byteValue = System.Text.Encoding.UTF8.GetBytes(salt + password);

            // Compute the hash value
            byte[] byteHash = SHA1.ComputeHash(byteValue);

            // Dispose the unmanaged cryptographic object 
            SHA1.Clear();

            return Convert.ToBase64String(byteHash);
        }

        public static bool ComparePasswords(string salt, string password, string storedPassword)
        {
            string passwordHash = string.Empty;

            // Create the hashed password
            passwordHash = CreateSaltedPassword(
                salt, password);

            // Compare the passwords
            return (string.Compare(storedPassword, passwordHash) == 0);
        }

        public static byte[] convertToByteArr(String str)
        {
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            return encoding.GetBytes(str);
        }

        public string ByteArrayToString(byte[] btData)
        {
            //Message = "";
            string sStringData = "";
            try
            {
                Encoding enc = Encoding.ASCII;
                sStringData = enc.GetString(btData);
            }
            catch (Exception exp)
            {

                throw exp;
            }
            return sStringData;
        }

    }

    
}
