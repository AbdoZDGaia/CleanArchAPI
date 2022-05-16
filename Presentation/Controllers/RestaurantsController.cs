using Microsoft.AspNetCore.Mvc;
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
    }
}
