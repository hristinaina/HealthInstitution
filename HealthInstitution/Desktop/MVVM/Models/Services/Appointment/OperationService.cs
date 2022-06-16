using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Core;
using HealthInstitution.Core.Repositories;
using HealthInstitution.Core.Repository;
using HealthInstitution.Desktop.MVVM.Models.Services.Appointment;

namespace HealthInstitution.Core.Services
{
    public class OperationService : AppointmentService
    {

        private readonly IOperationRepositoryService _operationRepository;
        public OperationService()
        {
            _operationRepository = new OperationRepositoryService();
        }

        public List<Operation> GetFutureOperations(Specialization specialization, Patient patient)
        {
            List<Operation> futureAppointments = new();
            foreach (Operation appointment in _operationRepository.GetOperations())
            {
                if (DateTime.Compare(appointment.Date, DateTime.Now) > 0 &&
                    (appointment.Doctor.Specialization == specialization || appointment.Patient == patient))
                {
                    futureAppointments.Add(appointment);
                }
            }
            futureAppointments = futureAppointments.OrderBy(x => x.Date).ToList();
            return futureAppointments;
        }

    }
}
