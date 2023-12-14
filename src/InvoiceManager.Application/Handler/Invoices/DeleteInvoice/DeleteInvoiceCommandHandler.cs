using InvoiceManager.Application.Handler.InvoiceLines.DeleteInvoiceLine;
using InvoiceManager.Domain.Common;
using InvoiceManager.Domain.InvoiceLines;
using InvoiceManager.Domain.Invoices;
using MediatR;

namespace InvoiceManager.Application.Handler.Invoices.DeleteInvoice;

public class DeleteInvoiceCommandHandler : IRequestHandler<DeleteInvoiceCommand, Result<DeleteInvoiceCommandResponse>>
{
    private readonly IInvoiceRepository _invoiceRepository;

    public DeleteInvoiceCommandHandler(IInvoiceRepository invoiceRepository)
    {
        _invoiceRepository = invoiceRepository;
    }

    public async Task<Result<DeleteInvoiceCommandResponse>> Handle(DeleteInvoiceCommand request, CancellationToken cancellationToken)
    {
        var deleteResponse = await _invoiceRepository.DeleteInvoice(request.Id);
        if (!deleteResponse.IsSuccess) return deleteResponse.Error;
        return new DeleteInvoiceCommandResponse(deleteResponse.Value);
    }
}
