using System;

namespace HealthInstitution.Exceptions
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
