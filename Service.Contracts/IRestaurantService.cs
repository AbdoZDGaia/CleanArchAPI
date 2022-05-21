using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface IRestaurantService
    {
        Task<IEnumerable<RestaurantDto>> GetAllRestaurantsAsync(bool trackChanges);
        Task<RestaurantDto> GetRestaurantByIdAsync(Guid id, bool trackChanges);
        Task<IEnumerable<RestaurantDto>> GetRestaurantsByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        Task<RestaurantDto> CreateRestaurantAsync(RestaurantForCreationDto restaurant);
        Task<(IEnumerable<RestaurantDto> restaurants, string ids)> CreateRestaurantsAsync(IEnumerable<RestaurantForCreationDto> restaurantCollection);
        Task DeleteRestaurantAsync(Guid restaurantId, bool trackChanges);
        Task UpdateRestaurantAsync(Guid restaurantId, RestaurantForUpdateDto restaurant, bool trackChanges);
    }
}
