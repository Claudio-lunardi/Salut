using Microsoft.EntityFrameworkCore;
using Salut.models;

namespace Salut.infra
{
    public class EntityContext : DbContext
    {

        public EntityContext(DbContextOptions<EntityContext> options) : base(options)
        {
        }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<NotaFiscal> NotaFiscal { get; set; }
    }
}