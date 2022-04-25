using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.Models.Entities
{
    public class AppointmentReview
    {
        private int _rating;
        private string _comment;

        public AppointmentReview(int rating, string comment)
        {
            _rating = rating;
            _comment = comment;
        }

        public int GetRating() => _rating;
        public void SetRating(int rating) => _rating = rating;
        public string GetComment() => _comment;
        public void SetComment(string comment) => _comment = comment;
    }
}
