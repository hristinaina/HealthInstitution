using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Repository
{
    public class ExaminationRelationsRepositoryService : IExaminationRelationsService
    {
        IExaminationRelationsRepository _repository;

        public ExaminationRelationsRepositoryService()
        {
            _repository = Institution.Instance().ExaminationReferencesRepository;
        }

        public void Add(Examination examination)
        {
            _repository.Add(examination);
        }

        public void Add(ExaminationReference examinationReference)
        {
            _repository.Add(examinationReference);
        }

        public ExaminationReference FindByExaminationID(int id)
        {
            return _repository.FindByExaminationID(id);
        }

        public void Remove(Examination examination)
        {
            _repository.Remove(examination);
        }
    }
}
