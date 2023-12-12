using InvoiceManager.Domain.Businesses;
using InvoiceManager.Domain.Common;
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
        if (result != 1) return "Error storing business into database";
        return newBusiness;
    }

    public async Task<Result<Business>> DeleteBusiness(uint id)
    {
        var business = _context.Business.Attach(new Business { Id = id });
        business.State = EntityState.Deleted;
        var result = await _context.SaveChangesAsync();
        if (result != 1) return $"Error deleting business Id:{id}";
        return business.Entity;
    }

    public async Task<Result<Business>> GetBusinessById(uint id)
    {
        var business = await _context.Business.SingleAsync(b => b.Id == id);
        if (business is null) return $"Business with Id:{id} not found";
        return business;
    }

    public async Task<Result<Business>> UpdateBusiness(Business newBusiness)
    {
        var businessToUpdate = await _context.Business.SingleAsync(b => b.Id == newBusiness.Id);
        if (businessToUpdate is null) return $"Business with Id:{newBusiness.Id} not found";
        if(newBusiness.Name != null) businessToUpdate.Name = newBusiness.Name;
        if (newBusiness.CIF != null) businessToUpdate.CIF = newBusiness.CIF;
        var result = await _context.SaveChangesAsync();
        if(result != 1) return $"Business with Id:{newBusiness.Id} could not be updated";
        return newBusiness;
    }
}
