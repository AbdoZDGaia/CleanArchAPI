using Entities;
using Shared.RequestFeatures;

namespace Contracts
{
    public interface ICustomerRepository
    {
        Task<PagedList<Customer>> GetAllCustomersAsync(Guid restaurantId, CustomerParameters customerParameters, bool trackChanges);
        Task<Customer?> GetCustomerAsync(Guid restaurantId, Guid id, bool trackChanges);
        void CreateCustomerForRestaurant(Guid restaurantId, Customer customer);
        void DeleteCustomer(Customer customer);
    }
}
