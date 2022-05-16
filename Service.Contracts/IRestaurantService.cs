using Entities;

namespace Service.Contracts
{
    public interface IRestaurantService
    {
        IEnumerable<Restaurant> GetAllRestaurants(bool trackChanges);
    }
}
