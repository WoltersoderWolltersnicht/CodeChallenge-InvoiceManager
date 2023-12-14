using InvoiceManager.Domain.Common;
using InvoiceManager.Domain.Invoices;
using Microsoft.EntityFrameworkCore;

namespace InvoiceManager.Infrastructure.EF.MySql.Repositories;

public class MySqlInvoiceRepository : IInvoiceRepository
{
    private readonly InvoiceManagerDbContext _context;

    public MySqlInvoiceRepository(InvoiceManagerDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Invoice>> CreateInvoice(Invoice newInvoice)
    {
        _context.Invoices.Add(newInvoice);
        var result = await _context.SaveChangesAsync();
        if (result != 1) return "Error storing invoice into database";
        return newInvoice;
    }

    public async Task<Result<Invoice>> DeleteInvoice(uint id)
    {
        var invoice = await GetInvoiceById(id);
        if (!invoice.IsSuccess) return invoice.Error;
        _context.Remove(invoice);
        var result = await _context.SaveChangesAsync();
        if (result != 1) return $"Error deleting invoice Id:{id}";
        return invoice.Value;
    }

    public async Task<Result<Invoice>> GetInvoiceById(uint id)
    {
        var business = await _context.Invoices.Include(i => i.Person).Include(i => i.Business).Include(i => i.InvoiceLines).FirstOrDefaultAsync(b => b.Id == id);
        if (business is null) return $"Invoice with Id:{id} not found";
        return business;
    }

    public async Task<Result<Invoice>> UpdateInvoice(Invoice newInvoice)
    {
        var invoiceToUpdate = await GetInvoiceById(newInvoice.Id);
        if (!invoiceToUpdate.IsSuccess) return invoiceToUpdate.Error;
        if (newInvoice.Estado != null) invoiceToUpdate.Value.Estado = newInvoice.Estado;
        var result = await _context.SaveChangesAsync();
        if (result != 1) return $"Invoice with Id:{newInvoice.Id} could not be updated";
        return newInvoice;
    }
}
