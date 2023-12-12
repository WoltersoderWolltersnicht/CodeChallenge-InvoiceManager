using InvoiceManager.Domain.InvoiceLines;

namespace InvoiceManager.Application.Handler.InvoiceLines.CreateInvoiceLine;

public record CreateInvoiceLineCommandResponse(InvoiceLine InvoiceLine);
