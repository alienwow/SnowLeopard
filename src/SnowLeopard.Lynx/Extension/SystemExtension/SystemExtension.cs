using System;

namespace SnowLeopard.Lynx.Extension
{
    /// <summary>
    /// System Extension
    /// </summary>
    public static class SystemExtension
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
