using Entities;

namespace Contracts
{
    public interface IRestaurantRepository
    {
        IEnumerable<Restaurant> GetAllRestaurants(bool trackChanges);
    }
}
