namespace SnowLeopard.Lynx.Extensions
{
    /// <summary>
    /// FileExtension
    /// </summary>
    public static class FileExtensions
    {
        /// <summary>
        /// 获取文件扩展名  不包含最后的.
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string GetFileExtension(this string self)
        {
            string ext = string.Empty;
            var dotPos = self.LastIndexOf('.');
            if (dotPos >= 0)
                ext = $"{self.Substring(dotPos + 1)}".ToLower();
            return ext;
        }
    }
}
