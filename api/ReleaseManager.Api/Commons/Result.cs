namespace ReleaseManager.Api.Commons;

public class Result
{
    public string? Error { get; protected set; }

    public bool Success => string.IsNullOrEmpty(Error);
    public bool Failure => !Success;

    protected Result() { }

    public static Result Ok()
    {
        return new Result();
    }

    public static Result Fail(string error)
    {
        return new Result { Error = error };
    }

    public bool IsError(string error)
    {
        return Failure && Error == error;
    }
}

public class Result<T> : Result
{
    public T? Item { get; private set; }

    public static Result<T> Ok(T item)
    {
        return new Result<T> { Item = item };
    }

    public static new Result<T> Fail(string error)
    {
        return new Result<T> { Error = error };
    }
}