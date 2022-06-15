using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Exceptions
{
    class ZeroIngredientsException : Exception
    {
        public ZeroIngredientsException()
        {
        }

        public ZeroIngredientsException(string message)
            : base(message)
        {
        }
    }
}
