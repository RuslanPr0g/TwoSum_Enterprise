using Enterprise.Application.Generators;
using Enterprise.Application.Requests;
using Microsoft.Extensions.Logging;
using TwoSum.Application.Contracts;
using TwoSum.Application.Results;
using TwoSum.Domain.Solution;

namespace TwoSum.Application.Services;

public sealed class SolutionService : ISolutionService
{
    private readonly ILogger<SolutionService> _logger;
    private readonly IIdGenerator<Guid> _idGenerator;
    private readonly ISolutionRepository _repository;

    public SolutionService(ILogger<SolutionService> logger, IIdGenerator<Guid> idGenerator, ISolutionRepository repository)
    {
        _logger = logger;
        _idGenerator = idGenerator;
        _repository = repository;
    }

    public async Task<SolutionAddedResult> ProposeSolution(AddSolutionRequest request)
    {
        _logger.LogInformation("Proposed solution target is: {0}", request.Target);

        if (request.Nums is null || request.Nums.Length <= 0)
        {
            _logger.LogInformation("Solution contains an empty collection: {0}", request);
            return SolutionAddedResult.CreateFail("No items found in the solution collection.");
        }

        Guid solutionId = _idGenerator.Generate();
        Solution solution = Solution.Create(solutionId, request.Nums, request.Target);

        await _repository.CreateSolution(solution);
        await _repository.SaveChanges();
        // TODO: add ef core interceptor to publish domain events

        _logger.LogInformation("Proposed solution Id is: {0}", solutionId);

        return SolutionAddedResult.CreateSuccess(solutionId);
    }
}
