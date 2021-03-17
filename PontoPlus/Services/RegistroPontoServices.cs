using System.Linq;
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

        public void Insert(RegistroPonto obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }

        public RegistroPonto FindById(int id)
        {
            return _context.RegistroPontos.FirstOrDefault(obj => obj.Id == id);
        }
    }
}