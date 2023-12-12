using InvoiceManager.Domain.Common;
using InvoiceManager.Domain.InvoiceLines;

namespace InvoiceManager.Infrastructure.EF.MySql.Repositories;

public class MySqlInvoiceLineRepository : IInvoiceLineRepository
{
    public Result<InvoiceLine> CreateInvoiceLine(InvoiceLine newInvoiceLine)
    {
        throw new NotImplementedException();
    }

    public Result<InvoiceLine> DeleteInvoiceLine(uint id)
    {
        throw new NotImplementedException();
    }

    public Result<InvoiceLine> GetInvoiceLineById(uint id)
    {
        throw new NotImplementedException();
    }

    public Result<InvoiceLine> UpdateInvoiceLine(uint id, InvoiceLine newInvoiceLine)
    {
        throw new NotImplementedException();
    }
}
