using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using PontoPlus.Models.Enums;

namespace PontoPlus.Models
{
    public class Usuario : IEqualityComparer<RegistroPonto>
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        [StringLength(80, MinimumLength = 3, ErrorMessage = "{0} o tamanho deve estar entre {2} e {1}")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        [EmailAddress(ErrorMessage = "Entre com um email válido")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        [StringLength(80, MinimumLength = 5, ErrorMessage = "{0} o tamanho deve estar entre {2} e {1}")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
        public Departamentos Departamentos { get; set; }
        
        [Required(ErrorMessage = "{0} é obrigatório")]
        [DataType(DataType.Time)]
        [Display(Name = "Entrada 1")]
        public TimeSpan EntradaAm { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        [DataType(DataType.Time)]
        [Display(Name = "Saída 1")]
        public TimeSpan SaidaAm { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        [DataType(DataType.Time)]
        [Display(Name = "Entrada 2")]
        public TimeSpan EntradaPm { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        [DataType(DataType.Time)]
        [Display(Name = "Saída 2")]
        public TimeSpan SaidaPm { get; set; }
        public ICollection<RegistroPonto> Pontos { get; set; }

        public Usuario()
        {           
        }

        public Usuario(int id, string nome, string email, string senha, Departamentos departamentos, TimeSpan entradaAm, TimeSpan saidaAm, TimeSpan entradaPm, TimeSpan saidaPm)
        { 
            Id = id;
            Nome = nome;
            Email = email;
            Senha = senha;
            Departamentos = departamentos;
            EntradaAm = entradaAm;
            SaidaAm = saidaAm;
            EntradaPm = entradaPm;
            SaidaPm = saidaPm;
        }

        public void AddRegistroPonto(RegistroPonto obj)
        {
            Pontos.Add(obj);
        }

        public void RemoveRegistroPonto(RegistroPonto obj)
        {
            Pontos.Remove(obj);
        }

        public TimeSpan CargaHoraria()
        {
            return (SaidaAm - EntradaAm + SaidaPm - EntradaPm);
        }

        public int DiasTrabalhados(DateTime initial, DateTime final)
        {
            return Pontos.Where(pt => pt.Entrada.Date >= initial && pt.Saida.Date <= final).Distinct(new Usuario()).Count();
        }

        public TimeSpan TotalHoras(DateTime initial, DateTime final)
        {
            long ticks = Pontos.Where(x => x.Entrada.Date >= initial && x.Saida.Date <= final).Sum(x => x.TotalTempo.Ticks);
            TimeSpan time = new TimeSpan(ticks);
            return time;
        }

        public TimeSpan TotalHorasExtra(DateTime initial, DateTime final)
        {
            return TotalHoras(initial, final) - CargaHoraria().Multiply(DiasTrabalhados(initial, final));
        }

        public bool Equals(RegistroPonto x, RegistroPonto y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode([DisallowNull] RegistroPonto obj)
        {
            return obj.Id;
        }
    }
}