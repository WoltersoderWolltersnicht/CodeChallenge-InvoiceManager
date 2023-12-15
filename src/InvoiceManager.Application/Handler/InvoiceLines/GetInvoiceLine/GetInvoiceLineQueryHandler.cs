using InvoiceManager.Domain.Common;
using InvoiceManager.Domain.InvoiceLines;
using MediatR;

namespace InvoiceManager.Application.Handler.InvoiceLines.GetInvoiceLine;

public class GetInvoiceLineQueryHandler : IRequestHandler<GetInvoiceLineQuery, Result<GetInvoiceLineQueryResponse>>
{
    private readonly IInvoiceLineRepository _invoiceLineRepository;

    public GetInvoiceLineQueryHandler(IInvoiceLineRepository invoiceLineRepository)
    {
        _invoiceLineRepository = invoiceLineRepository;
    }

    public async Task<Result<GetInvoiceLineQueryResponse>> Handle(GetInvoiceLineQuery request, CancellationToken cancellationToken)
    {
        var getResponse = await _invoiceLineRepository.GetById(request.Id);
        if (!getResponse.IsSuccess) return getResponse.Error;
        return new GetInvoiceLineQueryResponse(getResponse.Value);
    }
}
