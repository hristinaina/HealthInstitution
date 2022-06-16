using HealthInstitution.Core.Exceptions;
using HealthInstitution.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.UseCases.Validation
{
    class PatientSuggestionValidationService
    {

        public void ValidateSuggestionData(ExaminationQuery query)
        {
            {

                if (query.Doctor is null)
                {
                    throw new EmptyFieldException("Doctor not selected !");
                }
                if (DateTime.Compare(DateTime.Now, query.StartTime) > 0)
                {
                    throw new DateException("Start date must be in future !");
                }
                if (DateTime.Compare(DateTime.Now, query.EndTime) > 0)
                {
                    throw new DateException("End must be in future !");
                }
                if (DateTime.Compare(DateTime.Now, query.DeadlineDate) > 0)
                {
                    throw new DateException("Deadline must be in future !");
                }
                if (DateTime.Compare(query.StartTime, query.EndTime) > 0)
                {
                    throw new DateException("Unvalid time interval !");
                }

            }
        }
    }
}
