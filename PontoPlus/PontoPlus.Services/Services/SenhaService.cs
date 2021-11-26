
using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using PontoPlus.PontoPlus.Domain.Entities;
using PontoPlus.PontoPlus.Infra.Data;

namespace PontoPlus.PontoPlus.Services.Services
{
    public class SenhaService
    {
        private readonly PontoPlusContext _context;

        public SenhaService(PontoPlusContext context)
        {
            _context = context;
        }

        public SenhaToken GerarToken(string email)
        {
            // criando novo token
            SenhaToken novoToken = new SenhaToken(email);

            // salvando token
            _context.SenhaTokens.Add(novoToken);
            _context.SaveChanges();

            return novoToken;
        }

        public bool IsValidToken(string token)
        {
            SenhaToken existe = _context.SenhaTokens.FirstOrDefault(obj => obj.Token == token);

            if (existe == null)
            {
                return false;
            }

            TimeSpan ts = DateTime.Now - existe.Data;

            return ts.TotalMinutes <= 30;
        }

        internal SenhaToken GetEmailByToken(string token)
        {
            return _context.SenhaTokens.FirstOrDefault(o => o.Token == token);
        }

        internal void RemoverToken(string token)
        {
            SenhaToken senhaToken = _context.SenhaTokens.FirstOrDefault(o => o.Token == token);

            if (senhaToken == null)
            {
                return;
            }

            _context.SenhaTokens.Remove(senhaToken);
            _context.SaveChanges();
        }
    }
}