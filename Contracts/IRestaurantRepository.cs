using Entities;

namespace Contracts
{
    public interface IRestaurantRepository
    {
        Task<IEnumerable<Restaurant>> GetAllRestaurantsAsync(bool trackChanges);
        Task<Restaurant?> GetRestaurantAsync(Guid id, bool trackChanges);
        Task<IEnumerable<Restaurant>> GetRestaurantsByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        void CreateRestaurant(Restaurant restaurant);
        void DeleteRestaurant(Restaurant restaurant);
    }
}
