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

        public bool CreateAppointment(Doctor doctor, Patient patient, DateTime dateTime, string type, int duration = 15, bool validation = true)
        {
            DoctorService doctorService = new DoctorService(doctor);
          
            if (!doctorService.IsAvailable(dateTime, duration))
            {
                return false;
            }
            if (!patient.IsAvailable(dateTime, duration))
            {
                return false;
            }
            
            new ValidationService().ValidateAppointmentData(patient, doctor, dateTime, validation, duration);

            int appointmentId = 0;

            if (type == nameof(Examination))
            {

                appointmentId = _examinationRepository.NewId();
                //int prescriptionId = _prescriptionRepository.GetNewId();

                Examination examination = new Examination(appointmentId, doctor, patient, dateTime,
                                          new List<Prescription>());
                patient.Examinations.Add(examination);
                doctor.Examinations.Add(examination);
                _roomRepository.FindAvailableRoom(examination, dateTime);
                _examinationRepository.Add(examination);
                _examinationReferencesRepository.Add(examination);
                _examinationChangeRepository.Add(examination, dateTime, true, AppointmentStatus.CREATED);

            }

            else if (type == nameof(Operation))
            {
                appointmentId = _operationRepository.NewId();
                Operation operation = new Operation(appointmentId, doctor, patient, dateTime, duration);
                patient.Operations.Add(operation);
                doctor.Operations.Add(operation);
                _roomRepository.FindAvailableRoom(operation, dateTime);
                _operationRepository.Add(operation);
                _operationReferencesRepository.Add(operation);

            }

            return true;
        }
    }
}
