using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PontoPlus.PontoPlus.Infra.Data;
using PontoPlus.PontoPlus.Domain.Entities;
using PontoPlus.PontoPlus.Core.ViewModels;
using PontoPlus.PontoPlus.Services.Services;
using PontoPlus.PontoPlus.Core.Exceptions;
using PontoPlus.PontoPlus.Services.Filters;
using System.Data;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace PontoPlus.PontoPlus.API.Controllers
{
    [AutorizacaoFilter(Departamento = "RH")]
    public class UsuariosController : Controller
    {
        private readonly PontoPlusContext _context;
        private readonly UsuarioServices _usuarioServices;

        public UsuariosController(PontoPlusContext context, UsuarioServices usuarioServices)
        {
            _context = context;
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

        // GET: Usuarios 
        public async Task<IActionResult> Index()
        {
            return View(await _context.Usuarios.ToListAsync());
        }

        public IActionResult Relatorio(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");
            var users = _usuarioServices.FindAll();
            return View(users);
        }

        // GET: Usuarios/Details/{id}
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }
        // GET: Usuarios/Create
        public IActionResult ViewCreate()
        {
            return View();
        }
        // POST: Usuarios/Create
        [HttpPost]
        public async Task<IActionResult> Create(Usuario usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _usuarioServices.Insert(usuario);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    Console.WriteLine("Falso");
                    throw new Exception("ModelState não é válido");
                }
            }
            catch (DataException e)
            {
                throw new Exception("Ocorreu um erro fatal ao criar usuário: " + e.Message);
            }
        }

        // GET: usuarios/edit/{id}
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }
        // POST: Usuarios/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Email,Senha,Departamentos,EntradaAm,SaidaAm,EntradaPm,SaidaPm")] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (NotFoundException e)
                {
                    return RedirectToAction(nameof(Error), new { message = e.Message });
                }
                catch (DbUpdateConcurrencyException e)
                {
                    if (!UsuarioExists(usuario.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        return RedirectToAction(nameof(Error), new { message = e.Message });
                    }
                };
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Usuarios/Delete/{id}
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }
    }
}