using System;
using Microsoft.EntityFrameworkCore;
using ModeloTransacciones;

namespace DBTransacciones
{
    public class TransaccionesContext : DbContext 
    {
        public TransaccionesContext() : base() { }

        private const string ConnectionString = @"Server=tcp:genesios-server.database.windows.net,1433;" +
                                                "Initial Catalog=genesios-sql-azure-db;" +
                                                "Persist Security Info=False;" +
                                                "User ID=genesios-admin;" +
                                                "Password=Maxwell1367;" +
                                                "MultipleActiveResultSets=False;" +
                                                "Encrypt=True;" +
                                                "TrustServerCertificate=False;" +
                                                "Connection Timeout=30;"; 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
                                    => optionsBuilder.UseSqlServer(ConnectionString);

        public DbSet<Beneficiario> Beneficiarios { get; set; }
        public DbSet<CategoriaGasto> CategoriasGasto { get; set; }
        public DbSet<CategoriaIngreso> CategoriasIngreso { get; set; }
        public DbSet<Concepto> Conceptos { get; set; }
        public DbSet<Cuenta> Cuentas { get; set; }
        public DbSet<FormaTransaccion> FormasTransaccion { get; set; }
        public DbSet<Moneda> Monedas { get; set; }
        public DbSet<Situacion> Situaciones { get; set; }
        public DbSet<TipoTransaccion> TiposTransaccion { get; set; }
        public DbSet<Transaccion> Transacciones { get; set; }
    }
}
