using Microsoft.AspNetCore.Mvc;
using SnowLeopard.Infrastructure;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace SnowLeopard.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ValuesController : BaseApiController
    {
        // GET api/values
        [HttpGet]
        [ProducesResponseType(typeof(BaseDTO<IEnumerable<string>>), (int)HttpStatusCode.OK)]
        public async Task<IEnumerable<string>> Get()
        {
            return await Task.FromResult(new string[] { "value1", "value2" });
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BaseDTO<IEnumerable<string>>), (int)HttpStatusCode.OK)]
        public async Task<string> Get(int id)
        {
            return await Task.FromResult("value");
        }

        static int count = 0;
        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
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
                default:
                    break;
            }
            if (string.IsNullOrEmpty(value))
            {
                throw new Exceptions.BaseException($"参数{nameof(value)}为空");
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
