using Enterprise.Domain;
using static TwoSum.Domain.Solution.SolutionIterationStatus;

namespace TwoSum.Domain.Solution;

public sealed class SolutionIterationStatus : IValueObject<SolutionIterationStatusEnum>
{
    public SolutionIterationStatus(SolutionIterationStatusEnum status)
    {
        if (!Enum.IsDefined(typeof(SolutionIterationStatusEnum), status))
        {
            throw new Exception("There is no such status available!"); // TODO: create custom exception
        }

        Value = status;
    }

    public SolutionIterationStatusEnum Value { get; init; }

    public enum SolutionIterationStatusEnum
    {
        Started = 0,
        Processing,
        Finished
    }
}
