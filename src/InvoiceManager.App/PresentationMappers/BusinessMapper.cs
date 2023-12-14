using InvoiceManager.App.PresentationDto.Businesses;
using InvoiceManager.App.PresentationDto.InvoiceLines;
using InvoiceManager.App.PresentationDto.Invoices;
using InvoiceManager.Domain.Businesses;

namespace InvoiceManager.App.PresentationMappers;

public static class BusinessMapper
{
    public static BusinessRs Map(Business business)
    {
        return new BusinessRs()
        {
            Id = business.Id,
            CIF = business.CIF,
            Name = business.Name,
            Invoices = business.Invoices.Select(InvoiceMapper.Map).ToList(),
        };
    }

}
