using HealthInstitution.Exceptions;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Enumerations;
using HealthInstitution.MVVM.Models.Repositories;
using HealthInstitution.MVVM.Models.Repositories.References;
using HealthInstitution.MVVM.Models.Services.DoctorServices;
using HealthInstitution.MVVM.Models.Services.Rooms;
using HealthInstitution.MVVM.Models.Services.ValidationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.Models.Services
{
    class PatientScheduleAppointmentService
    {
        private Appointment _appointment;
        private Patient _patient;
        private ExaminationRepository _examinationRepository;
        private RoomRepository _roomRepository;
        private ExaminationReferencesRepository _examinationReferencesRepository;
        private ExaminationChangeRepository _examinationChangeRepository;
        private OperationRepository _operationRepository;
        private OperationReferencesRepository _operationReferencesRepository;


        public PatientScheduleAppointmentService(Patient patient)
        {
            _patient = patient;
            _examinationRepository = Institution.Instance().ExaminationRepository;
            _roomRepository = Institution.Instance().RoomRepository;
            _examinationReferencesRepository = Institution.Instance().ExaminationReferencesRepository;
            _examinationChangeRepository = Institution.Instance().ExaminationChangeRepository;
            _operationRepository = Institution.Instance().OperationRepository;
            _operationReferencesRepository = Institution.Instance().OperationReferencesRepository;
        }

        public bool CreateAppointment(Doctor doctor, DateTime dateTime)
        {
            TrollingService trollingService = new TrollingService(_patient);
            if (trollingService.IsTrolling())
            {
                throw new PatientBlockedException("System has blocked your account !");
            }


            int appointmentId = _examinationRepository.GetID();

            Examination examination = new Examination(appointmentId, doctor, _patient, dateTime,
                                      new List<Prescription>());
            new PatientAppointmentValidationService(examination, dateTime).ValidateAppointmentData();
            _patient.Examinations.Add(examination);
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