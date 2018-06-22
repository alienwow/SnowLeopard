using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;

namespace SnowLeopard.Infrastructure.Filters
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
                var result = new BaseDTO<SerializableError>()
                {
                    Code = (int)HttpStatusCode.BadRequest,
                    Msg = nameof(HttpStatusCode.BadRequest),
                    Data = new SerializableError(context.ModelState)
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
