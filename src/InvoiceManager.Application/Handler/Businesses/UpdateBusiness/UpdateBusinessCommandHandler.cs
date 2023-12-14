using InvoiceManager.Domain.Businesses;
using InvoiceManager.Domain.Common;
using MediatR;

namespace InvoiceManager.Application.Handler.Businesses.UpdateBusiness;

public class UpdateBusinessQueryHandler : IRequestHandler<UpdateBusinessCommand, Result<UpdateBusinessCommandResponse>>
{
    private readonly IBusinessRepository _businessRepository;

    public UpdateBusinessQueryHandler(IBusinessRepository businessRepository)
    {
        _businessRepository = businessRepository;
    }

    public async Task<Result<UpdateBusinessCommandResponse>> Handle(UpdateBusinessCommand request, CancellationToken cancellationToken)
    {
        var updateResponse = await _businessRepository.UpdateBusiness(new Business()
        {
            Id = request.Id,
            Name = request.Name,
            CIF = request.CIF,
        });

        if (!updateResponse.IsSuccess) return updateResponse.Error;
        return new UpdateBusinessCommandResponse(updateResponse.Value);
    }
}
