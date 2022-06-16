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
        private IExaminationRepositoryService _examinations;
        private readonly ExaminationChange _request;
        public int ID => _request.ID; 
        public string OldDate => _examinations.FindByID(_request.AppointmentID).Date.ToString();
        public string NewDate => _request.NewDate.ToString();
        public string Patient => Institution.Instance().PatientRepository.FindByID(_request.PatientID).FirstName + " " + Institution.Instance().PatientRepository.FindByID(_request.PatientID).LastName;
        public string Status => _request.ChangeStatus.ToString();

        public AppointmentChangeViewModel(ExaminationChange request)
        {
            _examinations = new ExaminationRepositoryService();
            _request = request;
        }
    }
}
