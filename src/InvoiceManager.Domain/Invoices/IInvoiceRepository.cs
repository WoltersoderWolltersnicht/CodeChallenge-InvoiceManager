using InvoiceManager.Domain.Common;

namespace InvoiceManager.Domain.Invoices;

public interface IInvoiceRepository
{
    public Task<Result<Invoice>> GetInvoiceById(uint id);
    public Task<Result<Invoice>> CreateInvoice(Invoice newInvoice);
    public Task<Result<Invoice>> UpdateInvoice(Invoice newInvoice);
    public Task<Result<Invoice>> DeleteInvoice(uint id);
}
