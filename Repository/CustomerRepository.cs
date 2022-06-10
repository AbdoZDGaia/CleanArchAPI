using Entities;
using Contracts;
using Microsoft.EntityFrameworkCore;
using Shared.RequestFeatures;

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

        public async Task<PagedList<Customer>> GetAllCustomersAsync(Guid restaurantId, CustomerParameters customerParameters, bool trackChanges)
        {
            var customers = await FindByCondition(c => c.RestaurantId == restaurantId &&
            (c.Age >= customerParameters.MinAge && c.Age <= customerParameters.MaxAge)
            , trackChanges)
             .OrderBy(c => c.Name)
             .Skip((customerParameters.PageNumber - 1) * customerParameters.PageSize)
             .Take(customerParameters.PageSize)
             .ToListAsync();

            var count = customers.Count;

            return new PagedList<Customer>(customers,
                count,
                customerParameters.PageNumber,
                customerParameters.PageSize);
        }

        public async Task<Customer?> GetCustomerAsync(Guid restaurantId, Guid id, bool trackChanges) =>
            await FindByCondition(c => c.Id.Equals(id)
            && c.RestaurantId.Equals(restaurantId), trackChanges)
                .SingleOrDefaultAsync();
    }
}
