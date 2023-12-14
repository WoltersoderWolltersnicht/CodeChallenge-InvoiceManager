using InvoiceManager.Domain.Common;
using InvoiceManager.Domain.Invoices;
using MediatR;

namespace InvoiceManager.Application.Handler.Invoices.CreateInvoice;

public record CreateInvoiceCommand(string Number, uint VAT, double Amount, InvoiceStatusEnum Status, List<InvoiceLineDto> InvoiceLines, uint BusinessId, PersonDto Person  ) : IRequest<Result<CreateInvoiceCommandResponse>>;

public record InvoiceLineDto(uint InvoiceLineVAT, double InvoiceLineAmount);

public record PersonDto(uint? Id, string? Name, string? Surname1, string? Surname2, string? NIF);