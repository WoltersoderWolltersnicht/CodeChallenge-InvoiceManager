using InvoiceManager.Application.Handler.Invoices.GetInvoice;
using InvoiceManager.Domain.Common;
using InvoiceManager.Domain.Exceptions;
using InvoiceManager.Domain.Invoices;
using MediatR;

namespace InvoiceManager.Application.Handler.Invoices.FilterInvoice;

public class FilterInvoiceQueryHandler : IRequestHandler<FilterInvoiceQuery, Result<FilterInvoiceQueryResponse>>
{
    private readonly IInvoiceRepository _invoiceRepository;

    public FilterInvoiceQueryHandler(IInvoiceRepository invoiceRepository)
    {
        _invoiceRepository = invoiceRepository;
    }

    public async Task<Result<FilterInvoiceQueryResponse>> Handle(FilterInvoiceQuery request, CancellationToken cancellationToken)
    {
        var getResponse = await _invoiceRepository.GetByFilter(request.Ids, request.Numbers, request.GUIDs);
        if (!getResponse.IsSuccess) return getResponse.Error;
        return new FilterInvoiceQueryResponse(getResponse.Value);
    }
}
