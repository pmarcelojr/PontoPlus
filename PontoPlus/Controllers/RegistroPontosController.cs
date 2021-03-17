using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PontoPlus.Models;
using PontoPlus.Services;
using PontoPlus.Services.Filters;

namespace PontoPlus.Controllers
{
    public class RegistroPontosController : Controller
    {
        private readonly RegistroPontoServices _registroPontoServices;
        private readonly UsuarioServices _usuarioServices;

        public RegistroPontosController(RegistroPontoServices registroPontoServices, UsuarioServices usuarioServices)
        {
            _registroPontoServices = registroPontoServices;
            _usuarioServices = usuarioServices;
        }

        [AutorizacaoFilter]
        public IActionResult Index()
        {
            int id = int.Parse(HttpContext.Session.GetString("UserId"));
            Usuario user = _usuarioServices.FindById(id);
            var list = _registroPontoServices.FindAll(user);
            return View(list);
        }
    }
}