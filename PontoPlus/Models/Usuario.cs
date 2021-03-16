using System;
using PontoPlus.Models.Enums;

namespace PontoPlus.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public Departamentos Departamentos { get; set; }
        
        public TimeSpan EntradaAm { get; set; }
        public TimeSpan SaidaAm { get; set; }
        public TimeSpan EntradaPm { get; set; }
        public TimeSpan SaidaPm { get; set; }

        public Usuario()
        { }
    }
}