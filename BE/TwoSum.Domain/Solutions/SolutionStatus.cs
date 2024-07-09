using Enterprise.Domain;
using TwoSum.Domain.Exceptions;
using static TwoSum.Domain.Solutions.SolutionStatus;

namespace TwoSum.Domain.Solutions;

public sealed class SolutionStatus : IValueObject<SolutionStatusEnum>
{
    public SolutionStatus(SolutionStatusEnum status)
    {
        if (!Enum.IsDefined(typeof(SolutionStatusEnum), status))
        {
            throw new InvalidEnumException("There is no such status available!");
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
