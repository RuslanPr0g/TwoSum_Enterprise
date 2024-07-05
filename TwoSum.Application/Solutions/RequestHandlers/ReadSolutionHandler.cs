using MediatR;
using TwoSum.Application.Solutions.Contracts;
using TwoSum.Application.Solutions.Query;
using TwoSum.Application.Solutions.Results;

namespace TwoSum.Application.Solutions.RequestHandlers;

public class ReadSolutionHandler : IRequestHandler<ReadSolutionQuery, ComputedSolutionResult>
{
    private readonly ISolutionService _solutionService;

    public ReadSolutionHandler(ISolutionService solutionService) => _solutionService = solutionService;

    public async Task<ComputedSolutionResult> Handle(ReadSolutionQuery query, CancellationToken cancellationToken) =>
        await _solutionService.RetrieveSolution(query);
}