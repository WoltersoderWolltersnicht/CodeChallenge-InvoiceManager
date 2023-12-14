using InvoiceManager.Domain.Common;
using MediatR;

namespace InvoiceManager.Application.Handler.Invoices.GetInvoice;

public record GetInvoiceQuery(uint Id) : IRequest<Result<GetInvoiceQueryResponse>>;
