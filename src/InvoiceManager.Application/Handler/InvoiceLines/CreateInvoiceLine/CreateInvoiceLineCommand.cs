using InvoiceManager.Domain.InvoiceLines;
using MediatR;

namespace InvoiceManager.Application.Handler.InvoiceLines.CreateInvoiceLine;

public record CreateInvoiceLineCommand(uint InvoiceId, InvoiceLine Invoice) : IRequest<CreateInvoiceLineCommandResponse>;