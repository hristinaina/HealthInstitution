using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Services
{
    public interface IDoctorPendingMedicineService
    {
        public bool SendToRevision(PendingMedicine medicine);
        public bool Delete(PendingMedicine medicine);

    }
}
