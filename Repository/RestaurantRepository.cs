using Contracts;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class RestaurantRepository : RepositoryBase<Restaurant>, IRestaurantRepository
    {
        public RestaurantRepository(RepositoryContext context) : base(context)
        {
        }

        public void CreateRestaurant(Restaurant restaurant) => Create(restaurant);

        public void DeleteRestaurant(Restaurant restaurant) => Delete(restaurant);

        public async Task<IEnumerable<Restaurant>> GetAllRestaurantsAsync(bool trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderBy(restaurant => restaurant.Name)
                .ToListAsync();
        }

        public async Task<Restaurant?> GetRestaurantAsync(Guid id, bool trackChanges)
        {
            return await FindByCondition(r => r.Id.Equals(id), trackChanges)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Restaurant>> GetRestaurantsByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
        {
            return await FindByCondition(r => ids.Contains(r.Id), trackChanges)
                .ToListAsync();
        }
    }
}
