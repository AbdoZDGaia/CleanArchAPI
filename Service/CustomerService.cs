using AutoMapper;
using Contracts;
using Entities;
using Entities.Exceptions;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

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
            await CheckIfRestaurantExists(restaurantId, trackChanges);

            var customerEntity = _mapper.Map<Customer>(customerDto);

            _repositoryManager.Customer.CreateCustomerForRestaurant(restaurantId, customerEntity);
            await _repositoryManager.SaveAsync();

            var customerToReturn = _mapper.Map<CustomerDto>(customerEntity);
            return customerToReturn;
        }

        public async Task DeleteCustomerForRestaurantAsync(Guid restaurantId, Guid id, bool trackChanges)
        {
            await CheckIfRestaurantExists(restaurantId, trackChanges);

            var customerDb = await GetCustomerForRestaurantAndCheckIfItExists(restaurantId, id, trackChanges);


            _repositoryManager.Customer.DeleteCustomer(customerDb);
            await _repositoryManager.SaveAsync();
        }

        public async Task<(IEnumerable<CustomerDto> customers, Metadata metadata)> GetAllCustomersAsync(Guid restaurantId, CustomerParameters customerParameters, bool trackChanges)
        {
            await CheckIfRestaurantExists(restaurantId, trackChanges);

            var customersWithMetaData = await _repositoryManager.Customer.GetAllCustomersAsync(restaurantId, customerParameters, trackChanges);
            var customersDto = _mapper.Map<IEnumerable<CustomerDto>>(customersWithMetaData);
            return (customers: customersDto, metadata: customersWithMetaData.Metadata!);
        }

        public async Task<CustomerDto> GetCustomerAsync(Guid restaurantId, Guid id, bool trackChanges)
        {
            await CheckIfRestaurantExists(restaurantId, trackChanges);

            var customer = await GetCustomerForRestaurantAndCheckIfItExists(restaurantId, id, trackChanges);

            var customerDto = _mapper.Map<CustomerDto>(customer);
            return customerDto;
        }

        public async Task<(CustomerForUpdateDto customerToPatch, Customer customerEntity)> GetCustomerForPatchAsync
            (Guid restaurantId, Guid id, bool restTrackChanges, bool custTrackChanges)
        {
            await CheckIfRestaurantExists(restaurantId, restTrackChanges);

            var customer = await GetCustomerForRestaurantAndCheckIfItExists(restaurantId, id, custTrackChanges);

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
            await CheckIfRestaurantExists(restaurantId, restTrackChanges);

            var customerDb = await GetCustomerForRestaurantAndCheckIfItExists(restaurantId, id, custTrackChanges);

            _mapper.Map(customerDto, customerDb);
            await _repositoryManager.SaveAsync();
        }

        private async Task<Customer> GetCustomerForRestaurantAndCheckIfItExists
            (Guid companyId, Guid id, bool trackChanges)
        {
            var customerDb = await _repositoryManager.Customer.GetCustomerAsync(companyId, id,
            trackChanges);
            if (customerDb is null)
                throw new CustomerNotFoundException(id);
            return customerDb;
        }

        private async Task CheckIfRestaurantExists(Guid restaurantId, bool restTrackChanges)
        {
            var restaurant = await _repositoryManager.Restaurant.GetRestaurantAsync(restaurantId, restTrackChanges);
            if (restaurant is null)
                throw new RestaurantNotFoundException(restaurantId);
        }
    }
}
