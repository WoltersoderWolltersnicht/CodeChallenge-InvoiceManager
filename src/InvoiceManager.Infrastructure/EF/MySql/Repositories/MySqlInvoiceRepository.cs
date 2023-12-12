using InvoiceManager.Domain.Common;
using InvoiceManager.Domain.InvoiceLines;
using InvoiceManager.Domain.Invoices;

namespace InvoiceManager.Infrastructure.EF.MySql.Repositories;

public class MySqlInvoiceRepository : IInvoiceRepository
{
    public Result<Invoice> CreateInvoice(Invoice newInvoice)
    {
        throw new NotImplementedException();
    }

    public Result<Invoice> DeleteInvoice(uint id)
    {
        throw new NotImplementedException();
    }

    public Result<Invoice> GetInvoiceById(uint id)
    {
        throw new NotImplementedException();
    }

    public Result<Invoice> UpdateInvoice(uint id, Invoice newInvoice)
    {
        throw new NotImplementedException();
    }
}
