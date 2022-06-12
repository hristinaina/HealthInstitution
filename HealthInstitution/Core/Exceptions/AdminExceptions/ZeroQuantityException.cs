using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Exceptions
{
    class ZeroQuantityException : Exception
    {
        public ZeroQuantityException()
        {
        }

        public ZeroQuantityException(string message)
            : base(message)
        {
        }
    }
}
