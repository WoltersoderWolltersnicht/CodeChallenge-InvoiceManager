using InvoiceManager.Domain.Common;
using System.Linq.Expressions;

namespace InvoiceManager.Domain.Businesses;

public interface IBusinessRepository
{
    public Task<Result<Business>> GetById(uint id);
    public Task<Result<Business>> Create(Business business);
    public Task<Result<Business>> Update(Business business);
    public Task<Result<Business>> Delete(Business business);
    public Task<Result<IEnumerable<Business>>> Filter(Expression<Func<Business, bool>> query);
}
