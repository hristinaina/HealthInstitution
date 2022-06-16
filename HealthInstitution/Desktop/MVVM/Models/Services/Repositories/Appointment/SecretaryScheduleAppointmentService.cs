using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HealthInstitution.Core.Repositories;
using HealthInstitution.Core.Repositories.References;
using HealthInstitution.Core.Services.DoctorServices;
using HealthInstitution.Core.Services.Rooms;

namespace HealthInstitution.Core.Services
{
    public class SecretaryScheduleAppointmentService
    {
        private readonly ExaminationRepository _examinationRepository;
        private readonly OperationRepository _operationRepository;
        private readonly ExaminationReferencesRepository _examinationReferencesRepository;
        private readonly OperationReferencesRepository _operationReferencesRepository;
        private readonly ExaminationChangeRepository _examinationChangeRepository;

        public SecretaryScheduleAppointmentService()
        {
            _examinationRepository = Institution.Instance().ExaminationRepository;
            _operationRepository = Institution.Instance().OperationRepository;
            _examinationReferencesRepository = Institution.Instance().ExaminationReferencesRepository;
            _operationReferencesRepository = Institution.Instance().OperationReferencesRepository;
            _examinationChangeRepository = Institution.Instance().ExaminationChangeRepository;
        }

        public bool ScheduleAppointment(Appointment appointment, int duration, bool validation = true)
        {
            DoctorService doctorService = new(appointment.Doctor);
            PatientService patientService = new(appointment.Patient);

            if (!doctorService.IsAvailable(appointment.Date, duration)) return false;
            if (!patientService.IsAvailable(appointment.Date, duration)) return false;

            new ValidationService().ValidateAppointmentData(appointment, appointment.Date, validation);

            if (duration == 15)
            {
                int appointmentId = _examinationRepository.GetNewID();
                Examination examination = new Examination(appointmentId, appointment.Doctor, appointment.Patient, appointment.Date,
                                          new List<Prescription>());
                CreateExamination(examination, appointment);
            }
            else
            {
                int appointmentId = _operationRepository.GetNewID();
                Operation operation = new Operation(appointmentId, appointment.Doctor, appointment.Patient, appointment.Date, duration);
                CreateOperation(operation, appointment);
            }
            return true;
        }

        private void CreateExamination(Examination examination, Appointment appointment)
        {
            appointment.Patient.Examinations.Add(examination);
            appointment.Doctor.Examinations.Add(examination);
            FindAvailableRoomService service = new FindAvailableRoomService();
            service.FindAvailableRoom(examination, appointment.Date);
            _examinationRepository.Add(examination);
            _examinationReferencesRepository.Add(examination);
            _examinationChangeRepository.Add(examination, appointment.Date, true, AppointmentStatus.CREATED);
        }

        private void CreateOperation(Operation operation, Appointment appointment)
        {
            appointment.Patient.Operations.Add(operation);
            appointment.Doctor.Operations.Add(operation);
            FindAvailableRoomService service = new FindAvailableRoomService();
            service.FindAvailableRoom(operation, appointment.Date);
            _operationRepository.Add(operation);
            _operationReferencesRepository.Add(operation);
        }
    }
}
