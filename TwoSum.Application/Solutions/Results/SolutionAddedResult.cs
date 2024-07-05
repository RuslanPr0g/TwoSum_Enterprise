using Enterprise.Application.Results;

namespace TwoSum.Application.Solutions.Results;

public sealed class SolutionAddedResult : HttpResult
{
    private SolutionAddedResult(Guid solutionId) : base()
    {
        SolutionId = solutionId;
    }

    private SolutionAddedResult(string message) : base(message)
    {
    }

    public Guid? SolutionId { get; private set; }

    public static SolutionAddedResult CreateSuccess(Guid SolutionId)
    {
        return new SolutionAddedResult(SolutionId);
    }

    public static SolutionAddedResult CreateFail(string message)
    {
        return new SolutionAddedResult(message);
    }
}
