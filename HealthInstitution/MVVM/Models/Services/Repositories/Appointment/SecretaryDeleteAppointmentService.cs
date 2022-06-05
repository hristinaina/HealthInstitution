using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Repositories;
using HealthInstitution.MVVM.Models.Repositories.References;

namespace HealthInstitution.MVVM.Models.Services
{
    public class SecretaryDeleteAppointmentService
    {
        private readonly ExaminationRepository _examinationRepository;
        private readonly OperationRepository _operationRepository;
        private readonly ExaminationReferencesRepository _examinationReferencesRepository;
        private readonly OperationReferencesRepository _operationReferencesRepository;
        private readonly ExaminationChangeRepository _examinationChangeRepository;

        public SecretaryDeleteAppointmentService()
        {
            _examinationRepository = Institution.Instance().ExaminationRepository;
            _operationRepository = Institution.Instance().OperationRepository;
            _examinationReferencesRepository = Institution.Instance().ExaminationReferencesRepository;
            _operationReferencesRepository = Institution.Instance().OperationReferencesRepository;
            _examinationChangeRepository = Institution.Instance().ExaminationChangeRepository;
        }

        public void DeleteFutureAppointments(Patient patient)
        {
            List<Examination> examinations = new List<Examination>(_examinationRepository.Examinations.ToArray());
            List<Operation> operations = new List<Operation>(_operationRepository.Operations.ToArray());

            foreach (Examination appointment in examinations)
            {
                if (appointment.Date >= DateTime.Now && patient.ID == appointment.Patient.ID) DeleteAppointment(appointment);
            }
            foreach (Operation appointment in operations)
            {
                if (appointment.Date >= DateTime.Now && patient.ID == appointment.Patient.ID) DeleteAppointment(appointment);
            }

            _examinationChangeRepository.DeleteUnresolvedRequestsByPatientId(patient.ID);
        }

        public void DeleteAppointment(Appointment appointment)
        {
            Patient patient = appointment.Patient;
            Doctor doctor = appointment.Doctor;
            Room room = appointment.Room;

            _examinationChangeRepository.RemoveByAppointmentId(appointment.ID);

            if (appointment is Examination)
            {
                patient.Examinations.Remove((Examination)appointment);
                doctor.Examinations.Remove((Examination)appointment);
                room.Appointments.Remove(appointment);
                _examinationRepository.Remove((Examination)appointment);
                _examinationReferencesRepository.Remove((Examination)appointment);
            }
            else if (appointment is Operation)
            {
                patient.Operations.Remove((Operation)appointment);
                doctor.Operations.Remove((Operation)appointment);
                _operationRepository.Remove((Operation)appointment);
                _operationReferencesRepository.Remove((Operation)appointment);
                room.Appointments.Remove(appointment);
            }
        }
    }
}
