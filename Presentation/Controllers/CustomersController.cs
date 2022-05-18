using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
