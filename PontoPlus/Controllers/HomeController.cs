using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PontoPlus.Models;
using PontoPlus.Services;

namespace PontoPlus.Controllers
{
    public class HomeController : Controller
    {
        private readonly UsuarioServices _usuarioServices;

        public HomeController(UsuarioServices usuarioServices)
        {
            _usuarioServices = usuarioServices;
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Usuario user)
        {
            Usuario usuario = _usuarioServices.ValidarLogin(user.Email, user.Senha);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
