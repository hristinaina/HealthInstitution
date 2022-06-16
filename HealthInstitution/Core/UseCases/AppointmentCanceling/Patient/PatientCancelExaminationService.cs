using HealthInstitution.Core;
using HealthInstitution.Core.Exceptions;
using HealthInstitution.Core.Repository;
using HealthInstitution.Core.Services.DoctorServices;

namespace HealthInstitution.Services
{
    internal class PatientCancelExaminationService : ICancelExamination
    {
        private readonly IExaminationRepositoryService _examinationRepository;
        private readonly IExaminationRelationsRepositoryService _examinationReferencesRepository;
        private readonly IExaminationChangeRepositoryService _examinationChangeRepository;
        private readonly ITroll _trollingService;

        public PatientCancelExaminationService()
        {
            _examinationRepository = new ExaminationRepositoryService();
            _examinationReferencesRepository = new ExaminationRelationsRepositoryService();
            _examinationChangeRepository = new ExaminationChangeRepositoryService();
            _trollingService = new TrollingService();
        }

        public bool CancelExamination(Examination examination)
        {
            Patient patient = examination.Patient;
            Core.Doctor doctor = examination.Doctor;
            Room room = examination.Room;
            bool resolved = examination.IsEditable();

            if (_trollingService.IsTrolling(patient))
            {
                throw new PatientBlockedException("System has blocked your account !");
            }

            if (resolved)
            {
                _examinationChangeRepository.RemoveByAppointmentId(examination.ID);
                patient.Examinations.Remove(examination);
                doctor.Examinations.Remove(examination);
                _examinationRepository.Remove(examination);
                _examinationReferencesRepository.Remove(examination);
                room.Appointments.Remove(examination);
            }
            _examinationChangeRepository.Add(examination, examination.Date, resolved, AppointmentStatus.DELETED);
            return resolved;
        }
    }
}
