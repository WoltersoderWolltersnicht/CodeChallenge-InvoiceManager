using MediatR;

namespace InvoiceManager.Application.Handler.InvoiceLines.UpdateInvoiceLine;

public class GetInvoiceLineQueryHandler : IRequestHandler<UpdateBusinessCommand, UpdateInvoiceLineCommandResponse>
{
    public Task<UpdateInvoiceLineCommandResponse> Handle(UpdateBusinessCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
