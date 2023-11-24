using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GlobalSolution.Entity;

namespace GlobalSolution.Data
{
    public class GlobalSolutionContext : DbContext
    {
        public GlobalSolutionContext (DbContextOptions<GlobalSolutionContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuario { get; set; } = default!;

        public DbSet<Paciente>? Paciente { get; set; }

        public DbSet<Entity.Monitor>? Monitor { get; set; }

        public DbSet<Moderador>? Moderador { get; set; }

        public DbSet<Instituicao>? Instituicao { get; set; }

        public DbSet<ContatoEmergencia>? ContatoEmergencia { get; set; }

        public DbSet<MonitorPaciente>? MonitorPaciente { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<MonitorPaciente>()
                .HasKey(mp => new { mp.MonitorId, mp.PacienteId });

            modelBuilder.Entity<MonitorPaciente>()
                .HasOne(mp => mp.Monitor)
                .WithMany(m => m.MonitorPacientes)
                .HasForeignKey(mp => mp.MonitorId);

            modelBuilder.Entity<MonitorPaciente>()
                .HasOne(mp => mp.Paciente)
                .WithMany(p => p.MonitorPacientes)
                .HasForeignKey(mp => mp.PacienteId);
        }
    }
}
