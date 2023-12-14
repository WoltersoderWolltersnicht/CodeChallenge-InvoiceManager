namespace InvoiceManager.Domain.Exceptions;

public class DatabaseException : Exception
{
    public DatabaseException(string error) : base(error) { }
}
