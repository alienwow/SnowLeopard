using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Net;

namespace SnowLeopard.Infrastructure.Filters
{
    /// <summary>
    /// HttpGlobalExceptionFilter
    /// </summary>
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly IHostingEnvironment _env;

        /// <summary>
        /// HttpGlobalExceptionFilter
        /// </summary>
        /// <param name="loggerFactory"></param>
        /// <param name="env"></param>
        public HttpGlobalExceptionFilter(ILoggerFactory loggerFactory, IHostingEnvironment env)
        {
            _loggerFactory = loggerFactory;
            _env = env;
        }

        /// <summary>
        /// OnException
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            var logger = _loggerFactory.CreateLogger(context.Exception.TargetSite.ReflectedType);

            logger.LogError(new EventId(context.Exception.HResult),
                            context.Exception,
                            context.Exception.Message);

            var json = new ErrorResponse("未知错误,请重试");

            if (_env.IsDevelopment()) json.DeveloperMessage = context.Exception;

            context.Result = new ApplicationErrorResult(json);
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            context.ExceptionHandled = true;
        }

        /// <summary>
        /// ApplicationErrorResult
        /// </summary>
        public class ApplicationErrorResult : ObjectResult
        {
            /// <summary>
            /// ApplicationErrorResult
            /// </summary>
            /// <param name="value"></param>
            public ApplicationErrorResult(object value) : base(value)
            {
                StatusCode = (int)HttpStatusCode.InternalServerError;
            }
        }

        /// <summary>
        /// ErrorResponse
        /// </summary>
        public class ErrorResponse
        {
            /// <summary>
            /// ErrorResponse
            /// </summary>
            /// <param name="msg"></param>
            public ErrorResponse(string msg)
            {
                Message = msg;
            }

            /// <summary>
            /// Message
            /// </summary>
            public string Message { get; set; }

            /// <summary>
            /// DeveloperMessage
            /// </summary>
            public object DeveloperMessage { get; set; }
        }
    }
}
