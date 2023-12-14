using InvoiceManager.Domain.Businesses;
using InvoiceManager.Domain.Common;
using InvoiceManager.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace InvoiceManager.Infrastructure.EF.MySql.Repositories;

public class MySqlBusinessRepository : IBusinessRepository
{
    private readonly InvoiceManagerDbContext _context;

    public MySqlBusinessRepository(InvoiceManagerDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Business>> CreateBusiness(Business newBusiness)
    {
        _context.Business.Add(newBusiness);
        var result = await _context.SaveChangesAsync();
        if (result != 1) return new DatabaseException("Error storing business into database");
        return newBusiness;
    }

    public async Task<Result<Business>> DeleteBusiness(uint id)
    {
        var entityToDelete = await GetBusinessById(id);
        if (!entityToDelete.IsSuccess) return entityToDelete.Error;

        var entityDeleted = _context.Remove(entityToDelete);
        var result = await _context.SaveChangesAsync();
        if (result != 1) return new DatabaseException($"Error deleting business Id:{id}");
        return entityDeleted.Entity;
    }

    public async Task<Result<Business>> GetBusinessById(uint id)
    {
        var business = await _context.Business
            .Include(b => b.Invoices).ThenInclude(i => i.InvoiceLines)
            .Include(b => b.Invoices).ThenInclude(i => i.Person)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (business is null) return new IdNotFoundException(id);
        return business;
    }

    public async Task<Result<Business>> UpdateBusiness(Business newBusiness)
    {
        var businessToUpdate = await GetBusinessById(newBusiness.Id);
        if (!businessToUpdate.IsSuccess) return businessToUpdate.Error;
        
        if(newBusiness.Name != null) businessToUpdate.Value.Name = newBusiness.Name;
        if (newBusiness.CIF != null) businessToUpdate.Value.CIF = newBusiness.CIF;
        var result = await _context.SaveChangesAsync();
        if(result != 1) return new DatabaseException("Business with Id:{newBusiness.Id} could not be updated");
        return businessToUpdate;
    }
}
