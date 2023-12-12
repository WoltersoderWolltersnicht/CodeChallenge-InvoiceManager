using MediatR;

namespace InvoiceManager.Application.Handler.Businesses.DeleteBusiness;

public record DeleteBusinessCommand(uint Id) : IRequest<DeleteBusinessCommandResponse>;