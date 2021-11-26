
using System;
using System.ComponentModel.DataAnnotations;

namespace PontoPlus.PontoPlus.Domain.Entities
{
    public class SenhaToken : Base
    {
        public string Token { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email {get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Data { get; set; }

        public SenhaToken(string email)
        {
            Email = email;
            Token = Guid.NewGuid().ToString();
            Data = DateTime.Now;
        }
        public override bool Validate()
        {
            return true;
        }
    }
}