using Microsoft.AspNetCore.Mvc;
using Presentation.ModelBinders;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/restaurants")]
    public class RestaurantsController : ControllerBase
    {
        private readonly IServiceManager _service;

        public RestaurantsController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetRestaurants()
        {
            var restaurants = await _service.RestaurantService.GetAllRestaurantsAsync(trackChanges: false);
            return Ok(restaurants);
        }

        [HttpGet("{id:guid}", Name = "RestaurantById")]
        public async Task<IActionResult> GetRestaurant(Guid id)
        {
            var restaurant = await _service.RestaurantService.GetRestaurantByIdAsync(id, trackChanges: false);
            return Ok(restaurant);
        }

        [HttpGet("collection/({ids})", Name = "RestaurantCollection")]
        public async Task<IActionResult> GetRestaurantCollection(
            [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            var restaurants = await _service.RestaurantService.GetRestaurantsByIdsAsync(ids, trackChanges: false);
            return Ok(restaurants);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRestaurant([FromBody] RestaurantForCreationDto restaurant)
        {
            if (restaurant is null)
            {
                return BadRequest("RestaurantForCreationDto object is null");
            }

            var createdRestaurant = await _service.RestaurantService.CreateRestaurantAsync(restaurant);
            return CreatedAtRoute("RestaurantById", new { id = createdRestaurant.Id }, createdRestaurant);
        }

        [HttpPost("collection")]
        public async Task<IActionResult> CreateRestaurantCollection([FromBody] IEnumerable<RestaurantForCreationDto> restaurants)
        {
            var result = await _service.RestaurantService.CreateRestaurantsAsync(restaurants);

            return CreatedAtRoute("RestaurantCollection", new { result.ids }, result.restaurants);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteRestaurant(Guid id)
        {
            await _service.RestaurantService.DeleteRestaurantAsync(id, trackChanges: false);
            return NoContent();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateRestaurant(Guid id, [FromBody] RestaurantForUpdateDto restaurant)
        {
            if (restaurant is null)
            {
                return BadRequest("RestaurantForUpdateDto object is null");
            }

            await _service.RestaurantService.UpdateRestaurantAsync(id, restaurant, trackChanges: true);
            return NoContent();
        }
    }
}
