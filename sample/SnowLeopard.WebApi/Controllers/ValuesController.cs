using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SnowLeopard.Infrastructure;
using SnowLeopard.Infrastructure.Http;
using SnowLeopard.Model.BaseModels;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace SnowLeopard.WebApi.Controllers
{
    /// <summary>
    /// Values
    /// </summary>
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ValuesController : BaseApiController
    {
        private readonly ILogger _logger;
        private readonly SnowLeopardHttpClient _snowLeopardHttpClient;

        public ValuesController(
            ILogger<ValuesController> logger,
            SnowLeopardHttpClient snowLeopardHttpClient
        )
        {
            _logger = logger;
            _snowLeopardHttpClient = snowLeopardHttpClient;
        }
        /// <summary>
        /// Get
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(BaseDTO<IEnumerable<string>>), (int)HttpStatusCode.OK)]
        public async Task<IEnumerable<string>> Get()
        {
            var result = await _snowLeopardHttpClient.GetAsync<string>("http://10.100.82.157:8013/api/v1/Health");
            return await Task.FromResult(new string[] { "value1", "value2" });
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BaseDTO<IEnumerable<string>>), (int)HttpStatusCode.OK)]
        public async Task<string> Get(int id)
        {
            return await Task.FromResult("value");
        }

        static int count = 0;

        /// <summary>
        /// Post
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public void Post([FromBody] string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new Exceptions.BaseException($"参数{nameof(value)}为空");
            }
            _logger.LogInformation($"即将发生异常：{count}");
            switch (count++)
            {
                case 0: throw new MemberAccessException();
                case 1: throw new ArgumentException();
                case 2: throw new ArgumentNullException();
                case 3: throw new ArithmeticException();
                case 4: throw new ArrayTypeMismatchException();
                case 5: throw new DivideByZeroException();
                case 6: throw new FormatException();
                case 7: throw new IndexOutOfRangeException();
                case 8: throw new InvalidCastException();
                case 9: throw new MulticastNotSupportedException();
                case 10: throw new NotSupportedException();
                case 11: throw new NullReferenceException();
                case 12: throw new OutOfMemoryException();
                case 13: throw new OverflowException();
                case 14: throw new StackOverflowException();
                case 15: throw new TypeInitializationException("", null);
                case 16: throw new NotFiniteNumberException();
                case 17: throw new Exceptions.BaseException($"参数{nameof(value)}为空");
                case 18: throw new Exception("为捕获的异常");
                default:
                    throw new Exception("为捕获的异常");
            }
        }

        /// <summary>
        /// Put
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
