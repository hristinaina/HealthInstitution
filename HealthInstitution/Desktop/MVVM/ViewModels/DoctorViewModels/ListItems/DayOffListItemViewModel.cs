using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Core;

namespace HealthInstitution.MVVM.ViewModels.DoctorViewModels
{
    class DayOffListItemViewModel : BaseViewModel
    {
        private DayOff _dayOff;
        public DayOff DayOff { get => _dayOff; set { _dayOff = value; } }

        public string StartDate => _dayOff.BeginDate.ToString("dd/MM/yyyy");
        public string EndDate => _dayOff.EndDate.ToString("dd/MM/yyyy");
        public bool Emergency => _dayOff.Emergency;
        public string Reason => _dayOff.Reason;
        public State State => _dayOff.State;

        public DayOffListItemViewModel(DayOff dayOff)
        {
            _dayOff = dayOff;
        }
    }
}
