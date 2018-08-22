namespace SnowLeopard.FastDFS
{
    /// <summary>
    /// FastDFSConfig
    /// </summary>
    public class FastDFSConfig
    {
        /// <summary>
        /// StorageMaxConnection
        /// </summary>
        public int? StorageMaxConnection { get; set; }

        /// <summary>
        /// TrackerMaxConnection
        /// </summary>
        public int? TrackerMaxConnection { get; set; }

        /// <summary>
        /// ConnectionTimeout
        /// </summary>
        public int? ConnectionTimeout { get; set; }

        /// <summary>
        /// ConnectionLifeTime
        /// </summary>
        public int? ConnectionLifeTime { get; set; }

        /// <summary>
        /// Trackers
        /// </summary>
        public FastDFSTracker[] Trackers { get; set; }
    }

    /// <summary>
    /// FastDFSTracker
    /// </summary>
    public class FastDFSTracker
    {
        public string IP { get; set; }
        public int Port { get; set; }
    }

}
