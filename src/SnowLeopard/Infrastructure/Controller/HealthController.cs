using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SnowLeopard.Lynx;

namespace SnowLeopard.Infrastructure
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [AllowAnonymous]
    public class HealthController : BaseApiController
    {
        private readonly ILogger _logger;
        private readonly LynxUtils _lynxUtils;

        /// <summary>
        /// HealthController
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="lynxUtils"></param>
        public HealthController(
            ILogger<HealthController> logger,
            LynxUtils lynxUtils
            )
        {
            _logger = logger;
            _lynxUtils = lynxUtils;
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string Get()
        {
            return _lynxUtils.EntryAssemblyVersion;
        }
    }
}
