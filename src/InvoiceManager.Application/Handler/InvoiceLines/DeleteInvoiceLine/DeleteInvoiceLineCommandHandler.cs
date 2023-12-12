using MediatR;

namespace InvoiceManager.Application.Handler.InvoiceLines.DeleteInvoiceLine;

public class DeleteInvoiceLineCommandHandler : IRequestHandler<DeleteInvoiceLineCommand, DeleteInvoiceLineCommandResponse>
{
    public Task<DeleteInvoiceLineCommandResponse> Handle(DeleteInvoiceLineCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
