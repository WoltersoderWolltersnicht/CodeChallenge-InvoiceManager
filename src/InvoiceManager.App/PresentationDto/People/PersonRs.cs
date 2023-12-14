using InvoiceManager.App.PresentationDto.Invoices;

namespace InvoiceManager.App.PresentationDto.People;

public class PersonRs
{
    public uint Id { get; set; }
    public string Name { get; set; }
    public string Surname1 { get; set; }
    public string Surname2 { get; set; }
    public string NIF { get; set; }
    public List<InvoiceRs> Invoices { get; set; }
}
