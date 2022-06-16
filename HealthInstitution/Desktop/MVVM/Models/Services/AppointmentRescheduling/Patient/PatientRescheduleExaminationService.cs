using HealthInstitution.Core;
using HealthInstitution.Core.Repositories;
using HealthInstitution.Core.Repositories.References;
using HealthInstitution.Core.Repository;
using HealthInstitution.Core.Services.Rooms;
using HealthInstitution.Core.Services.ValidationServices;
using System;

namespace HealthInstitution.Services
{
    internal class PatientRescheduleExaminationService : IRescheduleExamination
    {
        private readonly IExaminationRelationsRepositoryService _examinationRelationsRepository;
        private readonly IExaminationChangeRepositoryService _examinationChangeRepository;


        public PatientRescheduleExaminationService()
        {
            _examinationRelationsRepository = new ExaminationRelationsRepositoryService();
            _examinationChangeRepository = new ExaminationChangeRepositoryService();
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

            _examinationRelationsRepository.Remove(examination);
            _examinationRelationsRepository.Add(examination);
            _examinationChangeRepository.Add(examination, dateTime, resolved, AppointmentStatus.EDITED);

            return resolved;

        }
    }
}
