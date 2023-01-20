namespace WebApi.Core.Entities.Result
{
    public interface IResult
    {
        bool Success { get; }
        string Message { get; }
    }
}
