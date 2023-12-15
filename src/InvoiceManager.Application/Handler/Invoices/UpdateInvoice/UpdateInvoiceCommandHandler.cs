using InvoiceManager.Domain.Common;
using InvoiceManager.Domain.Invoices;
using MediatR;

namespace InvoiceManager.Application.Handler.Invoices.UpdateInvoice;

public class GetInvoiceQueryHandler : IRequestHandler<UpdateInvoiceCommand, Result<UpdateInvoiceCommandResponse>>
{
    private readonly IInvoiceRepository _invoiceRepository;

    public GetInvoiceQueryHandler(IInvoiceRepository invoiceRepository)
    {
        _invoiceRepository = invoiceRepository;
    }

    public async Task<Result<UpdateInvoiceCommandResponse>> Handle(UpdateInvoiceCommand request, CancellationToken cancellationToken)
    {
        var getInvoiceResult = await _invoiceRepository.GetById(request.Id);
        if (!getInvoiceResult.IsSuccess) return getInvoiceResult.Error;

        var invoiceToUpdate = getInvoiceResult.Value;

        if(request.InvoiceStatus != null) invoiceToUpdate.Estado = request.InvoiceStatus.Value;
        
        var updateResponse = await _invoiceRepository.Update(invoiceToUpdate);
        if (!updateResponse.IsSuccess) return updateResponse.Error;
        return new UpdateInvoiceCommandResponse(updateResponse.Value);
    }
}
