using MediatR;

namespace InvoiceManager.Application.Handler.Invoices.DeleteInvoice;

public record DeleteInvoiceCommand(uint Id) : IRequest<DeleteInvoiceCommandResponse>;