using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PontoPlus.Data;
using PontoPlus.Models;

namespace PontoPlus.Services
{
    public class RegistroPontoServices
    {
        private readonly PontoPlusContext _context;

        public RegistroPontoServices(PontoPlusContext context)
        {
            _context = context;
        }

        public List<RegistroPonto> FindAll()
        {
            return _context.RegistroPontos.ToList();
        }

        public List<RegistroPonto> FindAll(Usuario user)
        {
            return _context.RegistroPontos.Where(x => x.Usuario.Id == user.Id).ToList();
        }

        public void Insert(RegistroPonto obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }

        public RegistroPonto FindById(int id)
        {
            return _context.RegistroPontos.Include(obj => obj.Usuario).FirstOrDefault(obj => obj.Id == id);
        }

        public RegistroPonto FindByDayWithoutSaida(DateTime data, Usuario usuario)
        {
            return _context.RegistroPontos.Include(obj => obj.Usuario).FirstOrDefault(obj => obj.Entrada.Date == data.Date && obj.Saida == new DateTime() && obj.Usuario == usuario);
        }

        public void Update(RegistroPonto obj)
        {
            if (_context.RegistroPontos.Any(x => x.Id == obj.Id))
            {
                _context.Update(obj);
                _context.SaveChanges();
            }
        }
    }
}