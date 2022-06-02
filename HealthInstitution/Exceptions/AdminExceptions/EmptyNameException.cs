using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Exceptions.AdminExceptions
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
