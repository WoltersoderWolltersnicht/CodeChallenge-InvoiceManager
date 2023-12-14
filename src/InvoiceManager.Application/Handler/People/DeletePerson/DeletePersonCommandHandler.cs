using InvoiceManager.Application.Handler.People.CreatePerson;
using InvoiceManager.Domain.Common;
using InvoiceManager.Domain.People;
using MediatR;

namespace InvoiceManager.Application.Handler.People.DeletePerson;

public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand, Result<DeletePersonCommandResponse>>
{
    private readonly IPersonRepository _personRepository;

    public DeletePersonCommandHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<Result<DeletePersonCommandResponse>> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
    {
        var deleteResponse = await _personRepository.DeletePerson(request.Id);
        if (!deleteResponse.IsSuccess) return deleteResponse.Error;
        return new DeletePersonCommandResponse(deleteResponse.Value);
    }
}
