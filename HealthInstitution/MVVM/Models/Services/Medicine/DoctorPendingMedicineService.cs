using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.MVVM.Models.Repositories;
using HealthInstitution.MVVM.Models.Entities;

namespace HealthInstitution.MVVM.Models.Services
{
    class DoctorPendingMedicineService
    {
        private PendingMedicineRepository _pendingMedicineRepository;

        public DoctorPendingMedicineService()
        {
            _pendingMedicineRepository = Institution.Instance().PendingMedicineRepository;
        }

        public bool SendToRevision(PendingMedicine medicine)
        {
            foreach (PendingMedicine i in _pendingMedicineRepository.PendingMedicines)
            {
                if (i.ID == medicine.ID)
                {
                    _pendingMedicineRepository.PendingMedicines.Remove(i);
                    _pendingMedicineRepository.PendingMedicines.Add(medicine);
                    return true;
                }
            }
            return false;
        }

        public bool Delete(PendingMedicine medicine)
        {
            foreach (PendingMedicine i in _pendingMedicineRepository.PendingMedicines)
            {
                if (i.ID == medicine.ID)
                {
                    _pendingMedicineRepository.PendingMedicines.Remove(i);
                    return true;
                }
            }
            return false;
        }
    }
}
