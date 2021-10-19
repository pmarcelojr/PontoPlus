using System.Collections.Generic;

namespace PontoPlus.PontoPlus.Domain.Entities
{
    public abstract class Base
    {
        public int Id { get; set; }
        internal List<string> _errors;
        public IReadOnlyCollection<string> Errors => _errors;
        public abstract bool Validate();
    }
}