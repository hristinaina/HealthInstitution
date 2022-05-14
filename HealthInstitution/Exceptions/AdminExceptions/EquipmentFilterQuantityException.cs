using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Exceptions.AdminExceptions
{
    class EquipmentFilterQuantityException : Exception
    {
        public EquipmentFilterQuantityException()
        {
        }

        public EquipmentFilterQuantityException(string message)
            : base(message)
        {
        }
    }
}
