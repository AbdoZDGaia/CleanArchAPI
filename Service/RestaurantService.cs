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

        public RestaurantDto CreateRestaurant(RestaurantForCreationDto restaurant)
        {
            var restaurantEntity = _mapper.Map<Restaurant>(restaurant);

            _repositoryManager.Restaurant.CreateRestaurant(restaurantEntity);
            _repositoryManager.Save();

            var restaurantToReturn = _mapper.Map<RestaurantDto>(restaurantEntity);

            return restaurantToReturn;
        }

        public (IEnumerable<RestaurantDto> restaurants, string ids) CreateRestaurants(IEnumerable<RestaurantForCreationDto> restaurantCollection)
        {
            if (restaurantCollection is null)
            {
                throw new RestaurantCollectionBadRequest();
            }

            var restaurantEntities = _mapper.Map<IEnumerable<Restaurant>>(restaurantCollection);
            foreach (var restaurant in restaurantEntities)
            {
                _repositoryManager.Restaurant.CreateRestaurant(restaurant);
            }

            _repositoryManager.Save();

            var restaurantsToReturn = _mapper.Map<IEnumerable<RestaurantDto>>(restaurantEntities);
            var ids = string.Join(",", restaurantsToReturn.Select(r => r.Id));
            return (restaurantsToReturn, ids);
        }

        public void DeleteRestaurant(Guid restaurantId, bool trackChanges)
        {
            var restaurant = _repositoryManager.Restaurant.GetRestaurant(restaurantId, trackChanges);
            if (restaurant is null)
            {
                throw new RestaurantNotFoundException(restaurantId);
            }

            _repositoryManager.Restaurant.DeleteRestaurant(restaurant);
            _repositoryManager.Save();
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

        public IEnumerable<RestaurantDto> GetRestaurantsByIds(IEnumerable<Guid> ids, bool trackChanges)
        {
            if (ids is null)
                throw new IdParametersBadRequestException();

            var restaurantEntities = _repositoryManager.Restaurant.GetRestaurantsByIds(ids, trackChanges);
            if (ids.Count() != restaurantEntities.Count())
                throw new CollectionByIdsBadRequestException();

            var restaurantsToReturn = _mapper.Map<IEnumerable<RestaurantDto>>(restaurantEntities);
            return restaurantsToReturn;
        }

        public void UpdateRestaurant(Guid restaurantId, RestaurantForUpdateDto restaurant, bool trackChanges)
        {
            var restaurantEntity = _repositoryManager.Restaurant.GetRestaurant(restaurantId, trackChanges);
            if (restaurantEntity is null)
                throw new RestaurantNotFoundException(restaurantId);

            _mapper.Map(restaurant, restaurantEntity);
            _repositoryManager.Save();
        }
    }
}
