using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using SnowLeopard.Model.BaseModels;

namespace SnowLeopard.Infrastructure
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
                var ignoreResult = context.Controller.GetType().GetCustomAttributes(typeof(IgnoreResultFilterAttribute), true);
                if (ignoreResult == null)
                {
                    var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
                    if (controllerActionDescriptor != null)
                    {
                        ignoreResult = controllerActionDescriptor.MethodInfo.GetCustomAttributes(typeof(IgnoreResultFilterAttribute), true);
                        if (ignoreResult != null)
                        {
                            base.OnResultExecuting(context);
                            return;
                        }
                    }
                }
                else
                {
                    base.OnResultExecuting(context);
                    return;
                }

                //根据实际需求进行具体实现
                if (context.Result is ObjectResult)
                {
                    var objectResult = context.Result as ObjectResult;
                    var result = new BaseDTO<object>();
                    if (objectResult.Value == null)
                    {
                        result.Code = StatusCodes.Status204NoContent;
                        result.Msg = nameof(StatusCodes.Status204NoContent);
                    }
                    else
                    {
                        result.Code = StatusCodes.Status200OK;
                        result.Msg = nameof(StatusCodes.Status200OK);
                    }

                    result.Data = objectResult.Value;
                    context.Result = new OkObjectResult(result);
                }
                else if (context.Result is EmptyResult)
                {
                    var result = new BaseDTO<object>()
                    {
                        Code = StatusCodes.Status204NoContent,
                        Msg = nameof(StatusCodes.Status204NoContent),
                        Data = context.Result
                    };
                    context.Result = new OkObjectResult(result);
                }
                else if (context.Result is ContentResult)
                {
                    var contentResult = context.Result as ContentResult;

                    var result = new BaseDTO<object>()
                    {
                        Code = StatusCodes.Status200OK,
                        Msg = nameof(StatusCodes.Status200OK),
                        Data = contentResult.Content
                    };
                    context.Result = new OkObjectResult(result);
                }
                else if (context.Result is StatusCodeResult)
                {
                    var statusCodeResult = context.Result as StatusCodeResult;

                    var result = new BaseDTO<object>()
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
