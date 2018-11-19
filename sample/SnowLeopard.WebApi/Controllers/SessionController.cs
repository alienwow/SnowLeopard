using System.Threading.Tasks;
using Exceptionless.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetAsync(key, value).Wait();
        }

        public static async Task SetAsync<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
            await session.CommitAsync();
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default :
                                  JsonConvert.DeserializeObject<T>(value);
        }

        public static async Task<T> GetAsync<T>(this ISession session, string key)
        {
            await session.LoadAsync();
            var value = session.GetString(key);
            return value == null ? default :
                                  JsonConvert.DeserializeObject<T>(value);
        }
    }
}
