using InvoiceManager.Application.Handler.Businesses.CreateBusiness;
using InvoiceManager.Domain.Common;
using InvoiceManager.Domain.People;
using MediatR;

namespace InvoiceManager.Application.Handler.People.CreatePerson;

public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, Result<CreatePersonCommandResponse>>
{
    private readonly IPersonRepository _personRepository;

    public CreatePersonCommandHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<Result<CreatePersonCommandResponse>> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        var createResponse = await _personRepository.CreatePerson(request.Person);
        if (!createResponse.IsSuccess) return createResponse.Error;
        return new CreatePersonCommandResponse(createResponse.Value);
    }
}
