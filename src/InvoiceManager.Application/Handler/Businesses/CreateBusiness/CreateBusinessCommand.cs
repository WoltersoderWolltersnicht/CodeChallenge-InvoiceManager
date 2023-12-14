using InvoiceManager.Domain.Common;
using MediatR;

namespace InvoiceManager.Application.Handler.Businesses.CreateBusiness;

public record CreateBusinessCommand(string Name, string CIF) : IRequest<Result<CreateBusinessCommandResponse>>;