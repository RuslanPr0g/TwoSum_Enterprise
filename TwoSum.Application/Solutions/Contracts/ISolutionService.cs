using Enterprise.Application;
using TwoSum.Application.Solutions.Query;
using TwoSum.Application.Solutions.Requests;
using TwoSum.Application.Solutions.Results;

namespace TwoSum.Application.Solutions.Contracts;

// TODO: Split the system into read and write parts
public interface ISolutionService : IApplicationService
{
    Task<ComputedSolutionResult> RetrieveSolution(ReadSolutionQuery request);

    Task<SolutionAddedResult> ProposeSolution(AddSolutionRequest request);
}