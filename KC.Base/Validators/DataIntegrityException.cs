using System;

namespace KC.Base.Validators
{
    public class DataIntegrityException : Exception
    {
        public string Entity { get; }
        public string Field { get; }

        public DataIntegrityException(string entity, string field, string message) : base(message)
        {
            Entity = entity;
            Field = field;
        }
    }
}
