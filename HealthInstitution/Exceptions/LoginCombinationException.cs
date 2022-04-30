using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Exceptions
{
    class LoginCombinationException : Exception
    {
        public LoginCombinationException()
        {
        }

        public LoginCombinationException(string message)
            : base(message)
        {
        }

    }
}
