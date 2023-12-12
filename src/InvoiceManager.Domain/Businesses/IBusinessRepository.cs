using InvoiceManager.Domain.Common;

namespace InvoiceManager.Domain.Businesses;

public interface IBusinessRepository
{
    public Task<Result<Business>> GetBusinessById(uint id);
    public Task<Result<Business>> CreateBusiness(Business newBusiness);
    public Task<Result<Business>> UpdateBusiness(Business newBusiness);
    public Task<Result<Business>> DeleteBusiness(uint id);
}
