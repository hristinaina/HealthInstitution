using HealthInstitution.Core;
using HealthInstitution.Core.Exceptions;
using HealthInstitution.Core.Repository;
using HealthInstitution.Core.Services;
using HealthInstitution.Core.Services.DoctorServices;
using HealthInstitution.Core.Services.ValidationServices;
using System;
using System.Collections.Generic;

namespace HealthInstitution.Services
{
    public class PatientScheduleExaminationService : IScheduleExamination
    {
        private readonly IExaminationRepositoryService _examinationRepository;
        private readonly IExaminationRelationsRepositoryService _examinationReferencesRepository;
        private readonly IExaminationChangeRepositoryService _examinationChangeRepository;
        private readonly ITroll _trollingService;


        public PatientScheduleExaminationService()
        {
            _examinationRepository = new ExaminationRepositoryService();
            _examinationReferencesRepository = new ExaminationRelationsRepositoryService();
            _examinationChangeRepository = new ExaminationChangeRepositoryService();
            _trollingService = new TrollingService();
        }

        public bool CreateExamination(Patient patient, Core.Doctor doctor, DateTime dateTime)
        {
            if (_trollingService.IsTrolling(patient))
            {
                throw new PatientBlockedException("System has blocked your account !");
            }
            int appointmentId = _examinationRepository.GetNewID();

            Examination examination = new Examination(appointmentId, doctor, patient, dateTime,
                                      new List<Prescription>());
            new PatientExaminationValidationService().ValidateAppointmentData(examination, dateTime);
            patient.Examinations.Add(examination);
            doctor.Examinations.Add(examination);
            FindAvailableRoomService service = new FindAvailableRoomService();
            service.FindAvailableRoom(examination, dateTime);
            _examinationRepository.Add(examination);
            _examinationReferencesRepository.Add(examination);
            _examinationChangeRepository.Add(examination, dateTime, true, AppointmentStatus.CREATED);

            return true;
        }

    }
}