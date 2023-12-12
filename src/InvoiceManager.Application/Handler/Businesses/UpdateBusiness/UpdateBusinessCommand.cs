using InvoiceManager.Domain.Businesses;
using MediatR;

namespace InvoiceManager.Application.Handler.Businesses.UpdateBusiness;

public record UpdateBusinessCommand(Business Id) : IRequest<UpdateBusinessCommandResponse>;
