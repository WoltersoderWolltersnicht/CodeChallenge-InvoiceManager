using InvoiceManager.Domain.Businesses;
using InvoiceManager.Domain.InvoiceLines;
using InvoiceManager.Domain.People;

namespace InvoiceManager.Domain.Invoices;

public class Invoice
{
    public int Id { get; set; }
    public string GUID { get; set; }
    public string Number { get; set; }
    public double Amount { get; set; }
    public uint VAT { get; set; }
    public Business Business { get; set; }
    public Person Person { get; set; }
    public string Estado { get; set; }
    public List<InvoiceLine> InvoiceLines { get; set; }
}
