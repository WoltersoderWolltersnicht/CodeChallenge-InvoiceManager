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
        var invoice = _context.Invoices.Attach(new Invoice { Id = id });
        invoice.State = EntityState.Deleted;
        var result = await _context.SaveChangesAsync();
        if (result != 1) return $"Error deleting invoice Id:{id}";
        return invoice.Entity;
    }

    public async Task<Result<Invoice>> GetInvoiceById(uint id)
    {
        var business = await _context.Invoices.SingleAsync(b => b.Id == id);
        if (business is null) return $"Invoice with Id:{id} not found";
        return business;
    }

    public async Task<Result<Invoice>> UpdateInvoice(Invoice newInvoice)
    {
        var invoiceToUpdate = await _context.Invoices.SingleAsync(b => b.Id == newInvoice.Id);
        if (invoiceToUpdate is null) return $"Invoice with Id:{newInvoice.Id} not found";
        if (newInvoice.Estado != null) invoiceToUpdate.Estado = newInvoice.Estado;
        var result = await _context.SaveChangesAsync();
        if (result != 1) return $"Invoice with Id:{newInvoice.Id} could not be updated";
        return newInvoice;
    }
}
