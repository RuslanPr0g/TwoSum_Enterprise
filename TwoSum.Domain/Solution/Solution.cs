using Enterprise.Domain;
using TwoSum.Domain.Events;

namespace TwoSum.Domain.Solution;

// TODO: ADD UNIT TESTS
public sealed class Solution : AggregateRoot<SolutionId>
{
    private Solution(SolutionId id, int[] nums, int target)
        : base(id)
    {
        Nums = nums;
        Target = target;
        Status = new SolutionStatus(SolutionStatus.SolutionStatusEnum.Started);
        Iterations = [];

        PublishEvent(new SolutionCreatedDomainEvent(id));
    }

    public static Solution Create(Guid id, int[] nums, int target)
    {
        var entityId = new SolutionId(id);
        return new(entityId, nums, target);
    }

    public int[] Nums { get; init; }

    public int Target { get; init; }

    public SolutionStatus Status { get; private set; }

    public List<SolutionIteration> Iterations { get; private set; }

    public bool IsSolved()
    {
        return Status.Value is SolutionStatus.SolutionStatusEnum.Solved;
    }

    public void MoveNext()
    {
        if (IsSolved())
        {
            return;
        }

        if (Nums.Length <= 0)
        {
            Solve();
            return;
        }

        if (Status.Value is not SolutionStatus.SolutionStatusEnum.InProgress)
        {
            MoveToInProgress();
        }

        int? lastIterationIndex = null;

        if (Iterations.Count > 0)
        {
            var iterations = Iterations.Select(x => x.Index).ToList();
            iterations.Sort();
            lastIterationIndex = iterations.LastOrDefault();
        }

        int currentIndex = !lastIterationIndex.HasValue ? 0 : lastIterationIndex.Value + 1;

        SolutionIteration newIteration = SolutionIteration.Create(Guid.NewGuid(), Id, currentIndex);
        newIteration.MoveToProcessing();

        for (int i = 0; i < Nums.Length; i++)
        {
            if (i == currentIndex) continue;

            var sum = Nums[i] + Nums[currentIndex];
            if (sum == Target)
            {
                newIteration.Finish($"{i},{currentIndex}");
            }
        }

        Iterations.Add(newIteration);

        if (currentIndex == Nums.Length - 1 ||
            newIteration.Status.Value == SolutionIterationStatus.SolutionIterationStatusEnum.Finished)
        {
            Solve();
        }
        else
        {
            PublishEvent(new NextSolutionIterationRequested(Id));
        }
    }

    public string RetrieveSolutionAsString()
    {
        var (I, J, Message, IsSuccess) = RetrieveSolution();

        if (IsSuccess)
        {
            return $"I: {I}, J: {J}, nums: {string.Join(',', Nums)}";
        }

        return Message ?? "Solution cannot be computed due to a technical issue, please contact our support...";
    }

    public (int I, int J, string? Message, bool IsSuccess) RetrieveSolution()
    {
        if (Iterations.Any(x => x.Status.Value == SolutionIterationStatus.SolutionIterationStatusEnum.Finished))
        {
            var iteration = Iterations.FirstOrDefault(x => x.Result is not null);

            if (iteration?.Result is null)
            {
                return (-1, -1, "No solution found.", false);
            }

            var indecies = iteration.Result.Split(',');
            var trI = int.TryParse(indecies[0], out var I);
            var trJ = int.TryParse(indecies[1], out var J);

            if (!trI || !trJ)
            {
                return (-1, -1, "No solution found.", false);
            }

            return (I, J, "Enjoy!", true);
        }
        else
        {
            return (-1, -1, "No solution yet.", false);
        }
    }

    private void MoveToInProgress()
    {
        Status = new SolutionStatus(SolutionStatus.SolutionStatusEnum.InProgress);
    }

    private void Solve()
    {
        Status = new SolutionStatus(SolutionStatus.SolutionStatusEnum.Solved);
    }

#pragma warning disable CS0628 // New protected member declared in sealed type
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    protected Solution()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning restore CS0628 // New protected member declared in sealed type
    {
        Iterations = [];
    }
}
