using Microsoft.AspNetCore.Mvc;
using PontoPlus.PontoPlus.Services.Services;
using PontoPlus.PontoPlus.Infra.Data;

namespace PontoPlus.PontoPlus.API.Controllers
{
    public class EmailController : ControllerBase
    {
        private readonly EmailService _emailService;

        private readonly PontoPlusContext _context;

        public EmailController(PontoPlusContext context)
        {
            _context = context;
            _emailService = new EmailService(_context);
        }

        [HttpPost]
        [Route("/api/email/redefinir-senha")]
        public IActionResult esqueciMinhaSenha([FromQuery(Name = "para")] string emailPara)
        {
            bool sucesso = _emailService.RedefinirSenha(emailPara);

            if (!sucesso) {
                return BadRequest("E-mail não encontrado");
            }

            return Ok("E-mail disparado");
        }
    }
}