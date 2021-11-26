using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PontoPlus.PontoPlus.Domain.Entities;
using PontoPlus.PontoPlus.Domain.Enums;
using PontoPlus.PontoPlus.Infra.Data;

namespace PontoPlus.PontoPlus.Services.Services
{
    public class UsuarioServices
    {
        private readonly PontoPlusContext _context;

        private readonly ILogger _logger;

        public UsuarioServices(PontoPlusContext context, ILogger<UsuarioServices> logger)
        {
            _context = context;
            _logger = logger;
        }

        public Usuario ValidarLogin(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                _logger.LogError("Email ou senha nao pode estar em branco");
                return null;
            }

            Usuario user = _context.Usuarios.SingleOrDefault(x => x.Email == email);
            var senhaValida = BCrypt.Net.BCrypt.Verify(password, user.Senha);

            if (user == null || !senhaValida)
            {
                _logger.LogError("Senha incorreta");
                return null;
            }

            return user;
        }

        public void Insert(Usuario obj)
        {
            // verificando se usuário existe
            if (_context.Usuarios.Any(x => x.Email == obj.Email))
            {
                throw new System.Exception("O e-mail '" + obj.Email + "' já está sendo utilizado");
            }

            // criando hash de senha
            obj.Senha = BCrypt.Net.BCrypt.HashPassword(obj.Senha);

            // salvando usuário
            _context.Usuarios.Add(obj);
            _context.SaveChanges();
        }

        internal bool AtualizarSenha(string email, string senha)
        {
            Usuario usuario = _context.Usuarios.FirstOrDefault(o => o.Email == email);

            if (usuario == null)
            {
                return false;
            }

            usuario.AtualizarSenha(senha);
            _context.Usuarios.Update(usuario);
            _context.SaveChanges();
            return true;
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