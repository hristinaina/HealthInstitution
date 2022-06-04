using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Entities.References;
using HealthInstitution.MVVM.Models.Repositories;
using HealthInstitution.MVVM.Models.Enumerations;
using HealthInstitution.MVVM.Models.Repositories.References;

namespace HealthInstitution.MVVM.Models.Services
{
    class DoctorPrescriptionService
    {
        private PrescriptionRepository _prescriptionRepository;
        private PrescriptionMedicineRepository _prescriptionMedicineRepository;

        public DoctorPrescriptionService()
        {
            _prescriptionRepository = Institution.Instance().PrescriptionRepository;
            _prescriptionMedicineRepository = Institution.Instance().PrescriptionMedicineRepository;
        }

        public bool CreatePrescription(Medicine medicine, int longitudeInDays, int dailyFrequency,
                                       TherapyMealDependency therapyMealDependency, Examination examination)
        {
            int id = Institution.Instance().PrescriptionRepository.GetNewId();
            Prescription prescription = new Prescription(id, longitudeInDays, dailyFrequency, therapyMealDependency, DateTime.Now, medicine);
            if (examination.Patient.IsAllergic(prescription.Medicine.Ingredients)) throw new Exception("Patient is allergic !");

            _prescriptionRepository.Add(prescription);
            ExaminationService examinationService = new ExaminationService();
            examinationService.AddPrescription(examination, prescription);
            if ((medicine == null) || (dailyFrequency < 1) || (longitudeInDays < 1))
            {
                throw new Exception("Wrong input !");
            }
            PrescriptionMedicine prescriptionMedicine = new PrescriptionMedicine(medicine.ID, prescription.ID);
            _prescriptionMedicineRepository.Add(prescriptionMedicine);
            ExaminationReference examinationReference = new ExaminationReference(examination.ID, examination.Doctor.ID,
                                                                                 examination.Patient.ID, examination.Room.ID,
                                                                                 prescription.ID);
            Institution.Instance().ExaminationReferencesRepository.Add(examinationReference);

            return true;
        }
    }
}
