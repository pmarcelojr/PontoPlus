using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PontoPlus.PontoPlus.Domain.Entities;

namespace PontoPlus.PontoPlus.Infra.Mappings
{
    public class UsuarioMensagemMap : IEntityTypeConfiguration<UsuarioMensagem>
    {
        public void Configure(EntityTypeBuilder<UsuarioMensagem> builder)
        {
            builder.HasOne(x => x.Usuario)
                .WithMany(x => x.Mensagens)
                .HasForeignKey(x => x.SenderId);

            builder.HasOne(x => x.Usuario)
                .WithMany(x => x.Mensagens)
                .HasForeignKey(x => x.ReceiverId);
        }
    }
}
