using InvoiceManager.Application.Handler.Invoices.GetInvoice;
using InvoiceManager.Domain.Common;
using MediatR;

namespace InvoiceManager.Application.Handler.Invoices.FilterInvoice;

public record FilterInvoiceQuery(IEnumerable<uint>? Ids, IEnumerable<string>? Numbers, IEnumerable<string>? GUIDs) : IRequest<Result<FilterInvoiceQueryResponse>>;
