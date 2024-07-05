using Enterprise.Domain;

namespace TwoSum.Domain.Solutions;

public record SolutionIterationId(Guid Value) : Identity(Value);