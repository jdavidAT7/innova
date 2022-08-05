using Microsoft.EntityFrameworkCore;
using back_end.Entidades;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace back_end
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<CContratos>()
                .HasKey(x => new { x.ContratoId, x.ClienteId });

            modelBuilder.Entity<ClientesAdendums>()
                .HasKey(x => new { x.ClienteId, x.AdendumId });

            modelBuilder.Entity<ClientesServicios>()
                .HasKey(x => new { x.ClienteId, x.ServicioId });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ClienteAdendum> Adendums { get; set; }
        public DbSet<ClienteContrato> Contratos { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<CContratos> CContratos { get; set; }
        public DbSet<ClientesAdendums> ClientesAdendums { get; set; }
        public DbSet<ClientesServicios> ClientesServicios { get; set; }


        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<ProveedorAdendum> Adendumsp { get; set; }
        public DbSet<ProveedorContrato> Contratosp { get; set; }


    }
}
