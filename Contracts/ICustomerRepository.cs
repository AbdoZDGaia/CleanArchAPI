using Entities;

namespace Contracts
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync(Guid restaurantId, bool trackChanges);
        Task<Customer?> GetCustomerAsync(Guid restaurantId, Guid id, bool trackChanges);
        void CreateCustomerForRestaurant(Guid restaurantId, Customer customer);
        void DeleteCustomer(Customer customer);
    }
}
