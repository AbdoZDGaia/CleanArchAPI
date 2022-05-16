using Contracts;
using Entities;
using Service.Contracts;

namespace Service
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;

        public RestaurantService(IRepositoryManager repositoryManager, ILoggerManager loggerManager)
        {
            _repositoryManager = repositoryManager;
            _logger = loggerManager;
        }

        public IEnumerable<Restaurant> GetAllRestaurants(bool trackChanges)
        {
            try
            {
                return _repositoryManager.Restaurant.GetAllRestaurants(trackChanges);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the { nameof(GetAllRestaurants)} service method { ex}");
                throw;
            }
        }
    }
}
