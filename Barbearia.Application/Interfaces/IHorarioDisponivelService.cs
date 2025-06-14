using Barbearia.Application.DTOs.HorarioDisponivel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barbearia.Application.Interfaces
{
    public interface IHorarioDisponivelService
    {
        HorarioDisponivelResponse Adicionar(Guid tenantId, HorarioDisponivelRequest request);
        IEnumerable<HorarioDisponivelResponse> ListarPorBarbeiro(Guid tenantId, Guid barbeiroId);
    }
}
