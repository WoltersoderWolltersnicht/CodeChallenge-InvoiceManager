using InvoiceManager.App.PresentationDto.InvoiceLines;
using InvoiceManager.Domain.Invoices;

namespace InvoiceManager.App.PresentationDto.Invoices;

public class InvoiceRs
{
    public uint Id { get; set; }
    public string GUID { get; set; }
    public string Number { get; set; }
    public double Amount { get; set; }
    public uint VAT { get; set; }
    public uint BusinessId { get; set; }
    public uint PersonId { get; set; }
    public InvoiceStatusEnum Estado { get; set; }
    public List<InvoiceLineRs> InvoiceLines { get; set; }
}
