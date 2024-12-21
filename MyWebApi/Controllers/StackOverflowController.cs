using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MyWebApi.Services;
using System.Threading.Tasks;

namespace MyWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowLocalhost3000")]
    public class StackOverflowController : ControllerBase
    {
        private readonly StackOverflowService _stackOverflowService;
        private readonly StackOverflowSearchService _stackOverflowSearchService;

        public StackOverflowController(StackOverflowService stackOverflowService, StackOverflowSearchService stackOverflowSearchService)
        {
            _stackOverflowService = stackOverflowService;
            _stackOverflowSearchService = stackOverflowSearchService;
        }

        [HttpGet("recent-answers")]
        public async Task<IActionResult> GetRecentAnswers()
        {
            var result = await _stackOverflowService.GetRecentAnswersAsync();
            return Ok(result);
        }

        [HttpGet("manipulated-answers")]
        public async Task<IActionResult> GetManipulatedAnswers()
        {
            var answers = await _stackOverflowService.GetRecentAnswersAsync();
            return Ok(answers);
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetSearchedAnswers([FromQuery] string query, [FromQuery] int page)
        {
            var relevantAnswers = await _stackOverflowSearchService.GetRelevantSearchAnswers(query, page);
            return Ok(relevantAnswers);
        }
    }
}
