using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Enumerations;
using HealthInstitution.MVVM.ViewModels.Commands.AdminCommands;
using HealthInstitution.MVVM.ViewModels.Commands.AdminCommands.RoomCommands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HealthInstitution.MVVM.ViewModels.AdminViewModels
{
    class AdminRoomViewModel : BaseViewModel
    {
        private readonly Admin _admin;
        private Institution _institution;
        private RoomListItemViewModel _selectedRoom;

        public RoomListItemViewModel SelectedRoom { get => _selectedRoom; }


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

        public int Selection
        {
            get => _selection;
            set
            {
                if (value < 0) { EnableChanges = false; return; };
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
                SelectedTypeIndex = (int)_selectedRoom.Room.Type;
                OnPropertyChanged(nameof(SelectedTypeIndex));
            }
        }

        public string SelectedID { get; set; }
        public string SelectedNumber { get; set; }
        public string SelectedName { get; set; }
        public string SelectedType { get; set; }
        public int SelectedTypeIndex { get; set; }


        private int _newRoomType;
        public int NewRoomType
        {
            get => _newRoomType;
            set
            {
                _newRoomType = value;
                OnPropertyChanged(nameof(NewRoomType));
            }
        }

        private string _newRoomName;
        public string NewRoomName
        {
            get => _newRoomName;
            set
            {
                _newRoomName = value;
                OnPropertyChanged(nameof(NewRoomName));
            }
        }

        private int _newRoomNumber;
        public int NewRoomNumber
        {
            get => _newRoomNumber;
            set
            {
                _newRoomNumber = value;
                OnPropertyChanged(nameof(NewRoomNumber));
            }
        }



        private readonly ObservableCollection<RoomListItemViewModel> _rooms;
        public IEnumerable<RoomListItemViewModel> Rooms => _rooms;

        private List<string> _roomTypes;
        public List<string> RoomTypes => _roomTypes;

        public AdminNavigationViewModel Navigation { get; }

        public ICommand CreateNewRoom { get; set; }
        public ICommand DeleteRoom { get; set; }
        public ICommand ChangeRoom { get; set; }
        public ICommand CancelChange { get; set; }


        public AdminRoomViewModel()
        {
            _institution = Institution.Instance();
            _admin = (Admin)_institution.CurrentUser;
            _rooms = new ObservableCollection<RoomListItemViewModel>();
            Navigation = new AdminNavigationViewModel();

            CreateNewRoom = new CreateNewRoomCommand(this);
            DeleteRoom = new DeleteRoomCommand(this);
            ChangeRoom = new ChangeRoomCommand(this);
            CancelChange = new RoomChangeCancelCommand(this);

            EnableChanges = false;
            _roomTypes = new List<string>();
            FillRoomTypes();
            FillRoomList();

            // ..............
        }

        private void FillRoomTypes()
        {

            foreach (RoomType t in Enum.GetValues(typeof(RoomType)))
            {
                _roomTypes.Add(t.ToString());
            }
            OnPropertyChanged(nameof(RoomTypes));
        }

        public void FillRoomList()
        {
            _rooms.Clear();

            foreach (Room r in _institution.RoomRepository.Rooms)
            {
                if (r.ID != 0) _rooms.Add(new RoomListItemViewModel(r));
            }
        }
    }
}
