using Newtonsoft.Json;

namespace HealthInstitution.Core
{
    public class HospitalReview : Review
    {
        private int _hygene;
        private int _satisfaction;
     
        [JsonProperty("Hygene")]
        public int Hygene { get => _hygene; set { _hygene = value; } }
        [JsonProperty("Satisfaction")]
        public int Satisfacion { get => _satisfaction; set { _satisfaction = value; } }


        public HospitalReview(int service, int hygene, int satisfaction, int suggestion, string comment) : base(service, suggestion, comment)
        {
            _hygene = hygene;
            _satisfaction = satisfaction;
        }

        public HospitalReview() : base()
        {
            _hygene = 0;
            _satisfaction = 0;
        }
    }
}