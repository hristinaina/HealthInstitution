using System;
using System.Collections.Generic;
using HealthInstitution.Core.Repositories;
using HealthInstitution.Core.Repositories.References;
using HealthInstitution.Core.Repository;
using HealthInstitution.Core.Services;
using HealthInstitution.Core;
using HealthInstitution.Core.Repository;

namespace HealthInstitution.Core.Services
{
    class DoctorScheduleAppointmentService
    {
        private IExaminationRepositoryService _examinationRepository;
        private IRoomRepositoryService _roomRepository;
        private IExaminationRelationsRepositoryService _examinationReferencesRepository;
        private IExaminationChangeRepositoryService _examinationChangeRepository;
        private IOperationRepositoryService _operationRepository;
        private IOperationRelationsRepositoryService _operationReferencesRepository;


        public DoctorScheduleAppointmentService()
        {
            _examinationRepository = new ExaminationRepositoryService();
            _roomRepository = new RoomRepositoryService();
            _examinationReferencesRepository = new ExaminationRelationsRepositoryService();
            _examinationChangeRepository = new ExaminationChangeRepositoryService();
            _operationRepository = new OperationRepositoryService();
            _operationReferencesRepository = new OperationRelationsRepositoryService();
        }

        public bool CreateAppointment(Appointment appointment, DateTime dateTime, bool validation = true)
        {
            DoctorService doctorService = new DoctorService(appointment.Doctor);
            ExaminationService examinationService = new ExaminationService();
            int duration = examinationService.GetDuration(appointment);
            
            new ValidationService().ValidateAppointmentData(appointment, dateTime, validation);

            int appointmentId = 0;

            if (appointment.GetType() == typeof(Examination))
            {
                appointmentId = _examinationRepository.GetNewID();
                Examination examination = new Examination(appointmentId, appointment.Doctor, appointment.Patient, dateTime,
                                          new List<Prescription>());
                appointment.Patient.Examinations.Add(examination);
                appointment.Doctor.Examinations.Add(examination);
                FindAvailableRoomService service = new FindAvailableRoomService();
                service.FindAvailableRoom(examination, dateTime);
                _examinationRepository.Add(examination);
                _examinationReferencesRepository.Add(examination);
                _examinationChangeRepository.Add(examination, dateTime, true, AppointmentStatus.CREATED);

            }

            else
            {
                appointmentId = _operationRepository.GetNewID();
                Operation operation = new Operation(appointmentId, appointment.Doctor, appointment.Patient, dateTime, duration);
                appointment.Patient.Operations.Add(operation);
                appointment.Doctor.Operations.Add(operation);
                FindAvailableRoomService service = new FindAvailableRoomService();
                service.FindAvailableRoom(operation, dateTime);
                _operationRepository.Add(operation);
                _operationReferencesRepository.Add(operation);

            }

            return true;
        }
    }
}
