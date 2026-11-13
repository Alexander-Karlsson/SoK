using Invoices.Application.Dtos;
using Invoices.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Invoices.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InvoicesController (InvoiceService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateInvoice([FromBody] CreateInvoiceRequest request)
    {
        try
        {
            var invoice = await service.CreateInvoiceAsync(request);

            return CreatedAtAction(nameof(GetById), new { id = invoice.Id }, invoice);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (HttpRequestException)
        {
            return StatusCode(503, "Kunde inte nå customer");
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var invoices = await service.GetAllAsync();

        return Ok(invoices);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var invoice = await service.GetByIdAsync(id);

        if (invoice == null)
        {
            return NotFound();
        }

        return Ok(invoice);
    }

    [HttpPut]
    [Route("{id:guid}/status")]
    public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] UpdateInvoiceStatusRequest request)
    {
        try
        {
            var invoice = await service.UpdateStatusAsync(id, request);

            if (invoice == null)
            {
                return NotFound();
            }

            return Ok(invoice);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

}
