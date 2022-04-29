using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public PatientBlockedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
