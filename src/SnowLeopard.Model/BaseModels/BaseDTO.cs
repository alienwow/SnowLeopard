namespace SnowLeopard.Model.BaseModels
{
    /// <summary>
    /// BaseDTO
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseDTO<T>
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
