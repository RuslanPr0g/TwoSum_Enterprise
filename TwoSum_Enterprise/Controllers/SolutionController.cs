using Enterprise.Application.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TwoSum.Application.Contracts;
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
        private readonly ISolutionRepository _repo;

        public SolutionController(ILogger<SolutionController> logger, IMediator mediator, ISolutionRepository repo)
        {
            _logger = logger;
            _mediator = mediator;
            _repo = repo;
        }

        [HttpPost("solution")]
        public async Task<SolutionAddedResult> Post([FromBody] AddSolutionRequest request)
        {
            _logger.LogInformation("Got request to add a solution attempt: {0}", request);
            return await _mediator.Send(new PostSolutionQuery(request));
        }

        // TODO: REFACTOR TO USE ENTERPRISE BET PRACTICES WITH ELASTIC SEARCH, ETC.
        [HttpGet("solution")]
        public async Task<IActionResult> Get(Guid solutionId)
        {
            var solution = await _repo.GetSolutionById(new TwoSum.Domain.Solution.SolutionId(solutionId));

            if (solution is null)
            {
                return NotFound($"No solution is found by the id {solutionId}");
            }

            var result = solution.RetrieveSolution();

            if (!result.IsSuccess)
            {
                return NotFound(result.Message);
            }

            return Ok(new { Message = $"I: {result.I}, J: {result.J}" });
        }
    }
}
