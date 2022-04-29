using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.Models.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.ViewModels.AdminViewModels
{
    class AdminRoomViewModel : BaseViewModel
    {
        private readonly Admin _admin;
        private Institution _institution;
        private RoomListItemViewModel _selectedRoom;


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
                _selectedRoom = _rooms.ElementAt(_selection);
                SelectedID = _selectedRoom.ID;
                OnPropertyChanged(nameof(SelectedID));
                SelectedNumber = _selectedRoom.Number;
                OnPropertyChanged(nameof(SelectedNumber));
                SelectedName = _selectedRoom.Name;
                OnPropertyChanged(nameof(SelectedName));
                SelectedType = _selectedRoom.Type;
                OnPropertyChanged(nameof(SelectedType));
            }
        }

        public string SelectedID { get; set; }
        public string SelectedNumber { get; set; }
        public string SelectedName { get; set; }
        public string SelectedType { get; set; }

        //public AdminNavigationViewModel Navigation { get; }
        private readonly ObservableCollection<RoomListItemViewModel> _rooms;
        public IEnumerable<RoomListItemViewModel> Appointments => _rooms;


        public AdminRoomViewModel()
        {
            _institution = Institution.Instance();
            _admin = (Admin)_institution.CurrentUser;
            _rooms = new ObservableCollection<RoomListItemViewModel>();
            //Navigation = new PatientNavigationViewModel();
            EnableChanges = false;
            FillRoomList();
            // ..............
        }

        private void FillRoomList()
        {
            _rooms.Clear();

            _rooms.Add(new RoomListItemViewModel(Room.Create(1, 101, "Ime", Models.Enumerations.RoomType.EXAM_ROOM)));
            _rooms.Add(new RoomListItemViewModel(Room.Create(1, 101, "Ime", Models.Enumerations.RoomType.EXAM_ROOM)));
            _rooms.Add(new RoomListItemViewModel(Room.Create(1, 101, "Ime", Models.Enumerations.RoomType.EXAM_ROOM)));
            _rooms.Add(new RoomListItemViewModel(Room.Create(1, 101, "Ime", Models.Enumerations.RoomType.EXAM_ROOM)));
            _rooms.Add(new RoomListItemViewModel(Room.Create(1, 101, "Ime", Models.Enumerations.RoomType.EXAM_ROOM)));
            _rooms.Add(new RoomListItemViewModel(Room.Create(1, 101, "Ime", Models.Enumerations.RoomType.EXAM_ROOM)));
            _rooms.Add(new RoomListItemViewModel(Room.Create(1, 101, "Ime", Models.Enumerations.RoomType.EXAM_ROOM)));
        }
    }
}
