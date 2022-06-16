using System.Collections.Generic;

namespace HealthInstitution.Core
{
    public interface IExaminationRelationsRepository
    {
        public ExaminationReference FindByExaminationID(int id);

        public void Add(Examination examination);

        public void Add(ExaminationReference examinationReference);

        public void Remove(Examination examination);
        List<ExaminationReference> GetRelations();
    }
}