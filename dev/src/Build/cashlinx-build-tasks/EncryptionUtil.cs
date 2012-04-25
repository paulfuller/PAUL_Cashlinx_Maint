using System;
using System.Security.Cryptography;
using System.Text;

namespace Cashlinx.Build.Tasks
{
    public class EncryptionUtil
    {
        public static string Decrypt(string encryptedMessage, string key, bool useHashing)
        {
            if (string.IsNullOrEmpty(encryptedMessage) || string.IsNullOrEmpty(key))
            {
                return (string.Empty);
            }
            try
            {
                byte[] keyArray;
                var toEncryptArray = Convert.FromBase64String(encryptedMessage);

                //Validate the array we are going to decrypt
                if (toEncryptArray == null || toEncryptArray.Length <= 0)
                {
                    return (string.Empty);
                }

                if (useHashing)
                {
                    var hashmd5 = new MD5CryptoServiceProvider();
                    var tmpKeys = Encoding.UTF8.GetBytes(key);
                    if (tmpKeys.Length > 0)
                    {
                        keyArray = hashmd5.ComputeHash(tmpKeys);
                    }
                    else
                    {
                        return (string.Empty);
                    }
                }
                else
                {
                    keyArray = Encoding.UTF8.GetBytes(key);
                }

                //Validate the key array we are going to use to decrypt
                //the message
                if (keyArray == null || keyArray.Length <= 0)
                {
                    return (string.Empty);
                }

                //Create TripleDES decryption provider
                var tdes = new TripleDESCryptoServiceProvider
                {
                    Key = keyArray,
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                };

                //Decrypt the message
                var cTransform = tdes.CreateDecryptor();
                var resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                //Validate result array
                if (resultArray == null || resultArray.Length <= 0)
                {
                    return (string.Empty);
                }

                return (Encoding.UTF8.GetString(resultArray));

            }
            catch (System.Exception)
            {
                //Catches exceptions and returns empty string
                return (string.Empty);
            }
        }

        public static string Encrypt(string message, string key, bool useHashing)
        {
            if (string.IsNullOrEmpty(message) || string.IsNullOrEmpty(key))
            {
                return (string.Empty);
            }
            try
            {
                byte[] keyArray;
                var toEncryptArray = Encoding.UTF8.GetBytes(message);

                //Validate the array to encrypt
                if (toEncryptArray.Length <= 0)
                {
                    return (string.Empty);
                }

                if (useHashing)
                {
                    var hashmd5 = new MD5CryptoServiceProvider();
                    var tmpArray = Encoding.UTF8.GetBytes(key);
                    if (tmpArray.Length <= 0)
                    {
                        return (string.Empty);
                    }
                    keyArray = hashmd5.ComputeHash(tmpArray);
                }
                else
                {
                    keyArray = Encoding.UTF8.GetBytes(key);
                }

                //Validate key array
                if (keyArray == null || keyArray.Length <= 0)
                {
                    return (string.Empty);
                }

                var tdes = new TripleDESCryptoServiceProvider
                {
                    Key = keyArray,
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                };

                var cTransform = tdes.CreateEncryptor();
                var resultArray = cTransform.TransformFinalBlock(toEncryptArray,
                                                                 0,
                                                                 toEncryptArray.Length);

                //Validate result array
                if (resultArray == null || resultArray.Length <= 0)
                {
                    return (string.Empty);
                }

                return (Convert.ToBase64String(resultArray, 0, resultArray.Length));
            }
            catch (System.Exception)
            {
                return (string.Empty);
            }
        }
    }
}
