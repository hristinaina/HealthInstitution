using HealthInstitution.Core;
using HealthInstitution.Core.Repositories;
using HealthInstitution.Core.Repositories.References;
using HealthInstitution.Core.Repository;

namespace HealthInstitution.Services
{
    internal class PatientCancelExaminationService : ICancelExamination
    {
        private readonly IExaminationRepositoryService _examinationRepository;
        private readonly IExaminationRelationsRepositoryService _examinationReferencesRepository;
        private readonly IExaminationChangeRepositoryService _examinationChangeRepository;

        public PatientCancelExaminationService()
        {
            _examinationRepository = new ExaminationRepositoryService();
            _examinationReferencesRepository = new ExaminationRelationsRepositoryService();
            _examinationChangeRepository = new ExaminationChangeRepositoryService();
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
            _examinationChangeRepository.Add(examination, examination.Date, resolved, AppointmentStatus.DELETED);
            return resolved;
        }
    }
}
