using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SnowLeopard.Lynx.Extension
{
    /// <summary>
    /// AlgorithmNameEnum
    /// </summary>
    public enum AlgorithmNameEnum
    {
        HMACMD5 = 0,
        HMACSHA1,
        HMACSHA256,
        HMACSHA384,
        HMACSHA512
    }

    /// <summary>
    /// HashExtensions
    /// </summary>
    public static class HashExtension
    {
        #region MD5

        /// <summary>
        /// Creates a MD5 hash of the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="fromFile">是否是文件</param>
        /// <returns>A hash</returns>
        public static string Md5(this string input, bool fromFile = false)
        {
            if (input.IsMissing()) return string.Empty;

            if (fromFile && File.Exists(input))
            {
                using (FileStream fs = new FileStream(input, FileMode.Open))
                {
                    return fs.Md5();
                }
            }

            using (var sha = MD5.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(input);
                return sha.ComputeHash(bytes).Tox2String();
            }
        }

        /// <summary>
        /// Creates a MD5 hash of the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>A hash</returns>
        public static string Md5(this Stream input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            using (var sha = MD5.Create())
            {
                return sha.ComputeHash(input).Tox2String();
            }
        }

        /// <summary>
        /// Creates a MD5 hash of the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>A hash.</returns>
        public static string Md5(this byte[] input)
        {
            if (input == null)
            {
                return null;
            }

            using (var sha = MD5.Create())
            {
                return sha.ComputeHash(input).Tox2String();
            }
        }

        /// <summary>
        /// Creates a MD5 hash of the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="fromFile">是否是文件</param>
        /// <returns>A hash</returns>
        public static string Md5Base64(this string input, bool fromFile = false)
        {
            if (input.IsMissing()) return string.Empty;

            if (fromFile && File.Exists(input))
            {
                using (FileStream fs = new FileStream(input, FileMode.Open))
                {
                    return fs.Md5Base64();
                }
            }

            using (var sha = MD5.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(input);
                return sha.ComputeHash(bytes).ToBase64();
            }
        }

        /// <summary>
        /// Creates a MD5 hash of the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>A hash</returns>
        public static string Md5Base64(this Stream input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            using (var sha = MD5.Create())
            {
                return sha.ComputeHash(input).ToBase64();
            }
        }

        /// <summary>
        /// Creates a MD5 hash of the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>A hash.</returns>
        public static string Md5Base64(this byte[] input)
        {
            if (input == null)
            {
                return null;
            }

            using (var sha = MD5.Create())
            {
                return sha.ComputeHash(input).ToBase64();
            }
        }

        #endregion

        #region SHA1

        /// <summary>
        /// Creates a SHA1 hash of the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="fromFile">是否是文件</param>
        /// <returns>A hash</returns>
        public static string Sha1(this string input, bool fromFile = false)
        {
            if (input.IsMissing()) return string.Empty;

            if (fromFile && File.Exists(input))
            {
                using (FileStream fs = new FileStream(input, FileMode.Open))
                {
                    return fs.Sha1();
                }
            }

            using (var sha = SHA1.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(input);
                return sha.ComputeHash(bytes).Tox2String();
            }
        }

        /// <summary>
        /// Creates a SHA1 hash of the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>A hash</returns>
        public static string Sha1(this Stream input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            using (var sha = SHA1.Create())
            {
                return sha.ComputeHash(input).Tox2String();
            }
        }

        /// <summary>
        /// Creates a SHA1 hash of the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>A hash.</returns>
        public static string Sha1(this byte[] input)
        {
            if (input == null)
            {
                return null;
            }

            using (var sha = SHA1.Create())
            {
                return sha.ComputeHash(input).Tox2String();
            }
        }

        /// <summary>
        /// Creates a SHA1 hash of the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="fromFile">是否是文件</param>
        /// <returns>A hash</returns>
        public static string Sha1Base64(this string input, bool fromFile = false)
        {
            if (input.IsMissing()) return string.Empty;

            if (fromFile && File.Exists(input))
            {
                using (FileStream fs = new FileStream(input, FileMode.Open))
                {
                    return fs.Sha1Base64();
                }
            }

            using (var sha = SHA1.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(input);
                return sha.ComputeHash(bytes).ToBase64();
            }
        }

        /// <summary>
        /// Creates a SHA1 hash of the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>A hash</returns>
        public static string Sha1Base64(this Stream input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            using (var sha = SHA1.Create())
            {
                return sha.ComputeHash(input).ToBase64();
            }
        }

        /// <summary>
        /// Creates a SHA1 hash of the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>A hash.</returns>
        public static string Sha1Base64(this byte[] input)
        {
            if (input == null)
            {
                return null;
            }

            using (var sha = SHA1.Create())
            {
                return sha.ComputeHash(input).ToBase64();
            }
        }

        #endregion

        #region SHA256

        /// <summary>
        /// Creates a SHA256 hash of the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="fromFile">是否是文件</param>
        /// <returns>A hash</returns>
        public static string Sha256(this string input, bool fromFile = false)
        {
            if (input.IsMissing()) return string.Empty;

            if (fromFile && File.Exists(input))
            {
                using (FileStream fs = new FileStream(input, FileMode.Open))
                {
                    return fs.Sha256();
                }
            }

            using (var sha = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(input);
                return sha.ComputeHash(bytes).Tox2String();
            }
        }

        /// <summary>
        /// Creates a SHA256 hash of the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>A hash</returns>
        public static string Sha256(this Stream input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            using (var sha = SHA256.Create())
            {
                return sha.ComputeHash(input).Tox2String();
            }
        }

        /// <summary>
        /// Creates a SHA256 hash of the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>A hash.</returns>
        public static string Sha256(this byte[] input)
        {
            if (input == null)
            {
                return null;
            }

            using (var sha = SHA256.Create())
            {
                return sha.ComputeHash(input).Tox2String();
            }
        }

        /// <summary>
        /// Creates a SHA256 hash of the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="fromFile">是否是文件</param>
        /// <returns>A hash</returns>
        public static string Sha256Base64(this string input, bool fromFile = false)
        {
            if (input.IsMissing()) return string.Empty;

            if (fromFile && File.Exists(input))
            {
                using (FileStream fs = new FileStream(input, FileMode.Open))
                {
                    return fs.Sha256Base64();
                }
            }

            using (var sha = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(input);
                return sha.ComputeHash(bytes).ToBase64();
            }
        }

        /// <summary>
        /// Creates a SHA256 hash of the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>A hash</returns>
        public static string Sha256Base64(this Stream input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            using (var sha = SHA256.Create())
            {
                return sha.ComputeHash(input).ToBase64();
            }
        }

        /// <summary>
        /// Creates a SHA256 hash of the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>A hash.</returns>
        public static string Sha256Base64(this byte[] input)
        {
            if (input == null)
            {
                return null;
            }

            using (var sha = SHA256.Create())
            {
                return sha.ComputeHash(input).ToBase64();
            }
        }

        #endregion

        #region SHA384

        /// <summary>
        /// Creates a SHA384 hash of the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="fromFile">是否是文件</param>
        /// <returns>A hash</returns>
        public static string Sha384(this string input, bool fromFile = false)
        {
            if (input.IsMissing()) return string.Empty;

            if (fromFile && File.Exists(input))
            {
                using (FileStream fs = new FileStream(input, FileMode.Open))
                {
                    return fs.Sha384();
                }
            }

            using (var sha = SHA384.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(input);
                return sha.ComputeHash(bytes).Tox2String();
            }
        }

        /// <summary>
        /// Creates a SHA384 hash of the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>A hash</returns>
        public static string Sha384(this Stream input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            using (var sha = SHA384.Create())
            {
                return sha.ComputeHash(input).Tox2String();
            }
        }

        /// <summary>
        /// Creates a SHA384 hash of the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>A hash.</returns>
        public static string Sha384(this byte[] input)
        {
            if (input == null)
            {
                return null;
            }

            using (var sha = SHA384.Create())
            {
                return sha.ComputeHash(input).Tox2String();
            }
        }

        /// <summary>
        /// Creates a SHA384 hash of the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="fromFile">是否是文件</param>
        /// <returns>A hash</returns>
        public static string Sha384Base64(this string input, bool fromFile = false)
        {
            if (input.IsMissing()) return string.Empty;

            if (fromFile && File.Exists(input))
            {
                using (FileStream fs = new FileStream(input, FileMode.Open))
                {
                    return fs.Sha384Base64();
                }
            }

            using (var sha = SHA384.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(input);
                return sha.ComputeHash(bytes).ToBase64();
            }
        }

        /// <summary>
        /// Creates a SHA384 hash of the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>A hash</returns>
        public static string Sha384Base64(this Stream input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            using (var sha = SHA384.Create())
            {
                return sha.ComputeHash(input).ToBase64();
            }
        }

        /// <summary>
        /// Creates a SHA384 hash of the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>A hash.</returns>
        public static string Sha384Base64(this byte[] input)
        {
            if (input == null)
            {
                return null;
            }

            using (var sha = SHA384.Create())
            {
                return sha.ComputeHash(input).ToBase64();
            }
        }

        #endregion

        #region Sha512

        /// <summary>
        /// Creates a SHA512 hash of the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="fromFile">是否是文件</param>
        /// <returns>A hash</returns>
        public static string Sha512(this string input, bool fromFile = false)
        {
            if (input.IsMissing()) return string.Empty;

            if (fromFile && File.Exists(input))
            {
                using (FileStream fs = new FileStream(input, FileMode.Open))
                {
                    return fs.Sha512();
                }
            }

            using (var sha = SHA512.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(input);
                return sha.ComputeHash(bytes).Tox2String();
            }
        }

        /// <summary>
        /// Creates a SHA512 hash of the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>A hash</returns>
        public static string Sha512(this Stream input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            using (var sha = SHA512.Create())
            {
                return sha.ComputeHash(input).Tox2String();
            }
        }

        /// <summary>
        /// Creates a SHA512 hash of the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>A hash.</returns>
        public static string Sha512(this byte[] input)
        {
            if (input == null)
            {
                return null;
            }

            using (var sha = SHA512.Create())
            {
                return sha.ComputeHash(input).Tox2String();
            }
        }

        /// <summary>
        /// Creates a SHA512 hash of the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="fromFile">是否是文件</param>
        /// <returns>A hash</returns>
        public static string Sha512Base64(this string input, bool fromFile = false)
        {
            if (input.IsMissing()) return string.Empty;

            if (fromFile && File.Exists(input))
            {
                using (FileStream fs = new FileStream(input, FileMode.Open))
                {
                    return fs.Sha512Base64();
                }
            }

            using (var sha = SHA512.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(input);
                return sha.ComputeHash(bytes).ToBase64();
            }
        }

        /// <summary>
        /// Creates a SHA512 hash of the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>A hash</returns>
        public static string Sha512Base64(this Stream input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            using (var sha = SHA512.Create())
            {
                return sha.ComputeHash(input).ToBase64();
            }
        }

        /// <summary>
        /// Creates a SHA512 hash of the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>A hash.</returns>
        public static string Sha512Base64(this byte[] input)
        {
            if (input == null)
            {
                return null;
            }

            using (var sha = SHA512.Create())
            {
                return sha.ComputeHash(input).ToBase64();
            }
        }

        #endregion

        #region HMAC

        /// <summary>
        /// Creates a SHA512 hash of the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="algo">algorithmName: MD5,SHA1,SHA256,SHA384,SHA512 default: SHA256</param>
        /// <param name="fromFile">是否是文件</param>
        /// <returns>A hash</returns>
        public static string Hmac(this string input, AlgorithmNameEnum algo = AlgorithmNameEnum.HMACSHA256, bool fromFile = false)
        {
            if (input.IsMissing()) return string.Empty;

            if (fromFile && File.Exists(input))
            {
                using (FileStream fs = new FileStream(input, FileMode.Open))
                {
                    return fs.Hmac(algo);
                }
            }

            using (var sha = HMAC.Create(algo.ToString()))
            {
                var bytes = Encoding.UTF8.GetBytes(input);
                return sha.ComputeHash(bytes).Tox2String();
            }
        }

        /// <summary>
        /// Creates a SHA512 hash of the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="algo">algorithmName: MD5,SHA1,SHA256,SHA384,SHA512 default: SHA256</param>
        /// <returns>A hash</returns>
        public static string Hmac(this Stream input, AlgorithmNameEnum algo = AlgorithmNameEnum.HMACSHA256)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            using (var sha = HMAC.Create(algo.ToString()))
            {
                return sha.ComputeHash(input).Tox2String();
            }
        }

        /// <summary>
        /// Creates a SHA512 hash of the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="algo">algorithmName: MD5,SHA1,SHA256,SHA384,SHA512 default: SHA256</param>
        /// <returns>A hash.</returns>
        public static string Hmac(this byte[] input, AlgorithmNameEnum algo = AlgorithmNameEnum.HMACSHA256)
        {
            if (input == null)
            {
                return null;
            }

            using (var sha = HMAC.Create(algo.ToString()))
            {
                return sha.ComputeHash(input).Tox2String();
            }
        }

        /// <summary>
        /// Creates a SHA512 hash of the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="algo">algorithmName: MD5,SHA1,SHA256,SHA384,SHA512 default: SHA256</param>
        /// <param name="fromFile">是否是文件</param>
        /// <returns>A hash</returns>
        public static string HmacBase64(this string input, AlgorithmNameEnum algo = AlgorithmNameEnum.HMACSHA256, bool fromFile = false)
        {
            if (input.IsMissing()) return string.Empty;

            if (fromFile && File.Exists(input))
            {
                using (FileStream fs = new FileStream(input, FileMode.Open))
                {
                    return fs.HmacBase64(algo);
                }
            }

            using (var sha = HMAC.Create(algo.ToString()))
            {
                var bytes = Encoding.UTF8.GetBytes(input);
                return sha.ComputeHash(bytes).ToBase64();
            }
        }

        /// <summary>
        /// Creates a SHA512 hash of the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="algo">algorithmName: MD5,SHA1,SHA256,SHA384,SHA512 default: SHA256</param>
        /// <returns>A hash</returns>
        public static string HmacBase64(this Stream input, AlgorithmNameEnum algo = AlgorithmNameEnum.HMACSHA256)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            using (var sha = HMAC.Create(algo.ToString()))
            {
                return sha.ComputeHash(input).ToBase64();
            }
        }

        /// <summary>
        /// Creates a SHA512 hash of the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="algo">algorithmName: MD5,SHA1,SHA256,SHA384,SHA512 default: SHA256</param>
        /// <returns>A hash.</returns>
        public static string HmacBase64(this byte[] input, AlgorithmNameEnum algo = AlgorithmNameEnum.HMACSHA256)
        {
            if (input == null)
            {
                return null;
            }

            using (var sha = HMAC.Create(algo.ToString()))
            {
                return sha.ComputeHash(input).ToBase64();
            }
        }

        #endregion

        #region ToString

        private static string Tox2String(this byte[] input)
        {
            var sb = new StringBuilder();
            foreach (byte item in input)
                sb.Append(item.ToString("x2"));
            return sb.ToString();
        }

        private static string ToBase64(this byte[] input)
        {
            return Convert.ToBase64String(input);
        }

        #endregion
    }
}
