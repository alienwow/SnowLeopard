using Exceptionless.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SnowLeopard.Extensions;

namespace SnowLeopard.WebApi.Controllers
{
    /// <summary>
    /// Session
    /// </summary>
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SessionController : Controller
    {
        [HttpGet("SessionTest")]
        public IActionResult SessionTest([FromQuery]string num)
        {
            string[] lastTime = HttpContext.Session.Get<string[]>("sessiontest");
            var res = new string[2] { num, HttpContext.Session.Id };
            HttpContext.Session.Set<string[]>("sessiontest", res);
            string[] res1 = HttpContext.Session.Get<string[]>("sessiontest");

            string last = null;
            if (lastTime != null)
                last = string.Join(',', lastTime);

            return Content(JsonConvert.SerializeObject(new
            {
                Last = last,
                Current = string.Join(',', res),
                Writed = string.Join(',', res1)
            }));
        }
    }
}
