using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface IRestaurantService
    {
        IEnumerable<RestaurantDto> GetAllRestaurants(bool trackChanges);
        RestaurantDto GetRestaurantById(Guid id, bool trackChanges);
    }
}
