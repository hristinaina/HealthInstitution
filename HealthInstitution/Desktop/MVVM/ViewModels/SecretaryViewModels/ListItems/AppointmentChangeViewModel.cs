using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Core;
using HealthInstitution.Core;
using HealthInstitution.Core.Repository;

namespace HealthInstitution.MVVM.ViewModels.SecretaryViewModels
{
    public class AppointmentChangeViewModel : BaseViewModel
    {
        private IPatientRepositoryService _patientService;
        private readonly ExaminationChange _request;
        public int ID => _request.ID; 
        public string OldDate => Institution.Instance().ExaminationRepository.FindByID(_request.AppointmentID).Date.ToString();
        public string NewDate => _request.NewDate.ToString();
        public string Patient => _patientService.FindByID(_request.PatientID).FirstName + " " + _patientService.FindByID(_request.PatientID).LastName;
        public string Status => _request.ChangeStatus.ToString();

        public AppointmentChangeViewModel(ExaminationChange request)
        {
            _patientService = new PatientRepositoryService();
            _request = request;
        }
    }
}
