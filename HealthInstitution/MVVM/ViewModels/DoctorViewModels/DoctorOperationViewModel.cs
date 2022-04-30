using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.ObjectModel;
using HealthInstitution.MVVM.Models.Repositories;
using HealthInstitution.MVVM.Models.Entities;

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

        public DoctorOperationViewModel()
        {
            _operations = new ObservableCollection<OperationViewModel>();

            // test
            Operation operation = new Operation(1, DateTime.Now, false, false, 30);
            Patient patient = new Patient();
            patient.FirstName = "PAcijnet";
            patient.LastName = "Pacijentic";

            Room room = new Room();
            room.Name = "neka sobica";
            operation.Patient = patient;
            operation.Room = room;
            _operations.Add(new OperationViewModel(operation));
            operation.ID = 2;
            _operations.Add(new OperationViewModel(operation));
            operation.ID = 8;
            _operations.Add(new OperationViewModel(operation));
            operation.Date = DateTime.Now.AddDays(8);
            _operations.Add(new OperationViewModel(operation));
            _operations.Add(new OperationViewModel(operation));

            // test
        }
    }
}
