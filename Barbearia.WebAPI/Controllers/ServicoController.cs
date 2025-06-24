using Barbearia.Application.DTOs;
using Barbearia.Application.DTOs.Servico;
using Barbearia.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Barbearia.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServicoController : ControllerBase
{
    private readonly IServicoService _servicoService;

    public ServicoController(IServicoService servicoService)
    {
        _servicoService = servicoService;
    }

    [HttpPost]
    [Authorize]
    public IActionResult AdicionarServico([FromBody] ServicoRequest request)
    {
        var tenantId = Guid.Parse(User.Claims.First(c => c.Type == "tenant_id").Value);
        var response = _servicoService.AdicionarServico(tenantId, request);
        return Ok(response);
    }

    [HttpGet]
    [Authorize]
    public IActionResult ListarServicos()
    {
        var tenantId = Guid.Parse(User.Claims.First(c => c.Type == "tenant_id").Value);
        var response = _servicoService.ListarServicos(tenantId);
        return Ok(response);
    }

	[HttpPut("{servicoId}")]
	[Authorize]
	public IActionResult Atualizar(Guid servicoId, [FromQuery] Guid tenantId, [FromBody] ServicoRequest request)
	{
		_servicoService.AtualizarServico(servicoId, tenantId, request);
		return NoContent();
	}

    [HttpDelete("{servicoId}/inativar")]
    [Authorize]
    public IActionResult Remover(Guid servicoId, [FromQuery] Guid tenantId)
    {
        _servicoService.Inativar(servicoId, tenantId);
        return NoContent();
    }

    [HttpPatch("{servicoId}/ativar")]
    [Authorize]
    public IActionResult Ativar(Guid servicoId, [FromQuery] Guid tenantId)
    {
        _servicoService.Ativar(servicoId, tenantId);
        return NoContent();
    }

}
