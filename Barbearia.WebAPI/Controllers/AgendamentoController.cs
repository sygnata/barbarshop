using Barbearia.Application.DTOs.Agendamento;
using Barbearia.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Barbearia.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgendamentoController : ControllerBase
    {
        private readonly IAgendamentoService _agendamentoService;

        public AgendamentoController(IAgendamentoService agendamentoService)
        {
            _agendamentoService = agendamentoService;
        }

        [HttpPost]
        [Authorize]
        public IActionResult Agendar([FromBody] AgendamentoRequest request)
        {
            var tenantId = Guid.Parse(User.Claims.First(c => c.Type == "tenant_id").Value);
            var response = _agendamentoService.Agendar(tenantId, request);
            return Ok(response);
        }

        [HttpGet]
        [Authorize]
        public IActionResult ListarAgendamentos()
        {
            var tenantId = Guid.Parse(User.Claims.First(c => c.Type == "tenant_id").Value);
            var response = _agendamentoService.ListarAgendamentos(tenantId);
            return Ok(response);
        }
    }
}
