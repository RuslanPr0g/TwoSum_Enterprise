namespace Enterprise.Application.Requests;

public sealed record AddSolutionRequest : IHttpRequest
{
    public int[]? Nums { get; set; }
    public int Target { get; set; }
}
