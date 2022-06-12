using System;

namespace HealthInstitution.Core.Exceptions
{
    class EmptyFieldException : Exception
    {
        public EmptyFieldException()
        {
        }

        public EmptyFieldException(string message)
            : base(message)
        {
        }
    }
}
