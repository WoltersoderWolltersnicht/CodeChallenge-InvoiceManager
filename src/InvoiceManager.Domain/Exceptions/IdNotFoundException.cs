namespace InvoiceManager.Domain.Exceptions;

public class IdNotFoundException : Exception 
{
    public IdNotFoundException(uint id) : base($"Element with Id:{id} not found") { }
}
