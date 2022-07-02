using System;
using HealthInstitution.Core;
using HealthInstitution.Core.Repositories;
using HealthInstitution.Core.Repositories.References;
using HealthInstitution.Core.Repository;
using HealthInstitution.Core.Services;

namespace HealthInstitution.Services.Doctor
{
    class DoctorRescheduleAppointmentService : IRescheduleExamination, IRescheduleOperation
    {
        private IExaminationRepositoryService _examinationRepository;
        private IRoomRepositoryService _roomRepository;
        private IExaminationRelationsRepositoryService _examinationReferencesRepository;
        private IExaminationChangeRepositoryService _examinationChangeRepository;
        private IOperationRelationsRepositoryService _operationReferencesRepository;


        public DoctorRescheduleAppointmentService()
        {
            _examinationRepository = new ExaminationRepositoryService();
            _roomRepository = new RoomRepositoryService();
            _examinationReferencesRepository = new ExaminationRelationsRepositoryService();
            _examinationChangeRepository = new ExaminationChangeRepositoryService();
            _operationReferencesRepository = new OperationRelationsRepositoryService();
        }

        public bool RescheduleAppointment(Appointment appointment, DateTime dateTime, bool validation = true)
        {
            new ValidationService().ValidateAppointmentData(appointment, dateTime, validation);

            FindAvailableRoomService service = new FindAvailableRoomService();
            service.FindAvailableRoom(appointment, dateTime);
        
            appointment.Date = dateTime;


            if (appointment is Examination) return RescheduleExamination((Examination)appointment, dateTime);
            else return RescheduleOperation((Operation)appointment, dateTime);
        }

        public bool RescheduleExamination(Examination examination, DateTime dateTime)
        {
           
            _examinationReferencesRepository.Remove(examination);
            _examinationReferencesRepository.Add(examination);
            _examinationChangeRepository.Add(examination, dateTime, true, AppointmentStatus.EDITED);
            return true;

        }

        public bool RescheduleOperation(Operation operation, DateTime dateTime)
        {
            _operationReferencesRepository.Remove(operation);
            _operationReferencesRepository.Add(operation);
            return true;
        }
    }
}
