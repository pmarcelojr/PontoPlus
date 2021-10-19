using PontoPlus.PontoPlus.Core.Exceptions;
using PontoPlus.PontoPlus.Domain.Validators;
using System;

namespace PontoPlus.PontoPlus.Domain.Entities
{
    public class UsuarioMensagem : Base
    {
        public int SenderId { get; private set; }
        public int ReceiverId { get; private set; }
        public string Message { get; private set; }
        public DateTime SendAt { get; private set; }
        public bool IsRead { get; private set; }

        //EF. Prop.
        public Usuario Usuario { get; set; }

        protected UsuarioMensagem()
        {
        }

        public UsuarioMensagem(int senderId, int receiverId, string message)
        {
            SenderId = senderId;
            ReceiverId = receiverId;
            Message = message;
            SendAt = DateTime.Now;
            IsRead = false;

            Validate();
        }

        public override bool Validate()
        {
            var validator = new UsuarioMensagemValidator();
            var validation = validator.Validate(this);

            if (!validation.IsValid)
            {
                foreach (var error in validation.Errors)
                    _errors.Add(error.ErrorMessage);

                throw new DomainException("Alguns campos estão inválidos, por favor corrija-os", _errors);
            }

            return true;
        }
    }
}
