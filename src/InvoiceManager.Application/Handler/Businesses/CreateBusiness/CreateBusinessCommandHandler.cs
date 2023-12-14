using InvoiceManager.Domain.Businesses;
using InvoiceManager.Domain.Common;
using MediatR;

namespace InvoiceManager.Application.Handler.Businesses.CreateBusiness;

public class CreateBusinessCommandHandler : IRequestHandler<CreateBusinessCommand, Result<CreateBusinessCommandResponse>>
{
    private readonly IBusinessRepository _businessRepository;

    public CreateBusinessCommandHandler(IBusinessRepository businessRepository)
    {
        _businessRepository = businessRepository;
    }

    public async Task<Result<CreateBusinessCommandResponse>> Handle(CreateBusinessCommand request, CancellationToken cancellationToken)
    {
        var business = new Business()
        {
            Name = request.Name,
            CIF = request.CIF,
            Invoices = new()
        };

        var createResponse = await _businessRepository.CreateBusiness(business);
        if (!createResponse.IsSuccess) return createResponse.Error;
        return new CreateBusinessCommandResponse(createResponse.Value);
    }
}
