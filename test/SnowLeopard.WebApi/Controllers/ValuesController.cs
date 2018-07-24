using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SnowLeopard.Infrastructure;
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
        public ValuesController(ILogger<ValuesController> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// Get
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(BaseDTO<IEnumerable<string>>), (int)HttpStatusCode.OK)]
        public async Task<IEnumerable<string>> Get()
        {
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
            _logger.LogInformation($"即将发生异常：{count}");
            switch (count++)
            {
                case 0: throw new MemberAccessException(); break;
                case 1: throw new ArgumentException(); break;
                case 2: throw new ArgumentNullException(); break;
                case 3: throw new ArithmeticException(); break;
                case 4: throw new ArrayTypeMismatchException(); break;
                case 5: throw new DivideByZeroException(); break;
                case 6: throw new FormatException(); break;
                case 7: throw new IndexOutOfRangeException(); break;
                case 8: throw new InvalidCastException(); break;
                case 9: throw new MulticastNotSupportedException(); break;
                case 10: throw new NotSupportedException(); break;
                case 11: throw new NullReferenceException(); break;
                case 12: throw new OutOfMemoryException(); break;
                case 13: throw new OverflowException(); break;
                case 14: throw new StackOverflowException(); break;
                case 15: throw new TypeInitializationException("", null); break;
                case 16: throw new NotFiniteNumberException(); break;
                case 17: throw new Exceptions.BaseException($"参数{nameof(value)}为空"); break;
                case 18: throw new Exception("为捕获的异常"); break;
                default:
                    throw new Exception("为捕获的异常");
                    break;
            }
            if (string.IsNullOrEmpty(value))
            {
                throw new Exceptions.BaseException($"参数{nameof(value)}为空");
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
