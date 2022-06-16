using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Core;
using HealthInstitution.Core;
using HealthInstitution.Core.Repositories;
using HealthInstitution.Core.Repository;

namespace HealthInstitution.Core.Services
{
    public class PatientManagementService
    {
        private IPatientRepositoryService _patientService;

        public PatientManagementService()
        {
            _patientService = new PatientRepositoryService();
        }

        public void CreatePatient(string firstName, string lastName, string email, string password, Gender gender,
            double height, double weight)
        {
            _patientService.CreatePatient(firstName, lastName, email, password, gender, height, weight);
        }

        public void UpdatePatient(int id, string firstName, string lastName, string email, string password, Gender gender,
            double height, double weight)
        {
            Patient patient = _patientService.FindByID(id);
            patient.Update(id, firstName, lastName, email, password, gender, height, weight);
        }

        public void DeletePatient(int id)
        {
            Patient patient = _patientService.FindByID(id);
            patient.Delete();
            new SecretaryDeleteAppointmentService().DeleteFutureAppointments(patient);
        }

        public void BlockPatient(int id)
        {
            Patient patient = _patientService.FindByID(id);
            patient.Block(BlockadeType.SECRETARY);
        }

        public void UnblockPatient(int id)
        {
            Patient patient = _patientService.FindByID(id);
            patient.Unblock();
        }

        public void AddIllness(Patient patient, string illness)
        {
            foreach (Patient i in _patientService.GetPatients())
            {
                if (patient.ID == i.ID) patient.Record.HistoryOfIllnesses.Add(illness);

            }
        }
        public bool IsAllergic(Patient patient, List<Allergen> allergens)
        {
            foreach (Allergen i in patient.Record.Allergens)
            {
                foreach (Allergen allergen in allergens)
                {
                    if (i.ID == allergen.ID) return true;
                }
            }
            return false;
        }
    }
}
