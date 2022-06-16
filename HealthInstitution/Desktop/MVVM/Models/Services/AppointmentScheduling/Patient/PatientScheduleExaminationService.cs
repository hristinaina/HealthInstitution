using HealthInstitution.Core.Exceptions;
using HealthInstitution.Core;
using HealthInstitution.Core;
using HealthInstitution.Core.Repositories;
using HealthInstitution.Core.Repositories.References;
using HealthInstitution.Core.Services.DoctorServices;
using HealthInstitution.Core.Services.Rooms;
using HealthInstitution.Core.Services.ValidationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Services
{
    public class PatientScheduleExaminationService : IScheduleExamination
    {
        private ExaminationRepository _examinationRepository;
        private ExaminationReferencesRepository _examinationReferencesRepository;
        private ExaminationChangeRepository _examinationChangeRepository;


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

            int appointmentId = _examinationRepository.GetID();

            Examination examination = new Examination(appointmentId, doctor, patient, dateTime,
                                      new List<Prescription>());
            new PatientAppointmentValidationService(examination, dateTime).ValidateAppointmentData();
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