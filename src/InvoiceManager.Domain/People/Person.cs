using InvoiceManager.Domain.Common;
using InvoiceManager.Domain.Invoices;

namespace InvoiceManager.Domain.People;

public class Person : Entity
{
    public string Name { get; set; }
    public string Surname1 { get; set; }
    public string Surname2 { get; set; }
    public string NIF { get; set; }
    public List<Invoice> Invoices { get; set; }
}
