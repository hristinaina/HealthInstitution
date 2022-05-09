using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.Models.Entities.References;
using HealthInstitution.MVVM.Models.Services;
using HealthInstitution.MVVM.ViewModels.Commands.SecretaryCommands.AppointmentCommands;

namespace HealthInstitution.MVVM.ViewModels.SecretaryViewModels
{
    public class AppointmentsViewModel : BaseViewModel
    {
        public SecretaryNavigationViewModel Navigation { get; }

        private readonly ObservableCollection<ReferralItemViewModel> _referrals;
        public IEnumerable<ReferralItemViewModel> Referrals => _referrals;
        private ReferralItemViewModel _selectedReferral;

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

        public int SelectedReferralId { get; set; }

        public int Selection
        {
            get => _selection;
            set
            {
                if (value < 0) { return; }
                _selection = value;
                EnableChanges = true;
                OnPropertyChanged(nameof(Selection));
                _selectedReferral = _referrals.ElementAt(_selection);
                SelectedReferralId = Convert.ToInt32(_selectedReferral.Id);
                OnPropertyChanged(nameof(SelectedReferralId));
            }
        }

        private string _searchPhrase;
        public string SearchPhrase
        {
            get => _searchPhrase;
            set
            {
                _searchPhrase = value;
                OnPropertyChanged(SearchPhrase);
            }
        }

        public ICommand Search { get; set; }
        public ICommand Reset { get; set; }

        public AppointmentsViewModel()
        {
            _referrals = new ObservableCollection<ReferralItemViewModel>();

            Search = new SearchCommand(this);
            Reset = new ResetCommand(this);

            // funkcije koje fill-uju
            FillReferralsList();
        }

        public void FillReferralsList(string phrase = null)
        {
            _referrals.Clear();
            List<Referral> referrals = Institution.Instance().ReferralRepository.Referrals;
            if (phrase != null) referrals = SecretaryService.SearchMatchingReferrals(phrase);
            foreach (Referral referral in referrals)
            {
                _referrals.Add(new ReferralItemViewModel(referral));
            }
        }
    }
}
