using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MyWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;

        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("Received GET request.");
            return Ok("Hello, HTTPS!");
        }
    }
}
