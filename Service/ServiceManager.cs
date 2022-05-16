using AutoMapper;
using Contracts;
using Service.Contracts;

namespace Service
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IRestaurantService> _restaurantService;
        private readonly Lazy<ICustomerService> _customerService;
        private readonly Lazy<IAuthenticationService> _authenticationService;

        public ServiceManager(IRepositoryManager repositoryManager
            , ILoggerManager loggerManager
            , IMapper mapper)
        {
            _restaurantService = new Lazy<IRestaurantService>(
                () => new RestaurantService(repositoryManager, loggerManager, mapper));
            _customerService = new Lazy<ICustomerService>(
                () => new CustomerService(repositoryManager, loggerManager, mapper));
            _authenticationService = new Lazy<IAuthenticationService>(
                () => new AuthenticationService(loggerManager));
        }

        public IRestaurantService RestaurantService => _restaurantService.Value;

        public ICustomerService CustomerService => _customerService.Value;
        public IAuthenticationService AuthenticationService => _authenticationService.Value;
    }
}
