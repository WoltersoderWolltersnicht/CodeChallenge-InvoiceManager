using InvoiceManager.Domain.Common;
using System.Linq.Expressions;

namespace InvoiceManager.Domain.Invoices;

public interface IInvoiceRepository
{
    public Task<Result<Invoice>> GetById(uint id);
    public Task<Result<Invoice>> Create(Invoice invoice);
    public Task<Result<Invoice>> Update(Invoice invoice);
    public Task<Result<Invoice>> Delete(Invoice invoice);
    public Task<Result<IEnumerable<Invoice>>> Filter(Expression<Func<Invoice, bool>> query);

}
