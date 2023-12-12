using InvoiceManager.Domain.Common;

namespace InvoiceManager.Domain.Businesses;

public interface IBusinessRepository
{
    public Result<Business> GetBusinessById(uint id);
    public Result<Business> CreateBusiness(Business newBusiness);
    public Result<Business> UpdateBusiness(uint id, Business newBusiness);
    public Result<Business> DeleteBusiness(uint id);
}
