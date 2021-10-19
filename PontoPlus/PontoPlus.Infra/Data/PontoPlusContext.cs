using Microsoft.EntityFrameworkCore;
using PontoPlus.Manager.Domain.Entities;
using PontoPlus.Manager.Infra.Mappings;

namespace PontoPlus.Manager.Infra.Data
{
    public class PontoPlusContext : DbContext
    {
        public PontoPlusContext(DbContextOptions<PontoPlusContext> options) : base(options)
        {
        }

        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<RegistroPonto> RegistroPontos { get; set; }
        public virtual DbSet<UsuarioMensagem> UsuarioMensagens { get; set; }

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder.UseMySql(@"Server=localhost;Database=PontoPlus;Uid=root;Pwd=root;");
        // }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UsuarioMensagemMap());
        }
    }
}