using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Exceptions.AdminExceptions
{
    class EmptySearchPhraseException : Exception
    {
        public EmptySearchPhraseException()
        {
        }

        public EmptySearchPhraseException(string message)
            : base(message)
        {
        }
    }
}
