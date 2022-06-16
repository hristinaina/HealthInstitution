using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Core;
using HealthInstitution.Core.Repository;

namespace HealthInstitution.MVVM.ViewModels.SecretaryViewModels
{
    public class ReferralItemViewModel : BaseViewModel
    {
        Referral _referral;
        private IDoctorRepositoryService _doctorService;

        public string Id => _referral.Id.ToString();
        public string Patient => Institution.Instance().PatientRepository.FindByID(_referral.PatientId).FirstName + " " + Institution.Instance().PatientRepository.FindByID(_referral.PatientId).LastName;
        public string Specialization => _referral.Specialization.ToString();
        public string Doctor => (_referral.DoctorId != -1) ? _doctorService.FindByID(_referral.DoctorId).FirstName
            + " " + _doctorService.FindByID(_referral.DoctorId).LastName : " / ";

    public ReferralItemViewModel(Referral referral)
        {
            _referral = referral;
            _doctorService = new DoctorRepositoryService();
        }
    }
}
