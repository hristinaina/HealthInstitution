using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Repository
{
    public interface IExaminationRelationsService
    {
        public ExaminationReference FindByExaminationID(int id);

        public void Add(Examination examination);

        public void Add(ExaminationReference examinationReference);

        public void Remove(Examination examination);

    }
}
