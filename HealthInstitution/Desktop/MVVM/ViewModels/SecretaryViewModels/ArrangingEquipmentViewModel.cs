using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HealthInstitution.Core;
using HealthInstitution.Core;
using HealthInstitution.Core;
using HealthInstitution.Core.Repository;
using HealthInstitution.MVVM.ViewModels.AdminViewModels;
using HealthInstitution.MVVM.ViewModels.Commands.SecretaryCommands.EquipmentCommands;

namespace HealthInstitution.MVVM.ViewModels.SecretaryViewModels
{
    public class ArrangingEquipmentViewModel : BaseViewModel
    {
        public SecretaryNavigationViewModel Navigation { get; }
        private readonly ObservableCollection<EquipmentListItemViewModel> _equipmentArrangement;
        public IEnumerable<EquipmentListItemViewModel> EquipmentArrangement => _equipmentArrangement;

        private EquipmentListItemViewModel _selectedEquipment;
        public EquipmentListItemViewModel SelectedEquipment { get => _selectedEquipment; set => _selectedEquipment = value; }

        public string SelectedRoom { get; set; }
        public string SelectedName { get; set; }
        public string SelectedQuantity { get; set; }

        private List<Room> _rooms;
        public List<Room> Rooms => _rooms;

        private bool _enableChanges;
        public bool EnableChanges
        {
            get => _enableChanges;
            set
            {
                _enableChanges = value;
                OnPropertyChanged(nameof(EnableChanges));
            }
        }

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

        private int _selection;
        public int Selection
        {
            get => _selection;
            set
            {
                if (value < 0) { return; }
                _selection = value;
                OnPropertyChanged(nameof(Selection));
                _selectedEquipment = _equipmentArrangement.ElementAt(_selection);
                SelectedRoom = _selectedEquipment.RoomNumber;
                OnPropertyChanged(nameof(SelectedRoom));
                SelectedName = _selectedEquipment.Name;
                OnPropertyChanged(nameof(SelectedName));
                SelectedQuantity = _selectedEquipment.Quantity;
                OnPropertyChanged(nameof(SelectedQuantity));
                EnableChanges = true;
            }
        }

        private Room _newArrangementTargetRoom;
        public Room NewArrangementTargetRoom
        {
            get => _newArrangementTargetRoom;
            set
            {
                _newArrangementTargetRoom = value;
                OnPropertyChanged(nameof(NewArrangementTargetRoom));
            }
        }

        private int _newArrangementQuantity;
        public int NewArrangementQuantity

        {
            get => _newArrangementQuantity;
            set
            {
                _newArrangementQuantity = value;
                OnPropertyChanged(nameof(NewArrangementQuantity));
            }
        }

        public ArrangingEquipmentViewModel()
        {
            Navigation = new SecretaryNavigationViewModel();
            _equipmentArrangement = new ObservableCollection<EquipmentListItemViewModel>();
            _rooms = new List<Room>();
            Rearrange = new RearrangeCommand(this);
            EnableChanges = false;

            FillEquipmentArrangementList();
            FillRooms();
        }

        public ICommand Rearrange { get; set; }

        private void FillRooms()
        {
            IRoomRepositoryService rooms = new RoomRepositoryService();
            foreach (Room r in rooms.GetCurrentRooms())
            {
                _rooms.Add(r);
            }
        }

        public void FillEquipmentArrangementList()
        {
            _equipmentArrangement.Clear();

            IEquipmentRepositoryService service = new EquipmentRepositoryService();
            foreach (Equipment e in service.GetEquipment())
            {
                if (e.Type == EquipmentType.HALLWAY_EQUIPMENT || e.Type == EquipmentType.ROOM_FURNITURE) continue;
                foreach (Room r in e.ArrangmentByRooms.Keys)
                {
                    if (e.ArrangmentByRooms[r] > 5) continue;   // if quantity > 5 
                    _equipmentArrangement.Add(new EquipmentListItemViewModel(e, r));
                }
            }

            if (_equipmentArrangement.Count != 0)
            {
                Selection = 0;
                OnPropertyChanged(nameof(Selection));
            }
        }
    }
}
