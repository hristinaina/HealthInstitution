using System;

namespace HealthInstitution.Core.Exceptions
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
