using MediatR;

namespace InvoiceManager.Application.Handler.InvoiceLines.DeleteInvoiceLine;

public record DeleteInvoiceLineCommand(uint Id) : IRequest<DeleteInvoiceLineCommandResponse>;