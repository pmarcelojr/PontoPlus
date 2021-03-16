using System.Linq;
using PontoPlus.Data;
using PontoPlus.Models;

namespace PontoPlus.Services
{
    public class UsuarioServices
    {
        private readonly PontoPlusContext _context;

        public UsuarioServices(PontoPlusContext context)
        {
            _context = context;
        }

        public Usuario ValidarLogin(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return null;
            }

            Usuario user = _context.Usuarios.SingleOrDefault(x => x.Email == email && x.Senha == password);

            if (user == null)
            {
                return null;
            }

            return user;
        }
    }    
}