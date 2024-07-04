using MediatR;
using TwoSum.Application.Contracts;
using TwoSum.Application.Query;
using TwoSum.Application.Results;

namespace TwoSum.Application.RequestHandlers;

public class PostSolutionHandler : IRequestHandler<PostSolutionQuery, SolutionAddedResult>
{
    private readonly ISolutionService _solutionService;

    public PostSolutionHandler(ISolutionService solutionService) => _solutionService = solutionService;

    public async Task<SolutionAddedResult> Handle(PostSolutionQuery query, CancellationToken cancellationToken) => 
        await _solutionService.ProposeSolution(query.Request);
}