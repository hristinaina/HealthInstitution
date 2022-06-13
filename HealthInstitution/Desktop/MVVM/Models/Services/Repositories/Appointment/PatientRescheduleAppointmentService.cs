using HealthInstitution.Core.Repositories;
using HealthInstitution.Core.Repositories.References;
using HealthInstitution.Core.Services.Rooms;
using HealthInstitution.Core.Services.ValidationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Services
{
    class PatientRescheduleAppointmentService
    {
        private Appointment _appointment;
        private ExaminationRepository _examinationRepository;
        private RoomRepository _roomRepository;
        private ExaminationReferencesRepository _examinationReferencesRepository;
        private ExaminationChangeRepository _examinationChangeRepository;
        private OperationReferencesRepository _operationReferencesRepository;


        public PatientRescheduleAppointmentService(Appointment appointment)
        {
            _appointment = appointment;
            _examinationRepository = Institution.Instance().ExaminationRepository;
            _roomRepository = Institution.Instance().RoomRepository;
            _examinationReferencesRepository = Institution.Instance().ExaminationReferencesRepository;
            _examinationChangeRepository = Institution.Instance().ExaminationChangeRepository;
            _operationReferencesRepository = Institution.Instance().OperationReferencesRepository;
        }

        public bool RescheduleExamination(DateTime dateTime)
        {
            new PatientAppointmentValidationService(_appointment, dateTime).ValidateAppointmentData();

            FindAvailableRoomService service = new FindAvailableRoomService();
            service.FindAvailableRoom(_appointment, dateTime);
            bool resolved = _appointment.IsEditable();
            if (resolved)
            {
                _appointment.Date = dateTime;
            }

            if (_appointment is Examination)
            {
                _examinationReferencesRepository.Remove((Examination)_appointment);
                _examinationReferencesRepository.Add((Examination)_appointment);
                _examinationChangeRepository.Add((Examination)_appointment, dateTime, resolved, AppointmentStatus.EDITED);
            }

            else if (_appointment is Operation)
            {
                _operationReferencesRepository.Remove((Operation)_appointment);
                _operationReferencesRepository.Add((Operation)_appointment);
            }

            return resolved;

        }
    }
}
