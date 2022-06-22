using CaseCustumer.Domain.Models.Cliente;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseCustumer.Infra.Context
{
    public class ClienteDbContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public ClienteDbContext(DbContextOptions<ClienteDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Cliente>(c =>
            {
                c.HasOne(c => c.Endereco)
                .WithOne(e => e.Cliente)
                .HasForeignKey<Endereco>(e => e.ClienteId)
                .HasConstraintName("ClienteEnderecoFK")
                .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
