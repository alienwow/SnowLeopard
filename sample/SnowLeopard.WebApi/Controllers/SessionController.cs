using System;
using Exceptionless.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SnowLeopard.Infrastructure;

namespace SnowLeopard.WebApi.Controllers
{
    /// <summary>
    /// Session
    /// </summary>
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SessionController : BaseApiController
    {
        [HttpGet("SessionTest")]
        public IActionResult SessionTest()
        {
            //Thread.Sleep(100);
            string[] lastTime = HttpContext.Session.Get<string[]>("sessiontest");
            Random rand = new Random();
            var res = new string[5];
            for (int i = 0; i < 4; i++)
            {
                //res[i] = Guid.NewGuid().ToString();
                res[i] = rand.Next(0, 10).ToString();
            }
            res[4] = HttpContext.Session.Id;
            HttpContext.Session.Set<string[]>("sessiontest", res);
            //Thread.Sleep(100);
            string[] res1 = HttpContext.Session.Get<string[]>("sessiontest");
            //Thread.Sleep(100);
            return Content($"<br />上次：【{(lastTime == null ? string.Empty : string.Join(',', lastTime))}】<br />这次：【{string.Join(',', res)}】<br />写入后：【{string.Join(',', res1)}】<br /> , <a href='/home/SessionTest2' target='_blank'>点击读取Session</a>", "text/html", System.Text.Encoding.UTF8);
        }

        [HttpGet("SessionTest2")]
        public IActionResult SessionTest2()
        {
            //Thread.Sleep(100);
            string[] res = HttpContext.Session.Get<string[]>("sessiontest");
            return Content(string.Join(',', res));
        }
    }

    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) :
                                  JsonConvert.DeserializeObject<T>(value);
        }
    }
}
