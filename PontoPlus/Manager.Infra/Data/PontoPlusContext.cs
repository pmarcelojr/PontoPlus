using Microsoft.EntityFrameworkCore;
using PontoPlus.Manager.Domain.Entities;

namespace PontoPlus.Manager.Infra.Data
{
    public class PontoPlusContext : DbContext
    {
        public PontoPlusContext(DbContextOptions<PontoPlusContext> options) : base(options)
        {
        }

        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<RegistroPonto> RegistroPontos { get; set; }
    }
}