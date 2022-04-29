using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.ViewModels.SecretaryViewModels
{
    public class AppointmentRequestsViewModel : BaseViewModel
    {
        public SecretaryNavigationViewModel Navigation { get; }

        public AppointmentRequestsViewModel()
        {
            Navigation = new SecretaryNavigationViewModel();
            // ..............
        }
    }
}
