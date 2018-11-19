namespace SnowLeopard.WebApi.UnitTest.Models
{
    public class SessionTestResult
    {
        /// <summary>
        /// 上一个 Session 值
        /// </summary>
        public string Last { get; set; }

        /// <summary>
        /// 当前 Session 值
        /// </summary>
        public string Current { get; set; }

        /// <summary>
        /// 写入后 Session 值
        /// </summary>
        public string Writed { get; set; }
    }
}
