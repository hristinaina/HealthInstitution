using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Core;
using HealthInstitution.Core.Repositories;
using HealthInstitution.Core.Repositories.References;

namespace HealthInstitution.Core.Services
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

        public bool CreatePrescription(Prescription prescription, Examination examination)
        {
            int id = Institution.Instance().PrescriptionRepository.GetID();
            prescription.ID = id;
            PatientManagementService patientService = new();

            if (patientService.IsAllergic(examination.Patient, prescription.Medicine.Ingredients)) throw new Exception("Patient is allergic !");

            _prescriptionRepository.Add(prescription);
            ExaminationService examinationService = new ExaminationService();
            examinationService.AddPrescription(examination, prescription);
            if ((prescription.Medicine == null) || (prescription.TimesADay < 1) || (prescription.LongitudeInDays < 1))
            {
                throw new Exception("Wrong input !");
            }
            PrescriptionMedicine prescriptionMedicine = new PrescriptionMedicine(prescription.Medicine.ID, prescription.ID);
            _prescriptionMedicineRepository.Add(prescriptionMedicine);
            ExaminationReference examinationReference = new ExaminationReference(examination.ID, examination.Doctor.ID,
                                                                                 examination.Patient.ID, examination.Room.ID,
                                                                                 prescription.ID);
            Institution.Instance().ExaminationReferencesRepository.Add(examinationReference);

            return true;
        }
    }
}
