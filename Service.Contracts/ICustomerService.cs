using Entities;
using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface ICustomerService
    {
        IEnumerable<CustomerDto> GetAllCustomers(Guid restaurantId, bool trackChanges);
        CustomerDto GetCustomer(Guid restaurantId, Guid id, bool trackChanges);
        CustomerDto CreateCustomerForRestaurant(Guid restaurantId, CustomerForCreationDto customerDto, bool trackChanges);
        void DeleteCustomerForRestaurant(Guid restaurantId, Guid id, bool trackChanges);
        void UpdateCustomerForRestaurant(Guid restaurantId, Guid id, CustomerForUpdateDto customerDto, bool restTrackChanges, bool custTrackChanges);
        (CustomerForUpdateDto customerToPatch, Customer customerEntity) GetCustomerForPatch(Guid restaurantId, Guid id, bool restTrackChanges, bool custTrackChanges);
        void SaveChangesForPatch(CustomerForUpdateDto customerToPatch, Customer customerEntity);
    }
}
