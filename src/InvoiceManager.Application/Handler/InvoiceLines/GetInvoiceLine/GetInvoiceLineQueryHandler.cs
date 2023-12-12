using MediatR;

namespace InvoiceManager.Application.Handler.InvoiceLines.GetInvoiceLine;

public class GetInvoiceLineQueryHandler : IRequestHandler<GetInvoiceLineQuery, GetInvoiceLineQueryResponse>
{
    public Task<GetInvoiceLineQueryResponse> Handle(GetInvoiceLineQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
