using Newtonsoft.Json;

namespace HealthInstitution.Core
{
    public class HospitalReview : Review
    {
        private int _hygiene;
        private int _satisfaction;
     
        [JsonProperty("Hygiene")]
        public int Hygiene { get => _hygiene; set { _hygiene = value; } }
        [JsonProperty("Satisfaction")]
        public int Satisfacion { get => _satisfaction; set { _satisfaction = value; } }


        public HospitalReview(int service, int hygiene, int satisfaction, int suggestion, string comment) : base(service, suggestion, comment)
        {
            _hygiene = hygiene;
            _satisfaction = satisfaction;
        }

        public HospitalReview() : base()
        {
            _hygiene = 0;
            _satisfaction = 0;
        }
    }
}