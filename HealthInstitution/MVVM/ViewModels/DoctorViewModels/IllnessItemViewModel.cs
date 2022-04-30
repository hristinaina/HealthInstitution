using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.MVVM.Models.Entities;

namespace HealthInstitution.MVVM.ViewModels.DoctorViewModels
{
    public class IllnessItemViewModel : BaseViewModel
    {
        private readonly string _illness;

        public string Illness => _illness;

        public IllnessItemViewModel(string illness)
        {
            _illness = illness;
        }
    }
}
