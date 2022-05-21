using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

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
        public IActionResult GetCustomersForRestaurant(Guid restaurantId)
        {
            var customers = _service.CustomerService.GetAllCustomers(restaurantId, trackChanges: false);
            return Ok(customers);
        }

        [HttpGet("{id:guid}", Name = "GetCustomerForRestaurant")]
        public IActionResult GetCustomerForRestaurant(Guid restaurantId, Guid id)
        {
            var customer = _service.CustomerService.GetCustomer(restaurantId, id, trackChanges: false);
            return Ok(customer);
        }

        [HttpPost]
        public IActionResult CreateCustomerForRestaurant(Guid restaurantId, [FromBody] CustomerForCreationDto customerDto)
        {
            if (customerDto == null)
            {
                return BadRequest("CustomerForCreationDto is null");
            }

            var customerToReturn = _service.CustomerService.CreateCustomerForRestaurant(restaurantId, customerDto, trackChanges: false);

            return CreatedAtRoute("GetCustomerForRestaurant", new { restaurantId = restaurantId, id = customerToReturn.Id }, customerToReturn);
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteCustomerForRestaurant(Guid restaurantId, Guid id)
        {
            _service.CustomerService.DeleteCustomerForRestaurant(restaurantId, id, trackChanges: false);
            return NoContent();
        }

        [HttpPut("{id:guid}")]
        public IActionResult UpdateCustomerForRestaurant(Guid restaurantId, Guid id, [FromBody] CustomerForUpdateDto customerDto)
        {
            if (customerDto == null)
            {
                return BadRequest("CustomerForUpdateDto is null");
            }

            _service.CustomerService.UpdateCustomerForRestaurant(restaurantId, id, customerDto, restTrackChanges: false, custTrackChanges: true);

            return NoContent();
        }

        [HttpPatch("{id:guid}")]
        public IActionResult PartiallyUpdateCustomerForRestaurant(Guid restaurantId, Guid id,
            [FromBody] JsonPatchDocument<CustomerForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest("JsonPatchDocument is null");
            }

            var result = _service.CustomerService.GetCustomerForPatch(restaurantId, id, false, true);

            patchDoc.ApplyTo(result.customerToPatch);

            _service.CustomerService.SaveChangesForPatch(result.customerToPatch, result.customerEntity);

            return NoContent();
        }
    }
}
