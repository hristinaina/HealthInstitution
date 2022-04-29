using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.MVVM.Models.Entities;

namespace HealthInstitution.MVVM.ViewModels.DoctorViewModels
{
    class OperationViewModel : BaseViewModel
    {
        private readonly Operation _operation;

        public string Date => _operation.Date.ToString("d");
        public string Time => _operation.Date.ToString("t");
        public string Room => _operation.Room.ID.ToString();
        public string Patient => _operation.Patient.FirstName + _operation.Patient.LastName;

        public OperationViewModel(Operation operation)
        {
            _operation = operation;
        }
    }
}
