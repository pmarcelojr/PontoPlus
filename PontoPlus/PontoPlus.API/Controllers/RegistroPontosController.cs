using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PontoPlus.Manager.Domain.Entities;
using PontoPlus.Manager.Services.Services;
using PontoPlus.Manager.Services.Filters;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Filters;

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

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (HttpContext.Session.GetString("UserId") != null)
            {
                int id = int.Parse(HttpContext.Session.GetString("UserId"));
                var user = _usuarioServices.FindById(id);

                var usersChat = _usuarioServices.FindUsuariosChatByDepartamento(user.Departamentos);
                TempData["ChatUsers"] = JsonConvert.SerializeObject(usersChat);
            }
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