using System.Reflection;

namespace SnowLeopard
{
    /// <summary>
    /// SnowLeopardUtils
    /// </summary>
    public class SnowLeopardUtils
    {
        private string _snowLeopardVersion;

        /// <summary>
        /// SnowLeopardVersion
        /// </summary>
        public string SnowLeopardVersion
        {
            get
            {
                if (_snowLeopardVersion == null)
                {
                    var assemblyName = Assembly.GetExecutingAssembly().GetName();
                    _snowLeopardVersion = $"{assemblyName.Name}-{assemblyName.Version}";
                }
                return _snowLeopardVersion;
            }
        }
    }
}
