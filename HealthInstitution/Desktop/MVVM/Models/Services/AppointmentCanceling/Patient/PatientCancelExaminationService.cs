using HealthInstitution.Core;
using HealthInstitution.Core.Repositories;
using HealthInstitution.Core.Repositories.References;

namespace HealthInstitution.Services
{
    internal class PatientCancelExaminationService : ICancelExamination
    {
        private readonly ExaminationRepository _examinationRepository;
        private readonly ExaminationReferencesRepository _examinationReferencesRepository;
        private readonly ExaminationChangeRepository _examinationChangeRepository;

        public PatientCancelExaminationService()
        {
            _examinationRepository = Institution.Instance().ExaminationRepository;
            _examinationReferencesRepository = Institution.Instance().ExaminationReferencesRepository;
            _examinationChangeRepository = Institution.Instance().ExaminationChangeRepository;
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
