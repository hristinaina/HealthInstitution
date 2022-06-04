using HealthInstitution.Exceptions;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Enumerations;
using HealthInstitution.MVVM.Models.Repositories;
using HealthInstitution.MVVM.Models.Repositories.References;
using HealthInstitution.MVVM.Models.Services.DoctorServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.Models.Services
{
    class PatientAppointmentSchedulingService
    {
        private Patient _patient;
        private ExaminationRepository _examinationRepository;
        private RoomRepository _roomRepository;
        private ExaminationReferencesRepository _examinationReferencesRepository;
        private ExaminationChangeRepository _examinationChangeRepository;
        private OperationRepository _operationRepository;
        private OperationReferencesRepository _operationReferencesRepository;


        public PatientAppointmentSchedulingService(Patient patient)
        {
            _patient = patient;
            _examinationRepository = Institution.Instance().ExaminationRepository;
            _roomRepository = Institution.Instance().RoomRepository;
            _examinationReferencesRepository = Institution.Instance().ExaminationReferencesRepository;
            _examinationChangeRepository = Institution.Instance().ExaminationChangeRepository;
            _operationRepository = Institution.Instance().OperationRepository;
            _operationReferencesRepository = Institution.Instance().OperationReferencesRepository;
        }

        public bool CreateAppointment(Doctor doctor, Patient patient, DateTime dateTime, string type, int duration = 15, bool validation = true)
        {
            CheckTrolling();
            PatientService patientService = new PatientService(patient);
            if (!patientService.IsAvailable(dateTime, duration))
            {
                return false;
            }

            new ValidationService().ValidateAppointmentData(patient, doctor, dateTime, validation, duration);

            int appointmentId = _examinationRepository.NewId();
            //int prescriptionId = _prescriptionRepository.GetNewId();

            Examination examination = new Examination(appointmentId, doctor, patient, dateTime,
                                      new List<Prescription>());
            patient.Examinations.Add(examination);
            doctor.Examinations.Add(examination);
            _roomRepository.FindAvailableRoom(examination, dateTime);
            _examinationRepository.Add(examination);
            _examinationReferencesRepository.Add(examination);
            _examinationChangeRepository.Add(examination, dateTime, true, AppointmentStatus.CREATED);
            return true;
        }

        public bool RescheduleExamination(Examination examination, DateTime dateTime, bool validation = true)
        {
            CheckTrolling();
            new ValidationService().ValidateAppointmentData(examination.Patient, examination.Doctor, dateTime, validation);

            _roomRepository.FindAvailableRoom(examination, dateTime);
            bool resolved = examination.IsEditable();
            if (resolved)
            {
                examination.Date = dateTime;
            }

            _examinationReferencesRepository.Remove(examination);
            _examinationReferencesRepository.Add(examination);
            _examinationChangeRepository.Add(examination, dateTime, resolved, AppointmentStatus.EDITED);

            return resolved;

        }

        public bool CancelExamination(Examination examination)
        {
            Patient patient = examination.Patient;
            Doctor doctor = examination.Doctor;
            Room room = examination.Room;
            bool resolved = examination.IsEditable();

            if (resolved)
            {
                _examinationChangeRepository.RemoveByAppointmentId(examination.ID);
                patient.Examinations.Remove(examination);
                doctor.Examinations.Remove(examination);
                _examinationRepository.Remove(examination);
                _examinationReferencesRepository.Remove(examination);
                room.Appointments.Remove(examination);
            }
            _examinationChangeRepository.Add((Examination)examination, examination.Date, resolved, AppointmentStatus.DELETED);
            return resolved;
        }

        public void CheckTrolling()
        {
            TrollingService trollingService = new TrollingService(_patient);
            if (trollingService.IsTrolling())
                throw new PatientBlockedException("System has blocked your account !");
        }

    }
}