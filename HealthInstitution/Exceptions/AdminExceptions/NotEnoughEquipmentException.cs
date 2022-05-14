using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Exceptions.AdminExceptions
{
    class NotEnoughEquipmentException : Exception
    {
        public NotEnoughEquipmentException()
        {
        }

        public NotEnoughEquipmentException(string message)
            : base(message)
        {
        }
    }
}
