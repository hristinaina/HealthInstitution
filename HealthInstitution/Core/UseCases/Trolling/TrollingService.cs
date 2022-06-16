using System.Collections.Generic;

namespace HealthInstitution.Core.Services.DoctorServices
{
    public class TrollingService : ITroll, IChekTroll
    {
        Patient _patient;
        private List<ExaminationChange> _changes;
        public bool IsTrolling(Patient patient)
        {
            if (GetEditingAttempts(patient.ExaminationChanges) > 5 || GetCreatingAttempts(patient.ExaminationChanges) > 8)
            {
                _patient.Block(BlockadeType.SYSTEM);
                return true;
            }
            return false;
        }

        public int GetCreatingAttempts(List<ExaminationChange> changes)
        {
            int totalCreations = 0;
            foreach (ExaminationChange change in changes)
            {
                if (change.ChangeStatus == AppointmentStatus.CREATED)
                {
                    totalCreations++;
                }
            }

            return totalCreations;
        }

        public int GetEditingAttempts(List<ExaminationChange> changes)
        {
            int totalChanges = 0;
            foreach (ExaminationChange change in changes)
            {
                if (change.ChangeStatus == AppointmentStatus.EDITED || change.ChangeStatus == AppointmentStatus.DELETED)
                {
                    totalChanges++;
                }
            }

            return totalChanges;
        }
    }
}
