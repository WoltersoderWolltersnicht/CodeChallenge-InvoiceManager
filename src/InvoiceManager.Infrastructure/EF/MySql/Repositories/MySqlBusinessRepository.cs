using InvoiceManager.Domain.Businesses;
using InvoiceManager.Domain.Common;
using InvoiceManager.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InvoiceManager.Infrastructure.EF.MySql.Repositories;

public class MySqlBusinessRepository : IBusinessRepository
{
    private readonly InvoiceManagerDbContext _context;

    public MySqlBusinessRepository(InvoiceManagerDbContext context)
    {
        _context = context;
    }

    public async Task<Result<IEnumerable<Business>>> Filter(Expression<Func<Business, bool>> query)
    {
        var businesses = await _context.Business
            .Include(b => b.Invoices).ThenInclude(i => i.InvoiceLines)
            .Include(b => b.Invoices).ThenInclude(i => i.Person)
            .Where(query).ToListAsync();
        return businesses;
    }

    public async Task<Result<Business>> GetById(uint id)
    {
        var business = await _context.Business
            .Include(b => b.Invoices).ThenInclude(i => i.InvoiceLines)
            .Include(b => b.Invoices).ThenInclude(i => i.Person)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (business is null) return new IdNotFoundException("Business", id);
        return business;
    }

    public async Task<Result<Business>> Create(Business newEntity)
    {
        _context.Set<Business>().Add(newEntity);
        var result = await _context.SaveChangesAsync();
        if (result < 1) return new DatabaseException("Error storing business into database");
        return newEntity;
    }

    public async Task<Result<Business>> Delete(Business entity)
    {
        var entityDeleted = _context.Set<Business>().Remove(entity);
        var result = await _context.SaveChangesAsync();
        if (result < 1) return new DatabaseException($"Error deleting business Id:{entity.Id}");
        return entityDeleted.Entity;
    }

    public async Task<Result<Business>> Update(Business element)
    {
        var updateElement = _context.Entry(element);
        updateElement.State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return updateElement.Entity;
    }
}
