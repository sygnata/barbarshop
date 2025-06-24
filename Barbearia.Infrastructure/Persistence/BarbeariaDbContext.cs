using Barbearia.Domain.Entities;
using Barbearia.Domain.ValueObjects;
using Barbearia.Infrastructure.Persistence.Configurations;
using Barbearia.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TenantConfiguration).Assembly);

            modelBuilder.ApplyValueObjectConversions();


            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties().Where(p => p.ClrType == typeof(DateTime)))
                {
                    property.SetValueConverter(new ValueConverter<DateTime, DateTime>(
                        v => v.ToUniversalTime(),
                        v => DateTime.SpecifyKind(v, DateTimeKind.Utc)));
                }
            }


            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>()
                .HasIndex(u => new { u.TenantId, u.Email })
                .IsUnique();

            modelBuilder.Entity<Agendamento>()
                .Property(a => a.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Agendamento>()
                 .Property(a => a.DataHoraAgendada)
                 .HasColumnType("timestamp without time zone");
        }
    }

}

