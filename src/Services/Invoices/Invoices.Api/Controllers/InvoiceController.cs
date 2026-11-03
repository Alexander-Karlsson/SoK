using Invoices.Application.Dtos;
using Invoices.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Invoices.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InvoiceController (InvoiceService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateInvoice([FromBody] CreateInvoiceRequest request)
    {
        var invoice = await service.CreateInvoiceAsync(request);

        return Ok(invoice);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetInvoice(Guid id)
    {
        var invoice = await service.GetInvoiceAsync(id);

        if (invoice == null)
        {
            return NotFound();
        }

        return Ok(invoice);
    }

}
