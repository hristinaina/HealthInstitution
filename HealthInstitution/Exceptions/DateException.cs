using System;
namespace HealthInstitution.Exceptions
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
