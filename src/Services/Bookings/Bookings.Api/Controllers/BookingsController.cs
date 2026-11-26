using Bookings.Application.Bookings.DTOs.Request;
using Bookings.Application.Bookings.Mappings;
using Bookings.Application.Bookings.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bookings.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingsController(BookingAppService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBookingRequest request)
    {
        try
        {
            var details = await service.CreateBookingAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = details.Booking.Id }, details.ToResponse());
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
        var details = await service.GetByIdAsync(id);

        if (details is null)
            return NotFound();

        return Ok(details.ToResponse());
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] Guid? customerId)
    {
        var bookings = await service.GetAllAsync(customerId);
        return Ok(bookings.Select(b => b.ToSummary()));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateBookingRequest request)
    {
        try
        {
            return await service.UpdateBookingAsync(id, request)
                ? NoContent()
                : NotFound();
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

    [HttpPost("{id:guid}/cancel")]
    public async Task<IActionResult> Cancel(Guid id)
    {
        try
        {
            return await service.CancelBookingAsync(id)
                ? NoContent()
                : NotFound();
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
            return await service.DeleteBookingAsync(id) ? NoContent() : NotFound();
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }

    [HttpPost("{id:guid}/complete")]
    public async Task<IActionResult> Complete(Guid id, [FromBody] CompleteBookingRequest request)
    {
        try
        {
            return await service.CompleteBookingAsync(id, request) ? NoContent() : NotFound();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
        catch (HttpRequestException)
        {
            return StatusCode(503, "Kunde inte nå Invoices.");
        }
    }

    [HttpPost("{id:guid}/services/{serviceId:guid}/take")]
    public async Task<IActionResult> TakeService(Guid id, Guid serviceId, [FromBody] TakeServiceRequest request)
    {
        try
        {
            return await service.TakeServiceAsync(id, serviceId, request) ? NoContent() : NotFound();
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

    [HttpGet("open")]
    public async Task<IActionResult> GetOpenAssignments([FromQuery] string? title)
    => Ok(await service.GetOpenAssignmentsAsync(title));

    [HttpGet("assignments")]
    public async Task<IActionResult> GetAssignments([FromQuery] Guid employeeId)
    {
        try
        {
            return Ok(await service.GetAssignmentsForEmployeeAsync(employeeId));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
