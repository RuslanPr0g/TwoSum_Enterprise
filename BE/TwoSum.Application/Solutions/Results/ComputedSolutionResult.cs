using Enterprise.Application.Results;

namespace TwoSum.Application.Solutions.Results;

public sealed class ComputedSolutionResult : HttpResult
{
    private ComputedSolutionResult(string message) : base()
    {
        Message = message;
    }

    private ComputedSolutionResult(string message, bool fail = true) : base(message)
    {
        if (fail)
        {
            Message = string.Empty;
        }

        Message = string.Empty;
    }

    public string Message { get; private set; }

    public static ComputedSolutionResult CreateSuccess(string message)
    {
        return new ComputedSolutionResult(message);
    }

    public static ComputedSolutionResult CreateFail(string message)
    {
        return new ComputedSolutionResult(message, true);
    }
}
