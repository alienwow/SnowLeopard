namespace SnowLeopard.Infrastructure
{
    /// <summary>
    /// BaseViewModel
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseViewModel<T>
    {
        /// <summary>
        /// code
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// msg
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// data
        /// </summary>
        public T Data { get; set; }
    }

}
