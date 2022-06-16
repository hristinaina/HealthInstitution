using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Core;
using HealthInstitution.Core.Repositories;
using HealthInstitution.Core.Repositories.References;
using HealthInstitution.Core.Repository;

namespace HealthInstitution.Core.Services
{
    class DoctorPrescriptionService
    {
        private IPrescriptionMedicineRepositoryService _prescriptionMedicineService;
        private IPrescriptionRepositoryService _prescriptionService;

        public DoctorPrescriptionService()
        {
            _prescriptionMedicineService = new PrescriptionMedicineRepositoryService();
            _prescriptionService = new PrescriptionRepositoryService();
        }

        public bool CreatePrescription(Prescription prescription, Examination examination)
        {
            int id = _prescriptionService.GetNewID();
            prescription.ID = id;
            PatientManagementService patientService = new();

            if (patientService.IsAllergic(examination.Patient, prescription.Medicine.Ingredients)) throw new Exception("Patient is allergic !");

            _prescriptionService.Add(prescription);
            ExaminationService examinationService = new ExaminationService();
            examinationService.AddPrescription(examination, prescription);
            if ((prescription.Medicine == null) || (prescription.TimesADay < 1) || (prescription.LongitudeInDays < 1))
            {
                throw new Exception("Wrong input !");
            }
            PrescriptionMedicine prescriptionMedicine = new PrescriptionMedicine(prescription.Medicine.ID, prescription.ID);
            _prescriptionMedicineService.Add(prescriptionMedicine);
            ExaminationReference examinationReference = new ExaminationReference(examination.ID, examination.Doctor.ID,
                                                                                 examination.Patient.ID, examination.Room.ID,
                                                                                 prescription.ID);
            Institution.Instance().ExaminationReferencesRepository.Add(examinationReference);

            return true;
        }
    }
}
