using InvoiceManager.Application.Handler.People.GetPerson;
using InvoiceManager.Domain.Common;
using InvoiceManager.Domain.People;
using MediatR;

namespace InvoiceManager.Application.Handler.People.UpdatePerson;

public class GetPersonQueryHandler : IRequestHandler<UpdatePersonCommand, Result<UpdatePersonCommandResponse>>
{
    private readonly IPersonRepository _personRepository;

    public GetPersonQueryHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<Result<UpdatePersonCommandResponse>> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
    {
        var updateResponse = await _personRepository.UpdatePerson(request.Person);
        if (!updateResponse.IsSuccess) return updateResponse.Error;
        return new UpdatePersonCommandResponse(updateResponse.Value);
    }
}
