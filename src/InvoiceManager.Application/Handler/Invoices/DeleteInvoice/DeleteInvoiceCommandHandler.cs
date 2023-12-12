using MediatR;

namespace InvoiceManager.Application.Handler.Invoices.DeleteInvoice;

public class DeleteInvoiceCommandHandler : IRequestHandler<DeleteInvoiceCommand, DeleteInvoiceCommandResponse>
{
    public Task<DeleteInvoiceCommandResponse> Handle(DeleteInvoiceCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
