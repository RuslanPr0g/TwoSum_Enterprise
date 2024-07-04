using Enterprise.Application;
using Enterprise.Application.Requests;
using TwoSum.Application.Results;

namespace TwoSum.Application.Contracts;

public interface ISolutionService : IApplicationService
{
    Task<SolutionAddedResult> ProposeSolution(AddSolutionRequest request);
}