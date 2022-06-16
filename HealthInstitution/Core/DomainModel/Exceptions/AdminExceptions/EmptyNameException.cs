using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Exceptions
{
    class EmptyNameException : Exception
    {
        public EmptyNameException()
        {
        }

        public EmptyNameException(string message)
            : base(message)
        {
        }
    }
}
