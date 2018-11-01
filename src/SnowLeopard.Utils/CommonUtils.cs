using System.Reflection;

namespace SnowLeopard
{
    /// <summary>
    /// CommonUtils
    /// </summary>
    public class CommonUtils
    {
        private AssemblyName _entryAssemblyName;

        /// <summary>
        /// EntryAssemblyName
        /// </summary>
        public AssemblyName EntryAssemblyName
        {
            get
            {
                if (_entryAssemblyName == null)
                {
                    _entryAssemblyName = Assembly.GetEntryAssembly().GetName();
                }
                return _entryAssemblyName;
            }
        }

        private string _entryAssemblyVersion;

        /// <summary>
        /// EntryAssemblyVersion
        /// </summary>
        public string EntryAssemblyVersion
        {
            get
            {
                if (_entryAssemblyVersion == null)
                {
                    _entryAssemblyVersion = $"{EntryAssemblyName.Name}-{EntryAssemblyName.Version}";
                }
                return _entryAssemblyVersion;
            }
        }
    }
}
