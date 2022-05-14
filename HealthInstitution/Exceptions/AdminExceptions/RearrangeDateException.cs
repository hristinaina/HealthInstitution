using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Exceptions.AdminExceptions
{
    class RearrangeDateException : Exception
    {
        public RearrangeDateException()
        {
        }

        public RearrangeDateException(string message)
            : base(message)
        {
        }
    }
}
