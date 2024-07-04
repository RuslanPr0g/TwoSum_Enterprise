using Enterprise.Domain;

namespace TwoSum.Domain.Solution;

public record SolutionIterationId(Guid Value) : Identity(Value);