using InvoiceManager.Domain.InvoiceLines;
using MediatR;

namespace InvoiceManager.Application.Handler.InvoiceLines.UpdateInvoiceLine;

public record UpdateBusinessCommand(InvoiceLine InvoiceLine) : IRequest<UpdateInvoiceLineCommandResponse>;
