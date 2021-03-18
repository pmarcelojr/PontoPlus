using System;

namespace PontoPlus.Models
{
    public class RegistroPonto : IEquatable<RegistroPonto>
    {
        public int Id { get; set; }
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
        }

        public bool Equals(RegistroPonto other)
        {
            return this.Id == other.Id;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}