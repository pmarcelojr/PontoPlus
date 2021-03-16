using Microsoft.EntityFrameworkCore;
using PontoPlus.Models;

namespace PontoPlus.Data
{
    public class PontoPlusContext : DbContext
    {
        public PontoPlusContext(DbContextOptions<PontoPlusContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}