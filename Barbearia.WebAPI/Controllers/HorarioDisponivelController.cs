using Barbearia.Application.DTOs.HorarioDisponivel;
using Barbearia.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Barbearia.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HorarioDisponivelController : ControllerBase
    {
        private readonly IHorarioDisponivelService _horarioService;

        public HorarioDisponivelController(IHorarioDisponivelService horarioService)
        {
            _horarioService = horarioService;
        }

        [HttpPost]
        [Authorize]
        public IActionResult Adicionar([FromBody] HorarioDisponivelRequest request)
        {
            var tenantId = Guid.Parse(User.Claims.First(c => c.Type == "tenant_id").Value);
            var response = _horarioService.Adicionar(tenantId, request);
            return Ok(response);
        }

        [HttpGet("barbeiro/{barbeiroId}")]
        [Authorize]
        public IActionResult ListarPorBarbeiro(Guid barbeiroId)
        {
            var tenantId = Guid.Parse(User.Claims.First(c => c.Type == "tenant_id").Value);
            var response = _horarioService.ListarPorBarbeiro(tenantId, barbeiroId);
            return Ok(response);
        }
    }
}
