using Enterprise.Application.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TwoSum.Application.Query;
using TwoSum.Application.Results;

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
        public async Task<SolutionAddedResult> Post([FromBody] AddSolutionRequest request)
        {
            _logger.LogInformation("Got request to add a solution attempt: {0}", request);
            return await _mediator.Send(new PostSolutionQuery(request));
        }
    }
}
