using HealthInstitution.Core;
using HealthInstitution.Core.Repositories;
using HealthInstitution.Core.Repositories.References;
using HealthInstitution.Core.Repository;

namespace HealthInstitution.Services
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

        public bool CancelExamination(Examination examination)
        {
            Patient patient = examination.Patient;
            Doctor doctor = examination.Doctor;
            Room room = examination.Room;

            _examinationChangeRepository.RemoveByAppointmentId(examination.ID);
            patient.Examinations.Remove(examination);
            doctor.Examinations.Remove(examination);
            _examinationRepository.Remove(examination);
            _examinationReferencesRepository.Remove(examination);
            
            _examinationChangeRepository.Add(examination, examination.Date, true, AppointmentStatus.DELETED);

             room.Appointments.Remove(examination);
             return true;
        }

        public bool CancelOperation(Operation operation)
        {
            Patient patient = operation.Patient;
            Doctor doctor = operation.Doctor;
            Room room = operation.Room;

            _examinationChangeRepository.RemoveByAppointmentId(operation.ID);
            patient.Operations.Remove(operation);
            doctor.Operations.Remove(operation);
            _operationRepository.Remove(operation);
            _operationReferencesRepository.Remove(operation);

            room.Appointments.Remove(operation);
            return true;
        }
    }
}
