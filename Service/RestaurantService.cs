using AutoMapper;
using Contracts;
using Entities;
using Entities.Exceptions;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public RestaurantService(IRepositoryManager repositoryManager
            , ILoggerManager loggerManager
            , IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _logger = loggerManager;
            _mapper = mapper;
        }

        public IEnumerable<RestaurantDto> GetAllRestaurants(bool trackChanges)
        {
            var restaurants = _repositoryManager.Restaurant.GetAllRestaurants(trackChanges);
            var restaurantsDto = _mapper.Map<IEnumerable<RestaurantDto>>(restaurants);
            return restaurantsDto;
        }

        public RestaurantDto GetRestaurantById(Guid id, bool trackChanges)
        {
            var restaurant = _repositoryManager.Restaurant.GetRestaurant(id, trackChanges);
            if (restaurant is null)
                throw new RestaurantNotFoundException(id);
            
            var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);
            return restaurantDto;
        }
    }
}
