using InvoiceManager.Domain.Common;
using InvoiceManager.Domain.InvoiceLines;
using MediatR;

namespace InvoiceManager.Application.Handler.InvoiceLines.UpdateInvoiceLine;

public class UpdateInvoiceLineCommandHandler : IRequestHandler<UpdateInvoiceLineCommand, Result<UpdateInvoiceLineCommandResponse>>
{
    private readonly IInvoiceLineRepository _invoiceLineRepository;

    public UpdateInvoiceLineCommandHandler(IInvoiceLineRepository invoiceLineRepository)
    {
        _invoiceLineRepository = invoiceLineRepository;
    }
    public async Task<Result<UpdateInvoiceLineCommandResponse>> Handle(UpdateInvoiceLineCommand request, CancellationToken cancellationToken)
    {
        var getInvoiceLineResult = await _invoiceLineRepository.GetById(request.Id);
        if (!getInvoiceLineResult.IsSuccess) return getInvoiceLineResult.Error;

        var invoiceLineToUpdate = getInvoiceLineResult.Value;

        if (request.Amount != null)
        {
            //Update Invoice Amount Difference
            double amountDifference = request.Amount.Value - invoiceLineToUpdate.Amount.Value;
            invoiceLineToUpdate.Invoice.Amount += amountDifference;
            invoiceLineToUpdate.Amount = request.Amount;
        }

        if (request.VAT != null) invoiceLineToUpdate.VAT = request.VAT;

        var updateResponse = await _invoiceLineRepository.Update(invoiceLineToUpdate);

        if (!updateResponse.IsSuccess) return updateResponse.Error;
        return new UpdateInvoiceLineCommandResponse(updateResponse.Value);
    }
}
