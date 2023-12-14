using InvoiceManager.Domain.Common;
using MediatR;

namespace InvoiceManager.Application.Handler.InvoiceLines.GetInvoiceLine;

public record GetInvoiceLineQuery(uint Id) : IRequest<Result<GetInvoiceLineQueryResponse>>;
