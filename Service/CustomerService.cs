using AutoMapper;
using Contracts;
using Entities;
using Entities.Exceptions;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public CustomerService(IRepositoryManager repositoryManager
            , ILoggerManager loggerManager
            , IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<CustomerDto> CreateCustomerForRestaurantAsync(Guid restaurantId, CustomerForCreationDto customerDto, bool trackChanges)
        {
            var restaurant = await _repositoryManager.Restaurant.GetRestaurantAsync(restaurantId, trackChanges);
            if (restaurant == null)
            {
                throw new RestaurantNotFoundException(restaurantId);
            }

            var customerEntity = _mapper.Map<Customer>(customerDto);

            _repositoryManager.Customer.CreateCustomerForRestaurant(restaurantId, customerEntity);
            await _repositoryManager.SaveAsync();

            var customerToReturn = _mapper.Map<CustomerDto>(customerEntity);
            return customerToReturn;
        }

        public async Task DeleteCustomerForRestaurantAsync(Guid restaurantId, Guid id, bool trackChanges)
        {
            var restaurant = await _repositoryManager.Restaurant.GetRestaurantAsync(restaurantId, trackChanges);
            if (restaurant == null)
            {
                throw new RestaurantNotFoundException(restaurantId);
            }

            var customerForRestaurant = await _repositoryManager.Customer.GetCustomerAsync(restaurantId, id, trackChanges);
            if (customerForRestaurant == null)
            {
                throw new CustomerNotFoundException(id);
            }

            _repositoryManager.Customer.DeleteCustomer(customerForRestaurant);
            await _repositoryManager.SaveAsync();
        }

        public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync(Guid restaurantId, bool trackChanges)
        {
            var restaurant = await _repositoryManager.Restaurant.GetRestaurantAsync(restaurantId, trackChanges);
            if (restaurant is null)
                throw new RestaurantNotFoundException(restaurantId);

            var customers = await _repositoryManager.Customer.GetAllCustomersAsync(restaurantId, trackChanges);
            var customersDto = _mapper.Map<IEnumerable<CustomerDto>>(customers);
            return customersDto;
        }

        public async Task<CustomerDto> GetCustomerAsync(Guid restaurantId, Guid id, bool trackChanges)
        {
            var restaurant = await _repositoryManager.Restaurant.GetRestaurantAsync(restaurantId, trackChanges);
            if (restaurant is null)
                throw new RestaurantNotFoundException(restaurantId);

            var customer = await _repositoryManager.Customer.GetCustomerAsync(restaurantId, id, trackChanges);
            if (customer is null)
                throw new CustomerNotFoundException(id);

            var customerDto = _mapper.Map<CustomerDto>(customer);
            return customerDto;
        }

        public async Task<(CustomerForUpdateDto customerToPatch, Customer customerEntity)> GetCustomerForPatchAsync
            (Guid restaurantId, Guid id, bool restTrackChanges, bool custTrackChanges)
        {
            var restaurant = await _repositoryManager.Restaurant.GetRestaurantAsync(restaurantId, restTrackChanges);
            if (restaurant is null)
                throw new RestaurantNotFoundException(restaurantId);

            var customer = await _repositoryManager.Customer.GetCustomerAsync(restaurantId, id, custTrackChanges);
            if (customer is null)
                throw new CustomerNotFoundException(id);

            var customerToPatch = _mapper.Map<CustomerForUpdateDto>(customer);
            return (customerToPatch, customer);
        }

        public async Task SaveChangesForPatchAsync(CustomerForUpdateDto customerToPatch, Customer customerEntity)
        {
            _mapper.Map(customerToPatch, customerEntity);
            await _repositoryManager.SaveAsync();
        }

        public async Task UpdateCustomerForRestaurantAsync(Guid restaurantId, Guid id, CustomerForUpdateDto customerDto, bool restTrackChanges, bool custTrackChanges)
        {
            var restaurant = await _repositoryManager.Restaurant.GetRestaurantAsync(restaurantId, restTrackChanges);
            if (restaurant is null)
            {
                throw new RestaurantNotFoundException(restaurantId);
            }

            var customer = await _repositoryManager.Customer.GetCustomerAsync(restaurantId, id, custTrackChanges);
            if (customer is null)
            {
                throw new CustomerNotFoundException(id);
            }

            _mapper.Map(customerDto, customer);
            await _repositoryManager.SaveAsync();
        }
    }
}
