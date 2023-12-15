using InvoiceManager.Domain.Common;
using System.Linq.Expressions;

namespace InvoiceManager.Domain.InvoiceLines;

public interface IInvoiceLineRepository
{
    public Task<Result<InvoiceLine>> GetById(uint id);
    public Task<Result<InvoiceLine>> Create(InvoiceLine invoiceLine);
    public Task<Result<InvoiceLine>> Update(InvoiceLine invoiceLine);
    public Task<Result<InvoiceLine>> Delete(InvoiceLine invoiceLine);
    public Task<Result<IEnumerable<InvoiceLine>>> Filter(Expression<Func<InvoiceLine, bool>> query);

}
