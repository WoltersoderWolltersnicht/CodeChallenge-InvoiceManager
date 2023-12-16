using InvoiceManager.Domain.Invoices;

namespace InvoiceManager.Application.Handler.Invoices.FilterInvoice;

public record FilterInvoiceQueryResponse(IEnumerable<Invoice> Invoices);
