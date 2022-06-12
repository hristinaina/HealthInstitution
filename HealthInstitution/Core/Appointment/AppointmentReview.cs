using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HealthInstitution.Core
{
    public class AppointmentReview
    {
        private double _rating;
        private string _comment;

        [JsonProperty("Rating")]
        public double Rating { get => _rating; set { _rating = value; } }
        [JsonProperty("Comment")]
        public string Comment { get => _comment; set { _comment = value; } }

        public AppointmentReview(double rating, string comment)
        {
            _rating = rating;
            _comment = comment;
        }
    }
}
