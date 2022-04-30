using System;

namespace HealthInstitution.Exceptions
{
    class DoctorNotAvailableException : Exception
    {
        public DoctorNotAvailableException()
        {
        }

        public DoctorNotAvailableException(string message)
            : base(message)
        {
        }
    }
}
