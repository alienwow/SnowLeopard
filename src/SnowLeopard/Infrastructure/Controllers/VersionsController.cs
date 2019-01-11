using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SnowLeopard.Lynx;

namespace SnowLeopard.Infrastructure
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [AllowAnonymous]
    public class VersionsController : BaseApiController
    {
        private readonly ILogger _logger;
        private readonly SnowLeopardUtils _snowLeopardUtils;
        private readonly LynxUtils _lynxUtils;

        /// <summary>
        /// VersionsController
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="snowLeopardUtils"></param>
        /// <param name="lynxUtils"></param>
        public VersionsController(
            ILogger<VersionsController> logger,
            SnowLeopardUtils snowLeopardUtils,
            LynxUtils lynxUtils
        )
        {
            _logger = logger;
            _snowLeopardUtils = snowLeopardUtils;
            _lynxUtils = lynxUtils;
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <returns></returns>
        [HttpGet("EntryAssemblyVersion")]
        public string GetEntryAssemblyVersion()
        {
            return _lynxUtils.EntryAssemblyVersion;
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
