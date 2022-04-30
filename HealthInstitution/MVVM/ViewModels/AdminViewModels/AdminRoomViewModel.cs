﻿using HealthInstitution.MVVM.Models;
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
        public IEnumerable<RoomListItemViewModel> Rooms => _rooms;

        private List<string> _roomTypes;
        public List<string> RoomTypes => _roomTypes;


        public AdminRoomViewModel()
        {
            _institution = Institution.Instance();
            _admin = (Admin)_institution.CurrentUser;
            _rooms = new ObservableCollection<RoomListItemViewModel>();
            //Navigation = new PatientNavigationViewModel();
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

        private void FillRoomList()
        {
            _rooms.Clear();

            foreach (Room r in _institution.RoomRepository.Rooms)
            {
                _rooms.Add(new RoomListItemViewModel(r));
            }
        }
    }
}