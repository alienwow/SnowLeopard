using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SnowLeopard.Infrastructure
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [AllowAnonymous]
    public class VersionsController : BaseApiController
    {
        private readonly ILogger _logger;
        private readonly SnowLeopardUtils _snowLeopardUtils;
        private readonly CommonUtils _commonUtils;

        /// <summary>
        /// VersionsController
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="snowLeopardUtils"></param>
        /// <param name="commonUtils"></param>
        public VersionsController(
            ILogger<VersionsController> logger,
            SnowLeopardUtils snowLeopardUtils,
            CommonUtils commonUtils
        )
        {
            _logger = logger;
            _snowLeopardUtils = snowLeopardUtils;
            _commonUtils = commonUtils;
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <returns></returns>
        [HttpGet("EntryAssemblyVersion")]
        public string GetEntryAssemblyVersion()
        {
            return _commonUtils.EntryAssemblyVersion;
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <returns></returns>
        [HttpGet("SnowLeopardVersion")]
        public string GetSnowLeopardVersion()
        {
            return _snowLeopardUtils.SnowLeopardVersion;
        }
    }
}
