using InvoiceManager.Domain.Invoices;

namespace InvoiceManager.App.PresentationDto.InvoiceLines;

public class InvoiceLineRs
{
    public uint Id { get; set; }
    public uint? VAT { get; set; }
    public double? Amount { get; set; }
    public uint InvoiceId { get; set; }
}
