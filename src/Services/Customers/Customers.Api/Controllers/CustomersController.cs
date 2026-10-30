using Customers.Application.Customers.DTOs;
using Customers.Application.Customers.Services;
using Microsoft.AspNetCore.Mvc;

namespace Customers.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController(CustomerService service) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerRequest request)
        {
            var customer = await service.CreateCustomerAsync(request);

            return Ok(customer);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var customer = await service.GetByIdAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }
    }
}
