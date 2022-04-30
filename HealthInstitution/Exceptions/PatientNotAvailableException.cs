using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
