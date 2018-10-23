using System;

namespace SnowLeopard.Infrastructure
{
    /// <summary>
    /// ModelStateErrorMsg
    /// </summary>
    public class ModelStateErrorMsg
    {
        /// <summary>
        /// ModelStateErrorMsg
        /// </summary>
        public ModelStateErrorMsg() { }

        /// <summary>
        /// ModelStateErrorMsg
        /// </summary>
        /// <param name="key"></param>
        /// <param name="errorMsgs"></param>
        public ModelStateErrorMsg(string key, string[] errorMsgs)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));

            if (errorMsgs == null || errorMsgs.Length == 0)
                throw new ArgumentNullException(nameof(errorMsgs));

            Key = key;
            Value = errorMsgs;
        }

        /// <summary>
        /// Key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        public string[] Value { get; set; }
    }

}
