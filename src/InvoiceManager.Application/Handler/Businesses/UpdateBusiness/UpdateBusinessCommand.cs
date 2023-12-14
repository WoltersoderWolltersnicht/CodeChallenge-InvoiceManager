using InvoiceManager.Domain.Common;
using MediatR;

namespace InvoiceManager.Application.Handler.Businesses.UpdateBusiness;

public record UpdateBusinessCommand(uint Id, string Name, string CIF) : IRequest<Result<UpdateBusinessCommandResponse>>;
