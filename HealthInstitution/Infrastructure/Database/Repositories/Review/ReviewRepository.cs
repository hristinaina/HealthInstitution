using System.Collections.Generic;
using HealthInstitution.Core.Services;

namespace HealthInstitution.Core.Repositories
{
    public class ReviewRepository
    {
        private readonly string _fileName;
        private List<HospitalReview> _reviews;
        public List<HospitalReview> Reviews { get => _reviews; }
        public ReviewRepository(string patientFileName)
        {
            _fileName = patientFileName;
            _reviews = new List<HospitalReview>();
        }

        public void LoadFromFile()
        {
            _reviews = FileService.Deserialize<HospitalReview>(_fileName);
        }

        public void SaveToFile()
        {
            FileService.Serialize(_fileName, _reviews);
        }
    }
}