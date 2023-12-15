using InvoiceManager.Domain.Common;
using InvoiceManager.Domain.Exceptions;
using InvoiceManager.Domain.Invoices;
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
        var checkUniquePerson = await _personRepository.Filter(p => p.NIF == request.NIF);
        if (checkUniquePerson.Value.Any()) return new DupplicateKeyException($"Person with same NIF allready created with Id : {checkUniquePerson.Value.First().Id}");

        var personToCreate = new Person()
        {
            Name = request.Name,
            NIF = request.NIF,
            Surname1 = request.Surname1,
            Surname2 = request.Surname2,
            Invoices = new List<Invoice>()
        };

        var createResponse = await _personRepository.Create(personToCreate);
        if (!createResponse.IsSuccess) return createResponse.Error;
        return new CreatePersonCommandResponse(createResponse.Value);
    }
}
