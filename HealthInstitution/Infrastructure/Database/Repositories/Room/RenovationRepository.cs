using HealthInstitution.Core.Exceptions;
using HealthInstitution.Core;
using HealthInstitution.Core.Services;
using System;
using System.Collections.Generic;

namespace HealthInstitution.Infrastructure.Database.Repositories
{
    public class RenovationRepository : BaseRepository
    {
        private List<Renovation> _renovations;

        public List<Renovation> Renovations { get => _renovations; }

        public RenovationRepository(string fileName)
        {
            _fileName = fileName;
            _renovations = new List<Renovation>();
        }

        public override void LoadFromFile()
        {
            _renovations = FileService.Deserialize<Renovation>(_fileName);

        }

        public override void SaveToFile()
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

        public Renovation Create(DateTime startDate, DateTime endDate)
        {
            if (startDate <= DateTime.Today)
            {
                throw new DateException("Start date must be in future");
            }
            else if (endDate <= DateTime.Today)
            {
                throw new DateException("End date must be in future");
            }
            else if (startDate >= endDate)
            {
                throw new DateException("End date must be after start date");
            }
            return new Renovation(GetID(), startDate, endDate);
        }


        private bool CheckID(int id)
        {
            foreach (Renovation r in _renovations)
            {
                if (r.ID == id) return false;
            }
            return true;
        }

        private int GetID()
        {
            int i = 1;
            while (true)
            {
                if (CheckID(i)) return i;
                i++;
            }
        }

    }
}
