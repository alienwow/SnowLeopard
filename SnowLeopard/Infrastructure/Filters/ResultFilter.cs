using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace SnowLeopard.Infrastructure.Filters
{
    /// <summary>
    /// ResultFilter
    /// </summary>
    public class ResultFilter : ActionFilterAttribute
    {
        private readonly ILogger _logger;
        private readonly IHostingEnvironment _env;

        /// <summary>
        /// ActionResultFilter
        /// </summary>
        /// <param name="loggerFactory"></param>
        /// <param name="env"></param>
        public ResultFilter(ILoggerFactory loggerFactory, IHostingEnvironment env)
        {
            _logger = loggerFactory.CreateLogger<ResultFilter>();
            _env = env;
        }

        /// <summary>
        /// 在执行Result之前由 MVC 框架调用
        /// </summary>
        /// <param name="context"></param>
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.ModelState.IsValid)
            {
                //根据实际需求进行具体实现
                if (context.Result is ObjectResult)
                {
                    var objectResult = context.Result as ObjectResult;
                    var result = new BaseViewModel<object>();
                    if (objectResult.Value == null)
                    {
                        result.Code = (int)HttpStatusCode.NoContent;
                        result.Msg = nameof(HttpStatusCode.NoContent);
                    }
                    else
                    {
                        result.Code = (int)HttpStatusCode.OK;
                        result.Msg = nameof(HttpStatusCode.OK);
                    }

                    result.Data = objectResult.Value;
                    context.Result = new OkObjectResult(result);
                }
                else if (context.Result is EmptyResult)
                {
                    var result = new BaseViewModel<object>()
                    {
                        Code = (int)HttpStatusCode.NoContent,
                        Msg = nameof(HttpStatusCode.NoContent),
                        Data = context.Result
                    };
                    context.Result = new OkObjectResult(result);
                }
                else if (context.Result is ContentResult)
                {
                    var contentResult = context.Result as ContentResult;

                    var result = new BaseViewModel<object>()
                    {
                        Code = (int)HttpStatusCode.OK,
                        Msg = nameof(HttpStatusCode.OK),
                        Data = contentResult.Content
                    };
                    context.Result = new OkObjectResult(result);
                }
                else if (context.Result is StatusCodeResult)
                {
                    var statusCodeResult = context.Result as StatusCodeResult;

                    var result = new BaseViewModel<object>()
                    {
                        Code = statusCodeResult.StatusCode,
                        Msg = ((HttpStatusCode)statusCodeResult.StatusCode).ToString(),
                        Data = context.Result
                    };
                    context.Result = new OkObjectResult(result);
                }
            }
            base.OnResultExecuting(context);
        }

        /// <summary>
        /// 在执行Result之前由 MVC 框架调用
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (next == null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            OnResultExecuting(context);
            if (!context.Cancel)
            {
                OnResultExecuted(await next());
            }
        }

        /// <summary>
        /// 在执行Result后由 MVC 框架调用
        /// </summary>
        /// <param name="context"></param>
        public override void OnResultExecuted(ResultExecutedContext context)
        {
            base.OnResultExecuted(context);
        }

    }
}
