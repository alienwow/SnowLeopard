using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace SnowLeopard.Infrastructure
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [AllowAnonymous]
    public class HealthController : BaseApiController
    {
        private readonly ILogger _logger;

        /// <summary>
        /// HealthController
        /// </summary>
        /// <param name="loggerFactory"></param>
        public HealthController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<HealthController>();
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string Get()
        {
            _logger.LogInformation($"时间：{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
            return "ok";
        }
    }
}
