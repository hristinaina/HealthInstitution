using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.MVVM.Models.Entities;

namespace HealthInstitution.MVVM.ViewModels.SecretaryViewModels
{
    public class PatientListItemViewModel : BaseViewModel
    {
        Patient _patient;

        public string Id => _patient.ID.ToString();
        public string Name => _patient.FirstName;
        public string Surname => _patient.LastName;
        public string Gender => _patient.Gender.ToString();

        public PatientListItemViewModel(Patient patient)
        {
            _patient = patient;
        }
    }
}
