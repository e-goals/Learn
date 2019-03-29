using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Junjie.Web
{
    public class EnDeHelper
    {
        private const string key = "gojoanja";
        private const string iv = "0123456789ABCDEF";
        private const string salt = "19841210";
        private const int iterations = 1024;

        private EnDeHelper()
        {
        }

        public static HashAlgorithm CreateMD5()
        {
            return new MD5CryptoServiceProvider();
        }

        public static HashAlgorithm CreateSHA1()
        {
            return new SHA1CryptoServiceProvider();
        }

        public static HashAlgorithm CreateSHA256()
        {
            return new SHA256Managed();
        }

        public static HashAlgorithm CreateSHA384()
        {
            return new SHA384Managed();
        }

        public static HashAlgorithm CreateSHA512()
        {
            return new SHA512Managed();
        }

        public static SymmetricAlgorithm CreateAES()
        {
            return new AesManaged();
        }

        public static SymmetricAlgorithm CreateDES()
        {
            return new DESCryptoServiceProvider();
        }

        public static SymmetricAlgorithm CreateRC2()
        {
            return new RC2CryptoServiceProvider();
        }

        public static SymmetricAlgorithm CreateRijndael()
        {
            return new RijndaelManaged();
        }

        public static SymmetricAlgorithm CreateTripleDES()
        {
            return new TripleDESCryptoServiceProvider();
        }

        public static string Hash(HashAlgorithm hashAlgorithm, string sourceText)
        {
            byte[] source = (new UTF8Encoding()).GetBytes(sourceText);
            byte[] hashed = hashAlgorithm.ComputeHash(source);
            StringBuilder result = new StringBuilder(hashed.Length);
            for (int i = 0; i < hashed.Length; i++)
            {
                if (hashed.Length <= 256 && (i % 4 == 0))
                    result.Append("  ");
                else if (hashed.Length > 256 && (i % 8 == 0))
                    result.Append("<br />");
                result.Append(hashed[i].ToString("X2"));

            }
            return result.ToString();
        }

        public static bool IsHashMatch(HashAlgorithm hashAlgorithm, string hashedText, string sourceText)
        {
            string newHashedText = Hash(hashAlgorithm, sourceText);
            return (String.Compare(hashedText, newHashedText, false) == 0);
        }

        public static byte[] Encrypt(SymmetricAlgorithm algorithm, byte[] plainText,
            string key, string iv, string salt, int iterations, int keySize,
            CipherMode cipherMode, PaddingMode paddingMode)
        {
            if (algorithm == null)
                throw new ArgumentNullException("algorithm");
            if (plainText == null)
                throw new ArgumentNullException("plainText");
            if (String.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");
            if (String.IsNullOrEmpty(iv))
                throw new ArgumentNullException("iv");
            if (String.IsNullOrEmpty(salt))
                throw new ArgumentNullException("salt");

            // The salt size must be 8 bytes (64 bits) or larger.
            // The iteration count must be greater than zero. The minimum recommended number of iterations is 1000. 
            var deriveBytes = new Rfc2898DeriveBytes(key, Encoding.UTF8.GetBytes(salt), iterations);
            using (algorithm)
            {
                algorithm.Mode = cipherMode;
                algorithm.Padding = paddingMode;
                byte[] cipherTextBytes = null;
                using (var encryptor = algorithm.CreateEncryptor(deriveBytes.GetBytes(keySize / 8), Encoding.UTF8.GetBytes(iv)))
                {
                    using (var ms = new MemoryStream())
                    {
                        using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                        {
                            cs.Write(plainText, 0, plainText.Length);
                            cs.FlushFinalBlock();
                            cipherTextBytes = ms.ToArray();
                            ms.Close();
                            cs.Close();
                        }
                    }
                    algorithm.Clear();
                    return cipherTextBytes;
                }
            }
        }

        public static byte[] Encrypt(SymmetricAlgorithm algortghm, byte[] plainText,
            int keySize, CipherMode cipherMode, PaddingMode paddingMode)
        {
            return Encrypt(algortghm, plainText,
                EnDeHelper.key, EnDeHelper.iv, EnDeHelper.salt, EnDeHelper.iterations,
                keySize, cipherMode, paddingMode);
        }

        public static byte[] Decrypt(SymmetricAlgorithm algorithm, byte[] cipherText,
            string key, string iv, string salt, int iterations, int keySize,
            CipherMode cipherMode, PaddingMode paddingMode)
        {
            if (null == cipherText)
                throw new ArgumentNullException("cipherText");
            if (null == algorithm)
                throw new ArgumentNullException("algorithm");
            if (String.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");
            if (String.IsNullOrEmpty(iv))
                throw new ArgumentNullException("iv");
            if (String.IsNullOrEmpty(salt))
                throw new ArgumentNullException("salt");

            // The salt size must be 8 bytes (64 bits) or larger.
            // The iteration count must be greater than zero. The minimum recommended number of iterations is 1000. 
            var deriveBytes = new Rfc2898DeriveBytes(key, Encoding.UTF8.GetBytes(salt), iterations);

            using (algorithm)
            {
                algorithm.Mode = cipherMode;
                algorithm.Padding = paddingMode;
                byte[] plainTextBytes = new byte[cipherText.Length];
                int byteSize = -1;
                using (var decryptor = algorithm.CreateDecryptor(deriveBytes.GetBytes(keySize / 8), Encoding.UTF8.GetBytes(iv)))
                {
                    using (var ms = new MemoryStream(cipherText))
                    {
                        using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                        {
                            byteSize = cs.Read(plainTextBytes, 0, plainTextBytes.Length);
                            ms.Close();
                            cs.Close();
                        }
                    }
                }
                algorithm.Clear();
                Array.Resize(ref plainTextBytes, byteSize);
                return plainTextBytes;
            }
        }

        public static byte[] Decrypt(SymmetricAlgorithm algorithm, byte[] cipherText,
            int keySize, CipherMode cipherMode, PaddingMode paddingMode)
        {
            return Decrypt(algorithm, cipherText,
            EnDeHelper.key, EnDeHelper.iv, EnDeHelper.salt, EnDeHelper.iterations,
            keySize, cipherMode, paddingMode);
        }

        public static string Base64Encode(Encoding encode, string source)
        {
            byte[] bytes = encode.GetBytes(source);
            string result = Convert.ToBase64String(bytes);
            return result;
        }

        public static string Base64Encode(string source)
        {
            return Base64Encode(Encoding.UTF8, source);
        }

        public static string Base64Decode(Encoding encode, string base64)
        {
            byte[] bytes = Convert.FromBase64String(base64);
            string result = encode.GetString(bytes);
            return result;
        }

        public static string Base64Decode(string base64)
        {
            return Base64Decode(base64);
        }


    }
}