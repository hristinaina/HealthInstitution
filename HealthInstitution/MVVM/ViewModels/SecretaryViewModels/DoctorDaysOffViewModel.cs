using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.ViewModels.SecretaryViewModels
{
    public class DoctorDaysOffViewModel: BaseViewModel
    {
        public SecretaryNavigationViewModel Navigation { get; }

        //private readonly ObservableCollection<MissingEquipmentItemViewModel> _equipment;
        //public IEnumerable<MissingEquipmentItemViewModel> Equipment => _equipment;
        //public MissingEquipmentItemViewModel SelectedEquipment { get; set; }

        //public ICommand OrderEquipment { get; set; }

        private bool _dialogOpen;
        public bool DialogOpen
        {
            get => _dialogOpen;
            set
            {
                _dialogOpen = value;
                OnPropertyChanged(nameof(DialogOpen));
            }
        }

        public DoctorDaysOffViewModel()
        {
            Navigation = new SecretaryNavigationViewModel();

            //_equipment = new ObservableCollection<MissingEquipmentItemViewModel>();
            //OrderEquipment = new OrderEquipmentCommand(this);

            //FillEquipmentList();
        }

        public void FillEquipmentList()
        {
            /*_equipment.Clear();
            List<Equipment> equipment = Institution.Instance().EquipmentRepository.Equipment;
            foreach (Equipment e in equipment)
            {
                if (e.Quantity == 0)
                {
                    string status = Institution.Instance().EquipmentOrderRepository.CheckIfOrdered(e);
                    _equipment.Add(new MissingEquipmentItemViewModel(e, status));
                }
            }*/
        }
    }
}
