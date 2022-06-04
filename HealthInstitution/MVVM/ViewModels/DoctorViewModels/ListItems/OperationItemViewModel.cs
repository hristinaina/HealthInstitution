using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.MVVM.Models.Entities;

namespace HealthInstitution.MVVM.ViewModels.DoctorViewModels
{
    class OperationItemViewModel : BaseViewModel
    {
        private readonly Operation _operation;

        public Operation Operation { get => _operation; }

        public string Date => _operation.Date.ToString("MM/dd/yyyy HH:mm");
        public string Time => _operation.Date.ToString("MM/dd/yyyy HH:mm");
        public string Room => _operation.Room.ID.ToString();
        public Patient Patient => _operation.Patient;

        public OperationItemViewModel(Operation operation)
        {
            _operation = operation;
        }
    }
}
