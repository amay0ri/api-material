using ApiMaterial.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiMaterial.Data
{
    public class MaterialDbContext : DbContext
    {
        public MaterialDbContext(DbContextOptions<MaterialDbContext> options) : base(options)
        {
        }
        
        public DbSet<Colecao> Colecoes { get; set; }
        public DbSet<Material> Materiais { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Colecao>()
                .HasKey(p => p.Id);
            modelBuilder.Entity<Material>()
                .HasKey(p => p.Id);
            modelBuilder.Entity<Material>()
                .ToTable("Materiais");
            modelBuilder.Entity<Colecao>()
                .ToTable("Colecoes");

            base.OnModelCreating(modelBuilder);
        }
    }
}
