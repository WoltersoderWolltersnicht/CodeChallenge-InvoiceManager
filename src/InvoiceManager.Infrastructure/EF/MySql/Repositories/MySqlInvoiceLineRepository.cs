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
        newInvoiceLine.Invoice.Amount += newInvoiceLine.Amount.Value;

        _context.InvoiceLines.Add(newInvoiceLine);
        var result = await _context.SaveChangesAsync();
        if (result != 2) return "Error storing invoice line into database";
        return newInvoiceLine;
    }

    public async Task<Result<InvoiceLine>> DeleteInvoiceLine(uint id)
    {
        var invoiceLine = await GetInvoiceLineById(id);
        if (!invoiceLine.IsSuccess) return invoiceLine.Error;

        invoiceLine.Value.Invoice.Amount -= invoiceLine.Value.Amount.Value;

        _context.Remove(invoiceLine.Value);

        var result = await _context.SaveChangesAsync();
        if (result != 2) return $"Error deleting business Id:{id}";
        return invoiceLine.Value;
    }

    public async Task<Result<InvoiceLine>> GetInvoiceLineById(uint id)
    {
        var invoiceLine = await _context.InvoiceLines.Include(il => il.Invoice).SingleAsync(b => b.Id == id);
        if (invoiceLine is null) return $"Invoice line with Id:{id} not found";
        return invoiceLine;
    }

    public async Task<Result<InvoiceLine>> UpdateInvoiceLine(InvoiceLine newInvoiceLine)
    {
        var businessToUpdate = await GetInvoiceLineById(newInvoiceLine.Id);
        if (!businessToUpdate.IsSuccess) return businessToUpdate.Error;

        if (newInvoiceLine.Amount != null) 
        {
            businessToUpdate.Value.Amount = newInvoiceLine.Amount;
            double amountDifference = newInvoiceLine.Amount.Value - businessToUpdate.Value.Amount.Value;
            businessToUpdate.Value.Invoice.Amount += amountDifference;
        }

        if (newInvoiceLine.VAT != null) businessToUpdate.Value.VAT = newInvoiceLine.VAT;

        var result = await _context.SaveChangesAsync();
        if (result < 1) return $"Invoice line with Id:{newInvoiceLine.Id} could not be updated";
        return businessToUpdate;
    }
}
