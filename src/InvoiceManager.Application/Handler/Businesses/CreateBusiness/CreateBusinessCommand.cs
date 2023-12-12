using InvoiceManager.Domain.Businesses;
using InvoiceManager.Domain.Invoices;
using MediatR;

namespace InvoiceManager.Application.Handler.Businesses.CreateBusiness;

public record CreateBusinessCommand(Business Business) : IRequest<CreateBusinessCommandResponse>;