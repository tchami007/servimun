using Microsoft.EntityFrameworkCore;
using ServiMun.Models;

namespace ServiMun.Data
{

    public class TributoMunicipalContext : DbContext
    {
        public TributoMunicipalContext(DbContextOptions<TributoMunicipalContext> options) : base(options) { }
        public DbSet<TributoMunicipal> TributosMunicipales { get; set; }
        public DbSet<Contribuyente> Contribuyentes { get; set; }
        public DbSet<PadronBoleta> PadronBoletas { get;set; }
        public DbSet<PadronContribuyente> PadronContribuyentes { get; set; }

        // Servicios
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<ServicioCliente> ServicioClientes { get; set; }
        public DbSet<ServicioBoleta> ServicioBoletas { get; set; }

        // Movimientos
        public DbSet<Movimiento> Movimientos { get; set; }
        public DbSet<ComprobanteControl> ComprobantesControl { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //----------------------
            // Servicios Municipales
            //----------------------

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
            modelBuilder.Entity<PadronContribuyente>()
                .HasKey(pc => new { pc.IdContribuyente, pc.IdTributoMunicipal });

            // Configuracion relaciones PadronContributentes

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

            //----------------------
            // Servicios Municipales
            //----------------------

            // Configuración para Servicio
            modelBuilder.Entity<Servicio>(entity =>
            {
                entity.HasKey(e => e.IdServicio);

                entity.Property(e => e.NombreServicio)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Sintetico)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Estado)
                    .IsRequired();
            });

            // Configuración para Clientes (Contribuyente) con Servicio
            modelBuilder.Entity<ServicioCliente>()
            .HasKey(pc => new { pc.IdContribuyente, pc.IdServicio });

            // Configuracion relaciones ServicioCliente
            modelBuilder.Entity<ServicioCliente>()
                .HasOne(pc => pc.Contribuyente)
                .WithMany(c => c.ServicioClientes)
                .HasForeignKey(pc => pc.IdContribuyente);

            modelBuilder.Entity<ServicioCliente>()
                .HasOne(pc => pc.Servicio)
                .WithMany(t => t.ServicioClientes)
                .HasForeignKey(pc => pc.IdServicio);

            // Configuración para Servicio Boleta
            modelBuilder.Entity<ServicioBoleta>()
                .HasKey(pb => pb.IdBoletaServicio);

            modelBuilder.Entity<ServicioBoleta>()
                .HasOne(pb => pb.ServicioCliente)
                .WithMany(pc => pc.ServicioBoletas)
                .HasForeignKey(pb => pb.NumeroServicio)
                .HasPrincipalKey(pc => pc.NumeroServicio);

            //----------------------
            // Movimientos
            //----------------------

            // Configuración para Movimiento
            modelBuilder.Entity <Movimiento>(entity =>
            {
                entity.HasKey(e => e.IdMovimiento);
                entity.Property(e => e.FechaMovimiento)
                    .IsRequired();
                entity.Property(e => e.FechaReal)
                    .IsRequired();
                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasMaxLength(10);
                entity.Property(e => e.Numero)
                    .IsRequired();
                entity.Property(e => e.Periodo)
                    .IsRequired();
                entity.Property(e => e.Contrasiento)
                    .HasMaxLength(1);
            });

            modelBuilder.Entity<Movimiento>()
                .HasOne(m => m.Tributo)
                .WithMany(t => t.Movimientos) // Relación inversa
                .HasForeignKey(m => m.IdTributo);

            modelBuilder.Entity<Movimiento>()
                .HasOne(m => m.Servicio)
                .WithMany(s => s.Movimientos) // Relación inversa
                .HasForeignKey(m => m.IdServicio);

            // Configuración adicional de la entidad ComprobanteControl, si es necesario
            modelBuilder.Entity<ComprobanteControl>()
                        .HasKey(c => c.IdControl);

            base.OnModelCreating(modelBuilder);
        }
    }

}
