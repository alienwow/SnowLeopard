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

        /// <summary>
        /// VersionsController
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="snowLeopardUtils"></param>
        public VersionsController(
            ILogger<VersionsController> logger,
            SnowLeopardUtils snowLeopardUtils
            )
        {
            _logger = logger;
            _snowLeopardUtils = snowLeopardUtils;
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <returns></returns>
        [HttpGet("EntryAssemblyVersion")]
        public string GetEntryAssemblyVersion()
        {
            return _snowLeopardUtils.EntryAssemblyVersion;
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
