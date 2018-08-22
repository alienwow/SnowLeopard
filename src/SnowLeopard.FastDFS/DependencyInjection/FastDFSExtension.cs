using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Net;

namespace SnowLeopard.FastDFS.DependencyInjection
{
    public static class FastDFSExtension
    {
        /// <summary>
        /// Add SnowLeopard FastDFS
        /// </summary>
        /// <param name="services"></param>
        /// <param name="fastDFSConfig"></param>
        public static void AddSnowLeopardFastDFS(this IServiceCollection services, FastDFSConfig fastDFSConfig)
        {
            FDFSConfig.ConnectionTimeout = fastDFSConfig.ConnectionTimeout ?? 100;
            FDFSConfig.Connection_LifeTime = fastDFSConfig.ConnectionLifeTime ?? 3600;
            FDFSConfig.Storage_MaxConnection = fastDFSConfig.StorageMaxConnection ?? 100;
            FDFSConfig.Tracker_MaxConnection = fastDFSConfig.TrackerMaxConnection ?? 100;
            var trackerServers = new List<IPEndPoint>();
            FDFSConfig.Trackers = trackerServers;

            if (fastDFSConfig?.Trackers != null)
            {
                foreach (var tracker in fastDFSConfig.Trackers)
                {
                    if (string.IsNullOrEmpty(tracker.IP) == false)
                    {
                        var point = new IPEndPoint(IPAddress.Parse(tracker.IP), tracker.Port);
                        trackerServers.Add(point);
                    }
                }
            }
        }

    }
}
