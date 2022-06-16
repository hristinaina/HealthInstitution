using HealthInstitution.Core;
using HealthInstitution.Core.Exceptions;
using HealthInstitution.Core.Repositories;
using HealthInstitution.Core.Repositories.References;
using HealthInstitution.Core.Services.DoctorServices;
using HealthInstitution.Core.Services;
using HealthInstitution.Core.Services.ValidationServices;
using System;
using System.Collections.Generic;

namespace HealthInstitution.Services
{
    public class PatientScheduleExaminationService : IScheduleExamination
    {
        private readonly ExaminationRepository _examinationRepository;
        private readonly ExaminationReferencesRepository _examinationReferencesRepository;
        private readonly ExaminationChangeRepository _examinationChangeRepository;


        public PatientScheduleExaminationService()
        {
            _examinationRepository = Institution.Instance().ExaminationRepository;
            _examinationReferencesRepository = Institution.Instance().ExaminationReferencesRepository;
            _examinationChangeRepository = Institution.Instance().ExaminationChangeRepository;
        }

        public bool CreateExamination(Patient patient, Doctor doctor, DateTime dateTime)
        {
            TrollingService trollingService = new TrollingService(patient);
            if (trollingService.IsTrolling())
            {
                throw new PatientBlockedException("System has blocked your account !");
            }

            int appointmentId = _examinationRepository.GetNewID();

            Examination examination = new Examination(appointmentId, doctor, patient, dateTime,
                                      new List<Prescription>());
            new PatientExaminationValidationService(examination, dateTime).ValidateAppointmentData();
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