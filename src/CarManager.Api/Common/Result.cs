using CarManager.Core.Enums;

namespace CarManager.Api.Common
{
    public class Result<T>
    {
        public bool Success { get; init; }
        public ErrorCode Error { get; init; }
        public T? Data { get; init; }

        public static Result<T> Ok(T data)
            => new() { Success = true, Data = data };

        public static Result<T> Fail(ErrorCode error)
            => new() { Success = false, Error = error };
    }
}
