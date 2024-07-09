using MediatR;
using TwoSum.Application.Solutions.Contracts;
using TwoSum.Application.Solutions.Query;
using TwoSum.Application.Solutions.Results;

namespace TwoSum.Application.Solutions.RequestHandlers;

public class PostSolutionHandler : IRequestHandler<PostSolutionQuery, SolutionAddedResult>
{
    private readonly ISolutionService _solutionService;

    public PostSolutionHandler(ISolutionService solutionService) => _solutionService = solutionService;

    public async Task<SolutionAddedResult> Handle(PostSolutionQuery query, CancellationToken cancellationToken) =>
        await _solutionService.ProposeSolution(query.Request);
}