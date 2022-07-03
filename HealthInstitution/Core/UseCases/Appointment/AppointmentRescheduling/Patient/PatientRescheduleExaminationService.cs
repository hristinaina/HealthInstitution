using HealthInstitution.Core;
using HealthInstitution.Core.Exceptions;
using HealthInstitution.Core.Repository;
using HealthInstitution.Core.Services;
using HealthInstitution.Core.Services.DoctorServices;
using HealthInstitution.Core.Services.ValidationServices;
using System;

namespace HealthInstitution.Services
{
    internal class PatientRescheduleExaminationService : IRescheduleExamination
    {
        private readonly IExaminationRelationsRepositoryService _examinationRelationsRepository;
        private readonly IExaminationChangeRepositoryService _examinationChangeRepository;
        private readonly ITroll _trollingService;

        public PatientRescheduleExaminationService()
        {
            _examinationRelationsRepository = new ExaminationRelationsRepositoryService();
            _examinationChangeRepository = new ExaminationChangeRepositoryService();
            _trollingService = new TrollingService();
        }

        public bool RescheduleExamination(Examination examination, DateTime dateTime)
        {
            if (_trollingService.IsTrolling(examination.Patient))
            {
                throw new PatientBlockedException("System has blocked your account !");
            }

            new PatientExaminationValidationService().ValidateAppointmentData(examination, dateTime);

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
