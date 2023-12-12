using InvoiceManager.Domain.Invoices;
using MediatR;

namespace InvoiceManager.Application.Handler.Invoices.UpdateInvoice;

public record UpdateInvoiceCommand(Invoice Invoice) : IRequest<UpdateInvoiceCommandResponse>;
