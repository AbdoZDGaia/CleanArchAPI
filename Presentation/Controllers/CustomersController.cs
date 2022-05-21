using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Presentation.ActionFilters;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/restaurants/{restaurantId}/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly IServiceManager _service;

        public CustomersController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomersForRestaurant(Guid restaurantId,
            [FromQuery] CustomerParameters customerParameters)
        {
            var pagedResult = await _service.CustomerService.GetAllCustomersAsync(restaurantId, customerParameters, trackChanges: false);
            Response.Headers.Add("X-Pagination",
                JsonConvert.SerializeObject(pagedResult.metadata!));
            return Ok(pagedResult.customers);
        }

        [HttpGet("{id:guid}", Name = "GetCustomerForRestaurant")]
        public async Task<IActionResult> GetCustomerForRestaurant(Guid restaurantId, Guid id)
        {
            var customer = await _service.CustomerService.GetCustomerAsync(restaurantId, id, trackChanges: false);
            return Ok(customer);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateCustomerForRestaurant(Guid restaurantId, [FromBody] CustomerForCreationDto customerDto)
        {
            var customerToReturn = await _service.CustomerService.CreateCustomerForRestaurantAsync(restaurantId, customerDto, trackChanges: false);

            return CreatedAtRoute("GetCustomerForRestaurant", new { restaurantId = restaurantId, id = customerToReturn.Id }, customerToReturn);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCustomerForRestaurant(Guid restaurantId, Guid id)
        {
            await _service.CustomerService.DeleteCustomerForRestaurantAsync(restaurantId, id, trackChanges: false);
            return NoContent();
        }

        [HttpPut("{id:guid}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateCustomerForRestaurant(Guid restaurantId, Guid id, [FromBody] CustomerForUpdateDto customerDto)
        {
            await _service.CustomerService.UpdateCustomerForRestaurantAsync(restaurantId, id, customerDto, restTrackChanges: false, custTrackChanges: true);
            return NoContent();
        }

        [HttpPatch("{id:guid}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> PartiallyUpdateCustomerForRestaurant(Guid restaurantId, Guid id,
            [FromBody] JsonPatchDocument<CustomerForUpdateDto> patchDoc)
        {
            var result = await _service.CustomerService.GetCustomerForPatchAsync(restaurantId, id, false, true);

            patchDoc.ApplyTo(result.customerToPatch);

            await _service.CustomerService.SaveChangesForPatchAsync(result.customerToPatch, result.customerEntity);

            return NoContent();
        }
    }
}
