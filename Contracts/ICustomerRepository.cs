using Entities;

namespace Contracts
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetAllCustomers(Guid restaurantId, bool trackChanges);
        Customer? GetCustomer(Guid restaurantId, Guid id, bool trackChanges);
    }
}
