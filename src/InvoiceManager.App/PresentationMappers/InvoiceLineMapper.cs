using InvoiceManager.App.PresentationDto.InvoiceLines;
using InvoiceManager.Domain.InvoiceLines;

namespace InvoiceManager.App.PresentationMappers;

public static class InvoiceLineMapper
{
    public static InvoiceLineRs Map(InvoiceLine invoiceLine)
    {
        return new InvoiceLineRs()
        {
            Id = invoiceLine.Id,
            Amount = invoiceLine.Amount,
            InvoiceId = invoiceLine?.Invoice?.Id,
            VAT = invoiceLine.VAT
        };
    }
}
