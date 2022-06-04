using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Repositories;
using HealthInstitution.MVVM.Models.Repositories.References;
using HealthInstitution.MVVM.Models.Enumerations;

namespace HealthInstitution.MVVM.Models.Services
{
    class DoctorCancelAppointmentService
    {
        private ExaminationRepository _examinationRepository;
        private RoomRepository _roomRepository;
        private ExaminationReferencesRepository _examinationReferencesRepository;
        private ExaminationChangeRepository _examinationChangeRepository;
        private OperationRepository _operationRepository;
        private OperationReferencesRepository _operationReferencesRepository;


        public DoctorCancelAppointmentService()
        {
            _examinationRepository = Institution.Instance().ExaminationRepository;
            _roomRepository = Institution.Instance().RoomRepository;
            _examinationReferencesRepository = Institution.Instance().ExaminationReferencesRepository;
            _examinationChangeRepository = Institution.Instance().ExaminationChangeRepository;
            _operationRepository = Institution.Instance().OperationRepository;
            _operationReferencesRepository = Institution.Instance().OperationReferencesRepository;
        }


        public bool CancelExamination(Entities.Appointment appointment)
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
