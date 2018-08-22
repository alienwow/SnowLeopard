using System.Collections.Generic;
using System.Net;

namespace SnowLeopard.FastDFS
{
    public class FDFSManager
    {
        public static bool Initialize(List<IPEndPoint> trackers)
        {
            return ConnectionManager.Initialize(trackers);
        }
    }
}
