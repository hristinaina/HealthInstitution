using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Entities.References;
using HealthInstitution.MVVM.Models.Enumerations;
using HealthInstitution.MVVM.ViewModels.SecretaryViewModels;

namespace HealthInstitution.MVVM.Models.Services
{
    public static class SecretaryService
    {

        public static void ApproveChange(ExaminationChange request)
        {
            request.Resolved = true;
            Appointment appointment = Institution.Instance().ExaminationRepository.FindByID(request.AppointmentID);

            if (request.ChangeStatus.ToString() == "EDITED")
            {
                Institution.Instance().RescheduleExamination(appointment, request.NewDate);
                // TODO: dodati da vraca vrijednost o uspjesnoti promjene i zavisno od toga prikazati MassageBox: uspjesno, neuspjesno
            }
            //deleted
            else if (request.ChangeStatus.ToString() == "DELETED")
            {
                DeleteAppointment(appointment);
                string message = "The appointment has been successfully deleted.";
                MessageBox.Show(message);
            }
        }

        private static void DeleteAppointment(Appointment appointment) 
        {
            Patient patient = appointment.Patient;
            Doctor doctor = appointment.Doctor;
            Room room = appointment.Room;

            if (appointment is Examination)
            {
                patient.Examinations.Remove((Examination)appointment);
                doctor.Examinations.Remove((Examination)appointment);
                room.Appointments.Remove(appointment);
                Institution.Instance().ExaminationRepository.Remove((Examination)appointment);
                Institution.Instance().ExaminationReferencesRepository.Remove((Examination)appointment);
            }
            else if (appointment is Operation)
            {
                // TODO: prekopirati od Milice kad zavrsi
            }
        }

        public static void RejectChange(ExaminationChange request)
        {
            request.Resolved = true;

            string message = "This request has been successfully rejected.";
            MessageBox.Show(message);
        }

        public static void RemoveOutdatedRequests()
        {
            foreach (ExaminationChange request in Institution.Instance().ExaminationChangeRepository.Changes)
            {
                if (!request.Resolved && request.NewDate <= DateTime.Now)
                {
                    request.ChangeStatus = Models.Enumerations.AppointmentStatus.DELETED;
                    request.Resolved = true;
                }
            }
        }

        public static void DeletePatient(Patient patient)
        {
            patient.Deleted = true;

            DeleteFutureAppointments(patient);
        }

        private static void DeleteFutureAppointments(Patient patient)
        {
            List<Examination> examinations = new List<Examination>(Institution.Instance().ExaminationRepository.Examinations.ToArray());
            List<Operation> operations = new List<Operation>(Institution.Instance().OperationRepository.Operations.ToArray());

            foreach (Examination appointment in examinations)
            {
                if (appointment.Date >= DateTime.Now && patient.ID == appointment.Patient.ID) DeleteAppointment(appointment);
            }
            foreach (Operation appointment in operations)
            {
                if (appointment.Date >= DateTime.Now && patient.ID == appointment.Patient.ID) DeleteAppointment(appointment);
            }

            Institution.Instance().ExaminationChangeRepository.DeleteUnresolvedRequestsByPatientId(patient.ID);
        }

        public static void BlockPatient(Patient patient)
        {
            patient.Blocked = true;
            patient.BlockadeType = BlockadeType.SECRETARY;

            DeleteFutureAppointments(patient);
        }
    }
}
