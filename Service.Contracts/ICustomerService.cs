using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface ICustomerService
    {
        IEnumerable<CustomerDto> GetCustomers(Guid restaurantId, bool trackChanges);
    }
}
