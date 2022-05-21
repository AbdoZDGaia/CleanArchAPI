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

        public async Task<RestaurantDto> CreateRestaurantAsync(RestaurantForCreationDto restaurant)
        {
            var restaurantEntity = _mapper.Map<Restaurant>(restaurant);

            _repositoryManager.Restaurant.CreateRestaurant(restaurantEntity);
            await _repositoryManager.SaveAsync();

            var restaurantToReturn = _mapper.Map<RestaurantDto>(restaurantEntity);

            return restaurantToReturn;
        }

        public async Task<(IEnumerable<RestaurantDto> restaurants, string ids)> CreateRestaurantsAsync
            (IEnumerable<RestaurantForCreationDto> restaurantCollection)
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

            await _repositoryManager.SaveAsync();

            var restaurantsToReturn = _mapper.Map<IEnumerable<RestaurantDto>>(restaurantEntities);
            var ids = string.Join(",", restaurantsToReturn.Select(r => r.Id));
            return (restaurantsToReturn, ids);
        }

        public async Task DeleteRestaurantAsync(Guid restaurantId, bool trackChanges)
        {
            var restaurant = await GetRestaurantAndCheckIfItExists(restaurantId, trackChanges);

            _repositoryManager.Restaurant.DeleteRestaurant(restaurant);
            await _repositoryManager.SaveAsync();
        }

        public async Task<IEnumerable<RestaurantDto>> GetAllRestaurantsAsync(bool trackChanges)
        {
            var restaurants = await _repositoryManager.Restaurant.GetAllRestaurantsAsync(trackChanges);
            var restaurantsDto = _mapper.Map<IEnumerable<RestaurantDto>>(restaurants);
            return restaurantsDto;
        }

        public async Task<RestaurantDto> GetRestaurantByIdAsync(Guid id, bool trackChanges)
        {
            Restaurant? restaurant = await GetRestaurantAndCheckIfItExists(id, trackChanges);

            var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);
            return restaurantDto;
        }

        private async Task<Restaurant> GetRestaurantAndCheckIfItExists(Guid id, bool trackChanges)
        {
            var restaurant = await _repositoryManager.Restaurant.GetRestaurantAsync(id, trackChanges);
            if (restaurant is null)
                throw new RestaurantNotFoundException(id);
            return restaurant;
        }

        public async Task<IEnumerable<RestaurantDto>> GetRestaurantsByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
        {
            if (ids is null)
                throw new IdParametersBadRequestException();

            var restaurantEntities = await _repositoryManager.Restaurant.GetRestaurantsByIdsAsync(ids, trackChanges);
            if (ids.Count() != restaurantEntities.Count())
                throw new CollectionByIdsBadRequestException();

            var restaurantsToReturn = _mapper.Map<IEnumerable<RestaurantDto>>(restaurantEntities);
            return restaurantsToReturn;
        }

        public async Task UpdateRestaurantAsync(Guid restaurantId, RestaurantForUpdateDto restaurant, bool trackChanges)
        {
            var restaurantEntity = await GetRestaurantAndCheckIfItExists(restaurantId, trackChanges);

            _mapper.Map(restaurant, restaurantEntity);
            await _repositoryManager.SaveAsync();
        }
    }
}
