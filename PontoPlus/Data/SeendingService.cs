using System;
using System.Linq;
using PontoPlus.Models;
using PontoPlus.Models.Enums;

namespace PontoPlus.Data
{
    public class SeendingService
    {
        private readonly PontoPlusContext _context;

        public SeendingService(PontoPlusContext context)
        {
            _context = context;
        }
        public void Seed()
        {
            if (_context.Usuarios.Any())
            {
                return;
            }

            Usuario u1 = new Usuario(1, "Marcelo Santos", "ti@gmail.com", "12345", Departamentos.TI, new TimeSpan(7, 30, 00), new TimeSpan(11, 30, 00), new TimeSpan(13, 30, 00), new TimeSpan(17, 30, 00));
            Usuario u2 = new Usuario(2, "Katia Aguiar", "rh@gmail.com", "12345", Departamentos.RH, new TimeSpan(7, 30, 00), new TimeSpan(11, 30, 00), new TimeSpan(13, 30, 00), new TimeSpan(17, 30, 00));

            RegistroPonto p1 = new RegistroPonto(1, new DateTime(2021, 03, 12, 7, 30, 24), new DateTime(2021, 03, 12, 11, 31, 24), u1, new TimeSpan(4, 1, 00));
            RegistroPonto p2 = new RegistroPonto(2, new DateTime(2021, 03, 13, 11, 20, 14), new DateTime(2021, 03, 13, 17, 30, 14), u1, new TimeSpan(4, 10, 00));
            RegistroPonto p3 = new RegistroPonto(3, new DateTime(2021, 03, 15, 7, 25, 13), new DateTime(2021, 03, 15, 11, 45, 13), u1, new TimeSpan(4, 20, 00));
            RegistroPonto p4 = new RegistroPonto(4, new DateTime(2021, 03, 15, 11, 29, 07), new DateTime(2021, 03, 15, 17, 40, 07), u1, new TimeSpan(4, 11, 00));

            _context.Usuarios.AddRange(u1, u2);
            _context.RegistroPontos.AddRange(p1, p2, p3, p4);

            _context.SaveChanges();
        }
    }
}