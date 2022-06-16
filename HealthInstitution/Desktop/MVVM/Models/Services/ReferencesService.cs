using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Core;
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
                Patient p = Institution.Instance().PatientRepository.FindByID(change.PatientID);
                p.ExaminationChanges.Add(change);
            }
        }

        public static void ConnectExaminationReferences()
        {
            foreach (ExaminationReference reference in Institution.Instance().ExaminationReferencesRepository.GetReferences())
            {
                Examination examination = Institution.Instance().ExaminationRepository.FindByID(reference.ExaminationID);
                Doctor doctor = Institution.Instance().DoctorRepository.FindByID(reference.DoctorID);
                Patient patient = Institution.Instance().PatientRepository.FindByID(reference.PatientID);
                Prescription prescription = Institution.Instance().PrescriptionRepository.FindByID(reference.PerscriptionID);
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
                Doctor doctor = Institution.Instance().DoctorRepository.FindByID(reference.DoctorID);
                Patient patient = Institution.Instance().PatientRepository.FindByID(reference.PatientID);
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
            EquipmentService equipmentService = new EquipmentService();
            RoomService roomService = new RoomService();
            foreach (EquipmentArrangement a in Institution.Instance().EquipmentArragmentRepository.CurrentArrangement)
            {

                Room r = Institution.Instance().RoomRepository.FindById(a.RoomId);
                Equipment e = Institution.Instance().EquipmentRepository.FindById(a.EquipmentId);
                
                roomService.AddEquipment(e, a.Quantity, r);
                equipmentService.ArrangeInRoom(r, a.Quantity, e);
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
            foreach (Patient patient in Institution.Instance().PatientRepository.Patients)
            {
                patient.Examinations = Institution.Instance().ExaminationRepository.FindByPatientID(patient.ID);
                patient.Operations = Institution.Instance().OperationRepository.FindByPatientID(patient.ID);
                patient.Record.Referrals = Institution.Instance().ReferralRepository.FindByPatientID(patient.ID);

                List<PatientAllergen> patientAllergens = Institution.Instance().PatientAllergenRepository.FindByPatientID(patient.ID);
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
            foreach (Doctor doctor in Institution.Instance().DoctorRepository.Doctors)
            {
                List<DoctorDaysOff> doctorDaysOff = Institution.Instance().DoctorDaysOffRepository.FindByDoctorID(doctor.ID);
                doctor.DaysOff = Institution.Instance().DayOffRepository.DoctorDaysOffToDaysOff(doctorDaysOff);

                foreach (DayOff dayOff in doctor.DaysOff) dayOff.Doctor = doctor;
            }
        }

        public static void ConnectPrescriptionRepository()
        {
            foreach (Prescription prescription in Institution.Instance().PrescriptionRepository.Prescriptions)
            {
                PrescriptionMedicine prescriptionMedicine = Institution.Instance().PrescriptionMedicineRepository.
                                                             FindByPrescriptionID(prescription.ID);
                prescription.Medicine = Institution.Instance().MedicineRepository.PrescriptionMedicineToMedicine(prescriptionMedicine);
            }
        }

        public static void ConnectPatientNotifications()
        {
            foreach (Notification notification in Institution.Instance().NotificationRepository.Notifications)
            {
                Patient patient = Institution.Instance().PatientRepository.FindByID(notification.PatientId);
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
            if (!User.CheckEmail(email, Institution.Instance().PatientRepository.Patients, patientEmail)) return false;
            if (!User.CheckEmail(email, Institution.Instance().DoctorRepository.Doctors)) return false;
            if (!User.CheckEmail(email, Institution.Instance().SecretaryRepository.Secretaries)) return false;
            if (!User.CheckEmail(email, Institution.Instance().AdminRepository.Administrators)) return false;
            return true;
        }
    }
}
