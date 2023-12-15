using InvoiceManager.Domain.Businesses;
using InvoiceManager.Domain.Common;
using MediatR;

namespace InvoiceManager.Application.Handler.Businesses.DeleteBusiness;

public class DeleteBusinessCommandHandler : IRequestHandler<DeleteBusinessCommand, Result<DeleteBusinessCommandResponse>>
{
    private readonly IBusinessRepository _businessRepository;

    public DeleteBusinessCommandHandler(IBusinessRepository businessRepository)
    {
        _businessRepository = businessRepository;
    }

    public async Task<Result<DeleteBusinessCommandResponse>> Handle(DeleteBusinessCommand request, CancellationToken cancellationToken)
    {
        var businessToDeleteResult = await _businessRepository.GetById(request.Id);
        if (!businessToDeleteResult.IsSuccess) return businessToDeleteResult.Error;

        var deleteResponse = await _businessRepository.Delete(businessToDeleteResult.Value);
        if (!deleteResponse.IsSuccess) return deleteResponse.Error;
        return new DeleteBusinessCommandResponse(businessToDeleteResult.Value);
    }
}
