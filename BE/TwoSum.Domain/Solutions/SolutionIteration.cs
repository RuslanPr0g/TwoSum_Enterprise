using Enterprise.Domain;
using System.Xml.XPath;

namespace TwoSum.Domain.Solutions;

public sealed class SolutionIteration : Entity<SolutionIterationId>
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private SolutionIteration(SolutionIterationId id, SolutionId solutionId, int index)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        : base(id)
    {
        SolutionId = solutionId;
        Index = index;
        Status = new SolutionIterationStatus(SolutionIterationStatus.SolutionIterationStatusEnum.Started);
    }

    internal static SolutionIteration Create(Guid id, SolutionId solutionId, int index)
    {
        return new(new SolutionIterationId(id), solutionId, index);
    }

    public SolutionId SolutionId { get; init; }
    public Solution Solution { get; private set; }

    public int Index { get; init; }

    public string? Result { get; private set; }

    public SolutionIterationStatus Status { get; private set; }

    internal void MoveToProcessing()
    {
        Status = new SolutionIterationStatus(SolutionIterationStatus.SolutionIterationStatusEnum.Processing);
    }

    internal void Finish(string result)
    {
        Result = result;
        Status = new SolutionIterationStatus(SolutionIterationStatus.SolutionIterationStatusEnum.Finished);
    }

#pragma warning disable CS0628 // New protected member declared in sealed type
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    protected SolutionIteration()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning restore CS0628 // New protected member declared in sealed type
    {
    }
}
