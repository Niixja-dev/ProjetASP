using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ProjetASP.Models
{
    public class GRHContext : IdentityDbContext<Utilisateur>
    {
        public GRHContext(DbContextOptions<GRHContext> options) : base(options)
        {
        }

        public DbSet<Employe> Employes { get; set; }
        public DbSet<Conge> Conges { get; set; }
        public DbSet<Paie> Paies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuration Employe
            modelBuilder.Entity<Employe>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Matricule).IsUnique();
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.SalaireBase).HasPrecision(18, 2);
            });

            // Configuration Conge
            modelBuilder.Entity<Conge>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.HasOne(c => c.Employe)
                      .WithMany(e => e.Conges)
                      .HasForeignKey(c => c.EmployeId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuration Paie
            modelBuilder.Entity<Paie>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.HasOne(p => p.Employe)
                      .WithMany(e => e.Paies)
                      .HasForeignKey(p => p.EmployeId)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.Property(p => p.SalaireBase).HasPrecision(18, 2);
                entity.Property(p => p.Primes).HasPrecision(18, 2);
                entity.Property(p => p.HeuresSupplementaires).HasPrecision(18, 2);
                entity.Property(p => p.Retenues).HasPrecision(18, 2);
                entity.Property(p => p.CotisationsSociales).HasPrecision(18, 2);
                entity.Property(p => p.ImpotRevenu).HasPrecision(18, 2);
                entity.HasIndex(p => new { p.EmployeId, p.Mois, p.Annee }).IsUnique();
            });
        }
    }
}
