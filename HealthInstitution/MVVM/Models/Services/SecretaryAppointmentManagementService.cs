using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Exceptions;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Enumerations;
using HealthInstitution.MVVM.Models.Repositories;
using HealthInstitution.MVVM.Models.Repositories.References;
using HealthInstitution.MVVM.Models.Services.DoctorServices;

namespace HealthInstitution.MVVM.Models.Services
{
    public class SecretaryAppointmentManagementService
    {
        private readonly ExaminationRepository _examinationRepository;
        private readonly OperationRepository _operationRepository;
        private readonly ExaminationReferencesRepository _examinationReferencesRepository;
        private readonly OperationReferencesRepository _operationReferencesRepository;
        private readonly ExaminationChangeRepository _examinationChangeRepository;
        private readonly RoomRepository _roomRepository;

        public SecretaryAppointmentManagementService()
        {
            _examinationRepository = Institution.Instance().ExaminationRepository;
            _operationRepository = Institution.Instance().OperationRepository;
            _examinationReferencesRepository = Institution.Instance().ExaminationReferencesRepository;
            _operationReferencesRepository = Institution.Instance().OperationReferencesRepository;
            _examinationChangeRepository = Institution.Instance().ExaminationChangeRepository;
            _roomRepository = Institution.Instance().RoomRepository;
        }

        public void DeleteFutureAppointments(Patient patient)
        {
            List<Examination> examinations = new List<Examination>(_examinationRepository.Examinations.ToArray());
            List<Operation> operations = new List<Operation>(_operationRepository.Operations.ToArray());

            foreach (Examination appointment in examinations)
            {
                if (appointment.Date >= DateTime.Now && patient.ID == appointment.Patient.ID) DeleteAppointment(appointment);
            }
            foreach (Operation appointment in operations)
            {
                if (appointment.Date >= DateTime.Now && patient.ID == appointment.Patient.ID) DeleteAppointment(appointment);
            }

            _examinationChangeRepository.DeleteUnresolvedRequestsByPatientId(patient.ID);
        }

        public void DeleteAppointment(Appointment appointment)
        {
            Patient patient = appointment.Patient;
            Doctor doctor = appointment.Doctor;
            Room room = appointment.Room;

            _examinationChangeRepository.RemoveByAppointmentId(appointment.ID);

            if (appointment is Examination)
            {
                patient.Examinations.Remove((Examination)appointment);
                doctor.Examinations.Remove((Examination)appointment);
                room.Appointments.Remove(appointment);
                _examinationRepository.Remove((Examination)appointment);
                _examinationReferencesRepository.Remove((Examination)appointment);
            }
            else if (appointment is Operation)
            {
                patient.Operations.Remove((Operation)appointment);
                doctor.Operations.Remove((Operation)appointment);
                _operationRepository.Remove((Operation)appointment);
                _operationReferencesRepository.Remove((Operation)appointment);
                room.Appointments.Remove(appointment);
            }
        }

        public bool CreateAppointment(Doctor doctor, Patient patient, DateTime dateTime, string type, int duration = 15, bool validation = true)
        {
            DoctorService doctorService = new DoctorService(doctor);
            PatientService patientService = new PatientService(patient);

            if (!doctorService.IsAvailable(dateTime, duration))
            {
                return false;
            }
            if (!patientService.IsAvailable(dateTime, duration))
            {
                return false;
            }

            ValidateAppointmentData(patient, doctor, dateTime, validation, duration);
            int appointmentId = 0;

            if (type == nameof(Examination))
            {
                appointmentId = _examinationRepository.NewId();
                Examination examination = new Examination(appointmentId, doctor, patient, dateTime,
                                          new List<Prescription>());
                patient.Examinations.Add(examination);
                doctor.Examinations.Add(examination);
                _roomRepository.FindAvailableRoom(examination, dateTime);
                _examinationRepository.Add(examination);
                _examinationReferencesRepository.Add(examination);
                _examinationChangeRepository.Add(examination, dateTime, true, AppointmentStatus.CREATED);

            }
            else if (type == nameof(Operation))
            {
                appointmentId = _operationRepository.NewId();
                Operation operation = new Operation(appointmentId, doctor, patient, dateTime, duration);
                patient.Operations.Add(operation);
                doctor.Operations.Add(operation);
                _roomRepository.FindAvailableRoom(operation, dateTime);
                _operationRepository.Add(operation);
                _operationReferencesRepository.Add(operation);

            }
            return true;
        }

        public bool RescheduleExamination(Appointment appointment, DateTime dateTime, bool validation = true)
        {
            ValidateAppointmentData(appointment.Patient, appointment.Doctor, dateTime, validation);

            _roomRepository.FindAvailableRoom(appointment, dateTime);
            bool resolved = true;
            if (resolved)
            {
                appointment.Date = dateTime;
            }

            if (appointment is Examination)
            {
                _examinationReferencesRepository.Remove((Examination)appointment);
                _examinationReferencesRepository.Add((Examination)appointment);
                _examinationChangeRepository.Add((Examination)appointment, dateTime, resolved, AppointmentStatus.EDITED);
            }
            else if (appointment is Operation)
            {
                _operationReferencesRepository.Remove((Operation)appointment);
                _operationReferencesRepository.Add((Operation)appointment);
            }
            return resolved;
        }

        public void ValidateAppointmentData(Patient patient, Doctor doctor, DateTime dateTime, bool validation, int duration = 15)
        {
            DoctorService doctorService = new DoctorService(doctor);
            PatientService patientService = new PatientService(patient);

            if (DateTime.Compare(DateTime.Now, dateTime) > 0 && validation)
            {
                throw new DateException("Date must be in future !");
            }
            if ((dateTime - DateTime.Now).TotalDays < 1 && validation)
            {
                throw new DateException("Cannot schedule in next 24 hours");
            }
            if (doctor is null)
            {
                throw new EmptyFieldException("Doctor not selected !");
            }
            if (patient is null)
            {
                throw new EmptyFieldException("Patient not selected !");
            }
            if (!patientService.IsAvailable(dateTime, duration))
            {
                throw new UserNotAvailableException("Patient not available at selected time !");
            }
            if (!doctorService.IsAvailable(dateTime, duration))
            {
                throw new UserNotAvailableException("Doctor not available at selected time !");
            }
        }
    }
}
