using HealthInstitution.Core;
using HealthInstitution.Core.Repositories;
using HealthInstitution.Core.Repositories.References;
using HealthInstitution.Core.Services.Rooms;
using HealthInstitution.Core.Services.ValidationServices;
using System;

namespace HealthInstitution.Services
{
    internal class PatientRescheduleExaminationService : IRescheduleExamination
    {
        private readonly ExaminationReferencesRepository examinationReferencesRepository;
        private readonly ExaminationChangeRepository examinationChangeRepository;


        public PatientRescheduleExaminationService()
        {
            examinationReferencesRepository = Institution.Instance().ExaminationReferencesRepository;
            examinationChangeRepository = Institution.Instance().ExaminationChangeRepository;
        }

        public bool RescheduleExamination(Examination examination, DateTime dateTime)
        {
            new PatientExaminationValidationService(examination, dateTime).ValidateAppointmentData();

            FindAvailableRoomService service = new FindAvailableRoomService();
            service.FindAvailableRoom(examination, dateTime);
            bool resolved = examination.IsEditable();
            if (resolved)
            {
                examination.Date = dateTime;
            }

            examinationReferencesRepository.Remove(examination);
            examinationReferencesRepository.Add(examination);
            examinationChangeRepository.Add(examination, dateTime, resolved, AppointmentStatus.EDITED);

            return resolved;

        }
    }
}
