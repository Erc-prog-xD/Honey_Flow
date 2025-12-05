
using BackendApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Apiario> Apiarios {get; set;}
        public DbSet<Colmeia> Colmeias { get; set; }
        public DbSet<Producao> Producoes { get; set; }
        public DbSet<PerdaMel> PerdaMel { get; set; }
        public DbSet<VendaMel> VendaMel { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 🔹 Diz ao EF que Endereco faz parte de Agendamento
            modelBuilder.Entity<Apiario>().OwnsOne(a => a.Localizacao);

        }

    }
}