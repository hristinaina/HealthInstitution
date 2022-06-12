using HealthInstitution.Core;
using HealthInstitution.MVVM.ViewModels.Commands.AdminCommands.RenovationCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HealthInstitution.MVVM.ViewModels.AdminViewModels
{
    class AdminDivideComplexRenovationViewModel : BaseViewModel
    {
        private readonly Admin _admin;
        private readonly Institution _institution;

        private DateTime _startDate;
        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }

        private List<Room> _rooms;
        public List<Room> Rooms => _rooms;


        private Room _selectedRoom;
        public Room SelectedRoom
        {
            get => _selectedRoom;
            set
            {
                _selectedRoom = value;
                OnPropertyChanged(nameof(SelectedRoom));
            }
        }


        private List<string> _roomTypes;
        public List<string> RoomTypes => _roomTypes;

        private int _firstNewRoomType;
        public int FirstNewRoomType
        {
            get => _firstNewRoomType;
            set
            {
                _firstNewRoomType = value;
                OnPropertyChanged(nameof(FirstNewRoomType));
            }
        }

        private string _firstNewRoomName;
        public string FirstNewRoomName
        {
            get => _firstNewRoomName;
            set
            {
                _firstNewRoomName = value;
                OnPropertyChanged(nameof(FirstNewRoomName));
            }
        }

        private int _firstNewRoomNumber;
        public int FirstNewRoomNumber
        {
            get => _firstNewRoomNumber;
            set
            {
                _firstNewRoomNumber = value;
                OnPropertyChanged(nameof(FirstNewRoomNumber));
            }
        }


        private int _secondNewRoomType;
        public int SecondNewRoomType
        {
            get => _secondNewRoomType;
            set
            {
                _secondNewRoomType = value;
                OnPropertyChanged(nameof(SecondNewRoomType));
            }
        }

        private string _secondNewRoomName;
        public string SecondNewRoomName
        {
            get => _secondNewRoomName;
            set
            {
                _secondNewRoomName = value;
                OnPropertyChanged(nameof(SecondNewRoomName));
            }
        }

        private int _secondNewRoomNumber;
        public int SecondNewRoomNumber
        {
            get => _secondNewRoomNumber;
            set
            {
                _secondNewRoomNumber = value;
                OnPropertyChanged(nameof(SecondNewRoomNumber));
            }
        }

        public ICommand Back { get; set; }
        public ICommand Divide { get; set; }

        public AdminDivideComplexRenovationViewModel()
        {
            _institution = Institution.Instance();
            _admin = (Admin)_institution.CurrentUser;
            _rooms = new List<Room>();
            _roomTypes = new List<string>();

            _startDate = DateTime.Today;
            _endDate = DateTime.Today;

            Back = new BackCommand();
            Divide = new DivideRenovationCommand(this);

            FillRooms();
            FillRoomTypes();
        }

        private void FillRoomTypes()
        {

            foreach (RoomType t in Enum.GetValues(typeof(RoomType)))
            {
                _roomTypes.Add(t.ToString());
            }
        }

        private void FillRooms()
        {
            foreach (Room r in Institution.Instance().RoomRepository.Rooms)
            {
                if (r.ID == 0) continue;
                _rooms.Add(r);
            }
        }
    }
}
