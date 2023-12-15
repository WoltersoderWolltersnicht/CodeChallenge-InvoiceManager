using InvoiceManager.Domain.Common;
using InvoiceManager.Domain.Invoices;
using MediatR;

namespace InvoiceManager.Application.Handler.Invoices.UpdateInvoice;

public record UpdateInvoiceCommand(uint Id, InvoiceStatusEnum? InvoiceStatus) : IRequest<Result<UpdateInvoiceCommandResponse>>;
