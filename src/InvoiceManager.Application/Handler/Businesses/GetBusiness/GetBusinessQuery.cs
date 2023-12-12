using MediatR;

namespace InvoiceManager.Application.Handler.Businesses.GetBusiness;

public record GetBusinessQuery(uint Id) : IRequest<GetBusinessQueryResponse>;
