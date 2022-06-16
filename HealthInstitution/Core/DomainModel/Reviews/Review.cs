using Newtonsoft.Json;

namespace HealthInstitution.Core
{
    public class Review
    {
        private int _service;
        private int _suggestion;
        private string _comment;

        [JsonProperty("Service")]
        public int Service { get => _service; set { _service = value; } }
        [JsonProperty("Suggestion")]
        public int Suggestion { get => _suggestion; set { _suggestion = value; } }
        [JsonProperty("Comment")]
        public string Comment { get => _comment; set { _comment = value; } }

        public Review(int service, int suggestion, string comment)
        {
            _service = service;
            _suggestion = suggestion;
            _comment = comment;
        }

        public Review()
        {
            _service = 0;
            _suggestion = 0;
            _comment = " ";
        }
    }
}
