using InvoiceManager.Domain.Common;
using InvoiceManager.Domain.Exceptions;
using InvoiceManager.Domain.People;
using Microsoft.EntityFrameworkCore;

namespace InvoiceManager.Infrastructure.EF.MySql.Repositories;

public class MySqlPersonRepository : IPersonRepository
{
    private readonly InvoiceManagerDbContext _context;

    public MySqlPersonRepository(InvoiceManagerDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Person>> CreatePerson(Person newPerson)
    {
        _context.People.Add(newPerson);
        var result = await _context.SaveChangesAsync();
        if (result != 1) return new DatabaseException("Error storing person");
        return newPerson;
    }

    public async Task<Result<Person>> DeletePerson(uint id)
    {
        var person = await GetPersonById(id);
        if (!person.IsSuccess) return person.Error;

        _context.Remove(person);
        var result = await _context.SaveChangesAsync();
        if (result != 1) return new DatabaseException($"Error deleting person Id:{id}");
        return person.Value;
    }

    public async Task<Result<Person>> GetPersonById(uint id)
    {
        var person = await _context.People
            .Include(p => p.Invoices).ThenInclude(i => i.InvoiceLines)
            .Include(p => p.Invoices).ThenInclude(i => i.Business)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (person is null) return new IdNotFoundException(id);
        return person;
    }

    public async Task<Result<Person>> UpdatePerson(Person newPerson)
    {
        var personToUpdate = await GetPersonById(newPerson.Id);
        if (!personToUpdate.IsSuccess) return personToUpdate.Error;
        
        if (newPerson.Name != null) personToUpdate.Value.Name = newPerson.Name;
        if (newPerson.Surname1 != null) personToUpdate.Value.Surname1 = newPerson.Surname1;
        if (newPerson.Surname2 != null) personToUpdate.Value.Surname2 = newPerson.Surname2;
        if (newPerson.NIF != null) personToUpdate.Value.NIF = newPerson.NIF;

        var result = await _context.SaveChangesAsync();
        if (result != 1) return new DatabaseException($"Person with Id:{newPerson.Id} could not be updated");
        
        return personToUpdate.Value;
    }
}
