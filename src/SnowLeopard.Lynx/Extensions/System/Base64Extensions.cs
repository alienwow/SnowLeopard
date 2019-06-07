using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;

namespace SnowLeopard.Lynx.Extensions
{
    public static class Base64Extensions
    {
        /// <summary>
        /// ToBase64
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static string ToBase64(this Image image)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            using (var ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Png);
                return ms.ToBase64();
            }
        }

        /// <summary>
        /// ToBase64
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static string ToBase64(this Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            byte[] arr = new byte[stream.Length];
            stream.Read(arr, 0, (int)stream.Length);

            return arr.ToBase64();
        }

        /// <summary>
        /// ToBase64
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static async Task<string> ToBase64Async(this Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            byte[] arr = new byte[stream.Length];
            await stream.ReadAsync(arr, 0, (int)stream.Length);

            return arr.ToBase64();
        }

        /// <summary>
        /// ToBase64
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static string ToBase64(this byte[] arr)
        {
            if (arr == null)
                throw new ArgumentNullException(nameof(arr));

            return Convert.ToBase64String(arr);
        }

    }
}
