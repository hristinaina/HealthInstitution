using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

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

        public string SelectedID { get; private set; }
        public string SelectedStartDate { get; private set; }
        public string SelectedEndDate { get; private set; }
        public List<Room> SelectedRoomsUnderRenovation { get; private set; }
        public List<Room> SelectedResult { get; private set; }



        private ObservableCollection<RenovationListItemViewModel> _renovations;

        public IEnumerable<RenovationListItemViewModel> Renovations => _renovations;


        public AdminNavigationViewModel Navigation { get; }

        public AdminRenovationViewModel()
        {
            _institution = Institution.Instance();
            _admin = (Admin)_institution.CurrentUser;
            _renovations = new ObservableCollection<RenovationListItemViewModel>();
            Navigation = new AdminNavigationViewModel();

            FillRenovationList();
        }

        public void FillRenovationList()
        {
            _renovations.Clear();

            foreach (Renovation r in _institution.RenovationRepository.Renovations)
            {
                _renovations.Add(new RenovationListItemViewModel(r));
            }
        }
    }
}
