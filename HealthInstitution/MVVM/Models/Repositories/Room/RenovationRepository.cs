using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Services;
using System;
using System.Collections.Generic;

namespace HealthInstitution.MVVM.Models.Repositories.Room
{
    public class RenovationRepository
    {
        private readonly string _fileName;
        private List<Renovation> _renovations;

        public List<Renovation> Renovations { get => _renovations; }

        public RenovationRepository(string fileName)
        {
            _fileName = fileName;
            _renovations = new List<Renovation>();
        }

        public void LoadFromFile()
        {
            _renovations = FileService.Deserialize<Renovation>(_fileName);

        }

        public void SaveToFile()
        {
            FileService.Serialize<Renovation>(_fileName, _renovations);
        }

        public Renovation FindById(int id)
        {
            foreach (Renovation r in _renovations)
            {
                if (r.ID == id) return r;
            }
            return null;
        }

        public void StartRenovations()
        {
            foreach(Renovation r in _renovations)
            {
                if (r.StartDate <= DateTime.Today && !r.Started) r.StartRenovation();
            }
        }

        public void EndRenovations()
        {
            List<int> endedRenovations = new List<int>();
            foreach (Renovation r in _renovations)
            {
                if (r.EndDate <= DateTime.Today) endedRenovations.Add(r.ID);
            }

            foreach (int renovationId in endedRenovations) EndRenovation(renovationId);
        }

        public void EndRenovation(int id)
        {
            Renovation r = FindById(id);
            r.EndRenovation();

            List<RoomRenovation> futureRenovations = new List<RoomRenovation>();
            List<RoomRenovation> renovations = Institution.Instance().RoomRenovationRepository.RoomsUnderRenovations;
            foreach (RoomRenovation roomUnderRenovation in renovations)
            {
                if (r.ID != roomUnderRenovation.RenovationId)
                {
                    futureRenovations.Add(roomUnderRenovation);
                }
            }
            Institution.Instance().RoomRenovationRepository.RoomsUnderRenovations = futureRenovations;

            _renovations.Remove(r);
        }
    }
}
