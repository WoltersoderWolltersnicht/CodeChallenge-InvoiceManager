using InvoiceManager.App.PresentationDto.Invoices;

namespace InvoiceManager.App.PresentationDto.Businesses;

public class BusinessRs
{
    public uint Id { get; set; }
    public string Name { get; set; }
    public string CIF { get; set; }
    public List<InvoiceRs> Invoices { get; set; }
}
