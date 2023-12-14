using InvoiceManager.Domain.Common;
using MediatR;

namespace InvoiceManager.Application.Handler.Businesses.DeleteBusiness;

public record DeleteBusinessCommand(uint Id) : IRequest<Result<DeleteBusinessCommandResponse>>;