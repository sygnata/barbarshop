using Barbearia.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barbearia.Domain.Entities
{
    public class HorarioDisponivel
    {
        public Guid Id { get; set; }
        public BarbeiroId BarbeiroId { get; set; }
        public int DiaSemana { get; set; } // 0=Domingo, 6=Sábado
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFim { get; set; }
        public bool Ativo { get; private set; } = true;

        public void Inativar()
        {
            Ativo = false;
        }

        public void Ativar()
        {
            Ativo = true;
        }
    }
}
