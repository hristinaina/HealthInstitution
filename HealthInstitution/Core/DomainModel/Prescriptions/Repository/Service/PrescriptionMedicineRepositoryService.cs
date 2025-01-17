﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Repository
{
    public class PrescriptionMedicineRepositoryService : IPrescriptionMedicineRepositoryService
    {
        private readonly IPrescriptionMedicineRepository _prescriptionMedicineRepository;

        public PrescriptionMedicineRepositoryService()
        {
            _prescriptionMedicineRepository = Institution.Instance().PrescriptionMedicineRepository;
        }

        public void Add(PrescriptionMedicine prescriptionMedicine)
        {
            _prescriptionMedicineRepository.Add(prescriptionMedicine);
        }

        public PrescriptionMedicine FindByPrescriptionID(int prescriptionId)
        {
            return _prescriptionMedicineRepository.FindByPrescriptionID(prescriptionId);
        }

        public List<PrescriptionMedicine> GetPrescriptionMedicines()
        {
            return _prescriptionMedicineRepository.GetPrescriptionMedicines();
        }
    }
}
