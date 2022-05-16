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
            try
            {
                var restaurants = _service.RestaurantService.GetAllRestaurants(trackChanges: false);
                return Ok(restaurants);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
