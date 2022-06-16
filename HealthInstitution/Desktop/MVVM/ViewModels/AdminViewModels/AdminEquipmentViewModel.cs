using HealthInstitution.Core;
using HealthInstitution.MVVM.ViewModels.Commands.AdminCommands.EquipmentCommands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HealthInstitution.Core.Repository;

namespace HealthInstitution.MVVM.ViewModels.AdminViewModels
{
    public class AdminEquipmentViewModel : BaseViewModel
    {
        private Institution _institution;
        private Admin _admin;

        private EquipmentListItemViewModel _selectedEquipment;

        public EquipmentListItemViewModel SelectedEquipment { get => _selectedEquipment; set => _selectedEquipment = value; }

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

        private bool _enableChanges;
        private int _selection;
        public bool EnableChanges
        {
            get => _enableChanges;
            set
            {
                _enableChanges = value;
                OnPropertyChanged(nameof(EnableChanges));
            }
        }
        public int Selection
        {
            get => _selection;
            set
            {
                if (value < 0) { return; }
                _selection = value;
                EnableChanges = true;
                OnPropertyChanged(nameof(Selection));
                _selectedEquipment = _equipment.ElementAt(_selection);
                SelectedEquipmentName = _selectedEquipment.Name;
                OnPropertyChanged(nameof(SelectedEquipmentName));
                SelectedRoom = _selectedEquipment.RoomNumber;
                OnPropertyChanged(nameof(SelectedRoom));
                SelectedName = _selectedEquipment.Name;
                OnPropertyChanged(nameof(SelectedName));
                SelectedQuantity = _selectedEquipment.Quantity;
                OnPropertyChanged(nameof(SelectedQuantity));
            }
        }

        public string SelectedEquipmentName { get; set; }
        public string SelectedRoom { get; set; }
        public string SelectedName { get; set; }
        public string SelectedQuantity { get; set; }

        private string _searchPhrase;

        public string SearchPhrase { get => _searchPhrase;
            set
            {
                _searchPhrase = value;
                OnPropertyChanged(SearchPhrase);
            }
        }

        private int _filterEquipmentType;
        private int _filterRoomType;
        private string _filterMinQuantity;
        private string _filterMaxQuantity;

        public int FilterEquipmentType { get => _filterEquipmentType;
            set
            {
                _filterEquipmentType = value;
                OnPropertyChanged(nameof(FilterEquipmentType));
            }
        }
        public int FilterRoomType
        {
            get => _filterRoomType;
            set
            {
                _filterRoomType = value;
                OnPropertyChanged(nameof(FilterRoomType));
            }
        }
        public string FilterMinQuantity
        {
            get => _filterMinQuantity;
            set
            {
                _filterMinQuantity = value;
                OnPropertyChanged(nameof(FilterMinQuantity));
            }
        }
        public string FilterMaxQuantity
        {
            get => _filterMaxQuantity;
            set
            {
                _filterMaxQuantity = value;
                OnPropertyChanged(nameof(FilterMaxQuantity));
            }
        }

        private string _newArrangementStartDate;

        public string NewArrangementStartDate
        {
            get => _newArrangementStartDate;
            set
            {
                _newArrangementStartDate = value;
                OnPropertyChanged(nameof(NewArrangementStartDate));
            }
        }

        private Room _newArrangemenTargetRoom;
        public Room NewArrangemenTargetRoom
        {
            get => _newArrangemenTargetRoom;
            set
            {
                _newArrangemenTargetRoom = value;
                OnPropertyChanged(nameof(NewArrangemenTargetRoom));
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

        private readonly ObservableCollection<EquipmentListItemViewModel> _equipment;
        public IEnumerable<EquipmentListItemViewModel> Equipment => _equipment;

        private List<string> _roomTypes;
        public List<string> RoomTypes => _roomTypes;

        private List<string> _equipmentTypes;
        public List<string> EquipmentTypes => _equipmentTypes;

        private List<Room> _rooms;
        public List<Room> Rooms => _rooms;

        private List<Equipment> _allEquipment;
        public List<Equipment> AllEquipment { get => _allEquipment; set => _allEquipment = value; }

        private Dictionary<Equipment, List<Room>> _filteredEquipment;
        public Dictionary<Equipment, List<Room>> FilteredEquipment { get => _filteredEquipment; set => _filteredEquipment = value; }

        public ICommand Search { get; set; }
        public ICommand Reset { get; set; }
        public ICommand Filter { get; set; }
        public ICommand Rearrange { get; set; }

        public AdminNavigationViewModel Navigation { get; }

        public AdminEquipmentViewModel()
        {
            _institution = Institution.Instance();
            _admin = (Admin)_institution.CurrentUser;
            _equipment = new ObservableCollection<EquipmentListItemViewModel>();
            Navigation = new AdminNavigationViewModel();

            _roomTypes = new List<string>();
            _equipmentTypes = new List<string>();
            _rooms = new List<Room>();
            _allEquipment = _institution.EquipmentRepository.Equipment;
            _newArrangementStartDate = DateTime.Now.ToString("MM/dd/yyyy");

            Search = new SearchCommand(this);
            Reset = new ResetCommand(this);
            Filter = new FilterCommand(this);
            Rearrange = new RearrangeCommand(this);

            FillEquipmentList();
            FillEquipmentTypes();
            FillRoomTypes();
            FillRooms();
            // ..............
        }

        private void FillRooms()
        {
            IRoomRepositoryService rooms = new RoomRepositoryService();
            foreach (Room r in rooms.GetCurrentRooms())
            {
                _rooms.Add(r);
            }
        }

        private void FillEquipmentTypes()
        {

            foreach (EquipmentType t in Enum.GetValues(typeof(EquipmentType)))
            {
                _equipmentTypes.Add(t.ToString());
            }
            OnPropertyChanged(nameof(EquipmentTypes));
        }

        private void FillRoomTypes()
        {

            foreach (RoomType t in Enum.GetValues(typeof(RoomType)))
            {
                _roomTypes.Add(t.ToString());
            }
            OnPropertyChanged(nameof(RoomTypes));
        }

        public void FillEquipmentList()
        {
            _equipment.Clear();

            foreach (Equipment e in _allEquipment)
            {
                if (e.Type == EquipmentType.EXAM_EQUIPMENT || e.Type == EquipmentType.OPERATION_EQUIPMENT) continue;
                foreach (Room r in e.ArrangmentByRooms.Keys)
                {
                    _equipment.Add(new EquipmentListItemViewModel(e, r));
                }
            }
        }

        public void FilterEquipmentList()
        {
            _equipment.Clear();

            foreach (Equipment e in _filteredEquipment.Keys)
            {
                foreach(Room r in _filteredEquipment[e])
                {
                    _equipment.Add(new EquipmentListItemViewModel(e, r));
                }
            }
        }
    }
}
