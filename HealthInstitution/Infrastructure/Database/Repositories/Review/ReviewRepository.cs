using System.Collections.Generic;
using HealthInstitution.Core.Repository;
using HealthInstitution.Core.Services;

namespace HealthInstitution.Core.Repositories
{
    public class ReviewRepository : BaseRepository
    {
        private List<HospitalReview> _reviews;
        public List<HospitalReview> Reviews { get => _reviews; }
        public ReviewRepository(string patientFileName)
        {
            _fileName = patientFileName;
            _reviews = new List<HospitalReview>();
        }

        public override void LoadFromFile()
        {
            _reviews = FileService.Deserialize<HospitalReview>(_fileName);
        }

        public override void SaveToFile()
        {
            FileService.Serialize(_fileName, _reviews);
        }
    }
}