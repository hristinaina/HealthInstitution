using HealthInstitution.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Stores
{
    public class NavigationStore
    {
        public BaseViewModel _currentViewModel;
        public BaseViewModel CurrentViewModel {
            get { return _currentViewModel; }
            set {
                _currentViewModel = value;
                OnCurrentViewModelChanged();
           }
        }

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }

        public event Action CurrentViewModelChanged;
    }
}
