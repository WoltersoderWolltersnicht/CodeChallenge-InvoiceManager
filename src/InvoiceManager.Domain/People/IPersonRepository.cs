using InvoiceManager.Domain.Common;

namespace InvoiceManager.Domain.People;

public interface IPersonRepository
{
    public Result<Person> GetPersonById(uint id);
    public Result<Person> CreatePerson(Person newPerson);
    public Result<Person> UpdatePerson(uint id, Person newPerson);
    public Result<Person> DeletePerson(uint id);
}
