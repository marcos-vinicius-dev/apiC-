using Microsoft.EntityFrameworkCore;

namespace apitarefas.Models
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options)
            : base(options)
        { }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}