using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Exceptions
{
    class NameNotAvailableException : Exception
    {
        public NameNotAvailableException()
        {
        }

        public NameNotAvailableException(string message)
            : base(message)
        {
        }
    }
}
