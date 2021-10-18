using PontoPlus.Manager.Domain.Entities;
using PontoPlus.Manager.Infra.Data;
using System.Collections.Generic;
using System.Linq;

namespace PontoPlus.Manager.Services.Services
{
    public class UsuarioMensagemService
    {
        private readonly PontoPlusContext _context;

        public UsuarioMensagemService(PontoPlusContext context)
        {
            _context = context;
        }

        public void Insert(UsuarioMensagem obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }

        public List<UsuarioMensagem> GetConversa(int receiverId, int senderId)
        {
            return _context.UsuarioMensagens.Where(x =>
                x.ReceiverId == receiverId && x.SenderId == senderId ||
                x.ReceiverId == senderId && x.SenderId == receiverId)
            .ToList();
        }
    }
}
