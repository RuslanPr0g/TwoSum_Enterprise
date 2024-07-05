using Enterprise.Domain;
using TwoSum.Domain.Exceptions;
using static TwoSum.Domain.Solutions.SolutionIterationStatus;

namespace TwoSum.Domain.Solutions;

public sealed class SolutionIterationStatus : IValueObject<SolutionIterationStatusEnum>
{
    public SolutionIterationStatus(SolutionIterationStatusEnum status)
    {
        if (!Enum.IsDefined(typeof(SolutionIterationStatusEnum), status))
        {
            throw new InvalidEnumException("There is no such status available!");
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
