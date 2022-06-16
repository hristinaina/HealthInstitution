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
    public class SecretaryDeleteAppointmentService
    {
        private readonly IExaminationRepositoryService _examinationRepository;
        private readonly IOperationRepositoryService _operationRepository;
        private readonly IExaminationRelationsRepositoryService _examinationReferencesRepository;
        private readonly IOperationRelationsRepositoryService _operationReferencesRepository;
        private readonly IExaminationChangeRepositoryService _examinationChangeRepository;

        public SecretaryDeleteAppointmentService()
        {
            _examinationRepository = new ExaminationRepositoryService();
            _operationRepository = new OperationRepositoryService();
            _examinationReferencesRepository = new ExaminationRelationsRepositoryService();
            _operationReferencesRepository = new OperationRelationsRepositoryService();
            _examinationChangeRepository = new ExaminationChangeRepositoryService();
        }

        public void DeleteFutureAppointments(Patient patient)
        {
            List<Examination> examinations = new List<Examination>(_examinationRepository.GetExaminations().ToArray());
            List<Operation> operations = new List<Operation>(_operationRepository.GetOperations().ToArray());

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
