using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SnowLeopard.Lynx.Extension
{
    /// <summary>
    /// AES DES Encrypt And Decrypt Extension
    /// </summary>
    public static class SecurityExtension
    {
        /// <summary>
        /// 密钥长度
        /// </summary>
        public enum KeySizeEnum
        {
            /// <summary>
            /// 密钥长度为16位 128个字节
            /// </summary>
            KEY16 = 128,
            /// <summary>
            /// 密钥长度为32位 256个字节
            /// </summary>
            KEY32 = 256
        }

        #region AES

        /// <summary>
        /// 获取向量
        /// </summary>
        private const string IV = @"L+\~f4,Ir)b$=pkf";

        /// <summary>
        /// AESEncrypt
        /// </summary>
        /// <param name="input">待加密数据</param>
        /// <param name="key">密钥</param>
        /// <param name="kSize">密钥长度  default：16位</param>
        /// <returns></returns>
        public static string AESEncrypt(this string input, string key, KeySizeEnum kSize = KeySizeEnum.KEY16)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            var encrypted = string.Empty;
            var dataBytes = Encoding.UTF8.GetBytes(input);

            var aes = _genAESProvider(key, kSize);
            var mStream = new MemoryStream();
            var cStream = new CryptoStream(mStream, aes.CreateEncryptor(), CryptoStreamMode.Write);

            try
            {
                cStream.Write(dataBytes, 0, dataBytes.Length);
                cStream.FlushFinalBlock();
                encrypted = Convert.ToBase64String(mStream.ToArray());
            }
            catch
            {
                return string.Empty;
            }
            finally
            {
                mStream.Close();
                cStream.Close();
                aes.Clear();
            }

            return encrypted;
        }

        /// <summary>
        /// AESDecrypt
        /// </summary>
        /// <param name="input">待解密数据</param>
        /// <param name="key">密钥</param>
        /// <param name="kSize">密钥长度  default：8位</param>
        /// <returns></returns>
        public static string AESDecrypt(this string input, string key, KeySizeEnum kSize = KeySizeEnum.KEY16)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            var decrypted = string.Empty;
            var dataBytes = Convert.FromBase64String(input.Replace(" ", "+"));

            var aes = _genAESProvider(key, kSize);
            var mStream = new MemoryStream();
            var cStream = new CryptoStream(mStream, aes.CreateDecryptor(), CryptoStreamMode.Write);

            try
            {
                cStream.Write(dataBytes, 0, dataBytes.Length);
                cStream.FlushFinalBlock();
                decrypted = Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return string.Empty;
            }
            finally
            {
                mStream.Close();
                cStream.Close();
                aes.Clear();
            }

            return decrypted;
        }


        /// <summary>
        /// _genAESProvider
        /// </summary>
        /// <param name="key">密钥</param>
        /// <param name="kSize">密钥长度  default：16位</param>
        /// <returns></returns>
        private static Rijndael _genAESProvider(string key, KeySizeEnum kSize)
        {
            Rijndael aesProvider = Rijndael.Create();
            aesProvider.Mode = CipherMode.CBC;
            aesProvider.Padding = PaddingMode.PKCS7;
            aesProvider.KeySize = (int)kSize;
            aesProvider.BlockSize = (int)kSize;

            string strTemp;
            int keyLen = (int)kSize / 8;

            if (key.Length >= keyLen)//密钥长度足够
                strTemp = key.Substring(0, keyLen);
            else//密钥程度不足，为其添加所需要的长度
                strTemp = key + key.Md5().Substring(0, keyLen - key.Length);

            byte[] bytKey = Encoding.UTF8.GetBytes(strTemp);

            aesProvider.Key = bytKey;
            aesProvider.IV = bytKey;

            return aesProvider;
        }

        #endregion

        #region DES

        /// <summary>
        /// DESEncrypt ,推荐使用AES加密算法，AES不论在安全性及效率上都远高于DES，详情请看 姚华桢 的论文【AES 和 DES 的效率及安全性比较】
        /// </summary>
        /// <param name="input">待加密数据</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        public static string DESEncrypt(this string input, string key)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            var encrypted = string.Empty;
            var dataBytes = Encoding.UTF8.GetBytes(input);

            var encryptStream = new MemoryStream();
            var encStream = new CryptoStream(encryptStream, _genDESProvider(key).CreateEncryptor(), CryptoStreamMode.Write);

            try
            {
                encStream.Write(dataBytes, 0, dataBytes.Length);
                encStream.FlushFinalBlock();
                encrypted = Convert.ToBase64String(encryptStream.ToArray());
            }
            catch
            {
                return string.Empty;
            }
            finally
            {
                encryptStream.Close();
                encStream.Close();
            }

            return encrypted;
        }

        /// <summary>
        /// DESDecrypt
        /// </summary>
        /// <param name="input">待解密数据</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        public static string DESDecrypt(this string input, string key)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            var decrypted = string.Empty;
            var dataBytes = Convert.FromBase64String(input.Replace(" ", "+"));

            var decryptStream = new MemoryStream();
            var encStream = new CryptoStream(decryptStream, _genDESProvider(key).CreateDecryptor(), CryptoStreamMode.Write);

            try
            {
                encStream.Write(dataBytes, 0, dataBytes.Length);
                encStream.FlushFinalBlock();
                decrypted = Encoding.UTF8.GetString(decryptStream.ToArray());
            }
            catch
            {
                return string.Empty;
            }
            finally
            {
                decryptStream.Close();
                encStream.Close();
            }

            return decrypted;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private static DESCryptoServiceProvider _genDESProvider(string key)
        {
            var desProvider = new DESCryptoServiceProvider()
            {
                Mode = CipherMode.CBC,
                KeySize = 64,
                BlockSize = 64,
                Padding = PaddingMode.PKCS7
            };
            string strTemp;

            if (key.Length >= 8)//密钥长度足够
                strTemp = key.Substring(0, 8);
            else//密钥程度不足，为其添加所需要的长度
                strTemp = key + key.Md5().Substring(0, 8 - key.Length);

            byte[] bytKey = Encoding.UTF8.GetBytes(strTemp);

            desProvider.Key = bytKey;
            desProvider.IV = bytKey;

            return desProvider;
        }

        #endregion
    }
}
