using MailKit.Net.Smtp;
using MimeKit;

using System.Linq;
using PontoPlus.PontoPlus.Domain.Entities;
using PontoPlus.PontoPlus.Domain.Enums;
using PontoPlus.PontoPlus.Infra.Data;

namespace PontoPlus.PontoPlus.Services.Services
{
    public class EmailService
    {
        private readonly PontoPlusContext _context;
        
        public EmailService(PontoPlusContext context) {
            _context = context;
        }
        
        public bool RedefinirSenha(string destinatario)
        {
            var usuario = from u in _context.Usuarios
                            where u.Email.Equals(destinatario)
                            select u;

            Usuario encontrado = usuario.FirstOrDefault();

            if (encontrado == null) {
                return false;
            }


            MailboxAddress from = new MailboxAddress("PontoPlus", "leandroudala@gmail.com");

            MailboxAddress to = new MailboxAddress(encontrado.Nome, destinatario);

            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = @"<p>Olá,<br>
            <br>
            Foi solicitada a redefinição de senha de acesso ao sistema PontoPlus para este endereço de e-mail.<br>
            <br>
            Caso você não tenha solicitado a redefinição de senha, por favor, entre em contato com nossa central de
            atendimento urgentemente!<br>
            <br>
            <STRONG>NÃO COMPARTILHE ESTE E-MAIL COM NINGUÉM</STRONG><br>
            <br>
            Nossa equipe nunca pedirá suas senhas de acesso e/ou redirecionamento de e-mails disparados pelo próprio sistema.<br>
            <br>
            <a href=""https://facebook.com/leandroudala"">Clique aqui para redefinir sua senha</a>
            </p>";

            bodyBuilder.TextBody = "Olá, Foi solicitada a redefinição de senha de acesso ao sistema PontoPlus para este endereço de e-mail.";

            MimeMessage message = new MimeMessage();   
            message.From.Add(from);
            message.To.Add(to);
            message.Subject = "Redefinir Senha - PontoPlus";
            message.Body = bodyBuilder.ToMessageBody();

            SmtpClient client = new SmtpClient();
            client.Connect("smtp.gmail.com", 465, true);
            client.Authenticate("leandroudala@gmail.com", "ntjmnmufkohljppt");

            client.Send(message);
            client.Disconnect(true);
            client.Dispose();

            return true;
        }

    }

}