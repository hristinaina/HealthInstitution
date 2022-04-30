using System.ComponentModel;
using System;

namespace HealthInstitution.MVVM.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public DateTime MergeTime(string date, string time)
        {


            string[] dateTokens = date.Split(" ")[0].Split("/");
            string[] timeTokens = time.Split(" ")[1].Split(":");
            if (dateTokens.Length == 1)
            {
                dateTokens = date.Split(" ")[0].Split("-");
            }
            int month = int.Parse(dateTokens[0]);
            int day = int.Parse(dateTokens[1]);
            int year = int.Parse(dateTokens[2]);
            int hours = int.Parse(timeTokens[0]);
            int minutes = int.Parse(timeTokens[1]);
            return new DateTime(year, month, day, hours, minutes, 0, DateTimeKind.Local);
        }

        protected void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
