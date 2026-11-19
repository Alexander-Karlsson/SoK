using Bookings.Application.Bookings.DTOs.Request;
using Bookings.Application.Bookings.Mappings;
using Bookings.Application.Bookings.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bookings.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BusinessServicesController(BusinessAppService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBusinessServiceRequest request)
    {
        try
        {
            var created = await service.CreateBusinessServiceAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created.ToResponse());
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var found = await service.GetByIdAsync(id);

        if (found is null)
            return NotFound();

        return Ok(found.ToResponse());
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var services = await service.GetAllAsync();
        return Ok(services.Select(s => s.ToResponse()));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateBusinessServiceRequest request)
    {
        try
        {
            return await service.UpdateBusinessServiceAsync(id, request) ? NoContent() : NotFound();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            return await service.DeleteBusinessServiceAsync(id) ? NoContent() : NotFound();
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }
}