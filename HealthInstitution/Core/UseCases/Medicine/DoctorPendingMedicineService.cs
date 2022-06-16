using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Core.Repositories;
using HealthInstitution.Core;
using HealthInstitution.Core.Repository;

namespace HealthInstitution.Core.Services
{
    class DoctorPendingMedicineService : IDoctorPendingMedicineService
    {
        private IPendingMedicineRepository _pendingMedicineRepository;

        public DoctorPendingMedicineService()
        {
            _pendingMedicineRepository = Institution.Instance().PendingMedicineRepository;
        }

        public bool SendToRevision(PendingMedicine medicine)
        {
            foreach (PendingMedicine i in _pendingMedicineRepository.GetPendingMedicines())
            {
                if (i.ID == medicine.ID)
                {
                    _pendingMedicineRepository.GetPendingMedicines().Remove(i);
                    _pendingMedicineRepository.GetPendingMedicines().Add(medicine);
                    return true;
                }
            }
            return false;
        }

        public bool Delete(PendingMedicine medicine)
        {
            foreach (PendingMedicine i in _pendingMedicineRepository.GetPendingMedicines())
            {
                if (i.ID == medicine.ID)
                {
                    _pendingMedicineRepository.GetPendingMedicines().Remove(i);
                    return true;
                }
            }
            return false;
        }
    }
}
