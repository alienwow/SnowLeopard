using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace SnowLeopard.Lynx
{
    /// <summary>
    /// LynxUtils
    /// </summary>
    public class LynxUtils
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

        /// <summary>
        /// GetAllAssembly
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Assembly> GetAllAssembly()
        {
            var rv = new List<Assembly>();
            var path = Assembly.GetEntryAssembly().Location;
            var dir = new DirectoryInfo(Path.GetDirectoryName(path));

            var dlls = dir.GetFiles("*.dll", SearchOption.AllDirectories);

            foreach (var dll in dlls)
            {
                try
                {
                    rv.Add(AssemblyLoadContext.Default.LoadFromAssemblyPath(dll.FullName));
                }
                catch { }
            }
            return rv;
        }

        /// <summary>
        /// GetAllType
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Type> GetAllType()
        {
            return GetAllAssembly().SelectMany(x => x.GetTypes());
        }
    }
}
