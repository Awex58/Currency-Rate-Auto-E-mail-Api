namespace WebApi.Core.Entities.Result
{
    public class Result:IResult
    {
        public Result(bool success, string message):this(success)
        {
            Message = message;
                //Success = success;  yerine this(success) yazdık
        }

        public Result(bool success)
        {
            Success = success;
        }
        public bool Success { get; }
        public string Message { get; }
    }
}
