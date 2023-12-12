using InvoiceManager.Domain.Common;
using InvoiceManager.Domain.InvoiceLines;
using Microsoft.EntityFrameworkCore;

namespace InvoiceManager.Infrastructure.EF.MySql.Repositories;

public class MySqlInvoiceLineRepository : IInvoiceLineRepository
{
    private readonly InvoiceManagerDbContext _context;

    public MySqlInvoiceLineRepository(InvoiceManagerDbContext context)
    {
        _context = context;
    }

    public async Task<Result<InvoiceLine>> CreateInvoiceLine(InvoiceLine newInvoiceLine)
    {
        _context.InvoiceLines.Add(newInvoiceLine);
        var result = await _context.SaveChangesAsync();
        if (result != 1) return "Error storing invoice line into database";
        return newInvoiceLine;
    }

    public async Task<Result<InvoiceLine>> DeleteInvoiceLine(uint id)
    {
        var invoiceLine = _context.InvoiceLines.Attach(new InvoiceLine { Id = id });
        invoiceLine.State = EntityState.Deleted;
        var result = await _context.SaveChangesAsync();
        if (result != 1) return $"Error deleting business Id:{id}";
        return invoiceLine.Entity;
    }

    public async Task<Result<InvoiceLine>> GetInvoiceLineById(uint id)
    {
        var invoiceLine = await _context.InvoiceLines.SingleAsync(b => b.Id == id);
        if (invoiceLine is null) return $"Invoice line with Id:{id} not found";
        return invoiceLine;
    }

    public async Task<Result<InvoiceLine>> UpdateInvoiceLine(InvoiceLine newInvoiceLine)
    {
        var businessToUpdate = await _context.InvoiceLines.SingleAsync(b => b.Id == newInvoiceLine.Id);
        if (businessToUpdate is null) return $"Invoice line with Id:{newInvoiceLine.Id} not found";
        if (newInvoiceLine.Amount != null) businessToUpdate.Amount = newInvoiceLine.Amount;
        if (newInvoiceLine.Invoice != null) businessToUpdate.Invoice = newInvoiceLine.Invoice;
        var result = await _context.SaveChangesAsync();
        if (result != 1) return $"Invoice line with Id:{newInvoiceLine.Id} could not be updated";
        return newInvoiceLine;
    }
}
