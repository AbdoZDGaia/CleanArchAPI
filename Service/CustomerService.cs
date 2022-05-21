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

        public CustomerDto CreateCustomerForRestaurant(Guid restaurantId, CustomerForCreationDto customerDto, bool trackChanges)
        {
            var restaurant = _repositoryManager.Restaurant.GetRestaurant(restaurantId, trackChanges);
            if (restaurant == null)
            {
                throw new RestaurantNotFoundException(restaurantId);
            }

            var customerEntity = _mapper.Map<Customer>(customerDto);

            _repositoryManager.Customer.CreateCustomerForRestaurant(restaurantId, customerEntity);
            _repositoryManager.Save();

            var customerToReturn = _mapper.Map<CustomerDto>(customerEntity);
            return customerToReturn;
        }

        public void DeleteCustomerForRestaurant(Guid restaurantId, Guid id, bool trackChanges)
        {
            var restaurant = _repositoryManager.Restaurant.GetRestaurant(restaurantId, trackChanges);
            if (restaurant == null)
            {
                throw new RestaurantNotFoundException(restaurantId);
            }

            var customerForRestaurant = _repositoryManager.Customer.GetCustomer(restaurantId, id, trackChanges);
            if (customerForRestaurant == null)
            {
                throw new CustomerNotFoundException(id);
            }

            _repositoryManager.Customer.DeleteCustomer(customerForRestaurant);
            _repositoryManager.Save();
        }

        public IEnumerable<CustomerDto> GetAllCustomers(Guid restaurantId, bool trackChanges)
        {
            var restaurant = _repositoryManager.Restaurant.GetRestaurant(restaurantId, trackChanges);
            if (restaurant is null)
                throw new RestaurantNotFoundException(restaurantId);

            var customers = _repositoryManager.Customer.GetAllCustomers(restaurantId, trackChanges);
            var customersDto = _mapper.Map<IEnumerable<CustomerDto>>(customers);
            return customersDto;
        }

        public CustomerDto GetCustomer(Guid restaurantId, Guid id, bool trackChanges)
        {
            var restaurant = _repositoryManager.Restaurant.GetRestaurant(restaurantId, trackChanges);
            if (restaurant is null)
                throw new RestaurantNotFoundException(restaurantId);

            var customer = _repositoryManager.Customer.GetCustomer(restaurantId, id, trackChanges);
            if (customer is null)
                throw new CustomerNotFoundException(id);

            var customerDto = _mapper.Map<CustomerDto>(customer);
            return customerDto;
        }

        public (CustomerForUpdateDto customerToPatch, Customer customerEntity) GetCustomerForPatch
            (Guid restaurantId, Guid id, bool restTrackChanges, bool custTrackChanges)
        {
            var restaurant = _repositoryManager.Restaurant.GetRestaurant(restaurantId, restTrackChanges);
            if (restaurant is null)
                throw new RestaurantNotFoundException(restaurantId);

            var customer = _repositoryManager.Customer.GetCustomer(restaurantId, id, custTrackChanges);
            if (customer is null)
                throw new CustomerNotFoundException(id);

            var customerToPatch = _mapper.Map<CustomerForUpdateDto>(customer);
            return (customerToPatch, customer);
        }

        public void SaveChangesForPatch(CustomerForUpdateDto customerToPatch, Customer customerEntity)
        {
            _mapper.Map(customerToPatch, customerEntity);
            _repositoryManager.Save();
        }

        public void UpdateCustomerForRestaurant(Guid restaurantId, Guid id, CustomerForUpdateDto customerDto, bool restTrackChanges, bool custTrackChanges)
        {
            var restaurant = _repositoryManager.Restaurant.GetRestaurant(restaurantId, restTrackChanges);
            if (restaurant is null)
            {
                throw new RestaurantNotFoundException(restaurantId);
            }

            var customer = _repositoryManager.Customer.GetCustomer(restaurantId, id, custTrackChanges);
            if (customer is null)
            {
                throw new CustomerNotFoundException(id);
            }

            _mapper.Map(customerDto, customer);
            _repositoryManager.Save();
        }
    }
}
