using System;

namespace HealthInstitution.Exceptions
{
    class PatientBlockedException : Exception
    {
        public PatientBlockedException()
        {
        }

        public PatientBlockedException(string message)
            : base(message)
        {
        }

    }
}
