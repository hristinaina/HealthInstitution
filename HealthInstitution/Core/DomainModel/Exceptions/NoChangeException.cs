﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Exceptions
{
    class NoChangeException : Exception
    {
        public NoChangeException() : base()
        {
        }

        public NoChangeException(string message) : base(message)
        {
        }
    }
}
