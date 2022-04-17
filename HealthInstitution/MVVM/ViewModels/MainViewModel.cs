using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        public BaseViewModel CurrentViewModel { get; }
   
        public MainViewModel()
        {
            CurrentViewModel = new LoginViewModel();         }
    }
}
