using System.Collections.Generic;

namespace HealthInstitution.MVVM.ViewModels.AdminViewModels
{
    public class SurveyResultListItemViewModel : BaseViewModel
    {
        private string _category;
        
        private int _numOf1;
        private int _numOf2;
        private int _numOf3;
        private int _numOf4;
        private int _numOf5;
        private double _average;

        public string Category => _category;
        public string NumOf1 => _numOf1.ToString();
        public string NumOf2 => _numOf2.ToString();
        public string NumOf3 => _numOf3.ToString();
        public string NumOf4 => _numOf4.ToString();
        public string NumOf5 => _numOf5.ToString();
        public string Average => _average.ToString();

        public SurveyResultListItemViewModel(string category)
        {
            _category = category;
        }

        public SurveyResultListItemViewModel(string category, List<double> results)
        {
            _category = category;
            _average = results[0];
            if (results.Count > 1)
            {
                _numOf1 = (int)results[1];
                _numOf2 = (int)results[2];
                _numOf3 = (int)results[3];
                _numOf4 = (int)results[4];
                _numOf5 = (int)results[5];
            }
            
        }
    }
}