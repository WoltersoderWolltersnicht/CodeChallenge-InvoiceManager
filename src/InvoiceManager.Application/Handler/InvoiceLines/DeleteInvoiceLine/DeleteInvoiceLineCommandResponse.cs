using InvoiceManager.Domain.InvoiceLines;

namespace InvoiceManager.Application.Handler.InvoiceLines.DeleteInvoiceLine;

public record DeleteInvoiceLineCommandResponse(InvoiceLine InvoiceLine);
