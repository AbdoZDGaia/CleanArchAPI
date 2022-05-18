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
        private readonly ILoggerManager _loggerManager;
        private readonly IMapper _mapper;

        public CustomerService(IRepositoryManager repositoryManager
            , ILoggerManager loggerManager
            , IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _loggerManager = loggerManager;
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
    }
}
