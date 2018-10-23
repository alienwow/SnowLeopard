using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using SnowLeopard.Model.BaseModels;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SnowLeopard.Infrastructure
{
    /// <summary>
    /// ModelStateFilter
    /// </summary>
    public class ModelStateFilter : ActionFilterAttribute
    {
        private readonly ILogger _logger;
        private readonly IHostingEnvironment _env;

        /// <summary>
        /// ModelStateFilter
        /// </summary>
        /// <param name="loggerFactory"></param>
        /// <param name="env"></param>
        public ModelStateFilter(ILoggerFactory loggerFactory, IHostingEnvironment env)
        {
            _logger = loggerFactory.CreateLogger<GlobalExceptionFilter>();
            _env = env;
        }

        /// <summary>
        /// 在Action执行之前由 MVC 框架调用。
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                _logger.LogInformation("模型绑定失败！");

                var modelStateErrorMsgs = new List<ModelStateErrorMsg>();
                foreach (var modelStatePair in context.ModelState)
                {
                    var key = modelStatePair.Key;
                    var errors = modelStatePair.Value.Errors;
                    if (errors != null && errors.Count > 0)
                    {
                        var errorMessages = errors.Select(error =>
                        {
                            return string.IsNullOrEmpty(error.ErrorMessage) ?
                               "The input was not valid." : error.ErrorMessage;
                        }).ToArray();

                        modelStateErrorMsgs.Add(new ModelStateErrorMsg(key, errorMessages));
                    }
                }

                var result = new BaseDTO<List<ModelStateErrorMsg>>()
                {
                    Code = (int)HttpStatusCode.BadRequest,
                    Msg = nameof(HttpStatusCode.BadRequest),
                    Data = modelStateErrorMsgs
                };

                context.Result = new BadRequestObjectResult(result);
            }
            base.OnActionExecuting(context);
        }

        /// <summary>
        /// 在Action执行之前由 MVC 框架调用
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            return base.OnActionExecutionAsync(context, next);
        }

        /// <summary>
        /// 在Action执行之后由 MVC 框架调用
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }

        /// <summary>
        /// 在执行Result之前由 MVC 框架调用
        /// </summary>
        /// <param name="context"></param>
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            base.OnResultExecuting(context);
        }

        /// <summary>
        /// 在执行Result之前由 MVC 框架调用
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public override Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            return base.OnResultExecutionAsync(context, next);
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
