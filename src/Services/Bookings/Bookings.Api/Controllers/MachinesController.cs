using Bookings.Application.Bookings.DTOs.Request;
using Bookings.Application.Bookings.Services;
using Bookings.Application.Bookings.Mappings;
using Microsoft.AspNetCore.Mvc;

namespace Bookings.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MachinesController(MachineAppService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMachineRequest request)
    {
        try
        {
            var machine = await service.CreateMachineAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = machine.Id }, machine.ToResponse());
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var machine = await service.GetByIdAsync(id);

        if (machine is null)
            return NotFound();

        return Ok(machine.ToResponse());
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var machines = await service.GetAllAsync();
        return Ok(machines.Select(m => m.ToResponse()));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateMachineRequest request)
    {
        try
        {
            return await service.UpdateMachineAsync(id, request) ? NoContent() : NotFound();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            return await service.DeleteMachineAsync(id) ? NoContent() : NotFound();
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }
}
