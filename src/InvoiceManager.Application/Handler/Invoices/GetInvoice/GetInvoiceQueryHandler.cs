using InvoiceManager.Domain.Common;
using InvoiceManager.Domain.Invoices;
using MediatR;

namespace InvoiceManager.Application.Handler.Invoices.GetInvoice;

public class GetInvoiceQueryHandler : IRequestHandler<GetInvoiceQuery, Result<GetInvoiceQueryResponse>>
{
    private readonly IInvoiceRepository _invoiceRepository;

    public GetInvoiceQueryHandler(IInvoiceRepository invoiceRepository)
    {
        _invoiceRepository = invoiceRepository;
    }
    public async Task<Result<GetInvoiceQueryResponse>> Handle(GetInvoiceQuery request, CancellationToken cancellationToken)
    {
        var getResponse = await _invoiceRepository.GetInvoiceById(request.Id);
        if (!getResponse.IsSuccess) return getResponse.Error;
        return new GetInvoiceQueryResponse(getResponse.Value);
    }
}
