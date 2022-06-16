using HealthInstitution.Core;
using HealthInstitution.Core.Repositories;
using HealthInstitution.Core.Repositories.References;
using HealthInstitution.Core.Repository;

namespace HealthInstitution.Services.Doctor
{
    class DoctorCancelAppointmentService : ICancelExamination, ICancelOperation
    {
        private IExaminationRepositoryService _examinationRepository;
        private IRoomRepositoryService _roomRepository;
        private IExaminationRelationsRepositoryService _examinationReferencesRepository;
        private IExaminationChangeRepositoryService _examinationChangeRepository;
        private IOperationRepositoryService _operationRepository;
        private IOperationRelationsRepositoryService _operationReferencesRepository;


        public DoctorCancelAppointmentService()
        {
            _examinationRepository = new ExaminationRepositoryService() ;
            _roomRepository = new RoomRepositoryService();
            _examinationReferencesRepository = new ExaminationRelationsRepositoryService();
            _examinationChangeRepository = new ExaminationChangeRepositoryService();
            _operationRepository = new OperationRepositoryService();
            _operationReferencesRepository = new OperationRelationsRepositoryService();
        }

        public bool CancelAppointment(Appointment appointment)
        {
            Room room = appointment.Room;

            bool isDone;
            if (appointment is Examination) isDone = CancelExamination((Examination)appointment);
            else isDone = CancelOperation((Operation)appointment);

            room.Appointments.Remove(appointment);
            return isDone;
        }

        public bool CancelExamination(Examination examination)
        {

            _examinationChangeRepository.RemoveByAppointmentId(examination.ID);
            examination.Patient.Examinations.Remove(examination);
            examination.Doctor.Examinations.Remove(examination);
            _examinationRepository.Remove(examination);
            _examinationReferencesRepository.Remove(examination);
            
            _examinationChangeRepository.Add(examination, examination.Date, true, AppointmentStatus.DELETED);

             return true;
        }

        public bool CancelOperation(Operation operation)
        {
            operation.Patient.Operations.Remove(operation);
            operation.Doctor.Operations.Remove(operation);
            _operationRepository.Remove(operation);
            _operationReferencesRepository.Remove(operation);

            return true;
        }
    }
}
