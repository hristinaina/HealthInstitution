using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.Models.Entities.References;

namespace HealthInstitution.MVVM.ViewModels.SecretaryViewModels
{
    public class AppointmentRequestsViewModel : BaseViewModel
    {
        public SecretaryNavigationViewModel Navigation { get; }

        private readonly ObservableCollection<AppointmentChangeViewModel> _requests;
        public IEnumerable<AppointmentChangeViewModel> Requests => _requests;
        private AppointmentChangeViewModel _selectedRequest;

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
                _selectedRequest = _requests.ElementAt(_selection);
                //SelectedDoctor = _selectedPatient.Doctor;
                //OnPropertyChanged(nameof(SelectedDoctor));
                //SelectedDate = _selectedPatient.Date;
                //OnPropertyChanged(nameof(SelectedDate));
                //SelectedTime = _selectedPatient.Time;
                //OnPropertyChanged(nameof(SelectedTime));
            }
        }

        public AppointmentRequestsViewModel()
        {
            _requests = new ObservableCollection<AppointmentChangeViewModel>();
            Navigation = new SecretaryNavigationViewModel();
            EnableChanges = false;
            FillRequestsList();
        }

        public void FillRequestsList()
        {
            _requests.Clear();

            // automaticaly remove/resolve requests for which secretary was late to do so == outdated requests
            RemoveOutdatedRequests();

            List<ExaminationChange> requests = Institution.Instance().ExaminationChangeRepository.Changes;
            foreach (ExaminationChange request in requests)
            {
                if (!request.Resolved)
                {
                    _requests.Add(new AppointmentChangeViewModel(request));
                }
            }
        }

        private static void RemoveOutdatedRequests()
        {
            foreach (ExaminationChange request in Institution.Instance().ExaminationChangeRepository.Changes)
            {
                if (!request.Resolved && request.NewDate <= DateTime.Now)
                {
                    request.ChangeStatus = Models.Enumerations.AppointmentStatus.DELETED;
                    request.Resolved = true;

                    // ?to delete appointment as well or not? 
                    Institution.Instance().ExaminationRepository.Delete(request.AppointmentID);
                }
            }
        }

        private void ApproveChange(ExaminationChange request)
        {
            request.Resolved = true;
            //created
            if (request.ChangeStatus == 0)
            {
                // Milicina funkcija za kreiranje novih pregleda --- bitne sve provjere ---- ako vrati false izbaciti dijalog
            }
            //edited
            else if(request.ChangeStatus.ToString() == "EDITED")
            {
                // Milicina funkcija za editovanje pregleda --- bitne sve provjere ---- ako vrati false izbaciti dijalog
            }
            //deleted
            else if(request.ChangeStatus.ToString() == "DELETED")
            {
                Institution.Instance().ExaminationRepository.Delete(request.AppointmentID);
            }

        }

        private void RejectChange(ExaminationChange request)
        {
            request.Resolved = true;
            //created
            if (request.ChangeStatus == 0)
            {
                // do nothing
            }
            //edited
            else if (request.ChangeStatus.ToString() == "EDITED")
            {
                // kako signalizirati da sam odbila editovanje??
            }
            //deleted
            else if (request.ChangeStatus.ToString() == "DELETED")
            {
                // do nothing
            }

        }
    }
}
