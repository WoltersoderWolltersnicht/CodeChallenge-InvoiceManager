using InvoiceManager.Domain.Invoices;
using MediatR;

namespace InvoiceManager.Application.Handler.Invoices.CreateInvoice;

public record CreateInvoiceCommand(Invoice Invoice) : IRequest<CreateInvoiceCommandResponse>;