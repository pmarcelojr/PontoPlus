using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PontoPlus.Manager.Domain.Entities;
using PontoPlus.Manager.Domain.Enums;
using PontoPlus.Manager.Infra.Data;

namespace PontoPlus.Manager.Services.Services
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

        public void Insert(Usuario obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }

        public List<Usuario> FindAll()
        {
            return _context.Usuarios.Include(obj => obj.Pontos).ToList();
        }

        public Usuario FindById(int id)
        {
            return _context.Usuarios.FirstOrDefault(obj => obj.Id == id);
        }

        public List<Usuario> FindUsuariosChatByDepartamento(Departamentos departamento)
        {
            if (departamento == Departamentos.RH)
                return _context.Usuarios.Where(x => x.Departamentos != Departamentos.RH)
                    .ToList();

            return _context.Usuarios.Where(x => x.Departamentos == Departamentos.RH)
                    .ToList();
        }
    }
}