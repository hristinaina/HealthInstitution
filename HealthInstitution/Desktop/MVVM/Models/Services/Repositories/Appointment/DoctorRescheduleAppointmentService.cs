using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Core;
using HealthInstitution.Core.Repositories;
using HealthInstitution.Core.Repositories.References;
using HealthInstitution.Core.Repository;
using HealthInstitution.Core.Services.Rooms;

namespace HealthInstitution.Core.Services
{
    class DoctorRescheduleAppointmentService
    {
        private IExaminationRepositoryService _examinationRepository;
        private RoomRepository _roomRepository;
        private IExaminationRelationsRepositoryService _examinationReferencesRepository;
        private IExaminationChangeRepositoryService _examinationChangeRepository;
        private OperationReferencesRepository _operationReferencesRepository;


        public DoctorRescheduleAppointmentService()
        {
            _examinationRepository = new ExaminationRepositoryService();
            _roomRepository = Institution.Instance().RoomRepository;
            _examinationReferencesRepository = new ExaminationRelationsRepositoryService();
            _examinationChangeRepository = new ExaminationChangeRepositoryService();
            _operationReferencesRepository = Institution.Instance().OperationReferencesRepository;
        }

        public bool RescheduleExamination(Appointment appointment, DateTime dateTime, bool validation = true)
        {
            new ValidationService().ValidateAppointmentData(appointment, dateTime, validation);

            FindAvailableRoomService service = new FindAvailableRoomService();
            service.FindAvailableRoom(appointment, dateTime);
            bool resolved = true;
            if (resolved)
            {
                appointment.Date = dateTime;
            }

            if (appointment is Examination)
            {

                _examinationReferencesRepository.Remove((Examination)appointment);
                _examinationReferencesRepository.Add((Examination)appointment);
                _examinationChangeRepository.Add((Examination)appointment, dateTime, resolved, AppointmentStatus.EDITED);

            }

            else if (appointment is Operation)
            {
                _operationReferencesRepository.Remove((Operation)appointment);
                _operationReferencesRepository.Add((Operation)appointment);
            }

            return resolved;

        }
    }
}
