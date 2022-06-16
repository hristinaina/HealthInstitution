using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HealthInstitution.Core;
using HealthInstitution.Core.Repository;
using HealthInstitution.MVVM.ViewModels.Commands.SecretaryCommands.EquipmentCommands;
using HealthInstitution.MVVM.ViewModels.SecretaryViewModels.ListItems;

namespace HealthInstitution.MVVM.ViewModels.SecretaryViewModels
{
    public class OrderingEquipmentViewModel : BaseViewModel
    {
        public SecretaryNavigationViewModel Navigation { get; }

        private readonly ObservableCollection<MissingEquipmentItemViewModel> _equipment;
        public IEnumerable<MissingEquipmentItemViewModel> Equipment => _equipment;
        public MissingEquipmentItemViewModel SelectedEquipment { get; set; }
        public string Quantity { get; set; }

        public ICommand OrderEquipment { get; set; }

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

        public OrderingEquipmentViewModel()
        {
            Navigation = new SecretaryNavigationViewModel();

            _equipment = new ObservableCollection<MissingEquipmentItemViewModel>();
            OrderEquipment = new OrderEquipmentCommand(this);

            FillEquipmentList();
        }

        public void FillEquipmentList()
        {
            _equipment.Clear();

            IEquipmentRepositoryService equipmentRepository = new EquipmentRepositoryService();
            List<Equipment> equipment = equipmentRepository.GetEquipment();

            IEquipmentOrderRepositoryService orderRepository = new EquipmentOrderRepositoryService();

            foreach (Equipment e in equipment)
            {
                if (e.Quantity == 0)
                {
                    string status = orderRepository.CheckIfOrdered(e);
                    _equipment.Add(new MissingEquipmentItemViewModel(e, status));
                }
            }
        }
    }
}
