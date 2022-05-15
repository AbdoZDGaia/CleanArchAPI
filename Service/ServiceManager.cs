using Contracts;
using Service.Contracts;

namespace Service
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IRestaurantService> _restaurantService;
        private readonly Lazy<ICustomerService> _customerService;

        public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager loggerManager)
        {
            _restaurantService = new Lazy<IRestaurantService>(
                () => new RestaurantService(repositoryManager, loggerManager));
            _customerService = new Lazy<ICustomerService>(
                () => new CustomerService(repositoryManager, loggerManager));
        }
        
        public IRestaurantService RestaurantService => _restaurantService.Value;

        public ICustomerService CustomerService => _customerService.Value;
    }
}
