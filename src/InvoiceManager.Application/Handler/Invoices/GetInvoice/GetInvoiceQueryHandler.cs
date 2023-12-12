using MediatR;

namespace InvoiceManager.Application.Handler.Invoices.GetInvoice;

public class GetInvoiceQueryHandler : IRequestHandler<GetInvoiceQuery, GetInvoiceQueryResponse>
{
    public Task<GetInvoiceQueryResponse> Handle(GetInvoiceQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
