using InvoiceManager.Domain.Common;
using MediatR;

namespace InvoiceManager.Application.Handler.InvoiceLines.CreateInvoiceLine;

public record CreateInvoiceLineCommand(uint InvoiceId, uint InvoiceLineVAT, double InvoiceLineAmount) : IRequest<Result<CreateInvoiceLineCommandResponse>>;