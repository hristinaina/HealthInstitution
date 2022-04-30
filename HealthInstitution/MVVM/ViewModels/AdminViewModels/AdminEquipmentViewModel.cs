using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Enumerations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.ViewModels.AdminViewModels
{
    public class AdminEquipmentViewModel : BaseViewModel
    {
        private Institution _institution;
        private Admin _admin;

        private EquipmentListItemViewModel _selectedEquipment;


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
                _selection = value;
                EnableChanges = true;
                OnPropertyChanged(nameof(Selection));
                _selectedEquipment = _equipment.ElementAt(_selection);
                SelectedEquipment = _selectedEquipment.Name;
                OnPropertyChanged(nameof(SelectedEquipment));
                SelectedRoom = _selectedEquipment.Room;
                OnPropertyChanged(nameof(SelectedRoom));
                SelectedName = _selectedEquipment.Name;
                OnPropertyChanged(nameof(SelectedName));
                SelectedQuantity = _selectedEquipment.Quantity;
                OnPropertyChanged(nameof(SelectedQuantity));
            }
        }

        public string SelectedEquipment { get; set; }
        public string SelectedRoom { get; set; }
        public string SelectedName { get; set; }
        public string SelectedQuantity { get; set; }


        private readonly ObservableCollection<EquipmentListItemViewModel> _equipment;
        public IEnumerable<EquipmentListItemViewModel> Equipment => _equipment;

        private List<string> _roomTypes;
        public List<string> RoomTypes => _roomTypes;

        private List<string> _equipmentTypes;
        public List<string> EquipmentTypes => _equipmentTypes;

        private List<Room> _rooms;
        public List<Room> Rooms => _rooms;


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

            FillEquipmentList();
            FillEquipmentTypes();
            FillRoomTypes();
            FillRooms();
            // ..............
        }

        private void FillRooms()
        {
            foreach (Room r in Institution.Instance().RoomRepository.Rooms)
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

        private void FillEquipmentList()
        {
            _equipment.Clear();

            foreach (Equipment e in _institution.EquipmentRepository.Equipment)
            {
                foreach (Room r in e.ArrangmentByRooms.Keys)
                {
                    _equipment.Add(new EquipmentListItemViewModel(e, r));
                }
            }
        }
    }
}
