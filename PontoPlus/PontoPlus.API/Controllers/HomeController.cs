﻿using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PontoPlus.PontoPlus.Domain.Entities;
using PontoPlus.PontoPlus.Core.ViewModels;
using PontoPlus.PontoPlus.Services.Services;
using PontoPlus.PontoPlus.Services.Filters;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace PontoPlus.PontoPlus.API.Controllers
{
    public class HomeController : Controller
    {
        private readonly UsuarioServices _usuarioServices;
        private readonly RegistroPontoServices _registroPontoServices;

        public HomeController(UsuarioServices usuarioServices, RegistroPontoServices registroPontoServices)
        {
            _usuarioServices = usuarioServices;
            _registroPontoServices = registroPontoServices;
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
            Usuario usuario = _usuarioServices.FindById(id);
            RegistroPonto ponto = _registroPontoServices.FindByDayWithoutSaida(DateTime.Now, _usuarioServices.FindById(id));
            if (ponto != null)
            {
                usuario.AddRegistroPonto(ponto);
            }
            return View(usuario);
        }

        [HttpPost]
        [AutorizacaoFilter]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Usuario usuario)
        {
            int id = int.Parse(HttpContext.Session.GetString("UserId"));
            RegistroPonto ponto = _registroPontoServices.FindByDayWithoutSaida(DateTime.Now, _usuarioServices.FindById(id));
            if (ponto == null)
            {
                ponto = new RegistroPonto()
                {
                    Entrada = DateTime.Now,
                    UsuarioId = int.Parse(HttpContext.Session.GetString("UserId"))
                };

                _registroPontoServices.Insert(ponto);
            }
            else
            {
                ponto.Saida = DateTime.Now;
                ponto.TotalTempo = ponto.Saida.Subtract(ponto.Entrada);
                _registroPontoServices.Update(ponto);
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Usuario user)
        {
            if (user.Email != null && user.Senha != null)
            {
                Usuario usuario = _usuarioServices.ValidarLogin(user.Email, user.Senha);

                if (usuario == null)
                {
                    TempData["ErrorLogin"] = "Email ou Senha são inválidos";
                    return View();
                }

                HttpContext.Session.SetString("UserId", usuario.Id.ToString());
                HttpContext.Session.SetString("UserNome", usuario.Nome);
                HttpContext.Session.SetString("UserEmail", usuario.Email);
                HttpContext.Session.SetString("UserDepartamento", usuario.Departamentos.ToString());

                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public IActionResult SenhaEnviada() {
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Forget()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Instrucoes()
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
