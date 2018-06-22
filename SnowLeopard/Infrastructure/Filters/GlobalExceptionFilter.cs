using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using SnowLeopard.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SnowLeopard.Infrastructure.Filters
{
    /// <summary>
    /// GlobalExceptionFilter
    /// </summary>
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly IHostingEnvironment _env;

        /// <summary>
        /// 捕获的异常类
        /// </summary>
        private static readonly IEnumerable<Type> _baseExceptions = new Type[]
        {
            typeof(BaseException),
            typeof(ArgumentOutOfRangeException),
            typeof(MemberAccessException),              // 访问错误：类型成员不能被访问
            typeof(ArgumentException),                  // 参数错误：方法的参数无效
            typeof(ArgumentNullException),              // 参数为空：给方法传递一个不可接受的空参数
            typeof(ArithmeticException),                // 数学计算错误：由于数学运算导致的异常，覆盖面广
            typeof(ArrayTypeMismatchException),         // 数组类型不匹配
            typeof(DivideByZeroException),              // 被零除
            typeof(FormatException),                    // 参数的格式不正确
            typeof(IndexOutOfRangeException),           // 索引超出范围，小于0或比最后一个元素的索引还大
            typeof(InvalidCastException),               // 非法强制转换，在显式转换失败时引发
            typeof(MulticastNotSupportedException),     // 不支持的组播：组合两个非空委派失败时引发
            typeof(NotSupportedException),              // 调用的方法在类中没有实现
            typeof(NullReferenceException),             // 引用空引用对象时引发
            typeof(OutOfMemoryException),               // 无法为新语句分配内存时引发，内存不足
            typeof(OverflowException),                  // 溢出
            typeof(StackOverflowException),             // 栈溢出
            typeof(TypeInitializationException),        // 错误的初始化类型：静态构造函数有问题时引发
            typeof(NotFiniteNumberException),           // 无限大的值：数字不合法
        };

        /// <summary>
        /// GlobalExceptionFilter
        /// </summary>
        /// <param name="loggerFactory"></param>
        /// <param name="env"></param>
        public GlobalExceptionFilter(ILoggerFactory loggerFactory, IHostingEnvironment env)
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

            ObjectResult result = null;
            BaseDTO<object> baseResult = new BaseDTO<object>()
            {
                Code = StatusCodes.Status500InternalServerError,
                Msg = context.Exception.Message,
                Data = context.Exception.Message
            };
            if (_baseExceptions.Contains(context.Exception.GetType()) || context.Exception.GetType().IsSubclassOf(typeof(BaseException)))
            {
                if (context.Exception is BaseException baseException)
                {
                    baseResult.Code = baseException.Code;
                }
                result = new ApplicationErrorResult(baseResult);
            }
            else
            {
                result = new InternalServerErrorObjectResult(baseResult);
            }

            if (_env.IsDevelopment()) baseResult.Data = context.Exception;

            context.Result = result;
            context.ExceptionHandled = true;
        }

        /// <summary>
        /// An <see cref="ObjectResult"/> that when executed will produce a Bad Request (400) response.
        /// </summary>
        public class ApplicationErrorResult : ObjectResult
        {
            /// <summary>
            /// ApplicationErrorResult
            /// </summary>
            /// <param name="value"></param>
            public ApplicationErrorResult(object value) : base(value)
            {
                StatusCode = StatusCodes.Status200OK;
            }
        }


        /// <summary>
        /// An <see cref="ObjectResult"/> that when executed will produce a Internal Server Error (500) response.
        /// </summary>
        public class InternalServerErrorObjectResult : ObjectResult
        {
            /// <summary>
            /// Creates a new <see cref="BadRequestObjectResult"/> instance.
            /// </summary>
            /// <param name="error">Contains the errors to be returned to the client.</param>
            public InternalServerErrorObjectResult(object error)
                : base(error)
            {
                StatusCode = StatusCodes.Status500InternalServerError;
            }

        }
    }
}
