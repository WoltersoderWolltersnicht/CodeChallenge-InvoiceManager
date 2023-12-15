using InvoiceManager.Domain.Businesses;
using InvoiceManager.Domain.Common;
using MediatR;

namespace InvoiceManager.Application.Handler.Businesses.GetBusiness;

public class GetBusinessQueryHandler : IRequestHandler<GetBusinessQuery, Result<GetBusinessQueryResponse>>
{
    private readonly IBusinessRepository _businessRepository;

    public GetBusinessQueryHandler(IBusinessRepository businessRepository)
    {
        _businessRepository = businessRepository;
    }

    public async Task<Result<GetBusinessQueryResponse>> Handle(GetBusinessQuery request, CancellationToken cancellationToken)
    {
        var getResponse = await _businessRepository.GetById(request.Id);
        if (!getResponse.IsSuccess) return getResponse.Error;
        return new GetBusinessQueryResponse(getResponse.Value);
    }
}
