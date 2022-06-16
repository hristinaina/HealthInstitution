using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Core;
using HealthInstitution.Core.Repository;
using HealthInstitution.Core.Services.Equipments;
using HealthInstitution.Core.Services.Renovations;
using HealthInstitution.Core.Services.Rooms;

namespace HealthInstitution.Core.Services
{
    public static class ReferencesService
    {
        public static void ConnectExaminationChanges()
        {
            foreach (ExaminationChange change in Institution.Instance().ExaminationChangeRepository.Changes)
            {
                if (change.Resolved && change.ChangeStatus == AppointmentStatus.DELETED) {
                    return;
                } 
                Patient p = new PatientRepositoryService().FindByID(change.PatientID);
                p.ExaminationChanges.Add(change);
            }
        }

        public static void ConnectExaminationReferences()
        {
            foreach (ExaminationReference reference in Institution.Instance().ExaminationReferencesRepository.GetReferences())
            {
                Examination examination = Institution.Instance().ExaminationRepository.FindByID(reference.ExaminationID);
                Doctor doctor = new DoctorRepositoryService().FindByID(reference.DoctorID);
                Patient patient = new PatientRepositoryService().FindByID(reference.PatientID);
                Prescription prescription = new PrescriptionRepositoryService().FindByID(reference.PerscriptionID);
                Room room = Institution.Instance().RoomRepository.FindById(reference.RoomID);


                examination.Doctor = doctor;
                examination.Patient = patient;
                examination.Prescriptions.Add(prescription);
                examination.Room = room;

                room.Appointments.Add(examination);
                doctor.Examinations.Add(examination);
                patient.Examinations.Add(examination);
            }
        }

        public static void ConnectOperationReferences()
        {
            foreach (OperationReference reference in Institution.Instance().OperationReferencesRepository.GetReferences())
            {
                Operation operation = Institution.Instance().OperationRepository.FindByID(reference.OperationId);
                Doctor doctor = new DoctorRepositoryService().FindByID(reference.DoctorID);
                Patient patient = new PatientRepositoryService().FindByID(reference.PatientID);
                Room room = Institution.Instance().RoomRepository.FindById(reference.RoomID);

                operation.Doctor = doctor;
                operation.Patient = patient;
                operation.Room = room;

                room.Appointments.Add(operation);
                doctor.Operations.Add(operation);
                patient.Operations.Add(operation);
            }
        }

        public static void ArrangeEquipment()
        {
            foreach (EquipmentArrangement a in Institution.Instance().EquipmentArragmentRepository.CurrentArrangement)
            {

                Room r = Institution.Instance().RoomRepository.FindById(a.RoomId);
                Equipment e = Institution.Instance().EquipmentRepository.FindById(a.EquipmentId);
                RoomService room = new RoomService(r);
                room.AddEquipment(e, a.Quantity);
                EquipmentService equipment = new EquipmentService(e);
                equipment.ArrangeInRoom(r, a.Quantity);
            }
        }

        public static void ConnectRenovations()
        {
            foreach (RoomRenovation roomUnderRenovation in Institution.Instance().RoomRenovationRepository.RoomsUnderRenovations)
            {
                Renovation renovation = Institution.Instance().RenovationRepository.FindById(roomUnderRenovation.RenovationId);
                Room room = Institution.Instance().RoomRepository.FindById(roomUnderRenovation.RoomId);

                room.Renovations.Add(renovation);

                if (roomUnderRenovation.Result) renovation.Result.Add(room);
                else renovation.RoomsUnderRenovation.Add(room);
            }

            RenovationService renovationService = new RenovationService();
            renovationService.StartRenovations();
            renovationService.EndRenovations();

            Institution.Instance().EquipmentOrderRepository.Deliver(Institution.Instance().EquipmentRepository);
        }

        public static void FillMedicalRecord()
        {
            foreach (Patient patient in new PatientRepositoryService().GetPatients())
            {
                patient.Examinations = Institution.Instance().ExaminationRepository.FindByPatientID(patient.ID);
                patient.Operations = Institution.Instance().OperationRepository.FindByPatientID(patient.ID);
                patient.Record.Referrals = new ReferralRepositoryService().FindByPatientID(patient.ID);

                List<PatientAllergen> patientAllergens = new PatientAllergenRepositoryService().FindByPatientID(patient.ID);
                patient.Record.Allergens = Institution.Instance().AllergenRepository.PatientAllergenToAllergen(patientAllergens);
            }
        }

        public static void ConnectMedicineAllergens()
        {
            foreach (Medicine medicine in Institution.Instance().MedicineRepository.Medicines)
            {
                List<MedicineAllergen> medicineAllergens = Institution.Instance().MedicineAllergenRepository.FindByMedicineID(medicine.ID);
                medicine.Ingredients = Institution.Instance().AllergenRepository.MedicineAllergenToAllergen(medicineAllergens);
            }

            foreach (PendingMedicine medicine in Institution.Instance().PendingMedicineRepository.PendingMedicines)
            {
                List<MedicineAllergen> medicineAllergens = Institution.Instance().MedicineAllergenRepository.FindByMedicineID(medicine.ID);
                medicine.Ingredients = Institution.Instance().AllergenRepository.MedicineAllergenToAllergen(medicineAllergens);
            }
        }

        public static void ConnectPendingMedicineAllergens()
        {
            foreach (PendingMedicine medicine in Institution.Instance().PendingMedicineRepository.PendingMedicines)
            {
                List<MedicineAllergen> medicineAllergens = Institution.Instance().MedicineAllergenRepository.FindByMedicineID(medicine.ID);
                medicine.Ingredients = Institution.Instance().AllergenRepository.MedicineAllergenToAllergen(medicineAllergens);
            }
        }

        public static void ConnectDoctorDaysOff()
        {
            foreach (Doctor doctor in new DoctorRepositoryService().GetDoctors())
            {
                List<DoctorDaysOff> doctorDaysOff = Institution.Instance().DoctorDaysOffRepository.FindByDoctorID(doctor.ID);
                doctor.DaysOff = Institution.Instance().DayOffRepository.DoctorDaysOffToDaysOff(doctorDaysOff);

                foreach (DayOff dayOff in doctor.DaysOff) dayOff.Doctor = doctor;
            }
        }

        public static void ConnectPrescriptionRepository()
        {
            foreach (Prescription prescription in new PrescriptionRepositoryService().GetPrescriptions())
            {
                PrescriptionMedicine prescriptionMedicine = new PrescriptionMedicineRepositoryService().
                                                             FindByPrescriptionID(prescription.ID);
                prescription.Medicine = Institution.Instance().MedicineRepository.PrescriptionMedicineToMedicine(prescriptionMedicine);
            }
        }

        public static void ConnectPatientNotifications()
        {
            foreach (Notification notification in new NotificationRepositoryService().GetNotifications())
            {
                Patient patient = new PatientRepositoryService().FindByID(notification.PatientId);
                patient.Notifications.Add(notification);
            }
        }

        public static bool CheckIfEmailIsAvailable(string email, Patient patient = null)
        {
            string patientEmail = null;
            if (patient != null)
            {
                patientEmail = patient.Email;
            }
            if (!User.CheckEmail(email, new PatientRepositoryService().GetPatients(), patientEmail)) return false;
            if (!User.CheckEmail(email, new DoctorRepositoryService().GetDoctors())) return false;
            if (!User.CheckEmail(email, Institution.Instance().SecretaryRepository.Secretaries)) return false;
            if (!User.CheckEmail(email, Institution.Instance().AdminRepository.Administrators)) return false;
            return true;
        }
    }
}
