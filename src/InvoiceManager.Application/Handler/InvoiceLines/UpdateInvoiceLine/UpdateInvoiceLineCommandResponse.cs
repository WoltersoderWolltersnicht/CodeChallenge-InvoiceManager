using InvoiceManager.Domain.InvoiceLines;

namespace InvoiceManager.Application.Handler.InvoiceLines.UpdateInvoiceLine;

public record UpdateInvoiceLineCommandResponse(InvoiceLine InvoiceLine);