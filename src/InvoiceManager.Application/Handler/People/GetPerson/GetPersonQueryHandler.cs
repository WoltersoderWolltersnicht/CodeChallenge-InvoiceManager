using InvoiceManager.Domain.Common;
using InvoiceManager.Domain.People;
using MediatR;

namespace InvoiceManager.Application.Handler.People.GetPerson;

public class GetPersonQueryHandler : IRequestHandler<GetPersonQuery, Result<GetPersonQueryResponse>>
{
    private readonly IPersonRepository _personRepository;

    public GetPersonQueryHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<Result<GetPersonQueryResponse>> Handle(GetPersonQuery request, CancellationToken cancellationToken)
    {
        var getResponse = await _personRepository.GetById(request.Id);
        if (!getResponse.IsSuccess) return getResponse.Error;
        return new GetPersonQueryResponse(getResponse.Value);
    }
}
