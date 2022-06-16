using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Core;
using HealthInstitution.Core.Repositories;
using HealthInstitution.Core.Repositories.References;
using HealthInstitution.Core.Repository;

namespace HealthInstitution.Core.Services
{
    class DoctorCancelAppointmentService
    {
        private IExaminationRepositoryService _examinationRepository;
        private RoomRepository _roomRepository;
        private IExaminationRelationsRepositoryService _examinationReferencesRepository;
        private IExaminationChangeRepositoryService _examinationChangeRepository;
        private OperationRepository _operationRepository;
        private OperationReferencesRepository _operationReferencesRepository;


        public DoctorCancelAppointmentService()
        {
            _examinationRepository = new ExaminationRepositoryService() ;
            _roomRepository = Institution.Instance().RoomRepository;
            _examinationReferencesRepository = new ExaminationRelationsRepositoryService();
            _examinationChangeRepository = new ExaminationChangeRepositoryService();
            _operationRepository = Institution.Instance().OperationRepository;
            _operationReferencesRepository = Institution.Instance().OperationReferencesRepository;
        }


        public bool CancelExamination(Appointment appointment)
        {
            Patient patient = appointment.Patient;
            Doctor doctor = appointment.Doctor;
            Room room = appointment.Room;
            bool resolved = true;

            if (appointment is Examination)
            {
                if (resolved)
                {
                    _examinationChangeRepository.RemoveByAppointmentId(appointment.ID);
                    patient.Examinations.Remove((Examination)appointment);
                    doctor.Examinations.Remove((Examination)appointment);
                    _examinationRepository.Remove((Examination)appointment);
                    _examinationReferencesRepository.Remove((Examination)appointment);
                }
                _examinationChangeRepository.Add((Examination)appointment, appointment.Date, resolved, AppointmentStatus.DELETED);
            }

            else if (appointment is Operation)
            {
                _examinationChangeRepository.RemoveByAppointmentId(appointment.ID);
                patient.Operations.Remove((Operation)appointment);
                doctor.Operations.Remove((Operation)appointment);
                _operationRepository.Remove((Operation)appointment);
                _operationReferencesRepository.Remove((Operation)appointment);
            }

            if (resolved) room.Appointments.Remove(appointment);
            return resolved;
        }
    }
}
