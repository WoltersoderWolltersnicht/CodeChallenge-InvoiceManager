using InvoiceManager.Domain.Common;
using System.Linq.Expressions;

namespace InvoiceManager.Domain.People;

public interface IPersonRepository
{
    public Task<Result<Person>> GetById(uint id);
    public Task<Result<Person>> Create(Person person);
    public Task<Result<Person>> Update(Person person);
    public Task<Result<Person>> Delete(Person person);
    public Task<Result<IEnumerable<Person>>> Filter(Expression<Func<Person, bool>> query);
}
