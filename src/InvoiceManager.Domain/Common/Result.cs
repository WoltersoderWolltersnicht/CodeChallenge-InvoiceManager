namespace InvoiceManager.Domain.Common;

public class Result<T>
{
    public T Value { get; set; }
    public string Error { get; set; }
    public bool IsSuccess { get; set; }

    public static explicit operator Result<T>(T value) => new Result<T>
    {
        Value = value,
        IsSuccess = true
    };

    public static explicit operator Result<T>(string error) => new Result<T>()
    {
        Error = error,
        IsSuccess = false
    };

}
