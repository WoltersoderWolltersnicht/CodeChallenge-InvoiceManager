using InvoiceManager.Domain.Common;
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
        if (result != 1) return "Error storing business person database";
        return newPerson;
    }

    public async Task<Result<Person>> DeletePerson(uint id)
    {
        var person = _context.People.Attach(new Person { Id = id });
        person.State = EntityState.Deleted;
        var result = await _context.SaveChangesAsync();
        if (result != 1) return $"Error deleting person Id:{id}";
        return person.Entity;
    }

    public async Task<Result<Person>> GetPersonById(uint id)
    {
        var person = await _context.People.SingleAsync(b => b.Id == id);
        if (person is null) return $"Person with Id:{id} not found";
        return person;
    }

    public async Task<Result<Person>> UpdatePerson(Person newPerson)
    {
        var personToUpdate = await _context.People.SingleAsync(b => b.Id == newPerson.Id);
        if (personToUpdate is null) return $"Business with Id:{newPerson.Id} not found";
        
        if (newPerson.Name != null) personToUpdate.Name = newPerson.Name;
        if (newPerson.Surname1 != null) personToUpdate.Surname1 = newPerson.Surname1;
        if (newPerson.Surname2 != null) personToUpdate.Surname2 = newPerson.Surname2;
        if (newPerson.NIF != null) personToUpdate.NIF = newPerson.NIF;

        var result = await _context.SaveChangesAsync();
        if (result != 1) return $"Person with Id:{newPerson.Id} could not be updated";
        
        return newPerson;
    }
}
