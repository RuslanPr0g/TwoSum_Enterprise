namespace Enterprise.Application.Results;

public interface IHttpResult
{
    bool IsSuccess { get; }
    string? ErrorMessage { get; }
}

public class HttpResult : IHttpResult
{
    protected HttpResult(string failMessage)
    {
        ErrorMessage = failMessage;
        IsSuccess = false;
    }

    protected HttpResult()
    {
        IsSuccess = true;
    }

    public bool IsSuccess { get; private set; }

    public string? ErrorMessage { get; private set; }
}