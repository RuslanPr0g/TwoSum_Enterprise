using Enterprise.Domain;
using static TwoSum.Domain.Solution.SolutionStatus;

namespace TwoSum.Domain.Solution;

public sealed class SolutionStatus : IValueObject<SolutionStatusEnum>
{
    public SolutionStatus(SolutionStatusEnum status)
    {
        if (!Enum.IsDefined(typeof(SolutionStatusEnum), status))
        {
            throw new Exception("There is no such status available!"); // TODO: create custom exception
        }

        Value = status;
    }

    public SolutionStatusEnum Value { get; init; }

    public enum SolutionStatusEnum
    {
        Started = 0,
        InProgress,
        Solved
    }
}
