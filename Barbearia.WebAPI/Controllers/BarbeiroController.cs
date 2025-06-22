using Barbearia.Application.DTOs;
using Barbearia.Application.DTOs.Barbeiro;
using Barbearia.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Barbearia.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BarbeiroController : ControllerBase
{
    private readonly IBarbeiroService _barbeiroService;

    public BarbeiroController(IBarbeiroService barbeiroService)
    {
        _barbeiroService = barbeiroService;
    }

    [HttpPost]
    [Authorize]
    public IActionResult AdicionarBarbeiro([FromBody] BarbeiroRequest request)
    {
        var tenantId = Guid.Parse(User.Claims.First(c => c.Type == "tenant_id").Value);
        var response = _barbeiroService.AdicionarBarbeiro(tenantId, request);
        return Ok(response);
    }

    [HttpGet]
    [Authorize]
    public IActionResult ListarBarbeiros()
    {
        var tenantId = Guid.Parse(User.Claims.First(c => c.Type == "tenant_id").Value);
        var response = _barbeiroService.ListarBarbeiros(tenantId);
        return Ok(response);
    }

    [HttpPut("{barbeiroId}")]
    [Authorize]
    public IActionResult Atualizar(Guid barbeiroId, [FromQuery] Guid tenantId, [FromBody] BarbeiroRequest request)
    {
        _barbeiroService.AtualizarBarbeiro(tenantId, barbeiroId, request);
        return NoContent();
    }

    [HttpDelete("{barbeiroId}/inativar")]
    [Authorize]
    public IActionResult Remover(Guid barbeiroId, [FromQuery] Guid tenantId)
    {
        _barbeiroService.Inativar(barbeiroId, tenantId);
        return NoContent();
    }

    [HttpPatch("{barbeiroId}/ativar")]
    [Authorize]
    public IActionResult Ativar(Guid barbeiroId, [FromQuery] Guid tenantId)
    {
        _barbeiroService.Ativar(barbeiroId, tenantId);
        return NoContent();
    }
}
