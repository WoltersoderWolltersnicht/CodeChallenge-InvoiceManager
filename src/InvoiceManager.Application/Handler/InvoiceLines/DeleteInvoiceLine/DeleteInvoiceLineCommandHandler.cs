using InvoiceManager.Domain.Common;
using InvoiceManager.Domain.InvoiceLines;
using MediatR;

namespace InvoiceManager.Application.Handler.InvoiceLines.DeleteInvoiceLine;

public class DeleteInvoiceLineCommandHandler : IRequestHandler<DeleteInvoiceLineCommand, Result<DeleteInvoiceLineCommandResponse>>
{
    private readonly IInvoiceLineRepository _invoiceLineRepository;

    public DeleteInvoiceLineCommandHandler(IInvoiceLineRepository invoiceLineRepository)
    {
        _invoiceLineRepository = invoiceLineRepository;
    }

    public async Task<Result<DeleteInvoiceLineCommandResponse>> Handle(DeleteInvoiceLineCommand request, CancellationToken cancellationToken)
    {
        var deleteResponse = await _invoiceLineRepository.DeleteInvoiceLine(request.Id);
        if (!deleteResponse.IsSuccess) return deleteResponse.Error;
        return new DeleteInvoiceLineCommandResponse(deleteResponse.Value);
    }
}
