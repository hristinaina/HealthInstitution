using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Enumerations;
using HealthInstitution.MVVM.Models.Repositories;

namespace HealthInstitution.MVVM.Models.Services
{
    public class PatientManagementService
    {
        private readonly PatientRepository _patientRepository;

        public PatientManagementService()
        {
            _patientRepository = Institution.Instance().PatientRepository;
        }

        public void CreatePatient(string firstName, string lastName, string email, string password, Gender gender,
            double height, double weight)
        {
            _patientRepository.CreatePatient(firstName, lastName, email, password, gender, height, weight);
        }

        public void UpdatePatient(int id, string firstName, string lastName, string email, string password, Gender gender,
            double height, double weight)
        {
            Patient patient = _patientRepository.FindByID(id);
            patient.Update(id, firstName, lastName, email, password, gender, height, weight);
        }

        public void DeletePatient(int id)
        {
            Patient patient = _patientRepository.FindByID(id);
            patient.Delete();
            SecretaryAppointmentManagementService service = new();
            service.DeleteFutureAppointments(patient);
        }

        public void BlockPatient(int id)
        {
            Patient patient = _patientRepository.FindByID(id);
            patient.Block(BlockadeType.SECRETARY);
        }

        public void UnblockPatient(int id)
        {
            Patient patient = _patientRepository.FindByID(id);
            patient.Unblock();
        }
    }
}
