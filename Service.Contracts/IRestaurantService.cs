using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface IRestaurantService
    {
        IEnumerable<RestaurantDto> GetAllRestaurants(bool trackChanges);
        RestaurantDto GetRestaurantById(Guid id, bool trackChanges);
        IEnumerable<RestaurantDto> GetRestaurantsByIds(IEnumerable<Guid> ids, bool trackChanges);
        RestaurantDto CreateRestaurant(RestaurantForCreationDto restaurant);
        (IEnumerable<RestaurantDto> restaurants, string ids) CreateRestaurants(IEnumerable<RestaurantForCreationDto> restaurantCollection);
        void DeleteRestaurant(Guid restaurantId, bool trackChanges);
        void UpdateRestaurant(Guid restaurantId, RestaurantForUpdateDto restaurant, bool trackChanges);
    }
}
