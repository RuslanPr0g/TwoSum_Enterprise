using MediatR;
using Microsoft.AspNetCore.Mvc;
using TwoSum.Application.Solutions.Query;
using TwoSum.Application.Solutions.Requests;

namespace TwoSum_Enterprise.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SolutionController : ControllerBase
    {
        private readonly ILogger<SolutionController> _logger;
        private readonly IMediator _mediator;

        public SolutionController(ILogger<SolutionController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost("solution")]
        public async Task<IActionResult> Post([FromBody] AddSolutionRequest request)
        {
            _logger.LogInformation("Got request to add a solution attempt: {0}", request);
            return Ok(await _mediator.Send(new PostSolutionQuery(request)));
        }

        [HttpGet("solution")]
        public async Task<IActionResult> Get(Guid solutionId)
        {
            var result = await _mediator.Send(new ReadSolutionQuery(solutionId));

            if (result is null)
            {
                return NotFound($"No solution is found by the id {solutionId}");
            }

            if (!result.IsSuccess)
            {
                if (result.Message is null) return BadRequest(result);
                if (result.Message.Contains("yet")) return Problem("wait for your solution, please...", statusCode: 102);
            }

            return Ok(result);
        }
    }
}
