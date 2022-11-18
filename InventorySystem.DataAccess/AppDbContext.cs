using InventorySystem.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.DataAccess
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts):base(opts)
        {

        }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Sucursal> Sucursales { get; set; }
        public DbSet<Inventario> Inventario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>().HasKey(x => x.Id).HasName("ProductoId");
            modelBuilder.Entity<Producto>().Property(x => x.Id).IsRequired(true);

            modelBuilder.Entity<Sucursal>().HasKey(x => x.Id).HasName("SucursalId");
            modelBuilder.Entity<Sucursal>().Property(x => x.Id).IsRequired(true);

            modelBuilder.Entity<Inventario>().HasKey(x => x.Id).HasName("InventarioId");
            modelBuilder.Entity<Inventario>().Property(x => x.Id).IsRequired(true);

            modelBuilder.Entity<Inventario>().HasOne(x => x.Producto).WithMany(x=>x.Inventario);
            modelBuilder.Entity<Inventario>().HasOne(x => x.Sucursal).WithMany(x=>x.Inventario);


            base.OnModelCreating(modelBuilder); 
        }
    }
}