using Entities;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service.Contracts
{
    public interface ICustomerService
    {
        Task<(IEnumerable<CustomerDto> customers, Metadata metadata)> GetAllCustomersAsync(Guid restaurantId, CustomerParameters customerParameters, bool trackChanges);
        Task<CustomerDto> GetCustomerAsync(Guid restaurantId, Guid id, bool trackChanges);
        Task<CustomerDto> CreateCustomerForRestaurantAsync(Guid restaurantId, CustomerForCreationDto customerDto, bool trackChanges);
        Task DeleteCustomerForRestaurantAsync(Guid restaurantId, Guid id, bool trackChanges);
        Task UpdateCustomerForRestaurantAsync(Guid restaurantId, Guid id, CustomerForUpdateDto customerDto, bool restTrackChanges, bool custTrackChanges);
        Task<(CustomerForUpdateDto customerToPatch, Customer customerEntity)> GetCustomerForPatchAsync(Guid restaurantId, Guid id, bool restTrackChanges, bool custTrackChanges);
        Task SaveChangesForPatchAsync(CustomerForUpdateDto customerToPatch, Customer customerEntity);
    }
}
