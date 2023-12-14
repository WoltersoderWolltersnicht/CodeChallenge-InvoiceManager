namespace InvoiceManager.Domain.Exceptions;

public class DupplicateKeyException : Exception
{
    public DupplicateKeyException(string  message) : base(message) { }  
}
