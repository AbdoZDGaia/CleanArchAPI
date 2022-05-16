using Contracts;
using Entities;

namespace Repository
{
    public class RestaurantRepository : RepositoryBase<Restaurant>, IRestaurantRepository
    {
        public RestaurantRepository(RepositoryContext context) : base(context)
        {
        }

        public IEnumerable<Restaurant> GetAllRestaurants(bool trackChanges)
        {
            return FindAll(trackChanges)
                .OrderBy(restaurant => restaurant.Name)
                .ToList();
        }
    }
}
