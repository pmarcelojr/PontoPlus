using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PontoPlus.Manager.Domain.Entities;

namespace PontoPlus.Manager.Infra.Mappings
{
    public class UsuariosMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            throw new System.NotImplementedException();
        }
    }
}