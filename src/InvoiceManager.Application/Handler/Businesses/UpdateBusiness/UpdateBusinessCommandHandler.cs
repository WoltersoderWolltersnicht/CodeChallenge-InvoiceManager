using InvoiceManager.Domain.Businesses;
using InvoiceManager.Domain.Common;
using InvoiceManager.Domain.Exceptions;
using MediatR;

namespace InvoiceManager.Application.Handler.Businesses.UpdateBusiness;

public class UpdateBusinessCommandHandler : IRequestHandler<UpdateBusinessCommand, Result<UpdateBusinessCommandResponse>>
{
    private readonly IBusinessRepository _businessRepository;

    public UpdateBusinessCommandHandler(IBusinessRepository businessRepository)
    {
        _businessRepository = businessRepository;
    }

    public async Task<Result<UpdateBusinessCommandResponse>> Handle(UpdateBusinessCommand request, CancellationToken cancellationToken)
    {
        var checkUniqueBusiness = await _businessRepository.Filter(b => b.Id != request.Id && b.Name == request.Name || b.CIF == request.CIF);
        if (checkUniqueBusiness.Value.Any()) return new DupplicateKeyException($"Business with same CIF or Name allready created with Id : {checkUniqueBusiness.Value.First().Id}");

        var businessToUpdate = await _businessRepository.GetById(request.Id);
        if (!businessToUpdate.IsSuccess) return businessToUpdate.Error;

        if(request.Name != null) businessToUpdate.Value.Name = request.Name;
        if (request.CIF != null) businessToUpdate.Value.CIF = request.CIF;

        var updateResponse = await _businessRepository.Update(businessToUpdate.Value);
        if (!updateResponse.IsSuccess) return updateResponse.Error;
        return new UpdateBusinessCommandResponse(updateResponse.Value);
    }
}
