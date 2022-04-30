using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
