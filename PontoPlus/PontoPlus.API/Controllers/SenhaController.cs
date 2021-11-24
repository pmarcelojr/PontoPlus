using Microsoft.AspNetCore.Mvc;
using PontoPlus.PontoPlus.Services.Services;
using Microsoft.Extensions.Logging;
using PontoPlus.PontoPlus.Infra.Data;
using PontoPlus.PontoPlus.Domain.Entities;
using Newtonsoft.Json.Linq;

namespace PontoPlus.PontoPlus.API.Controllers
{
    public class SenhaController : Controller
    {
        private readonly SenhaService _senhaService;
        private readonly UsuarioServices _usuarioService;
        private readonly ILogger<SenhaController> _logger;

        public SenhaController(SenhaService senhaService, UsuarioServices usuarioService, ILogger<SenhaController> logger)
        {
            _senhaService = senhaService;
            _logger = logger;
            _usuarioService = usuarioService;
        }

        public IActionResult NovaSenha(string token)
        {
            bool tokenValido = _senhaService.IsValidToken(token);
            if (tokenValido)
            {
                return View();
            }

            _senhaService.RemoverToken(token);
            return Redirect("/");
        }

        [HttpPost]
        [Route("/api/senha/atualizar")]
        public IActionResult AtualizarSenha([FromBody] NovaSenha novaSenha)
        {
            string token = novaSenha.Token;
            string senha = novaSenha.Senha;
            SenhaToken senhaToken = _senhaService.GetEmailByToken(token);

            if (senhaToken == null)
            {
                return BadRequest("Token não encontrado");
            }

            bool atualizado = _usuarioService.AtualizarSenha(senhaToken.Email, senha);

            if (atualizado)
            {
                _senhaService.RemoverToken(token);
                return Ok();
            }

            return BadRequest("Ocorreu um erro ao atualizar a senha. Por favor, tente novamente mais tarde");
        }
    }
}