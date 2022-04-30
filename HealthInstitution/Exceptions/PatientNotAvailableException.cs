using System;

namespace HealthInstitution.Exceptions
{
    class PatientNotAvailableException : Exception
    {
        public PatientNotAvailableException()
        {
        }

        public PatientNotAvailableException(string message)
            : base(message)
        {
        }
    }
}
