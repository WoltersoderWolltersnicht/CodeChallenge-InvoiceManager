using MediatR;

namespace InvoiceManager.Application.Handler.Businesses.GetBusiness;

public class GetBusinessQueryHandler : IRequestHandler<GetBusinessQuery, GetBusinessQueryResponse>
{
    public Task<GetBusinessQueryResponse> Handle(GetBusinessQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
