using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using PontoPlus.Manager.Core.Exceptions;
using PontoPlus.Manager.Domain.Enums;
using PontoPlus.Manager.Domain.Validators;

namespace PontoPlus.Manager.Domain.Entities
{
    public class Usuario : Base, IEqualityComparer<RegistroPonto>
    {
        public string Nome { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Senha { get; set; }

        public Departamentos Departamentos { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "Entrada 1")]
        public TimeSpan EntradaAm { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "Saída 1")]
        public TimeSpan SaidaAm { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "Entrada 2")]
        public TimeSpan EntradaPm { get; set; }

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
            _errors = new List<string>();

            Validate();
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
            return Pontos.Where(pt => pt.Entrada.Date >= initial && pt.Saida.Date <= final && pt.Entrada.Date <= final).Distinct(new Usuario()).Count();
        }

        public TimeSpan TotalHoras(DateTime initial, DateTime final)
        {
            long ticks = Pontos.Where(pt => pt.Entrada.Date >= initial && pt.Saida.Date <= final && pt.Saida.Date >= initial).Sum(pt => pt.TotalTempo.Ticks);
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

        public override bool Validate()
        {
            var validator = new UsuarioValidator();
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