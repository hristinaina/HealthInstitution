using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core
{
    public class ExaminationReview
    {
        private int _service;
        private int _hygene;
        private int _satisfaction;
        private int _suggestion;
        private string _comment;

        [JsonProperty("Service")]
        public int Service { get => _service; set { _service = value; } }
        [JsonProperty("Hygene")]
        public int Hygene { get => _hygene; set { _hygene = value; } }
        [JsonProperty("Satisfaction")]
        public int Satisfacion { get => _satisfaction; set { _satisfaction = value; } }
        [JsonProperty("Suggestion")]
        public int Suggestion { get => _suggestion; set { _suggestion = value; } }
        [JsonProperty("Comment")]
        public string Comment { get => _comment; set { _comment = value; } }

        public ExaminationReview(int service, int hygene, int satisfaction, int suggestion, string comment)
        {
            _service = service;
            _hygene = hygene;
            _satisfaction = satisfaction;
            _suggestion = suggestion;
            _comment = comment;
        }

        public ExaminationReview()
        {
            _service = 0;
            _hygene = 0;
            _satisfaction = 0;
            _suggestion = 0;
            _comment = " ";
        }
    }
}
