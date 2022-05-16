using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface ICustomerService
    {
        IEnumerable<CustomerDto> GetAllCustomers(Guid restaurantId, bool trackChanges);
        CustomerDto GetCustomer(Guid restaurantId, Guid id, bool trackChanges);
    }
}
