using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Core;

namespace HealthInstitution.MVVM.ViewModels.SecretaryViewModels
{
    public class DaysOffItemViewModel : BaseViewModel
    {
        DayOff _dayOff;

        public int ID => _dayOff.ID;
        public string Doctor => _dayOff.Doctor.ToString();
        public DateTime BeginDate => _dayOff.StartDate;
        public DateTime EndDate => _dayOff.EndDate;
        public string Reason => _dayOff.Reason;

        public DaysOffItemViewModel(DayOff dayOff)
        {
            _dayOff = dayOff;
        }
    }
}
