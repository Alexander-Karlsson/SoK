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

            return CreatedAtAction("GetCustomerById",new { id = customer.Id }, customer);
        }

        
        [HttpGet("{id:guid}", Name = "GetCustomerById")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var customer = await service.GetByIdAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var customers = await service.GetAllAsync();

            return Ok(customers);
        }
        
        [HttpGet("search")]
        public async Task<IActionResult> SearchByNameAsync(
            [FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return BadRequest("Search query is required.");

            var customers = await service.SearchByNameAsync(query.Trim());

            return Ok(customers);
        }
        
        [HttpGet("number/{customerNumber}")]
        public async Task<IActionResult> GetByCustomerNumberAsync(
            string customerNumber)
        {
            var customer = await service.GetByCustomerNumberAsync(customerNumber);

            if (customer is null)
                return NotFound();

            return Ok(customer);
        }
        
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateCustomerAsync(Guid id, [FromBody] UpdateCustomerRequest request)
        {
            var customer = await service.UpdateAsync(id, request);

            if (customer is null)
                return NotFound();

            return Ok(customer);
        }
        
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCustomerAsync(Guid id)
        {
            var deleted = await service.DeleteAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
