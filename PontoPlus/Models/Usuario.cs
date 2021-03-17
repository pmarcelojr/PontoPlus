using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PontoPlus.Models.Enums;

namespace PontoPlus.Models
{
    public class Usuario
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
    }
}