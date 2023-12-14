using InvoiceManager.Domain.Common;
using MediatR;

namespace InvoiceManager.Application.Handler.InvoiceLines.UpdateInvoiceLine;

public record UpdateInvoiceLineCommand(uint Id, uint? VAT, double? Amount) : IRequest<Result<UpdateInvoiceLineCommandResponse>>;
