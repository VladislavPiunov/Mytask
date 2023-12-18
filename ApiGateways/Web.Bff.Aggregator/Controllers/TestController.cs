using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Shared.Dto;
using Shared.Options;

namespace Web.Bff.Aggregator.Controllers
{
    [ApiController]
    [Route("api/test")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;

        private readonly DescriptionConfiguration _descConf;

        public TestController(
            ILogger<TestController> logger,
            IOptions<DescriptionConfiguration> descConf)
        {
            _logger = logger;

            _descConf = descConf.Value ?? throw new ArgumentNullException(nameof(DescriptionConfiguration));
        }

        [HttpGet("config")]
        [ProducesResponseType(typeof(ServiceConfigDTO), 200)]
        public async Task<IActionResult> GetConfig()
        {
            return Ok(new ServiceConfigDTO(_descConf, null));
        }
    }
}