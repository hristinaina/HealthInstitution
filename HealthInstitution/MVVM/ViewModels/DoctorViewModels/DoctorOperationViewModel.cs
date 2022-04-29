using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.ObjectModel;
using HealthInstitution.MVVM.Models.Repositories;

namespace HealthInstitution.MVVM.ViewModels.DoctorViewModels
{
    class DoctorOperationViewModel : BaseViewModel
    {
        private readonly ObservableCollection<OperationViewModel> _operations;

        public IEnumerable<OperationViewModel> Operations => _operations;

        public ICommand ScheduleOperation { get; }
        public ICommand MedicalRecord { get; }
        public ICommand UpdateOperation { get; }
        public ICommand DeleteOperation { get; }
        public ICommand CreateOperation { get; }
        public ICommand CancelOperation { get; }

        public DoctorOperationViewModel(OperationRepository operationRepository)
        {
            _operations = new ObservableCollection<OperationViewModel>();

            // add test examples here
        }
    }
}
