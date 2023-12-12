using MediatR;

namespace InvoiceManager.Application.Handler.Invoices.CreateInvoice;

public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, CreateInvoiceCommandResponse>
{
    public Task<CreateInvoiceCommandResponse> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
