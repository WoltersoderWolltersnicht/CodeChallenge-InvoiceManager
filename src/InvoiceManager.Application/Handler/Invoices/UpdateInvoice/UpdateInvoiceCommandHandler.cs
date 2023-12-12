using MediatR;

namespace InvoiceManager.Application.Handler.Invoices.UpdateInvoice;

public class GetInvoiceQueryHandler : IRequestHandler<UpdateInvoiceCommand, UpdateInvoiceCommandResponse>
{
    public Task<UpdateInvoiceCommandResponse> Handle(UpdateInvoiceCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
