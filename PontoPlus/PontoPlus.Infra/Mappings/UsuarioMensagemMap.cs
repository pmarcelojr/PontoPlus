using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PontoPlus.Manager.Domain.Entities;

namespace PontoPlus.Manager.Infra.Mappings
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
