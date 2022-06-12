using HealthInstitution.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.ViewModels.AdminViewModels
{
    class RenovationListItemViewModel : BaseViewModel
    {
        private Renovation _renovation;

        public Renovation Renovation { get => _renovation; set => _renovation = value; }

        public string ID => _renovation.ID.ToString();
        public string StartDate => _renovation.StartDate.ToShortDateString();
        public string EndDate => _renovation.EndDate.ToShortDateString();
        public string Started => _renovation.Started.ToString();

        public RenovationListItemViewModel(Renovation renovation)
        {
            _renovation = renovation;
        }
    }
}
