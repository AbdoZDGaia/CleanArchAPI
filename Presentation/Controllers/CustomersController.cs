using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/restaurants/{restaurantId}/employees")]
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

        [HttpGet("{id:guid}")]
        public IActionResult GetCustomerForRestaurant(Guid restaurantId, Guid id)
        {
            var customer = _service.CustomerService.GetCustomer(restaurantId, id, trackChanges: false);
            return Ok(customer);
        }
    }
}
