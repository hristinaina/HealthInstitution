using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Repositories;
using HealthInstitution.Repositories;
using HealthInstitution.MVVM.Models.Services;
using HealthInstitution.MVVM.Models.Services.DoctorServices;

namespace HealthInstitution.MVVM.Models.Services
{
    public class EmergencyAppointmentService
    {
        private DateTime _newDate;
        private Doctor _doctor;
        private Patient _patient;
        private int _newDuration;

        private readonly DoctorRepository _doctorRepository;
        private readonly ExaminationRepository _examinationRepository;
        private readonly OperationRepository _operationRepository;

        public EmergencyAppointmentService()
        {
            _doctor = null;
            _doctorRepository = Institution.Instance().DoctorRepository;
            _examinationRepository = Institution.Instance().ExaminationRepository;
            _operationRepository = Institution.Instance().OperationRepository;
        }

        public void ChangeDuration(int duration)
        {
            _newDuration = duration;
        }

        public bool CreateAppointment(Specialization specialization, Patient patient)
        {
            _patient = patient;
            DateTime startTime = DateTime.Now.AddMinutes(5);
            DateTime currentTime = DateTime.Now.AddMinutes(5);
            _newDate = currentTime;
            string type = _newDuration == 15 ? nameof(Examination) : nameof(Operation);
            bool done = false;

            for (; currentTime < startTime.AddHours(2); currentTime = currentTime.AddMinutes(15))
            {
                bool specialistException = false;
                foreach (Doctor doctor in _doctorRepository.Doctors)
                {
                    if (doctor.Specialization == specialization)
                    {
                        specialistException = true;
                        done = Institution.Instance().CreateAppointment(doctor, patient, currentTime, type, _newDuration, false);
                        if (done)
                        {
                            _newDate = currentTime;
                            _doctor = doctor;
                            return true;
                        }
                    }
                }
                if (!specialistException)
                    throw new Exception("There are currently no doctors with selected specialization that work in this hospital.");
            }
            return done;
        }

        public Appointment FindAppointment(Patient patient, Doctor doctor, DateTime newDate)
        {
            Appointment appointment = _examinationRepository.FindAppointment(doctor, patient, newDate);
            if (appointment == null)
                appointment = _operationRepository.FindAppointment(doctor, patient, newDate);
            return appointment;
        }

        public void FindNewAppointmentTime(Appointment appointment, int duration, Dictionary<Appointment, DateTime> appointments)
        {
            //if (appointment.Emergency) return;
            DateTime startTime = appointment.Date;

            while (true)
            {
                startTime = startTime.AddMinutes(15);
                try
                {
                    bool done = CheckNewAppointmentTime(appointment.Doctor, appointment.Patient, startTime, duration, false);
                    if (done)
                    {
                        appointments.Add(appointment, startTime);
                        break;
                    }
                }
                catch (Exception e) { }
            }
        }

        private bool CheckNewAppointmentTime(Doctor doctor, Patient patient, DateTime dateTime, int duration, bool validation = false)
        {

            PatientService patientService = new PatientService(patient);
            DoctorService doctorService = new DoctorService(doctor); 
            if (!doctorService.IsAvailable(dateTime, duration))
            {
                return false;
            }
            if (!patientService.IsAvailable(dateTime, duration))
            {
                return false;
            }
            Institution.Instance().ValidateAppointmentData(patient, doctor, dateTime, validation);
            return true;
        }

        public int GetDuration(Appointment appointment)
        {
            int duration = 15;
            if (appointment.GetType() == typeof(Operation))
            {
                Operation o = (Operation)appointment;
                duration = o.Duration;
            }
            return duration;
        }

        public void NotifyDoctor()
        {
            Appointment newAppointment = FindAppointment(_patient, _doctor, _newDate);
            newAppointment.Emergency = true;
            _doctor.Notifications.Add("An emergency appointment with id=" + newAppointment.ID.ToString() + " has been scheduled!");
        }

        public void SendNotifications(Appointment rescheduledAppointment, DateTime oldDate, DateTime newDate, Patient patient, Doctor doctor)
        {
            string message = "Appointment with id=" + rescheduledAppointment.ID.ToString() + " has been changed." +
                " Changed date from " + oldDate.ToString() + " to " + newDate.ToString();
            Notification notification = Institution.Instance().NotificationRepository.CreateNotification(patient.ID, message);
            patient.Notifications.Add(notification);
            doctor.Notifications.Add(message);
            Appointment newAppointment = FindAppointment(patient, doctor, oldDate);
            newAppointment.Emergency = true;
            doctor.Notifications.Add("An emergency appointment with id=" + newAppointment.ID.ToString() + " has been scheduled!");
        }

    }
}
