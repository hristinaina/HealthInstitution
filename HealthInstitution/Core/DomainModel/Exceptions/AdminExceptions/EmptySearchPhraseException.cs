using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Exceptions
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
