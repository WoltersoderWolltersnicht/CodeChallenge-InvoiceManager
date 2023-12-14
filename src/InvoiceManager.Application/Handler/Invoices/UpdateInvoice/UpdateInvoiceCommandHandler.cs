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
        var updateResponse = await _invoiceRepository.UpdateInvoice(new Invoice()
        {
            Id = request.Id,
            Estado = request.InvoiceStatus
        });

        if (!updateResponse.IsSuccess) return updateResponse.Error;
        return new UpdateInvoiceCommandResponse(updateResponse.Value);
    }
}
