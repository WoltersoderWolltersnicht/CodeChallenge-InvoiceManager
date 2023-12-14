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
        var updateResponse = await _invoiceLineRepository.UpdateInvoiceLine(new InvoiceLine()
        {
            Id = request.Id,
            Amount = request.Amount,
            VAT = request.VAT
        });

        if (!updateResponse.IsSuccess) return updateResponse.Error;
        return new UpdateInvoiceLineCommandResponse(updateResponse.Value);
    }
}
