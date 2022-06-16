using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HealthInstitution.Core;
using HealthInstitution.Core.Repository;
using HealthInstitution.Core.Services.SurveyResults;

namespace HealthInstitution.MVVM.ViewModels.AdminViewModels
{
    public class AdminSurveysViewModel : BaseViewModel
    {
        private IDoctorRepositoryService _doctorService;
        private readonly Admin _admin;
        private Institution _institution;
        private IHospitalSurveyResultsService _hospitalSurveyResults;
        private IDoctorSuvreyResultsService _doctorSurveyResults;


        private readonly List<Doctor> _doctors;

        private List<string> _types;
        public List<string> Types
        {
            get => _types;
            set => _types = value;
        }

        private int _chosenType;
        public int ChosenType
        {
            get => _chosenType;
            set
            {
                if (value == 0)
                {
                    _results = _hospitalSurveyResults.GetResults();
                    _surveyComments = _hospitalSurveyResults.GetComments();
                    FillResultList();
                    FillCommentsList();
                }
                else
                {
                    Doctor d = _doctors.ElementAt(value - 1);
                    _results = _doctorSurveyResults.GetResults(d);
                    _surveyComments = _doctorSurveyResults.GetComments(d);
                    FillResultList();
                    FillCommentsList();
                }
                _chosenType = value; 
                 
            }
        }

        private Dictionary<string, List<double>> _results;
        private ObservableCollection<SurveyResultListItemViewModel> _surveyResults;
        public IEnumerable<SurveyResultListItemViewModel> SurveyResults => _surveyResults;


        private List<string> _surveyComments;

        private ObservableCollection<SurveyResultListItemViewModel> _bestDoctors;
        public IEnumerable<SurveyResultListItemViewModel> BestDoctors => _bestDoctors;

        private ObservableCollection<SurveyResultListItemViewModel> _worstDoctors;
        public IEnumerable<SurveyResultListItemViewModel> WorstDoctors => _worstDoctors;

        private ObservableCollection<SurveyResultListItemViewModel> _comments;
        public IEnumerable<SurveyResultListItemViewModel> Comments => _comments;

        public AdminNavigationViewModel Navigation { get; }
        public AdminSurveysViewModel()
        {
            _doctorService = new DoctorRepositoryService();
            _institution = Institution.Instance();
            Navigation = new AdminNavigationViewModel();

            _hospitalSurveyResults = new HospitalSurveyResultsService();
            _doctorSurveyResults = new DoctorSurveyResultsService();

            _doctors = _doctorService.GetDoctors();

            //_results = new Dictionary<string, List<double>>();
            //_surveyComments = new List<string>();

            _results = _hospitalSurveyResults.GetResults();
            _surveyComments = _hospitalSurveyResults.GetComments();


            IDoctorRankingService service = new DoctorRankingService();

            _bestDoctors = new ObservableCollection<SurveyResultListItemViewModel>();
            FillBestDoctorsList();

            _worstDoctors = new ObservableCollection<SurveyResultListItemViewModel>();
            FillWorstDoctorsList();

            _comments = new ObservableCollection<SurveyResultListItemViewModel>();
            FillCommentsList();

            _types = new List<string>();
            FillSurveyType();

            _surveyResults = new ObservableCollection<SurveyResultListItemViewModel>();
            FillResultList();
        }

        private void FillCommentsList()
        {
            _comments.Clear();

            foreach (string comment in _surveyComments)
            {
                _comments.Add(new SurveyResultListItemViewModel(comment));
            }
        }

        private void FillWorstDoctorsList()
        {
            _worstDoctors.Clear();

            IDoctorRankingService service = new DoctorRankingService();
            foreach (Tuple<Doctor, double> doctor in service.GetWorst())
            {
                if (doctor is null) continue;
                _worstDoctors.Add(new SurveyResultListItemViewModel(doctor.Item1.FirstName + " " + doctor.Item1.LastName, new List<double>() { doctor.Item2 }));
            }
        }

        private void FillBestDoctorsList()
        {
            _bestDoctors.Clear();

            IDoctorRankingService service = new DoctorRankingService();
            foreach (Tuple<Doctor, double> doctor in service.GetBest())
            {
                if (doctor is null) continue;
                _bestDoctors.Add(new SurveyResultListItemViewModel(doctor.Item1.FirstName + " " + doctor.Item1.LastName, new List<double>() {doctor.Item2}));
            }
        }

        private void FillResultList()
        {
            _surveyResults.Clear();

            foreach (string category in _results.Keys)
            {
                _surveyResults.Add(new SurveyResultListItemViewModel(category, _results[category]));
            }
        }


        private void FillSurveyType()
        {
            _types.Clear();

            _types.Add("Hospital");

            foreach (Doctor d in _doctors)
            {
                _types.Add(d.ToString());
            }
        }
    }
}