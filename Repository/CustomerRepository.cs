using Entities;
using Contracts;

namespace Repository
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(RepositoryContext context) : base(context)
        {
        }

        public IEnumerable<Customer> GetAllCustomers(Guid restaurantId, bool trackChanges)
        {
            return FindByCondition(c => c.RestaurantId == restaurantId, trackChanges)
                .OrderBy(c => c.Name)
                .ToList();
        }
    }
}
