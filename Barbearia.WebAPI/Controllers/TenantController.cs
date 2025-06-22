using Barbearia.Application.DTOs;
using Barbearia.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Barbearia.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TenantController : ControllerBase
{
    private readonly ITenantService _tenantService;

    public TenantController(ITenantService tenantService)
    {
        _tenantService = tenantService;
    }

    [HttpPost("criar")]
    public IActionResult CriarTenant([FromBody] CreateTenantRequest request)
    {
        var result = _tenantService.CriarTenant(request);
        return Ok(result);
    }

    [HttpGet("config")]
    public IActionResult ObterConfiguracoes([FromQuery] Guid tenantId)
    {
        var tenant = _tenantService.ObterTenant(tenantId);
        if (tenant == null)
            return NotFound();

      

        return Ok(tenant);
    }
}