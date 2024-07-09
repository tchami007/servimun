using Microsoft.EntityFrameworkCore;
using ServiMun.Models;

namespace ServiMun.Data
{

    public class TributoMunicipalContext : DbContext
    {
        public TributoMunicipalContext(DbContextOptions<TributoMunicipalContext> options) : base(options) { }

        public DbSet<TributoMunicipal> TributosMunicipales { get; set; }
        public DbSet<Contribuyente> Contribuyentes { get; set; }
        public DbSet<PadronContribuyente> PadronContribuyentes { get; set; }
        public DbSet<PadronBoleta> PadronBoletas { get;set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración para TributoMunicipal
            modelBuilder.Entity<TributoMunicipal>(entity =>
            {
                entity.HasKey(e => e.IdTributo);

                entity.Property(e => e.NombreTributo)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Sintetico)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Estado)
                    .IsRequired();
            });

            // Configuración para Contribuyente
            modelBuilder.Entity<Contribuyente>(entity =>
            {
                entity.HasKey(e => e.IdContribuyente);

                entity.HasIndex(e => e.NumeroDocumentoContribuyente)
                    .IsUnique();

                entity.Property(e => e.NumeroDocumentoContribuyente)
                    .IsRequired();

                entity.Property(e => e.ApellidoNombreContribuyente)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.DomicilioCalleContribuyente)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.DomicilioNumeroContribuyente)
                    .IsRequired();

                entity.Property(e => e.TelefonoContribuyente)
                    .HasMaxLength(15);

                entity.Property(e => e.SexoContribuyente)
                    .IsRequired()
                    .HasMaxLength(1);

                entity.Property(e => e.FechaNacimientoContribuyente)
                    .IsRequired();
            });

            // Configuración para PadronContribuyente
            // Configuración para PadronContribuyente
            modelBuilder.Entity<PadronContribuyente>()
                .HasKey(pc => new { pc.IdContribuyente, pc.IdTributoMunicipal });

            //modelBuilder.Entity<PadronContribuyente>()
            //    .HasAlternateKey(pc => pc.NumeroPadron);

            modelBuilder.Entity<PadronContribuyente>()
                .HasOne(pc => pc.Contribuyente)
                .WithMany(c => c.PadronContribuyentes)
                .HasForeignKey(pc => pc.IdContribuyente);

            modelBuilder.Entity<PadronContribuyente>()
                .HasOne(pc => pc.TributoMunicipal)
                .WithMany(t => t.PadronContribuyentes)
                .HasForeignKey(pc => pc.IdTributoMunicipal);


            // Configuración para PadronBoleta
            modelBuilder.Entity<PadronBoleta>()
                .HasKey(pb => pb.IdBoleta);

            modelBuilder.Entity<PadronBoleta>()
                .HasOne(pb => pb.PadronContribuyente)
                .WithMany(pc => pc.PadronBoletas)
                .HasForeignKey(pb => pb.NumeroPadron)
                .HasPrincipalKey(pc => pc.NumeroPadron);

            base.OnModelCreating(modelBuilder);

        }
    }

}
