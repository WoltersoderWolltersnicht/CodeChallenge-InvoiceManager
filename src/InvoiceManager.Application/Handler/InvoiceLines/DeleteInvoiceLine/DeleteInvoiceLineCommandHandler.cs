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
        var getInvoiceLineResponse = await _invoiceLineRepository.GetById(request.Id);
        if (!getInvoiceLineResponse.IsSuccess) return getInvoiceLineResponse.Error;

        var invoiceLineToDelete = getInvoiceLineResponse.Value;
        invoiceLineToDelete.Invoice.Amount -= invoiceLineToDelete.Amount.Value;

        var deleteResponse = await _invoiceLineRepository.Delete(getInvoiceLineResponse.Value);
        if (!deleteResponse.IsSuccess) return deleteResponse.Error;
        return new DeleteInvoiceLineCommandResponse(deleteResponse.Value);
    }
}
