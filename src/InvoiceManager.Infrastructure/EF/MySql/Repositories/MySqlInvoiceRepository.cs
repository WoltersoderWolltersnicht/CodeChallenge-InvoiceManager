using InvoiceManager.Domain.Common;
using InvoiceManager.Domain.Exceptions;
using InvoiceManager.Domain.Invoices;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InvoiceManager.Infrastructure.EF.MySql.Repositories;

public class MySqlInvoiceRepository : IInvoiceRepository
{
    private readonly InvoiceManagerDbContext _context;

    public MySqlInvoiceRepository(InvoiceManagerDbContext context)
    {
        _context = context;
    }

    public async Task<Result<IEnumerable<Invoice>>> Filter(Expression<Func<Invoice, bool>> query)
    {
        var invoices = await _context
            .Invoices.Include(i => i.Person)
            .Include(i => i.Business)
            .Include(i => i.InvoiceLines).Where(query).ToListAsync();
        
        return invoices;
    }


    public async Task<Result<Invoice>> GetById(uint id)
    {
        var business = await _context.Invoices
            .Include(i => i.Person)
            .Include(i => i.Business)
            .Include(i => i.InvoiceLines)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (business is null) return new IdNotFoundException("Invoice", id);
        return business;
    }

    public async Task<Result<Invoice>> Create(Invoice newEntity)
    {
        _context.Set<Invoice>().Add(newEntity);
        var result = await _context.SaveChangesAsync();
        if (result < 1) return new DatabaseException("Error storing business into database");
        return newEntity;
    }

    public async Task<Result<Invoice>> Delete(Invoice entity)
    {
        var entityDeleted = _context.Set<Invoice>().Remove(entity);
        var result = await _context.SaveChangesAsync();
        if (result < 1) return new DatabaseException($"Error deleting business Id:{entity.Id}");
        return entityDeleted.Entity;
    }

    public async Task<Result<Invoice>> Update(Invoice element)
    {
        var updateElement = _context.Entry(element);
        updateElement.State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return updateElement.Entity;
    }
}
