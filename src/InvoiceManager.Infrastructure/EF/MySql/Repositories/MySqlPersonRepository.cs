using InvoiceManager.Domain.Common;
using InvoiceManager.Domain.People;

namespace InvoiceManager.Infrastructure.EF.MySql.Repositories;

public class MySqlPersonRepository : IPersonRepository
{
    public Result<Person> CreatePerson(Person newPerson)
    {
        throw new NotImplementedException();
    }

    public Result<Person> DeletePerson(uint id)
    {
        throw new NotImplementedException();
    }

    public Result<Person> GetPersonById(uint id)
    {
        throw new NotImplementedException();
    }

    public Result<Person> UpdatePerson(uint id, Person newPerson)
    {
        throw new NotImplementedException();
    }
}
