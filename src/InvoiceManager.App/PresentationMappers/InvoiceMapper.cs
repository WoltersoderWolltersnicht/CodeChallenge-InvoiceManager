using InvoiceManager.App.PresentationDto.Invoices;
using InvoiceManager.Domain.Invoices;

namespace InvoiceManager.App.PresentationMappers;

public static class InvoiceMapper
{
    public static InvoiceRs Map(Invoice invoice)
    {
        return new InvoiceRs()
        {
            Id = invoice.Id,
            Amount = invoice.Amount,
            BusinessId = invoice?.Business?.Id,
            Estado = invoice.Estado,
            GUID = invoice.GUID,
            InvoiceLines = invoice.InvoiceLines?.Select(InvoiceLineMapper.Map)?.ToList(),
            Number = invoice.Number,
            PersonId = invoice?.Person?.Id,
            VAT = invoice.VAT,
        };
    }
}
