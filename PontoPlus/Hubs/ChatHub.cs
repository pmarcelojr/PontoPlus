using Microsoft.AspNetCore.SignalR;
using PontoPlus.PontoPlus.Domain.Entities;
using PontoPlus.PontoPlus.Services.Services;
using System.Threading.Tasks;

namespace PontoPlus.Hubs
{
    public class ChatHub : Hub
    {
        private readonly UsuarioMensagemService _usuarioMensagemService;

        public ChatHub(UsuarioMensagemService usuarioMensagemService)
        {
            _usuarioMensagemService = usuarioMensagemService;
        }

        public async Task SendMessage(string senderId, string receiverId, string message)
        {
            var userMessage = new UsuarioMensagem(
                senderId: int.Parse(senderId),
                receiverId: int.Parse(receiverId),
                message: message);

            _usuarioMensagemService.Insert(userMessage);

            await Clients.All.SendAsync("ReceiveMessage", userMessage);
        }
    }
}
