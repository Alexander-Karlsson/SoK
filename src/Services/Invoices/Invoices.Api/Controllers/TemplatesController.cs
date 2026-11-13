using Invoices.Application.Dtos;
using Invoices.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Invoices.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TemplatesController(TemplateService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateTemplate([FromBody] CreateTemplateRequest request)
    {
        var template = await service.CreateTemplateAsync(request);
        return Ok(template);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var templates = await service.GetAllAsync();
        return Ok(templates);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var template = await service.GetByIdAsync(id);
        if (template == null)
        {
            return NotFound();
        }
        return Ok(template);
    }
}
