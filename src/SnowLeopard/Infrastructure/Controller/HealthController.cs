using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SnowLeopard.Infrastructure
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [AllowAnonymous]
    public class HealthController : BaseApiController
    {
        private readonly ILogger _logger;
        private readonly CommonUtils _commonUtils;

        /// <summary>
        /// HealthController
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="commonUtils"></param>
        public HealthController(
            ILogger<HealthController> logger,
            CommonUtils commonUtils
            )
        {
            _logger = logger;
            _commonUtils = commonUtils;
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string Get()
        {
            return _commonUtils.EntryAssemblyVersion;
        }
    }
}
