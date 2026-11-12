using Bookings.Application.Bookings.DTOs.Request;
using Bookings.Application.Bookings.Mappings;
using Bookings.Application.Bookings.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bookings.Api.Controllers;

[ApiController]
[Route("api/controller")]
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
    public async Task<IActionResult> GetAll()
    {
        var bookings = await service.GetAllAsync();
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
            return BadRequest(ex.Message);
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
}
