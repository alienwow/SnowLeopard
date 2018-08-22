using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SnowLeopard.FastDFS
{
    /// <summary>
    /// FDFSConfig
    /// </summary>
    internal class FDFSConfig
    {
        internal static int Storage_MaxConnection { get; set; }

        internal static int Tracker_MaxConnection { get; set; }

        internal static int ConnectionTimeout { get; set; }

        internal static int Connection_LifeTime { get; set; }

        internal static Encoding Charset = Encoding.UTF8;

        internal static List<IPEndPoint> Trackers { get; set; }
    }
}
