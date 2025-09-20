namespace Project.Models.Core;

public class Result
{
    public bool Success { get; }
    public string? Error { get; }

    protected Result(bool success, string? error)
    {
        this.Success = success;
        this.Error = error;
    }

    public static Result Ok() => new(true, null);
    public static Result Fail(string error) => new(false, error);

    public static Result<T> Ok<T>(T value) => new(true, value, null);
    public static Result<T> Fail<T>(string error) => new(false, default, error);
}

public class Result<T> : Result
{
    public T? Value { get; }

    internal Result(bool success, T? value, string? error)
        : base(success, error)
    {
        this.Value = value;
    }

    public static implicit operator Result<T>(T value) => Ok(value);

    public new Result<T> Fail(string error) => new(false, default, error);
}
