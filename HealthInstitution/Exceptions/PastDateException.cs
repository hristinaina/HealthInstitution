using System;
namespace HealthInstitution.Exceptions
{
    class PastDateException : Exception
    {
        public PastDateException()
        {
        }

        public PastDateException(string message)
            : base(message)
        {
        }
    }
}
