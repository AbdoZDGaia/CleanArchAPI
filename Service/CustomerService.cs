using AutoMapper;
using Contracts;
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

        public IEnumerable<CustomerDto> GetCustomers(Guid restaurantId, bool trackChanges)
        {
            var restaurant = _repositoryManager.Restaurant.GetRestaurant(restaurantId, trackChanges);
            if (restaurant is null)
                throw new RestaurantNotFoundException(restaurantId);
            
            var customers = _repositoryManager.Customer.GetAllCustomers(restaurantId, trackChanges);
            var customersDto = _mapper.Map<IEnumerable<CustomerDto>>(customers);
            return customersDto;
        }
    }
}
