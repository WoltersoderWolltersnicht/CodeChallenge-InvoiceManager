using InvoiceManager.Domain.Common;

namespace InvoiceManager.Domain.InvoiceLines;

public interface IInvoiceLineRepository
{
    public Task<Result<InvoiceLine>> GetInvoiceLineById(uint id);
    public Task<Result<InvoiceLine>> CreateInvoiceLine(InvoiceLine newInvoiceLine);
    public Task<Result<InvoiceLine>> UpdateInvoiceLine(InvoiceLine newInvoiceLine);
    public Task<Result<InvoiceLine>> DeleteInvoiceLine(uint id);
}
