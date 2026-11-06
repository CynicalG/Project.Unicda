using CRUD_PERSONAS.Models;
using System;

namespace CRUD_PERSONAS.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            Options = options;
        }

        public DbSet<Persona> Personas { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }
        public DbContextOptions<AppDbContext> Options { get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Mapear la propiedad 'Role' a la columna 'Role' en la base de datos
            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasColumnName("Role");

            // Configuración de decimales para evitar advertencias
            modelBuilder.Entity<Product>().Property(p => p.Price).HasPrecision(18, 2);
            modelBuilder.Entity<Invoice>().Property(i => i.SubTotal).HasPrecision(18, 2);
            modelBuilder.Entity<Invoice>().Property(i => i.ItbisAmount).HasPrecision(18, 2);
            modelBuilder.Entity<Invoice>().Property(i => i.TotalToPay).HasPrecision(18, 2);
            modelBuilder.Entity<Invoice>().Property(i => i.AmountPaid).HasPrecision(18, 2);
            modelBuilder.Entity<Invoice>().Property(i => i.ChangeDue).HasPrecision(18, 2);
            modelBuilder.Entity<InvoiceDetail>().Property(i => i.UnitPrice).HasPrecision(18, 2);
            modelBuilder.Entity<InvoiceDetail>().Property(i => i.SubTotal).HasPrecision(18, 2);
        }

        internal void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}