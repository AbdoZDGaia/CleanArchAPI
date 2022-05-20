using Entities;

namespace Contracts
{
    public interface IRestaurantRepository
    {
        IEnumerable<Restaurant> GetAllRestaurants(bool trackChanges);
        Restaurant? GetRestaurant(Guid id, bool trackChanges);
        IEnumerable<Restaurant> GetRestaurantsByIds(IEnumerable<Guid> ids, bool trackChanges);
        void CreateRestaurant(Restaurant restaurant);
        void DeleteRestaurant(Restaurant restaurant);
    }
}
