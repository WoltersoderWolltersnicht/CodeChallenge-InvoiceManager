using InvoiceManager.Domain.Businesses;
using InvoiceManager.Domain.Common;

namespace InvoiceManager.Infrastructure.EF.MySql.Repositories;

public class MySqlBusinessRepository : IBusinessRepository
{
    public Result<Business> CreateBusiness(Business newBusiness)
    {
        throw new NotImplementedException();
    }

    public Result<Business> DeleteBusiness(uint id)
    {
        throw new NotImplementedException();
    }

    public Result<Business> GetBusinessById(uint id)
    {
        throw new NotImplementedException();
    }

    public Result<Business> UpdateBusiness(uint id, Business newBusiness)
    {
        throw new NotImplementedException();
    }
}
