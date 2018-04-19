using Laboratory.Core;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Laboratory.EncryptionTest
{
    /// <summary>
    /// 数据加密服务
    /// </summary>
    public class EncryptionService
    {
        private string encryptionKey = "25126848FE0144F4A0E04D82066F25B7";

        #region Methods
        /// <summary>
        /// Create salt key
        /// </summary>
        /// <param name="size">Key size</param>
        /// <returns>Salt key</returns>
        public virtual string CreateSaltKey(int size)
        {
            // Generate a cryptographic random number
            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[size];
            rng.GetBytes(buff);

            // Return a Base64 string representation of the random number
            return Convert.ToBase64String(buff);
        }

        /// <summary>
        /// Create a password hash
        /// </summary>
        /// <param name="password">{assword</param>
        /// <param name="saltkey">Salk key</param>
        /// <param name="passwordFormat">Password format (hash algorithm)</param>
        /// <returns>Password hash</returns>
        public virtual string CreatePasswordHash(string password, string saltkey, string passwordFormat = "SHA1")
        {
            if (String.IsNullOrEmpty(passwordFormat))
                passwordFormat = "SHA1";
            string saltAndPassword = String.Concat(password, saltkey);

            //return FormsAuthentication.HashPasswordForStoringInConfigFile(saltAndPassword, passwordFormat);
            var algorithm = HashAlgorithm.Create(passwordFormat);
            if (algorithm == null)
                throw new ArgumentException("Unrecognized hash name", "hashName");

            var hashByteArray = algorithm.ComputeHash(Encoding.UTF8.GetBytes(saltAndPassword));
            return BitConverter.ToString(hashByteArray).Replace("-", "");
        }

        /// <summary>
        /// SHA1加密
        /// </summary>
        /// <param name="plainText">明文</param>
        /// <returns></returns>
        public virtual string SHA1(string plainText, bool enablePrivateKey = false)
        {
            var passwordFormat = "SHA1";

            //return FormsAuthentication.HashPasswordForStoringInConfigFile(saltAndPassword, passwordFormat);
            var algorithm = HashAlgorithm.Create(passwordFormat);
            if (algorithm == null)
                throw new ArgumentException("Unrecognized hash name", "hashName");

            string saltAndPassword = plainText;
            if (enablePrivateKey)
            {
                var saltkey = AppConfig.SHA1_SALT ?? "salt_default_key";
                saltAndPassword = String.Concat(plainText, saltkey);
            }

            var hashByteArray = algorithm.ComputeHash(Encoding.UTF8.GetBytes(saltAndPassword));
            return BitConverter.ToString(hashByteArray).Replace("-", "");
        }

        /// <summary>
        /// SHA256加密
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public virtual string SHA256(string plainText)
        {
            HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider();
            byte[] byteValue = System.Text.Encoding.UTF8.GetBytes(plainText);
            byte[] byteHash = hashAlgorithm.ComputeHash(byteValue);
            return Convert.ToBase64String(byteHash);
        }

        /// <summary>
        /// Encrypt text
        /// </summary>
        /// <param name="plainText">Text to encrypt</param>
        /// <param name="encryptionPrivateKey">Encryption private key</param>
        /// <returns>Encrypted text</returns>
        public virtual string EncryptText(string plainText, string encryptionPrivateKey = "")
        {
            if (string.IsNullOrEmpty(plainText))
                return plainText;

            if (String.IsNullOrEmpty(encryptionPrivateKey))
                encryptionPrivateKey = encryptionKey;
            if (encryptionPrivateKey.Length < 16)
                encryptionPrivateKey = encryptionPrivateKey.PadRight(16, '0');

            var tDESalg = new TripleDESCryptoServiceProvider();
            tDESalg.Key = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(0, 16));
            tDESalg.IV = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(8, 8));

            byte[] encryptedBinary = EncryptTextToMemory(plainText, tDESalg.Key, tDESalg.IV);
            return Convert.ToBase64String(encryptedBinary);
        }

        /// <summary>
        /// Decrypt text
        /// </summary>
        /// <param name="cipherText">Text to decrypt</param>
        /// <param name="encryptionPrivateKey">Encryption private key</param>
        /// <returns>Decrypted text</returns>
        public virtual string DecryptText(string cipherText, string encryptionPrivateKey = "")
        {
            if (String.IsNullOrEmpty(cipherText))
                return cipherText;

            if (String.IsNullOrEmpty(encryptionPrivateKey))
                encryptionPrivateKey = encryptionKey;
            if (encryptionPrivateKey.Length < 16)
                encryptionPrivateKey = encryptionPrivateKey.PadRight(16, '0');

            var tDESalg = new TripleDESCryptoServiceProvider();
            tDESalg.Key = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(0, 16));
            tDESalg.IV = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(8, 8));

            byte[] buffer = Convert.FromBase64String(cipherText);
            return DecryptTextFromMemory(buffer, tDESalg.Key, tDESalg.IV);
        }

        /// <summary>
        /// MD5加密，编码为UTF-8
        /// </summary>
        /// <param name="cipherText"></param>
        /// <returns></returns>
        public virtual string MD5Hash(string cipherText)
        {
            return MD5Hash(cipherText, Encoding.UTF8);
        }

        /// <summary>
        /// MD5加密，编码为UTF-8
        /// </summary>
        /// <param name="cipherText">要加密的字符串</param>
        /// <param name="saltkey">密文</param>
        /// <returns></returns>
        public virtual string MD5Hash(string cipherText, string saltkey)
        {
            cipherText = string.Concat(cipherText, saltkey);
            return MD5Hash(cipherText, Encoding.UTF8);
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        public virtual string MD5Hash(string cipherText, Encoding encode)
        {
            var crypto = new MD5CryptoServiceProvider();
            var bytes = encode.GetBytes(cipherText);
            bytes = crypto.ComputeHash(bytes);
            var sb = new StringBuilder();
            foreach (var num in bytes)
            {
                sb.AppendFormat("{0:x2}", num);
            }
            return sb.ToString().ToLower();
        }

        #region RsaEncrypt --- 使用 RSA 公钥加密

        /// <summary>
        /// 使用 RSA 公钥加密
        /// </summary>
        /// <param name="message"></param>
        /// <param name="publicXml"></param>
        /// <returns></returns>
        public virtual string RsaEncrypt(string message, string publicXml = null)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                if (string.IsNullOrEmpty(publicXml))
                {
                    publicXml = "<RSAKeyValue><Modulus>tyKYdwjrUyGMXjxS6aQ3eTI8RuikMlsLuQlycvcZrhVGWKiMrtI3E0kzOtbIvYbebw5mWESP30phhCm/cKMCW4ERL2oZM1MD4+jpYb1WOYb7leTPoA+Vzac1qp9wRSNvkdcnD6hUYvhmZG+4EiovlNZHoJUtNFWEfw5aMqZR+Ok=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
                }

                rsa.FromXmlString(publicXml);

                byte[] messageBytes = Encoding.UTF8.GetBytes(message);
                byte[] resultBytes = rsa.Encrypt(messageBytes, false);

                return Convert.ToBase64String(resultBytes);
            }
        }

        #endregion

        #region RsaDecrypt --- 使用 RSA 私钥解密

        /// <summary>
        /// 使用 RSA 私钥解密
        /// </summary>
        /// <param name="message"></param>
        /// <param name="privateXml"></param>
        /// <returns></returns>
        public virtual string RsaDecrypt(string message, string privateXml = null)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                if (string.IsNullOrEmpty(privateXml))
                {
                    privateXml = "<RSAKeyValue><Modulus>tyKYdwjrUyGMXjxS6aQ3eTI8RuikMlsLuQlycvcZrhVGWKiMrtI3E0kzOtbIvYbebw5mWESP30phhCm/cKMCW4ERL2oZM1MD4+jpYb1WOYb7leTPoA+Vzac1qp9wRSNvkdcnD6hUYvhmZG+4EiovlNZHoJUtNFWEfw5aMqZR+Ok=</Modulus><Exponent>AQAB</Exponent><P>29AepSPo0LxbRbKIWZ5SP3fLHIjehOnMoscvHXHtbAr1KZsphiAUXMaohdGBGewy5exa/Me3Ir69DshLUHfYTQ==</P><Q>1Ui2L9AAc5DQ0TziHDH6CmQf5kYy5h59nDr5x4aYgvyVNFCqbpjhBqTstS0+bEur8aErUNppTH+YjB4QccpxDQ==</Q><DP>hQHiCTs5XVUcRYhKSTArxIvQM2v+eZ6fXL/6Gm8dowreXlatQaOXrqvmvVKQNAdgpQ/n3p1ai4OvEorQR9i84Q==</DP><DQ>L3FXJVeNYieKa6CxYzgyuHEBL4XZ+Jo7sq7jtOBZuHla7yIqZyOWmpXBGaQQyKIkg0Eok3miBqQzWKevXMB9WQ==</DQ><InverseQ>yqM8j5FVsCLfFvXicyLqHfsqmQYgRbmMLlwIqiQE7L/WchE+V89bNpJ4RYFsSZBcNTAhhzBcxHV4cSI3Ve4CPw==</InverseQ><D>rfBX60c3a5/DmIWnIm5smdoS0i6INaOwALFuWK9FXsiqJe8An9LdqJ2v4MS7qPd+MPD2WAPWungFIugcAjlxNCiJdE+QOUS8Wt8+kZUHKQsTFtTNPq4gEp1XvnGC5sI5xE9q6QtwJqaHiWJEWq+NyPgFfoxRFjdykFAtx/6Yv2E=</D></RSAKeyValue>";
                }

                rsa.FromXmlString(privateXml);

                byte[] messageBytes = Convert.FromBase64String(message);
                byte[] resultBytes = rsa.Decrypt(messageBytes, false);

                return Encoding.UTF8.GetString(resultBytes);
            }
        }

        #endregion

        #region 通用加密算法

        /// <summary>
        /// 哈希加密算法
        /// </summary>
        /// <param name="hashAlgorithm"> 所有加密哈希算法实现均必须从中派生的基类 </param>
        /// <param name="strInput"> 待加密的字符串 </param>
        /// <param name="encoding"> 字符编码 </param>
        /// <returns></returns>
        private static string HashEncrypt(HashAlgorithm hashAlgorithm, string strInput, Encoding encoding)
        {
            var data = hashAlgorithm.ComputeHash(encoding.GetBytes(strInput));

            return BitConverter.ToString(data).Replace("-", "");
        }

        /// <summary>
        /// 验证哈希值
        /// </summary>
        /// <param name="hashAlgorithm"> 所有加密哈希算法实现均必须从中派生的基类 </param>
        /// <param name="unhashedText"> 未加密的字符串 </param>
        /// <param name="hashedText"> 经过加密的哈希值 </param>
        /// <param name="encoding"> 字符编码 </param>
        /// <returns></returns>
        private static bool VerifyHashValue(HashAlgorithm hashAlgorithm, string unhashedText, string hashedText,
            Encoding encoding)
        {
            return string.Equals(HashEncrypt(hashAlgorithm, unhashedText, encoding), hashedText,
                StringComparison.OrdinalIgnoreCase);
        }

        #endregion

        #region HMAC-SHA256 加密

        /// <summary>
        /// HMAC-SHA256 加密
        /// </summary>
        /// <param name="strInput"> 要加密的字符串 </param>
        /// <param name="key"> 密钥 </param>
        /// <param name="encoding"> 字符编码 </param>
        /// <returns></returns>
        public string HMACSHA256Encrypt(string strInput, string key, Encoding encoding)
        {
            return HashEncrypt(new HMACSHA256(encoding.GetBytes(key)), strInput, encoding);
        }

        /// <summary>
        /// HMAC-SHA256 加密
        /// </summary>
        /// <param name="strInput"> 要加密的字符串 </param>
        /// <param name="salt"> salt </param>
        /// <returns></returns>
        public string HMACSHA256Encrypt(string strInput, string salt)
        {
            return HashEncrypt(new HMACSHA256(Encoding.Default.GetBytes(salt)), strInput, Encoding.Default);
        }

        #endregion

        #endregion

        #region Utilities

        private byte[] EncryptTextToMemory(string data, byte[] key, byte[] iv)
        {
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, new TripleDESCryptoServiceProvider().CreateEncryptor(key, iv), CryptoStreamMode.Write))
                {
                    byte[] toEncrypt = new UnicodeEncoding().GetBytes(data);
                    cs.Write(toEncrypt, 0, toEncrypt.Length);
                    cs.FlushFinalBlock();
                }

                return ms.ToArray();
            }
        }

        private string DecryptTextFromMemory(byte[] data, byte[] key, byte[] iv)
        {
            using (var ms = new MemoryStream(data))
            {
                using (var cs = new CryptoStream(ms, new TripleDESCryptoServiceProvider().CreateDecryptor(key, iv), CryptoStreamMode.Read))
                {
                    var sr = new StreamReader(cs, new UnicodeEncoding());
                    return sr.ReadLine();
                }
            }
        }

        #endregion
    }
}
