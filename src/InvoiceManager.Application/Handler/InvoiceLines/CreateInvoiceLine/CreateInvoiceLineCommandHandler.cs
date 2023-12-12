using MediatR;

namespace InvoiceManager.Application.Handler.InvoiceLines.CreateInvoiceLine;

public class CreateInvoiceLineCommandHandler : IRequestHandler<CreateInvoiceLineCommand, CreateInvoiceLineCommandResponse>
{
    public Task<CreateInvoiceLineCommandResponse> Handle(CreateInvoiceLineCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
