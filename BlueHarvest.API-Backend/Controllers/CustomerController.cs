using BlueHarvestAPI;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

namespace BlueHarvest.API.Controllers
{
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("{customerId}")]
        [ProducesResponseType(typeof(Customer), 200)]
        public IActionResult GetCustomerInfo(int customerId)
        {
            Customer customerInfo = _customerService.GetCustomerInfo(customerId);
            if (customerInfo == null) return NotFound();
            return Ok(customerInfo);
        }

        [HttpPost]
        public IActionResult CreateDummyUsers()
        {
            _customerService.CreateDummyCustomers();
            return Ok();
        }
    }
}
