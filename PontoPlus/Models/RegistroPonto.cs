using System;

namespace PontoPlus.Models
{
    public class RegistroPonto
    {
        public int Id { get; set; }
        public DateTime Entrada { get; set; }
        public DateTime Saida { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public TimeSpan TotalTempo { get; set; }
    }
}