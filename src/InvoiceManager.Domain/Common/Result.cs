namespace InvoiceManager.Domain.Common;

public class Result<T>
{
    public T Value { get; set; }
    public Exception Error { get; set; }
    public bool IsSuccess { get; set; }

    public static implicit operator Result<T>(T value) => new Result<T>
    {
        Value = value,
        IsSuccess = true
    };

    public static implicit operator Result<T>(Exception error) => new Result<T>()
    {
        Error = error,
        IsSuccess = false
    };

}
