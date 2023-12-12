using MediatR;

namespace InvoiceManager.Application.Handler.Invoices.GetInvoice;

public record GetInvoiceQuery(uint Id) : IRequest<GetInvoiceQueryResponse>;
