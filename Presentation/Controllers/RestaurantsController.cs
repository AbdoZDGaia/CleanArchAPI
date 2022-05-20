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
        public IActionResult GetRestaurants()
        {
            var restaurants = _service.RestaurantService.GetAllRestaurants(trackChanges: false);
            return Ok(restaurants);
        }

        [HttpGet("{id:guid}", Name = "RestaurantById")]
        public IActionResult GetRestaurant(Guid id)
        {
            var restaurant = _service.RestaurantService.GetRestaurantById(id, trackChanges: false);
            return Ok(restaurant);
        }

        [HttpGet("collection/({ids})", Name = "RestaurantCollection")]
        public IActionResult GetRestaurantCollection(
            [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            var restaurants = _service.RestaurantService.GetRestaurantsByIds(ids, trackChanges: false);
            return Ok(restaurants);
        }

        [HttpPost]
        public IActionResult CreateRestaurant([FromBody] RestaurantForCreationDto restaurant)
        {
            if (restaurant is null)
            {
                return BadRequest("RestaurantForCreationDto object is null");
            }

            var createdRestaurant = _service.RestaurantService.CreateRestaurant(restaurant);
            return CreatedAtRoute("RestaurantById", new { id = createdRestaurant.Id }, createdRestaurant);
        }

        [HttpPost("collection")]
        public IActionResult CreateRestaurantCollection([FromBody] IEnumerable<RestaurantForCreationDto> restaurants)
        {
            var result = _service.RestaurantService.CreateRestaurants(restaurants);

            return CreatedAtRoute("RestaurantCollection", new { result.ids }, result.restaurants);
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteRestaurant(Guid id)
        {
            _service.RestaurantService.DeleteRestaurant(id, trackChanges: false);
            return NoContent();
        }
    }
}
