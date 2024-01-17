using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Mytask.API.Extensions.Options;
using Steeltoe.Common.Discovery;
using Steeltoe.Discovery;

namespace Mytask.API.Controllers
{
    [ApiController]
    [Route("api/test")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;
        private readonly DiscoveryHttpClientHandler _handler;

        private readonly ConnectionsConfiguration _connConf;

        public TestController(
            ILogger<TestController> logger,
            IDiscoveryClient client,
            IOptions<ConnectionsConfiguration> envireConf)
        {
            _logger = logger;
            _handler = new DiscoveryHttpClientHandler(client);

            _connConf = envireConf.Value ?? throw new ArgumentNullException(nameof(ConnectionsConfiguration));
        }

        [HttpGet("config")]
        [ProducesResponseType(typeof(ConnectionsConfiguration), 200)]
        public IActionResult GetConfig()
        {
            return Ok(_connConf);
        }
    }
}
