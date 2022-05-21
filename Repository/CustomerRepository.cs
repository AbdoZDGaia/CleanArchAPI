using Entities;
using Contracts;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(RepositoryContext context) : base(context)
        {
        }

        public void CreateCustomerForRestaurant(Guid restaurantId, Customer customer)
        {
            customer.RestaurantId = restaurantId;
            Create(customer);
        }

        public void DeleteCustomer(Customer customer) => Delete(customer);

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync(Guid restaurantId, bool trackChanges) =>
            await FindByCondition(c => c.RestaurantId == restaurantId, trackChanges)
                .OrderBy(c => c.Name)
                .ToListAsync();

        public async Task<Customer?> GetCustomerAsync(Guid restaurantId, Guid id, bool trackChanges) =>
            await FindByCondition(c => c.Id.Equals(id)
            && c.RestaurantId.Equals(restaurantId), trackChanges)
                .SingleOrDefaultAsync();
    }
}
