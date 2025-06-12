using Barbearia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barbearia.Infrastructure.Persistence
{

    public class BarbeariaDbContext : DbContext
    {
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Servico> Servicos { get; set; }
        public DbSet<Barbeiro> Barbeiros { get; set; }
        public DbSet<HorarioDisponivel> HorariosDisponiveis { get; set; }
        public DbSet<Agendamento> Agendamentos { get; set; }

        public BarbeariaDbContext(DbContextOptions<BarbeariaDbContext> options) : base(options) { }
    }
}
