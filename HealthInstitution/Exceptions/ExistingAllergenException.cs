using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Exceptions
{
    class ExistingAllergenException : Exception
    {
        
        public ExistingAllergenException()
        {
        }

        public ExistingAllergenException(string message)
            : base(message)
        {
        }
        
    }
}
