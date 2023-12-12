using InvoiceManager.Domain.Common;

namespace InvoiceManager.Domain.Invoices;

public interface IInvoiceRepository
{
    public Result<Invoice> GetInvoiceById(uint id);
    public Result<Invoice> CreateInvoice(Invoice newInvoice);
    public Result<Invoice> UpdateInvoice(uint id, Invoice newInvoice);
    public Result<Invoice> DeleteInvoice(uint id);
}
