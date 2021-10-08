using System;
using System.Collections.Generic;

namespace PontoPlus.Manager.Domain.Entities
{
    public class RegistroPonto : Base, IEquatable<RegistroPonto>
    {
        public DateTime Entrada { get; set; }
        public DateTime Saida { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public TimeSpan TotalTempo { get; set; }

        public RegistroPonto()
        {
        }

        public RegistroPonto(int id, DateTime entrada, DateTime saida, Usuario usuario, TimeSpan totalTempo)
        {
            Id = id;
            Entrada = entrada;
            Saida = saida;
            Usuario = usuario;
            TotalTempo = totalTempo;
            _errors = new List<string>();
        }

        public bool Equals(RegistroPonto other)
        {
            return this.Id == other.Id;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public override bool Validate()
        {
            throw new NotImplementedException();
        }
    }
}