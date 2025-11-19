using Microsoft.EntityFrameworkCore;
using gestor_tareas_api.Models;

namespace gestor_tareas_api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }  
   

        public DbSet<Tarea> Tareas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Tarea>().ToTable("Tareas");

            modelBuilder.Entity<Tarea>().HasKey( t=> t.Id);

            modelBuilder.Entity<Tarea>()
                .Property(t => t.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Tarea>()
                .Property(t => t.Titulo)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Tarea>()
                .Property(t => t.Descripcion)
                .HasMaxLength(1000);
            
            modelBuilder.Entity<Tarea>()
                .Property(t => t.Prioridad)
                .IsRequired()
                .HasMaxLength(50);

        }

    }
}