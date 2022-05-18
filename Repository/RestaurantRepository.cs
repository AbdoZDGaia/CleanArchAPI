using Contracts;
using Entities;

namespace Repository
{
    public class RestaurantRepository : RepositoryBase<Restaurant>, IRestaurantRepository
    {
        public RestaurantRepository(RepositoryContext context) : base(context)
        {
        }

        public void CreateRestaurant(Restaurant restaurant) => Create(restaurant);

        public IEnumerable<Restaurant> GetAllRestaurants(bool trackChanges)
        {
            return FindAll(trackChanges)
                .OrderBy(restaurant => restaurant.Name)
                .ToList();
        }

        public Restaurant? GetRestaurant(Guid id, bool trackChanges)
        {
            return FindByCondition(r => r.Id.Equals(id), trackChanges)
                .SingleOrDefault();
        }

        public IEnumerable<Restaurant> GetRestaurantsByIds(IEnumerable<Guid> ids, bool trackChanges)
        {
            return FindByCondition(r => ids.Contains(r.Id), trackChanges)
                .ToList();
        }
    }
}
