using InvoiceManager.Domain.Invoices;

namespace InvoiceManager.Domain.Businesses;

public class Business
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string CIF { get; set; }
    public List<Invoice> Invoices { get; set; }
}
