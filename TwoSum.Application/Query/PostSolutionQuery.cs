using Enterprise.Application.Requests;
using MediatR;
using TwoSum.Application.Results;

namespace TwoSum.Application.Query;

public record PostSolutionQuery(AddSolutionRequest Request) : IRequest<SolutionAddedResult>;