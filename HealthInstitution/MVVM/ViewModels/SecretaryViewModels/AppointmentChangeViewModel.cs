using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Entities.References;

namespace HealthInstitution.MVVM.ViewModels.SecretaryViewModels
{
    public class AppointmentChangeViewModel : BaseViewModel
    {
        private readonly ExaminationChange _request;
        public int ID => _request.ID; // will not be showed in a list
        public string OldDate => Institution.Instance().ExaminationRepository.FindByID(_request.AppointmentID).Date.ToString();
        public string NewDate => _request.NewDate.ToString();
        public string Patient => Institution.Instance().PatientRepository.FindByID(_request.PatientID).FirstName + " " + Institution.Instance().PatientRepository.FindByID(_request.PatientID).LastName;
        public string Status => _request.ChangeStatus.ToString();
        //public int AppointmentId => _request.AppointmentID;   // will not be showed in a list
        //public int PatientId => _request.PatientID;   // will not be showed in a list
        //public bool Resolved => _request.Resolved;   // will not be showed in a list

        public AppointmentChangeViewModel(ExaminationChange request)
        {
            _request = request;
        }
    }
}
