using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Core;
using HealthInstitution.Core;


namespace HealthInstitution.MVVM.ViewModels.DoctorViewModels
{
    class PendingMedicineItemViewModel : BaseViewModel
    {
        private readonly PendingMedicine _pendingMedicine;
        public PendingMedicine PendingMedicine => _pendingMedicine;
        private string _revisionReason;
        public string RevisionReason => _revisionReason;

        public string MedicineName => _pendingMedicine.Name;
        public string Description => _pendingMedicine.Description;
        public State State => _pendingMedicine.State;

        public PendingMedicineItemViewModel(PendingMedicine pendingMedicine)
        {
            _pendingMedicine = pendingMedicine;
        }
    }
}
