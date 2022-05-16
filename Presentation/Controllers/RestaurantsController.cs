using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

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

        [HttpGet("{id:guid}")]
        public IActionResult GetRestaurant(Guid id)
        {
            var restaurant = _service.RestaurantService.GetRestaurantById(id, trackChanges: false);
            return Ok(restaurant);
        }
    }
}
