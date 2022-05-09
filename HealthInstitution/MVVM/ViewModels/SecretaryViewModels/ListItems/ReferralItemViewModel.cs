using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.Models.Entities.References;

namespace HealthInstitution.MVVM.ViewModels.SecretaryViewModels
{
    public class ReferralItemViewModel : BaseViewModel
    {
        Referral _referral;

        public string Id => _referral.Id.ToString();
        public string Patient => Institution.Instance().PatientRepository.FindByID(_referral.PatientId).FirstName + " " + Institution.Instance().PatientRepository.FindByID(_referral.PatientId).LastName;
        public string Specialization => _referral.Specialization.ToString();
        public string Doctor => Institution.Instance().DoctorRepository.FindByID(_referral.DoctorId).FirstName + " " + Institution.Instance().DoctorRepository.FindByID(_referral.DoctorId).LastName;

        public ReferralItemViewModel(Referral referral)
        {
            _referral = referral;
        }
    }
}
