namespace SharedKernel.Common   
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public T? Value { get; }
        public string? Error { get; }

        protected Result(bool isSuccess, T? value, string? error)
        {
            if (isSuccess && error != null)
                throw new InvalidOperationException();
            if (!isSuccess && value != null)
                throw new InvalidOperationException();

            IsSuccess = isSuccess;
            Value = value;
            Error = error;
        }

        public static Result<T> Success(T value) => new Result<T>(true, value, null);
        public static Result<T> Failure(string error) => new Result<T>(false, default, error);
        public static implicit operator bool(Result<T>? result) => result is not null && result.IsSuccess;
    }

    public class Result
    {
        public bool IsSuccess { get; }
        public string? Error { get; }

        protected Result(bool isSuccess, string? error)
        {
            if (isSuccess && error != null)
                throw new InvalidOperationException();
            if (!isSuccess && error == null)
                throw new InvalidOperationException();

            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success() => new Result(true, null);
        public static Result Failure(string error) => new Result(false, error);
        public static implicit operator bool(Result? result) => result is not null && result.IsSuccess;
    }
}