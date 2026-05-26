using Microsoft.EntityFrameworkCore;

namespace WebCRUDMVCSQL.Models
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {

        }

        public DbSet<Produto> Produto { get; set; }

        public DbSet<Client> Client { get; set; }

        public DbSet<Usuario> Usuario { get; set; }

        public DbSet<Pedido> Pedido { get; set; }
    }
}
