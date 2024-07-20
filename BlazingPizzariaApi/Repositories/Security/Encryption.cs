using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace BlazingPizza.Api.Repositories.Security
{
    public static class Encryption
    {
        private static readonly byte[] Key = Encoding.UTF8.GetBytes(@"9M539'{#%fG$'Y#9");

        private static readonly byte[] IV = Encoding.UTF8.GetBytes(@"3U:4\\NA.;0UCIt$a");

        public static string EncryptString(string plainText)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.GenerateIV();

                var iv = aesAlg.IV;
                var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, iv);

                using (var msEncrypt = new MemoryStream())
                {
                    msEncrypt.Write(iv, 0, iv.Length);
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    using (var swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    };
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        public static string DecryptString(string cipherText)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (var msDescrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
                {
                    using (var csDescrypt = new CryptoStream(msDescrypt, decryptor, CryptoStreamMode.Read))
                    using (var srDecrypt = new StreamReader(csDescrypt))
                    {
                        return srDecrypt.ReadToEnd();   
                    }
                }
            }
        }
    }
}
