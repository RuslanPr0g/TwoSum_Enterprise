using MediatR;
using TwoSum.Application.Solutions.Requests;
using TwoSum.Application.Solutions.Results;

namespace TwoSum.Application.Solutions.Query;

public record PostSolutionQuery(AddSolutionRequest Request) : IRequest<SolutionAddedResult>;

public record ReadSolutionQuery(Guid SolutionId) : IRequest<ComputedSolutionResult>;