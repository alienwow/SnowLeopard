using Microsoft.AspNetCore.Http;
using System;

namespace SnowLeopard.Exceptions
{
    /// <summary>
    /// BaseException
    /// </summary>
    public class BaseException : Exception
    {
        /// <summary>
        /// BaseException
        /// </summary>
        public BaseException() : base() { }

        /// <summary>
        /// BaseException
        /// </summary>
        /// <param name="code"><see cref="StatusCodes"/></param>
        public BaseException(int code) : base()
        {
            Code = code;
        }

        /// <summary>
        /// BaseException
        /// </summary>
        /// <param name="message"></param>
        public BaseException(string message) : base(message)
        {
        }

        /// <summary>
        /// BaseException
        /// </summary>
        /// <param name="message"></param>
        /// <param name="code"><see cref="StatusCodes"/></param>
        public BaseException(string message, int code) : base(message)
        {
            Code = code;
        }

        /// <summary>
        /// BaseException
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public BaseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// BaseException
        /// </summary>
        /// <param name="message"></param>
        /// <param name="code"><see cref="StatusCodes"/></param>
        /// <param name="innerException"></param>
        public BaseException(string message, int code, Exception innerException) : base(message, innerException)
        {
            Code = code;
        }

        /// <summary>
        /// Exception Code <see cref="StatusCodes"/>
        /// </summary>
        public int Code { get; set; }
    }
}
