using Microsoft.AspNetCore.Mvc;
using PontoPlus.Manager.Services.Services;
using System;

namespace PontoPlus.Manager.API.Controllers
{
    public class MensagemController : ControllerBase
    {
        private readonly UsuarioMensagemService _usuarioMensagemService;

        public MensagemController(UsuarioMensagemService usuarioMensagemService)
        {
            _usuarioMensagemService = usuarioMensagemService;
        }

        [HttpGet]
        [Route("/api/messages/{receiverId}/{senderId}")]
        public IActionResult Get(int receiverId, int senderId)
        {
            try
            {
                var mensagens = _usuarioMensagemService.GetConversa(receiverId, senderId);

                if (mensagens != null)
                {
                    return Ok(mensagens);
                }

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocorreu algum erro interno, por favor tente novamente");
            }
        }
    }
}
