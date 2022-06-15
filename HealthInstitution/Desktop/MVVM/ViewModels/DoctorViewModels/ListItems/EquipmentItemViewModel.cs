using HealthInstitution.Core;

namespace HealthInstitution.MVVM.ViewModels.DoctorViewModels
{
    class EquipmentItemViewModel : BaseViewModel
    {
        private readonly Equipment _equipment;
        public Equipment Equipment { get => _equipment; }

        public string Name => _equipment.Name;
        public int Quantity => _equipment.Quantity;

        public EquipmentItemViewModel(Equipment equipment)
        {
            _equipment = equipment;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
