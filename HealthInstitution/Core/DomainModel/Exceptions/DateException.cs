using System;
namespace HealthInstitution.Core.Exceptions
{
    class DateException : Exception
    {
        public DateException()
        {
        }

        public DateException(string message)
            : base(message)
        {
        }
    }
}
