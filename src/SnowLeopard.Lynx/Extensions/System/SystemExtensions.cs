using System;

namespace SnowLeopard.Lynx.Extensions
{
    /// <summary>
    /// System Extensions
    /// </summary>
    public static class SystemExtensions
    {
        #region Guid Extensions

        /// <summary>
        /// ToNoSplitString
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string ToNoSplitString(this Guid self)
        {
            return self.ToString().Replace("-", string.Empty);
        }

        #endregion
    }
}
