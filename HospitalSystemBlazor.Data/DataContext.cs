using HospitalSystemBlazor.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystemBlazor.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Doctor> Doctores { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Horario> Horarios { get; set; }
        public DbSet<Especialidad> Especialidades { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<DiaTrabajo> DiaTrabajos { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected  override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Horario>()
                .Property(h => h.HoraInicio)
                .HasConversion(
                    v => v.ToString("HH:mm:ss"),
                    v => TimeOnly.Parse(v)
                );

            modelBuilder.Entity<Horario>()
                .Property(h => h.HoraFinal)
                .HasConversion(
                    v => v.ToString("HH:mm:ss"),
                    v => TimeOnly.Parse(v)
                );

        }
    }
}
