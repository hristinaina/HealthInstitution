﻿using HealthInstitution.Core.Services;
using System.Collections.Generic;
using HealthInstitution.Core.Repository;

namespace HealthInstitution.Core.Repositories
{
    public class OperationReferencesRepository : BaseRepository, IOperationRelationsRepository
    {
        private List<OperationReference> _references;

        public OperationReferencesRepository(string FileName)
        {
            _fileName = FileName;
            _references = new List<OperationReference>();
        }
        public List<OperationReference> GetReferences()
        {
            return _references;
        }

        public override void LoadFromFile()
        {
            _references = FileService.Deserialize<OperationReference>(_fileName);
        }

        public override void SaveToFile()
        {
            FileService.Serialize<OperationReference>(_fileName, _references);
        }

        public OperationReference FindByOperationID(int id)
        {
            foreach (OperationReference reference in _references)
            {
                if (reference.OperationId == id) return reference;
            }
            return null;
        }

        public void Remove(Operation operation)
        {
            OperationReference reference = FindByOperationID(operation.ID);
            _references.Remove(reference);
        }
        public void Add(Operation operation)
        {
            _references.Add(new OperationReference(operation.ID, operation.Doctor.ID, operation.Patient.ID, operation.Room.ID));
        }
    }
}
