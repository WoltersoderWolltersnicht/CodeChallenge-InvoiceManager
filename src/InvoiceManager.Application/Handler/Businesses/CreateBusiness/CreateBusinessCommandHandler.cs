using InvoiceManager.Domain.Businesses;
using InvoiceManager.Domain.Common;
using InvoiceManager.Domain.Exceptions;
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
        var checkUniqueBusiness = await _businessRepository.Filter(b => b.CIF == request.CIF || b.Name == request.Name);
        if (checkUniqueBusiness.Value.Any()) 
            return new DupplicateKeyException($"Business with same CIF or Name allready created with Id : {checkUniqueBusiness.Value.First().Id}");

        var business = new Business()
        {
            Name = request.Name,
            CIF = request.CIF,
            Invoices = new()
        };

        var createResponse = await _businessRepository.Create(business);
        if (!createResponse.IsSuccess) return createResponse.Error;
        return new CreateBusinessCommandResponse(createResponse.Value);
    }
}
