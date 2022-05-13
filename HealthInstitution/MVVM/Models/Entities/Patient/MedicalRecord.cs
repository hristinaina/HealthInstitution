using HealthInstitution.MVVM.Models.Entities.References;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.Models.Entities
{
    public class MedicalRecord
    {
        private double _height;
        private double _weight;
        private List<Allergen> _allergens;
        private List<Referral> _referrals;
        private List<string> _historyOfIllnesses;

        public double Height { get => _height; set { _height = value; } }
        public double Weight { get => _weight; set { _weight = value; } }
        public List<string> HistoryOfIllnesses { get => _historyOfIllnesses; set { _historyOfIllnesses = value; } }
        [JsonIgnore]
        public List<Allergen> Allergens
        {
            get
            {
                if (_allergens is null) _allergens = new List<Allergen>();
                return _allergens;
            }
            set
            {
                _allergens = value;
            }
        }
       
        [JsonIgnore]
        public List<Referral> Referrals
        {
            get
            {
                if (_referrals is null) _referrals = new List<Referral>();
                return _referrals;
            }
            set
            {
                _referrals = value;
            }
        }

        public MedicalRecord()
        {
        }

        public MedicalRecord(double height, double weight, List<Allergen> allergens)
        {
            _height = height;
            _weight = weight;
            _allergens = allergens;
        }
    }
}
