using Contracts;
using Service.Contracts;

namespace Service
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _loggerManager;

        public RestaurantService(IRepositoryManager repositoryManager,ILoggerManager loggerManager)
        {
            _repositoryManager = repositoryManager;
            _loggerManager = loggerManager;
        }
    }
}
