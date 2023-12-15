using InvoiceManager.Domain.Common;
using InvoiceManager.Domain.Exceptions;
using InvoiceManager.Domain.InvoiceLines;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InvoiceManager.Infrastructure.EF.MySql.Repositories;

public class MySqlInvoiceLineRepository : IInvoiceLineRepository
{
    private readonly InvoiceManagerDbContext _context;

    public MySqlInvoiceLineRepository(InvoiceManagerDbContext context)
    {
        _context = context;
    }

    public async Task<Result<IEnumerable<InvoiceLine>>> Filter(Expression<Func<InvoiceLine, bool>> query)
    {
        var invoiceLines = await _context.InvoiceLines
            .Include(il => il.Invoice)
            .Where(query).ToListAsync();
        return invoiceLines;
    }

    public async Task<Result<InvoiceLine>> GetById(uint id)
    {
        var invoiceLine = await _context.InvoiceLines.Include(il => il.Invoice).FirstOrDefaultAsync(b => b.Id == id);
        if (invoiceLine is null) return new IdNotFoundException(id);
        return invoiceLine;
    }

    public async Task<Result<InvoiceLine>> Create(InvoiceLine newEntity)
    {
        _context.Set<InvoiceLine>().Add(newEntity);
        var result = await _context.SaveChangesAsync();
        if (result < 1) return new DatabaseException("Error storing business into database");
        return newEntity;
    }

    public async Task<Result<InvoiceLine>> Delete(InvoiceLine entity)
    {
        var entityDeleted = _context.Set<InvoiceLine>().Remove(entity);
        var result = await _context.SaveChangesAsync();
        if (result < 1) return new DatabaseException($"Error deleting business Id:{entity.Id}");
        return entityDeleted.Entity;
    }

    public async Task<Result<InvoiceLine>> Update(InvoiceLine element)
    {
        var updateElement = _context.Entry(element);
        updateElement.State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return updateElement.Entity;
    }
}
