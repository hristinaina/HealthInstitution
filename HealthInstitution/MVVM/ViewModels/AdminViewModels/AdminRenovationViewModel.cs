using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using HealthInstitution.MVVM.ViewModels.Commands.AdminCommands.RenovationCommands;

namespace HealthInstitution.MVVM.ViewModels.AdminViewModels
{
    class AdminRenovationViewModel : BaseViewModel
    {
        private readonly Admin _admin;
        private Institution _institution;
        private RenovationListItemViewModel _selectedRenovation;

        public RenovationListItemViewModel SelectedRenovation { get => _selectedRenovation; }


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
                _selectedRenovation = _renovations.ElementAt(_selection);
                SelectedID = _selectedRenovation.ID;
                OnPropertyChanged(nameof(SelectedID));
                SelectedStartDate = _selectedRenovation.StartDate;
                OnPropertyChanged(nameof(SelectedStartDate));
                SelectedEndDate = _selectedRenovation.EndDate;
                OnPropertyChanged(nameof(SelectedEndDate));
                SelectedRoomsUnderRenovation = _selectedRenovation.Renovation.Rooms;
                OnPropertyChanged(nameof(SelectedRoomsUnderRenovation));
                SelectedResult = _selectedRenovation.Renovation.Result;
                OnPropertyChanged(nameof(SelectedResult));
            }
        }

        public string SelectedID { get; set; }
        public string SelectedStartDate { get; set; }
        public string SelectedEndDate { get; set; }
        public List<Room> SelectedRoomsUnderRenovation { get; set; }
        public List<Room> SelectedResult { get; set; }


        private List<Room> _rooms;
        public List<Room> Rooms => _rooms;

        private Room _newRenovationRoom;
        public Room NewRenovationRoom
        {
            get => _newRenovationRoom;
            set
            {
                _newRenovationRoom = value;
                OnPropertyChanged(nameof(NewRenovationRoom));
            }
        }

        private DateTime _newRenovationStartDate;
        public DateTime NewRenovationStartDate
        {
            get => _newRenovationStartDate;
            set
            {
                _newRenovationStartDate = value;
                OnPropertyChanged(nameof(NewRenovationStartDate));
            }
        }

        private DateTime _newRenovationEndDate;
        public DateTime NewRenovationEndDate
        {
            get => _newRenovationEndDate;
            set
            {
                _newRenovationEndDate = value;
                OnPropertyChanged(nameof(NewRenovationEndDate));
            }
        }


        private ObservableCollection<RenovationListItemViewModel> _renovations;

        public IEnumerable<RenovationListItemViewModel> Renovations => _renovations;


        public AdminNavigationViewModel Navigation { get; }

        public ICommand ScheduleRenovation { get; set; }
        public ICommand MergeRooms { get; set; }
        public ICommand DivideRoom { get; set; }

        public AdminRenovationViewModel()
        {
            _institution = Institution.Instance();
            _admin = (Admin)_institution.CurrentUser;
            _renovations = new ObservableCollection<RenovationListItemViewModel>();
            Navigation = new AdminNavigationViewModel();

            _rooms = new List<Room>();
            NewRenovationStartDate = DateTime.Today;
            NewRenovationEndDate = DateTime.Today;

            ScheduleRenovation = new ScheduleRenovationCommand(this);
            MergeRooms = new MergeRoomsCommand();
            DivideRoom = new DivideRoomCommand();

            FillRenovationList();
            FillRooms();
        }

        public void FillRenovationList()
        {
            _renovations.Clear();

            foreach (Renovation r in _institution.RenovationRepository.Renovations)
            {
                _renovations.Add(new RenovationListItemViewModel(r));
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
