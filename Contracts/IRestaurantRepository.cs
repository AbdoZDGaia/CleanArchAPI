using Entities;

namespace Contracts
{
    public interface IRestaurantRepository
    {
        IEnumerable<Restaurant> GetAllRestaurants(bool trackChanges);
        Restaurant? GetRestaurant(Guid id, bool trackChanges);
    }
}
