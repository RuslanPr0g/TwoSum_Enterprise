using Enterprise.Domain;
using TwoSum.Domain.Solutions;

namespace TwoSum.Domain.Events;

public sealed class SolutionCreatedDomainEvent(SolutionId SolutionId) : IDomainEvent
{
    public SolutionId SolutionId { get; } = SolutionId;
}

public sealed class NextSolutionIterationRequested(SolutionId SolutionId) : IDomainEvent
{
    public SolutionId SolutionId { get; } = SolutionId;
}
