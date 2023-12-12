using InvoiceManager.Domain.Invoices;

namespace InvoiceManager.Domain.InvoiceLines;

public class InvoiceLine
{
    public uint Id { get; set; }
    public uint? VAT { get; set; }
    public double? Amount { get; set; }
    public Invoice Invoice { get; set; }
}
