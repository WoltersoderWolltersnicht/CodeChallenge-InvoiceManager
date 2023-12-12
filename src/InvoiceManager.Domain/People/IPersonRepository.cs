using InvoiceManager.Domain.Common;

namespace InvoiceManager.Domain.People;

public interface IPersonRepository
{
    public Task<Result<Person>> GetPersonById(uint id);
    public Task<Result<Person>> CreatePerson(Person newPerson);
    public Task<Result<Person>> UpdatePerson(Person newPerson);
    public Task<Result<Person>> DeletePerson(uint id);
}
