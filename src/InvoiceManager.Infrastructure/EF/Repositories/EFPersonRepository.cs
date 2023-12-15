using InvoiceManager.Domain.Common;
using InvoiceManager.Domain.Exceptions;
using InvoiceManager.Domain.People;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InvoiceManager.Infrastructure.EF.Repositories;

public class EFPersonRepository : IPersonRepository
{
    private readonly InvoiceManagerDbContext _context;

    public EFPersonRepository(InvoiceManagerDbContext context)
    {
        _context = context;
    }

    public async Task<Result<IEnumerable<Person>>> Filter(Expression<Func<Person, bool>> query)
    {
        var perople = await _context.People.Where(query).ToListAsync();
        return perople;
    }

    public async Task<Result<Person>> GetById(uint id)
    {
        var person = await _context.People
            .Include(p => p.Invoices).ThenInclude(i => i.InvoiceLines)
            .Include(p => p.Invoices).ThenInclude(i => i.Business)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (person is null) return new IdNotFoundException("Person", id);
        return person;
    }

    public async Task<Result<Person>> Create(Person newEntity)
    {
        _context.Set<Person>().Add(newEntity);
        var result = await _context.SaveChangesAsync();
        if (result < 1) return new DatabaseException("Error storing business into database");
        return newEntity;
    }

    public async Task<Result<Person>> Delete(Person entity)
    {
        var entityDeleted = _context.People.Remove(entity);

        var result = await _context.SaveChangesAsync();
        if (result < 1) return new DatabaseException($"Error deleting business Id:{entity.Id}");
        return entityDeleted.Entity;
    }

    public async Task<Result<Person>> Update(Person element)
    {
        var updateElement = _context.Entry(element);
        updateElement.State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return updateElement.Entity;
    }
}
