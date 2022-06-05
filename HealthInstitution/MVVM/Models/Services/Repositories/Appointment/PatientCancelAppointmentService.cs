using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Enumerations;
using HealthInstitution.MVVM.Models.Repositories;
using HealthInstitution.MVVM.Models.Repositories.References;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.Models.Services.Repositories.Appointment
{
    class PatientCancelAppointmentService
    {
        private Examination _examination;
        private ExaminationRepository _examinationRepository;
        private RoomRepository _roomRepository;
        private ExaminationReferencesRepository _examinationReferencesRepository;
        private ExaminationChangeRepository _examinationChangeRepository;
        private OperationRepository _operationRepository;
        private OperationReferencesRepository _operationReferencesRepository;


        public PatientCancelAppointmentService(Examination examination)
        {
            _examination = examination;
            _examinationRepository = Institution.Instance().ExaminationRepository;
            _roomRepository = Institution.Instance().RoomRepository;
            _examinationReferencesRepository = Institution.Instance().ExaminationReferencesRepository;
            _examinationChangeRepository = Institution.Instance().ExaminationChangeRepository;
            _operationRepository = Institution.Instance().OperationRepository;
            _operationReferencesRepository = Institution.Instance().OperationReferencesRepository;
        }


        public bool CancelExamination()
        {
            Patient patient = _examination.Patient;
            Doctor doctor = _examination.Doctor;
            Room room = _examination.Room;
            bool resolved = _examination.IsEditable();

            if (resolved)
            {
                _examinationChangeRepository.RemoveByAppointmentId(_examination.ID);
                patient.Examinations.Remove((Examination)_examination);
                doctor.Examinations.Remove((Examination)_examination);
                _examinationRepository.Remove((Examination)_examination);
                _examinationReferencesRepository.Remove((Examination)_examination);
                room.Appointments.Remove(_examination);
            }
            _examinationChangeRepository.Add((Examination)_examination, _examination.Date, resolved, AppointmentStatus.DELETED);
            return resolved;
        }
    }
}
