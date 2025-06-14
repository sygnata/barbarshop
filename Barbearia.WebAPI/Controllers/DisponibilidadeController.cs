using Barbearia.Application.DTOs.Agendamento;
using Barbearia.Application.Interfaces;
using Barbearia.Infrastructure.Providers;
using Microsoft.AspNetCore.Mvc;

namespace Barbearia.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DisponibilidadeController : ControllerBase
    {
        private readonly IAgendamentoService _agendamentoService;
        private readonly ITenantProvider _tenantProvider;

        public DisponibilidadeController(IAgendamentoService agendamentoService, ITenantProvider tenantProvider)
        {
            _agendamentoService = agendamentoService;
            _tenantProvider = tenantProvider;
        }

        [HttpPost("listar")]
        public ActionResult<DisponibilidadeResponse> ListarDisponibilidade(DisponibilidadeRequest request)
        {
            var tenantId = _tenantProvider.ObterTenantId();
            var horarios = _agendamentoService.ListarDisponibilidade(tenantId, request.BarbeiroId, request.ServicoId, request.Data);

            return Ok(new DisponibilidadeResponse
            {
                HorariosDisponiveis = horarios
            });
        }
    }
}
