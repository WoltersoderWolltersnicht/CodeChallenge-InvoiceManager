using InvoiceManager.Domain.Common;
using InvoiceManager.Domain.InvoiceLines;
using InvoiceManager.Domain.Invoices;
using MediatR;

namespace InvoiceManager.Application.Handler.InvoiceLines.CreateInvoiceLine;

public class CreateInvoiceLineCommandHandler : IRequestHandler<CreateInvoiceLineCommand, Result<CreateInvoiceLineCommandResponse>>
{
    private readonly IInvoiceLineRepository _invoiceLineRepository;
    private readonly IInvoiceRepository _invoiceRepository;

    public CreateInvoiceLineCommandHandler(IInvoiceLineRepository invoiceLineRepository, IInvoiceRepository invoiceRepository)
    {
        _invoiceLineRepository = invoiceLineRepository;
        _invoiceRepository = invoiceRepository;
    }

    public async Task<Result<CreateInvoiceLineCommandResponse>> Handle(CreateInvoiceLineCommand request, CancellationToken cancellationToken)
    {
        var getInvoiceResponse = await _invoiceRepository.GetInvoiceById(request.InvoiceId);
        if (!getInvoiceResponse.IsSuccess) return getInvoiceResponse.Error;
            
        var createInvoiceLineResponse = await _invoiceLineRepository.CreateInvoiceLine(new InvoiceLine()
        {
            Amount = request.InvoiceLineAmount,
            Invoice = getInvoiceResponse.Value,
            VAT = request.InvoiceLineVAT
        });

        if (!createInvoiceLineResponse.IsSuccess) return createInvoiceLineResponse.Error;
        return new CreateInvoiceLineCommandResponse(createInvoiceLineResponse.Value);
    }
}
