using InvoiceManager.Domain.Common;

namespace InvoiceManager.Domain.InvoiceLines;

public interface IInvoiceLineRepository
{
    public Result<InvoiceLine> GetInvoiceLineById(uint id);
    public Result<InvoiceLine> CreateInvoiceLine(InvoiceLine newInvoiceLine);
    public Result<InvoiceLine> UpdateInvoiceLine(uint id, InvoiceLine newInvoiceLine);
    public Result<InvoiceLine> DeleteInvoiceLine(uint id);
}
