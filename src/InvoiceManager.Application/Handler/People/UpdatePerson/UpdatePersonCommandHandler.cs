using InvoiceManager.Domain.Common;
using InvoiceManager.Domain.Exceptions;
using InvoiceManager.Domain.People;
using MediatR;

namespace InvoiceManager.Application.Handler.People.UpdatePerson;

public class UpdatePersonQueryHandler : IRequestHandler<UpdatePersonCommand, Result<UpdatePersonCommandResponse>>
{
    private readonly IPersonRepository _personRepository;

    public UpdatePersonQueryHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<Result<UpdatePersonCommandResponse>> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
    {
        if(request.NIF != null)
        {
            var checkUniquePerson = await _personRepository.Filter(p => p.NIF == request.NIF);
            if (checkUniquePerson.Value.Any()) return new DupplicateKeyException($"Person with same NIF allready created with Id : {checkUniquePerson.Value.First().Id}");
        }

        var personToUpdateResult = await _personRepository.GetById(request.Id);
        if (!personToUpdateResult.IsSuccess) return personToUpdateResult.Error;

        var personToUpdate = personToUpdateResult.Value;

        if (request.Name != null) personToUpdate.Name = request.Name;
        if (request.Surname1 != null) personToUpdate.Surname1 = request.Surname1;
        if (request.Surname2 != null) personToUpdate.Surname2 = request.Surname2;
        if (request.NIF != null) personToUpdate.NIF = request.NIF;

        var updateResponse = await _personRepository.Update(personToUpdate);
        if (!updateResponse.IsSuccess) return updateResponse.Error;
        return new UpdatePersonCommandResponse(updateResponse.Value);
    }
}
