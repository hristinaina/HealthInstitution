using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.Exceptions;
using HealthInstitution.MVVM.Models.Repositories;
using HealthInstitution.MVVM.Models.Repositories.References;
using HealthInstitution.MVVM.Models.Enumerations;
using HealthInstitution.MVVM.Models.Services.Rooms;

namespace HealthInstitution.MVVM.Models.Services
{
    class DoctorScheduleAppointmentService
    {
        private ExaminationRepository _examinationRepository;
        private RoomRepository _roomRepository;
        private ExaminationReferencesRepository _examinationReferencesRepository;
        private ExaminationChangeRepository _examinationChangeRepository;
        private OperationRepository _operationRepository;
        private OperationReferencesRepository _operationReferencesRepository;


        public DoctorScheduleAppointmentService()
        {
            _examinationRepository = Institution.Instance().ExaminationRepository;
            _roomRepository = Institution.Instance().RoomRepository;
            _examinationReferencesRepository = Institution.Instance().ExaminationReferencesRepository;
            _examinationChangeRepository = Institution.Instance().ExaminationChangeRepository;
            _operationRepository = Institution.Instance().OperationRepository;
            _operationReferencesRepository = Institution.Instance().OperationReferencesRepository;
        }

        public bool CreateAppointment(Appointment appointment, DateTime dateTime, bool validation = true)
        {
            DoctorService doctorService = new DoctorService(appointment.Doctor);
            ExaminationService examinationService = new ExaminationService();
            int duration = examinationService.GetDuration(appointment);
            
            new ValidationService().ValidateAppointmentData(appointment, dateTime, validation);

            int appointmentId = 0;

            if (appointment.GetType() == typeof(Examination))
            {
                appointmentId = _examinationRepository.NewId();
                Examination examination = new Examination(appointmentId, appointment.Doctor, appointment.Patient, dateTime,
                                          new List<Prescription>());
                appointment.Patient.Examinations.Add(examination);
                appointment.Doctor.Examinations.Add(examination);
                FindAvailableRoomService service = new FindAvailableRoomService();
                service.FindAvailableRoom(examination, dateTime);
                _examinationRepository.Add(examination);
                _examinationReferencesRepository.Add(examination);
                _examinationChangeRepository.Add(examination, dateTime, true, AppointmentStatus.CREATED);

            }

            else
            {
                appointmentId = _operationRepository.NewId();
                Operation operation = new Operation(appointmentId, appointment.Doctor, appointment.Patient, dateTime, duration);
                appointment.Patient.Operations.Add(operation);
                appointment.Doctor.Operations.Add(operation);
                FindAvailableRoomService service = new FindAvailableRoomService();
                service.FindAvailableRoom(operation, dateTime);
                _operationRepository.Add(operation);
                _operationReferencesRepository.Add(operation);

            }

            return true;
        }
    }
}
